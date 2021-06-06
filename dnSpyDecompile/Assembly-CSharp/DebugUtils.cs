using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020009AF RID: 2479
public class DebugUtils
{
	// Token: 0x060086FE RID: 34558 RVA: 0x002B9878 File Offset: 0x002B7A78
	[Conditional("UNITY_EDITOR")]
	public static void Assert(bool test)
	{
		if (!test)
		{
			UnityEngine.Debug.Break();
		}
	}

	// Token: 0x060086FF RID: 34559 RVA: 0x002B9882 File Offset: 0x002B7A82
	[Conditional("UNITY_EDITOR")]
	public static void Assert(bool test, string message)
	{
		if (!test)
		{
			UnityEngine.Debug.LogWarning(message);
			UnityEngine.Debug.Break();
		}
	}

	// Token: 0x06008700 RID: 34560 RVA: 0x002B9894 File Offset: 0x002B7A94
	public static string HashtableToString(Hashtable table)
	{
		string text = "";
		foreach (object obj in table)
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
			text = string.Concat(new object[]
			{
				text,
				dictionaryEntry.Key,
				" = ",
				dictionaryEntry.Value,
				"\n"
			});
		}
		return text;
	}

	// Token: 0x06008701 RID: 34561 RVA: 0x002B991C File Offset: 0x002B7B1C
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

	// Token: 0x06008702 RID: 34562 RVA: 0x002B995C File Offset: 0x002B7B5C
	public static string GetHierarchyPath(UnityEngine.Object obj, char separator = '.')
	{
		StringBuilder stringBuilder = new StringBuilder();
		DebugUtils.GetHierarchyPath_Internal(stringBuilder, obj, separator);
		return stringBuilder.ToString();
	}

	// Token: 0x06008703 RID: 34563 RVA: 0x002B9971 File Offset: 0x002B7B71
	public static string GetHierarchyPathAndType(UnityEngine.Object obj, char separator = '.')
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[Type]=").Append(obj.GetType().FullName).Append(" [Path]=");
		DebugUtils.GetHierarchyPath_Internal(stringBuilder, obj, separator);
		return stringBuilder.ToString();
	}

	// Token: 0x06008704 RID: 34564 RVA: 0x002B99AC File Offset: 0x002B7BAC
	private static bool GetHierarchyPath_Internal(StringBuilder b, UnityEngine.Object obj, char separator)
	{
		if (obj == null)
		{
			return false;
		}
		Transform transform = (obj is GameObject) ? ((GameObject)obj).transform : ((obj is Component) ? ((Component)obj).transform : null);
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

	// Token: 0x06008705 RID: 34565 RVA: 0x002B9A63 File Offset: 0x002B7C63
	public static object[] ToArray(IEnumerable e)
	{
		if (e != null)
		{
			return e.Cast<object>().ToArray<object>();
		}
		return new object[0];
	}
}
