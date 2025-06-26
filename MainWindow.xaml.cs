using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Prog6221_POE
{
    public partial class MainWindow : Window
    {
        private string userName = "";
        private string userInterest = "";
        private List<UserTask> userTasks = new();
        private List<string> activityLog = new();

        private readonly Dictionary<string, string> keywordResponses = new()
        {
            { "password", "Use a strong password with letters, numbers, and symbols." },
            { "scam", "Be cautious of suspicious messages and never share personal details." },
            { "privacy", "Keep your social media and account privacy settings up-to-date." },
            { "malware", "Use antivirus software and keep your system updated." },
            { "firewall", "A firewall protects your network by blocking unwanted connections." },
            { "encryption", "Encryption keeps your data safe from unauthorized access." }
        };

        private readonly List<string> phishingTips = new()
        {
            "Don't trust emails asking for urgent action.",
            "Check the sender's email address carefully.",
            "Never click unknown links — hover to see where it goes.",
            "Look for grammar mistakes in suspicious messages.",
            "Use multi-factor authentication when possible."
        };

        public MainWindow()
        {
            InitializeComponent();
            AppendToChat("🤖 Bot: Hello! I'm your Cybersecurity Awareness Bot. What's your name?");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            HandleInput(UserInput.Text.Trim());
            UserInput.Clear();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleInput(UserInput.Text.Trim());
                UserInput.Clear();
            }
        }

        private void HandleInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendToChat($"👤 You: {input}");
            string lower = input.ToLower();

            // Ask for name if not set
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                AppendToChat($"🤖 Bot: Nice to meet you, {userName}! Ask me about passwords, scams, phishing, or privacy.");
                return;
            }

            // Detect interest sharing
            if (lower.Contains("interested in") || lower.Contains("i like"))
            {
                userInterest = lower.Replace("interested in", "")
                                    .Replace("i like", "").Trim();
                AppendToChat($"🤖 Bot: Thanks for sharing! I'll remember you're interested in {userInterest}.");
                return;
            }

            // Activity log command
            if (lower.Contains("show activity log") || lower.Contains("what have you done") || lower.Contains("my history"))
            {
                ViewActivityLog();
                return;
            }

            // Advanced NLP-style matching
            if (lower.Contains("password") || lower.Contains("secure login") || lower.Contains("strong password") || lower.Contains("create password"))
            {
                AppendToChat("🤖 Bot: 🔐 Use a long password with uppercase, lowercase, numbers, and symbols. Don’t reuse passwords.");
                return;
            }

            if (lower.Contains("scam") || lower.Contains("scammed") || lower.Contains("fake link") || lower.Contains("fraud"))
            {
                AppendToChat("🤖 Bot: 🕵️‍♂️ Scammers often pretend to be trusted sources. Double-check email senders and never rush into clicking.");
                return;
            }

            if (lower.Contains("phishing") || lower.Contains("suspicious email") || lower.Contains("link looks weird") || lower.Contains("fishy message"))
            {
                var rand = new Random();
                var tip = phishingTips[rand.Next(phishingTips.Count)];
                AppendToChat($"🤖 Bot: {tip}");
                return;
            }

            if (lower.Contains("privacy") || lower.Contains("private") || lower.Contains("stay hidden") || lower.Contains("data safe"))
            {
                AppendToChat("🤖 Bot: 🛡️ Make your accounts private, avoid oversharing, and review app permissions often.");
                return;
            }

            if (lower.Contains("firewall"))
            {
                AppendToChat("🤖 Bot: 🧱 A firewall blocks suspicious connections to your device. Keep it on for extra protection.");
                return;
            }

            if (lower.Contains("encryption"))
            {
                AppendToChat("🤖 Bot: 🔒 Encryption scrambles data so only intended recipients can access it. Apps like WhatsApp use this.");
                return;
            }

            if (lower.Contains("malware") || lower.Contains("virus") || lower.Contains("infected"))
            {
                AppendToChat("🤖 Bot: 💣 Use up-to-date antivirus software and don’t install unknown files or open email attachments.");
                return;
            }

            if (lower.Contains("worried") || lower.Contains("anxious") || lower.Contains("i’m scared") || lower.Contains("nervous"))
            {
                AppendToChat($"🤖 Bot: It’s normal to feel that way, {userName}. Cybersecurity is tough, but you’re doing great. I’ve got your back!");
                return;
            }

            AppendToChat("🤖 Bot: I didn’t quite understand that. You can ask me about passwords, scams, phishing, or privacy.");
        }


        private void AppendToChat(string message)
        {
            ChatHistory.Text += message + "\n\n";
        }

        public void LogActivity(string entry)
        {
            string time = DateTime.Now.ToString("HH:mm");
            activityLog.Add($"{time} - {entry}");
            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private void ViewActivityLog()
        {
            if (activityLog.Count == 0)
            {
                AppendToChat("🤖 Bot: No recent activity yet.");
            }
            else
            {
                AppendToChat("🤖 Bot: Here's your recent activity:");
                foreach (string entry in activityLog)
                {
                    AppendToChat("• " + entry);
                }
            }
        }

        private void ViewLog_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogWindow logWindow = new ActivityLogWindow(activityLog);
            logWindow.Owner = this;
            logWindow.ShowDialog();
        }


        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitle.Text) || string.IsNullOrWhiteSpace(TaskDescription.Text))
            {
                AppendToChat("🤖 Bot: Please enter both a title and description for the task.");
                return;
            }

            UserTask task = new()
            {
                Title = TaskTitle.Text,
                Description = TaskDescription.Text,
                ReminderDate = TaskReminderDate.SelectedDate,
                IsCompleted = false
            };

            userTasks.Add(task);
            TaskList.Items.Add(task.ToString());
            AppendToChat($"🤖 Bot: Task '{task.Title}' added.");
            LogActivity($"Task added: {task.Title}");

            TaskTitle.Clear();
            TaskDescription.Clear();
            TaskReminderDate.SelectedDate = null;
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            int i = TaskList.SelectedIndex;
            if (i >= 0)
            {
                userTasks[i].IsCompleted = true;
                TaskList.Items[i] = userTasks[i].ToString();
                AppendToChat($"🤖 Bot: Marked task '{userTasks[i].Title}' as complete.");
                LogActivity($"Task completed: {userTasks[i].Title}");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            int i = TaskList.SelectedIndex;
            if (i >= 0)
            {
                string title = userTasks[i].Title;
                userTasks.RemoveAt(i);
                TaskList.Items.RemoveAt(i);
                AppendToChat($"🤖 Bot: Deleted task '{title}'.");
                LogActivity($"Task deleted: {title}");
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow q = new QuizWindow();
            q.Owner = this;
            q.ShowDialog();
        }

    }
}
