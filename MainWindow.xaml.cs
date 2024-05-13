using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;

namespace find_primes
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static CancellationTokenSource cts;
        private CancellationToken ct;

        private long firstPrime;

        public long FirstPrime 
        { 
            get => firstPrime; 
            set {
                firstPrime = value; 
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FirstPrime)));
            } 
        }

        private long secondPrime;

        public long SecondPrime
        {
            get => secondPrime;
            set
            {
                secondPrime = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SecondPrime)));
            }
        }

        private long thirdPrime;

        public long ThirdPrime
        {
            get => thirdPrime;
            set
            {
                thirdPrime = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ThirdPrime)));
            }
        }
        public long FindPrime(string condition, CancellationToken ct)
        {
            long number = 2;

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    return -1;
                }

                if (IsPrime(number) && number.ToString().Contains(condition))
                {
                    return number;
                }
                number++;
            }
        }

        public bool IsPrime(long number)
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
        private async void startFirst_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            ct = cts.Token;
            string first = first_condition.Text;
            startFirst.BorderBrush = Brushes.Red;
            await Task.Run(async () =>
            {
                FirstPrime = FindPrime(first, ct);
            });
            startFirst.BorderBrush = Brushes.Green;
        }

        private async void startSecond_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            ct = cts.Token;
            string second = second_condition.Text;
            startSecond.BorderBrush = Brushes.Red;
            await Task.Run(async () =>
            {
                SecondPrime = FindPrime(second, ct);
            });
            startSecond.BorderBrush = Brushes.Green;
        }

        private async void startThird_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            ct = cts.Token;
            string third = third_condition.Text;
            startThird.BorderBrush = Brushes.Red;
            await Task.Run(async () =>
            {
                ThirdPrime = FindPrime(third, ct);
            });
            startThird.BorderBrush = Brushes.Green;
        }

        private void cancelAll_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
