using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000238 RID: 568
[Serializable]
public class QuestDialogOnCompleteDbfRecord : DbfRecord
{
	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00099BEE File Offset: 0x00097DEE
	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00099BF6 File Offset: 0x00097DF6
	[DbfField("PLAY_ORDER")]
	public int PlayOrder
	{
		get
		{
			return this.m_playOrder;
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001E50 RID: 7760 RVA: 0x00099BFE File Offset: 0x00097DFE
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00099C06 File Offset: 0x00097E06
	[DbfField("AUDIO_NAME")]
	public string AudioName
	{
		get
		{
			return this.m_audioName;
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00099C0E File Offset: 0x00097E0E
	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition
	{
		get
		{
			return this.m_altBubblePosition;
		}
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001E53 RID: 7763 RVA: 0x00099C16 File Offset: 0x00097E16
	[DbfField("WAIT_BEFORE")]
	public double WaitBefore
	{
		get
		{
			return this.m_waitBefore;
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00099C1E File Offset: 0x00097E1E
	[DbfField("WAIT_AFTER")]
	public double WaitAfter
	{
		get
		{
			return this.m_waitAfter;
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001E55 RID: 7765 RVA: 0x00099C26 File Offset: 0x00097E26
	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab
	{
		get
		{
			return this.m_persistPrefab;
		}
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x00099C2E File Offset: 0x00097E2E
	public void SetQuestDialogId(int v)
	{
		this.m_questDialogId = v;
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x00099C37 File Offset: 0x00097E37
	public void SetPlayOrder(int v)
	{
		this.m_playOrder = v;
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x00099C40 File Offset: 0x00097E40
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x00099C49 File Offset: 0x00097E49
	public void SetAudioName(string v)
	{
		this.m_audioName = v;
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x00099C52 File Offset: 0x00097E52
	public void SetAltBubblePosition(bool v)
	{
		this.m_altBubblePosition = v;
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x00099C5B File Offset: 0x00097E5B
	public void SetWaitBefore(double v)
	{
		this.m_waitBefore = v;
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x00099C64 File Offset: 0x00097E64
	public void SetWaitAfter(double v)
	{
		this.m_waitAfter = v;
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x00099C6D File Offset: 0x00097E6D
	public void SetPersistPrefab(bool v)
	{
		this.m_persistPrefab = v;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x00099C78 File Offset: 0x00097E78
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1458105184U)
			{
				if (num != 1026926188U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "PLAY_ORDER")
				{
					return this.m_playOrder;
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
		else if (num <= 2696302966U)
		{
			if (num != 2300801615U)
			{
				if (num == 2696302966U)
				{
					if (name == "PERSIST_PREFAB")
					{
						return this.m_persistPrefab;
					}
				}
			}
			else if (name == "PREFAB_NAME")
			{
				return this.m_prefabName;
			}
		}
		else if (num != 3082479862U)
		{
			if (num != 3448897561U)
			{
				if (num == 3840082817U)
				{
					if (name == "WAIT_AFTER")
					{
						return this.m_waitAfter;
					}
				}
			}
			else if (name == "AUDIO_NAME")
			{
				return this.m_audioName;
			}
		}
		else if (name == "QUEST_DIALOG_ID")
		{
			return this.m_questDialogId;
		}
		return null;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x00099E18 File Offset: 0x00098018
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1458105184U)
			{
				if (num != 1026926188U)
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
					if (!(name == "PLAY_ORDER"))
					{
						return;
					}
					this.m_playOrder = (int)val;
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
		else if (num <= 2696302966U)
		{
			if (num != 2300801615U)
			{
				if (num != 2696302966U)
				{
					return;
				}
				if (!(name == "PERSIST_PREFAB"))
				{
					return;
				}
				this.m_persistPrefab = (bool)val;
				return;
			}
			else
			{
				if (!(name == "PREFAB_NAME"))
				{
					return;
				}
				this.m_prefabName = (string)val;
				return;
			}
		}
		else if (num != 3082479862U)
		{
			if (num != 3448897561U)
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
				if (!(name == "AUDIO_NAME"))
				{
					return;
				}
				this.m_audioName = (string)val;
				return;
			}
		}
		else
		{
			if (!(name == "QUEST_DIALOG_ID"))
			{
				return;
			}
			this.m_questDialogId = (int)val;
			return;
		}
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00099F88 File Offset: 0x00098188
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1952853119U)
		{
			if (num <= 1458105184U)
			{
				if (num != 1026926188U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "PLAY_ORDER")
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
		else if (num <= 2696302966U)
		{
			if (num != 2300801615U)
			{
				if (num == 2696302966U)
				{
					if (name == "PERSIST_PREFAB")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "PREFAB_NAME")
			{
				return typeof(string);
			}
		}
		else if (num != 3082479862U)
		{
			if (num != 3448897561U)
			{
				if (num == 3840082817U)
				{
					if (name == "WAIT_AFTER")
					{
						return typeof(double);
					}
				}
			}
			else if (name == "AUDIO_NAME")
			{
				return typeof(string);
			}
		}
		else if (name == "QUEST_DIALOG_ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x0009A128 File Offset: 0x00098328
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogOnCompleteDbfRecords loadRecords = new LoadQuestDialogOnCompleteDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x0009A140 File Offset: 0x00098340
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogOnCompleteDbfAsset questDialogOnCompleteDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogOnCompleteDbfAsset)) as QuestDialogOnCompleteDbfAsset;
		if (questDialogOnCompleteDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDialogOnCompleteDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDialogOnCompleteDbfAsset.Records.Count; i++)
		{
			questDialogOnCompleteDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDialogOnCompleteDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400118C RID: 4492
	[SerializeField]
	private int m_questDialogId;

	// Token: 0x0400118D RID: 4493
	[SerializeField]
	private int m_playOrder;

	// Token: 0x0400118E RID: 4494
	[SerializeField]
	private string m_prefabName;

	// Token: 0x0400118F RID: 4495
	[SerializeField]
	private string m_audioName;

	// Token: 0x04001190 RID: 4496
	[SerializeField]
	private bool m_altBubblePosition;

	// Token: 0x04001191 RID: 4497
	[SerializeField]
	private double m_waitBefore;

	// Token: 0x04001192 RID: 4498
	[SerializeField]
	private double m_waitAfter;

	// Token: 0x04001193 RID: 4499
	[SerializeField]
	private bool m_persistPrefab;
}
