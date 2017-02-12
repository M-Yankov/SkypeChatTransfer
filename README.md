# SkypeChatTransfer
Transfer chat messages from one account to another

## Pre-requests 

 - Windows (10 is recommended)
 - Installed Visual Studio 2015 with .NET 4.5
 - Knowledge with C# and Entity framework
 - Very good knowledge and understanding in databases
 - SQL is advantage
 - Installed [sqlite-netFx46-setup-bundle-x86-2015-1.0.104.0.exe](http://system.data.sqlite.org/downloads/1.0.104.0/sqlite-netFx46-setup-bundle-x86-2015-1.0.104.0.exe) (VS 2015)
 - Db Browser for SQLite (optional) 

Skype is using SQLite for database where all messages, contacts etc. are stored.   
The database is located at `%appdata%/skype/<your skype name>/main.db`. Make copies of databases and work with them.

## The Problem

You have two Skype accounts and have same contact in both.  
This project shows how to transfer messages from the old account conversation to the new. 


For example: I have Skype account (`yankov`) with contact named `John` having a long conversation. Also I have another Skype account (M.Yankov) where the same `John` is in my contacts list.  
So I want to transfer messages for conversation with `John` from `yankov` to `M.Yankov`.

I didn't find any ready solution for this problem. And also i want to be like a real message not like: 
```
[Friday, 10 February, 2017 18:18] John: Skype screen share?
[Friday, 10 February, 2017 18:19] yankov: Meeting will be after 2 hrs.
```

## Notes And Good To Know

The solution is not 100% tested, but it works. Always make a backup files before start doing it.
Maybe it cannot solve your problem, but it can be a good beginning.  
This is only a demo. You can find better solution. If you have questions post an issue.

### Add the old data account

- Install `System.Data.SQLite`. It will install:
  - `System.Data.SQLite.Core`
  - `System.Data.SQLite.EF6`
  - `System.Data.SQLite.Linq`
- Add new item in external class library project
- `Select ADO.NET Entity Data Model`
- EF Designer From Database
- New Connection
- System.Data.SQLite Database File
- Browser the `main.db` file, Test connection, click Ok (Keep in mind that the appliaction may need access to the directory)
- Make sure `Save connection settings in App.Config as:` is checked
- Click Next
- Check `Tables` click `Finish`
- Click `OK` on the security warning window (twice)

Warning after generating EF models

**C# Error	CS0102	The type 'Account' already contains a definition for 'set_availability'**

To fix the error:
- Open generated `.edmx` file.
- Rename `set_availability`
- Save the diagram and confirm if security window shows.
- Ensure build is successful

#### Do the same steps for new skype account 


**Don't forget to add connection strings in the main `App.Config` in Console appliaction. (Start up appliaction)**



