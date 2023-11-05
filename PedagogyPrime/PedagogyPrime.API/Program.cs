using PedagogyPrime.Persistence;
using PedagogyPrime.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
	using(var scope = app.Services.CreateScope())
	{
		var db = scope.ServiceProvider.GetRequiredService<PedagogyPrimeDbContext>();
		db.Database.EnsureCreated();
	}
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
