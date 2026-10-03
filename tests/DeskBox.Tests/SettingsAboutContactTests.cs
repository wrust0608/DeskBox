namespace DeskBox.Tests;

public sealed class SettingsAboutContactTests
{
    [Fact]
    public void AboutSection_UsesInAppFeedbackAndExposesNoEmailOrRepositoryButton()
    {
        string root = FindRepositoryRoot();
        string xaml = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Views/SettingsWindow.xaml"));
        string viewModel = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/ViewModels/SettingsViewModel.cs"));
        string aboutViewModel = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/ViewModels/SettingsViewModel.AboutAndUpdates.cs"));
        string responsiveLayout = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Views/SettingsWindow.xaml.cs"));
        string dialogCode = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Views/SettingsWindow.DataTools.cs"));
        string storeActions = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Views/SettingsWindow.StorageAndUpdates.cs"));
        string feedbackView = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Views/SettingsWindow.Feedback.cs"));
        string project = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/DeskBox.csproj"));
        string zhCn = File.ReadAllText(Path.Combine(
            root,
            "src/DeskBox/Strings/zh-CN.json"));
        var xamlDocument = System.Xml.Linq.XDocument.Parse(xaml);
        var versionText = Assert.Single(xamlDocument
            .Descendants()
            .Where(element =>
                element.Name.LocalName == "TextBlock" &&
                string.Equals(
                    element.Attribute("Text")?.Value,
                    "{Binding AppVersion}",
                    StringComparison.Ordinal)));
        var supportDialogContent = Assert.Single(xamlDocument
            .Descendants()
            .Where(element =>
                element.Name.LocalName == "StackPanel" &&
                string.Equals(
                    element.Attributes().FirstOrDefault(attribute => attribute.Name.LocalName == "Name")?.Value,
                    "SupportDialogContent",
                    StringComparison.Ordinal)));
        var customQrImage = Assert.Single(xamlDocument
            .Descendants()
            .Where(element =>
                element.Name.LocalName == "Image" &&
                string.Equals(
                    element.Attribute("Source")?.Value,
                    "ms-appx:///Assets/Support/custom-support-qr.png",
                    StringComparison.Ordinal)));

        Assert.Contains("Settings.About.FeedbackTitle", xaml, StringComparison.Ordinal);
        Assert.Contains("ShowFeedbackDialogButton_Click", xaml, StringComparison.Ordinal);
        Assert.Contains("Settings.About.FeedbackSendButton", xaml, StringComparison.Ordinal);
        Assert.Contains("x:Name=\"AboutRightPanel\"", xaml, StringComparison.Ordinal);
        Assert.Contains("Spacing=\"6\"", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("ShowMyFeedbackButton", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("FeedbackEmailButton", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("1047078635@qq.com", viewModel, StringComparison.Ordinal);
        Assert.DoesNotContain("1047078635@qq.com", feedbackView, StringComparison.Ordinal);
        Assert.DoesNotContain("FeedbackEmail", viewModel, StringComparison.Ordinal);
        Assert.DoesNotContain("FeedbackEmail", aboutViewModel, StringComparison.Ordinal);
        Assert.DoesNotContain("FeedbackEmail", responsiveLayout, StringComparison.Ordinal);
        Assert.DoesNotContain("FeedbackEmail", storeActions, StringComparison.Ordinal);
        Assert.DoesNotContain("mailto", xaml, StringComparison.Ordinal);
        Assert.Contains("mailto:tieensn57@gmail.com", feedbackView, StringComparison.Ordinal);
        Assert.Empty(ProductionSourcesContaining(root, "FeedbackEmail"));
        Assert.Empty(ProductionSourcesContaining(root, "1047078635"));
        Assert.Contains("Grid.SetRow(AboutRightPanel", responsiveLayout, StringComparison.Ordinal);
        Assert.Contains("x:Name=\"AboutMeDialog\"", xaml, StringComparison.Ordinal);
        Assert.Contains("ShowAboutMeButton_Click", xaml, StringComparison.Ordinal);
        Assert.Contains("Settings.Dialog.AboutMeP1", xaml, StringComparison.Ordinal);
        Assert.Contains("Settings.Dialog.AboutMeP2", xaml, StringComparison.Ordinal);
        Assert.Contains("Settings.Dialog.AboutMeP3", xaml, StringComparison.Ordinal);
        Assert.Contains("Text=\"{Binding AppVersion}\"", xaml, StringComparison.Ordinal);
        Assert.Null(versionText.Attribute("Foreground"));
        Assert.DoesNotContain("ms-appx:///Assets/wechat-qrcode.jpg", xaml, StringComparison.Ordinal);
        Assert.Contains("StoreSupportCardVisibility", xaml, StringComparison.Ordinal);
        Assert.Contains("x:Name=\"SupportDeskBoxDialog\"", xaml, StringComparison.Ordinal);
        Assert.Equal("480", supportDialogContent.Attribute("Width")?.Value);
        Assert.Equal("480", supportDialogContent.Attribute("MaxWidth")?.Value);
        Assert.Contains("ShowStoreSupportDialogButton_Click", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("OpenMicrosoftStoreButton_Click", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("ms-appx:///Assets/Support/support-wechat.png", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("ms-appx:///Assets/Support/support-alipay.png", xaml, StringComparison.Ordinal);
        Assert.Contains("ms-appx:///Assets/Support/custom-support-qr.png", xaml, StringComparison.Ordinal);
        Assert.True(File.Exists(Path.Combine(root, "src", "DeskBox", "Assets", "Support", "custom-support-qr.png")));
        Assert.Contains("https://apps.microsoft.com/detail/", viewModel, StringComparison.Ordinal);
        Assert.Contains("9PBZSNB4D69H", viewModel, StringComparison.Ordinal);
        Assert.Contains("deskbox_about_support", viewModel, StringComparison.Ordinal);
        Assert.Contains("?cid=", viewModel, StringComparison.Ordinal);
        Assert.Contains("ms-windows-store://pdp/?ProductId=", viewModel, StringComparison.Ordinal);
        Assert.Contains("&cid=", viewModel, StringComparison.Ordinal);
        Assert.Contains("IsDirectInstallerUpdateDelivery ? Visibility.Visible : Visibility.Collapsed", aboutViewModel, StringComparison.Ordinal);
        Assert.Contains("AboutMeDialog.ShowAsync", dialogCode, StringComparison.Ordinal);
        Assert.Contains("SupportDeskBoxDialog.ShowAsync", storeActions, StringComparison.Ordinal);
        Assert.Contains("ViewModel.StoreSupportCardVisibility != Visibility.Visible", storeActions, StringComparison.Ordinal);
        Assert.Contains("Assets\\Support\\*.png", project, StringComparison.Ordinal);
        Assert.DoesNotContain("AboutRepositoryButton", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("AboutRepositoryButton", responsiveLayout, StringComparison.Ordinal);
        Assert.DoesNotContain("OpenRepositoryButton_Click", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("ShowProductReasonButton_Click", dialogCode, StringComparison.Ordinal);
        Assert.DoesNotContain("AboutMeDialog.Title", dialogCode, StringComparison.Ordinal);
        Assert.DoesNotContain("AboutVersionTextBlock", xaml, StringComparison.Ordinal);
        Assert.DoesNotContain("AboutDeveloperText", xaml, StringComparison.Ordinal);
        Assert.Contains("<PublisherDisplayName>tẹt bị ngu</PublisherDisplayName>", File.ReadAllText(Path.Combine(root, "src/DeskBox/Package.appxmanifest")), StringComparison.Ordinal);
        Assert.Contains("#define MyAppPublisher \"tẹt bị ngu\"", File.ReadAllText(Path.Combine(root, "installer/DeskBox.iss")), StringComparison.Ordinal);
    }

    private static string[] ProductionSourcesContaining(
        string root,
        string needle,
        params string[] relativeDirectories)
    {
        string projectDirectory = Path.Combine(root, "src", "DeskBox");
        IEnumerable<string> searchRoots = relativeDirectories.Length == 0
            ? [projectDirectory]
            : relativeDirectories.Select(directory => Path.Combine(projectDirectory, directory));

        return searchRoots
            .SelectMany(directory => Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories))
            .Where(path =>
            {
                string relative = Path.GetRelativePath(projectDirectory, path)
                    .Replace(Path.DirectorySeparatorChar, '/');
                return !relative.StartsWith("bin/", StringComparison.OrdinalIgnoreCase) &&
                       !relative.StartsWith("obj/", StringComparison.OrdinalIgnoreCase) &&
                       !relative.StartsWith("AppPackages/", StringComparison.OrdinalIgnoreCase) &&
                       !relative.StartsWith("artifacts/", StringComparison.OrdinalIgnoreCase);
            })
            .Where(path => File.ReadAllText(path).Contains(needle, StringComparison.OrdinalIgnoreCase))
            .Select(path => Path.GetRelativePath(root, path))
            .Order()
            .ToArray();
    }

    private static int CountOccurrences(string source, string value)
    {
        int count = 0;
        int offset = 0;

        while ((offset = source.IndexOf(value, offset, StringComparison.Ordinal)) >= 0)
        {
            count++;
            offset += value.Length;
        }

        return count;
    }

    private static string FindRepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null)
        {
            if (File.Exists(Path.Combine(
                    current.FullName,
                    "src",
                    "DeskBox",
                    "DeskBox.csproj")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new DirectoryNotFoundException(
            "DeskBox repository root was not found.");
    }
}
