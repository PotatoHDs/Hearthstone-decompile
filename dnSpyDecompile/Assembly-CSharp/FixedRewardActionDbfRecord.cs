using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E0 RID: 480
[Serializable]
public class FixedRewardActionDbfRecord : DbfRecord
{
	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0008D86E File Offset: 0x0008BA6E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0008D876 File Offset: 0x0008BA76
	[DbfField("TYPE")]
	public FixedRewardAction.Type Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0008D87E File Offset: 0x0008BA7E
	[DbfField("WING_ID")]
	public int WingId
	{
		get
		{
			return this.m_wingId;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0008D886 File Offset: 0x0008BA86
	public WingDbfRecord WingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_wingId);
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0008D898 File Offset: 0x0008BA98
	[DbfField("WING_PROGRESS")]
	public int WingProgress
	{
		get
		{
			return this.m_wingProgress;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0008D8A0 File Offset: 0x0008BAA0
	[DbfField("WING_FLAGS")]
	public ulong WingFlags
	{
		get
		{
			return this.m_wingFlags;
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0008D8A8 File Offset: 0x0008BAA8
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0008D8B0 File Offset: 0x0008BAB0
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0008D8C2 File Offset: 0x0008BAC2
	[DbfField("TOTAL_HERO_LEVEL")]
	public int TotalHeroLevel
	{
		get
		{
			return this.m_totalHeroLevel;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0008D8CA File Offset: 0x0008BACA
	[DbfField("HERO_LEVEL")]
	public int HeroLevel
	{
		get
		{
			return this.m_heroLevel;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0008D8D2 File Offset: 0x0008BAD2
	[DbfField("TUTORIAL_PROGRESS")]
	public int TutorialProgress
	{
		get
		{
			return this.m_tutorialProgress;
		}
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0008D8DA File Offset: 0x0008BADA
	[DbfField("META_ACTION_FLAGS")]
	public ulong MetaActionFlags
	{
		get
		{
			return this.m_metaActionFlags;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0008D8E2 File Offset: 0x0008BAE2
	[DbfField("ACHIEVE_ID")]
	public int AchieveId
	{
		get
		{
			return this.m_achieveId;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0008D8EA File Offset: 0x0008BAEA
	public AchieveDbfRecord AchieveRecord
	{
		get
		{
			return GameDbf.Achieve.GetRecord(this.m_achieveId);
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0008D8FC File Offset: 0x0008BAFC
	[DbfField("ACCOUNT_LICENSE_ID")]
	public long AccountLicenseId
	{
		get
		{
			return this.m_accountLicenseId;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0008D904 File Offset: 0x0008BB04
	[DbfField("ACCOUNT_LICENSE_FLAGS")]
	public ulong AccountLicenseFlags
	{
		get
		{
			return this.m_accountLicenseFlags;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0008D90C File Offset: 0x0008BB0C
	[DbfField("ACTIVE_EVENT")]
	public string ActiveEvent
	{
		get
		{
			return this.m_activeEvent;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0008D914 File Offset: 0x0008BB14
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0008D91C File Offset: 0x0008BB1C
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x0008D92E File Offset: 0x0008BB2E
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x0008D937 File Offset: 0x0008BB37
	public void SetType(FixedRewardAction.Type v)
	{
		this.m_type = v;
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x0008D940 File Offset: 0x0008BB40
	public void SetWingId(int v)
	{
		this.m_wingId = v;
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x0008D949 File Offset: 0x0008BB49
	public void SetWingProgress(int v)
	{
		this.m_wingProgress = v;
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x0008D952 File Offset: 0x0008BB52
	public void SetWingFlags(ulong v)
	{
		this.m_wingFlags = v;
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x0008D95B File Offset: 0x0008BB5B
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x0008D964 File Offset: 0x0008BB64
	public void SetTotalHeroLevel(int v)
	{
		this.m_totalHeroLevel = v;
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x0008D96D File Offset: 0x0008BB6D
	public void SetHeroLevel(int v)
	{
		this.m_heroLevel = v;
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x0008D976 File Offset: 0x0008BB76
	public void SetTutorialProgress(int v)
	{
		this.m_tutorialProgress = v;
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0008D97F File Offset: 0x0008BB7F
	public void SetMetaActionFlags(ulong v)
	{
		this.m_metaActionFlags = v;
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x0008D988 File Offset: 0x0008BB88
	public void SetAchieveId(int v)
	{
		this.m_achieveId = v;
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x0008D991 File Offset: 0x0008BB91
	public void SetAccountLicenseId(long v)
	{
		this.m_accountLicenseId = v;
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x0008D99A File Offset: 0x0008BB9A
	public void SetAccountLicenseFlags(ulong v)
	{
		this.m_accountLicenseFlags = v;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0008D9A3 File Offset: 0x0008BBA3
	public void SetActiveEvent(string v)
	{
		this.m_activeEvent = v;
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x0008D9AC File Offset: 0x0008BBAC
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x0008D9B8 File Offset: 0x0008BBB8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2658990091U)
		{
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
				else if (num != 655212868U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "ACHIEVE_ID")
				{
					return this.m_achieveId;
				}
			}
			else if (num <= 2102410176U)
			{
				if (num != 1559555090U)
				{
					if (num == 2102410176U)
					{
						if (name == "HERO_LEVEL")
						{
							return this.m_heroLevel;
						}
					}
				}
				else if (name == "WING_ID")
				{
					return this.m_wingId;
				}
			}
			else if (num != 2335490227U)
			{
				if (num == 2658990091U)
				{
					if (name == "TOTAL_HERO_LEVEL")
					{
						return this.m_totalHeroLevel;
					}
				}
			}
			else if (name == "TUTORIAL_PROGRESS")
			{
				return this.m_tutorialProgress;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2832947347U)
			{
				if (num != 2695008864U)
				{
					if (num == 2832947347U)
					{
						if (name == "META_ACTION_FLAGS")
						{
							return this.m_metaActionFlags;
						}
					}
				}
				else if (name == "WING_PROGRESS")
				{
					return this.m_wingProgress;
				}
			}
			else if (num != 2947115096U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return this.m_noteDesc;
					}
				}
			}
			else if (name == "ACTIVE_EVENT")
			{
				return this.m_activeEvent;
			}
		}
		else if (num <= 3553888982U)
		{
			if (num != 3365816664U)
			{
				if (num == 3553888982U)
				{
					if (name == "WING_FLAGS")
					{
						return this.m_wingFlags;
					}
				}
			}
			else if (name == "ACCOUNT_LICENSE_ID")
			{
				return this.m_accountLicenseId;
			}
		}
		else if (num != 3756307540U)
		{
			if (num == 4257872637U)
			{
				if (name == "CLASS_ID")
				{
					return this.m_classId;
				}
			}
		}
		else if (name == "ACCOUNT_LICENSE_FLAGS")
		{
			return this.m_accountLicenseFlags;
		}
		return null;
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x0008DCE4 File Offset: 0x0008BEE4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2658990091U)
		{
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
					}
					else
					{
						if (!(name == "TYPE"))
						{
							return;
						}
						if (val == null)
						{
							this.m_type = FixedRewardAction.Type.TUTORIAL_PROGRESS;
							return;
						}
						if (val is FixedRewardAction.Type || val is int)
						{
							this.m_type = (FixedRewardAction.Type)val;
							return;
						}
						if (val is string)
						{
							this.m_type = FixedRewardAction.ParseTypeValue((string)val);
							return;
						}
					}
					return;
				}
				if (num != 655212868U)
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
					if (!(name == "ACHIEVE_ID"))
					{
						return;
					}
					this.m_achieveId = (int)val;
					return;
				}
			}
			else if (num <= 2102410176U)
			{
				if (num != 1559555090U)
				{
					if (num != 2102410176U)
					{
						return;
					}
					if (!(name == "HERO_LEVEL"))
					{
						return;
					}
					this.m_heroLevel = (int)val;
					return;
				}
				else
				{
					if (!(name == "WING_ID"))
					{
						return;
					}
					this.m_wingId = (int)val;
					return;
				}
			}
			else if (num != 2335490227U)
			{
				if (num != 2658990091U)
				{
					return;
				}
				if (!(name == "TOTAL_HERO_LEVEL"))
				{
					return;
				}
				this.m_totalHeroLevel = (int)val;
				return;
			}
			else
			{
				if (!(name == "TUTORIAL_PROGRESS"))
				{
					return;
				}
				this.m_tutorialProgress = (int)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2832947347U)
			{
				if (num != 2695008864U)
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
					return;
				}
				else
				{
					if (!(name == "WING_PROGRESS"))
					{
						return;
					}
					this.m_wingProgress = (int)val;
					return;
				}
			}
			else if (num != 2947115096U)
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
				if (!(name == "ACTIVE_EVENT"))
				{
					return;
				}
				this.m_activeEvent = (string)val;
				return;
			}
		}
		else if (num <= 3553888982U)
		{
			if (num != 3365816664U)
			{
				if (num != 3553888982U)
				{
					return;
				}
				if (!(name == "WING_FLAGS"))
				{
					return;
				}
				this.m_wingFlags = (ulong)val;
				return;
			}
			else
			{
				if (!(name == "ACCOUNT_LICENSE_ID"))
				{
					return;
				}
				this.m_accountLicenseId = (long)val;
				return;
			}
		}
		else if (num != 3756307540U)
		{
			if (num != 4257872637U)
			{
				return;
			}
			if (!(name == "CLASS_ID"))
			{
				return;
			}
			this.m_classId = (int)val;
			return;
		}
		else
		{
			if (!(name == "ACCOUNT_LICENSE_FLAGS"))
			{
				return;
			}
			this.m_accountLicenseFlags = (ulong)val;
			return;
		}
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x0008DFF8 File Offset: 0x0008C1F8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2658990091U)
		{
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
						return typeof(FixedRewardAction.Type);
					}
				}
				else if (num != 655212868U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "ACHIEVE_ID")
				{
					return typeof(int);
				}
			}
			else if (num <= 2102410176U)
			{
				if (num != 1559555090U)
				{
					if (num == 2102410176U)
					{
						if (name == "HERO_LEVEL")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "WING_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 2335490227U)
			{
				if (num == 2658990091U)
				{
					if (name == "TOTAL_HERO_LEVEL")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "TUTORIAL_PROGRESS")
			{
				return typeof(int);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2832947347U)
			{
				if (num != 2695008864U)
				{
					if (num == 2832947347U)
					{
						if (name == "META_ACTION_FLAGS")
						{
							return typeof(ulong);
						}
					}
				}
				else if (name == "WING_PROGRESS")
				{
					return typeof(int);
				}
			}
			else if (num != 2947115096U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "ACTIVE_EVENT")
			{
				return typeof(string);
			}
		}
		else if (num <= 3553888982U)
		{
			if (num != 3365816664U)
			{
				if (num == 3553888982U)
				{
					if (name == "WING_FLAGS")
					{
						return typeof(ulong);
					}
				}
			}
			else if (name == "ACCOUNT_LICENSE_ID")
			{
				return typeof(long);
			}
		}
		else if (num != 3756307540U)
		{
			if (num == 4257872637U)
			{
				if (name == "CLASS_ID")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "ACCOUNT_LICENSE_FLAGS")
		{
			return typeof(ulong);
		}
		return null;
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x0008E31D File Offset: 0x0008C51D
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadFixedRewardActionDbfRecords loadRecords = new LoadFixedRewardActionDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x0008E334 File Offset: 0x0008C534
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		FixedRewardActionDbfAsset fixedRewardActionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(FixedRewardActionDbfAsset)) as FixedRewardActionDbfAsset;
		if (fixedRewardActionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("FixedRewardActionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < fixedRewardActionDbfAsset.Records.Count; i++)
		{
			fixedRewardActionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (fixedRewardActionDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400101C RID: 4124
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400101D RID: 4125
	[SerializeField]
	private FixedRewardAction.Type m_type = FixedRewardAction.ParseTypeValue("tutorial_progress");

	// Token: 0x0400101E RID: 4126
	[SerializeField]
	private int m_wingId;

	// Token: 0x0400101F RID: 4127
	[SerializeField]
	private int m_wingProgress;

	// Token: 0x04001020 RID: 4128
	[SerializeField]
	private ulong m_wingFlags;

	// Token: 0x04001021 RID: 4129
	[SerializeField]
	private int m_classId;

	// Token: 0x04001022 RID: 4130
	[SerializeField]
	private int m_totalHeroLevel;

	// Token: 0x04001023 RID: 4131
	[SerializeField]
	private int m_heroLevel;

	// Token: 0x04001024 RID: 4132
	[SerializeField]
	private int m_tutorialProgress;

	// Token: 0x04001025 RID: 4133
	[SerializeField]
	private ulong m_metaActionFlags;

	// Token: 0x04001026 RID: 4134
	[SerializeField]
	private int m_achieveId;

	// Token: 0x04001027 RID: 4135
	[SerializeField]
	private long m_accountLicenseId;

	// Token: 0x04001028 RID: 4136
	[SerializeField]
	private ulong m_accountLicenseFlags;

	// Token: 0x04001029 RID: 4137
	[SerializeField]
	private string m_activeEvent = "always";

	// Token: 0x0400102A RID: 4138
	[SerializeField]
	private int m_cardId;
}
