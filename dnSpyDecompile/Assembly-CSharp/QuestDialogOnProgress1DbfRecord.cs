using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200023B RID: 571
[Serializable]
public class QuestDialogOnProgress1DbfRecord : DbfRecord
{
	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001E6A RID: 7786 RVA: 0x0009A25A File Offset: 0x0009845A
	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0009A262 File Offset: 0x00098462
	[DbfField("PLAY_ORDER")]
	public int PlayOrder
	{
		get
		{
			return this.m_playOrder;
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0009A26A File Offset: 0x0009846A
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0009A272 File Offset: 0x00098472
	[DbfField("AUDIO_NAME")]
	public string AudioName
	{
		get
		{
			return this.m_audioName;
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0009A27A File Offset: 0x0009847A
	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition
	{
		get
		{
			return this.m_altBubblePosition;
		}
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0009A282 File Offset: 0x00098482
	[DbfField("WAIT_BEFORE")]
	public double WaitBefore
	{
		get
		{
			return this.m_waitBefore;
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0009A28A File Offset: 0x0009848A
	[DbfField("WAIT_AFTER")]
	public double WaitAfter
	{
		get
		{
			return this.m_waitAfter;
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0009A292 File Offset: 0x00098492
	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab
	{
		get
		{
			return this.m_persistPrefab;
		}
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x0009A29A File Offset: 0x0009849A
	public void SetQuestDialogId(int v)
	{
		this.m_questDialogId = v;
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0009A2A3 File Offset: 0x000984A3
	public void SetPlayOrder(int v)
	{
		this.m_playOrder = v;
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x0009A2AC File Offset: 0x000984AC
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0009A2B5 File Offset: 0x000984B5
	public void SetAudioName(string v)
	{
		this.m_audioName = v;
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0009A2BE File Offset: 0x000984BE
	public void SetAltBubblePosition(bool v)
	{
		this.m_altBubblePosition = v;
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x0009A2C7 File Offset: 0x000984C7
	public void SetWaitBefore(double v)
	{
		this.m_waitBefore = v;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x0009A2D0 File Offset: 0x000984D0
	public void SetWaitAfter(double v)
	{
		this.m_waitAfter = v;
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x0009A2D9 File Offset: 0x000984D9
	public void SetPersistPrefab(bool v)
	{
		this.m_persistPrefab = v;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x0009A2E4 File Offset: 0x000984E4
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

	// Token: 0x06001E7B RID: 7803 RVA: 0x0009A484 File Offset: 0x00098684
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

	// Token: 0x06001E7C RID: 7804 RVA: 0x0009A5F4 File Offset: 0x000987F4
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

	// Token: 0x06001E7D RID: 7805 RVA: 0x0009A794 File Offset: 0x00098994
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogOnProgress1DbfRecords loadRecords = new LoadQuestDialogOnProgress1DbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x0009A7AC File Offset: 0x000989AC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogOnProgress1DbfAsset questDialogOnProgress1DbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogOnProgress1DbfAsset)) as QuestDialogOnProgress1DbfAsset;
		if (questDialogOnProgress1DbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDialogOnProgress1DbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDialogOnProgress1DbfAsset.Records.Count; i++)
		{
			questDialogOnProgress1DbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDialogOnProgress1DbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001196 RID: 4502
	[SerializeField]
	private int m_questDialogId;

	// Token: 0x04001197 RID: 4503
	[SerializeField]
	private int m_playOrder;

	// Token: 0x04001198 RID: 4504
	[SerializeField]
	private string m_prefabName;

	// Token: 0x04001199 RID: 4505
	[SerializeField]
	private string m_audioName;

	// Token: 0x0400119A RID: 4506
	[SerializeField]
	private bool m_altBubblePosition;

	// Token: 0x0400119B RID: 4507
	[SerializeField]
	private double m_waitBefore;

	// Token: 0x0400119C RID: 4508
	[SerializeField]
	private double m_waitAfter;

	// Token: 0x0400119D RID: 4509
	[SerializeField]
	private bool m_persistPrefab;
}
