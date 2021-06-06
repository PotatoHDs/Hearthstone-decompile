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

// Token: 0x02000B3E RID: 2878
public class CardTextTool : MonoBehaviour
{
	// Token: 0x06009886 RID: 39046 RVA: 0x00315F58 File Offset: 0x00314158
	private void Start()
	{
		Application.targetFrameRate = 20;
		Application.runInBackground = false;
		Type[] serviceTypes = new Type[]
		{
			typeof(IAssetLoader),
			typeof(FontTable),
			typeof(GraphicsManager),
			typeof(ShaderTime)
		};
		HearthstoneServices.InitializeRuntimeServices(serviceTypes);
		IJobDependency[] dependencies = HearthstoneServices.CreateServiceDependencies(serviceTypes);
		Processor.QueueJob("CardTextTool.Initialize", this.Job_Initialize(), dependencies);
	}

	// Token: 0x06009887 RID: 39047 RVA: 0x00315FCA File Offset: 0x003141CA
	private void OnApplicationQuit()
	{
		PlayerPrefs.SetString("CARD_TEXT_NAME", this.m_nameText);
		PlayerPrefs.SetString("CARD_TEXT_DESCRIPTION", this.m_descriptionText);
		PlayerPrefs.Save();
	}

	// Token: 0x06009888 RID: 39048 RVA: 0x00315FF4 File Offset: 0x003141F4
	public void UpdateDescriptionText()
	{
		string text = this.m_DescriptionInputFiled.text;
		text = Regex.Replace(text, "(\\$|#)", "");
		this.m_descriptionText = text;
		text = this.FixedNewline(text);
		this.m_AbilityCardDescription.Text = text;
		this.m_AllyCardDescription.Text = text;
		this.m_WeaponCardDescription.Text = text;
		this.m_HeroCardDescription.Text = text;
		this.m_HeroPowerCardDescription.Text = text;
		this.m_BossCardDescription.Text = text;
	}

	// Token: 0x06009889 RID: 39049 RVA: 0x00316078 File Offset: 0x00314278
	public void UpdateNameText()
	{
		string text = this.m_NameInputFiled.text;
		this.m_nameText = text;
		this.m_AbilityCardName.Text = text;
		this.m_AllyCardName.Text = text;
		this.m_WeaponCardName.Text = text;
		this.m_HeroCardName.Text = text;
		this.m_HeroPowerName.Text = text;
		this.m_BossName.Text = text;
	}

	// Token: 0x0600988A RID: 39050 RVA: 0x003160E0 File Offset: 0x003142E0
	public void PasteClipboard()
	{
		PropertyInfo property = typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
		this.m_descriptionText = (string)property.GetValue(null, null);
		this.m_DescriptionInputFiled.text = this.m_descriptionText;
		this.UpdateDescriptionText();
	}

