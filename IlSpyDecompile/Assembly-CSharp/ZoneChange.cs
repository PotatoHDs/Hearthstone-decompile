public class ZoneChange
{
	private ZoneChangeList m_parentList;

	private PowerTask m_powerTask;

	private Entity m_entity;

	private Zone m_sourceZone;

	private TAG_ZONE m_sourceZoneTag;

	private int? m_sourcePos;

	private int? m_sourceControllerId;

	private Zone m_destinationZone;

	private TAG_ZONE m_destinationZoneTag;

	private int? m_destinationPos;

	private int? m_destinationControllerId;

	public ZoneChangeList GetParentList()
	{
		return m_parentList;
	}

	public void SetParentList(ZoneChangeList parentList)
	{
		m_parentList = parentList;
	}

	public PowerTask GetPowerTask()
	{
		return m_powerTask;
	}

	public void SetPowerTask(PowerTask powerTask)
	{
		m_powerTask = powerTask;
	}

	public Entity GetEntity()
	{
		return m_entity;
	}

	public void SetEntity(Entity entity)
	{
		m_entity = entity;
	}

	public Zone GetDestinationZone()
	{
		return m_destinationZone;
	}

	public void SetDestinationZone(Zone zone)
	{
		m_destinationZone = zone;
	}

	public TAG_ZONE GetDestinationZoneTag()
	{
		return m_destinationZoneTag;
	}

	public void SetDestinationZoneTag(TAG_ZONE tag)
	{
		m_destinationZoneTag = tag;
	}

	public int GetDestinationPosition()
	{
		if (m_destinationPos.HasValue)
		{
			return m_destinationPos.Value;
		}
		return 0;
	}

	public void SetDestinationPosition(int pos)
	{
		m_destinationPos = pos;
	}

	public int GetDestinationControllerId()
	{
		if (m_destinationControllerId.HasValue)
		{
			return m_destinationControllerId.Value;
		}
		return 0;
	}

	public void SetDestinationControllerId(int controllerId)
	{
		m_destinationControllerId = controllerId;
	}

	public void ClearDestinationControllerId()
	{
		m_destinationControllerId = null;
	}

	public Zone GetSourceZone()
	{
		return m_sourceZone;
	}

	public void SetSourceZone(Zone zone)
	{
		m_sourceZone = zone;
	}

	public TAG_ZONE GetSourceZoneTag()
	{
		return m_sourceZoneTag;
	}

	public void SetSourceZoneTag(TAG_ZONE tag)
	{
		m_sourceZoneTag = tag;
	}

	public int GetSourcePosition()
	{
		if (m_sourcePos.HasValue)
		{
			return m_sourcePos.Value;
		}
		return 0;
	}

	public void SetSourcePosition(int pos)
	{
		m_sourcePos = pos;
	}

	public int GetSourceControllerId()
	{
		if (m_sourceControllerId.HasValue)
		{
			return m_sourceControllerId.Value;
		}
		return 0;
	}

	public void SetSourceControllerId(int controllerId)
	{
		m_sourceControllerId = controllerId;
	}

	public void ClearSourceControllerId()
	{
		m_sourceControllerId = null;
	}

	public bool HasSourceZone()
	{
		return m_sourceZone != null;
	}

	public bool HasSourceZoneTag()
	{
		return m_sourceZoneTag != TAG_ZONE.INVALID;
	}

	public bool HasSourcePosition()
	{
		return m_sourcePos.HasValue;
	}

	public bool HasSourceData()
	{
		if (HasSourceZoneTag())
		{
			return true;
		}
		if (HasSourcePosition())
		{
			return true;
		}
		return false;
	}

	public bool HasDestinationZone()
	{
		return m_destinationZone != null;
	}

	public bool HasDestinationZoneTag()
	{
		return m_destinationZoneTag != TAG_ZONE.INVALID;
	}

	public bool HasDestinationPosition()
	{
		return m_destinationPos.HasValue;
	}

	public bool HasDestinationControllerId()
	{
		return m_destinationControllerId.HasValue;
	}

	public bool HasDestinationData()
	{
		if (HasDestinationZoneTag())
		{
			return true;
		}
		if (HasDestinationPosition())
		{
			return true;
		}
		if (HasDestinationControllerId())
		{
			return true;
		}
		return false;
	}

	public bool HasDestinationZoneChange()
	{
		if (HasDestinationZoneTag())
		{
			return true;
		}
		if (HasDestinationControllerId())
		{
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		return $"powerTask=[{m_powerTask}] entity={m_entity} srcZoneTag={m_sourceZoneTag} srcPos={m_sourcePos} dstZoneTag={m_destinationZoneTag} dstPos={m_destinationPos}";
	}
}
