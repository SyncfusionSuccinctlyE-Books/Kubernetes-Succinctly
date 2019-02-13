import { Injectable } from "@angular/core";
import "rxjs/add/observable/of";
import { Observable } from "rxjs/Observable";
import { Reminder } from "../models/reminder";

@Injectable()
export class ApiMockService {
  constructor() {}
  public getAllReminders(): Observable<Reminder[]> {
    return Observable.of([
      new Reminder({ id: "1", title: "Test Reminder", dueDate: new Date() })
    ]);
  }

  public createReminder(reminder: Reminder): Observable<any> {
    return Observable.of([true]);
  }
}
