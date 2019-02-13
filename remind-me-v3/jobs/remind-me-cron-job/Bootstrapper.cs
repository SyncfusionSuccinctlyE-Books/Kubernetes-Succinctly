using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace remind_me_cron_job
{
    public class Bootstrapper
    {
        private readonly IRestClient client;

        public Bootstrapper(IRestClient client)
        {
            this.client = client;
        }

        public void Start(string[] args)
        {
            var getRemindersRequest = new RestRequest("/api/Reminder/all", Method.GET);
            var response = this.client.Execute<List<Reminder>>(getRemindersRequest);
            if (null == response.Data)
            {
                return;
            }

            var remindersToSend = response.Data.Where(r => r.DueDate.Date == DateTime.Today.Date);
            foreach (var reminder in remindersToSend)
            {
                this.SendMail(reminder);
                var deleteRemindersRequest = new RestRequest($"/api/Reminder/{reminder.Id}", Method.DELETE);
                this.client.Execute(deleteRemindersRequest);
                Console.WriteLine("Reminder deleted");
            }
        }

        private void SendMail(Reminder reminder)

        {
            var messageBody = $"This is Remind Me Mail Service. " +
                              $"I would like to remind you about: {reminder.Title} which is due on {reminder.DueDate:D}.";
            Console.WriteLine("Mail To: user@kubernetessuccinctly.com From: Remind Me Mail Bot");
            Console.WriteLine("You have a reminder scheduled for today!");
            Console.WriteLine(messageBody);
            Console.WriteLine("--------");
        }
    }
}