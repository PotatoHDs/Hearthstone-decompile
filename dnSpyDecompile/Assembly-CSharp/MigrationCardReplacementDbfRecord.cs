using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000214 RID: 532
[Serializable]
public class MigrationCardReplacementDbfRecord : DbfRecord
{
	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00095902 File Offset: 0x00093B02
	[DbfField("STEP_ID")]
	public int StepId
	{
		get
		{
			return this.m_stepId;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0009590A File Offset: 0x00093B0A
	[DbfField("OLD_CARD_ID")]
	public int OldCardId
	{
		get
		{
			return this.m_oldCardId;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00095912 File Offset: 0x00093B12
	public CardDbfRecord OldCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_oldCardId);
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00095924 File Offset: 0x00093B24
	[DbfField("NEW_CARD_ID")]
	public int NewCardId
	{
		get
		{
			return this.m_newCardId;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0009592C File Offset: 0x00093B2C
	public CardDbfRecord NewCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_newCardId);
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0009593E File Offset: 0x00093B3E
	[DbfField("REQUIRED_HERO_CLASS_ID")]
	public int RequiredHeroClassId
	{
		get
		{
			return this.m_requiredHeroClassId;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00095946 File Offset: 0x00093B46
	public ClassDbfRecord RequiredHeroClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_requiredHeroClassId);
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00095958 File Offset: 0x00093B58
	[DbfField("REQUIRED_HERO_LEVEL")]
	public int RequiredHeroLevel
	{
		get
		{
			return this.m_requiredHeroLevel;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00095960 File Offset: 0x00093B60
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x00095968 File Offset: 0x00093B68
	public void SetStepId(int v)
	{
		this.m_stepId = v;
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x00095971 File Offset: 0x00093B71
	public void SetOldCardId(int v)
	{
		this.m_oldCardId = v;
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0009597A File Offset: 0x00093B7A
	public void SetNewCardId(int v)
	{
		this.m_newCardId = v;
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00095983 File Offset: 0x00093B83
	public void SetRequiredHeroClassId(int v)
	{
		this.m_requiredHeroClassId = v;
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0009598C File Offset: 0x00093B8C
	public void SetRequiredHeroLevel(int v)
	{
		this.m_requiredHeroLevel = v;
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x00095995 File Offset: 0x00093B95
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x000959A0 File Offset: 0x00093BA0
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 352089880U)
			{
				if (num != 943663394U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "REQUIRED_HERO_CLASS_ID")
				{
					return this.m_requiredHeroClassId;
				}
			}
			else if (name == "NEW_CARD_ID")
			{
				return this.m_newCardId;
			}
		}
		else if (num <= 3050375899U)
		{
			if (num != 1541954861U)
			{
				if (num == 3050375899U)
				{
					if (name == "OLD_CARD_ID")
					{
						return this.m_oldCardId;
					}
				}
			}
			else if (name == "STEP_ID")
			{
				return this.m_stepId;
			}
		}
		else if (num != 3514104216U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "REQUIRED_HERO_LEVEL")
		{
			return this.m_requiredHeroLevel;
		}
		return null;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x00095ADC File Offset: 0x00093CDC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 352089880U)
			{
				if (num != 943663394U)
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
					if (!(name == "REQUIRED_HERO_CLASS_ID"))
					{
						return;
					}
					this.m_requiredHeroClassId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "NEW_CARD_ID"))
				{
					return;
				}
				this.m_newCardId = (int)val;
				return;
			}
		}
		else if (num <= 3050375899U)
		{
			if (num != 1541954861U)
			{
				if (num != 3050375899U)
				{
					return;
				}
				if (!(name == "OLD_CARD_ID"))
				{
					return;
				}
				this.m_oldCardId = (int)val;
				return;
			}
			else
			{
				if (!(name == "STEP_ID"))
				{
					return;
				}
				this.m_stepId = (int)val;
				return;
			}
		}
		else if (num != 3514104216U)
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
			if (!(name == "REQUIRED_HERO_LEVEL"))
			{
				return;
			}
			this.m_requiredHeroLevel = (int)val;
			return;
		}
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x00095BF8 File Offset: 0x00093DF8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 352089880U)
			{
				if (num != 943663394U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "REQUIRED_HERO_CLASS_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "NEW_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3050375899U)
		{
			if (num != 1541954861U)
			{
				if (num == 3050375899U)
				{
					if (name == "OLD_CARD_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "STEP_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3514104216U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "REQUIRED_HERO_LEVEL")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x00095D29 File Offset: 0x00093F29
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMigrationCardReplacementDbfRecords loadRecords = new LoadMigrationCardReplacementDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x00095D40 File Offset: 0x00093F40
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MigrationCardReplacementDbfAsset migrationCardReplacementDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MigrationCardReplacementDbfAsset)) as MigrationCardReplacementDbfAsset;
		if (migrationCardReplacementDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("MigrationCardReplacementDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < migrationCardReplacementDbfAsset.Records.Count; i++)
		{
			migrationCardReplacementDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (migrationCardReplacementDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001127 RID: 4391
	[SerializeField]
	private int m_stepId;

	// Token: 0x04001128 RID: 4392
	[SerializeField]
	private int m_oldCardId;

	// Token: 0x04001129 RID: 4393
	[SerializeField]
	private int m_newCardId;

	// Token: 0x0400112A RID: 4394
	[SerializeField]
	private int m_requiredHeroClassId;

	// Token: 0x0400112B RID: 4395
	[SerializeField]
	private int m_requiredHeroLevel;

	// Token: 0x0400112C RID: 4396
	[SerializeField]
	private int m_sortOrder;
}
