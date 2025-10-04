using System.Collections.Specialized;

namespace Magehelper.ViewModels.Tabs;

public partial class TabArtifactViewModel : ObservableObject,
    IRecipient<FileActionMessage>,
    IRecipient<AddTraditionArtifactMessage>,
    IRecipient<DeleteTraditionArtifactMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    [ObservableProperty] private bool _canAddArtifact = true;
    [ObservableProperty] private string _tabName = "Traditionsartefakt";

    public ObservableCollection<string> ArtifactControls => [];

    public TabArtifactViewModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);

        if (Settings.GetInstance().ChangeTraditionArtifactTabName)
        {
            ArtifactControls.CollectionChanged += ArtifactControls_CollectionChanged;
        }
    }

    private void ArtifactControls_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        TabName = ArtifactControls.Count == 1 ? ArtifactControls[0] : "Traditionsartefakt";
    }

    public void ResetTab()
    {
        CanAddArtifact = true;
        ArtifactControls.Clear();
    }

    public void AddArtifact(string artifact)
    {
        ArtifactControls.Add(artifact);

        foreach (string artifactName in _core.ArtifactNames)
        {
            if (!TraditionalArtifactHelper.IsInitialized[artifactName])
            {
                CanAddArtifact = true;
                return;
            }

            CanAddArtifact = false;
        }
    }

    private void CreateStaff(string length, string material, string pasp)
    {
        Staff staff = new()
        {
            Length = Array.IndexOf(Staff.LengthStrings, length),
            Material = Array.IndexOf(Staff.MaterialStrings, material),
            Pasp = int.Parse(pasp)
        };

        staff.AfvTotal();
    }

    private void CreateCrystalBall(string material)
    {
        _ = new CrystalBall()
        {
            Material = material switch
            {
                "Künstlicher Kristall" => CrystalBallMaterial.ArtificialCrystal,
                "Natürlicher Kristall" => CrystalBallMaterial.NaturalCrystal,
                _ => CrystalBallMaterial.Glass
            }
        };
    }

    private void CreateBowl(string material)
    {
        _ = new Bowl()
        {
            Material = material == "Silber" ? BowlMaterial.Silber : BowlMaterial.Mondsilber
        };
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                ResetTab();

                break;
            case FileAction.Loaded:
                ResetTab();

                foreach (string artifact in _core.ArtifactNames)
                {
                    if (TraditionalArtifactHelper.IsInitialized[artifact])
                    {
                        AddArtifact(artifact);
                    }
                }

                break;
        }
    }

    public void Receive(AddTraditionArtifactMessage message)
    {
        switch (message.ArtifactName)
        {
            case "Magierstab":
                CreateStaff(message.AdditionalValues["StaffLength"], message.AdditionalValues["StaffMaterial"], message.AdditionalValues["AdditionalPasp"]);

                break;
            case "Kristallkugel":
                CreateCrystalBall(message.AdditionalValues["CrystalBallMaterial"]);

                break;
            case "Alchemistenschale":
                CreateBowl(message.AdditionalValues["BowlMaterial"]);
                break;
        }

        AddArtifact(message.ArtifactName);
    }

    public void Receive(DeleteTraditionArtifactMessage message)
    {
        ArtifactControls.Remove(ArtifactControls.First(c => c.GetType() == message.Value));
    }
}
