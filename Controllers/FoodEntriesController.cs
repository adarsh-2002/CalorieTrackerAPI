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
    public class FoodEntriesController : ControllerBase
    {
        private readonly IEntryService<FoodEntry> _entryService;

        public FoodEntriesController(IEntryService<FoodEntry> entryService)
        {
            _entryService = entryService;
        }

        // GET: api/FoodEntries
        [HttpGet("getFoodList")]
        public async Task<ActionResult<IEnumerable<FoodEntry>>> GetFoodEntries(int uid)
        {
            return _entryService.GetAllEntries(uid).OrderByDescending(x=>x.Date).ToList();
        }

        // GET: api/FoodEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodEntry>> GetFoodEntry(int id)
        {
            var foodEntry = _entryService.GetEntry(id);

            if (foodEntry == null)
            {
                return NotFound();
            }

            return foodEntry;
        }

        // PUT: api/FoodEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodEntry(int id, FoodEntry foodEntry)
        {
            if (id != foodEntry.Id)
            {
                return BadRequest();
            }
            try
            {
                _entryService.UpdateEntry(id, foodEntry);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/FoodEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodEntry>> PostFoodEntry(FoodEntry foodEntry)
        {
            try
            {
                _entryService.AddEntry(foodEntry);
                return CreatedAtAction("GetFoodEntry", new { id = foodEntry.Id }, foodEntry);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // DELETE: api/FoodEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodEntry(int id)
        {
            var foodEntry = _entryService.GetEntry(id);
            if (foodEntry == null)
            {
                return NotFound();
            }

            _entryService.DeleteEntry(id);

            return NoContent();
        }
    }
}
