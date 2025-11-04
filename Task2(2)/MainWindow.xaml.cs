using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task2_2_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            Thread thread1 = new Thread(() =>
            {
                Thread.CurrentThread.Priority= ThreadPriority.Lowest;
                for (int i = 0; i < 101; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Text1.Text = $"Значение: {i.ToString()}";
                    });
                    Thread.Sleep(100);
                }

            });
            Thread thread2 = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
                for (int i = 0; i < 101; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Text2.Text = $"Значение: {i.ToString()}";
                    });
                    Thread.Sleep(100);
                }

            });
            Thread thread3 = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                for (int i = 0; i < 101; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Text3.Text = $"Значение: {i.ToString()}";
                    });
                    Thread.Sleep(100);
                }

            });
            thread1.Start();
            thread2.Start();
            thread3.Start();
            Thread waitThread = new Thread(() =>
            {
                
                thread1.Join();
                thread2.Join();
                thread3.Join();

                
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Все потоки завершили работу!", "Готово",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

                    
                    Text1.Text = "Поток 1 завершен";
                    Text2.Text = "Поток 2 завершен";
                    Text3.Text = "Поток 3 завершен";
                });
            });

            waitThread.Start();
        }
    }
}