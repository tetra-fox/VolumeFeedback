using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace VolumeFeedback.Views;

public class SettingsView : UserControl
{
	public SettingsView() => this.InitializeComponent();

	private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
	
	private async void Button_OpenFile_Click(object? sender, RoutedEventArgs args)
	{
		string filepath = await this.GetFile();
		File.Copy(filepath, Files.CustomFilePath, true);
	}

	private async Task<string> GetFile()
	{
		OpenFileDialog dialog = new();
		dialog.Filters.Add(new FileDialogFilter { Name = "Audio File", Extensions =  { "wav", "mp3", "ogg", "flac", "aif" } });

		string[]? result = await dialog.ShowAsync((Window) this.GetVisualRoot());

		return string.Join("\\", result!);
	}
}