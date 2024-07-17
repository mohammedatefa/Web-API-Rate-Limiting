
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Web_API_Rate_Limiting.Context;
using Web_API_Rate_Limiting.Repository;
using Web_API_Rate_Limiting.Utilitis.MapperProfiles;
using Web_API_Rate_Limiting.Utilitis.UOW;

namespace Web_API_Rate_Limiting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UniversityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            });
            //Add Services
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //add api rate limit

            #region FixedWindow Api Rate Limite
            builder.Services.AddRateLimiter(options =>
                {
                    options.AddFixedWindowLimiter("FixedLimiterPolicy", option =>
                    {
                        //the time that requests take to be handle
                        option.Window = TimeSpan.FromSeconds(5);
                        //the number of requests 
                        option.PermitLimit = 5; //thats mean 5 Requests/5s
                                                //the number of request stored in the queue 
                        option.QueueLimit = 10;
                        //the way that requests handle from queue FIFO or LIFO
                        option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

                    });
                    //that is mean too many requests error 
                    options.RejectionStatusCode = 429;
                });
            #endregion

            #region SlidingWindow Api Limite
            builder.Services.AddRateLimiter(options =>
            {
                options.AddSlidingWindowLimiter("SlidingWindowLimiterPolicy", option =>
                {
                    option.Window = TimeSpan.FromSeconds(3);
                    option.PermitLimit = 2;
                    option.QueueLimit = 2;
                    option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    //that mean when the window have 5 then end the window and slide to the next to save time
                    option.SegmentsPerWindow = 5;

                });
                options.RejectionStatusCode = 429;
            });
            #endregion

            #region Concurrency Api Rate Limite
            builder.Services.AddRateLimiter(options =>
            {
                options.AddConcurrencyLimiter("ConcurrencyLimiterPolicy", option =>
                {
                    option.PermitLimit = 5;
                    option.QueueLimit = 10;
                    option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
                options.RejectionStatusCode = 429;
            }); 
            #endregion

            //using automapper
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }    
            app.UseHttpsRedirection();
            app.UseRateLimiter();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
