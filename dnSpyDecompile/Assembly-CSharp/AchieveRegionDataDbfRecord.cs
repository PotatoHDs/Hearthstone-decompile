using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000165 RID: 357
[Serializable]
public class AchieveRegionDataDbfRecord : DbfRecord
{
	// Token: 0x17000162 RID: 354
	// (get) Token: 0x0600166C RID: 5740 RVA: 0x0007D616 File Offset: 0x0007B816
	[DbfField("ACHIEVE_ID")]
	public int AchieveId
	{
		get
		{
			return this.m_achieveId;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x0600166D RID: 5741 RVA: 0x0007D61E File Offset: 0x0007B81E
	[DbfField("REGION")]
	public int Region
	{
		get
		{
			return this.m_region;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x0600166E RID: 5742 RVA: 0x0007D626 File Offset: 0x0007B826
	[DbfField("REWARDABLE_LIMIT")]
	public int RewardableLimit
	{
		get
		{
			return this.m_rewardableLimit;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600166F RID: 5743 RVA: 0x0007D62E File Offset: 0x0007B82E
	[DbfField("REWARDABLE_INTERVAL")]
	public double RewardableInterval
	{
		get
		{
			return this.m_rewardableInterval;
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06001670 RID: 5744 RVA: 0x0007D636 File Offset: 0x0007B836
	[DbfField("PROGRESSABLE_EVENT")]
	public string ProgressableEvent
	{
		get
		{
			return this.m_progressableEvent;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06001671 RID: 5745 RVA: 0x0007D63E File Offset: 0x0007B83E
	[DbfField("ACTIVATE_EVENT")]
	public string ActivateEvent
	{
		get
		{
			return this.m_activateEvent;
		}
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0007D646 File Offset: 0x0007B846
	public void SetAchieveId(int v)
	{
		this.m_achieveId = v;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0007D64F File Offset: 0x0007B84F
	public void SetRegion(int v)
	{
		this.m_region = v;
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0007D658 File Offset: 0x0007B858
	public void SetRewardableLimit(int v)
	{
		this.m_rewardableLimit = v;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x0007D661 File Offset: 0x0007B861
	public void SetRewardableInterval(double v)
	{
		this.m_rewardableInterval = v;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x0007D66A File Offset: 0x0007B86A
	public void SetProgressableEvent(string v)
	{
		this.m_progressableEvent = v;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0007D673 File Offset: 0x0007B873
	public void SetActivateEvent(string v)
	{
		this.m_activateEvent = v;
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x0007D67C File Offset: 0x0007B87C
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1135546125U)
		{
			if (num != 655212868U)
			{
				if (num != 1046164302U)
				{
					if (num == 1135546125U)
					{
						if (name == "PROGRESSABLE_EVENT")
						{
							return this.m_progressableEvent;
						}
					}
				}
				else if (name == "REWARDABLE_LIMIT")
				{
					return this.m_rewardableLimit;
				}
			}
			else if (name == "ACHIEVE_ID")
			{
				return this.m_achieveId;
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 1454802138U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return base.ID;
					}
				}
			}
			else if (name == "REWARDABLE_INTERVAL")
			{
				return this.m_rewardableInterval;
			}
		}
		else if (num != 2317380439U)
		{
			if (num == 3781468093U)
			{
				if (name == "REGION")
				{
					return this.m_region;
				}
			}
		}
		else if (name == "ACTIVATE_EVENT")
		{
			return this.m_activateEvent;
		}
		return null;
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1135546125U)
		{
			if (num != 655212868U)
			{
				if (num != 1046164302U)
				{
					if (num != 1135546125U)
					{
						return;
					}
					if (!(name == "PROGRESSABLE_EVENT"))
					{
						return;
					}
					this.m_progressableEvent = (string)val;
					return;
				}
				else
				{
					if (!(name == "REWARDABLE_LIMIT"))
					{
						return;
					}
					this.m_rewardableLimit = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "ACHIEVE_ID"))
				{
					return;
				}
				this.m_achieveId = (int)val;
				return;
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 1454802138U)
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
				if (!(name == "REWARDABLE_INTERVAL"))
				{
					return;
				}
				this.m_rewardableInterval = (double)val;
				return;
			}
		}
		else if (num != 2317380439U)
		{
			if (num != 3781468093U)
			{
				return;
			}
			if (!(name == "REGION"))
			{
				return;
			}
			this.m_region = (int)val;
			return;
		}
		else
		{
			if (!(name == "ACTIVATE_EVENT"))
			{
				return;
			}
			this.m_activateEvent = (string)val;
			return;
		}
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0007D8C4 File Offset: 0x0007BAC4
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1135546125U)
		{
			if (num != 655212868U)
			{
				if (num != 1046164302U)
				{
					if (num == 1135546125U)
					{
						if (name == "PROGRESSABLE_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "REWARDABLE_LIMIT")
				{
					return typeof(int);
				}
			}
			else if (name == "ACHIEVE_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 1454802138U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "REWARDABLE_INTERVAL")
			{
				return typeof(double);
			}
		}
		else if (num != 2317380439U)
		{
			if (num == 3781468093U)
			{
				if (name == "REGION")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "ACTIVATE_EVENT")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x0007D9F2 File Offset: 0x0007BBF2
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveRegionDataDbfRecords loadRecords = new LoadAchieveRegionDataDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x0007DA08 File Offset: 0x0007BC08
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveRegionDataDbfAsset achieveRegionDataDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveRegionDataDbfAsset)) as AchieveRegionDataDbfAsset;
		if (achieveRegionDataDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchieveRegionDataDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achieveRegionDataDbfAsset.Records.Count; i++)
		{
			achieveRegionDataDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achieveRegionDataDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E91 RID: 3729
	[SerializeField]
	private int m_achieveId;

	// Token: 0x04000E92 RID: 3730
	[SerializeField]
	private int m_region;

	// Token: 0x04000E93 RID: 3731
	[SerializeField]
	private int m_rewardableLimit;

	// Token: 0x04000E94 RID: 3732
	[SerializeField]
	private double m_rewardableInterval;

	// Token: 0x04000E95 RID: 3733
	[SerializeField]
	private string m_progressableEvent = "none";

	// Token: 0x04000E96 RID: 3734
	[SerializeField]
	private string m_activateEvent = "none";
}
