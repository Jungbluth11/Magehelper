using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddTimerWindow.xaml
    /// </summary>
    public partial class AddTimerWindow : Window
    {
        private readonly TabContentTimer tabContentTimer;

        public AddTimerWindow(TabContentTimer tabContentTimer)
        {
            InitializeComponent();
            this.tabContentTimer = tabContentTimer;
        }

        private void TBoxDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(TBoxDuration.Text, "[^0-9]"))
            {
                TBoxDuration.Text = string.Empty;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TBoxText.Text) && !string.IsNullOrWhiteSpace(TBoxDuration.Text))
            {
                string text = TBoxText.Text;
                int duration = int.Parse(TBoxDuration.Text);
                switch (DropdownDurationMod.SelectedIndex)
                {
                    case 1:
                        duration *= Timers.DurationSRMultiplier;
                        break;
                    case 2:
                        duration *= Timers.DurationDaysMultiplier;
                        break;
                    default:
                        duration *= Timers.DurationKRMultplier;
                        break;
                }
                Timer timer = tabContentTimer.Timers.Add(text, duration);
                tabContentTimer.AddTimer(timer);
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}