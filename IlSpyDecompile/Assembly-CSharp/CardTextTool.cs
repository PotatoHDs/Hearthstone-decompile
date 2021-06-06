using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using UnityEngine;
using UnityEngine.UI;

public class CardTextTool : MonoBehaviour
{
	[Serializable]
	public class LocalizedFont
	{
		public Locale m_Locale;

		public FontDefinition m_FontDef;
	}

	private const string PREFS_LOCALE = "CARD_TEXT_LOCALE";

	private const string PREFS_NAME = "CARD_TEXT_NAME";

	private const string PREFS_DESCRIPTION = "CARD_TEXT_DESCRIPTION";

	public GameObject m_CardsRoot;

	public Actor m_AbilityActor;

	public Actor m_AllyActor;

	public Actor m_WeaponActor;

	public Actor m_HeroActor;

	public Actor m_HeroPowerActor;

	public Actor m_BossCardActor;

	public Texture2D m_AbilityPortraitTexture;

	public Texture2D m_AllyPortraitTexture;

	public Texture2D m_WeaponPortraitTexture;

	public Texture2D m_HeroPortraitTexture;

	public Texture2D m_HeroPowerPortraitTexture;

	public Texture2D m_BossPortraitTexture;

	public UberText m_AbilityCardDescription;

	public UberText m_AllyCardDescription;

	public UberText m_WeaponCardDescription;

	public UberText m_HeroCardDescription;

	public UberText m_HeroPowerCardDescription;

	public UberText m_BossCardDescription;

	public InputField m_DescriptionInputFiled;

	public UberText m_AbilityCardName;

	public UberText m_AllyCardName;

	public UberText m_WeaponCardName;

	public UberText m_HeroCardName;

	public UberText m_HeroPowerName;

	public UberText m_BossName;

	public InputField m_NameInputFiled;

	public Button m_LocaleDropDownMainButton;

	public Button m_LocaleDropDownSelectionButton;

	public List<LocalizedFont> m_LocalizedFontCollection;

	private string m_nameText;

	private string m_descriptionText;

	private Locale m_locale;

	private void Start()
	{
		Application.targetFrameRate = 20;
		Application.runInBackground = false;
		Type[] serviceTypes = new Type[4]
		{
			typeof(IAssetLoader),
			typeof(FontTable),
			typeof(GraphicsManager),
			typeof(ShaderTime)
		};
		HearthstoneServices.InitializeRuntimeServices(serviceTypes);
		IJobDependency[] dependencies = HearthstoneServices.CreateServiceDependencies(serviceTypes);
		Processor.QueueJob("CardTextTool.Initialize", Job_Initialize(), dependencies);
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.SetString("CARD_TEXT_NAME", m_nameText);
		PlayerPrefs.SetString("CARD_TEXT_DESCRIPTION", m_descriptionText);
		PlayerPrefs.Save();
	}

	public void UpdateDescriptionText()
	{
		string text = m_DescriptionInputFiled.text;
		text = FixedNewline(m_descriptionText = Regex.Replace(text, "(\\$|#)", ""));
		m_AbilityCardDescription.Text = text;
		m_AllyCardDescription.Text = text;
		m_WeaponCardDescription.Text = text;
		m_HeroCardDescription.Text = text;
		m_HeroPowerCardDescription.Text = text;
		m_BossCardDescription.Text = text;
	}

	public void UpdateNameText()
	{
		string text = (m_nameText = m_NameInputFiled.text);
		m_AbilityCardName.Text = text;
		m_AllyCardName.Text = text;
		m_WeaponCardName.Text = text;
		m_HeroCardName.Text = text;
		m_HeroPowerName.Text = text;
		m_BossName.Text = text;
	}

	public void PasteClipboard()
	{
		PropertyInfo property = typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
		m_descriptionText = (string)property.GetValue(null, null);
		m_DescriptionInputFiled.text = m_descriptionText;
		UpdateDescriptionText();
	}

