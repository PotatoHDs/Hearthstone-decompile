using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ScheduledCharacterDialogDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_characterDialogId;

	[SerializeField]
	private string m_event;

	[SerializeField]
	private ScheduledCharacterDialog.Event m_clientEvent = ScheduledCharacterDialog.ParseEventValue("login_flow_complete");

	[SerializeField]
	private long m_clientEventData;

	[SerializeField]
	private bool m_showToReturningPlayer;

	[SerializeField]
	private bool m_showToNewPlayer;

	[SerializeField]
	private string m_enabled = "true";

	[SerializeField]
	private int m_displayOrder;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("CHARACTER_DIALOG_ID")]
	public int CharacterDialogId => m_characterDialogId;

	public CharacterDialogDbfRecord CharacterDialogRecord => GameDbf.CharacterDialog.GetRecord(m_characterDialogId);

	[DbfField("EVENT")]
	public string Event => m_event;

	[DbfField("CLIENT_EVENT")]
	public ScheduledCharacterDialog.Event ClientEvent => m_clientEvent;

	[DbfField("CLIENT_EVENT_DATA")]
	public long ClientEventData => m_clientEventData;

	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public bool ShowToReturningPlayer => m_showToReturningPlayer;

	[DbfField("SHOW_TO_NEW_PLAYER")]
	public bool ShowToNewPlayer => m_showToNewPlayer;

	[DbfField("ENABLED")]
	public string Enabled => m_enabled;

	[DbfField("DISPLAY_ORDER")]
	public int DisplayOrder => m_displayOrder;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetCharacterDialogId(int v)
	{
		m_characterDialogId = v;
	}

	public void SetEvent(string v)
	{
		m_event = v;
	}

	public void SetClientEvent(ScheduledCharacterDialog.Event v)
	{
		m_clientEvent = v;
	}

	public void SetClientEventData(long v)
	{
		m_clientEventData = v;
	}

	public void SetShowToReturningPlayer(bool v)
	{
		m_showToReturningPlayer = v;
	}

	public void SetShowToNewPlayer(bool v)
	{
		m_showToNewPlayer = v;
	}

	public void SetEnabled(string v)
	{
		m_enabled = v;
	}

	public void SetDisplayOrder(int v)
	{
		m_displayOrder = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"CHARACTER_DIALOG_ID" => m_characterDialogId, 
			"EVENT" => m_event, 
			"CLIENT_EVENT" => m_clientEvent, 
			"CLIENT_EVENT_DATA" => m_clientEventData, 
			"SHOW_TO_RETURNING_PLAYER" => m_showToReturningPlayer, 
			"SHOW_TO_NEW_PLAYER" => m_showToNewPlayer, 
			"ENABLED" => m_enabled, 
			"DISPLAY_ORDER" => m_displayOrder, 
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
		case "CHARACTER_DIALOG_ID":
			m_characterDialogId = (int)val;
			break;
		case "EVENT":
			m_event = (string)val;
			break;
		case "CLIENT_EVENT":
			if (val == null)
			{
				m_clientEvent = ScheduledCharacterDialog.Event.LOGIN_FLOW_COMPLETE;
			}
			else if (val is ScheduledCharacterDialog.Event || val is int)
			{
				m_clientEvent = (ScheduledCharacterDialog.Event)val;
			}
			else if (val is string)
			{
				m_clientEvent = ScheduledCharacterDialog.ParseEventValue((string)val);
			}
			break;
		case "CLIENT_EVENT_DATA":
			m_clientEventData = (long)val;
			break;
		case "SHOW_TO_RETURNING_PLAYER":
			m_showToReturningPlayer = (bool)val;
			break;
		case "SHOW_TO_NEW_PLAYER":
			m_showToNewPlayer = (bool)val;
			break;
		case "ENABLED":
			m_enabled = (string)val;
			break;
		case "DISPLAY_ORDER":
			m_displayOrder = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"CHARACTER_DIALOG_ID" => typeof(int), 
			"EVENT" => typeof(string), 
			"CLIENT_EVENT" => typeof(ScheduledCharacterDialog.Event), 
			"CLIENT_EVENT_DATA" => typeof(long), 
			"SHOW_TO_RETURNING_PLAYER" => typeof(bool), 
			"SHOW_TO_NEW_PLAYER" => typeof(bool), 
			"ENABLED" => typeof(string), 
			"DISPLAY_ORDER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScheduledCharacterDialogDbfRecords loadRecords = new LoadScheduledCharacterDialogDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScheduledCharacterDialogDbfAsset scheduledCharacterDialogDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScheduledCharacterDialogDbfAsset)) as ScheduledCharacterDialogDbfAsset;
		if (scheduledCharacterDialogDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ScheduledCharacterDialogDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < scheduledCharacterDialogDbfAsset.Records.Count; i++)
		{
			scheduledCharacterDialogDbfAsset.Records[i].StripUnusedLocales();
		}
		records = scheduledCharacterDialogDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
