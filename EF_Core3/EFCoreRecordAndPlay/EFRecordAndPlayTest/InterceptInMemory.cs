using System;
using System.Linq;
using Xunit;

namespace EFRecordAndPlayTest
{
    public class InterceptInMemory
    {
        [Fact]
        public void TestPlayReplayInMemory()
        {
            var c = new MyDataContext();
            c.PersonWithBlog.Add(new PersonWithBlog()
            {
                Name = "Andrei",
                Url = "http://msprogrammer.serviciipeweb.ro/"
            });

            c.SaveChanges();
            var q = c.PersonWithBlog.FirstOrDefault(it => it.Id == 1);
            Assert.NotNull(q);
        }
    }
}
