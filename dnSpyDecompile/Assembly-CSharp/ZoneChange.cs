using System;

// Token: 0x02000360 RID: 864
public class ZoneChange
{
	// Token: 0x0600322A RID: 12842 RVA: 0x00100BFD File Offset: 0x000FEDFD
	public ZoneChangeList GetParentList()
	{
		return this.m_parentList;
	}

	// Token: 0x0600322B RID: 12843 RVA: 0x00100C05 File Offset: 0x000FEE05
	public void SetParentList(ZoneChangeList parentList)
	{
		this.m_parentList = parentList;
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x00100C0E File Offset: 0x000FEE0E
	public PowerTask GetPowerTask()
	{
		return this.m_powerTask;
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x00100C16 File Offset: 0x000FEE16
	public void SetPowerTask(PowerTask powerTask)
	{
		this.m_powerTask = powerTask;
	}

	// Token: 0x0600322E RID: 12846 RVA: 0x00100C1F File Offset: 0x000FEE1F
	public Entity GetEntity()
	{
		return this.m_entity;
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x00100C27 File Offset: 0x000FEE27
	public void SetEntity(Entity entity)
	{
		this.m_entity = entity;
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x00100C30 File Offset: 0x000FEE30
	public Zone GetDestinationZone()
	{
		return this.m_destinationZone;
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x00100C38 File Offset: 0x000FEE38
	public void SetDestinationZone(Zone zone)
	{
		this.m_destinationZone = zone;
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x00100C41 File Offset: 0x000FEE41
	public TAG_ZONE GetDestinationZoneTag()
	{
		return this.m_destinationZoneTag;
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x00100C49 File Offset: 0x000FEE49
	public void SetDestinationZoneTag(TAG_ZONE tag)
	{
		this.m_destinationZoneTag = tag;
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x00100C52 File Offset: 0x000FEE52
	public int GetDestinationPosition()
	{
		if (this.m_destinationPos != null)
		{
			return this.m_destinationPos.Value;
		}
		return 0;
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x00100C6E File Offset: 0x000FEE6E
	public void SetDestinationPosition(int pos)
	{
		this.m_destinationPos = new int?(pos);
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x00100C7C File Offset: 0x000FEE7C
	public int GetDestinationControllerId()
	{
		if (this.m_destinationControllerId != null)
		{
			return this.m_destinationControllerId.Value;
		}
		return 0;
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x00100C98 File Offset: 0x000FEE98
	public void SetDestinationControllerId(int controllerId)
	{
		this.m_destinationControllerId = new int?(controllerId);
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x00100CA6 File Offset: 0x000FEEA6
	public void ClearDestinationControllerId()
	{
		this.m_destinationControllerId = null;
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x00100CB4 File Offset: 0x000FEEB4
	public Zone GetSourceZone()
	{
		return this.m_sourceZone;
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x00100CBC File Offset: 0x000FEEBC
	public void SetSourceZone(Zone zone)
	{
		this.m_sourceZone = zone;
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x00100CC5 File Offset: 0x000FEEC5
	public TAG_ZONE GetSourceZoneTag()
	{
		return this.m_sourceZoneTag;
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x00100CCD File Offset: 0x000FEECD
	public void SetSourceZoneTag(TAG_ZONE tag)
	{
		this.m_sourceZoneTag = tag;
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x00100CD6 File Offset: 0x000FEED6
	public int GetSourcePosition()
	{
		if (this.m_sourcePos != null)
		{
			return this.m_sourcePos.Value;
		}
		return 0;
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x00100CF2 File Offset: 0x000FEEF2
	public void SetSourcePosition(int pos)
	{
		this.m_sourcePos = new int?(pos);
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x00100D00 File Offset: 0x000FEF00
	public int GetSourceControllerId()
	{
		if (this.m_sourceControllerId != null)
		{
			return this.m_sourceControllerId.Value;
		}
		return 0;
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x00100D1C File Offset: 0x000FEF1C
	public void SetSourceControllerId(int controllerId)
	{
		this.m_sourceControllerId = new int?(controllerId);
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x00100D2A File Offset: 0x000FEF2A
	public void ClearSourceControllerId()
	{
		this.m_sourceControllerId = null;
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x00100D38 File Offset: 0x000FEF38
	public bool HasSourceZone()
	{
		return this.m_sourceZone != null;
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x00100D46 File Offset: 0x000FEF46
	public bool HasSourceZoneTag()
	{
		return this.m_sourceZoneTag > TAG_ZONE.INVALID;
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x00100D51 File Offset: 0x000FEF51
	public bool HasSourcePosition()
	{
		return this.m_sourcePos != null;
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x00100D5E File Offset: 0x000FEF5E
	public bool HasSourceData()
	{
		return this.HasSourceZoneTag() || this.HasSourcePosition();
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x00100D75 File Offset: 0x000FEF75
	public bool HasDestinationZone()
	{
		return this.m_destinationZone != null;
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x00100D83 File Offset: 0x000FEF83
	public bool HasDestinationZoneTag()
	{
		return this.m_destinationZoneTag > TAG_ZONE.INVALID;
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x00100D8E File Offset: 0x000FEF8E
	public bool HasDestinationPosition()
	{
		return this.m_destinationPos != null;
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x00100D9B File Offset: 0x000FEF9B
	public bool HasDestinationControllerId()
	{
		return this.m_destinationControllerId != null;
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x00100DA8 File Offset: 0x000FEFA8
	public bool HasDestinationData()
	{
		return this.HasDestinationZoneTag() || this.HasDestinationPosition() || this.HasDestinationControllerId();
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x00100DC9 File Offset: 0x000FEFC9
	public bool HasDestinationZoneChange()
	{
		return this.HasDestinationZoneTag() || this.HasDestinationControllerId();
	}

	// Token: 0x0600324C RID: 12876 RVA: 0x00100DE0 File Offset: 0x000FEFE0
	public override string ToString()
	{
		return string.Format("powerTask=[{0}] entity={1} srcZoneTag={2} srcPos={3} dstZoneTag={4} dstPos={5}", new object[]
		{
			this.m_powerTask,
			this.m_entity,
			this.m_sourceZoneTag,
			this.m_sourcePos,
			this.m_destinationZoneTag,
			this.m_destinationPos
		});
	}

	// Token: 0x04001BDD RID: 7133
	private ZoneChangeList m_parentList;

	// Token: 0x04001BDE RID: 7134
	private PowerTask m_powerTask;

	// Token: 0x04001BDF RID: 7135
	private Entity m_entity;

	// Token: 0x04001BE0 RID: 7136
	private Zone m_sourceZone;

	// Token: 0x04001BE1 RID: 7137
	private TAG_ZONE m_sourceZoneTag;

	// Token: 0x04001BE2 RID: 7138
	private int? m_sourcePos;

	// Token: 0x04001BE3 RID: 7139
	private int? m_sourceControllerId;

	// Token: 0x04001BE4 RID: 7140
	private Zone m_destinationZone;

	// Token: 0x04001BE5 RID: 7141
	private TAG_ZONE m_destinationZoneTag;

	// Token: 0x04001BE6 RID: 7142
	private int? m_destinationPos;

	// Token: 0x04001BE7 RID: 7143
	private int? m_destinationControllerId;
}
