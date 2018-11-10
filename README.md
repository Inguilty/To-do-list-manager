# iOS-Xamarin-MvvmCross-Social-Network
This project demonstrates how to create iOS app with adaptive design via Xamarin Framework and MvvmCross pattern and Singleton design pattern
1. Working with SQLite database with 2 tables: Users and Tasks.  
2. The project was built using principles of Mvvm-pattern, there is core project with back and (Models, View models),
   and ios project with Views
3. This app has followed all the major paradigms of the OOP, all ViewModels inherit BaseViewModel and overrides virtual methods from base 
   class,all services are inherited from other ViewModels, all was written in according with common code style, all methods and fields has 
   their right access modifier.
4. Also in this project was implemented a lot of things: SQL-requests, regular expressions, enums, generic methods and collections, delegates and events,
   LINQ expressions, data binding (via specific MvvmCross form), dispose of threads, attributes, late binding, asynchronous requests,
   parsing data stored in JSON from news API, services to interact with database, also was implemented service to get access to the system 
   to add/change profile picture
