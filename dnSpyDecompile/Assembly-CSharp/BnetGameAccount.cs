using System;
using System.Collections.Generic;
using bgs;
using bgs.types;
using PegasusClient;
using PegasusFSG;

// Token: 0x02000769 RID: 1897
public class BnetGameAccount
{
	// Token: 0x06006A8F RID: 27279 RVA: 0x0022A458 File Offset: 0x00228658
	public BnetGameAccount Clone()
	{
		BnetGameAccount bnetGameAccount = (BnetGameAccount)base.MemberwiseClone();
		if (this.m_id != null)
		{
			bnetGameAccount.m_id = this.m_id.Clone();
		}
		if (this.m_ownerId != null)
		{
			bnetGameAccount.m_ownerId = this.m_ownerId.Clone();
		}
		if (this.m_programId != null)
		{
			bnetGameAccount.m_programId = this.m_programId.Clone();
		}
		if (this.m_battleTag != null)
		{
			bnetGameAccount.m_battleTag = this.m_battleTag.Clone();
		}
		bnetGameAccount.m_gameFields = new global::Map<uint, object>();
		foreach (KeyValuePair<uint, object> keyValuePair in this.m_gameFields)
		{
			bnetGameAccount.m_gameFields.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return bnetGameAccount;
	}

	// Token: 0x06006A90 RID: 27280 RVA: 0x0022A554 File Offset: 0x00228754
	public BnetGameAccountId GetId()
	{
		return this.m_id;
	}

	// Token: 0x06006A91 RID: 27281 RVA: 0x0022A55C File Offset: 0x0022875C
	public void SetId(BnetGameAccountId id)
	{
		this.m_id = id;
	}

	// Token: 0x06006A92 RID: 27282 RVA: 0x0022A565 File Offset: 0x00228765
	public BnetAccountId GetOwnerId()
	{
		return this.m_ownerId;
	}

	// Token: 0x06006A93 RID: 27283 RVA: 0x0022A56D File Offset: 0x0022876D
	public void SetOwnerId(BnetAccountId id)
	{
		this.m_ownerId = id;
	}

	// Token: 0x06006A94 RID: 27284 RVA: 0x0022A576 File Offset: 0x00228776
	public BnetProgramId GetProgramId()
	{
		return this.m_programId;
	}

	// Token: 0x06006A95 RID: 27285 RVA: 0x0022A57E File Offset: 0x0022877E
	public void SetProgramId(BnetProgramId programId)
	{
		this.m_programId = programId;
	}

	// Token: 0x06006A96 RID: 27286 RVA: 0x0022A587 File Offset: 0x00228787
	public BnetBattleTag GetBattleTag()
	{
		return this.m_battleTag;
	}

	// Token: 0x06006A97 RID: 27287 RVA: 0x0022A58F File Offset: 0x0022878F
	public void SetBattleTag(BnetBattleTag battleTag)
	{
		this.m_battleTag = battleTag;
	}

	// Token: 0x06006A98 RID: 27288 RVA: 0x0022A598 File Offset: 0x00228798
	public bool IsAway()
	{
		return this.m_away;
	}

	// Token: 0x06006A99 RID: 27289 RVA: 0x0022A5A0 File Offset: 0x002287A0
	public void SetAway(bool away)
	{
		this.m_away = away;
	}

	// Token: 0x06006A9A RID: 27290 RVA: 0x0022A5A9 File Offset: 0x002287A9
	public long GetAwayTimeMicrosec()
	{
		return this.m_awayTimeMicrosec;
	}

	// Token: 0x06006A9B RID: 27291 RVA: 0x0022A5B1 File Offset: 0x002287B1
	public void SetAwayTimeMicrosec(long awayTimeMicrosec)
	{
		this.m_awayTimeMicrosec = awayTimeMicrosec;
	}

	// Token: 0x06006A9C RID: 27292 RVA: 0x0022A5BA File Offset: 0x002287BA
	public bool IsBusy()
	{
		return this.m_busy;
	}

	// Token: 0x06006A9D RID: 27293 RVA: 0x0022A5C2 File Offset: 0x002287C2
	public void SetBusy(bool busy)
	{
		this.m_busy = busy;
	}

	// Token: 0x06006A9E RID: 27294 RVA: 0x0022A5CB File Offset: 0x002287CB
	public bool IsOnline()
	{
		return this.m_online;
	}

	// Token: 0x06006A9F RID: 27295 RVA: 0x0022A5D3 File Offset: 0x002287D3
	public void SetOnline(bool online)
	{
		this.m_online = online;
	}

	// Token: 0x06006AA0 RID: 27296 RVA: 0x0022A5DC File Offset: 0x002287DC
	public long GetLastOnlineMicrosec()
	{
		return this.m_lastOnlineMicrosec;
	}

	// Token: 0x06006AA1 RID: 27297 RVA: 0x0022A5E4 File Offset: 0x002287E4
	public void SetLastOnlineMicrosec(long microsec)
	{
		this.m_lastOnlineMicrosec = microsec;
	}

	// Token: 0x06006AA2 RID: 27298 RVA: 0x0022A5ED File Offset: 0x002287ED
	public string GetRichPresence()
	{
		return this.m_richPresence;
	}

	// Token: 0x06006AA3 RID: 27299 RVA: 0x0022A5F5 File Offset: 0x002287F5
	public void SetRichPresence(string richPresence)
	{
		this.m_richPresence = richPresence;
	}

	// Token: 0x06006AA4 RID: 27300 RVA: 0x0022A5FE File Offset: 0x002287FE
	public global::Map<uint, object> GetGameFields()
	{
		return this.m_gameFields;
	}

	// Token: 0x06006AA5 RID: 27301 RVA: 0x0022A606 File Offset: 0x00228806
	public bool HasGameField(uint fieldId)
	{
		return this.m_gameFields.ContainsKey(fieldId);
	}

	// Token: 0x06006AA6 RID: 27302 RVA: 0x0022A614 File Offset: 0x00228814
	public void SetGameField(uint fieldId, object val)
	{
		this.m_gameFields[fieldId] = val;
	}

	// Token: 0x06006AA7 RID: 27303 RVA: 0x0022A623 File Offset: 0x00228823
	public bool RemoveGameField(uint fieldId)
	{
		return this.m_gameFields.Remove(fieldId);
	}

	// Token: 0x06006AA8 RID: 27304 RVA: 0x0022A631 File Offset: 0x00228831
	public bool TryGetGameField(uint fieldId, out object val)
	{
		return this.m_gameFields.TryGetValue(fieldId, out val);
	}

	// Token: 0x06006AA9 RID: 27305 RVA: 0x0022A640 File Offset: 0x00228840
	public bool TryGetGameFieldBool(uint fieldId, out bool val)
	{
		val = false;
		object obj = null;
		if (!this.m_gameFields.TryGetValue(fieldId, out obj))
		{
			return false;
		}
		val = (bool)obj;
		return true;
	}

	// Token: 0x06006AAA RID: 27306 RVA: 0x0022A670 File Offset: 0x00228870
	public bool TryGetGameFieldInt(uint fieldId, out int val)
	{
		val = 0;
		object obj = null;
		if (!this.m_gameFields.TryGetValue(fieldId, out obj))
		{
			return false;
		}
		val = (int)obj;
		return true;
	}

	// Token: 0x06006AAB RID: 27307 RVA: 0x0022A6A0 File Offset: 0x002288A0
	public bool TryGetGameFieldString(uint fieldId, out string val)
	{
		val = null;
		object obj = null;
		if (!this.m_gameFields.TryGetValue(fieldId, out obj))
		{
			return false;
		}
		val = (string)obj;
		return true;
	}

	// Token: 0x06006AAC RID: 27308 RVA: 0x0022A6D0 File Offset: 0x002288D0
	public bool TryGetGameFieldBytes(uint fieldId, out byte[] val)
	{
		val = null;
		object obj = null;
		if (!this.m_gameFields.TryGetValue(fieldId, out obj))
		{
			return false;
		}
		val = (byte[])obj;
		return true;
	}

	// Token: 0x06006AAD RID: 27309 RVA: 0x0022A700 File Offset: 0x00228900
	public object GetGameField(uint fieldId)
	{
		object result = null;
		this.m_gameFields.TryGetValue(fieldId, out result);
		return result;
	}

	// Token: 0x06006AAE RID: 27310 RVA: 0x0022A720 File Offset: 0x00228920
	public bool GetGameFieldBool(uint fieldId)
	{
		object obj = null;
		return this.m_gameFields.TryGetValue(fieldId, out obj) && obj != null && (bool)obj;
	}

	// Token: 0x06006AAF RID: 27311 RVA: 0x0022A74C File Offset: 0x0022894C
	public int GetGameFieldInt(uint fieldId)
	{
		object obj = null;
		if (this.m_gameFields.TryGetValue(fieldId, out obj) && obj != null)
		{
			return (int)obj;
		}
		return 0;
	}

	// Token: 0x06006AB0 RID: 27312 RVA: 0x0022A778 File Offset: 0x00228978
	public string GetGameFieldString(uint fieldId)
	{
		object obj = null;
		if (this.m_gameFields.TryGetValue(fieldId, out obj) && obj != null)
		{
			return (string)obj;
		}
		return null;
	}

	// Token: 0x06006AB1 RID: 27313 RVA: 0x0022A7A4 File Offset: 0x002289A4
	public byte[] GetGameFieldBytes(uint fieldId)
	{
		object obj = null;
		if (this.m_gameFields.TryGetValue(fieldId, out obj) && obj != null)
		{
			return (byte[])obj;
		}
		return null;
	}

	// Token: 0x06006AB2 RID: 27314 RVA: 0x0022A7D0 File Offset: 0x002289D0
	public EntityId GetGameFieldEntityId(uint fieldId)
	{
		object obj = null;
		if (this.m_gameFields.TryGetValue(fieldId, out obj) && obj != null)
		{
			return (EntityId)obj;
		}
		return default(EntityId);
	}

	// Token: 0x06006AB3 RID: 27315 RVA: 0x0022A802 File Offset: 0x00228A02
	public bool CanBeInvitedToGame()
	{
		return this.GetGameFieldBool(1U);
	}

	// Token: 0x06006AB4 RID: 27316 RVA: 0x0022A80B File Offset: 0x00228A0B
	public string GetClientVersion()
	{
		return this.GetGameFieldString(19U);
	}

	// Token: 0x06006AB5 RID: 27317 RVA: 0x0022A815 File Offset: 0x00228A15
	public string GetClientEnv()
	{
		return this.GetGameFieldString(20U);
	}

	// Token: 0x06006AB6 RID: 27318 RVA: 0x0022A81F File Offset: 0x00228A1F
	public string GetDebugString()
	{
		return this.GetGameFieldString(2U);
	}

	// Token: 0x06006AB7 RID: 27319 RVA: 0x0022A828 File Offset: 0x00228A28
	public PartyId GetPartyId()
	{
		return PartyId.FromEntityId(this.GetGameFieldEntityId(26U));
	}

	// Token: 0x06006AB8 RID: 27320 RVA: 0x0022A838 File Offset: 0x00228A38
	public SessionRecord GetSessionRecord()
	{
		byte[] gameFieldBytes = this.GetGameFieldBytes(22U);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<SessionRecord>(gameFieldBytes, 0, -1);
		}
		return null;
	}

