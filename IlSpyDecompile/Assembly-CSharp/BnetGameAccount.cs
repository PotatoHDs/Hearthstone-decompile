using System.Collections.Generic;
using bgs;
using bgs.types;
using PegasusClient;
using PegasusFSG;

public class BnetGameAccount
{
	private BnetGameAccountId m_id;

	private BnetAccountId m_ownerId;

	private BnetProgramId m_programId;

	private BnetBattleTag m_battleTag;

	private bool m_away;

	private long m_awayTimeMicrosec;

	private bool m_busy;

	private bool m_online;

	private long m_lastOnlineMicrosec;

	private string m_richPresence;

	private Map<uint, object> m_gameFields = new Map<uint, object>();

	public string FullPresenceSummary => string.Format("GameAccount [id={0} battleTag={1} {2} {3} richPresence={4} away={5} busy={6} lastOnline={7} awayTime={8}]", m_id, m_battleTag, m_programId, m_online ? "online" : "offline", m_richPresence, m_away, m_busy, (m_lastOnlineMicrosec == 0L) ? "null" : TimeUtils.ConvertEpochMicrosecToDateTime(m_lastOnlineMicrosec).ToString("R"), (m_awayTimeMicrosec == 0L) ? "null" : TimeUtils.ConvertEpochMicrosecToDateTime(m_awayTimeMicrosec).ToString("R"));

	public BnetGameAccount Clone()
	{
		BnetGameAccount bnetGameAccount = (BnetGameAccount)MemberwiseClone();
		if (m_id != null)
		{
			bnetGameAccount.m_id = m_id.Clone();
		}
		if (m_ownerId != null)
		{
			bnetGameAccount.m_ownerId = m_ownerId.Clone();
		}
		if (m_programId != null)
		{
			bnetGameAccount.m_programId = m_programId.Clone();
		}
		if (m_battleTag != null)
		{
			bnetGameAccount.m_battleTag = m_battleTag.Clone();
		}
		bnetGameAccount.m_gameFields = new Map<uint, object>();
		foreach (KeyValuePair<uint, object> gameField in m_gameFields)
		{
			bnetGameAccount.m_gameFields.Add(gameField.Key, gameField.Value);
		}
		return bnetGameAccount;
	}

	public BnetGameAccountId GetId()
	{
		return m_id;
	}

	public void SetId(BnetGameAccountId id)
	{
		m_id = id;
	}

	public BnetAccountId GetOwnerId()
	{
		return m_ownerId;
	}

	public void SetOwnerId(BnetAccountId id)
	{
		m_ownerId = id;
	}

	public BnetProgramId GetProgramId()
	{
		return m_programId;
	}

	public void SetProgramId(BnetProgramId programId)
	{
		m_programId = programId;
	}

	public BnetBattleTag GetBattleTag()
	{
		return m_battleTag;
	}

	public void SetBattleTag(BnetBattleTag battleTag)
	{
		m_battleTag = battleTag;
	}

	public bool IsAway()
	{
		return m_away;
	}

	public void SetAway(bool away)
	{
		m_away = away;
	}

	public long GetAwayTimeMicrosec()
	{
		return m_awayTimeMicrosec;
	}

	public void SetAwayTimeMicrosec(long awayTimeMicrosec)
	{
		m_awayTimeMicrosec = awayTimeMicrosec;
	}

	public bool IsBusy()
	{
		return m_busy;
	}

	public void SetBusy(bool busy)
	{
		m_busy = busy;
	}

	public bool IsOnline()
	{
		return m_online;
	}

	public void SetOnline(bool online)
	{
		m_online = online;
	}

	public long GetLastOnlineMicrosec()
	{
		return m_lastOnlineMicrosec;
	}

	public void SetLastOnlineMicrosec(long microsec)
	{
		m_lastOnlineMicrosec = microsec;
	}

	public string GetRichPresence()
	{
		return m_richPresence;
	}

	public void SetRichPresence(string richPresence)
	{
		m_richPresence = richPresence;
	}

	public Map<uint, object> GetGameFields()
	{
		return m_gameFields;
	}

	public bool HasGameField(uint fieldId)
	{
		return m_gameFields.ContainsKey(fieldId);
	}

	public void SetGameField(uint fieldId, object val)
	{
		m_gameFields[fieldId] = val;
	}

	public bool RemoveGameField(uint fieldId)
	{
		return m_gameFields.Remove(fieldId);
	}

	public bool TryGetGameField(uint fieldId, out object val)
	{
		return m_gameFields.TryGetValue(fieldId, out val);
	}

	public bool TryGetGameFieldBool(uint fieldId, out bool val)
	{
		val = false;
		object value = null;
		if (!m_gameFields.TryGetValue(fieldId, out value))
		{
			return false;
		}
		val = (bool)value;
		return true;
	}

