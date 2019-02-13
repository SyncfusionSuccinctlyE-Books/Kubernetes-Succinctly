import { Reminder } from "./reminder";

describe("Reminder", () => {
  it("should create an instance", () => {
    expect(new Reminder()).toBeTruthy();
  });

  it("should accept values in the constructor", () => {
    const reminder = new Reminder({
      title: "test title",
      dueDate: new Date("2018-01-17T08:44:29+0100")
    });

    expect(reminder.title).toEqual("test title");
    expect(reminder.dueDate.getDate()).toEqual(17);
  });
});
