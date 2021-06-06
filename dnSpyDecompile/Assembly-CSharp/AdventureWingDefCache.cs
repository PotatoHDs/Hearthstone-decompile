using System;
using System.Collections.Generic;

// Token: 0x02000035 RID: 53
public class AdventureWingDefCache : AdventureDefCacheBase<AdventureWingDef, WingDbId, WingDbfRecord>
{
	// Token: 0x060001E0 RID: 480 RVA: 0x0000AA80 File Offset: 0x00008C80
	public AdventureWingDefCache(bool preloadRecords) : base(preloadRecords)
	{
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000AA89 File Offset: 0x00008C89
	protected override List<WingDbfRecord> GetRecords()
	{
		return GameDbf.Wing.GetRecords((WingDbfRecord r) => !string.IsNullOrEmpty(r.AdventureWingDefPrefab), -1);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000AAB5 File Offset: 0x00008CB5
	protected override string GetPrefabFromRecord(WingDbfRecord record)
	{
		return record.AdventureWingDefPrefab;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000AABD File Offset: 0x00008CBD
	protected override void InitalizeDef(AdventureWingDef def, WingDbfRecord record)
	{
		def.Init(record);
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000AAC6 File Offset: 0x00008CC6
	protected override WingDbId GetDefId(AdventureWingDef def)
	{
		return def.GetWingId();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000AACE File Offset: 0x00008CCE
	protected override WingDbfRecord GetRecordForDefId(WingDbId id)
	{
		return GameDbf.Wing.GetRecord((int)id);
	}
}
