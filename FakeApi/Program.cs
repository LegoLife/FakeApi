using System.Threading.RateLimiting;
using FakeApi.Abstractions;
using FakeApi.Data;
using FakeApi.Data.Repositories;
using FakeApi.Dto;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<SqlDbContext>(ops =>
{
	ops.UseSqlServer(builder.Configuration.GetConnectionString("ApiDbConnection"));
});
builder.Services.AddScoped<IRepository<ComputerRecord>, ComputerRepository>();
builder.Services.AddScoped<IRepository<VehicleRecord>, VehicleRepository>();
builder.Services.AddScoped<IRepository<MenuItem>, DinnerMenuRepository>();

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();


var app = builder.Build();
app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
	x.DocumentTitle = "Fake Api";
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Use(async (context, next) =>
{
	Log.Logger.Information(context.Request.GetDisplayUrl());
	await next(context);
});

if (app.Environment.IsProduction())
{
	app.UseRateLimiter(new RateLimiterOptions()
	{
		GlobalLimiter = PartitionedRateLimiter.Create<HttpContext,string>(context =>
		{
			
			return RateLimitPartition.GetTokenBucketLimiter<string>("Token",
				_ => new TokenBucketRateLimiterOptions()
				{
					TokenLimit = 5,
					QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
					ReplenishmentPeriod = TimeSpan.FromSeconds(10),
					TokensPerPeriod = 5,
					AutoReplenishment = true,
					QueueLimit = 0
				});
		}),
		RejectionStatusCode = 429
		
	});
}

app.Run();