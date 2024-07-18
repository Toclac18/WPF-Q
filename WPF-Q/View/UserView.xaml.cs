using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Q.Models;

namespace WPF_Q.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private readonly DbtestContext _context;
        private readonly User _user;

        public UserView(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void TakeTest_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserTakeTest(_user);
            window.Show();
            this.Close();
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserHistory(_user);
            window.Show();
            this.Close();
        }
    }
}
