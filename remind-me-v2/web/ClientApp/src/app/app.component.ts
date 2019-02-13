import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Reminder } from "./models/reminder";
import { ReminderDataService } from "./services/reminder-data.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
  form: FormGroup;
  title: string;
  dueDate: Date;
  reminder: Reminder;
  reminders: Reminder[];
  minDate: Date = new Date();

  constructor(
    private fb: FormBuilder,
    private reminderDataService: ReminderDataService
  ) {
    this.form = fb.group({
      title: [null, Validators.required],
      dueDate: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.fetchReminders();
  }

  private fetchReminders() {
    this.reminderDataService
      .getAllReminders()
      .subscribe(reminders => (this.reminders = reminders));
  }

  onSubmit(reminder) {
    this.reminderDataService
      .addReminder(reminder)
      .subscribe(result => this.fetchReminders());
    this.form.reset();
  }
}
