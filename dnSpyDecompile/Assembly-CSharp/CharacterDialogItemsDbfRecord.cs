using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[Serializable]
public class CharacterDialogItemsDbfRecord : DbfRecord
{
	// Token: 0x17000281 RID: 641
	// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00089A72 File Offset: 0x00087C72
	[DbfField("CHARACTER_DIALOG_ID")]
	public int CharacterDialogId
	{
		get
		{
			return this.m_characterDialogId;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00089A7A File Offset: 0x00087C7A
	[DbfField("PLAY_ORDER")]
	public int PlayOrder
	{
		get
		{
			return this.m_playOrder;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00089A82 File Offset: 0x00087C82
	[DbfField("USE_INNKEEPER_QUOTE")]
	public bool UseInnkeeperQuote
	{
		get
		{
			return this.m_useInnkeeperQuote;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00089A8A File Offset: 0x00087C8A
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00089A92 File Offset: 0x00087C92
	[DbfField("AUDIO_NAME")]
	public string AudioName
	{
		get
		{
			return this.m_audioName;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00089A9A File Offset: 0x00087C9A
	[DbfField("BUBBLE_TEXT")]
	public DbfLocValue BubbleText
	{
		get
		{
			return this.m_bubbleText;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00089AA2 File Offset: 0x00087CA2
	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition
	{
		get
		{
			return this.m_altBubblePosition;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00089AAA File Offset: 0x00087CAA
	[DbfField("WAIT_BEFORE")]
	public double WaitBefore
	{
		get
		{
			return this.m_waitBefore;
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00089AB2 File Offset: 0x00087CB2
	[DbfField("WAIT_AFTER")]
	public double WaitAfter
	{
		get
		{
			return this.m_waitAfter;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00089ABA File Offset: 0x00087CBA
	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab
	{
		get
		{
			return this.m_persistPrefab;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060019DA RID: 6618 RVA: 0x00089AC2 File Offset: 0x00087CC2
	[DbfField("ACHIEVE_EVENT_TYPE")]
	public string AchieveEventType
	{
		get
		{
			return this.m_achieveEventType;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x060019DB RID: 6619 RVA: 0x00089ACA File Offset: 0x00087CCA
	[DbfField("ALT_POSITION")]
	public bool AltPosition
	{
		get
		{
			return this.m_altPosition;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060019DC RID: 6620 RVA: 0x00089AD2 File Offset: 0x00087CD2
	[DbfField("MINIMUM_DURATION_SECONDS")]
	public double MinimumDurationSeconds
	{
		get
		{
			return this.m_minimumDurationSeconds;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060019DD RID: 6621 RVA: 0x00089ADA File Offset: 0x00087CDA
	[DbfField("LOCALE_EXTRA_SECONDS")]
	public double LocaleExtraSeconds
	{
		get
		{
			return this.m_localeExtraSeconds;
		}
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x00089AE2 File Offset: 0x00087CE2
	public void SetCharacterDialogId(int v)
	{
		this.m_characterDialogId = v;
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x00089AEB File Offset: 0x00087CEB
	public void SetPlayOrder(int v)
	{
		this.m_playOrder = v;
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x00089AF4 File Offset: 0x00087CF4
	public void SetUseInnkeeperQuote(bool v)
	{
		this.m_useInnkeeperQuote = v;
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x00089AFD File Offset: 0x00087CFD
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x00089B06 File Offset: 0x00087D06
	public void SetAudioName(string v)
	{
		this.m_audioName = v;
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x00089B0F File Offset: 0x00087D0F
	public void SetBubbleText(DbfLocValue v)
	{
		this.m_bubbleText = v;
		v.SetDebugInfo(base.ID, "BUBBLE_TEXT");
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x00089B29 File Offset: 0x00087D29
	public void SetAltBubblePosition(bool v)
	{
		this.m_altBubblePosition = v;
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x00089B32 File Offset: 0x00087D32
	public void SetWaitBefore(double v)
	{
		this.m_waitBefore = v;
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x00089B3B File Offset: 0x00087D3B
	public void SetWaitAfter(double v)
	{
		this.m_waitAfter = v;
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x00089B44 File Offset: 0x00087D44
	public void SetPersistPrefab(bool v)
	{
		this.m_persistPrefab = v;
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x00089B4D File Offset: 0x00087D4D
	public void SetAchieveEventType(string v)
	{
		this.m_achieveEventType = v;
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x00089B56 File Offset: 0x00087D56
	public void SetAltPosition(bool v)
	{
		this.m_altPosition = v;
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x00089B5F File Offset: 0x00087D5F
	public void SetMinimumDurationSeconds(double v)
	{
		this.m_minimumDurationSeconds = v;
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x00089B68 File Offset: 0x00087D68
	public void SetLocaleExtraSeconds(double v)
	{
		this.m_localeExtraSeconds = v;
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x00089B74 File Offset: 0x00087D74
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1367724989U)
			{
				if (num != 582680871U)
				{
					if (num != 1026926188U)
					{
						if (num == 1367724989U)
						{
							if (name == "USE_INNKEEPER_QUOTE")
							{
								return this.m_useInnkeeperQuote;
							}
						}
					}
					else if (name == "PLAY_ORDER")
					{
						return this.m_playOrder;
					}
				}
				else if (name == "BUBBLE_TEXT")
				{
					return this.m_bubbleText;
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1427503831U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "CHARACTER_DIALOG_ID")
				{
					return this.m_characterDialogId;
				}
			}
			else if (num != 1788065238U)
			{
				if (num == 1952853119U)
				{
					if (name == "ALT_BUBBLE_POSITION")
					{
						return this.m_altBubblePosition;
					}
				}
			}
			else if (name == "WAIT_BEFORE")
			{
				return this.m_waitBefore;
			}
		}
		else if (num <= 3448897561U)
		{
			if (num <= 2300801615U)
			{
				if (num != 1980064440U)
				{
					if (num == 2300801615U)
					{
						if (name == "PREFAB_NAME")
						{
							return this.m_prefabName;
						}
					}
				}
				else if (name == "LOCALE_EXTRA_SECONDS")
				{
					return this.m_localeExtraSeconds;
				}
			}
			else if (num != 2696302966U)
			{
				if (num == 3448897561U)
				{
					if (name == "AUDIO_NAME")
					{
						return this.m_audioName;
					}
				}
			}
			else if (name == "PERSIST_PREFAB")
			{
				return this.m_persistPrefab;
			}
		}
		else if (num <= 3840082817U)
		{
			if (num != 3695817082U)
			{
				if (num == 3840082817U)
				{
					if (name == "WAIT_AFTER")
					{
						return this.m_waitAfter;
					}
				}
			}
			else if (name == "MINIMUM_DURATION_SECONDS")
			{
				return this.m_minimumDurationSeconds;
			}
		}
		else if (num != 3869064212U)
		{
			if (num == 3995750798U)
			{
				if (name == "ALT_POSITION")
				{
					return this.m_altPosition;
				}
			}
		}
		else if (name == "ACHIEVE_EVENT_TYPE")
		{
			return this.m_achieveEventType;
		}
		return null;
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x00089E5C File Offset: 0x0008805C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1367724989U)
			{
				if (num != 582680871U)
				{
					if (num != 1026926188U)
					{
						if (num != 1367724989U)
						{
							return;
						}
						if (!(name == "USE_INNKEEPER_QUOTE"))
						{
							return;
						}
						this.m_useInnkeeperQuote = (bool)val;
						return;
					}
					else
					{
						if (!(name == "PLAY_ORDER"))
						{
							return;
						}
						this.m_playOrder = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "BUBBLE_TEXT"))
					{
						return;
					}
					this.m_bubbleText = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1427503831U)
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
					if (!(name == "CHARACTER_DIALOG_ID"))
					{
						return;
					}
					this.m_characterDialogId = (int)val;
					return;
				}
			}
			else if (num != 1788065238U)
			{
				if (num != 1952853119U)
				{
					return;
				}
				if (!(name == "ALT_BUBBLE_POSITION"))
				{
					return;
				}
				this.m_altBubblePosition = (bool)val;
				return;
			}
			else
			{
				if (!(name == "WAIT_BEFORE"))
				{
					return;
				}
				this.m_waitBefore = (double)val;
				return;
			}
		}
		else if (num <= 3448897561U)
		{
			if (num <= 2300801615U)
			{
				if (num != 1980064440U)
				{
					if (num != 2300801615U)
					{
						return;
					}
					if (!(name == "PREFAB_NAME"))
					{
						return;
					}
					this.m_prefabName = (string)val;
					return;
				}
				else
				{
					if (!(name == "LOCALE_EXTRA_SECONDS"))
					{
						return;
					}
					this.m_localeExtraSeconds = (double)val;
					return;
				}
			}
			else if (num != 2696302966U)
			{
				if (num != 3448897561U)
				{
					return;
				}
				if (!(name == "AUDIO_NAME"))
				{
					return;
				}
				this.m_audioName = (string)val;
				return;
			}
			else
			{
				if (!(name == "PERSIST_PREFAB"))
				{
					return;
				}
				this.m_persistPrefab = (bool)val;
				return;
			}
		}
		else if (num <= 3840082817U)
		{
			if (num != 3695817082U)
			{
				if (num != 3840082817U)
				{
					return;
				}
				if (!(name == "WAIT_AFTER"))
				{
					return;
				}
				this.m_waitAfter = (double)val;
				return;
			}
			else
			{
				if (!(name == "MINIMUM_DURATION_SECONDS"))
				{
					return;
				}
				this.m_minimumDurationSeconds = (double)val;
				return;
			}
		}
		else if (num != 3869064212U)
		{
			if (num != 3995750798U)
			{
				return;
			}
			if (!(name == "ALT_POSITION"))
			{
				return;
			}
			this.m_altPosition = (bool)val;
			return;
		}
		else
		{
			if (!(name == "ACHIEVE_EVENT_TYPE"))
			{
				return;
			}
			this.m_achieveEventType = (string)val;
			return;
		}
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0008A110 File Offset: 0x00088310
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1367724989U)
			{
				if (num != 582680871U)
				{
					if (num != 1026926188U)
					{
						if (num == 1367724989U)
						{
							if (name == "USE_INNKEEPER_QUOTE")
							{
								return typeof(bool);
							}
						}
					}
					else if (name == "PLAY_ORDER")
					{
						return typeof(int);
					}
				}
				else if (name == "BUBBLE_TEXT")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1427503831U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "CHARACTER_DIALOG_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 1788065238U)
			{
				if (num == 1952853119U)
				{
					if (name == "ALT_BUBBLE_POSITION")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "WAIT_BEFORE")
			{
				return typeof(double);
			}
		}
		else if (num <= 3448897561U)
		{
			if (num <= 2300801615U)
			{
				if (num != 1980064440U)
				{
					if (num == 2300801615U)
					{
						if (name == "PREFAB_NAME")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "LOCALE_EXTRA_SECONDS")
				{
					return typeof(double);
				}
			}
			else if (num != 2696302966U)
			{
				if (num == 3448897561U)
				{
					if (name == "AUDIO_NAME")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "PERSIST_PREFAB")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3840082817U)
		{
			if (num != 3695817082U)
			{
				if (num == 3840082817U)
				{
					if (name == "WAIT_AFTER")
					{
						return typeof(double);
					}
				}
			}
			else if (name == "MINIMUM_DURATION_SECONDS")
			{
				return typeof(double);
			}
		}
		else if (num != 3869064212U)
		{
			if (num == 3995750798U)
			{
				if (name == "ALT_POSITION")
				{
					return typeof(bool);
				}
			}
		}
		else if (name == "ACHIEVE_EVENT_TYPE")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x0008A3FD File Offset: 0x000885FD
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCharacterDialogItemsDbfRecords loadRecords = new LoadCharacterDialogItemsDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x0008A414 File Offset: 0x00088614
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CharacterDialogItemsDbfAsset characterDialogItemsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CharacterDialogItemsDbfAsset)) as CharacterDialogItemsDbfAsset;
		if (characterDialogItemsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CharacterDialogItemsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < characterDialogItemsDbfAsset.Records.Count; i++)
		{
			characterDialogItemsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (characterDialogItemsDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x0008A493 File Offset: 0x00088693
	public override void StripUnusedLocales()
	{
		this.m_bubbleText.StripUnusedLocales();
	}

	// Token: 0x04000FBB RID: 4027
	[SerializeField]
	private int m_characterDialogId;

	// Token: 0x04000FBC RID: 4028
	[SerializeField]
	private int m_playOrder;

	// Token: 0x04000FBD RID: 4029
	[SerializeField]
	private bool m_useInnkeeperQuote;

	// Token: 0x04000FBE RID: 4030
	[SerializeField]
	private string m_prefabName;

	// Token: 0x04000FBF RID: 4031
	[SerializeField]
	private string m_audioName;

	// Token: 0x04000FC0 RID: 4032
	[SerializeField]
	private DbfLocValue m_bubbleText;

	// Token: 0x04000FC1 RID: 4033
	[SerializeField]
	private bool m_altBubblePosition;

	// Token: 0x04000FC2 RID: 4034
	[SerializeField]
	private double m_waitBefore;

	// Token: 0x04000FC3 RID: 4035
	[SerializeField]
	private double m_waitAfter;

	// Token: 0x04000FC4 RID: 4036
	[SerializeField]
	private bool m_persistPrefab;

	// Token: 0x04000FC5 RID: 4037
	[SerializeField]
	private string m_achieveEventType;

	// Token: 0x04000FC6 RID: 4038
	[SerializeField]
	private bool m_altPosition;

	// Token: 0x04000FC7 RID: 4039
	[SerializeField]
	private double m_minimumDurationSeconds;

	// Token: 0x04000FC8 RID: 4040
	[SerializeField]
	private double m_localeExtraSeconds;
}
