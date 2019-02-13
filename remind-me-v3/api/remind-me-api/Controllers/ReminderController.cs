using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using remind_me_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace remind_me_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly string connectionString;

        public ReminderController(IConfiguration configuration) => this.connectionString = configuration["dbPath"];

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (var db = new LiteDatabase(this.connectionString))
            {
                var remindersCollection = db.GetCollection<Reminder>("reminders");
                remindersCollection.Delete(r => r.Id == id);
            }
        }

        // GET api/reminder/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<Reminder>> Get()
        {
            using (var db = new LiteDatabase(this.connectionString))
            {
                var remindersCollection = db.GetCollection<Reminder>("reminders");
                return remindersCollection.FindAll().ToList();
            }
        }

        // POST api/reminder
        [HttpPost]
        public void Post([FromBody] Reminder value)
        {
            using (var db = new LiteDatabase(this.connectionString))
            {
                var remindersCollection = db.GetCollection<Reminder>("reminders");
                value.Id = Guid.NewGuid().ToString();
                remindersCollection.Insert(value);
            }
        }
    }
}