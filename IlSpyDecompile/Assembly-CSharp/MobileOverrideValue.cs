using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MobileOverrideValue<T>
{
	public ScreenCategory[] screens;

	public T[] values;

	public MobileOverrideValue()
	{
		screens = new ScreenCategory[1];
		screens[0] = ScreenCategory.PC;
		values = new T[1];
		values[0] = default(T);
	}

	public MobileOverrideValue(T defaultValue)
	{
		screens = new ScreenCategory[1] { ScreenCategory.PC };
		values = new T[1] { defaultValue };
	}

	public void Add(ScreenCategory screen, T value)
	{
		int num = Array.IndexOf(screens, screen);
		if (num != -1)
		{
			values[num] = value;
			return;
		}
		List<ScreenCategory> list = screens.ToList();
		List<T> list2 = values.ToList();
		list.Add(screen);
		list2.Add(value);
		screens = list.ToArray();
		values = list2.ToArray();
	}

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

	public T[] GetValues()
	{
		return values;
	}

	public T GetValueForScreen(ScreenCategory screen, object defaultValue)
	{
		int num = Array.IndexOf(screens, screen);
		if (num != -1)
		{
			return values[num];
		}
		return (T)defaultValue;
	}
}
