import { TestBed, inject } from "@angular/core/testing";

import { ReminderDataService } from "./reminder-data.service";
import { Reminder } from "../models/reminder";

describe("ReminderDataService", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReminderDataService]
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
        service.addReminder(reminder);
        expect(service.getAllReminders().length).toBeGreaterThanOrEqual(1);
      }
    ));
  });
});
