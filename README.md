EFRecordAndPlay
===============

Entity Framework Record And Play


With this helper you can record and then play the actions in Entity Framework(>= 6).

For recording actions just reference the dll and use

    DbInterception.Add(new InterceptionRecordOrPlay(@"C:\filme\a.zip", ModeInterception.Record)); 

(Note: For ASP.NET you will use Server.MapPath("~/a folder that supports write/namefile.zip")

For replay use 

    DbInterception.Add(new InterceptionRecordOrPlay(@"C:\filme\a.zip", ModeInterception.Play));

This can be use for 

1. Unit Testing
2. Making demos
3. Recording user actions when a bug occured 

See [https://github.com/ignatandrei/EFRecordAndPlay/wiki/](https://github.com/ignatandrei/EFRecordAndPlay/wiki/)

Video for using at [https://www.youtube.com/playlist?list=PL4aSKgR4yk4Mi1eLKArsgoqQRluxXv2-Y](https://www.youtube.com/playlist?list=PL4aSKgR4yk4Mi1eLKArsgoqQRluxXv2-Y)

NuGET package at [https://www.nuget.org/packages/EFRecordAndPlay/](https://www.nuget.org/packages/EFRecordAndPlay/)

Tutorial series at 
[http://msprogrammer.serviciipeweb.ro/category/howto/entity-framework-howto/](http://msprogrammer.serviciipeweb.ro/category/howto/entity-framework-howto/)

# Support this software

This software is available for free and all of its source code is public domain.  If you want further modifications, or just to show that you appreciate this, money are always welcome.

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://paypal.me/ignatandrei1970/25)

* $5 for a cup of coffee
* $10 for pizza 
* $25 for a lunch or two
* $100+ for upgrading my development environment

