using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using finalproj.BL;

namespace finalproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        // GET: api/<CalendarEventsController>
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<CalendarEvent>> Get(int userId)
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            DBservicesEvent dbService = new DBservicesEvent();
            return dbService.Read(userId);
        }

        // GET api/<CalendarEventsController>/5
        [HttpGet("{eventId}/{userId}")]
        public IActionResult Get(int eventId, int userId)
        {
            DBservicesEvent dbService = new DBservicesEvent();
            CalendarEvent calendarEvent = dbService.ReadOne(eventId, userId);

            if (calendarEvent != null)
            {
                return Ok(calendarEvent);
            }
            else
            {
                return NotFound("Event not found");
            }
        }

        // POST api/<CalendarEventsController>
        [HttpPost]
        public IActionResult Post([FromBody] CalendarEvent calendarEvent)
        {
            DBservicesEvent dbService = new DBservicesEvent();
            int result = dbService.Insert(calendarEvent);

            if (result > 0)
            {
                return Ok(calendarEvent); // Return the created event
            }
            else
            {
                return BadRequest("Event could not be added");
            }
        }

        // PUT api/<CalendarEventsController>/5
        [HttpPut("{eventId}")]
        public IActionResult Put(int eventId, [FromBody] CalendarEvent calendarEvent)
        {
            calendarEvent.EventId = eventId;
            DBservicesEvent dbService = new DBservicesEvent();
            int result = dbService.Update(calendarEvent);

            if (result > 0)
            {
                return Ok(calendarEvent); // Return the updated event
            }
            else
            {
                return BadRequest("Event not updated");
            }
        }

        // DELETE api/<CalendarEventsController>/5
        [HttpDelete("{eventId}/{userId}")]
        public IActionResult Delete(int eventId, int userId)
        {
            DBservicesEvent dbService = new DBservicesEvent();
            CalendarEvent calendarEvent = new CalendarEvent { EventId = eventId, UserId = userId };
            bool result = dbService.Delete(calendarEvent);

            if (result)
            {
                return Ok("Event deleted successfully");
            }
            else
            {
                return NotFound("Event not found or not deleted");
            }
        }
    }
}
