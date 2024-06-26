using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MouseController : ControllerBase
    {
        private static List<Mouse> _mice = new List<Mouse>
        {
            new Mouse { ID = 1, Model = "Logitech MX Master 3", DPI = 4000 },
            new Mouse { ID = 2, Model = "Razer DeathAdder", DPI = 6400 },
            new Mouse { ID = 3, Model = "MSI Clutch GM20", DPI = 6000},
        };

        [HttpGet]
        public ActionResult<IEnumerable<Mouse>> Get()
        {
            return _mice;
        }

        [HttpPost]
        public ActionResult<Mouse> Post(Mouse mouse)
        {
            mouse.ID = _mice.Any() ? _mice.Max(m => m.ID) + 1 : 1;
            _mice.Add(mouse);
            return CreatedAtAction(nameof(Get), new { id = mouse.ID }, mouse);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var mouse = _mice.FirstOrDefault(m => m.ID == id);
            if (mouse == null)
            {
                return NotFound();
            }

            _mice.Remove(mouse);
            return NoContent();
        }
    }
}
