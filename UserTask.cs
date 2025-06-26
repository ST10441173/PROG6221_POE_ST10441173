using System;

namespace Prog6221_POE
{
    public class UserTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[✔]" : "[ ]";
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" : "";
            return $"{status} {Title} - {Description}{reminder}";
        }
    }
}
