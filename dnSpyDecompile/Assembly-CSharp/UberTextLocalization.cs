using System;
using UnityEngine;

// Token: 0x02000AAB RID: 2731
public static class UberTextLocalization
{
	// Token: 0x06009229 RID: 37417 RVA: 0x002F7F60 File Offset: 0x002F6160
	public static Vector3 GetPositionOffset(UberText.LocalizationSettings localizedSettings)
	{
		Vector3 result = Vector3.zero;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_PositionOffset;
			}
		}
		return result;
	}

	// Token: 0x0600922A RID: 37418 RVA: 0x002F7F90 File Offset: 0x002F6190
	public static float GetUnboundedCharSize(UberText.LocalizationSettings localizedSettings)
	{
		float result = 0f;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_UnboundCharacterSizeModifier;
			}
		}
		return result;
	}

	// Token: 0x0600922B RID: 37419 RVA: 0x002F7FC0 File Offset: 0x002F61C0
	public static float GetOutlineModifier(UberText.LocalizationSettings localizedSettings)
	{
		float result = 1f;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_OutlineModifier;
			}
		}
		return result;
	}

	// Token: 0x0600922C RID: 37420 RVA: 0x002F7FF0 File Offset: 0x002F61F0
	public static float GetSingleLineAdjustment(UberText.LocalizationSettings localizedSettings)
	{
		float result = 0f;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_SingleLineAdjustment;
			}
		}
		return result;
	}

	// Token: 0x0600922D RID: 37421 RVA: 0x002F8020 File Offset: 0x002F6220
	public static float GetLineSpaceModifier(UberText.LocalizationSettings localizedSettings)
	{
		float result = 1f;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_LineSpaceModifier;
			}
		}
		return result;
	}

	// Token: 0x0600922E RID: 37422 RVA: 0x002F8050 File Offset: 0x002F6250
	public static float GetFontSizeModifier(UberText.LocalizationSettings localizedSettings)
	{
		float result = 1f;
		if (localizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = localizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null)
			{
				result = locale.m_FontSizeModifier;
			}
		}
		return result;
	}
}
