using System;

namespace remind_me_api.Models
{
    public class Reminder
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
    }
}
