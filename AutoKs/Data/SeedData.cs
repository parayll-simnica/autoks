using AutoKs.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoKs.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            if (!context.CarsList.Any())
            {

                var test = JsonConvert.DeserializeObject<DataContainer>(File.ReadAllText(@".\Data\carbrands.json"));
                foreach (var item in test.results)
                {
                    item.Id = Guid.NewGuid();
                    context.Add(item);
                }
                context.SaveChanges();
            }
        }
    }

    public class DataContainer
    {

        public List<CarList> results { get; set; }

    }


}
