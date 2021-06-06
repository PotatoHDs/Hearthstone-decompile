using System;

// Token: 0x020009B0 RID: 2480
public class DeeplinkUtils
{
	// Token: 0x06008707 RID: 34567 RVA: 0x002B9A7C File Offset: 0x002B7C7C
	public static Map<string, string> GetDeepLinkArgs(string[] deeplink)
	{
		Map<string, string> map = new Map<string, string>();
		if (deeplink != null && deeplink.Length != 0)
		{
			foreach (string text in DeeplinkUtils.GetArgsString(deeplink).Split(new char[]
			{
				'&'
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					'='
				});
				if (array2.Length != 2 || array2[0].Length == 0)
				{
					Log.DeepLink.PrintInfo("Skipping invalid formed arg {0}", new object[]
					{
						text
					});
				}
				else
				{
					string text2 = array2[0];
					string text3 = Uri.UnescapeDataString(array2[1]);
					if (map.ContainsKey(text2))
					{
						Log.DeepLink.PrintInfo("Duplicate arg {0} in deeplink, overwritting previous value {1} with {2}", new object[]
						{
							text2,
							map[text2],
							text3
						});
					}
					Log.DeepLink.PrintDebug("Found deeplink arg {0} = {1}", new object[]
					{
						text2,
						text3
					});
					map[text2] = text3;
				}
			}
		}
		return map;
	}

	// Token: 0x06008708 RID: 34568 RVA: 0x002B9B7C File Offset: 0x002B7D7C
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
