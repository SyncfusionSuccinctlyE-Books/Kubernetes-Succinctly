using System;
using System.IO;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Moq;
using remind_me_api.Controllers;
using remind_me_api.Models;
using Xunit;

namespace remind_me_api.tests
{
    public class ReminderControllerTests : IClassFixture<DatabaseFixture>
    {
        public ReminderControllerTests(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            sut = new ReminderController(this.databaseFixture.ConfigurationRoot.Object);
        }

        private readonly DatabaseFixture databaseFixture;
        private readonly ReminderController sut;

        [Fact]
        public void GetShouldRetrieveAllRecords()
        {
            using (var db = new LiteDatabase(databaseFixture.DatabasePath))
            {
                var remindersCollection = db.GetCollection<Reminder>("reminders");
                var reminderToAdd = new Reminder {DueDate = DateTime.Today, Id = "2", Title = "Test"};
                remindersCollection.Insert(reminderToAdd);
            }

            Assert.Contains(sut.Get().Value, r => r.Id.Equals("2"));
        }

        [Fact]
        public void PostShouldSaveRecord()
        {
            var reminderToAdd = new Reminder {DueDate = DateTime.Today, Id = "1", Title = "Test"};
            sut.Post(reminderToAdd);
            using (var db = new LiteDatabase(databaseFixture.DatabasePath))
            {
                var remindersCollection = db.GetCollection<Reminder>("reminders");
                Assert.NotNull(remindersCollection.Find(r => r.Id.Equals(reminderToAdd.Id)));
            }
        }
    }

    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            DatabasePath = "testdatabase.db";
            ConfigurationRoot = new Mock<IConfigurationRoot>();
            ConfigurationRoot.SetupGet(x => x["dbPath"]).Returns(DatabasePath);
        }

        public Mock<IConfigurationRoot> ConfigurationRoot { get; set; }

        public string DatabasePath { get; set; }

        public void Dispose()
        {
            if (File.Exists(DatabasePath)) File.Delete(DatabasePath);
        }
    }
}