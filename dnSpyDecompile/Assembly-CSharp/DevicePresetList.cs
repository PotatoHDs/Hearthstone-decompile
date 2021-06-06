using System;
using System.Collections.Generic;

// Token: 0x020009DE RID: 2526
public class DevicePresetList : List<DevicePreset>
{
	// Token: 0x06008921 RID: 35105 RVA: 0x002C1840 File Offset: 0x002BFA40
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
