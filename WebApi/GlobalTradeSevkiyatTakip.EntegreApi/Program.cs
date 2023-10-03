using GlobalTradeSevkiyatTakip.Application.Registrations;
using GlobalTradeSevkiyatTakip.Persistance.Registrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ApiCorsPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
