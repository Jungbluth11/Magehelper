using System.Windows;
using System.Windows.Controls;
using DSAUtils.UI.WPF;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentFlameSword.xaml
    /// </summary>
    public partial class TabContentFlameSword : UserControl
    {
        private readonly MainWindow mainWindow;
        public FlameSword FlameSword { get; }

        public TabContentFlameSword(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            mainWindow.TabContentFlameSword = this;
            FlameSword = new FlameSword(mainWindow.Core);
            InitializeComponent();
        }

        public void EnableTab()
        {
            StringNone.Visibility = Visibility.Collapsed;
            GridContent.Visibility = Visibility.Visible;
            BtnActivate.IsEnabled = true;
        }

        internal void ResetTab()
        {
            StringNone.Visibility = Visibility.Visible;
            GridContent.Visibility = Visibility.Collapsed;
            BtnActivate.IsEnabled = false;
            BtnDeactivate.IsEnabled = false;
            GridContent.IsEnabled = false;
            StringRkp.Content = 0;
            NumericUpDownTp.Value = 0;
            NumericUpDownParry.Value = 0;
            NumericUpDownAttack.Value = 0;
            NumericUpDownGS.Value = 0;
        }

        private int RollRKP()
        {
            object[] result = (object[])FlameSword.RollActivation();
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(result);
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.ShowDialog();
            return (int)result[0];
        }

        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            bool btnRollEnabled = false;
            if (mainWindow.TabContentCharacter != null && mainWindow.TabContentCharacter.Character.IsLoaded)
            {
                btnRollEnabled = true;
            }
            DialogNumberWindow dialogNumberWindow = new DialogNumberWindow("RkP* des Flammenschwerts", "RkP* des Flammenschwerts", RollRKP, "RkP* auswürfeln", btnRollEnabled);
            dialogNumberWindow.ShowDialog();
            FlameSword.PointsTotal = dialogNumberWindow.Value;
            StringRkp.Content = FlameSword.PointsTotal.ToString();
            GridContent.IsEnabled = true;
            BtnActivate.IsEnabled = false;
            BtnDeactivate.IsEnabled = true;
        }

        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            BtnActivate.IsEnabled = true;
            BtnDeactivate.IsEnabled = false;
            GridContent.IsEnabled = false;
            StringRkp.Content = 0;
            NumericUpDownTp.Value = 0;
            NumericUpDownParry.Value = 0;
            NumericUpDownAttack.Value = 0;
            NumericUpDownGS.Value = 0;
            FlameSword.ResetTool();
        }

        private void NumericUpDownTp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            FlameSword.ModifyTp(NumericUpDownTp.Value);
            StringTp.Content = FlameSword.TP[0].ToString();
            StringRkp.Content = FlameSword.PointsRemain.ToString();
        }

        private void NumericUpDownParry_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            FlameSword.ModifyParry(NumericUpDownParry.Value);
            StringParry.Content = FlameSword.Parry[0].ToString();
            StringRkp.Content = FlameSword.PointsRemain.ToString();
        }

        private void NumericUpDownAttack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            FlameSword.ModifyAttack(NumericUpDownAttack.Value);
            StringAttack.Content = FlameSword.Attack[0].ToString();
            StringRkp.Content = FlameSword.PointsRemain.ToString();
        }

        private void NumericUpDownGS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            FlameSword.ModifyGs(NumericUpDownGS.Value);
            StringGs.Content = FlameSword.GS[0].ToString();
            StringRkp.Content = FlameSword.PointsRemain.ToString();
        }
    }
}