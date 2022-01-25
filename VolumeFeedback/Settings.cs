using System.IO;
using System.Text.Json;
using VolumeFeedback.Models;

namespace VolumeFeedback;

public static class Settings
{
	public static SettingsStore? Load() => File.Exists(Files.ConfigFilePath) ? JsonSerializer.Deserialize<SettingsStore>(File.ReadAllText(Files.ConfigFilePath)) : new SettingsStore();

	public static void Save(SettingsStore settings) => File.WriteAllText(Files.ConfigFilePath, JsonSerializer.Serialize(settings));
	
}