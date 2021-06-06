using System;

public class DeeplinkUtils
{
	public static Map<string, string> GetDeepLinkArgs(string[] deeplink)
	{
		Map<string, string> map = new Map<string, string>();
		if (deeplink != null && deeplink.Length != 0)
		{
			string[] array = GetArgsString(deeplink).Split('&');
			foreach (string text in array)
			{
				string[] array2 = text.Split('=');
				if (array2.Length != 2 || array2[0].Length == 0)
				{
					Log.DeepLink.PrintInfo("Skipping invalid formed arg {0}", text);
					continue;
				}
				string text2 = array2[0];
				string text3 = Uri.UnescapeDataString(array2[1]);
				if (map.ContainsKey(text2))
				{
					Log.DeepLink.PrintInfo("Duplicate arg {0} in deeplink, overwritting previous value {1} with {2}", text2, map[text2], text3);
				}
				Log.DeepLink.PrintDebug("Found deeplink arg {0} = {1}", text2, text3);
				map[text2] = text3;
			}
		}
		return map;
	}

	private static string GetArgsString(string[] deeplink)
	{
		int num = deeplink.Length - 1;
		string text = deeplink[num];
		int num2 = text.LastIndexOf('?') + 1;
		if (num2 >= text.Length)
		{
			return string.Empty;
		}
		return text.Substring(num2);
	}
}
