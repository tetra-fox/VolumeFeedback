using System;
using System.Text.Json.Serialization;
using VolumeFeedback.Enums;

namespace VolumeFeedback.Models;

public class SettingsStore
{
	private Sound _soundSetting;
	private bool _firstRun;
	
	public SettingsStore()
	{
		this.SoundSetting = Sound.New;
		this.OnPropertyChanged += () => Settings.Save(this);
	}
	
	public Sound SoundSetting
	{
		get => this._soundSetting;
		set
		{
			this._soundSetting = value;
			this.OnPropertyChanged?.Invoke();
		}
	}
	
	public bool FirstRun
	{
		get => this._firstRun;
		set
		{
			this._firstRun = value;
			this.OnPropertyChanged?.Invoke();
		}
	}
	
	public event Action OnPropertyChanged;
}