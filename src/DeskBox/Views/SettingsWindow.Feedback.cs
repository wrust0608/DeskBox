using System;
using Microsoft.UI.Xaml;
using Windows.System;

namespace DeskBox.Views;

public sealed partial class SettingsWindow
{
    private async void ShowFeedbackDialogButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string subject = Uri.EscapeDataString("[DeskBox Vietnamese] Phản hồi");
            string mailtoUri = $"mailto:tieensn57@gmail.com?subject={subject}";
            await Launcher.LaunchUriAsync(new Uri(mailtoUri));
        }
        catch
        {
            // Fallback or ignore if no mail client configured
        }
    }
}
