using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LoginPopupSequencePopupDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_loginPopupSequenceId;

	[SerializeField]
	private Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType m_popupType = Assets.LoginPopupSequencePopup.ParseLoginPopupSequencePopupTypeValue("basic");

	[SerializeField]
	private bool m_requiresWildUnlocked;

	[SerializeField]
	private bool m_suppressForReturningPlayer;

	[SerializeField]
	private string m_prefabOverride;

	[SerializeField]
	private string m_backgroundMaterial;

	[SerializeField]
	private DbfLocValue m_headerText;

	[SerializeField]
	private DbfLocValue m_bodyText;

	[SerializeField]
	private DbfLocValue m_buttonText;

	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_cardPremium;

	[SerializeField]
	private string m_featuredCardsEvent = "never";

	[DbfField("LOGIN_POPUP_SEQUENCE_ID")]
	public int LoginPopupSequenceId => m_loginPopupSequenceId;

	[DbfField("POPUP_TYPE")]
	public Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType PopupType => m_popupType;

	[DbfField("REQUIRES_WILD_UNLOCKED")]
	public bool RequiresWildUnlocked => m_requiresWildUnlocked;

	[DbfField("SUPPRESS_FOR_RETURNING_PLAYER")]
	public bool SuppressForReturningPlayer => m_suppressForReturningPlayer;

	[DbfField("PREFAB_OVERRIDE")]
	public string PrefabOverride => m_prefabOverride;

	[DbfField("BACKGROUND_MATERIAL")]
	public string BackgroundMaterial => m_backgroundMaterial;

	[DbfField("HEADER_TEXT")]
	public DbfLocValue HeaderText => m_headerText;

	[DbfField("BODY_TEXT")]
	public DbfLocValue BodyText => m_bodyText;

	[DbfField("BUTTON_TEXT")]
	public DbfLocValue ButtonText => m_buttonText;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	[DbfField("CARD_PREMIUM")]
	public int CardPremium => m_cardPremium;

	[DbfField("FEATURED_CARDS_EVENT")]
	public string FeaturedCardsEvent => m_featuredCardsEvent;

	public void SetLoginPopupSequenceId(int v)
	{
		m_loginPopupSequenceId = v;
	}

	public void SetPopupType(Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType v)
	{
		m_popupType = v;
	}

	public void SetRequiresWildUnlocked(bool v)
	{
		m_requiresWildUnlocked = v;
	}

	public void SetSuppressForReturningPlayer(bool v)
	{
		m_suppressForReturningPlayer = v;
	}

	public void SetPrefabOverride(string v)
	{
		m_prefabOverride = v;
	}

	public void SetBackgroundMaterial(string v)
	{
		m_backgroundMaterial = v;
	}

	public void SetHeaderText(DbfLocValue v)
	{
		m_headerText = v;
		v.SetDebugInfo(base.ID, "HEADER_TEXT");
	}

	public void SetBodyText(DbfLocValue v)
	{
		m_bodyText = v;
		v.SetDebugInfo(base.ID, "BODY_TEXT");
	}

	public void SetButtonText(DbfLocValue v)
	{
		m_buttonText = v;
		v.SetDebugInfo(base.ID, "BUTTON_TEXT");
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetCardPremium(int v)
	{
		m_cardPremium = v;
	}

	public void SetFeaturedCardsEvent(string v)
	{
		m_featuredCardsEvent = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"LOGIN_POPUP_SEQUENCE_ID" => m_loginPopupSequenceId, 
			"POPUP_TYPE" => m_popupType, 
			"REQUIRES_WILD_UNLOCKED" => m_requiresWildUnlocked, 
			"SUPPRESS_FOR_RETURNING_PLAYER" => m_suppressForReturningPlayer, 
			"PREFAB_OVERRIDE" => m_prefabOverride, 
			"BACKGROUND_MATERIAL" => m_backgroundMaterial, 
			"HEADER_TEXT" => m_headerText, 
			"BODY_TEXT" => m_bodyText, 
			"BUTTON_TEXT" => m_buttonText, 
			"CARD_ID" => m_cardId, 
			"CARD_PREMIUM" => m_cardPremium, 
			"FEATURED_CARDS_EVENT" => m_featuredCardsEvent, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "LOGIN_POPUP_SEQUENCE_ID":
			m_loginPopupSequenceId = (int)val;
			break;
		case "POPUP_TYPE":
			if (val == null)
			{
				m_popupType = Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType.INVALID;
			}
			else if (val is Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType || val is int)
			{
				m_popupType = (Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType)val;
			}
			else if (val is string)
			{
				m_popupType = Assets.LoginPopupSequencePopup.ParseLoginPopupSequencePopupTypeValue((string)val);
			}
			break;
		case "REQUIRES_WILD_UNLOCKED":
			m_requiresWildUnlocked = (bool)val;
			break;
		case "SUPPRESS_FOR_RETURNING_PLAYER":
			m_suppressForReturningPlayer = (bool)val;
			break;
		case "PREFAB_OVERRIDE":
			m_prefabOverride = (string)val;
			break;
		case "BACKGROUND_MATERIAL":
			m_backgroundMaterial = (string)val;
			break;
		case "HEADER_TEXT":
			m_headerText = (DbfLocValue)val;
			break;
		case "BODY_TEXT":
			m_bodyText = (DbfLocValue)val;
			break;
		case "BUTTON_TEXT":
			m_buttonText = (DbfLocValue)val;
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "CARD_PREMIUM":
			m_cardPremium = (int)val;
			break;
		case "FEATURED_CARDS_EVENT":
			m_featuredCardsEvent = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"LOGIN_POPUP_SEQUENCE_ID" => typeof(int), 
			"POPUP_TYPE" => typeof(Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType), 
			"REQUIRES_WILD_UNLOCKED" => typeof(bool), 
			"SUPPRESS_FOR_RETURNING_PLAYER" => typeof(bool), 
			"PREFAB_OVERRIDE" => typeof(string), 
			"BACKGROUND_MATERIAL" => typeof(string), 
			"HEADER_TEXT" => typeof(DbfLocValue), 
			"BODY_TEXT" => typeof(DbfLocValue), 
			"BUTTON_TEXT" => typeof(DbfLocValue), 
			"CARD_ID" => typeof(int), 
			"CARD_PREMIUM" => typeof(int), 
			"FEATURED_CARDS_EVENT" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginPopupSequencePopupDbfRecords loadRecords = new LoadLoginPopupSequencePopupDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginPopupSequencePopupDbfAsset loginPopupSequencePopupDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginPopupSequencePopupDbfAsset)) as LoginPopupSequencePopupDbfAsset;
		if (loginPopupSequencePopupDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LoginPopupSequencePopupDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < loginPopupSequencePopupDbfAsset.Records.Count; i++)
		{
			loginPopupSequencePopupDbfAsset.Records[i].StripUnusedLocales();
		}
		records = loginPopupSequencePopupDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_headerText.StripUnusedLocales();
		m_bodyText.StripUnusedLocales();
		m_buttonText.StripUnusedLocales();
	}
}