	// Token: 0x06006AB9 RID: 27321 RVA: 0x0022A85F File Offset: 0x00228A5F
	public string GetCardsOpened()
	{
		return this.GetGameFieldString(4U);
	}

	// Token: 0x06006ABA RID: 27322 RVA: 0x0022A868 File Offset: 0x00228A68
	public int GetDruidLevel()
	{
		return this.GetGameFieldInt(5U);
	}

	// Token: 0x06006ABB RID: 27323 RVA: 0x0022A871 File Offset: 0x00228A71
	public int GetHunterLevel()
	{
		return this.GetGameFieldInt(6U);
	}

	// Token: 0x06006ABC RID: 27324 RVA: 0x0022A87A File Offset: 0x00228A7A
	public int GetMageLevel()
	{
		return this.GetGameFieldInt(7U);
	}

	// Token: 0x06006ABD RID: 27325 RVA: 0x0022A883 File Offset: 0x00228A83
	public int GetPaladinLevel()
	{
		return this.GetGameFieldInt(8U);
	}

	// Token: 0x06006ABE RID: 27326 RVA: 0x0022A88C File Offset: 0x00228A8C
	public int GetPriestLevel()
	{
		return this.GetGameFieldInt(9U);
	}

	// Token: 0x06006ABF RID: 27327 RVA: 0x0022A896 File Offset: 0x00228A96
	public int GetRogueLevel()
	{
		return this.GetGameFieldInt(10U);
	}

