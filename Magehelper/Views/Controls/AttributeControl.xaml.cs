namespace Magehelper.Views.Controls;

public sealed partial class AttributeControl : UserControl
{
    public bool ShowIncreaseButton
    {
        get => (bool)GetValue(ShowIncreaseButtonProperty);
        set => SetValue(ShowIncreaseButtonProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(AttributeControl),
        new(default(string),
            OnTextChanged));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(int),
        typeof(AttributeControl),
        new(default(int),
            OnValueChanged));

    public static readonly DependencyProperty ShowIncreaseButtonProperty = DependencyProperty.Register(
        nameof(ShowIncreaseButton),
        typeof(bool),
        typeof(AttributeControl),
        new(default(bool),
            OnShowIncreaseButtonChanged));

    public delegate void ClickHandler(object sender, RoutedEventArgs e);

    public event ClickHandler? Click;

    public AttributeControl()
    {
        InitializeComponent();
        IsEnabledChanged += AttributeControl_IsEnabledChanged;
    }

    private void AttributeControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        NumberBox.IsEnabled = IsEnabled;
        Button.IsEnabled = IsEnabled;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => Click?.Invoke(sender, e);

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private static void OnShowIncreaseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button b = ((d as UserControl)!.Content as Grid)!.Children.OfType<Button>().FirstOrDefault()!;

        if ((bool)e.NewValue)
        {
            b.Visibility = Visibility.Visible;
        }
        else
        {
            b.Visibility = Visibility.Collapsed;
        }
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        (((d as UserControl)!.Content as Grid)!.Children[0] as StackPanel)!.Children.OfType<TextBlock>().FirstOrDefault()!.Text =
        e.NewValue.ToString();

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        (((d as UserControl)!.Content as Grid)!.Children[0] as StackPanel)!.Children.OfType<NumberBox>().FirstOrDefault()!.Value =
        (int)e.NewValue;

    private void NumberBox_OnValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args) => Value = (int)args.NewValue;
}
