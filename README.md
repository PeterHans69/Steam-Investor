# [Dowload here](http://download1649.mediafire.com/5kb5pe5967lg/si5ku94ddr7848g/Setup.msi) Steam Investor v1  (Pre-Aplha!)
 
Join the [Discord](https://discord.gg/x4kuTWW) if you have any questions or problems.

# For The programmers
I know the code is messy, its my first project I have ever ended.
Feel free to improve it!

Installing the application with visual studio:
-Go to "MyPathes.cs" and change these variables to:
  -"public static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);"
  -"public static string MySteamItems = path + "\\MySteamItems.json";"
-After that go to "App.manifest" and change "<requestedExecutionLevel level="asInvoker" uiAccess="false" />" to "<requestedExecutionLevel level="requireAdministrator" uiAccess="false" />" so the application can save the users data.
