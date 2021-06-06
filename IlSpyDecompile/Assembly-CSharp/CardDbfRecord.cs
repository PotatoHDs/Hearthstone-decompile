using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteMiniGuid;

	[SerializeField]
	private string m_longGuid;

	[SerializeField]
	private DbfLocValue m_textInHand;

	[SerializeField]
	private string m_gameplayEvent = "always";

	[SerializeField]
	private string m_craftingEvent = "always";

	[SerializeField]
	private string m_goldenCraftingEvent;

	[SerializeField]
	private int m_suggestionWeight;

	[SerializeField]
	private int m_changeVersion;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_flavorText;

	[SerializeField]
	private DbfLocValue m_howToGetCard;

	[SerializeField]
	private DbfLocValue m_howToGetGoldCard;

	[SerializeField]
	private DbfLocValue m_howToGetDiamondCard;

	[SerializeField]
	private DbfLocValue m_targetArrowText;

	[SerializeField]
	private string m_artistName;

	[SerializeField]
	private DbfLocValue m_shortName;

	[SerializeField]
	private string m_creditsCardName;

	[SerializeField]
	private string m_featuredCardsEvent;

	[SerializeField]
	private Assets.Card.CardTextBuilderType m_cardTextBuilderType;

	[SerializeField]
	private string m_watermarkTextureOverride;

	[DbfField("NOTE_MINI_GUID")]
	public string NoteMiniGuid => m_noteMiniGuid;

	[DbfField("LONG_GUID")]
	public string LongGuid => m_longGuid;

	[DbfField("TEXT_IN_HAND")]
	public DbfLocValue TextInHand => m_textInHand;

	[DbfField("GAMEPLAY_EVENT")]
	public string GameplayEvent => m_gameplayEvent;

	[DbfField("CRAFTING_EVENT")]
	public string CraftingEvent => m_craftingEvent;

	[DbfField("GOLDEN_CRAFTING_EVENT")]
	public string GoldenCraftingEvent => m_goldenCraftingEvent;

	[DbfField("SUGGESTION_WEIGHT")]
	public int SuggestionWeight => m_suggestionWeight;

	[DbfField("CHANGE_VERSION")]
	public int ChangeVersion => m_changeVersion;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("FLAVOR_TEXT")]
	public DbfLocValue FlavorText => m_flavorText;

	[DbfField("HOW_TO_GET_CARD")]
	public DbfLocValue HowToGetCard => m_howToGetCard;

	[DbfField("HOW_TO_GET_GOLD_CARD")]
	public DbfLocValue HowToGetGoldCard => m_howToGetGoldCard;

	[DbfField("HOW_TO_GET_DIAMOND_CARD")]
	public DbfLocValue HowToGetDiamondCard => m_howToGetDiamondCard;

	[DbfField("TARGET_ARROW_TEXT")]
	public DbfLocValue TargetArrowText => m_targetArrowText;

	[DbfField("ARTIST_NAME")]
	public string ArtistName => m_artistName;

	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName => m_shortName;

	[DbfField("CREDITS_CARD_NAME")]
	public string CreditsCardName => m_creditsCardName;

	[DbfField("FEATURED_CARDS_EVENT")]
	public string FeaturedCardsEvent => m_featuredCardsEvent;

	[DbfField("CARD_TEXT_BUILDER_TYPE")]
	public Assets.Card.CardTextBuilderType CardTextBuilderType => m_cardTextBuilderType;

	[DbfField("WATERMARK_TEXTURE_OVERRIDE")]
	public string WatermarkTextureOverride => m_watermarkTextureOverride;

	public CardHeroDbfRecord CardHero => GameDbf.CardHero.GetRecord((CardHeroDbfRecord r) => r.CardId == base.ID);

	public CardPlayerDeckOverrideDbfRecord PlayerDeckOverride => GameDbf.CardPlayerDeckOverride.GetRecord((CardPlayerDeckOverrideDbfRecord r) => r.CardId == base.ID);

	public List<CardAdditonalSearchTermsDbfRecord> SearchTerms => GameDbf.CardAdditonalSearchTerms.GetRecords((CardAdditonalSearchTermsDbfRecord r) => r.CardId == base.ID);

	public List<CardChangeDbfRecord> CardChanges => GameDbf.CardChange.GetRecords((CardChangeDbfRecord r) => r.CardId == base.ID);

	public List<CardSetTimingDbfRecord> CardSetTimings => GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord r) => r.CardId == base.ID);

	public List<CardTagDbfRecord> Tags => GameDbf.CardTag.GetRecords((CardTagDbfRecord r) => r.CardId == base.ID);

	public void SetNoteMiniGuid(string v)
	{
		m_noteMiniGuid = v;
	}

	public void SetLongGuid(string v)
	{
		m_longGuid = v;
	}

	public void SetTextInHand(DbfLocValue v)
	{
		m_textInHand = v;
		v.SetDebugInfo(base.ID, "TEXT_IN_HAND");
	}

	public void SetGameplayEvent(string v)
	{
		m_gameplayEvent = v;
	}

	public void SetCraftingEvent(string v)
	{
		m_craftingEvent = v;
	}

	public void SetGoldenCraftingEvent(string v)
	{
		m_goldenCraftingEvent = v;
	}

	public void SetSuggestionWeight(int v)
	{
		m_suggestionWeight = v;
	}

	public void SetChangeVersion(int v)
	{
		m_changeVersion = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetFlavorText(DbfLocValue v)
	{
		m_flavorText = v;
		v.SetDebugInfo(base.ID, "FLAVOR_TEXT");
	}

	public void SetHowToGetCard(DbfLocValue v)
	{
		m_howToGetCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_CARD");
	}

	public void SetHowToGetGoldCard(DbfLocValue v)
	{
		m_howToGetGoldCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_GOLD_CARD");
	}

	public void SetHowToGetDiamondCard(DbfLocValue v)
	{
		m_howToGetDiamondCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_DIAMOND_CARD");
	}

	public void SetTargetArrowText(DbfLocValue v)
	{
		m_targetArrowText = v;
		v.SetDebugInfo(base.ID, "TARGET_ARROW_TEXT");
	}

	public void SetArtistName(string v)
	{
		m_artistName = v;
	}

	public void SetShortName(DbfLocValue v)
	{
		m_shortName = v;
		v.SetDebugInfo(base.ID, "SHORT_NAME");
	}

	public void SetCreditsCardName(string v)
	{
		m_creditsCardName = v;
	}

	public void SetFeaturedCardsEvent(string v)
	{
		m_featuredCardsEvent = v;
	}

	public void SetCardTextBuilderType(Assets.Card.CardTextBuilderType v)
	{
		m_cardTextBuilderType = v;
	}

	public void SetWatermarkTextureOverride(string v)
	{
		m_watermarkTextureOverride = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_MINI_GUID" => m_noteMiniGuid, 
			"LONG_GUID" => m_longGuid, 
			"TEXT_IN_HAND" => m_textInHand, 
			"GAMEPLAY_EVENT" => m_gameplayEvent, 
			"CRAFTING_EVENT" => m_craftingEvent, 
			"GOLDEN_CRAFTING_EVENT" => m_goldenCraftingEvent, 
			"SUGGESTION_WEIGHT" => m_suggestionWeight, 
			"CHANGE_VERSION" => m_changeVersion, 
			"NAME" => m_name, 
			"FLAVOR_TEXT" => m_flavorText, 
			"HOW_TO_GET_CARD" => m_howToGetCard, 
			"HOW_TO_GET_GOLD_CARD" => m_howToGetGoldCard, 
			"HOW_TO_GET_DIAMOND_CARD" => m_howToGetDiamondCard, 
			"TARGET_ARROW_TEXT" => m_targetArrowText, 
			"ARTIST_NAME" => m_artistName, 
			"SHORT_NAME" => m_shortName, 
			"CREDITS_CARD_NAME" => m_creditsCardName, 
			"FEATURED_CARDS_EVENT" => m_featuredCardsEvent, 
			"CARD_TEXT_BUILDER_TYPE" => m_cardTextBuilderType, 
			"WATERMARK_TEXTURE_OVERRIDE" => m_watermarkTextureOverride, 
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
		case "NOTE_MINI_GUID":
			m_noteMiniGuid = (string)val;
			break;
		case "LONG_GUID":
			m_longGuid = (string)val;
			break;
		case "TEXT_IN_HAND":
			m_textInHand = (DbfLocValue)val;
			break;
		case "GAMEPLAY_EVENT":
			m_gameplayEvent = (string)val;
			break;
		case "CRAFTING_EVENT":
			m_craftingEvent = (string)val;
			break;
		case "GOLDEN_CRAFTING_EVENT":
			m_goldenCraftingEvent = (string)val;
			break;
		case "SUGGESTION_WEIGHT":
			m_suggestionWeight = (int)val;
			break;
		case "CHANGE_VERSION":
			m_changeVersion = (int)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "FLAVOR_TEXT":
			m_flavorText = (DbfLocValue)val;
			break;
		case "HOW_TO_GET_CARD":
			m_howToGetCard = (DbfLocValue)val;
			break;
		case "HOW_TO_GET_GOLD_CARD":
			m_howToGetGoldCard = (DbfLocValue)val;
			break;
		case "HOW_TO_GET_DIAMOND_CARD":
			m_howToGetDiamondCard = (DbfLocValue)val;
			break;
		case "TARGET_ARROW_TEXT":
			m_targetArrowText = (DbfLocValue)val;
			break;
		case "ARTIST_NAME":
			m_artistName = (string)val;
			break;
		case "SHORT_NAME":
			m_shortName = (DbfLocValue)val;
			break;
		case "CREDITS_CARD_NAME":
			m_creditsCardName = (string)val;
			break;
		case "FEATURED_CARDS_EVENT":
			m_featuredCardsEvent = (string)val;
			break;
		case "CARD_TEXT_BUILDER_TYPE":
			if (val == null)
			{
				m_cardTextBuilderType = Assets.Card.CardTextBuilderType.DEFAULT;
			}
			else if (val is Assets.Card.CardTextBuilderType || val is int)
			{
				m_cardTextBuilderType = (Assets.Card.CardTextBuilderType)val;
			}
			else if (val is string)
			{
				m_cardTextBuilderType = Assets.Card.ParseCardTextBuilderTypeValue((string)val);
			}
			break;
		case "WATERMARK_TEXTURE_OVERRIDE":
			m_watermarkTextureOverride = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_MINI_GUID" => typeof(string), 
			"LONG_GUID" => typeof(string), 
			"TEXT_IN_HAND" => typeof(DbfLocValue), 
			"GAMEPLAY_EVENT" => typeof(string), 
			"CRAFTING_EVENT" => typeof(string), 
			"GOLDEN_CRAFTING_EVENT" => typeof(string), 
			"SUGGESTION_WEIGHT" => typeof(int), 
			"CHANGE_VERSION" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"FLAVOR_TEXT" => typeof(DbfLocValue), 
			"HOW_TO_GET_CARD" => typeof(DbfLocValue), 
			"HOW_TO_GET_GOLD_CARD" => typeof(DbfLocValue), 
			"HOW_TO_GET_DIAMOND_CARD" => typeof(DbfLocValue), 
			"TARGET_ARROW_TEXT" => typeof(DbfLocValue), 
			"ARTIST_NAME" => typeof(string), 
			"SHORT_NAME" => typeof(DbfLocValue), 
			"CREDITS_CARD_NAME" => typeof(string), 
			"FEATURED_CARDS_EVENT" => typeof(string), 
			"CARD_TEXT_BUILDER_TYPE" => typeof(Assets.Card.CardTextBuilderType), 
			"WATERMARK_TEXTURE_OVERRIDE" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardDbfRecords loadRecords = new LoadCardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardDbfAsset cardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardDbfAsset)) as CardDbfAsset;
		if (cardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardDbfAsset.Records.Count; i++)
		{
			cardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_textInHand.StripUnusedLocales();
		m_name.StripUnusedLocales();
		m_flavorText.StripUnusedLocales();
		m_howToGetCard.StripUnusedLocales();
		m_howToGetGoldCard.StripUnusedLocales();
		m_howToGetDiamondCard.StripUnusedLocales();
		m_targetArrowText.StripUnusedLocales();
		m_shortName.StripUnusedLocales();
	}
}
