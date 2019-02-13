export class Reminder {
  id = "";
  title = "";
  dueDate: Date = new Date();

  constructor(values: Object = {}) {
    Object.assign(this, values);
  }
}
