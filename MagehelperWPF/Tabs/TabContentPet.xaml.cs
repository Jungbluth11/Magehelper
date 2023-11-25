using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DSAUtils.UI.WPF;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentPet.xaml
    /// </summary>
    public partial class TabContentPet : UserControl
    {
        public Pet Pet { get; }

        public TabContentPet(MainWindow mainWindow)
        {
            InitializeComponent();
            Pet = new Pet(mainWindow.Core);
            mainWindow.tabContentPet = this;
            mainWindow.Core.AddPetGUIAction = AddPet;
        }

        public void AddPet()
        {
            string attack = Pet.Attack.ToString();
            string parry = Pet.Parry.ToString();
            string gs = Pet.GS.ToString();
            if (Pet.IsFlying)
            {
                attack += "/" + Pet.AttackFlying.ToString();
                parry += "/" + Pet.ParryFlying.ToString();
                gs += "/" + Pet.GSFlying.ToString();
            }
            NumericUpDownMU.Value = Pet.MU;
            NumericUpDownKL.Value = Pet.KL;
            NumericUpDownIN.Value = Pet.IN;
            NumericUpDownCH.Value = Pet.CH;
            NumericUpDownFF.Value = Pet.FF;
            NumericUpDownGE.Value = Pet.GE;
            NumericUpDownKO.Value = Pet.KO;
            NumericUpDownKK.Value = Pet.KK;
            NumericUpDownAup.Value = Pet.AuP;
            NumericUpDownLep.Value = Pet.LeP;
            NumericUpDownAsp.Value = Pet.AsP;
            StringRkw.Content = Pet.RKW;
            StringAttack.Content = attack;
            StringParry.Content = parry;
            StringGs.Content = gs;
            DataGridSpells.ItemsSource = Pet.KnownSpells;
            BtnAddPet.Visibility = Visibility.Collapsed;
            BtnAddSpell.Visibility = Visibility.Visible;
            GridPetAttributes.IsEnabled = true;
            GridPetData.IsEnabled = true;
        }

        internal void ResetTab()
        {
            NumericUpDownMU.Value = 0;
            NumericUpDownKL.Value = 0;
            NumericUpDownIN.Value = 0;
            NumericUpDownCH.Value = 0;
            NumericUpDownFF.Value = 0;
            NumericUpDownGE.Value = 0;
            NumericUpDownKO.Value = 0;
            NumericUpDownKK.Value = 0;
            NumericUpDownAup.Value = 0;
            NumericUpDownLep.Value = 0;
            NumericUpDownAsp.Value = 0;
            StringRkw.Content = 0;
            StringAttack.Content = 0;
            StringParry.Content = 0;
            StringGs.Content = 0;
            DataGridSpells.ItemsSource = null;
            DataGridSpells.Items.Clear();
            BtnAddPet.Visibility = Visibility.Visible;
            BtnAddSpell.Visibility = Visibility.Collapsed;
            GridPetAttributes.IsEnabled = false;
            GridPetData.IsEnabled = false;
        }

        private void IncreaseAttribute(string attribute)
        {
            try
            {
                Pet.IncreaseAttribute(attribute);
            }
            catch
            {
                MessageBox.Show("Maximum erreicht!", "Attribut Steigern - Magehelper", MessageBoxButton.OK);
            }
        }

        private void NumericUpDownMU_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.MU = NumericUpDownMU.Value;
        }

        private void NumericUpDownKL_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.KL = NumericUpDownKL.Value;
        }

        private void NumericUpDownIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.IN = NumericUpDownIN.Value;
        }

        private void NumericUpDownCH_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.CH = NumericUpDownCH.Value;
        }

        private void NumericUpDownFF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.FF = NumericUpDownFF.Value;
        }

        private void NumericUpDownGE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.GE = NumericUpDownGE.Value;
        }

        private void NumericUpDownKO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.KO = NumericUpDownKO.Value;
        }

        private void NumericUpDownKK_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.KK = NumericUpDownKK.Value;
        }

        private void NumericUpDownMR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.MR = NumericUpDownMR.Value;
        }

        private void NumericUpDownAup_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.AuP = NumericUpDownAup.Value;
        }

        private void NumericUpDownLep_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.LeP = NumericUpDownLep.Value;
        }

        private void NumericUpDownAsp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Pet.AsP = NumericUpDownAsp.Value;
        }

        private void BtnAddPet_Click(object sender, RoutedEventArgs e)
        {
            PetGeneratorWindow petGeneratorWindow = new PetGeneratorWindow(Pet, AddPet);
            petGeneratorWindow.ShowDialog();
        }

        private void BtnAddSpell_Click(object sender, RoutedEventArgs e)
        {
            AddPetSpellWindow addPetSpellWindow = new AddPetSpellWindow(Pet);
            addPetSpellWindow.ShowDialog();
            DataGridSpells.Items.Refresh();
        }

        private void BtnIncreaseMU_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("MU");
        }

        private void BtnIncreaseKL_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("KL");
        }

        private void BtnIncreaseIN_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("IN");
        }

        private void BtnIncreaseCH_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("CH");
        }

        private void BtnIncreaseFF_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("FF");
        }

        private void BtnIncreaseGE_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("GE");
        }

        private void BtnIncreaseKO_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("KO");
        }

        private void BtnIncreaseKK_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("KK");
        }

        private void BtnIncreaseMR_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("MR");
        }

        private void BtnIncreaseRkw_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("RkW");
        }

        private void BtnIncreaseAttack_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("Attack");
        }

        private void BtnIncreaseParry_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("Parry");
        }

        private void BtnIncreaseGs_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("GS");
        }

        private void BtnIncreaseAup_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("AU");
        }

        private void BtnIncreaseLep_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("LE");
        }

        private void BtnIncreaseAsp_Click(object sender, RoutedEventArgs e)
        {
            IncreaseAttribute("AE");
        }

        private void BtnResetAup_Click(object sender, RoutedEventArgs e)
        {
            Pet.ResetAuP();
            NumericUpDownAup.Value = Pet.AuP;
        }

        private void BtnResetLep_Click(object sender, RoutedEventArgs e)
        {
            Pet.ResetLeP();
            NumericUpDownLep.Value = Pet.LeP;
        }

        private void BtnResetAsp_Click(object sender, RoutedEventArgs e)
        {
            Pet.ResetAsP();
            NumericUpDownAsp.Value = Pet.AsP;
        }

        private void BtnSpell_Click(object sender, RoutedEventArgs e)
        {
            string[] result = Pet.RollSpell(Pet.KnownSpells.Single(c => c.Name == (sender as Button).Tag.ToString()));
            Wuerfelfenster wuerfelfenster = new Wuerfelfenster(result);
            wuerfelfenster.WindowStyle = WindowStyle.ToolWindow;
            wuerfelfenster.ShowDialog();
        }
    }
}