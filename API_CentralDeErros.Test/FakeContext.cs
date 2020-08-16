using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API_CentralDeErros.Test
{
    public class FakeContext
    {
        public DbContextOptions<CentralContext> FakeOptions { get; }
        public readonly IMapper Mapper;

        private Dictionary<Type, string> DataFileNames { get; } =
            new Dictionary<Type, string>();

        private string FileName<T>()
        {
            return DataFileNames[typeof(T)];
        }

        public FakeContext(string testName)
        {
            FakeOptions = new DbContextOptionsBuilder<CentralContext>()
                .UseInMemoryDatabase(databaseName: $"CentralErrors_{testName}")
                .Options;
            DataFileNames.Add(typeof(Alert), $"FakeData{Path.DirectorySeparatorChar}alert.json");
            DataFileNames.Add(typeof(AlertDTO), $"FakeData{Path.DirectorySeparatorChar}alert.json");

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();

            });

            this.Mapper = configuration.CreateMapper();
        }

        public void FillWith<T>() where T : class
        {
            using (var context = new CentralContext(FakeOptions))
            {
                if (context.Set<T>().Count() == 0)
                {
                    foreach (T item in GetFakeData<T>())
                        context.Set<T>().Add(item);
                    context.SaveChanges();
                }
            }
        }

        public List<T> GetFakeData<T>()
        {
            string content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }
    }
}
