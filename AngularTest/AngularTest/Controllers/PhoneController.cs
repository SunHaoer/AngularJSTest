using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Models;

namespace AngularTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly PhoneContext _context;

        public TodoController(PhoneContext context)
        {
            _context = context;

            if (_context.Phones.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Phones.Add(new Phone { PhoneUser = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetTodoItems()
        {
            return await _context.Phones.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> GetTodoItem(long id)
        {
            var todoItem = await _context.Phones.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Phone>> PostTodoItem(Phone todoItem)
        {
            _context.Phones.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, Phone todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phone>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Phones.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
    }
}