# AybCommerce B2B

AybCommerce is a simple, open source B2B E-Commerce solution.

### Prerequisites

You just need to install the [.net core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2 ".net core 2.2")

### Getting Started
 
###### Screenshots 
<img src="https://raw.githubusercontent.com/arslanaybars/AybCommerce-B2B/master/Screenshots/Login.png" width="800" height="450"/>

[See more screenshots](https://github.com/arslanaybars/AybCommerce-B2B/blob/master/Screenshots/README.md "AybCommerce Screenshots")

###### Credentials
	admin username: admin@admin.com  password: 123123
	customer username: ayb@ars.com  password: 123123

###### Datastore

You can clone then run the project  as use in-memory database.

If you want to run in real sql database then...
1. Set your local connection string in appsettings.json
```json
{
"ConnectionStrings": {
  "AybCommerceConnection": "Server=.;Database=AybCommerce;Trusted_Connection=True;"
  },
}
```
2. Make a migration into your local database. If you set your connection string correct just follow bellow. 

- In Package Manager Console default projet should selected AybCommerce.Persistance.Data
- Startup project must point AybCommerce.UI

 	   Update-Database

3.  You need to comment-out  `ConfigureProductionServices`. 
```csharp
 public void ConfigureDevelopmentServices(IServiceCollection services)
 {
     // use in-memory database
     // ConfigureInMemoryDatabases(services);

     // use real database
     ConfigureProductionServices(services);
 }
```
###### Email Service
Update your MailConfiguration to use MailService
In appsetting.json
```json
{ 
  "MailConfiguration": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-username@gmail.com",
    "Password": "your-password"
  },
}
```


### Solution developed with
- ASP.NET Core MVC 2.2
- ASP.NET Identity 2.2
- SQL Server
- EF 2.2
- jQuery , JavaScript
- [jQuery DataTables](https://datatables.net/ "jQuery DataTables")
- and so on...

### License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details

## Acknowledgments

* [Colorlib](https://colorlib.com/ "Colorlib")
