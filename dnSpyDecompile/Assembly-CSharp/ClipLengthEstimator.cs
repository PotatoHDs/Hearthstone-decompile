using System;
using System.Globalization;

// Token: 0x02000888 RID: 2184
public class ClipLengthEstimator
{
	// Token: 0x06007763 RID: 30563 RVA: 0x0026FC0C File Offset: 0x0026DE0C
	public static float StringToReadTime(string input)
	{
		float lengthInTextElements = (float)new StringInfo(input).LengthInTextElements;
		float num = ClipLengthEstimator.perLocaleCharacterReadTime[Locale.enUS];
		Locale locale = Localization.GetLocale();
		if (ClipLengthEstimator.perLocaleCharacterReadTime.ContainsKey(locale))
		{
			num = ClipLengthEstimator.perLocaleCharacterReadTime[locale];
		}
		float num2 = lengthInTextElements * num;
		if (num2 < ClipLengthEstimator.MINIMUM_SAFE_DURATION)
		{
			return ClipLengthEstimator.MINIMUM_SAFE_DURATION;
		}
		return num2;
	}

	// Token: 0x04005D8F RID: 23951
	public static readonly float MINIMUM_SAFE_DURATION = 0.5f;

	// Token: 0x04005D90 RID: 23952
	private static Map<Locale, float> perLocaleCharacterReadTime = new Map<Locale, float>
	{
		{
			Locale.enUS,
			0.075f
		},
		{
			Locale.koKR,
			0.375f
		},
		{
			Locale.zhTW,
			0.375f
		},
		{
			Locale.zhCN,
			0.375f
		}
	};
}
