using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

public class ProxyController : Controller
{
    public IConfiguration Configuration { get; set; }

    private RestClient client;

    public ProxyController(IConfiguration config)
    {
        this.Configuration = config;
        this.client = new RestClient(Configuration["apiUrl"]);
    }
    public IActionResult SaveReminder([FromBody] Reminder reminder)
    {
        var request = new RestRequest("/api/Reminder", Method.POST);
        request.AddParameter("application/json", JsonConvert.SerializeObject(reminder), ParameterType.RequestBody);
        var response = this.client.Execute(request);
        return StatusCode((int) response.StatusCode);
    }
    public IActionResult GetAllReminders()
    {
        var request = new RestRequest("/api/Reminder/all", Method.GET);
        var response = this.client.Execute(request);
        return Ok(response.Content);
    }
    public IActionResult DeleteReminder(string reminderId)
    {
        return Unauthorized();
    }
}