	// Token: 0x06006AC0 RID: 27328 RVA: 0x0022A8A0 File Offset: 0x00228AA0
	public int GetShamanLevel()
	{
		return this.GetGameFieldInt(11U);
	}

	// Token: 0x06006AC1 RID: 27329 RVA: 0x0022A8AA File Offset: 0x00228AAA
	public int GetWarlockLevel()
	{
		return this.GetGameFieldInt(12U);
	}

	// Token: 0x06006AC2 RID: 27330 RVA: 0x0022A8B4 File Offset: 0x00228AB4
	public int GetWarriorLevel()
	{
		return this.GetGameFieldInt(13U);
	}

	// Token: 0x06006AC3 RID: 27331 RVA: 0x0022A8BE File Offset: 0x00228ABE
	public int GetGainMedal()
	{
		return this.GetGameFieldInt(14U);
	}

	// Token: 0x06006AC4 RID: 27332 RVA: 0x0022A8C8 File Offset: 0x00228AC8
	public int GetTutorialBeaten()
	{
		return this.GetGameFieldInt(15U);
	}

	// Token: 0x06006AC5 RID: 27333 RVA: 0x0022A8D2 File Offset: 0x00228AD2
	public int GetCollectionEvent()
	{
		return this.GetGameFieldInt(16U);
	}

