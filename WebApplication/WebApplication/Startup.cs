using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repos;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc((opt)=> opt.EnableEndpointRouting =false);
            services.AddTransient<TrainerLogic, TrainerLogic>();
            services.AddTransient<ClientLogic, ClientLogic>();
            services.AddTransient<WorkoutDetailLogic, WorkoutDetailLogic>();
            services.AddTransient<ExtraInfoLogic, ExtraInfoLogic>();

            services.AddTransient<IRepoBase<GymClient>, ClientRepository>();
            services.AddTransient<IRepoBase<Trainer>, TrainerRepository>();
            services.AddTransient<IRepoBase<WorkoutDetail>, DetailRepository>();
            services.AddTransient<IRepoBase<ExtraInfo>, InfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMvcWithDefaultRoute();
        }
    }
}
