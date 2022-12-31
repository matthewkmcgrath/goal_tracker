using GoalTracker.Models;
using GoalTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase
{
    public GoalController()
    {
    }

    // GET action that gets all goals
    [HttpGet]
    public ActionResult<List<Goal>> GetAll() =>
        GoalService.GetAll();

    // GET action that requires an id parameter and gets a singular goal
    [HttpGet("{id}")]
    public ActionResult<Goal> Get(int id)
    {
	var goal = GoalService.Get(id);

	if(goal == null)
            return NotFound();

	return goal;
    }

    // POST action that allows users to post new items to endpoint
    [HttpPost]
    public IActionResult Create(Goal goal)
    {            
        GoalService.Add(goal);
	return CreatedAtAction(nameof(Create), new { id = goal.Id }, goal);
    }

    // PUT action that allows users to update items
    [HttpPut("{id}")]
    public IActionResult Update(int id, Goal goal)
    {
        if (id != goal.Id)
            return BadRequest();
           
        var existingGoal = GoalService.Get(id);
        if(existingGoal is null)
            return NotFound();
   
        GoalService.Update(goal);           
   
        return NoContent();
    }

    // DELETE action that allows users to delete items
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var goal = GoalService.Get(id);

        if (goal is null)
            return NotFound();

        GoalService.Delete(id);

        return NoContent();
    }
}
