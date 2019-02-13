import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Reminder } from "../models/reminder";
import { ApiService } from "./api.service";

@Injectable()
export class ReminderDataService {
  reminders: Reminder[] = [];

  constructor(private apiService: ApiService) {}

  addReminder(reminder: Reminder): Observable<any> {
    return this.apiService.createReminder(reminder);
  }

  deleteTodoById(id: string): ReminderDataService {
    throw new Error("Not implemented");
  }

  getAllReminders(): Observable<Reminder[]> {
    return this.apiService.getAllReminders();
  }
}
