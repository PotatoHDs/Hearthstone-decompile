using System;
using System.Collections.Generic;

// Token: 0x02000034 RID: 52
public class AdventureDefCache : AdventureDefCacheBase<AdventureDef, AdventureDbId, AdventureDbfRecord>
{
	// Token: 0x060001DA RID: 474 RVA: 0x0000AA45 File Offset: 0x00008C45
	public AdventureDefCache(bool preloadRecords) : base(preloadRecords)
	{
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000AA4E File Offset: 0x00008C4E
	protected override List<AdventureDbfRecord> GetRecords()
	{
		return GameUtils.GetAdventureRecordsWithDefPrefab();
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000AA55 File Offset: 0x00008C55
	protected override string GetPrefabFromRecord(AdventureDbfRecord record)
	{
		return record.AdventureDefPrefab;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000AA5D File Offset: 0x00008C5D
	protected override void InitalizeDef(AdventureDef def, AdventureDbfRecord record)
	{
		def.Init(record, GameUtils.GetAdventureDataRecordsWithSubDefPrefab());
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000AA6B File Offset: 0x00008C6B
	protected override AdventureDbId GetDefId(AdventureDef def)
	{
		return def.GetAdventureId();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000AA73 File Offset: 0x00008C73
	protected override AdventureDbfRecord GetRecordForDefId(AdventureDbId id)
	{
		return GameDbf.Adventure.GetRecord((int)id);
	}
}