	public bool TryGetGameFieldInt(uint fieldId, out int val)
	{
		val = 0;
		object value = null;
		if (!m_gameFields.TryGetValue(fieldId, out value))
		{
			return false;
		}
		val = (int)value;
		return true;
	}

	public bool TryGetGameFieldString(uint fieldId, out string val)
	{
		val = null;
		object value = null;
		if (!m_gameFields.TryGetValue(fieldId, out value))
		{
			return false;
		}
		val = (string)value;
		return true;
	}

	public bool TryGetGameFieldBytes(uint fieldId, out byte[] val)
	{
		val = null;
		object value = null;
		if (!m_gameFields.TryGetValue(fieldId, out value))
		{
			return false;
		}
		val = (byte[])value;
		return true;
	}

	public object GetGameField(uint fieldId)
	{
		object value = null;
		m_gameFields.TryGetValue(fieldId, out value);
		return value;
	}

	public bool GetGameFieldBool(uint fieldId)
	{
		object value = null;
		if (m_gameFields.TryGetValue(fieldId, out value) && value != null)
		{
			return (bool)value;
		}
		return false;
	}

	public int GetGameFieldInt(uint fieldId)
	{
		object value = null;
		if (m_gameFields.TryGetValue(fieldId, out value) && value != null)
		{
			return (int)value;
		}
		return 0;
	}

	public string GetGameFieldString(uint fieldId)
	{
		object value = null;
		if (m_gameFields.TryGetValue(fieldId, out value) && value != null)
		{
			return (string)value;
		}
		return null;
	}

	public byte[] GetGameFieldBytes(uint fieldId)
	{
		object value = null;
		if (m_gameFields.TryGetValue(fieldId, out value) && value != null)
		{
			return (byte[])value;
		}
		return null;
	}

	public EntityId GetGameFieldEntityId(uint fieldId)
	{
		object value = null;
		if (m_gameFields.TryGetValue(fieldId, out value) && value != null)
		{
			return (EntityId)value;
		}
		return default(EntityId);
	}

	public bool CanBeInvitedToGame()
	{
		return GetGameFieldBool(1u);
	}

	public string GetClientVersion()
	{
		return GetGameFieldString(19u);
	}

	public string GetClientEnv()
	{
		return GetGameFieldString(20u);
	}

	public string GetDebugString()
	{
		return GetGameFieldString(2u);
	}

	public PartyId GetPartyId()
	{
		return PartyId.FromEntityId(GetGameFieldEntityId(26u));
	}

	public SessionRecord GetSessionRecord()
	{
		byte[] gameFieldBytes = GetGameFieldBytes(22u);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<SessionRecord>(gameFieldBytes);
		}
		return null;
	}

	public string GetCardsOpened()
	{
		return GetGameFieldString(4u);
	}

	public int GetDruidLevel()
	{
		return GetGameFieldInt(5u);
	}

	public int GetHunterLevel()
	{
		return GetGameFieldInt(6u);
	}

	public int GetMageLevel()
	{
		return GetGameFieldInt(7u);
	}

	public int GetPaladinLevel()
	{
		return GetGameFieldInt(8u);
	}

	public int GetPriestLevel()
	{
		return GetGameFieldInt(9u);
	}

	public int GetRogueLevel()
	{
		return GetGameFieldInt(10u);
	}

	public int GetShamanLevel()
	{
		return GetGameFieldInt(11u);
	}

	public int GetWarlockLevel()
	{
		return GetGameFieldInt(12u);
	}

	public int GetWarriorLevel()
	{
		return GetGameFieldInt(13u);
	}

	public int GetGainMedal()
	{
		return GetGameFieldInt(14u);
	}

	public int GetTutorialBeaten()
	{
		return GetGameFieldInt(15u);
	}

	public int GetCollectionEvent()
	{
		return GetGameFieldInt(16u);
	}

	public DeckValidity GetDeckValidity()
	{
		byte[] gameFieldBytes = GetGameFieldBytes(24u);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<DeckValidity>(gameFieldBytes);
		}
		return null;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetGameAccount bnetGameAccount = obj as BnetGameAccount;
		if ((object)bnetGameAccount == null)
		{
			return false;
		}
		return m_id.Equals(bnetGameAccount.m_id);
	}

	public bool Equals(BnetGameAccountId other)
	{
		if ((object)other == null)
		{
			return false;
		}
		return m_id.Equals(other);
	}

	public override int GetHashCode()
	{
		return m_id.GetHashCode();
	}

	public static bool operator ==(BnetGameAccount a, BnetGameAccount b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_id == b.m_id;
	}

	public static bool operator !=(BnetGameAccount a, BnetGameAccount b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		if (m_id == null)
		{
			return "UNKNOWN GAME ACCOUNT";
		}
		return $"[id={m_id} programId={m_programId} battleTag={m_battleTag} online={m_online}]";
	}
}
