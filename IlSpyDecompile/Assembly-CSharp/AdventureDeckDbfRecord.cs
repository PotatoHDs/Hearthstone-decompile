using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureDeckDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_classId;

	[SerializeField]
	private int m_deckId;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	[SerializeField]
	private DbfLocValue m_unlockedDescriptionText;

	[SerializeField]
	private int m_unlockGameSaveSubkeyId;

	[SerializeField]
	private int m_unlockValue;

	[SerializeField]
	private string m_displayTexture;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	[DbfField("CLASS_ID")]
	public int ClassId => m_classId;

	public ClassDbfRecord ClassRecord => GameDbf.Class.GetRecord(m_classId);

	[DbfField("DECK_ID")]
	public int DeckId => m_deckId;

	public DeckDbfRecord DeckRecord => GameDbf.Deck.GetRecord(m_deckId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText => m_unlockCriteriaText;

	[DbfField("UNLOCKED_DESCRIPTION_TEXT")]
	public DbfLocValue UnlockedDescriptionText => m_unlockedDescriptionText;

	[DbfField("UNLOCK_GAME_SAVE_SUBKEY")]
	public int UnlockGameSaveSubkey => m_unlockGameSaveSubkeyId;

	public GameSaveSubkeyDbfRecord UnlockGameSaveSubkeyRecord => GameDbf.GameSaveSubkey.GetRecord(m_unlockGameSaveSubkeyId);

	[DbfField("UNLOCK_VALUE")]
	public int UnlockValue => m_unlockValue;

	[DbfField("DISPLAY_TEXTURE")]
	public string DisplayTexture => m_displayTexture;

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetClassId(int v)
	{
		m_classId = v;
	}

	public void SetDeckId(int v)
	{
		m_deckId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	public void SetUnlockedDescriptionText(DbfLocValue v)
	{
		m_unlockedDescriptionText = v;
		v.SetDebugInfo(base.ID, "UNLOCKED_DESCRIPTION_TEXT");
	}

	public void SetUnlockGameSaveSubkey(int v)
	{
		m_unlockGameSaveSubkeyId = v;
	}

	public void SetUnlockValue(int v)
	{
		m_unlockValue = v;
	}

	public void SetDisplayTexture(string v)
	{
		m_displayTexture = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ADVENTURE_ID" => m_adventureId, 
			"CLASS_ID" => m_classId, 
			"DECK_ID" => m_deckId, 
			"SORT_ORDER" => m_sortOrder, 
			"UNLOCK_CRITERIA_TEXT" => m_unlockCriteriaText, 
			"UNLOCKED_DESCRIPTION_TEXT" => m_unlockedDescriptionText, 
			"UNLOCK_GAME_SAVE_SUBKEY" => m_unlockGameSaveSubkeyId, 
			"UNLOCK_VALUE" => m_unlockValue, 
			"DISPLAY_TEXTURE" => m_displayTexture, 
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
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "CLASS_ID":
			m_classId = (int)val;
			break;
		case "DECK_ID":
			m_deckId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "UNLOCK_CRITERIA_TEXT":
			m_unlockCriteriaText = (DbfLocValue)val;
			break;
		case "UNLOCKED_DESCRIPTION_TEXT":
			m_unlockedDescriptionText = (DbfLocValue)val;
			break;
		case "UNLOCK_GAME_SAVE_SUBKEY":
			m_unlockGameSaveSubkeyId = (int)val;
			break;
		case "UNLOCK_VALUE":
			m_unlockValue = (int)val;
			break;
		case "DISPLAY_TEXTURE":
			m_displayTexture = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ADVENTURE_ID" => typeof(int), 
			"CLASS_ID" => typeof(int), 
			"DECK_ID" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			"UNLOCK_CRITERIA_TEXT" => typeof(DbfLocValue), 
			"UNLOCKED_DESCRIPTION_TEXT" => typeof(DbfLocValue), 
			"UNLOCK_GAME_SAVE_SUBKEY" => typeof(int), 
			"UNLOCK_VALUE" => typeof(int), 
			"DISPLAY_TEXTURE" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDeckDbfRecords loadRecords = new LoadAdventureDeckDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDeckDbfAsset adventureDeckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDeckDbfAsset)) as AdventureDeckDbfAsset;
		if (adventureDeckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureDeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureDeckDbfAsset.Records.Count; i++)
		{
			adventureDeckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureDeckDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_unlockCriteriaText.StripUnusedLocales();
		m_unlockedDescriptionText.StripUnusedLocales();
	}
}
