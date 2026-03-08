using System.Text.Json.Serialization;
using ConstrutoraApi.Data;
using ConstrutoraApi.Interfaces;
using ConstrutoraApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                });
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ConstrutoraDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IExcelService, ExcelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
