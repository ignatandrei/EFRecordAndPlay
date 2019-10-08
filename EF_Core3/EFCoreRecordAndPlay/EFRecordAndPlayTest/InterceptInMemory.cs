using EFRecordAndPlay;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace EFRecordAndPlayTest
{
    public class InterceptInMemory
    {
        [Fact]
        public void TestPlayReplayInMemory()
        {
            string nameFileToRecord = "a.zip";

            if (File.Exists(nameFileToRecord))
            {
                File.Delete(nameFileToRecord);
            }
           
            var opt = new DbContextOptions<MyDataContext>();
            var optionsBuilder = new DbContextOptionsBuilder(opt);
            optionsBuilder.AddInterceptors(new InterceptionRecordOrPlay(nameFileToRecord, ModeInterception.Record));
            optionsBuilder.UseInMemoryDatabase(databaseName: "testing");
            using var c = new MyDataContext(optionsBuilder.Options as DbContextOptions<MyDataContext>);
            c.PersonWithBlog.Add(new PersonWithBlog()
            {
                Id=1,
                Name = "Andrei",
                Url = "http://msprogrammer.serviciipeweb.ro/"
            });

            c.SaveChanges();
            var q = c.PersonWithBlog.FirstOrDefault(it => it.Id == 1);
            Assert.NotNull(q);
            Assert.False(File.Exists(nameFileToRecord),"in memory is not database relational");
        }
        [Fact]
        public void TestInterceptSqlite()
        {
            #region record
            string nameDbToCreate = "testing.db";
            int nrItems = 200;
            string nameFileToRecord = "a.zip";
            if (File.Exists(nameDbToCreate))
            {
                File.Delete(nameDbToCreate);
            }
            if (File.Exists(nameFileToRecord))
            {
                File.Delete(nameFileToRecord);
            }
            var opt = new DbContextOptions<MyDataContext>();
            var optionsBuilder = new DbContextOptionsBuilder(opt);
            optionsBuilder.AddInterceptors(new InterceptionRecordOrPlay(nameFileToRecord, ModeInterception.Record));
            optionsBuilder.UseSqlite($"Data Source={nameDbToCreate}");

            using (var c = new MyDataContext(optionsBuilder.Options as DbContextOptions<MyDataContext>))
            {
                c.Database.EnsureCreated();
                for (int i = 0; i < nrItems; i++)
                {
                    c.PersonWithBlog.Add(new PersonWithBlog()
                    {
                        Id=i,
                        Name = "Andrei" + i,
                        Url = "http://msprogrammer.serviciipeweb.ro/"
                    });
                }

                c.SaveChanges();
                var person = c.PersonWithBlog.FirstOrDefault(it => it.Id == 1);
                Assert.NotNull(person);
            }
            #endregion

            #region play
            Assert.True(File.Exists(nameFileToRecord));
            File.Delete(nameDbToCreate);
            opt = new DbContextOptions<MyDataContext>();
            optionsBuilder = new DbContextOptionsBuilder(opt);
            optionsBuilder.AddInterceptors(new InterceptionRecordOrPlay(nameFileToRecord, ModeInterception.Play));
            optionsBuilder.UseSqlite($"Data Source={nameDbToCreate}");
            using var cNotExists = new MyDataContext(optionsBuilder.Options as DbContextOptions<MyDataContext>);            

            cNotExists.Database.EnsureCreated();
            for (int i = 0; i < nrItems; i++)
            {
                cNotExists.PersonWithBlog.Add(new PersonWithBlog()
                {
                    Id=i*100,
                    Name = "my test Andrei",
                    Url = "http://msprogrammer.serviciipeweb.ro/"
                });
            }
            cNotExists.SaveChanges();


            var q = cNotExists.PersonWithBlog.FirstOrDefault(it => it.Id == nrItems/2);
            Assert.NotNull(q);
            Assert.DoesNotContain("test", q.Name);
            #endregion

        }

    }
}
