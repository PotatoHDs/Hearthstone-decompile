using System.Collections.Generic;

public class DevicePresetList : List<DevicePreset>
{
	public string[] GetNames()
	{
		string[] array = new string[base.Count];
		for (int i = 0; i < base.Count; i++)
		{
			array[i] = base[i].name;
		}
		return array;
	}
}
