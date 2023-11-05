For local development:

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

- run command `dotnet ef --startup-project '../PedagogiPrime.API migrations add "MigrationName"`. If the command runs successfully a new migration with the name "yyyyMMddhhmmss_MigrationName. There you will be able to see all changes that will be executed over the database when the update command will run. Please verify if the changes are correct in order to not broke the database.

- run update command `dotnet ef --startup-project '../PedagogiPrime.API database update`. If the command runs successfully the database should be updated with the changes from the migration file.