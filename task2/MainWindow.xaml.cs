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

namespace task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Update_ProgressBar(object sender, RoutedEventArgs e)
        {
            btnStartProgress.IsEnabled = false;
            Thread thread = new Thread(() =>{
                try
                {
                    for (int i = 0; i < 101; i++)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            progressValue.Value = i;
                            progressText.Text = $"{i}%";
                        });
                        Thread.Sleep(10);
                    }
                    
                }
                catch (Exception e)
                {
                    throw new Exception("Ошбка");

                }
            });
            thread.Start();
            
        }

        private void HighPriority(object sender, RoutedEventArgs e)
        {
            bool isHighPriorityThreadRunning = true;
            btnHighPriority.IsEnabled = false;

            Thread highPriorityThread = new Thread(() =>
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Dispatcher.Invoke(() =>
                {
                    txtLog.Text = ($"Поток запущен(Id:{threadId}. Изначальный приоритет: {Thread.CurrentThread.Priority}");
                });
                for (int i = 1; i <= 3; i++)
                {
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() =>
                         txtLog.Text = ($"Этап {i}/3 с нормальным приоритетом (ID: {threadId})"));
                }
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Dispatcher.Invoke(() =>
                {
                    txtLog.Text = ($"Приоритет повышен до: {Thread.CurrentThread.Priority}");

                });

                // Работа с высоким приоритетом
                for (int i = 1; i <= 3; i++)
                {
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() =>
                        txtLog.Text = ($"Этап {i}/3 с ВЫСОКИМ приоритетом (ID: {threadId})"));
                }

                // Понижаем приоритет обратно
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
               
            });
            highPriorityThread.Start();
        }

        private void UpdateThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    // Эта строка вызовет исключение!
                    progressValue.Value = 50;
                }
                catch (Exception ex)
                {
                    // Обрабатываем исключение в UI потоке
                    Dispatcher.Invoke(() =>
                    {
                        txtLog.Text = ($"❌ ОШИБКА: {ex.GetType().Name} \n Сообщение: {ex.Message}");
                      
                        
                       
                    });
                }
            });
            thread.Start();
        }
    }
}