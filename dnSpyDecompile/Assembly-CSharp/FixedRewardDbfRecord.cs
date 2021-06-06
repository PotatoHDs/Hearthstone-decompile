using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E3 RID: 483
[Serializable]
public class FixedRewardDbfRecord : DbfRecord
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0008E472 File Offset: 0x0008C672
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0008E47A File Offset: 0x0008C67A
	[DbfField("TYPE")]
	public FixedReward.Type Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0008E482 File Offset: 0x0008C682
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0008E48A File Offset: 0x0008C68A
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0008E49C File Offset: 0x0008C69C
	[DbfField("CARD_PREMIUM")]
	public int CardPremium
	{
		get
		{
			return this.m_cardPremium;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0008E4A4 File Offset: 0x0008C6A4
	[DbfField("CARD_BACK_ID")]
	public int CardBackId
	{
		get
		{
			return this.m_cardBackId;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0008E4AC File Offset: 0x0008C6AC
	public CardBackDbfRecord CardBackRecord
	{
		get
		{
			return GameDbf.CardBack.GetRecord(this.m_cardBackId);
		}
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0008E4BE File Offset: 0x0008C6BE
	[DbfField("META_ACTION_ID")]
	public int MetaActionId
	{
		get
		{
			return this.m_metaActionId;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0008E4C6 File Offset: 0x0008C6C6
	public FixedRewardActionDbfRecord MetaActionRecord
	{
		get
		{
			return GameDbf.FixedRewardAction.GetRecord(this.m_metaActionId);
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0008E4D8 File Offset: 0x0008C6D8
	[DbfField("META_ACTION_FLAGS")]
	public ulong MetaActionFlags
	{
		get
		{
			return this.m_metaActionFlags;
		}
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x0008E4E0 File Offset: 0x0008C6E0
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x0008E4E9 File Offset: 0x0008C6E9
	public void SetType(FixedReward.Type v)
	{
		this.m_type = v;
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x0008E4F2 File Offset: 0x0008C6F2
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x0008E4FB File Offset: 0x0008C6FB
	public void SetCardPremium(int v)
	{
		this.m_cardPremium = v;
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x0008E504 File Offset: 0x0008C704
	public void SetCardBackId(int v)
	{
		this.m_cardBackId = v;
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x0008E50D File Offset: 0x0008C70D
	public void SetMetaActionId(int v)
	{
		this.m_metaActionId = v;
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x0008E516 File Offset: 0x0008C716
	public void SetMetaActionFlags(ulong v)
	{
		this.m_metaActionFlags = v;
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x0008E520 File Offset: 0x0008C720
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 451390141U)
			{
				if (num != 338683789U)
				{
					if (num == 451390141U)
					{
						if (name == "CARD_ID")
						{
							return this.m_cardId;
						}
					}
				}
				else if (name == "TYPE")
				{
					return this.m_type;
				}
			}
			else if (num != 1386967833U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return base.ID;
					}
				}
			}
			else if (name == "CARD_PREMIUM")
			{
				return this.m_cardPremium;
			}
		}
		else if (num <= 2832947347U)
		{
			if (num != 1560548161U)
			{
				if (num == 2832947347U)
				{
					if (name == "META_ACTION_FLAGS")
					{
						return this.m_metaActionFlags;
					}
				}
			}
			else if (name == "CARD_BACK_ID")
			{
				return this.m_cardBackId;
			}
		}
		else if (num != 3022554311U)
		{
			if (num == 3462221213U)
			{
				if (name == "META_ACTION_ID")
				{
					return this.m_metaActionId;
				}
			}
		}
		else if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		return null;
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x0008E690 File Offset: 0x0008C890
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 451390141U)
			{
				if (num != 338683789U)
				{
					if (num != 451390141U)
					{
						return;
					}
					if (!(name == "CARD_ID"))
					{
						return;
					}
					this.m_cardId = (int)val;
					return;
				}
				else
				{
					if (!(name == "TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_type = FixedReward.Type.UNKNOWN;
						return;
					}
					if (val is FixedReward.Type || val is int)
					{
						this.m_type = (FixedReward.Type)val;
						return;
					}
					if (val is string)
					{
						this.m_type = FixedReward.ParseTypeValue((string)val);
						return;
					}
				}
			}
			else if (num != 1386967833U)
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
				if (!(name == "CARD_PREMIUM"))
				{
					return;
				}
				this.m_cardPremium = (int)val;
				return;
			}
		}
		else if (num <= 2832947347U)
		{
			if (num != 1560548161U)
			{
				if (num != 2832947347U)
				{
					return;
				}
				if (!(name == "META_ACTION_FLAGS"))
				{
					return;
				}
				this.m_metaActionFlags = (ulong)val;
			}
			else
			{
				if (!(name == "CARD_BACK_ID"))
				{
					return;
				}
				this.m_cardBackId = (int)val;
				return;
			}
		}
		else if (num != 3022554311U)
		{
			if (num != 3462221213U)
			{
				return;
			}
			if (!(name == "META_ACTION_ID"))
			{
				return;
			}
			this.m_metaActionId = (int)val;
			return;
		}
		else
		{
			if (!(name == "NOTE_DESC"))
			{
				return;
			}
			this.m_noteDesc = (string)val;
			return;
		}
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x0008E820 File Offset: 0x0008CA20
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 451390141U)
			{
				if (num != 338683789U)
				{
					if (num == 451390141U)
					{
						if (name == "CARD_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "TYPE")
				{
					return typeof(FixedReward.Type);
				}
			}
			else if (num != 1386967833U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "CARD_PREMIUM")
			{
				return typeof(int);
			}
		}
		else if (num <= 2832947347U)
		{
			if (num != 1560548161U)
			{
				if (num == 2832947347U)
				{
					if (name == "META_ACTION_FLAGS")
					{
						return typeof(ulong);
					}
				}
			}
			else if (name == "CARD_BACK_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3022554311U)
		{
			if (num == 3462221213U)
			{
				if (name == "META_ACTION_ID")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x0008E98C File Offset: 0x0008CB8C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadFixedRewardDbfRecords loadRecords = new LoadFixedRewardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x0008E9A4 File Offset: 0x0008CBA4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		FixedRewardDbfAsset fixedRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(FixedRewardDbfAsset)) as FixedRewardDbfAsset;
		if (fixedRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("FixedRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < fixedRewardDbfAsset.Records.Count; i++)
		{
			fixedRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (fixedRewardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400102D RID: 4141
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400102E RID: 4142
	[SerializeField]
	private FixedReward.Type m_type = FixedReward.ParseTypeValue("unknown");

	// Token: 0x0400102F RID: 4143
	[SerializeField]
	private int m_cardId;

	// Token: 0x04001030 RID: 4144
	[SerializeField]
	private int m_cardPremium;

	// Token: 0x04001031 RID: 4145
	[SerializeField]
	private int m_cardBackId;

	// Token: 0x04001032 RID: 4146
	[SerializeField]
	private int m_metaActionId;

	// Token: 0x04001033 RID: 4147
	[SerializeField]
	private ulong m_metaActionFlags;
}
