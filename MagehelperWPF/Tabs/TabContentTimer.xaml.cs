using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentTimer.xaml
    /// </summary>
    public partial class TabContentTimer : UserControl
    {
        private readonly ObservableCollection<Timer> timerListLeft = new ObservableCollection<Timer>();
        private readonly ObservableCollection<Timer> timerListRight = new ObservableCollection<Timer>();
        public Timers Timers { get; }

        public TabContentTimer(MainWindow mainWindow)
        {
            InitializeComponent();
            Timers = new Timers(mainWindow.Core);
            mainWindow.Core.AddTimerGUIAction = AddTimer;
            TimerListPanelLeft.ItemsSource = timerListLeft;
            TimerListPanelRight.ItemsSource = timerListRight;
        }

        internal void ResetTab()
        {
            timerListLeft.Clear();
            timerListRight.Clear();
        }

        internal void AddTimer(Timer timer)
        {
            if (timer.Duration < Timers.DurationDaysMultiplier)
            {
                timerListLeft.Add(timer);
            }
            else
            {
                timerListRight.Add(timer);
            }
        }

        private void BtnAddTimer_Click(object sender, RoutedEventArgs e)
        {
            new AddTimerWindow(this).ShowDialog();
        }

        private void BtnRemoveTimer_Click(object sender, RoutedEventArgs e)
        {
            Timer timer = (Timer)(sender as Button).Tag;
            if (timer.Duration < Timers.DurationDaysMultiplier)
            {
                timerListLeft.Remove(timer);
            }
            else
            {
                timerListRight.Remove(timer);
            }
            Timers.Remove(timer.Guid);
        }

        private void BtnDecreaseKR_Click(object sender, RoutedEventArgs e)
        {
            Timer timer = (Timer)(sender as Button).Tag;
            Timers.CountDown(timer.Guid, Timers.DurationKRMultplier);
            if (timer.Duration - Timers.DurationKRMultplier > 0)
            {
                timerListLeft[timerListLeft.IndexOf(timer)] = Timers[timer.Guid];
            }
            else
            {
                timerListLeft.Remove(timer);
            }
        }

        private void BtnDecreaseDay_Click(object sender, RoutedEventArgs e)
        {
            Timer timer = (Timer)(sender as Button).Tag;
            Timers.CountDown(timer.Guid, Timers.DurationDaysMultiplier);
            if (timer.Duration - Timers.DurationDaysMultiplier > 0)
            {
                timerListRight[timerListRight.IndexOf(timer)] = Timers[timer.Guid];
            }
            else
            {
                timerListRight.Remove(timer);
            }
        }
    }
}