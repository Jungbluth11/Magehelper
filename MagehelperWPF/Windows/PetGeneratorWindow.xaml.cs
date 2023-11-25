using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddPetWindow.xaml
    /// </summary>
    public partial class PetGeneratorWindow : Window
    {
        private readonly PetGenerator petGenerator;
        private readonly Action uiAction;
        private readonly Action<PetData, bool, int[]> dataAction;

        public PetGeneratorWindow(Pet pet, Action action)
        {
            petGenerator = new PetGenerator(pet);
            uiAction = action;
            dataAction = pet.AddPet;
            InitializeComponent();
            DropdownSpecies.ItemsSource = petGenerator.SpeciesStrings;
        }

        private void CbMightyCompanion_CheckedStateChanged(object sender, RoutedEventArgs e)
        {
            petGenerator.IsMightyCompanion = (bool)CbMightyCompanion.IsChecked;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            int[] attributes = new int[]
            {
                NumericUpDownMU.Value,
                NumericUpDownKL.Value,
                NumericUpDownIN.Value,
                NumericUpDownCH.Value,
                NumericUpDownFF.Value,
                NumericUpDownGE.Value,
                NumericUpDownKO.Value,
                NumericUpDownKK.Value,
                NumericUpDownLE.Value,
                NumericUpDownAE.Value,
                NumericUpDownAU.Value
            };
            dataAction(petGenerator.BaseData, petGenerator.IsMightyCompanion, attributes);
            uiAction();
            Close();
        }

        private void DropdownSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            petGenerator.Species = e.AddedItems[0].ToString();
            NumericUpDownMU.Value = petGenerator.BaseData.MUStartMin;
            NumericUpDownMU.MinValue = petGenerator.BaseData.MUStartMin;
            NumericUpDownMU.MaxValue = petGenerator.BaseData.MUStartMax;
            NumericUpDownKL.Value = petGenerator.BaseData.KLStartMin;
            NumericUpDownKL.MinValue = petGenerator.BaseData.KLStartMin;
            NumericUpDownKL.MaxValue = petGenerator.BaseData.KLStartMax;
            NumericUpDownIN.Value = petGenerator.BaseData.INStartMin;
            NumericUpDownIN.MinValue = petGenerator.BaseData.INStartMin;
            NumericUpDownIN.MaxValue = petGenerator.BaseData.INStartMax;
            NumericUpDownCH.Value = petGenerator.BaseData.CHStartMin;
            NumericUpDownCH.MinValue = petGenerator.BaseData.CHStartMin;
            NumericUpDownCH.MaxValue = petGenerator.BaseData.CHStartMax;
            NumericUpDownFF.Value = petGenerator.BaseData.FFStartMin;
            NumericUpDownFF.MinValue = petGenerator.BaseData.FFStartMin;
            NumericUpDownFF.MaxValue = petGenerator.BaseData.FFStartMax;
            NumericUpDownGE.Value = petGenerator.BaseData.GEStartMin;
            NumericUpDownGE.MinValue = petGenerator.BaseData.GEStartMin;
            NumericUpDownGE.MaxValue = petGenerator.BaseData.GEStartMax;
            NumericUpDownKO.Value = petGenerator.BaseData.KOStartMin;
            NumericUpDownKO.MinValue = petGenerator.BaseData.KOStartMin;
            NumericUpDownKO.MaxValue = petGenerator.BaseData.KOStartMax;
            NumericUpDownKK.Value = petGenerator.BaseData.KKStartMin;
            NumericUpDownKK.MinValue = petGenerator.BaseData.KKStartMin;
            NumericUpDownKK.MaxValue = petGenerator.BaseData.KKStartMax;
            NumericUpDownLE.Value = petGenerator.BaseData.LEStart;
            NumericUpDownLE.MinValue = petGenerator.BaseData.LEStart;
            NumericUpDownLE.MaxValue = petGenerator.BaseData.LEStart + 3;
            NumericUpDownAE.Value = petGenerator.BaseData.AEStart;
            NumericUpDownAE.MinValue = petGenerator.BaseData.AEStart;
            NumericUpDownAE.MaxValue = petGenerator.BaseData.AEStart + 3;
            NumericUpDownAU.Value = petGenerator.BaseData.AUStart;
            NumericUpDownAU.MinValue = petGenerator.BaseData.AUStart;
            NumericUpDownAU.MaxValue = petGenerator.BaseData.AUStart + 3;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownMU_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.MU = NumericUpDownMU.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownKL_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.KL = NumericUpDownKL.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.IN = NumericUpDownIN.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownCH_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.CH = NumericUpDownCH.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownFF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.FF = NumericUpDownFF.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownGE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.GE = NumericUpDownGE.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownKO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.KO = NumericUpDownKO.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownKK_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.KK = NumericUpDownKK.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownLE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.LE = NumericUpDownLE.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownAE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.AE = NumericUpDownAE.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }

        private void NumericUpDownAU_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            petGenerator.AU = NumericUpDownAU.Value;
            StringPointsValue.Content = petGenerator.PointsRemain;
            StringApValue.Content = petGenerator.Cost;
        }
    }
}