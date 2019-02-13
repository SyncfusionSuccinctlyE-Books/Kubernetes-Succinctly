import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";
import { ReminderDataService } from "./services/reminder-data.service";
import { Reminder } from "./models/reminder";

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
    this.reminders = this.reminderDataService.getAllReminders();
  }

  onSubmit(reminder) {
    this.reminderDataService.addReminder(reminder);
    this.form.reset();
  }
}
