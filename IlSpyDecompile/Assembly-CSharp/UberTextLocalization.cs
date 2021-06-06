using UnityEngine;

public static class UberTextLocalization
{
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
