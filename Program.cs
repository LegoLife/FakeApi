using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
	x.DocumentTitle = "Fake Api";
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

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