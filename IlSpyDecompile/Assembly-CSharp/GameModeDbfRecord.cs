using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class GameModeDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private string m_event;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private string m_gameModeButtonState;

	[SerializeField]
	private string m_linkedScene;

	[SerializeField]
	private string m_showAsNewEvent;

	[SerializeField]
	private string m_showAsEarlyAccessEvent;

	[SerializeField]
	private string m_showAsBetaEvent;

	[SerializeField]
	private int m_featureUnlockId;

	[SerializeField]
	private int m_featureUnlockId2;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("GAME_MODE_BUTTON_STATE")]
	public string GameModeButtonState => m_gameModeButtonState;

	[DbfField("LINKED_SCENE")]
	public string LinkedScene => m_linkedScene;

	[DbfField("SHOW_AS_NEW_EVENT")]
	public string ShowAsNewEvent => m_showAsNewEvent;

	[DbfField("SHOW_AS_EARLY_ACCESS_EVENT")]
	public string ShowAsEarlyAccessEvent => m_showAsEarlyAccessEvent;

	[DbfField("SHOW_AS_BETA_EVENT")]
	public string ShowAsBetaEvent => m_showAsBetaEvent;

	[DbfField("FEATURE_UNLOCK_ID")]
	public int FeatureUnlockId => m_featureUnlockId;

	[DbfField("FEATURE_UNLOCK_ID_2")]
	public int FeatureUnlockId2 => m_featureUnlockId2;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetGameModeButtonState(string v)
	{
		m_gameModeButtonState = v;
	}

	public void SetLinkedScene(string v)
	{
		m_linkedScene = v;
	}

	public void SetShowAsNewEvent(string v)
	{
		m_showAsNewEvent = v;
	}

	public void SetShowAsEarlyAccessEvent(string v)
	{
		m_showAsEarlyAccessEvent = v;
	}

	public void SetShowAsBetaEvent(string v)
	{
		m_showAsBetaEvent = v;
	}

	public void SetFeatureUnlockId(int v)
	{
		m_featureUnlockId = v;
	}

	public void SetFeatureUnlockId2(int v)
	{
		m_featureUnlockId2 = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"EVENT" => m_event, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"SORT_ORDER" => m_sortOrder, 
			"GAME_MODE_BUTTON_STATE" => m_gameModeButtonState, 
			"LINKED_SCENE" => m_linkedScene, 
			"SHOW_AS_NEW_EVENT" => m_showAsNewEvent, 
			"SHOW_AS_EARLY_ACCESS_EVENT" => m_showAsEarlyAccessEvent, 
			"SHOW_AS_BETA_EVENT" => m_showAsBetaEvent, 
			"FEATURE_UNLOCK_ID" => m_featureUnlockId, 
			"FEATURE_UNLOCK_ID_2" => m_featureUnlockId2, 
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
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "EVENT":
			m_event = (string)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "GAME_MODE_BUTTON_STATE":
			m_gameModeButtonState = (string)val;
			break;
		case "LINKED_SCENE":
			m_linkedScene = (string)val;
			break;
		case "SHOW_AS_NEW_EVENT":
			m_showAsNewEvent = (string)val;
			break;
		case "SHOW_AS_EARLY_ACCESS_EVENT":
			m_showAsEarlyAccessEvent = (string)val;
			break;
		case "SHOW_AS_BETA_EVENT":
			m_showAsBetaEvent = (string)val;
			break;
		case "FEATURE_UNLOCK_ID":
			m_featureUnlockId = (int)val;
			break;
		case "FEATURE_UNLOCK_ID_2":
			m_featureUnlockId2 = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"EVENT" => typeof(string), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"SORT_ORDER" => typeof(int), 
			"GAME_MODE_BUTTON_STATE" => typeof(string), 
			"LINKED_SCENE" => typeof(string), 
			"SHOW_AS_NEW_EVENT" => typeof(string), 
			"SHOW_AS_EARLY_ACCESS_EVENT" => typeof(string), 
			"SHOW_AS_BETA_EVENT" => typeof(string), 
			"FEATURE_UNLOCK_ID" => typeof(int), 
			"FEATURE_UNLOCK_ID_2" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameModeDbfRecords loadRecords = new LoadGameModeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameModeDbfAsset gameModeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameModeDbfAsset)) as GameModeDbfAsset;
		if (gameModeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"GameModeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < gameModeDbfAsset.Records.Count; i++)
		{
			gameModeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = gameModeDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
	}
}
