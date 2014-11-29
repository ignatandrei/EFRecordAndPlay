EFRecordAndPlay
===============

Entity Framework Record And Play


With this helper you can record and then play the actions in Entity Framework(>= 6).

For recording actions just reference the dll and use

DbInterception.Add(new InterceptionRecordOrPlay(@"C:\filme\a.zip", ModeInterception.Record)); 


For replay use 

DbInterception.Add(new InterceptionRecordOrPlay(@"C:\filme\a.zip", ModeInterception.Play));

This can be use for 

1. Unit Testing
2. Making demos
3. Recording user actions when a bug occured 
