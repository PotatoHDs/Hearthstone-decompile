using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200023E RID: 574
[Serializable]
public class QuestDialogOnProgress2DbfRecord : DbfRecord
{
	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0009A8C6 File Offset: 0x00098AC6
	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0009A8CE File Offset: 0x00098ACE
	[DbfField("PLAY_ORDER")]
	public int PlayOrder
	{
		get
		{
			return this.m_playOrder;
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0009A8D6 File Offset: 0x00098AD6
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0009A8DE File Offset: 0x00098ADE
	[DbfField("AUDIO_NAME")]
	public string AudioName
	{
		get
		{
			return this.m_audioName;
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0009A8E6 File Offset: 0x00098AE6
	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition
	{
		get
		{
			return this.m_altBubblePosition;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0009A8EE File Offset: 0x00098AEE
	[DbfField("WAIT_BEFORE")]
	public double WaitBefore
	{
		get
		{
			return this.m_waitBefore;
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0009A8F6 File Offset: 0x00098AF6
	[DbfField("WAIT_AFTER")]
	public double WaitAfter
	{
		get
		{
			return this.m_waitAfter;
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0009A8FE File Offset: 0x00098AFE
	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab
	{
		get
		{
			return this.m_persistPrefab;
		}
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x0009A906 File Offset: 0x00098B06
	public void SetQuestDialogId(int v)
	{
		this.m_questDialogId = v;
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x0009A90F File Offset: 0x00098B0F
	public void SetPlayOrder(int v)
	{
		this.m_playOrder = v;
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x0009A918 File Offset: 0x00098B18
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x0009A921 File Offset: 0x00098B21
	public void SetAudioName(string v)
	{
		this.m_audioName = v;
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x0009A92A File Offset: 0x00098B2A
	public void SetAltBubblePosition(bool v)
	{
		this.m_altBubblePosition = v;
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x0009A933 File Offset: 0x00098B33
	public void SetWaitBefore(double v)
	{
		this.m_waitBefore = v;
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x0009A93C File Offset: 0x00098B3C
	public void SetWaitAfter(double v)
	{
		this.m_waitAfter = v;
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x0009A945 File Offset: 0x00098B45
	public void SetPersistPrefab(bool v)
	{
		this.m_persistPrefab = v;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x0009A950 File Offset: 0x00098B50
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

	// Token: 0x06001E97 RID: 7831 RVA: 0x0009AAF0 File Offset: 0x00098CF0
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

	// Token: 0x06001E98 RID: 7832 RVA: 0x0009AC60 File Offset: 0x00098E60
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

	// Token: 0x06001E99 RID: 7833 RVA: 0x0009AE00 File Offset: 0x00099000
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogOnProgress2DbfRecords loadRecords = new LoadQuestDialogOnProgress2DbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x0009AE18 File Offset: 0x00099018
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogOnProgress2DbfAsset questDialogOnProgress2DbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogOnProgress2DbfAsset)) as QuestDialogOnProgress2DbfAsset;
		if (questDialogOnProgress2DbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDialogOnProgress2DbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDialogOnProgress2DbfAsset.Records.Count; i++)
		{
			questDialogOnProgress2DbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDialogOnProgress2DbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011A0 RID: 4512
	[SerializeField]
	private int m_questDialogId;

	// Token: 0x040011A1 RID: 4513
	[SerializeField]
	private int m_playOrder;

	// Token: 0x040011A2 RID: 4514
	[SerializeField]
	private string m_prefabName;

	// Token: 0x040011A3 RID: 4515
	[SerializeField]
	private string m_audioName;

	// Token: 0x040011A4 RID: 4516
	[SerializeField]
	private bool m_altBubblePosition;

	// Token: 0x040011A5 RID: 4517
	[SerializeField]
	private double m_waitBefore;

	// Token: 0x040011A6 RID: 4518
	[SerializeField]
	private double m_waitAfter;

	// Token: 0x040011A7 RID: 4519
	[SerializeField]
	private bool m_persistPrefab;
}
