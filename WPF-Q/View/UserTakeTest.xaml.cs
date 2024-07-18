using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_Q.Models;

namespace WPF_Q.View
{
    public partial class UserTakeTest : Window
    {
        private readonly DbtestContext _context;
        private readonly User _user;
        private readonly List<Question> _questions;
        private bool isSubmited = false;

        public UserTakeTest(User user)
        {
            InitializeComponent();
            _context = new DbtestContext();
            _user = user;
            _questions = LoadQuestions();
            DisplayQuestions();
        }

        private List<Question> LoadQuestions()
        {
            var questions = _context.Questions.ToList();
            return questions.OrderBy(q => Guid.NewGuid()).ToList(); // Randomly shuffle questions
        }

        private void DisplayQuestions()
        {
            foreach (var question in _questions)
            {
                var questionBlock = new TextBlock
                {
                    Text = question.Text,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                QuestionsPanel.Children.Add(questionBlock);

                var answers = question.Answer.Split('@').OrderBy(a => Guid.NewGuid()).ToArray(); // Randomly shuffle answers
                var radioGroup = new StackPanel { Margin = new Thickness(0, 0, 0, 20) };

                foreach (var answer in answers)
                {
                    var radioButton = new RadioButton
                    {
                        Content = answer,
                        GroupName = question.Id.ToString()
                    };
                    radioGroup.Children.Add(radioButton);
                }
                QuestionsPanel.Children.Add(radioGroup);
            }
        }

        private async void SubmitTest_Click(object sender, RoutedEventArgs e)
        {
            if (isSubmited) return;
            int score = 0;

            foreach (var question in _questions)
            {
                var radioGroup = QuestionsPanel.Children.OfType<StackPanel>()
                    .FirstOrDefault(sp => sp.Children.OfType<RadioButton>().Any(rb => rb.GroupName == question.Id.ToString()));

                if (radioGroup != null)
                {
                    var selectedAnswer = radioGroup.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true)?.Content.ToString();
                    if (selectedAnswer == question.CorrectAnswer)
                    {
                        score++;
                    }
                }
            }

            Models.UserTakeTest userTakeTest = new Models.UserTakeTest
            {
                UserId = _user.Id,
                Answer = score + "/" + _questions.Count
            };
            await _context.UserTakeTests.AddAsync(userTakeTest);

            isSubmited = true;
            MessageBox.Show($"You scored {score} out of {_questions.Count}.", "Test Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserView(_user);
            window.Show();
            this.Close();
        }
    }
}
