using System;
using UnityEngine;

// Token: 0x020009DB RID: 2523
[Serializable]
public class PlatformDependentValue<T>
{
	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x060088F1 RID: 35057 RVA: 0x002C1139 File Offset: 0x002BF339
	// (set) Token: 0x060088F2 RID: 35058 RVA: 0x002C1142 File Offset: 0x002BF342
	public T PC
	{
		get
		{
			return this.GetValue(PlatformSettingType.PC);
		}
		set
		{
			this.SetValue(PlatformSettingType.PC, value);
		}
	}

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x060088F3 RID: 35059 RVA: 0x002C114C File Offset: 0x002BF34C
	// (set) Token: 0x060088F4 RID: 35060 RVA: 0x002C1155 File Offset: 0x002BF355
	public T Mac
	{
		get
		{
			return this.GetValue(PlatformSettingType.Mac);
		}
		set
		{
			this.SetValue(PlatformSettingType.Mac, value);
		}
	}

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x060088F5 RID: 35061 RVA: 0x002C115F File Offset: 0x002BF35F
	// (set) Token: 0x060088F6 RID: 35062 RVA: 0x002C1168 File Offset: 0x002BF368
	public T iOS
	{
		get
		{
			return this.GetValue(PlatformSettingType.iOS);
		}
		set
		{
			this.SetValue(PlatformSettingType.iOS, value);
		}
	}

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x060088F7 RID: 35063 RVA: 0x002C1172 File Offset: 0x002BF372
	// (set) Token: 0x060088F8 RID: 35064 RVA: 0x002C117B File Offset: 0x002BF37B
	public T Android
	{
		get
		{
			return this.GetValue(PlatformSettingType.Android);
		}
		set
		{
			this.SetValue(PlatformSettingType.Android, value);
		}
	}

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x060088F9 RID: 35065 RVA: 0x002C1185 File Offset: 0x002BF385
	// (set) Token: 0x060088FA RID: 35066 RVA: 0x002C118E File Offset: 0x002BF38E
	public T Tablet
	{
		get
		{
			return this.GetValue(PlatformSettingType.Tablet);
		}
		set
		{
			this.SetValue(PlatformSettingType.Tablet, value);
		}
	}

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x060088FB RID: 35067 RVA: 0x002C1198 File Offset: 0x002BF398
	// (set) Token: 0x060088FC RID: 35068 RVA: 0x002C11A1 File Offset: 0x002BF3A1
	public T MiniTablet
	{
		get
		{
			return this.GetValue(PlatformSettingType.MiniTablet);
		}
		set
		{
			this.SetValue(PlatformSettingType.MiniTablet, value);
		}
	}

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x060088FD RID: 35069 RVA: 0x002C11AB File Offset: 0x002BF3AB
	// (set) Token: 0x060088FE RID: 35070 RVA: 0x002C11B4 File Offset: 0x002BF3B4
	public T Phone
	{
		get
		{
			return this.GetValue(PlatformSettingType.Phone);
		}
		set
		{
			this.SetValue(PlatformSettingType.Phone, value);
		}
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x060088FF RID: 35071 RVA: 0x002C11BE File Offset: 0x002BF3BE
	// (set) Token: 0x06008900 RID: 35072 RVA: 0x002C11C7 File Offset: 0x002BF3C7
	public T Mouse
	{
		get
		{
			return this.GetValue(PlatformSettingType.Mouse);
		}
		set
		{
			this.SetValue(PlatformSettingType.Mouse, value);
		}
	}

	// Token: 0x170007B2 RID: 1970
	// (get) Token: 0x06008901 RID: 35073 RVA: 0x002C11D1 File Offset: 0x002BF3D1
	// (set) Token: 0x06008902 RID: 35074 RVA: 0x002C11DA File Offset: 0x002BF3DA
	public T Touch
	{
		get
		{
			return this.GetValue(PlatformSettingType.Touch);
		}
		set
		{
			this.SetValue(PlatformSettingType.Touch, value);
		}
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x06008903 RID: 35075 RVA: 0x002C11E4 File Offset: 0x002BF3E4
	// (set) Token: 0x06008904 RID: 35076 RVA: 0x002C11EE File Offset: 0x002BF3EE
	public T LowMemory
	{
		get
		{
			return this.GetValue(PlatformSettingType.LowMemory);
		}
		set
		{
			this.SetValue(PlatformSettingType.LowMemory, value);
		}
	}

	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x06008905 RID: 35077 RVA: 0x002C11F9 File Offset: 0x002BF3F9
	// (set) Token: 0x06008906 RID: 35078 RVA: 0x002C1203 File Offset: 0x002BF403
	public T MediumMemory
	{
		get
		{
			return this.GetValue(PlatformSettingType.MediumMemory);
		}
		set
		{
			this.SetValue(PlatformSettingType.MediumMemory, value);
		}
	}

	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06008907 RID: 35079 RVA: 0x002C120E File Offset: 0x002BF40E
	// (set) Token: 0x06008908 RID: 35080 RVA: 0x002C1218 File Offset: 0x002BF418
	public T HighMemory
	{
		get
		{
			return this.GetValue(PlatformSettingType.HighMemory);
		}
		set
		{
			this.SetValue(PlatformSettingType.HighMemory, value);
		}
	}

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06008909 RID: 35081 RVA: 0x002C1223 File Offset: 0x002BF423
	// (set) Token: 0x0600890A RID: 35082 RVA: 0x002C122D File Offset: 0x002BF42D
	public T NormalScreenDensity
	{
		get
		{
			return this.GetValue(PlatformSettingType.NormalScreenDensity);
		}
		set
		{
			this.SetValue(PlatformSettingType.NormalScreenDensity, value);
		}
	}

	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x0600890B RID: 35083 RVA: 0x002C1238 File Offset: 0x002BF438
	// (set) Token: 0x0600890C RID: 35084 RVA: 0x002C1242 File Offset: 0x002BF442
	public T HighScreenDensity
	{
		get
		{
			return this.GetValue(PlatformSettingType.HighScreenDensity);
		}
		set
		{
			this.SetValue(PlatformSettingType.HighScreenDensity, value);
		}
	}

	// Token: 0x0600890D RID: 35085 RVA: 0x002C124D File Offset: 0x002BF44D
	public PlatformDependentValue(PlatformCategory t)
	{
		this.type = t;
		this.InitSettingsMap();
	}

	// Token: 0x0600890E RID: 35086 RVA: 0x002C127C File Offset: 0x002BF47C
	public static implicit operator T(PlatformDependentValue<T> val)
	{
		return val.Value;
	}

	// Token: 0x0600890F RID: 35087 RVA: 0x002C1284 File Offset: 0x002BF484
	public void Reset()
	{
		this.resolved = false;
	}

	// Token: 0x06008910 RID: 35088 RVA: 0x002C1290 File Offset: 0x002BF490
	private void InitSettingsMap()
	{
		for (int i = 0; i < 14; i++)
		{
			this.settings[i] = default(T);
			this.isSet[i] = false;
		}
	}

	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x06008911 RID: 35089 RVA: 0x002C12C8 File Offset: 0x002BF4C8
	public T Value
	{
		get
		{
			if (this.resolved)
			{
				return this.result;
			}
			switch (this.type)
			{
			case PlatformCategory.OS:
				this.result = this.GetOSSetting(PlatformSettings.OS);
				break;
			case PlatformCategory.Screen:
				this.result = this.GetScreenSetting(PlatformSettings.Screen);
				break;
			case PlatformCategory.Memory:
				this.result = this.GetMemorySetting(PlatformSettings.Memory);
				break;
			case PlatformCategory.Input:
				this.result = this.GetInputSetting(PlatformSettings.Input);
				break;
			}
			this.resolved = true;
			return this.result;
		}
	}

	// Token: 0x06008912 RID: 35090 RVA: 0x002C135E File Offset: 0x002BF55E
	private void SetValue(PlatformSettingType type, T value)
	{
		this.settings[(int)type] = value;
		this.isSet[(int)type] = true;
	}

	// Token: 0x06008913 RID: 35091 RVA: 0x002C1376 File Offset: 0x002BF576
	public T GetValue(PlatformSettingType type)
	{
		return this.settings[(int)type];
	}

	// Token: 0x06008914 RID: 35092 RVA: 0x002C1384 File Offset: 0x002BF584
	public bool IsSet(PlatformSettingType type)
	{
		return this.isSet[(int)type];
	}

	// Token: 0x06008915 RID: 35093 RVA: 0x002C1390 File Offset: 0x002BF590
	private T GetOSSetting(OSCategory os)
	{
		switch (os)
		{
		case OSCategory.PC:
			if (this.IsSet(PlatformSettingType.PC))
			{
				return this.GetValue(PlatformSettingType.PC);
			}
			break;
		case OSCategory.Mac:
			if (!this.IsSet(PlatformSettingType.Mac))
			{
				return this.GetOSSetting(OSCategory.PC);
			}
			return this.GetValue(PlatformSettingType.Mac);
		case OSCategory.iOS:
			if (!this.IsSet(PlatformSettingType.iOS))
			{
				return this.GetOSSetting(OSCategory.PC);
			}
			return this.GetValue(PlatformSettingType.iOS);
		case OSCategory.Android:
			if (!this.IsSet(PlatformSettingType.Android))
			{
				return this.GetOSSetting(OSCategory.PC);
			}
			return this.GetValue(PlatformSettingType.Android);
		}
		Debug.LogError("Could not find OS dependent value");
		return default(T);
	}

	// Token: 0x06008916 RID: 35094 RVA: 0x002C1428 File Offset: 0x002BF628
	private T GetScreenSetting(ScreenCategory screen)
	{
		switch (screen)
		{
		case ScreenCategory.Phone:
			if (!this.IsSet(PlatformSettingType.Phone))
			{
				return this.GetScreenSetting(ScreenCategory.Tablet);
			}
			return this.GetValue(PlatformSettingType.Phone);
		case ScreenCategory.MiniTablet:
			if (!this.IsSet(PlatformSettingType.MiniTablet))
			{
				return this.GetScreenSetting(ScreenCategory.Tablet);
			}
			return this.GetValue(PlatformSettingType.MiniTablet);
		case ScreenCategory.Tablet:
			if (!this.IsSet(PlatformSettingType.Tablet))
			{
				return this.GetScreenSetting(ScreenCategory.PC);
			}
			return this.GetValue(PlatformSettingType.Tablet);
		case ScreenCategory.PC:
			if (this.IsSet(PlatformSettingType.PC))
			{
				return this.GetValue(PlatformSettingType.PC);
			}
			break;
		}
		Debug.LogError("Could not find screen dependent value");
		return default(T);
	}

	// Token: 0x06008917 RID: 35095 RVA: 0x002C14C0 File Offset: 0x002BF6C0
	private T GetMemorySetting(MemoryCategory memory)
	{
		switch (memory)
		{
		case MemoryCategory.Low:
			if (this.IsSet(PlatformSettingType.LowMemory))
			{
				return this.GetValue(PlatformSettingType.LowMemory);
			}
			break;
		case MemoryCategory.Medium:
			if (!this.IsSet(PlatformSettingType.MediumMemory))
			{
				return this.GetMemorySetting(MemoryCategory.Low);
			}
			return this.GetValue(PlatformSettingType.MediumMemory);
		case MemoryCategory.High:
			if (!this.IsSet(PlatformSettingType.HighMemory))
			{
				return this.GetMemorySetting(MemoryCategory.Medium);
			}
			return this.GetValue(PlatformSettingType.HighMemory);
		}
		Debug.LogError("Could not find memory dependent value");
		return default(T);
	}

	// Token: 0x06008918 RID: 35096 RVA: 0x002C1540 File Offset: 0x002BF740
	private T GetInputSetting(InputCategory input)
	{
		if (input != InputCategory.Mouse)
		{
			if (input == InputCategory.Touch)
			{
				if (!this.IsSet(PlatformSettingType.Touch))
				{
					return this.GetInputSetting(InputCategory.Mouse);
				}
				return this.GetValue(PlatformSettingType.Touch);
			}
		}
		else if (this.IsSet(PlatformSettingType.Mouse))
		{
			return this.GetValue(PlatformSettingType.Mouse);
		}
		Debug.LogError("Could not find input dependent value");
		return default(T);
	}

	// Token: 0x06008919 RID: 35097 RVA: 0x002C1594 File Offset: 0x002BF794
	private T GetScreenDensitySetting(ScreenDensityCategory input)
	{
		if (input != ScreenDensityCategory.Normal)
		{
			if (input == ScreenDensityCategory.High)
			{
				if (!this.IsSet(PlatformSettingType.HighScreenDensity))
				{
					return this.GetScreenDensitySetting(ScreenDensityCategory.Normal);
				}
				return this.GetValue(PlatformSettingType.HighScreenDensity);
			}
		}
		else if (this.IsSet(PlatformSettingType.NormalScreenDensity))
		{
			return this.GetValue(PlatformSettingType.NormalScreenDensity);
		}
		Debug.LogError("Could not find screen density dependent value");
		return default(T);
	}

	// Token: 0x04007325 RID: 29477
	private bool resolved;

	// Token: 0x04007326 RID: 29478
	private T result;

	// Token: 0x04007327 RID: 29479
	[SerializeField]
	private PlatformCategory type;

	// Token: 0x04007328 RID: 29480
	private T defaultValue;

	// Token: 0x04007329 RID: 29481
	[SerializeField]
	private T[] settings = new T[14];

	// Token: 0x0400732A RID: 29482
	[SerializeField]
	private bool[] isSet = new bool[14];
}
