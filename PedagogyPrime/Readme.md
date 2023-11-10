For local development:

# Install the local database
You need to install the mssql server tools. [Link](https://www.youtube.com/watch?v=P6y0R3XzWlc&ab_channel=AmitThinks)
Then, you need the create the local databse. [Link](https://www.youtube.com/watch?v=_12OOgKzi7I&ab_channel=AVKDigital)
After that, you need to create local database using the command **sqllocaldb c [server_name]** from the connection string.
# Adding a new table in the database:
This exemple gives you the basic knowledge about how to add a new table for an entity in the database. 

This exemple create a new Table 'User' into the database.
## Add the entity
```c#
public class User : BaseEntity
{
	public string Username { get; set; }
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Password { get; set; }
	public Role Role { get; set; }
}
```

## Add a new DataSet property into `PedagogyPrimeDbContext.cs` file
```c#
    public DbSet<User> Users => Set<User>();
```

## Run commands for creating the new table into the database
- go to the folder specific for `PedagogyPrime.Persistence`.

- run command `dotnet ef --startup-project '../PedagogyPrime.API' migrations add "MigrationName"`. If this command fails, run `dotnet tool install --global dotnet-ef` to install the **dotnet-ef** tool globally. If the command runs successfully a new migration with the name "yyyyMMddhhmmss_MigrationName". There you will be able to see all changes that will be executed over the database when the update command will run. Please verify if the changes are correct in order to not broke the database.

- run update command `dotnet ef --startup-project '../PedagogyPrime.API' database update`. If the command runs successfully the database should be updated with the changes from the migration file.

- a tutorial on how to manage migrations in .NET [Link](https://www.youtube.com/watch?v=sWhAk2kIBtk&ab_channel=OpariucRaresIoan)