	// Token: 0x0600988B RID: 39051 RVA: 0x0031612E File Offset: 0x0031432E
	public void CopyToClipboard()
	{
		typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, this.m_descriptionText, null);
	}

	// Token: 0x0600988C RID: 39052 RVA: 0x00316153 File Offset: 0x00314353
	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		if (PlayerPrefs.HasKey("CARD_TEXT_LOCALE"))
		{
			this.m_locale = (Locale)PlayerPrefs.GetInt("CARD_TEXT_LOCALE");
		}
		Localization.SetLocale(this.m_locale);
		Localization.Initialize();
		this.SetupLocaleDropDown();
		this.SetLocale();
		this.m_AbilityActor.SetPortraitTexture(this.m_AbilityPortraitTexture);
		this.m_AllyActor.SetPortraitTexture(this.m_AllyPortraitTexture);
		this.m_WeaponActor.SetPortraitTexture(this.m_WeaponPortraitTexture);
		this.m_HeroActor.SetPortraitTexture(this.m_HeroPortraitTexture);
		this.m_HeroPowerActor.SetPortraitTexture(this.m_HeroPowerPortraitTexture);
		this.m_BossCardActor.SetPortraitTexture(this.m_BossPortraitTexture);
		if (PlayerPrefs.HasKey("CARD_TEXT_NAME"))
		{
			this.m_NameInputFiled.text = PlayerPrefs.GetString("CARD_TEXT_NAME");
		}
		if (PlayerPrefs.HasKey("CARD_TEXT_DESCRIPTION"))
		{
			this.m_DescriptionInputFiled.text = PlayerPrefs.GetString("CARD_TEXT_DESCRIPTION");
		}
		UberText[] componentsInChildren = this.m_CardsRoot.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Cache = false;
		}
		this.UpdateDescriptionText();
		this.UpdateNameText();
		yield break;
	}

	// Token: 0x0600988D RID: 39053 RVA: 0x00316164 File Offset: 0x00314364
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

	// Token: 0x0600988E RID: 39054 RVA: 0x003161DC File Offset: 0x003143DC
	private void SetupLocaleDropDown()
	{
		GameObject gameObject = this.m_LocaleDropDownSelectionButton.transform.parent.gameObject;
		gameObject.SetActive(true);
		foreach (object obj in Enum.GetValues(typeof(Locale)))
		{
			Locale locale = (Locale)obj;
			if (locale != Locale.UNKNOWN)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_LocaleDropDownSelectionButton.gameObject);
				gameObject2.transform.parent = this.m_LocaleDropDownSelectionButton.transform.parent;
				Button component = gameObject2.GetComponent<Button>();
				component.GetComponentInChildren<Text>().text = locale.ToString();
				Locale locSet = locale;
				component.onClick.AddListener(delegate()
				{
					this.OnClick_LocaleSetButton(locSet);
				});
			}
		}
		UnityEngine.Object.Destroy(this.m_LocaleDropDownSelectionButton.gameObject);
		this.SetLocaleButtonText(this.m_locale);
		gameObject.SetActive(false);
	}

	// Token: 0x0600988F RID: 39055 RVA: 0x003162F8 File Offset: 0x003144F8
	private void OnClick_LocaleSetButton(Locale locale)
	{
		this.m_LocaleDropDownMainButton.GetComponentInChildren<Text>().text = locale.ToString();
		this.m_locale = locale;
		this.SaveLocale(this.m_locale);
		this.SetLocale();
	}

	// Token: 0x06009890 RID: 39056 RVA: 0x00316330 File Offset: 0x00314530
	private void SetLocaleButtonText(Locale loc)
	{
		this.m_LocaleDropDownMainButton.GetComponentInChildren<Text>().text = loc.ToString();
	}

	// Token: 0x06009891 RID: 39057 RVA: 0x0031634F File Offset: 0x0031454F
	private void SaveLocale(Locale loc)
	{
		PlayerPrefs.SetInt("CARD_TEXT_LOCALE", (int)this.m_locale);
		PlayerPrefs.Save();
	}

	// Token: 0x06009892 RID: 39058 RVA: 0x00316366 File Offset: 0x00314566
	private void SetLocale()
	{
		base.StartCoroutine(this.SetLocaleCoroutine());
	}

	// Token: 0x06009893 RID: 39059 RVA: 0x00316375 File Offset: 0x00314575
	private IEnumerator SetLocaleCoroutine()
	{
		Localization.SetLocale(this.m_locale);
		yield return null;
		this.UpdateCardFonts(Locale.enUS);
		this.UpdateCardFonts(this.m_locale);
		yield break;
	}

	// Token: 0x06009894 RID: 39060 RVA: 0x00316384 File Offset: 0x00314584
	private void UpdateCardFonts(Locale loc)
	{
		foreach (CardTextTool.LocalizedFont localizedFont in this.m_LocalizedFontCollection)
		{
			if (localizedFont.m_Locale == loc)
			{
				if (localizedFont.m_FontDef.name == "FranklinGothic")
				{
					this.m_AbilityCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_AllyCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_WeaponCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_HeroCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_HeroPowerCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_BossCardDescription.SetFontWithoutLocalization(localizedFont.m_FontDef);
				}
				if (localizedFont.m_FontDef.name == "Belwe_Outline")
				{
					this.m_AbilityCardName.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_AllyCardName.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_WeaponCardName.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_HeroCardName.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_HeroPowerName.SetFontWithoutLocalization(localizedFont.m_FontDef);
					this.m_BossName.SetFontWithoutLocalization(localizedFont.m_FontDef);
				}
			}
		}
	}

	// Token: 0x04007F81 RID: 32641
	private const string PREFS_LOCALE = "CARD_TEXT_LOCALE";

	// Token: 0x04007F82 RID: 32642
	private const string PREFS_NAME = "CARD_TEXT_NAME";

	// Token: 0x04007F83 RID: 32643
	private const string PREFS_DESCRIPTION = "CARD_TEXT_DESCRIPTION";

	// Token: 0x04007F84 RID: 32644
	public GameObject m_CardsRoot;

	// Token: 0x04007F85 RID: 32645
	public Actor m_AbilityActor;

	// Token: 0x04007F86 RID: 32646
	public Actor m_AllyActor;

	// Token: 0x04007F87 RID: 32647
	public Actor m_WeaponActor;

	// Token: 0x04007F88 RID: 32648
	public Actor m_HeroActor;

	// Token: 0x04007F89 RID: 32649
	public Actor m_HeroPowerActor;

	// Token: 0x04007F8A RID: 32650
	public Actor m_BossCardActor;

	// Token: 0x04007F8B RID: 32651
	public Texture2D m_AbilityPortraitTexture;

	// Token: 0x04007F8C RID: 32652
	public Texture2D m_AllyPortraitTexture;

	// Token: 0x04007F8D RID: 32653
	public Texture2D m_WeaponPortraitTexture;

	// Token: 0x04007F8E RID: 32654
	public Texture2D m_HeroPortraitTexture;

	// Token: 0x04007F8F RID: 32655
	public Texture2D m_HeroPowerPortraitTexture;

	// Token: 0x04007F90 RID: 32656
	public Texture2D m_BossPortraitTexture;

	// Token: 0x04007F91 RID: 32657
	public UberText m_AbilityCardDescription;

	// Token: 0x04007F92 RID: 32658
	public UberText m_AllyCardDescription;

	// Token: 0x04007F93 RID: 32659
	public UberText m_WeaponCardDescription;

	// Token: 0x04007F94 RID: 32660
	public UberText m_HeroCardDescription;

	// Token: 0x04007F95 RID: 32661
	public UberText m_HeroPowerCardDescription;

	// Token: 0x04007F96 RID: 32662
	public UberText m_BossCardDescription;

	// Token: 0x04007F97 RID: 32663
	public InputField m_DescriptionInputFiled;

	// Token: 0x04007F98 RID: 32664
	public UberText m_AbilityCardName;

	// Token: 0x04007F99 RID: 32665
	public UberText m_AllyCardName;

	// Token: 0x04007F9A RID: 32666
	public UberText m_WeaponCardName;

	// Token: 0x04007F9B RID: 32667
	public UberText m_HeroCardName;

	// Token: 0x04007F9C RID: 32668
	public UberText m_HeroPowerName;

	// Token: 0x04007F9D RID: 32669
	public UberText m_BossName;

	// Token: 0x04007F9E RID: 32670
	public InputField m_NameInputFiled;

	// Token: 0x04007F9F RID: 32671
	public Button m_LocaleDropDownMainButton;

	// Token: 0x04007FA0 RID: 32672
	public Button m_LocaleDropDownSelectionButton;

	// Token: 0x04007FA1 RID: 32673
	public List<CardTextTool.LocalizedFont> m_LocalizedFontCollection;

	// Token: 0x04007FA2 RID: 32674
	private string m_nameText;

	// Token: 0x04007FA3 RID: 32675
	private string m_descriptionText;

	// Token: 0x04007FA4 RID: 32676
	private Locale m_locale;

	// Token: 0x02002780 RID: 10112
	[Serializable]
	public class LocalizedFont
	{
		// Token: 0x0400F430 RID: 62512
		public Locale m_Locale;

		// Token: 0x0400F431 RID: 62513
		public FontDefinition m_FontDef;
	}
}
