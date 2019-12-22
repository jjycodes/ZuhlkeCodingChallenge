This is my best effort to the Zuhlke CSV Coding Challenge.

## About the app
The console app is named ZuhlkeCodingChallenge and I have provided shortcuts to both a windows (win-x64 scd - Shortcut) 
and a linux self (linux-x64 scd - Shortcut) contained deployments. Along with executables is a copy of the provided sample data (sales.csv)
which can be added as a command line argument, or can be manually input when prompted.

## Comments / Observations
Upon reading the requirements, I noticed  right away that there are no explicit mention of any validations when processing the data.I assumed that for any implementation to any business logic, it is a must to handle errors and exceptions, and somehow need to be presented, esp if there are non technical users of your app, like a Store.

I also observed the provided mysql schema for the sales table, and got confused for quite a while on the unique constraints provided. Having separate unique constraints across the OrderID, ProductID and Customer ID, guarantees that there will only be one record for a certain customer, one for a certain order, and one for a certain product. To me, it made more sense for a sales implementation to have this as a single unique constraint applied to these columns as a group, such that an order can have multiple products from one customer, and a customer can have multiple orders, etc. I provided a base implementation for the latter, and a custom override for the former since it appeared to me as the constraints provided are what is being required.

## Design / Approach
Being recommended as a good engineering practice, i favored a composition approach over inheritance. 
The app consists of at least 4 parts - database context, validation, business service and a datasource. 

First, I created a data model that we will be interacting with, and chose Entity Framework. The database context is associated to anything that binds and persists data to the database. I chose to inject a factory, instead of the instance itself, following Microsoft recommendations about the dbcontext, only creating it only when necessary. Clean decoupled code.
A business service is my implementation to a business feature stack. I made sure that the methods it prescribes, have a single responsibility on the model it acts upon. These business services could be aggregated into the main StoreController code, which are simply injected on its constructor. 
I chose to compose a service with its own validation helper/service, since it works on the datamodel, might as well validate them, and catch any data errors that do not conform to the constraints. As mentioned above, the CustomSalesValidationService is the representation of the prescribed database constraints, and I worked on a simple logic to identify which ones among the sales data are valid or not, If valid, then those are written into the database. If not, then they are collected as logs and displayed as business exceptions. and because constraints, are also enforced in the table, succeeding duplicates, will be eventually be caught as system exceptions, when entity framework throws them.

I made sure that whenever there are changes to requirements, the setup of this app should be as easy any plug and play, as well as its extensibility.
When there is a new / different database to connect, simply implement the IDatabaseContext and simply create a mapping of the business entity, as well as providing its own DBContextOptions.
If there is a different datasource, then I made sure that the services and validation classes work with the base class, and anything new should simply inherit from that. I believe that as more complexity is introduced in the system, common properties and implementations should be written in a base class. 
In cases of new Store module implementations, like a Product Inventory or Customers, then simply create business services and domain models to represent them, register them in the DI service collection, and inject them as new components to main StoreController. 
Finally, as to what comes with any inversion of control approach, tests and mocks are provided to ensure units remain loosely coupled and independent of each other. 
There may be variants to this overall approach in one way or another, and I believe there is no perfect implementation, only efficient ones, so, I'd be happy and open to discuss for any suggestions.

## About
Author : Jew Jason Yap
Date : 12/2/2019
Technologies used : C#, .Net Core 2.2, EF Core 2.2, CSVHelper, MSSQL 2016