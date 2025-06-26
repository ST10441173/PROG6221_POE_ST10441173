# 🛡️ Cybersecurity Awareness Chatbot - PROG6221 POE

A C# WPF desktop application that simulates a conversational chatbot to educate users on cybersecurity topics, manage personal tasks, and take quizzes — all through a rich GUI interface.

---

## 📌 Features

### 💬 Cybersecurity Chatbot (NLP Simulation)
- Natural language input support (e.g. “What’s a strong password?”)
- Bot responds to topics like:
  - Passwords
  - Phishing
  - Scams
  - Malware
  - Privacy & Encryption
- Emotional reassurance if user types things like “I'm scared” or “worried”

### 🗂️ Task Assistant
- Add tasks with titles, descriptions, and reminder dates
- Mark tasks complete
- Delete tasks
- Task assistant is integrated directly into the GUI
- **Reminder system** alerts the user of tasks due today (in chat or popup)

### 🧠 Quiz Mini-Game
- Launch quiz from GUI
- Covers key cybersecurity topics
- Shows score on completion
- Logs quiz results to activity history

### 📜 Activity Log
- Logs all major user actions:
  - Task added, deleted, completed
  - Quiz started/completed
- View log as a **popup GUI window**
- Keeps only the most recent 10 entries

---

## 🛠️ How to Run the App

### Requirements:
- .NET 6.0+ SDK
- Visual Studio 2022 or later (WPF support enabled)

### Steps:
1. Clone/download the repo
2. Open `Prog6221_POE.sln` in Visual Studio
3. Restore NuGet packages if prompted
4. Set build target to `.NET 6` or `.NET 8` (your version)
5. Press **F5** to build and run the app

---

## 🗂️ Folder Structure

