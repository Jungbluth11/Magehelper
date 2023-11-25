using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentArtifact.xaml
    /// </summary>
    public partial class TabContentArtifact : UserControl
    {
        private readonly MainWindow mainWindow;
        public Staff Staff { get; private set; }
        public CrystalBall CrystalBall { get; private set; }
        public Bowl Bowl { get; private set; }
        public BoneCub BoneCub { get; private set; }
        public RingOfLife RingOfLife { get; private set; }
        public ObsidianDagger ObsidianDagger { get; private set; }
        public StaffControl StaffControl { get; private set; }
        public CrystalBallControl CrystalBallControl { get; private set; }
        public BowlControl BowlControl { get; private set; }
        public BoneCubControl BoneCubControl { get; private set; }
        public RingOfLifeControl RingOfLifeControl { get; private set; }
        public ObsidianDaggerControl ObsidianDaggerControl { get; private set; }

        public TabContentArtifact(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Core.AddArtifactGUIAction = AddArtifact;
        }

        public void AddArtifact(string artifact)
        {
            BtnAddArtifact.Visibility = Visibility.Collapsed;
            switch (artifact)
            {
                case "Alchemistenschale":
                    if (mainWindow.Core.Bowl is null)
                    {
                        Bowl = new Bowl(mainWindow.Core);
                    }
                    BowlControl = new BowlControl(mainWindow.Core.Bowl);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, BowlControl));
                    break;
                case "Knochenkeule":
                    if (mainWindow.Core.Bowl is null)
                    {
                        BoneCub = new BoneCub(mainWindow.Core);
                    }
                    BoneCubControl = new BoneCubControl(mainWindow.Core.BoneCub);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, BoneCubControl));
                    break;
                // only used with core.ReadFile()
                case "Kristallkugel":
                    CrystalBallControl = new CrystalBallControl(mainWindow.Core.CrystalBall);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, CrystalBallControl));
                    break;
                // only used with core.ReadFile() and core.ReadFileLegacy()
                case "Magierstab":
                    StaffControl = new StaffControl(mainWindow.Core.Staff, mainWindow);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, StaffControl));
                    if (mainWindow.Core.HasFlameSword)
                    {
                        mainWindow.TabContentFlameSword.EnableTab();
                    }
                    break;
                case "Ring des Lebens":
                    if (mainWindow.Core.Bowl is null)
                    {
                        RingOfLife = new RingOfLife(mainWindow.Core);
                    }
                    RingOfLifeControl = new RingOfLifeControl(mainWindow.Core.RingOfLife);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, RingOfLifeControl));
                    break;
                case "Vulkanglasdolch":
                    if (mainWindow.Core.Bowl is null)
                    {
                        ObsidianDagger = new ObsidianDagger(mainWindow.Core);
                    }
                    ObsidianDaggerControl = new ObsidianDaggerControl(mainWindow.Core.ObsidianDagger);
                    StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, ObsidianDaggerControl));
                    break;
                default:
                    break;
            }
        }

        internal void AddStaff(int length, int material, int pasp)
        {
            Staff = new Staff(mainWindow.Core);
            Staff.Length = length;
            Staff.Material = material;
            Staff.Pasp = pasp;
            Staff.AfvTotal();
            StaffControl = new StaffControl(Staff, mainWindow);
            BtnAddArtifact.Visibility = Visibility.Collapsed;
            StackPanelArtifacts.Children.Add(new ArtifactControl(Staff.Name, StaffControl));
            if (length == 1)
            {
                AddCrystalBallWindow addCrystalBallWindow = new AddCrystalBallWindow(this);
                addCrystalBallWindow.ShowDialog();
            }
        }

        internal void AddCrystallBall(CrystalBallMaterial crystalBallKind)
        {
            CrystalBall = new CrystalBall(mainWindow.Core);
            CrystalBall.Material = crystalBallKind;
            CrystalBallControl = new CrystalBallControl(CrystalBall);
            BtnAddArtifact.Visibility = Visibility.Collapsed;
            StackPanelArtifacts.Children.Add(new ArtifactControl(CrystalBall.Name, CrystalBallControl));
        }

        internal void ResetTab()
        {
            StackPanelArtifacts.Children.Clear();
            BtnAddArtifact.Visibility = Visibility.Visible;
        }

        private void BtnAddArtifact_Click(object sender, RoutedEventArgs e)
        {
            AddArtifactWindow addArtifactWindow = new AddArtifactWindow(mainWindow);
            if (addArtifactWindow.ShowDialog() == true)
            {
                switch (addArtifactWindow.SelectedArtifact)
                {
                    case "Magierstab":
                        new AddStaffWindow(this).ShowDialog();
                        break;
                    case "Kristallkugel":
                        new AddCrystalBallWindow(this).ShowDialog();
                        break;
                    default:
                        AddArtifact(addArtifactWindow.SelectedArtifact);
                        break;
                }
            }
        }
    }
}