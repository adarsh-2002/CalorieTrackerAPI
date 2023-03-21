using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Services;
using CalorieTrackerWeb.Models;

namespace CalorieTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IEntryService<Workout> _service;

        public WorkoutsController(IEntryService<Workout> service)
        {
            _service = service;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts(int uid)
        {
            return _service.GetAllEntries(uid);
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(int id)
        {
            var workout = _service.GetEntry(id);

            if (workout == null)
            {
                return NotFound();
            }

            return workout;
        }

        // PUT: api/Workouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int id, Workout workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }
            try
            {
                _service.UpdateEntry(id, workout);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Workouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(Workout workout)
        {
            try
            {
                _service.AddEntry(workout);
                return CreatedAtAction("GetWorkout", new { id = workout.Id }, workout);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = _service.GetEntry(id);
            if (workout == null)
            {
                return NotFound();
            }

            _service.DeleteEntry(id);
            return NoContent();
        }
    }
}
