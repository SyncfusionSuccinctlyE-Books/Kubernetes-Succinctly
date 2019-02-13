import { Injectable } from "@angular/core";
import { Reminder } from "../models/reminder";
import { UUID } from "angular2-uuid";

@Injectable()
export class ReminderDataService {
  reminders: Reminder[] = [];

  constructor() {}

  addReminder(reminder: Reminder): ReminderDataService {
    reminder.id = UUID.UUID();
    this.reminders.push(reminder);
    return this;
  }

  deleteTodoById(id: string): ReminderDataService {
    this.reminders = this.reminders.filter(r => r.id !== id);
    return this;
  }

  getAllReminders(): Reminder[] {
    return this.reminders;
  }
}
