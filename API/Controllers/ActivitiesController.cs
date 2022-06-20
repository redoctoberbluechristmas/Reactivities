using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController   // The endpoint will be "activities" because we are using the route "controller" in superclass.
    {
        private readonly DataContext _context;
        public ActivitiesController(DataContext context) // Inject database inside controller.
        {
            _context = context;
        }

        [HttpGet]  // Endpoint
        public async Task<ActionResult<List<Activity>>> GetActivities() // Task will return an ActionResult, with Type parameter of List, which will be of Type Activity.
        {
            return await _context.Activities.ToListAsync();
        }  

        [HttpGet("{id}")]  // Allow user to select an individual Activity by passing in root parameter "{}" - will 
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id); // find Activity with ID we're passing in.
        }
    }
}