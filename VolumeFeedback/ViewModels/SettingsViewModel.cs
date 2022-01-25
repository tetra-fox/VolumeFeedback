using VolumeFeedback.Models;

namespace VolumeFeedback.ViewModels;

public class SettingsViewModel : ViewModelBase
{
	public SettingsViewModel(SettingsStore? settings) => this.Settings = settings;

	private SettingsStore? Settings { get; }
}