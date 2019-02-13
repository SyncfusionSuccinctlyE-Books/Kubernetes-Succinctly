import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Reminder } from "../models/reminder";

@Injectable()
export class ApiService {
  constructor(private httpClient: HttpClient) {}

  public getAllReminders(): Observable<Reminder[]> {
    return this.httpClient.get<Reminder[]>("/Proxy/GetAllReminders");
  }

  public createReminder(reminder: Reminder): Observable<any> {
    return this.httpClient.post("/Proxy/SaveReminder", reminder);
  }
}
