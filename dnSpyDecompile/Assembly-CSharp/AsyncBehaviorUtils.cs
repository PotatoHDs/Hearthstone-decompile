using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AC5 RID: 2757
public static class AsyncBehaviorUtils
{
	// Token: 0x06009330 RID: 37680 RVA: 0x002FBB0C File Offset: 0x002F9D0C
	public static List<IAsyncInitializationBehavior> GetAsyncBehaviors(Component component)
	{
		Component[] components = component.GetComponents<Component>();
		List<IAsyncInitializationBehavior> list = new List<IAsyncInitializationBehavior>();
		Component[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			IAsyncInitializationBehavior asyncInitializationBehavior = array[i] as IAsyncInitializationBehavior;
			if (asyncInitializationBehavior != null)
			{
				list.Add(asyncInitializationBehavior);
			}
		}
		return list;
	}
}
