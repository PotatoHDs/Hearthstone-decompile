using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001EA RID: 490
[Serializable]
public class GameModeDbfRecord : DbfRecord
{
	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0009064A File Offset: 0x0008E84A
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00090652 File Offset: 0x0008E852
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0009065A File Offset: 0x0008E85A
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00090662 File Offset: 0x0008E862
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0009066A File Offset: 0x0008E86A
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00090672 File Offset: 0x0008E872
	[DbfField("GAME_MODE_BUTTON_STATE")]
	public string GameModeButtonState
	{
		get
		{
			return this.m_gameModeButtonState;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0009067A File Offset: 0x0008E87A
	[DbfField("LINKED_SCENE")]
	public string LinkedScene
	{
		get
		{
			return this.m_linkedScene;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00090682 File Offset: 0x0008E882
	[DbfField("SHOW_AS_NEW_EVENT")]
	public string ShowAsNewEvent
	{
		get
		{
			return this.m_showAsNewEvent;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0009068A File Offset: 0x0008E88A
	[DbfField("SHOW_AS_EARLY_ACCESS_EVENT")]
	public string ShowAsEarlyAccessEvent
	{
		get
		{
			return this.m_showAsEarlyAccessEvent;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00090692 File Offset: 0x0008E892
	[DbfField("SHOW_AS_BETA_EVENT")]
	public string ShowAsBetaEvent
	{
		get
		{
			return this.m_showAsBetaEvent;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0009069A File Offset: 0x0008E89A
	[DbfField("FEATURE_UNLOCK_ID")]
	public int FeatureUnlockId
	{
		get
		{
			return this.m_featureUnlockId;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000906A2 File Offset: 0x0008E8A2
	[DbfField("FEATURE_UNLOCK_ID_2")]
	public int FeatureUnlockId2
	{
		get
		{
			return this.m_featureUnlockId2;
		}
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x000906AA File Offset: 0x0008E8AA
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000906B3 File Offset: 0x0008E8B3
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000906BC File Offset: 0x0008E8BC
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000906D6 File Offset: 0x0008E8D6
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000906F0 File Offset: 0x0008E8F0
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000906F9 File Offset: 0x0008E8F9
	public void SetGameModeButtonState(string v)
	{
		this.m_gameModeButtonState = v;
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x00090702 File Offset: 0x0008E902
	public void SetLinkedScene(string v)
	{
		this.m_linkedScene = v;
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x0009070B File Offset: 0x0008E90B
	public void SetShowAsNewEvent(string v)
	{
		this.m_showAsNewEvent = v;
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x00090714 File Offset: 0x0008E914
	public void SetShowAsEarlyAccessEvent(string v)
	{
		this.m_showAsEarlyAccessEvent = v;
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x0009071D File Offset: 0x0008E91D
	public void SetShowAsBetaEvent(string v)
	{
		this.m_showAsBetaEvent = v;
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x00090726 File Offset: 0x0008E926
	public void SetFeatureUnlockId(int v)
	{
		this.m_featureUnlockId = v;
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x0009072F File Offset: 0x0008E92F
	public void SetFeatureUnlockId2(int v)
	{
		this.m_featureUnlockId2 = v;
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x00090738 File Offset: 0x0008E938
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 714481862U)
			{
				if (num != 236776447U)
				{
					if (num != 237382363U)
					{
						if (num == 714481862U)
						{
							if (name == "GAME_MODE_BUTTON_STATE")
							{
								return this.m_gameModeButtonState;
							}
						}
					}
					else if (name == "SHOW_AS_EARLY_ACCESS_EVENT")
					{
						return this.m_showAsEarlyAccessEvent;
					}
				}
				else if (name == "EVENT")
				{
					return this.m_event;
				}
			}
			else if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (name == "DESCRIPTION")
			{
				return this.m_description;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 1697257895U)
			{
				if (num != 2279702235U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "SHOW_AS_NEW_EVENT")
				{
					return this.m_showAsNewEvent;
				}
			}
			else if (name == "FEATURE_UNLOCK_ID_2")
			{
				return this.m_featureUnlockId2;
			}
		}
		else if (num <= 3832007291U)
		{
			if (num != 3742737450U)
			{
				if (num == 3832007291U)
				{
					if (name == "LINKED_SCENE")
					{
						return this.m_linkedScene;
					}
				}
			}
			else if (name == "FEATURE_UNLOCK_ID")
			{
				return this.m_featureUnlockId;
			}
		}
		else if (num != 3884587951U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "SHOW_AS_BETA_EVENT")
		{
			return this.m_showAsBetaEvent;
		}
		return null;
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x0009097C File Offset: 0x0008EB7C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 714481862U)
			{
				if (num != 236776447U)
				{
					if (num != 237382363U)
					{
						if (num != 714481862U)
						{
							return;
						}
						if (!(name == "GAME_MODE_BUTTON_STATE"))
						{
							return;
						}
						this.m_gameModeButtonState = (string)val;
						return;
					}
					else
					{
						if (!(name == "SHOW_AS_EARLY_ACCESS_EVENT"))
						{
							return;
						}
						this.m_showAsEarlyAccessEvent = (string)val;
						return;
					}
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
			else if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num != 1458105184U)
					{
						return;
					}
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
				else
				{
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "DESCRIPTION"))
				{
					return;
				}
				this.m_description = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 1697257895U)
			{
				if (num != 2279702235U)
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
					if (!(name == "SHOW_AS_NEW_EVENT"))
					{
						return;
					}
					this.m_showAsNewEvent = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "FEATURE_UNLOCK_ID_2"))
				{
					return;
				}
				this.m_featureUnlockId2 = (int)val;
				return;
			}
		}
		else if (num <= 3832007291U)
		{
			if (num != 3742737450U)
			{
				if (num != 3832007291U)
				{
					return;
				}
				if (!(name == "LINKED_SCENE"))
				{
					return;
				}
				this.m_linkedScene = (string)val;
				return;
			}
			else
			{
				if (!(name == "FEATURE_UNLOCK_ID"))
				{
					return;
				}
				this.m_featureUnlockId = (int)val;
				return;
			}
		}
		else if (num != 3884587951U)
		{
			if (num != 4214602626U)
			{
				return;
			}
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
		else
		{
			if (!(name == "SHOW_AS_BETA_EVENT"))
			{
				return;
			}
			this.m_showAsBetaEvent = (string)val;
			return;
		}
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x00090BC0 File Offset: 0x0008EDC0
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 714481862U)
			{
				if (num != 236776447U)
				{
					if (num != 237382363U)
					{
						if (num == 714481862U)
						{
							if (name == "GAME_MODE_BUTTON_STATE")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "SHOW_AS_EARLY_ACCESS_EVENT")
					{
						return typeof(string);
					}
				}
				else if (name == "EVENT")
				{
					return typeof(string);
				}
			}
			else if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "DESCRIPTION")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 1697257895U)
			{
				if (num != 2279702235U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "SHOW_AS_NEW_EVENT")
				{
					return typeof(string);
				}
			}
			else if (name == "FEATURE_UNLOCK_ID_2")
			{
				return typeof(int);
			}
		}
		else if (num <= 3832007291U)
		{
			if (num != 3742737450U)
			{
				if (num == 3832007291U)
				{
					if (name == "LINKED_SCENE")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "FEATURE_UNLOCK_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3884587951U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "SHOW_AS_BETA_EVENT")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x00090E34 File Offset: 0x0008F034
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameModeDbfRecords loadRecords = new LoadGameModeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x00090E4C File Offset: 0x0008F04C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameModeDbfAsset gameModeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameModeDbfAsset)) as GameModeDbfAsset;
		if (gameModeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GameModeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < gameModeDbfAsset.Records.Count; i++)
		{
			gameModeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (gameModeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00090ECB File Offset: 0x0008F0CB
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x040010AC RID: 4268
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x040010AD RID: 4269
	[SerializeField]
	private string m_event;

	// Token: 0x040010AE RID: 4270
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x040010AF RID: 4271
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x040010B0 RID: 4272
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x040010B1 RID: 4273
	[SerializeField]
	private string m_gameModeButtonState;

	// Token: 0x040010B2 RID: 4274
	[SerializeField]
	private string m_linkedScene;

	// Token: 0x040010B3 RID: 4275
	[SerializeField]
	private string m_showAsNewEvent;

	// Token: 0x040010B4 RID: 4276
	[SerializeField]
	private string m_showAsEarlyAccessEvent;

	// Token: 0x040010B5 RID: 4277
	[SerializeField]
	private string m_showAsBetaEvent;

	// Token: 0x040010B6 RID: 4278
	[SerializeField]
	private int m_featureUnlockId;

	// Token: 0x040010B7 RID: 4279
	[SerializeField]
	private int m_featureUnlockId2;
}
