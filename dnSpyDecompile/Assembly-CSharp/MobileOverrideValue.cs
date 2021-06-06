using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020005EB RID: 1515
[Serializable]
public class MobileOverrideValue<T>
{
	// Token: 0x060052F5 RID: 21237 RVA: 0x001B202C File Offset: 0x001B022C
	public MobileOverrideValue()
	{
		this.screens = new ScreenCategory[1];
		this.screens[0] = ScreenCategory.PC;
		this.values = new T[1];
		this.values[0] = default(T);
	}

	// Token: 0x060052F6 RID: 21238 RVA: 0x001B2075 File Offset: 0x001B0275
	public MobileOverrideValue(T defaultValue)
	{
		this.screens = new ScreenCategory[]
		{
			ScreenCategory.PC
		};
		this.values = new T[]
		{
			defaultValue
		};
	}

	// Token: 0x060052F7 RID: 21239 RVA: 0x001B20A4 File Offset: 0x001B02A4
	public void Add(ScreenCategory screen, T value)
	{
		int num = Array.IndexOf<ScreenCategory>(this.screens, screen);
		if (num != -1)
		{
			this.values[num] = value;
			return;
		}
		List<ScreenCategory> list = this.screens.ToList<ScreenCategory>();
		List<T> list2 = this.values.ToList<T>();
		list.Add(screen);
		list2.Add(value);
		this.screens = list.ToArray();
		this.values = list2.ToArray();
	}

	// Token: 0x060052F8 RID: 21240 RVA: 0x001B2110 File Offset: 0x001B0310
	public static implicit operator T(MobileOverrideValue<T> val)
	{
		if (val == null)
		{
			return default(T);
		}
		ScreenCategory[] array = val.screens;
		T[] array2 = val.values;
		if (array.Length < 1)
		{
			Debug.LogError("MobileOverrideValue should always have at least one value!");
			return default(T);
		}
		T result = array2[0];
		ScreenCategory screen = PlatformSettings.Screen;
		for (int i = 1; i < array.Length; i++)
		{
			if (screen == array[i])
			{
				result = array2[i];
			}
		}
		return result;
	}

	// Token: 0x060052F9 RID: 21241 RVA: 0x001B2185 File Offset: 0x001B0385
	public T[] GetValues()
	{
		return this.values;
	}

	// Token: 0x060052FA RID: 21242 RVA: 0x001B2190 File Offset: 0x001B0390
	public T GetValueForScreen(ScreenCategory screen, object defaultValue)
	{
		int num = Array.IndexOf<ScreenCategory>(this.screens, screen);
		if (num != -1)
		{
			return this.values[num];
		}
		return (T)((object)defaultValue);
	}

	// Token: 0x040049C7 RID: 18887
	public ScreenCategory[] screens;

	// Token: 0x040049C8 RID: 18888
	public T[] values;
}
