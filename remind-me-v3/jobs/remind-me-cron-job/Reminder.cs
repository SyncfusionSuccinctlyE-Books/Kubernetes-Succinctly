using System;

namespace remind_me_cron_job
{
    public class Reminder
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
    }
}