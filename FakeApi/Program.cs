using System.Threading.RateLimiting;
using FakeApi.Abstractions;
using FakeApi.Dto;
using FakeApi.Utils;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<ComputerRecord>, FakeRepo<ComputerRecord>>();
builder.Services.AddScoped<IRepository<VehicleRecord>, FakeRepo<VehicleRecord>>();


Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File($"{Environment.CurrentDirectory}\\logs\\api-.log")
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