using Gateway.IServices;
using Gateway.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IFlightServices, FlightServices>();
builder.Services.AddScoped<ITicketServices, TicketServices>();
builder.Services.AddScoped<IPrivilegeServices, PrivilegeServices>();
builder.Services.AddScoped<IPrivilegeHistoryServices, PrivilegeHistoryServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