	public void CopyToClipboard()
	{
		typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, m_descriptionText, null);
	}

	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		if (PlayerPrefs.HasKey("CARD_TEXT_LOCALE"))
		{
			m_locale = (Locale)PlayerPrefs.GetInt("CARD_TEXT_LOCALE");
		}
		Localization.SetLocale(m_locale);
		Localization.Initialize();
		SetupLocaleDropDown();
		SetLocale();
		m_AbilityActor.SetPortraitTexture(m_AbilityPortraitTexture);
		m_AllyActor.SetPortraitTexture(m_AllyPortraitTexture);
		m_WeaponActor.SetPortraitTexture(m_WeaponPortraitTexture);
		m_HeroActor.SetPortraitTexture(m_HeroPortraitTexture);
		m_HeroPowerActor.SetPortraitTexture(m_HeroPowerPortraitTexture);
		m_BossCardActor.SetPortraitTexture(m_BossPortraitTexture);
		if (PlayerPrefs.HasKey("CARD_TEXT_NAME"))
		{
			m_NameInputFiled.text = PlayerPrefs.GetString("CARD_TEXT_NAME");
		}
		if (PlayerPrefs.HasKey("CARD_TEXT_DESCRIPTION"))
		{
			m_DescriptionInputFiled.text = PlayerPrefs.GetString("CARD_TEXT_DESCRIPTION");
		}
		UberText[] componentsInChildren = m_CardsRoot.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Cache = false;
		}
		UpdateDescriptionText();
		UpdateNameText();
		yield break;
	}

	private string FixedNewline(string text)
	{
		if (text.Length < 2)
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < text.Length; i++)
		{
			if (i + 1 < text.Length && text[i] == '\\' && text[i + 1] == 'n')
			{
				stringBuilder.Append('\n');
				i++;
			}
			else
			{
				stringBuilder.Append(text[i]);
			}
		}
		return stringBuilder.ToString();
	}

	private void SetupLocaleDropDown()
	{
		GameObject gameObject = m_LocaleDropDownSelectionButton.transform.parent.gameObject;
		gameObject.SetActive(value: true);
		foreach (Locale value in Enum.GetValues(typeof(Locale)))
		{
			if (value != Locale.UNKNOWN)
			{
				GameObject obj = UnityEngine.Object.Instantiate(m_LocaleDropDownSelectionButton.gameObject);
				obj.transform.parent = m_LocaleDropDownSelectionButton.transform.parent;
				Button component = obj.GetComponent<Button>();
				component.GetComponentInChildren<Text>().text = value.ToString();
				Locale locSet = value;
				component.onClick.AddListener(delegate
				{
					OnClick_LocaleSetButton(locSet);
				});
			}
		}
		UnityEngine.Object.Destroy(m_LocaleDropDownSelectionButton.gameObject);
		SetLocaleButtonText(m_locale);
		gameObject.SetActive(value: false);
	}

	private void OnClick_LocaleSetButton(Locale locale)
	{
		m_LocaleDropDownMainButton.GetComponentInChildren<Text>().text = locale.ToString();
		m_locale = locale;
		SaveLocale(m_locale);
		SetLocale();
	}

	private void SetLocaleButtonText(Locale loc)
	{
		m_LocaleDropDownMainButton.GetComponentInChildren<Text>().text = loc.ToString();
	}

	private void SaveLocale(Locale loc)
	{
		PlayerPrefs.SetInt("CARD_TEXT_LOCALE", (int)m_locale);
		PlayerPrefs.Save();
	}

	private void SetLocale()
	{
		StartCoroutine(SetLocaleCoroutine());
	}

	private IEnumerator SetLocaleCoroutine()
	{
		Localization.SetLocale(m_locale);
		yield return null;
		UpdateCardFonts(Locale.enUS);
		UpdateCardFonts(m_locale);
	}

	private void UpdateCardFonts(Locale loc)
	{
		foreach (LocalizedFont item in m_LocalizedFontCollection)
		{
			if (item.m_Locale == loc)
			{
				if (item.m_FontDef.name == "FranklinGothic")
				{
					m_AbilityCardDescription.SetFontWithoutLocalization(item.m_FontDef);
					m_AllyCardDescription.SetFontWithoutLocalization(item.m_FontDef);
					m_WeaponCardDescription.SetFontWithoutLocalization(item.m_FontDef);
					m_HeroCardDescription.SetFontWithoutLocalization(item.m_FontDef);
					m_HeroPowerCardDescription.SetFontWithoutLocalization(item.m_FontDef);
					m_BossCardDescription.SetFontWithoutLocalization(item.m_FontDef);
				}
				if (item.m_FontDef.name == "Belwe_Outline")
				{
					m_AbilityCardName.SetFontWithoutLocalization(item.m_FontDef);
					m_AllyCardName.SetFontWithoutLocalization(item.m_FontDef);
					m_WeaponCardName.SetFontWithoutLocalization(item.m_FontDef);
					m_HeroCardName.SetFontWithoutLocalization(item.m_FontDef);
					m_HeroPowerName.SetFontWithoutLocalization(item.m_FontDef);
					m_BossName.SetFontWithoutLocalization(item.m_FontDef);
				}
			}
		}
	}
}
