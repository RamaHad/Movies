using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add DbContext Services
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionstring));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//my services
builder.Services.AddTransient<IGenresServices, GenresServices>();
builder.Services.AddTransient<IMoviesServices, MoviesServices>();
builder.Services.AddAutoMapper(typeof(Program)); //type of program to can use mapper at any class in the project



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
