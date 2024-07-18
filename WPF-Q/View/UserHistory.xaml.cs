
using System.Windows;
using WPF_Q.Models;

namespace WPF_Q.View
{
    public partial class UserHistory : Window
    {
        private readonly DbtestContext _context;
        private readonly User _user;

        public UserHistory(User user)
        {
            InitializeComponent();
            _context = new DbtestContext();
            _user = user;
            LoadUserHistory();
        }

        private void LoadUserHistory()
        {
            var userHistory = _context.UserTakeTests
                .Where(utt => utt.UserId == _user.Id)
                .ToList();

            dataGridUserHistory.ItemsSource = userHistory;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserView(_user);
            window.Show();
            this.Close();
        }

        private void dataGridUserHistory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
