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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace find_primes
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void start_Button_Click(object sender, RoutedEventArgs e)
        {
            startButton.Content = "Processing...";
            startButton.IsEnabled = false;
            progressBar.Visibility = Visibility.Visible;

            string first = first_condition.Text;
            string second = second_condition.Text;
            string third = third_condition.Text;

            Task<long> f = Task.Run(() => FindPrime(first));
            Task<long> s = Task.Run(() => FindPrime(second));
            Task<long> t = Task.Run(() => FindPrime(third));

            try
            {
                long[] results = await Task.WhenAll(f, s, t);
                first_prime.Text = results[0].ToString();
                second_prime.Text = results[1].ToString();
                third_prime.Text = results[2].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                startButton.Content = "Find Primes";
                startButton.IsEnabled = true;
                progressBar.Visibility = Visibility.Hidden;
            }
        }
        private long FindPrime(string condition)
        {
            long number = 2; // Starting search from the smallest prime number

            while (true)
            {
                if (IsPrime(number) && number.ToString().Contains(condition))
                {
                    return number;
                }
                number++;
            }
        }

        private bool IsPrime(long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            long boundary = (long)Math.Sqrt(number);
            for (long i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
