# iOS-Xamarin-MvvmCross-Social-Network
This project shows how to create iOS app with adaptive design via Xamarin Framework and MvvmCross pattern and Singleton design pattern.
This app can:
1. Working with SQLite database with 2 tables: Users and Tasks.  
2. The project was built using principles of Mvvm-pattern, there is core project with back end (Models, View models), and ios project with    Views.
3. This app has followed all the major paradigms of the OOP, ViewModels inherit BaseViewModel and overrides virtual methods from base 
   class, all services are inherited from other ViewModels, code was written in according with common code style, all methods and fields      has their right access modifier.
4. In this project was implemented a lot of things: SQL-requests, regular expressions, enums, generic methods and collections,
   delegates and events, LINQ expressions, data binding (via specific MvvmCross form), dispose of threads, attributes, late binding, asynchronous requests, parsing data stored in JSON from news API, services to interact with database, also in this project was 
   implemented service to get access to the system in order add/change profile picture.
