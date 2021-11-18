using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialsBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TutorialsController : ControllerBase
    {
        private static List<Tutorial> _tutorials;
        private readonly ILogger<TutorialsController> _logger;

        static TutorialsController()
        {
            _tutorials = new List<Tutorial>
            {
                new Tutorial
                {
                    Id = 1,
                    Title = "TutorialA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Description = "Test",
                    Published = true
                }
            };
        }

        public TutorialsController(ILogger<TutorialsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task AddTutorial(Tutorial tutorial)
        {
            tutorial.Id = _tutorials.Count + 1;
            tutorial.CreatedAt = DateTime.Now;
            tutorial.UpdatedAt = DateTime.Now;
            _tutorials.Add(tutorial);
        }

        [HttpGet]
        public List<Tutorial> GetTutorialsList()
            => _tutorials;

        [HttpDelete("{id}")]
        public void DeleteTutorialById(int id)
        {
            var item = _tutorials.FirstOrDefault(x => x.Id == id);
            if(item != null)
            {
                _tutorials.Remove(item);
            }
        }

        [HttpGet("{id}")]
        public async Task<Tutorial> GetTutorialAsync(int id)
            => _tutorials.FirstOrDefault(x => x.Id == id);

        [HttpPut]
        public async Task<Tutorial> UpdateTutorial(Tutorial tutorial)
        {
            var item = _tutorials.FirstOrDefault(x => x.Id == tutorial.Id);
            int index = -1;
            if(item != null)
            {
                index = _tutorials.IndexOf(item);
                tutorial.UpdatedAt = DateTime.Now;
                _tutorials[index] = tutorial;
            }

            return index != -1 ? _tutorials[index] : null;
        }

    }
}
