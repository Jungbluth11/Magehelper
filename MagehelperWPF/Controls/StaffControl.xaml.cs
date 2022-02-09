using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für StaffControl.xaml
    /// </summary>
    public partial class StaffControl : UserControl, IArtifactData
    {
        private readonly Staff staff;
        private readonly MainWindow mainWindow;

        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public StaffControl(Staff staff, MainWindow mainWindow)
        {
            InitializeComponent();
            this.staff = staff;
            this.mainWindow = mainWindow;
            ArtifactSpellsControl = new ArtifactSpellsControl("Stabzauber", staff, AddSpell, "Verfügbare Volumenpunkte");
            StringLength.Content = Staff.LengthStrings[staff.Length];
            StringMaterial.Content = Staff.MaterialStrings[staff.Material];
            StringPasp.Content = staff.Pasp;
        }

        public ArtifactSpell? AddSpell()
        {
            AddStaffSpellWindow addStaffSpellWindow = new AddStaffSpellWindow(staff);
            if (addStaffSpellWindow.ShowDialog() == true)
            {
                try
                {
                    ArtifactSpell artifactSpell = staff.AddSpell(addStaffSpellWindow.SpellName, addStaffSpellWindow.SpellCharacteristic, addStaffSpellWindow.SpellPoints);
                    if (addStaffSpellWindow.SpellName == "Zauberspeicher")
                    {
                        new EnableSpellStorageWindow(addStaffSpellWindow.SpellPoints, mainWindow).ShowDialog();
                        if (mainWindow.TabContentSpellStorage != null)
                        {
                            mainWindow.TabContentSpellStorage.EnableTab();
                        }
                    }
                    else if (addStaffSpellWindow.SpellName == "Flammenschwert" && mainWindow.TabContentFlameSword != null)
                    {
                        mainWindow.TabContentFlameSword.EnableTab();
                    }
                    return artifactSpell;
                }
                catch (Exception e)
                {
                    ErrorMessages.Error(e.Message);
                }
            }
            return null;
        }

        private void CbApport_CheckedChanged(object sender, RoutedEventArgs e)
        {
            staff.HasApport = (bool)CbApport.IsChecked;
            staff.AfvTotal();
            ArtifactSpellsControl.SetSpellCounter();
        }

        private void CbFlameSwordFour_CheckedChanged(object sender, RoutedEventArgs e)
        {
            staff.IsFlameSwordFour = (bool)CbFlameSwordFour.IsChecked;
            staff.AfvTotal();
            ArtifactSpellsControl.SetSpellCounter();
        }

        private void CbFlameSwordFive_CheckedChanged(object sender, RoutedEventArgs e)
        {
            staff.IsFlameSwordFive = (bool)CbFlameSwordFive.IsChecked;
            staff.AfvTotal();
            ArtifactSpellsControl.SetSpellCounter();
        }
    }
}