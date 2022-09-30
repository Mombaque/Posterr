Diogo Mombaque Borges

Run the .bat file in Docker/run-docker-compose.bat to create the containers. It will create the SQL Server, API and mssql-tools containers. The API can be accessed in localhost:8000/swagger or through the Postman collections avaiable here.

This project was built with a lot of good programming practices. All the core elements were separated in subprojects, each one with a responsability. 

- BuildingBlocks
	It has all the core functionalities for the controllers, commands and repositories. It could be a nuget package to be reused in different projects.

- Posterr.Domain

	Here are all the business logic.
	
		I used the Command pattern, which has  CommandHandlers that process its own commands. In this project I saw the need to create just one, the UserCommandHandler, because all the commands were user related. If this project had authentication logic, I would create a LoginCommandHandler, and so on. FluentValidation was used for command's properties validations.
	
		When the command is executed, the validations are performed. If it is not valid, a notification will be created (this notification will be used in the API later). Other validations are performed as needed, like checking if the a user exists in the database with the userId informed by the command.
		
		The CommandHandler uses some classes through dependency injection, like repositories, mediatorHandler and UnitOfWork, all abstracted. This abstraction allows mock use in the command unit testing. I will explain about the tests later.
		
		When a commit needs to be done, the Commit method, which is in the parent class, will use UnitOfWork for this operation.
		
		Entities are in the Models folder. All entities inherit from a base class, wich has its Id type abstracted. Each entity represents a table in the database. All properties have the "set" as "protected". This way, only classes that inherit it can change its value (necessary in tests becausa of the builder pattern). The post limit validation was performed in the User entity, because it could be reused in other commands. 
	
- Posterr.InfraData

	Here are the repositories implementation, using Entity Framework for queries. They are used as abstractions, so the interfaces are in Posterr.Domain. Is also has the context and mappings needed.
	
	All queries are covered with unit tests.
	
- Posterr.DataBase

	It is a SQL project with all the tables creation and database seed. It generates scripts automatically for this. There is no need to worry about a lot of database operations, like ALTER TABLE. For instance, if a column has its type changed, the project will create the ALTER TABLE by itself, in a script generated when the project is published.
	
- Posterr.Test

	Here are all the unit testing. It is organized by command and repository tests. The Builder Pattern was used for mock creation. 
	
	When testing a command, it is possible to assert if the command is being validated (through FluentValidation), or if a specific class used a specific method, with specific parameters. It is also possible to assert how many times this method was used.
	
	Repository tests needed a in memory database, which is created when the tests are. The same builder pattern was used for mocks.

- Critique
I tried to publish the database scripts (table creation and seed, generated automatically) through the SQL Project in the container, but it was working just when I publish manually in Visual Studio. So I found a solution for this, which is using the mssql-tools image for the script execution in docker-compose. But the right way for doing this is letting the database project do all the work.

I could use RabbitMQ


