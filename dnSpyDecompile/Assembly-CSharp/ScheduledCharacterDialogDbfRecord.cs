using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200026B RID: 619
[Serializable]
public class ScheduledCharacterDialogDbfRecord : DbfRecord
{
	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x06002038 RID: 8248 RVA: 0x000A040A File Offset: 0x0009E60A
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x06002039 RID: 8249 RVA: 0x000A0412 File Offset: 0x0009E612
	[DbfField("CHARACTER_DIALOG_ID")]
	public int CharacterDialogId
	{
		get
		{
			return this.m_characterDialogId;
		}
	}

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x0600203A RID: 8250 RVA: 0x000A041A File Offset: 0x0009E61A
	public CharacterDialogDbfRecord CharacterDialogRecord
	{
		get
		{
			return GameDbf.CharacterDialog.GetRecord(this.m_characterDialogId);
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x0600203B RID: 8251 RVA: 0x000A042C File Offset: 0x0009E62C
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x0600203C RID: 8252 RVA: 0x000A0434 File Offset: 0x0009E634
	[DbfField("CLIENT_EVENT")]
	public ScheduledCharacterDialog.Event ClientEvent
	{
		get
		{
			return this.m_clientEvent;
		}
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x0600203D RID: 8253 RVA: 0x000A043C File Offset: 0x0009E63C
	[DbfField("CLIENT_EVENT_DATA")]
	public long ClientEventData
	{
		get
		{
			return this.m_clientEventData;
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x0600203E RID: 8254 RVA: 0x000A0444 File Offset: 0x0009E644
	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public bool ShowToReturningPlayer
	{
		get
		{
			return this.m_showToReturningPlayer;
		}
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x0600203F RID: 8255 RVA: 0x000A044C File Offset: 0x0009E64C
	[DbfField("SHOW_TO_NEW_PLAYER")]
	public bool ShowToNewPlayer
	{
		get
		{
			return this.m_showToNewPlayer;
		}
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06002040 RID: 8256 RVA: 0x000A0454 File Offset: 0x0009E654
	[DbfField("ENABLED")]
	public string Enabled
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06002041 RID: 8257 RVA: 0x000A045C File Offset: 0x0009E65C
	[DbfField("DISPLAY_ORDER")]
	public int DisplayOrder
	{
		get
		{
			return this.m_displayOrder;
		}
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x000A0464 File Offset: 0x0009E664
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x000A046D File Offset: 0x0009E66D
	public void SetCharacterDialogId(int v)
	{
		this.m_characterDialogId = v;
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x000A0476 File Offset: 0x0009E676
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x000A047F File Offset: 0x0009E67F
	public void SetClientEvent(ScheduledCharacterDialog.Event v)
	{
		this.m_clientEvent = v;
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x000A0488 File Offset: 0x0009E688
	public void SetClientEventData(long v)
	{
		this.m_clientEventData = v;
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x000A0491 File Offset: 0x0009E691
	public void SetShowToReturningPlayer(bool v)
	{
		this.m_showToReturningPlayer = v;
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x000A049A File Offset: 0x0009E69A
	public void SetShowToNewPlayer(bool v)
	{
		this.m_showToNewPlayer = v;
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x000A04A3 File Offset: 0x0009E6A3
	public void SetEnabled(string v)
	{
		this.m_enabled = v;
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x000A04AC File Offset: 0x0009E6AC
	public void SetDisplayOrder(int v)
	{
		this.m_displayOrder = v;
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000A04B8 File Offset: 0x0009E6B8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1832324731U)
		{
			if (num <= 1427503831U)
			{
				if (num != 236776447U)
				{
					if (num == 1427503831U)
					{
						if (name == "CHARACTER_DIALOG_ID")
						{
							return this.m_characterDialogId;
						}
					}
				}
				else if (name == "EVENT")
				{
					return this.m_event;
				}
			}
			else if (num != 1441469002U)
			{
				if (num != 1458105184U)
				{
					if (num == 1832324731U)
					{
						if (name == "SHOW_TO_RETURNING_PLAYER")
						{
							return this.m_showToReturningPlayer;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (name == "CLIENT_EVENT_DATA")
			{
				return this.m_clientEventData;
			}
		}
		else if (num <= 2294480894U)
		{
			if (num != 2100707457U)
			{
				if (num == 2294480894U)
				{
					if (name == "ENABLED")
					{
						return this.m_enabled;
					}
				}
			}
			else if (name == "CLIENT_EVENT")
			{
				return this.m_clientEvent;
			}
		}
		else if (num != 2320110678U)
		{
			if (num != 2435862099U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return this.m_noteDesc;
					}
				}
			}
			else if (name == "SHOW_TO_NEW_PLAYER")
			{
				return this.m_showToNewPlayer;
			}
		}
		else if (name == "DISPLAY_ORDER")
		{
			return this.m_displayOrder;
		}
		return null;
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x000A0684 File Offset: 0x0009E884
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 1832324731U)
		{
			if (num <= 2294480894U)
			{
				if (num != 2100707457U)
				{
					if (num != 2294480894U)
					{
						return;
					}
					if (!(name == "ENABLED"))
					{
						return;
					}
					this.m_enabled = (string)val;
					return;
				}
				else
				{
					if (!(name == "CLIENT_EVENT"))
					{
						return;
					}
					if (val == null)
					{
						this.m_clientEvent = ScheduledCharacterDialog.Event.LOGIN_FLOW_COMPLETE;
						return;
					}
					if (val is ScheduledCharacterDialog.Event || val is int)
					{
						this.m_clientEvent = (ScheduledCharacterDialog.Event)val;
						return;
					}
					if (val is string)
					{
						this.m_clientEvent = ScheduledCharacterDialog.ParseEventValue((string)val);
						return;
					}
				}
			}
			else if (num != 2320110678U)
			{
				if (num != 2435862099U)
				{
					if (num != 3022554311U)
					{
						return;
					}
					if (!(name == "NOTE_DESC"))
					{
						return;
					}
					this.m_noteDesc = (string)val;
					return;
				}
				else
				{
					if (!(name == "SHOW_TO_NEW_PLAYER"))
					{
						return;
					}
					this.m_showToNewPlayer = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "DISPLAY_ORDER"))
				{
					return;
				}
				this.m_displayOrder = (int)val;
			}
			return;
		}
		if (num <= 1427503831U)
		{
			if (num != 236776447U)
			{
				if (num != 1427503831U)
				{
					return;
				}
				if (!(name == "CHARACTER_DIALOG_ID"))
				{
					return;
				}
				this.m_characterDialogId = (int)val;
				return;
			}
			else
			{
				if (!(name == "EVENT"))
				{
					return;
				}
				this.m_event = (string)val;
				return;
			}
		}
		else if (num != 1441469002U)
		{
			if (num != 1458105184U)
			{
				if (num != 1832324731U)
				{
					return;
				}
				if (!(name == "SHOW_TO_RETURNING_PLAYER"))
				{
					return;
				}
				this.m_showToReturningPlayer = (bool)val;
				return;
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else
		{
			if (!(name == "CLIENT_EVENT_DATA"))
			{
				return;
			}
			this.m_clientEventData = (long)val;
			return;
		}
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x000A0878 File Offset: 0x0009EA78
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1832324731U)
		{
			if (num <= 1427503831U)
			{
				if (num != 236776447U)
				{
					if (num == 1427503831U)
					{
						if (name == "CHARACTER_DIALOG_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "EVENT")
				{
					return typeof(string);
				}
			}
			else if (num != 1441469002U)
			{
				if (num != 1458105184U)
				{
					if (num == 1832324731U)
					{
						if (name == "SHOW_TO_RETURNING_PLAYER")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (name == "CLIENT_EVENT_DATA")
			{
				return typeof(long);
			}
		}
		else if (num <= 2294480894U)
		{
			if (num != 2100707457U)
			{
				if (num == 2294480894U)
				{
					if (name == "ENABLED")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "CLIENT_EVENT")
			{
				return typeof(ScheduledCharacterDialog.Event);
			}
		}
		else if (num != 2320110678U)
		{
			if (num != 2435862099U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "SHOW_TO_NEW_PLAYER")
			{
				return typeof(bool);
			}
		}
		else if (name == "DISPLAY_ORDER")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x000A0A4C File Offset: 0x0009EC4C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScheduledCharacterDialogDbfRecords loadRecords = new LoadScheduledCharacterDialogDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x000A0A64 File Offset: 0x0009EC64
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScheduledCharacterDialogDbfAsset scheduledCharacterDialogDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScheduledCharacterDialogDbfAsset)) as ScheduledCharacterDialogDbfAsset;
		if (scheduledCharacterDialogDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ScheduledCharacterDialogDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < scheduledCharacterDialogDbfAsset.Records.Count; i++)
		{
			scheduledCharacterDialogDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (scheduledCharacterDialogDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400122D RID: 4653
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400122E RID: 4654
	[SerializeField]
	private int m_characterDialogId;

	// Token: 0x0400122F RID: 4655
	[SerializeField]
	private string m_event;

	// Token: 0x04001230 RID: 4656
	[SerializeField]
	private ScheduledCharacterDialog.Event m_clientEvent = ScheduledCharacterDialog.ParseEventValue("login_flow_complete");

	// Token: 0x04001231 RID: 4657
	[SerializeField]
	private long m_clientEventData;

	// Token: 0x04001232 RID: 4658
	[SerializeField]
	private bool m_showToReturningPlayer;

	// Token: 0x04001233 RID: 4659
	[SerializeField]
	private bool m_showToNewPlayer;

	// Token: 0x04001234 RID: 4660
	[SerializeField]
	private string m_enabled = "true";

	// Token: 0x04001235 RID: 4661
	[SerializeField]
	private int m_displayOrder;
}
