using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200020E RID: 526
[Serializable]
public class LoginPopupSequencePopupDbfRecord : DbfRecord
{
	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00094CCE File Offset: 0x00092ECE
	[DbfField("LOGIN_POPUP_SEQUENCE_ID")]
	public int LoginPopupSequenceId
	{
		get
		{
			return this.m_loginPopupSequenceId;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00094CD6 File Offset: 0x00092ED6
	[DbfField("POPUP_TYPE")]
	public Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType PopupType
	{
		get
		{
			return this.m_popupType;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00094CDE File Offset: 0x00092EDE
	[DbfField("REQUIRES_WILD_UNLOCKED")]
	public bool RequiresWildUnlocked
	{
		get
		{
			return this.m_requiresWildUnlocked;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00094CE6 File Offset: 0x00092EE6
	[DbfField("SUPPRESS_FOR_RETURNING_PLAYER")]
	public bool SuppressForReturningPlayer
	{
		get
		{
			return this.m_suppressForReturningPlayer;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00094CEE File Offset: 0x00092EEE
	[DbfField("PREFAB_OVERRIDE")]
	public string PrefabOverride
	{
		get
		{
			return this.m_prefabOverride;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00094CF6 File Offset: 0x00092EF6
	[DbfField("BACKGROUND_MATERIAL")]
	public string BackgroundMaterial
	{
		get
		{
			return this.m_backgroundMaterial;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00094CFE File Offset: 0x00092EFE
	[DbfField("HEADER_TEXT")]
	public DbfLocValue HeaderText
	{
		get
		{
			return this.m_headerText;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00094D06 File Offset: 0x00092F06
	[DbfField("BODY_TEXT")]
	public DbfLocValue BodyText
	{
		get
		{
			return this.m_bodyText;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00094D0E File Offset: 0x00092F0E
	[DbfField("BUTTON_TEXT")]
	public DbfLocValue ButtonText
	{
		get
		{
			return this.m_buttonText;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00094D16 File Offset: 0x00092F16
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00094D1E File Offset: 0x00092F1E
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00094D30 File Offset: 0x00092F30
	[DbfField("CARD_PREMIUM")]
	public int CardPremium
	{
		get
		{
			return this.m_cardPremium;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00094D38 File Offset: 0x00092F38
	[DbfField("FEATURED_CARDS_EVENT")]
	public string FeaturedCardsEvent
	{
		get
		{
			return this.m_featuredCardsEvent;
		}
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x00094D40 File Offset: 0x00092F40
	public void SetLoginPopupSequenceId(int v)
	{
		this.m_loginPopupSequenceId = v;
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x00094D49 File Offset: 0x00092F49
	public void SetPopupType(Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType v)
	{
		this.m_popupType = v;
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x00094D52 File Offset: 0x00092F52
	public void SetRequiresWildUnlocked(bool v)
	{
		this.m_requiresWildUnlocked = v;
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x00094D5B File Offset: 0x00092F5B
	public void SetSuppressForReturningPlayer(bool v)
	{
		this.m_suppressForReturningPlayer = v;
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x00094D64 File Offset: 0x00092F64
	public void SetPrefabOverride(string v)
	{
		this.m_prefabOverride = v;
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x00094D6D File Offset: 0x00092F6D
	public void SetBackgroundMaterial(string v)
	{
		this.m_backgroundMaterial = v;
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x00094D76 File Offset: 0x00092F76
	public void SetHeaderText(DbfLocValue v)
	{
		this.m_headerText = v;
		v.SetDebugInfo(base.ID, "HEADER_TEXT");
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00094D90 File Offset: 0x00092F90
	public void SetBodyText(DbfLocValue v)
	{
		this.m_bodyText = v;
		v.SetDebugInfo(base.ID, "BODY_TEXT");
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x00094DAA File Offset: 0x00092FAA
	public void SetButtonText(DbfLocValue v)
	{
		this.m_buttonText = v;
		v.SetDebugInfo(base.ID, "BUTTON_TEXT");
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00094DC4 File Offset: 0x00092FC4
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x00094DCD File Offset: 0x00092FCD
	public void SetCardPremium(int v)
	{
		this.m_cardPremium = v;
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x00094DD6 File Offset: 0x00092FD6
	public void SetFeaturedCardsEvent(string v)
	{
		this.m_featuredCardsEvent = v;
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x00094DE0 File Offset: 0x00092FE0
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1386967833U)
		{
			if (num <= 461394230U)
			{
				if (num != 160774710U)
				{
					if (num != 451390141U)
					{
						if (num == 461394230U)
						{
							if (name == "HEADER_TEXT")
							{
								return this.m_headerText;
							}
						}
					}
					else if (name == "CARD_ID")
					{
						return this.m_cardId;
					}
				}
				else if (name == "POPUP_TYPE")
				{
					return this.m_popupType;
				}
			}
			else if (num != 512270051U)
			{
				if (num != 1015438284U)
				{
					if (num == 1386967833U)
					{
						if (name == "CARD_PREMIUM")
						{
							return this.m_cardPremium;
						}
					}
				}
				else if (name == "FEATURED_CARDS_EVENT")
				{
					return this.m_featuredCardsEvent;
				}
			}
			else if (name == "LOGIN_POPUP_SEQUENCE_ID")
			{
				return this.m_loginPopupSequenceId;
			}
		}
		else if (num <= 2636340827U)
		{
			if (num != 1458105184U)
			{
				if (num != 1770463370U)
				{
					if (num == 2636340827U)
					{
						if (name == "BACKGROUND_MATERIAL")
						{
							return this.m_backgroundMaterial;
						}
					}
				}
				else if (name == "REQUIRES_WILD_UNLOCKED")
				{
					return this.m_requiresWildUnlocked;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 3083958081U)
		{
			if (num != 2992308412U)
			{
				if (num == 3083958081U)
				{
					if (name == "BUTTON_TEXT")
					{
						return this.m_buttonText;
					}
				}
			}
			else if (name == "PREFAB_OVERRIDE")
			{
				return this.m_prefabOverride;
			}
		}
		else if (num != 3294267989U)
		{
			if (num == 3679449341U)
			{
				if (name == "BODY_TEXT")
				{
					return this.m_bodyText;
				}
			}
		}
		else if (name == "SUPPRESS_FOR_RETURNING_PLAYER")
		{
			return this.m_suppressForReturningPlayer;
		}
		return null;
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x00095040 File Offset: 0x00093240
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1386967833U)
		{
			if (num <= 461394230U)
			{
				if (num != 160774710U)
				{
					if (num != 451390141U)
					{
						if (num != 461394230U)
						{
							return;
						}
						if (!(name == "HEADER_TEXT"))
						{
							return;
						}
						this.m_headerText = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "CARD_ID"))
						{
							return;
						}
						this.m_cardId = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "POPUP_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_popupType = Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType.INVALID;
						return;
					}
					if (val is Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType || val is int)
					{
						this.m_popupType = (Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType)val;
						return;
					}
					if (val is string)
					{
						this.m_popupType = Assets.LoginPopupSequencePopup.ParseLoginPopupSequencePopupTypeValue((string)val);
						return;
					}
				}
			}
			else if (num != 512270051U)
			{
				if (num != 1015438284U)
				{
					if (num != 1386967833U)
					{
						return;
					}
					if (!(name == "CARD_PREMIUM"))
					{
						return;
					}
					this.m_cardPremium = (int)val;
					return;
				}
				else
				{
					if (!(name == "FEATURED_CARDS_EVENT"))
					{
						return;
					}
					this.m_featuredCardsEvent = (string)val;
				}
			}
			else
			{
				if (!(name == "LOGIN_POPUP_SEQUENCE_ID"))
				{
					return;
				}
				this.m_loginPopupSequenceId = (int)val;
				return;
			}
			return;
		}
		if (num <= 2636340827U)
		{
			if (num != 1458105184U)
			{
				if (num != 1770463370U)
				{
					if (num != 2636340827U)
					{
						return;
					}
					if (!(name == "BACKGROUND_MATERIAL"))
					{
						return;
					}
					this.m_backgroundMaterial = (string)val;
					return;
				}
				else
				{
					if (!(name == "REQUIRES_WILD_UNLOCKED"))
					{
						return;
					}
					this.m_requiresWildUnlocked = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num <= 3083958081U)
		{
			if (num != 2992308412U)
			{
				if (num != 3083958081U)
				{
					return;
				}
				if (!(name == "BUTTON_TEXT"))
				{
					return;
				}
				this.m_buttonText = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "PREFAB_OVERRIDE"))
				{
					return;
				}
				this.m_prefabOverride = (string)val;
				return;
			}
		}
		else if (num != 3294267989U)
		{
			if (num != 3679449341U)
			{
				return;
			}
			if (!(name == "BODY_TEXT"))
			{
				return;
			}
			this.m_bodyText = (DbfLocValue)val;
			return;
		}
		else
		{
			if (!(name == "SUPPRESS_FOR_RETURNING_PLAYER"))
			{
				return;
			}
			this.m_suppressForReturningPlayer = (bool)val;
			return;
		}
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x000952B8 File Offset: 0x000934B8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1386967833U)
		{
			if (num <= 461394230U)
			{
				if (num != 160774710U)
				{
					if (num != 451390141U)
					{
						if (num == 461394230U)
						{
							if (name == "HEADER_TEXT")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "CARD_ID")
					{
						return typeof(int);
					}
				}
				else if (name == "POPUP_TYPE")
				{
					return typeof(Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType);
				}
			}
			else if (num != 512270051U)
			{
				if (num != 1015438284U)
				{
					if (num == 1386967833U)
					{
						if (name == "CARD_PREMIUM")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "FEATURED_CARDS_EVENT")
				{
					return typeof(string);
				}
			}
			else if (name == "LOGIN_POPUP_SEQUENCE_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2636340827U)
		{
			if (num != 1458105184U)
			{
				if (num != 1770463370U)
				{
					if (num == 2636340827U)
					{
						if (name == "BACKGROUND_MATERIAL")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "REQUIRES_WILD_UNLOCKED")
				{
					return typeof(bool);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3083958081U)
		{
			if (num != 2992308412U)
			{
				if (num == 3083958081U)
				{
					if (name == "BUTTON_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "PREFAB_OVERRIDE")
			{
				return typeof(string);
			}
		}
		else if (num != 3294267989U)
		{
			if (num == 3679449341U)
			{
				if (name == "BODY_TEXT")
				{
					return typeof(DbfLocValue);
				}
			}
		}
		else if (name == "SUPPRESS_FOR_RETURNING_PLAYER")
		{
			return typeof(bool);
		}
		return null;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0009552F File Offset: 0x0009372F
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginPopupSequencePopupDbfRecords loadRecords = new LoadLoginPopupSequencePopupDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x00095548 File Offset: 0x00093748
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginPopupSequencePopupDbfAsset loginPopupSequencePopupDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginPopupSequencePopupDbfAsset)) as LoginPopupSequencePopupDbfAsset;
		if (loginPopupSequencePopupDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LoginPopupSequencePopupDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < loginPopupSequencePopupDbfAsset.Records.Count; i++)
		{
			loginPopupSequencePopupDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (loginPopupSequencePopupDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x000955C7 File Offset: 0x000937C7
	public override void StripUnusedLocales()
	{
		this.m_headerText.StripUnusedLocales();
		this.m_bodyText.StripUnusedLocales();
		this.m_buttonText.StripUnusedLocales();
	}

	// Token: 0x04001115 RID: 4373
	[SerializeField]
	private int m_loginPopupSequenceId;

	// Token: 0x04001116 RID: 4374
	[SerializeField]
	private Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType m_popupType = Assets.LoginPopupSequencePopup.ParseLoginPopupSequencePopupTypeValue("basic");

	// Token: 0x04001117 RID: 4375
	[SerializeField]
	private bool m_requiresWildUnlocked;

	// Token: 0x04001118 RID: 4376
	[SerializeField]
	private bool m_suppressForReturningPlayer;

	// Token: 0x04001119 RID: 4377
	[SerializeField]
	private string m_prefabOverride;

	// Token: 0x0400111A RID: 4378
	[SerializeField]
	private string m_backgroundMaterial;

	// Token: 0x0400111B RID: 4379
	[SerializeField]
	private DbfLocValue m_headerText;

	// Token: 0x0400111C RID: 4380
	[SerializeField]
	private DbfLocValue m_bodyText;

	// Token: 0x0400111D RID: 4381
	[SerializeField]
	private DbfLocValue m_buttonText;

	// Token: 0x0400111E RID: 4382
	[SerializeField]
	private int m_cardId;

	// Token: 0x0400111F RID: 4383
	[SerializeField]
	private int m_cardPremium;

	// Token: 0x04001120 RID: 4384
	[SerializeField]
	private string m_featuredCardsEvent = "never";
}