	// Token: 0x06006AC6 RID: 27334 RVA: 0x0022A8DC File Offset: 0x00228ADC
	public DeckValidity GetDeckValidity()
	{
		byte[] gameFieldBytes = this.GetGameFieldBytes(24U);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<DeckValidity>(gameFieldBytes, 0, -1);
		}
		return null;
	}

	// Token: 0x06006AC7 RID: 27335 RVA: 0x0022A904 File Offset: 0x00228B04
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetGameAccount bnetGameAccount = obj as BnetGameAccount;
		return bnetGameAccount != null && this.m_id.Equals(bnetGameAccount.m_id);
	}

	// Token: 0x06006AC8 RID: 27336 RVA: 0x0022A933 File Offset: 0x00228B33
	public bool Equals(BnetGameAccountId other)
	{
		return other != null && this.m_id.Equals(other);
	}

	// Token: 0x06006AC9 RID: 27337 RVA: 0x0022A946 File Offset: 0x00228B46
	public override int GetHashCode()
	{
		return this.m_id.GetHashCode();
	}

	// Token: 0x06006ACA RID: 27338 RVA: 0x0022A953 File Offset: 0x00228B53
	public static bool operator ==(BnetGameAccount a, BnetGameAccount b)
	{
		return a == b || (a != null && b != null && a.m_id == b.m_id);
	}

	// Token: 0x06006ACB RID: 27339 RVA: 0x0022A974 File Offset: 0x00228B74
	public static bool operator !=(BnetGameAccount a, BnetGameAccount b)
	{
		return !(a == b);
	}

	// Token: 0x06006ACC RID: 27340 RVA: 0x0022A980 File Offset: 0x00228B80
	public override string ToString()
	{
		if (this.m_id == null)
		{
			return "UNKNOWN GAME ACCOUNT";
		}
		return string.Format("[id={0} programId={1} battleTag={2} online={3}]", new object[]
		{
			this.m_id,
			this.m_programId,
			this.m_battleTag,
			this.m_online
		});
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x06006ACD RID: 27341 RVA: 0x0022A9DC File Offset: 0x00228BDC
	public string FullPresenceSummary
	{
		get
		{
			return string.Format("GameAccount [id={0} battleTag={1} {2} {3} richPresence={4} away={5} busy={6} lastOnline={7} awayTime={8}]", new object[]
			{
				this.m_id,
				this.m_battleTag,
				this.m_programId,
				this.m_online ? "online" : "offline",
				this.m_richPresence,
				this.m_away,
				this.m_busy,
				(this.m_lastOnlineMicrosec == 0L) ? "null" : global::TimeUtils.ConvertEpochMicrosecToDateTime(this.m_lastOnlineMicrosec).ToString("R"),
				(this.m_awayTimeMicrosec == 0L) ? "null" : global::TimeUtils.ConvertEpochMicrosecToDateTime(this.m_awayTimeMicrosec).ToString("R")
			});
		}
	}

	// Token: 0x040056E7 RID: 22247
	private BnetGameAccountId m_id;

	// Token: 0x040056E8 RID: 22248
	private BnetAccountId m_ownerId;

	// Token: 0x040056E9 RID: 22249
	private BnetProgramId m_programId;

	// Token: 0x040056EA RID: 22250
	private BnetBattleTag m_battleTag;

	// Token: 0x040056EB RID: 22251
	private bool m_away;

	// Token: 0x040056EC RID: 22252
	private long m_awayTimeMicrosec;

	// Token: 0x040056ED RID: 22253
	private bool m_busy;

	// Token: 0x040056EE RID: 22254
	private bool m_online;

	// Token: 0x040056EF RID: 22255
	private long m_lastOnlineMicrosec;

	// Token: 0x040056F0 RID: 22256
	private string m_richPresence;

	// Token: 0x040056F1 RID: 22257
	private global::Map<uint, object> m_gameFields = new global::Map<uint, object>();
}
