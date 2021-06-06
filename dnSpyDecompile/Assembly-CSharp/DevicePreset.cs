using System;

// Token: 0x020009DD RID: 2525
[Serializable]
public class DevicePreset : ICloneable
{
	// Token: 0x0600891B RID: 35099 RVA: 0x002C15F4 File Offset: 0x002BF7F4
	public object Clone()
	{
		return base.MemberwiseClone();
	}

	// Token: 0x0600891C RID: 35100 RVA: 0x002C15FC File Offset: 0x002BF7FC
	public void ReadFromConfig(ConfigFile config)
	{
		this.name = config.Get("Emulation.DeviceName", this.name.ToString());
		DevicePreset devicePreset = DevicePreset.s_devicePresets.Find((DevicePreset x) => x.name.Equals(this.name));
		this.os = devicePreset.os;
		this.input = devicePreset.input;
		this.screen = devicePreset.screen;
		this.screenDensity = devicePreset.screenDensity;
	}

	// Token: 0x0600891D RID: 35101 RVA: 0x002C166C File Offset: 0x002BF86C
	public void WriteToConfig(ConfigFile config)
	{
		Log.ConfigFile.Print("Writing Emulated Device: " + this.name + " to " + config.GetPath(), Array.Empty<object>());
		config.Set("Emulation.DeviceName", this.name.ToString());
		config.Delete("Emulation.OSCategory", true);
		config.Delete("Emulation.InputCategory", true);
		config.Delete("Emulation.ScreenCategory", true);
		config.Delete("Emulation.ScreenDensityCategory", true);
		config.Save(null);
	}

	// Token: 0x0400732B RID: 29483
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

	// Token: 0x0400732C RID: 29484
	public string name = "No Emulation";

	// Token: 0x0400732D RID: 29485
	public OSCategory os = OSCategory.PC;

	// Token: 0x0400732E RID: 29486
	public InputCategory input;

	// Token: 0x0400732F RID: 29487
	public ScreenCategory screen = ScreenCategory.PC;

	// Token: 0x04007330 RID: 29488
	public ScreenDensityCategory screenDensity = ScreenDensityCategory.High;
}
