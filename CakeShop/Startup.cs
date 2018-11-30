using CakeShop.Domain.Models;
using CakeShop.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICakeRepository, CakeRepository>();
            services.AddSingleton<IMuffinRepository, MuffinRepository>();

            SetInitialData(services);
        }

        private static void SetInitialData(IServiceCollection services)
        {
            var cakeRepo = services.BuildServiceProvider().GetService<ICakeRepository>();
            if (cakeRepo.All().Any())
            {
                return;
            }
            cakeRepo.Add(new Cake
            {
                Name = "Death by cake",
                CaloriesPerSlice = 666,
                Recipe = "Mix ingredients together. Put into a lined cake tin and bake at 200 degrees celcius for 25 minutes.",
                Creator = "Shane S.",
                Ingredients = new List<string> { "250g self raising flour", "1kg of sugar", "500g butter", "100ml milk", "1 egg" },
                Id = Guid.Parse("2388afc4-92c7-470d-9afe-797b1a9258bd"),
                Price = 4,
                RecipePrice = 50,
                Added = DateTime.UtcNow
            }
);
            cakeRepo.Add(new Cake
            {
                Name = "The wrong cake",
                CaloriesPerSlice = 666,
                Recipe = "Mix ingredients together by hand. Put into a lined cake tin and bake at 150 degrees celcius for 30 minutes.",
                Creator = "Mat E.",
                Ingredients = new List<string> { "250g self raising flour", "1 Banana", "2 oranges", "100ml milk", "2 eggs" },
                Id = Guid.Parse("d17eeb84-6d52-49b7-b30c-f4b041016aca"),
                Price = 12,
                RecipePrice = 50,
                Added = DateTime.UtcNow
            });

            cakeRepo.Add(new Cake
            {
                Name = "The right cake",
                CaloriesPerSlice = 666,
                Recipe = "Mix all ingredients together by hand apart from the chocolate. Put into a lined cake tin and bake at 150 degrees celcius for 30 minutes. Melt the chocolate and pour over the top. Allow 2 hours to set in the fridge.",
                Creator = "Mat E.",
                Ingredients = new List<string> { "250g self raising flour", "1kg of sugar", "500 grams chocolate", "500g butter", "100ml milk", "1 egg" },
                Id = Guid.Parse("21869e4f-c3ab-4a19-b7fe-d8c0a99773ae"),
                Price = 2,
                RecipePrice = 50,
                Added = DateTime.UtcNow
            });

            var muffinRepo = services.BuildServiceProvider().GetService<IMuffinRepository>();

            muffinRepo.Add(new Muffin
            {
                Name = "The right Muffin",
                Creator = "Mat E.",
                Ingredients = new List<string> { "250g self raising flour", "1kg of sugar", "500 grams chocolate", "500g butter", "100ml milk", "1 egg" },
                Id = Guid.Parse("21869e4f-c3ab-4a19-b7fe-d8c0a99773ae"),
                Price = 2,
                Added = DateTime.UtcNow
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
