using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using testScopeContext.Models;

namespace testScopeContext.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HomeController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IActionResult Index()
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            var myDbContext = serviceScope.ServiceProvider.GetRequiredService<IMyDbContext>();
            var count = myDbContext.Customers.Count();

            return Content($"OK - {count}");
        }

        [HttpGet("/data")]
        public IActionResult Data()
        {
            var range = Enumerable.Range(1, 100);

            Parallel.ForEach(range, async i =>
            {
                using var serviceScope = _serviceScopeFactory.CreateScope();
                var myDbContext = serviceScope.ServiceProvider.GetRequiredService<IMyDbContext>();

                await myDbContext.Customers.AddAsync(new Customers
                {
                    NAME = "name" + i,
                    ADDRESS = "addr " + i,
                    Salary = 10000 + i
                });

                await myDbContext.SaveChangesAsync();
            });

            return Content("ok");
        }
    }
}