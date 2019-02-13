using RestSharp;

namespace remind_me_cron_job
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var restClient = new RestClient(Environment.GetEnvironmentVariable("apiUrl"));
            var bootstrapper = new Bootstrapper(restClient);
            bootstrapper.Start(args);
        }
    }
}