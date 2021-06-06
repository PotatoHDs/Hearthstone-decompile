using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

public class DebugUtils
{
	[Conditional("UNITY_EDITOR")]
	public static void Assert(bool test)
	{
		if (!test)
		{
			UnityEngine.Debug.Break();
		}
	}

	[Conditional("UNITY_EDITOR")]
	public static void Assert(bool test, string message)
	{
		if (!test)
		{
			UnityEngine.Debug.LogWarning(message);
			UnityEngine.Debug.Break();
		}
	}

	public static string HashtableToString(Hashtable table)
	{
		string text = "";
		foreach (DictionaryEntry item in table)
		{
			text = string.Concat(text, item.Key, " = ", item.Value, "\n");
		}
		return text;
	}

	public static int CountParents(GameObject go)
	{
		int num = 0;
		if (go != null)
		{
			Transform parent = go.transform.parent;
			while (parent != null)
			{
				num++;
				parent = parent.transform.parent;
			}
		}
		return num;
	}

	public static string GetHierarchyPath(Object obj, char separator = '.')
	{
		StringBuilder stringBuilder = new StringBuilder();
		GetHierarchyPath_Internal(stringBuilder, obj, separator);
		return stringBuilder.ToString();
	}

	public static string GetHierarchyPathAndType(Object obj, char separator = '.')
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[Type]=").Append(obj.GetType().FullName).Append(" [Path]=");
		GetHierarchyPath_Internal(stringBuilder, obj, separator);
		return stringBuilder.ToString();
	}

	private static bool GetHierarchyPath_Internal(StringBuilder b, Object obj, char separator)
	{
		if (obj == null)
		{
			return false;
		}
		Transform transform = ((obj is GameObject) ? ((GameObject)obj).transform : ((obj is Component) ? ((Component)obj).transform : null));
		List<string> list = new List<string>();
		while (transform != null)
		{
			list.Insert(0, transform.gameObject.name);
			transform = transform.parent;
		}
		if (list.Count > 0 && separator == '/')
		{
			b.Append(separator);
		}
		for (int i = 0; i < list.Count; i++)
		{
			b.Append(list[i]);
			if (i < list.Count - 1)
			{
				b.Append(separator);
			}
		}
		return true;
	}

	public static object[] ToArray(IEnumerable e)
	{
		if (e != null)
		{
			return e.Cast<object>().ToArray();
		}
		return new object[0];
	}
}
