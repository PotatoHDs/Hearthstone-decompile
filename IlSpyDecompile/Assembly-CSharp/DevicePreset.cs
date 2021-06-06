using System;

[Serializable]
public class DevicePreset : ICloneable
{
	public static readonly DevicePresetList s_devicePresets = new DevicePresetList
	{
		new DevicePreset
		{
			name = "No Emulation"
		},
		new DevicePreset
		{
			name = "PC",
			os = OSCategory.PC,
			screen = ScreenCategory.PC,
			input = InputCategory.Mouse
		},
		new DevicePreset
		{
			name = "iPhone",
			os = OSCategory.iOS,
			screen = ScreenCategory.Phone,
			input = InputCategory.Touch
		},
		new DevicePreset
		{
			name = "iPad",
			os = OSCategory.iOS,
			screen = ScreenCategory.Tablet,
			input = InputCategory.Touch
		},
		new DevicePreset
		{
			name = "Android Phone",
			os = OSCategory.Android,
			screen = ScreenCategory.Phone,
			input = InputCategory.Touch
		},
		new DevicePreset
		{
			name = "Android Tablet",
			os = OSCategory.Android,
			screen = ScreenCategory.Tablet,
			input = InputCategory.Touch,
			screenDensity = ScreenDensityCategory.Normal
		}
	};

	public string name = "No Emulation";

	public OSCategory os = OSCategory.PC;

	public InputCategory input;

	public ScreenCategory screen = ScreenCategory.PC;

	public ScreenDensityCategory screenDensity = ScreenDensityCategory.High;

	public object Clone()
	{
		return MemberwiseClone();
	}

	public void ReadFromConfig(ConfigFile config)
	{
		name = config.Get("Emulation.DeviceName", name.ToString());
		DevicePreset devicePreset = s_devicePresets.Find((DevicePreset x) => x.name.Equals(name));
		os = devicePreset.os;
		input = devicePreset.input;
		screen = devicePreset.screen;
		screenDensity = devicePreset.screenDensity;
	}

	public void WriteToConfig(ConfigFile config)
	{
		Log.ConfigFile.Print("Writing Emulated Device: " + name + " to " + config.GetPath());
		config.Set("Emulation.DeviceName", name.ToString());
		config.Delete("Emulation.OSCategory");
		config.Delete("Emulation.InputCategory");
		config.Delete("Emulation.ScreenCategory");
		config.Delete("Emulation.ScreenDensityCategory");
		config.Save();
	}
}
