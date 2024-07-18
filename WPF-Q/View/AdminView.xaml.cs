using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_Q.Models;

namespace WPF_ASG_EF.View
{
    public partial class AdminView : Window
    {
        private readonly DbtestContext _context;
        private Question _selectedQuestion = null;

        public AdminView()
        {
            _context = new DbtestContext();
            InitializeComponent();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            dataGridQuestions.ItemsSource = _context.Questions.ToList();
        }

        private void CreateQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(txtQuestionText.Text) ||
                    string.IsNullOrWhiteSpace(txtCorrectAnswer.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer1.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer2.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer3.Text))
                {
                    MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string answers = string.Join("@", txtCorrectAnswer.Text, txtAnswer1.Text, txtAnswer2.Text, txtAnswer3.Text);

                // Create new question object from input fields
                Question newQuestion = new Question
                {
                    Text = txtQuestionText.Text,
                    Answer = answers,
                    CorrectAnswer = txtCorrectAnswer.Text,
                    CreatedDate = DateTime.Now
                };

                // Add new question to DbContext and save changes
                _context.Questions.Add(newQuestion);
                _context.SaveChanges();

                // Refresh the DataGrid
                LoadQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating question: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(txtQuestionText.Text) ||
                    string.IsNullOrWhiteSpace(txtCorrectAnswer.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer1.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer2.Text) ||
                    string.IsNullOrWhiteSpace(txtAnswer3.Text))
                {
                    MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string answers = string.Join("@", txtCorrectAnswer.Text, txtAnswer1.Text, txtAnswer2.Text, txtAnswer3.Text);

                _selectedQuestion.Text = txtQuestionText.Text;
                _selectedQuestion.Answer = answers;
                _selectedQuestion.CorrectAnswer = txtCorrectAnswer.Text;

                _context.Questions.Update(_selectedQuestion);
                _context.SaveChanges();

                // Refresh the DataGrid
                LoadQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating question: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.Questions.Remove(_selectedQuestion);
                _context.SaveChanges();

                // Refresh the DataGrid
                LoadQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting question: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridQuestions.SelectedItem is Question selectedQuestion)
            {
                _selectedQuestion = selectedQuestion;

                txtQuestionText.Text = selectedQuestion.Text;
                txtCorrectAnswer.Text = selectedQuestion.CorrectAnswer;

                if (selectedQuestion.Answer != null)
                {
                    var answers = selectedQuestion.Answer.Split('@');
                    txtCorrectAnswer.Text = answers.Length > 0 ? answers[0] : "";
                    txtAnswer1.Text = answers.Length > 1 ? answers[1] : "";
                    txtAnswer2.Text = answers.Length > 2 ? answers[2] : "";
                    txtAnswer3.Text = answers.Length > 3 ? answers[3] : "";
                }
            }
        }
    }
}
