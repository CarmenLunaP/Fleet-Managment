using Fleet_Managment.Contex;
using Microsoft.EntityFrameworkCore;

// creating the web aplication
var builder = WebApplication.CreateBuilder(args);

// Configuration Data base using Entity Famework Core
builder.Services.AddDbContext<ContexBD>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services (controller, endpoints, swagger,  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ApplicationBuilder 
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); // http and https
app.UseAuthorization(); // police autoritation
app.MapControllers(); // map MCV to http rout

// run aplication

app.Run(); 

