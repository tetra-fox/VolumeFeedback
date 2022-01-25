using System;
using System.IO;

namespace VolumeFeedback;

public static class Files
{
	private const string ConfigFileName = "config.json";
	private const string CustomFileName = "sound.wav";
	public static readonly string ConfigFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VolumeFeedback");
	public static readonly string ConfigFilePath = Path.Combine(ConfigFolderPath, ConfigFileName);
	public static readonly string CustomFilePath = Path.Combine(ConfigFolderPath, CustomFileName);
}