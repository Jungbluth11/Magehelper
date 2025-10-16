namespace Magehelper.Views.Dialogs;

public sealed partial class DiceResultDialog : ContentDialog
{
    public int[]? DiceResults { get; init; }
    public string? AdditionalText { get; init; }
    public bool AddResults { get; init; }

    public DiceResultDialog()
    {
        InitializeComponent();

        if (AddResults)
        {
            TextDiceResults.Text = DiceResults!.Sum().ToString();
        }
        else
        {
            TextDiceResults.Text = DiceResults!
                .Aggregate(string.Empty, (current, diceResult) => current + (diceResult + " ,"))
                .TrimEnd(',');
        }

        TextAdditionalText.Text = AdditionalText ?? string.Empty;
    }
}
