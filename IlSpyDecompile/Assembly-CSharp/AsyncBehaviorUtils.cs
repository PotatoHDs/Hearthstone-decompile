using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public static class AsyncBehaviorUtils
{
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
