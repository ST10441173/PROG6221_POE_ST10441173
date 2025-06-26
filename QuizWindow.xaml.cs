using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Prog6221_POE
{
    public partial class QuizWindow : Window
    {
        private class QuizQuestion
        {
            public string Question { get; set; }
            public List<string> Options { get; set; }
            public string CorrectAnswer { get; set; }
        }

        private readonly List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizWindow()
        {
            InitializeComponent();

            questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What is phishing?", Options = new List<string>{ "A virus", "An email scam", "A firewall", "An antivirus" }, CorrectAnswer = "An email scam" },
                new QuizQuestion { Question = "True or False: You should use the same password for every account.", Options = new List<string>{ "True", "False" }, CorrectAnswer = "False" },
                new QuizQuestion { Question = "Which is a strong password?", Options = new List<string>{ "123456", "mypassword", "Pass@2024", "Muhammad01" }, CorrectAnswer = "Pass@2024" },
                new QuizQuestion { Question = "What should you do with suspicious links?", Options = new List<string>{ "Click to check", "Ignore and report", "Reply to sender", "Share with friends" }, CorrectAnswer = "Ignore and report" },
                new QuizQuestion { Question = "What does 2FA stand for?", Options = new List<string>{ "Two-Factor Authentication", "Two-Firewall Access", "Fast Access", "Dual-Fire Activation" }, CorrectAnswer = "Two-Factor Authentication" },
                new QuizQuestion { Question = "Which of these is safe to share online?", Options = new List<string>{ "Your home address", "Bank login", "Public email", "ID number" }, CorrectAnswer = "Public email" },
                new QuizQuestion { Question = "True or False: Antivirus software should be updated regularly.", Options = new List<string>{ "True", "False" }, CorrectAnswer = "True" },
                new QuizQuestion { Question = "What is a firewall?", Options = new List<string>{ "Antivirus", "Physical barrier", "Network protection", "Game app" }, CorrectAnswer = "Network protection" },
                new QuizQuestion { Question = "Where should you store passwords?", Options = new List<string>{ "Browser history", "Notebook", "Password manager", "Social media bio" }, CorrectAnswer = "Password manager" },
                new QuizQuestion { Question = "True or False: Public Wi-Fi is always secure.", Options = new List<string>{ "True", "False" }, CorrectAnswer = "False" },
            };

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                QuestionText.Text = $"✅ Quiz completed! Your score: {score}/{questions.Count}";

                OptionsPanel.Children.Clear();
                NextButton.Visibility = Visibility.Collapsed;

                return;
            }

            var question = questions[currentQuestionIndex];
            QuestionText.Text = $"Q{currentQuestionIndex + 1}: {question.Question}";

            OptionsPanel.Children.Clear();

            foreach (var option in question.Options)
            {
                RadioButton rb = new RadioButton
                {
                    Content = option,
                    GroupName = "Options",
                    Margin = new Thickness(0, 5, 0, 5)
                };
                OptionsPanel.Children.Add(rb);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var question = questions[currentQuestionIndex];
            string selected = null;

            foreach (RadioButton rb in OptionsPanel.Children)
            {
                if (rb.IsChecked == true)
                {
                    selected = rb.Content.ToString();
                    break;
                }
            }

            if (selected == null)
            {
                MessageBox.Show("Please select an answer.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (selected == question.CorrectAnswer)
            {
                score++;
                MessageBox.Show("✅ Correct!", "Answer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"❌ Incorrect.\nCorrect Answer: {question.CorrectAnswer}", "Answer", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (Owner is MainWindow main)
            {
                main.LogActivity($"Quiz completed: {score}/{questions.Count} correct");
            }


            currentQuestionIndex++;
            LoadQuestion();
        }
    }
}
