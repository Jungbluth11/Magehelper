using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DSAUtils;
using DSAUtils.HeldentoolInterop;
using DSAUtils.UI.WPF;
using Magehelper.Core;
using Microsoft.Win32;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class TabContentCharacter : UserControl
    {
        public string TabName => "Charakter";
        public Character Character { get; }

        public TabContentCharacter(MainWindow mainWindow)
        {
            mainWindow.TabContentCharacter = this;
            Character = new Character(mainWindow.Core);
            InitializeComponent();
        }

        private void LoadCharacter(Charakter charakter)
        {
            Character.LoadCharacter(charakter);
            AttributePanel.IsEnabled = true;
            TabControlCharacter.IsEnabled = true;
            BtnResetAup.IsEnabled = true;
            BtnResetLep.IsEnabled = true;
            BtnResetAsp.IsEnabled = true;
            NumericUpDownMU.Value = Character.MU;
            NumericUpDownKL.Value = Character.KL;
            NumericUpDownIN.Value = Character.IN;
            NumericUpDownCH.Value = Character.CH;
            NumericUpDownFF.Value = Character.FF;
            NumericUpDownGE.Value = Character.GE;
            NumericUpDownKO.Value = Character.KO;
            NumericUpDownKK.Value = Character.KK;
            NumericUpDownMR.Value = Character.MR;
            NumericUpDownAup.Value = Character.AuP;
            NumericUpDownLep.Value = Character.LeP;
            NumericUpDownAsp.Value = Character.AsP;
            DataGridSpells.ItemsSource = Character.Spells;
            DataGridRituals.ItemsSource = Character.Rituals;
        }

        internal void ResetTab()
        {
            AttributePanel.IsEnabled = false;
            TabControlCharacter.IsEnabled = false;
            BtnResetAup.IsEnabled = false;
            BtnResetLep.IsEnabled = false;
            BtnResetAsp.IsEnabled = false;
            NumericUpDownMU.Value = 0;
            NumericUpDownKL.Value = 0;
            NumericUpDownIN.Value = 0;
            NumericUpDownCH.Value = 0;
            NumericUpDownFF.Value = 0;
            NumericUpDownGE.Value = 0;
            NumericUpDownKO.Value = 0;
            NumericUpDownKK.Value = 0;
            NumericUpDownMR.Value = 0;
            NumericUpDownAup.Value = 0;
            NumericUpDownLep.Value = 0;
            NumericUpDownAsp.Value = 0;
            DataGridSpells.ItemsSource = null;
            DataGridRituals.ItemsSource = null;
            DataGridSpells.Items.Clear();
            DataGridRituals.Items.Clear();
        }

        private void NumericUpDownMU_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.MU = NumericUpDownMU.Value;
        }

        private void NumericUpDownKL_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.KL = NumericUpDownKL.Value;
        }

        private void NumericUpDownIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.IN = NumericUpDownIN.Value;
        }

        private void NumericUpDownCH_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.CH = NumericUpDownCH.Value;
        }

        private void NumericUpDownFF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.FF = NumericUpDownFF.Value;
        }

        private void NumericUpDownGE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.GE = NumericUpDownGE.Value;
        }

        private void NumericUpDownKO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.KO = NumericUpDownKO.Value;
        }

        private void NumericUpDownKK_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.KK = NumericUpDownKK.Value;
        }

        private void NumericUpDownMR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.MR = NumericUpDownMR.Value;
        }

        private void NumericUpDownAup_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.AuP = NumericUpDownAup.Value;
        }

        private void NumericUpDownLep_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.LeP = NumericUpDownLep.Value;
        }

        private void NumericUpDownAsp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Character.AsP = NumericUpDownAsp.Value;
        }

        private void BtnResetAup_Click(object sender, RoutedEventArgs e)
        {
            Character.ResetAuP();
            NumericUpDownAup.Value = Character.AuP;
        }

        private void BtnResetLep_Click(object sender, RoutedEventArgs e)
        {
            Character.ResetLeP();
            NumericUpDownLep.Value = Character.LeP;
        }

        private void BtnResetAsp_Click(object sender, RoutedEventArgs e)
        {
            Character.ResetAsP();
            NumericUpDownAsp.Value = Character.AsP;
        }

        private void BtnRoll1W20_Click(object sender, RoutedEventArgs e)
        {
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(DSA.Probe(1));
            wuerfelfenster.ShowDialog();
        }

        private void BtnRoll3W20_Click(object sender, RoutedEventArgs e)
        {
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(DSA.Probe(3));
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.ShowDialog();
        }

        private void BtnRollW6_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == AmountW6)
            {
                return;
            }
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(DSA.Roll(AmountW6.Value, 6));
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.Add = true;
            wuerfelfenster.ShowDialog();
        }

        private void BtnSpell_Click(object sender, RoutedEventArgs e)
        {
            string[] result = Character.RollSpell((CharacterSpell)(sender as Button).Tag);
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(result);
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.ShowDialog();
        }

        private void BtnRitual_Click(object sender, RoutedEventArgs e)
        {
            string[] result = Character.RollRitual((CharacterRitual)(sender as Button).Tag);
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(result);
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.ShowDialog();
        }

        internal void MenuItemCharacterLoadFromTool_Click(object sender, RoutedEventArgs e)
        {
            LoadFromToolWindow loadFromToolWindow = new LoadFromToolWindow(Character.GetCharactersFromTool(), LoadCharacter);
            loadFromToolWindow.ShowDialog();
        }

        internal void MenuItemCharacterLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML | *.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    LoadCharacter(new Charakter(File.ReadAllText(openFileDialog.FileName)));
                }
                catch (FormatException ex)
                {
                    ErrorMessages.Error(ex.Message);
                }
            }
        }
    }
}