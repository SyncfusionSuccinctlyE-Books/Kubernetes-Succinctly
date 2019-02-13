import { inject, TestBed } from "@angular/core/testing";
import { Reminder } from "../models/reminder";
import { ApiMockService } from "./api-mock.service";
import { ApiService } from "./api.service";
import { ReminderDataService } from "./reminder-data.service";

describe("ReminderDataService", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ReminderDataService,
        { provide: ApiService, useClass: ApiMockService }
      ]
    });
  });

  it("should be created", inject(
    [ReminderDataService],
    (service: ReminderDataService) => {
      expect(service).toBeTruthy();
    }
  ));

  describe("#addReminder", () => {
    it("should add a reminder", inject(
      [ReminderDataService],
      (service: ReminderDataService) => {
        const reminder = new Reminder({ title: "Reminder Test" });
        service
          .addReminder(reminder)
          .subscribe(result => expect(result != null));
      }
    ));
  });

  describe("#getReminders", () => {
    it("should get all reminders", inject(
      [ReminderDataService],
      (service: ReminderDataService) => {
        service
          .getAllReminders()
          .subscribe(reminder =>
            expect(reminder.length).toBeGreaterThanOrEqual(1)
          );
      }
    ));
  });
});
