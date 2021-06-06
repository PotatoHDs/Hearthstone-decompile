using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000241 RID: 577
[Serializable]
public class QuestDialogOnReceivedDbfRecord : DbfRecord
{
	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0009AF32 File Offset: 0x00099132
	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0009AF3A File Offset: 0x0009913A
	[DbfField("PLAY_ORDER")]
	public int PlayOrder
	{
		get
		{
			return this.m_playOrder;
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0009AF42 File Offset: 0x00099142
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0009AF4A File Offset: 0x0009914A
	[DbfField("AUDIO_NAME")]
	public string AudioName
	{
		get
		{
			return this.m_audioName;
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0009AF52 File Offset: 0x00099152
	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition
	{
		get
		{
			return this.m_altBubblePosition;
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0009AF5A File Offset: 0x0009915A
	[DbfField("WAIT_BEFORE")]
	public double WaitBefore
	{
		get
		{
			return this.m_waitBefore;
		}
	}

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0009AF62 File Offset: 0x00099162
	[DbfField("WAIT_AFTER")]
	public double WaitAfter
	{
		get
		{
			return this.m_waitAfter;
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0009AF6A File Offset: 0x0009916A
	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab
	{
		get
		{
			return this.m_persistPrefab;
		}
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x0009AF72 File Offset: 0x00099172
	public void SetQuestDialogId(int v)
	{
		this.m_questDialogId = v;
	}

	// Token: 0x06001EAB RID: 7851 RVA: 0x0009AF7B File Offset: 0x0009917B
	public void SetPlayOrder(int v)
	{
		this.m_playOrder = v;
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x0009AF84 File Offset: 0x00099184
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x0009AF8D File Offset: 0x0009918D
	public void SetAudioName(string v)
	{
		this.m_audioName = v;
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x0009AF96 File Offset: 0x00099196
	public void SetAltBubblePosition(bool v)
	{
		this.m_altBubblePosition = v;
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x0009AF9F File Offset: 0x0009919F
	public void SetWaitBefore(double v)
	{
		this.m_waitBefore = v;
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x0009AFA8 File Offset: 0x000991A8
	public void SetWaitAfter(double v)
	{
		this.m_waitAfter = v;
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x0009AFB1 File Offset: 0x000991B1
	public void SetPersistPrefab(bool v)
	{
		this.m_persistPrefab = v;
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x0009AFBC File Offset: 0x000991BC
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

	// Token: 0x06001EB3 RID: 7859 RVA: 0x0009B15C File Offset: 0x0009935C
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

	// Token: 0x06001EB4 RID: 7860 RVA: 0x0009B2CC File Offset: 0x000994CC
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

	// Token: 0x06001EB5 RID: 7861 RVA: 0x0009B46C File Offset: 0x0009966C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogOnReceivedDbfRecords loadRecords = new LoadQuestDialogOnReceivedDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x0009B484 File Offset: 0x00099684
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogOnReceivedDbfAsset questDialogOnReceivedDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogOnReceivedDbfAsset)) as QuestDialogOnReceivedDbfAsset;
		if (questDialogOnReceivedDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDialogOnReceivedDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDialogOnReceivedDbfAsset.Records.Count; i++)
		{
			questDialogOnReceivedDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDialogOnReceivedDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011AA RID: 4522
	[SerializeField]
	private int m_questDialogId;

	// Token: 0x040011AB RID: 4523
	[SerializeField]
	private int m_playOrder;

	// Token: 0x040011AC RID: 4524
	[SerializeField]
	private string m_prefabName;

	// Token: 0x040011AD RID: 4525
	[SerializeField]
	private string m_audioName;

	// Token: 0x040011AE RID: 4526
	[SerializeField]
	private bool m_altBubblePosition;

	// Token: 0x040011AF RID: 4527
	[SerializeField]
	private double m_waitBefore;

	// Token: 0x040011B0 RID: 4528
	[SerializeField]
	private double m_waitAfter;

	// Token: 0x040011B1 RID: 4529
	[SerializeField]
	private bool m_persistPrefab;
}
