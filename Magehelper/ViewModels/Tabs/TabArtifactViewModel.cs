namespace Magehelper.ViewModels.Tabs;

public partial class TabArtifactViewModel : ObservableObject,
    IRecipient<FileActionMessage>,
    IRecipient<AddArtifactMessage>,
    IRecipient<DeleteArtifactMessage>
{
    private readonly Artifacts _artifacts;
    public ObservableCollection<ArtifactControlViewModel> Artifacts { get; } = [];

    public TabArtifactViewModel()
    {
        _artifacts = Core.Core.GetInstance().Artifacts ?? [];
        WeakReferenceMessenger.Default.RegisterAll(this);
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

                foreach (Artifact artifact in _artifacts)
                {
                    Artifacts.Add(new(artifact));
                }
                break;
        }
    }

    public void Receive(AddArtifactMessage message)
    {
        Artifacts.Add(new(message.Value));
    }

    public void Receive(DeleteArtifactMessage message)
    {
        Artifacts.Remove(message.Value);
    }

    public void ResetTab()
    {
        Artifacts.Clear();
    }
}
