using System.Collections.Generic;

public class AdventureWingDefCache : AdventureDefCacheBase<AdventureWingDef, WingDbId, WingDbfRecord>
{
	public AdventureWingDefCache(bool preloadRecords)
		: base(preloadRecords)
	{
	}

	protected override List<WingDbfRecord> GetRecords()
	{
		return GameDbf.Wing.GetRecords((WingDbfRecord r) => !string.IsNullOrEmpty(r.AdventureWingDefPrefab));
	}

	protected override string GetPrefabFromRecord(WingDbfRecord record)
	{
		return record.AdventureWingDefPrefab;
	}

	protected override void InitalizeDef(AdventureWingDef def, WingDbfRecord record)
	{
		def.Init(record);
	}

	protected override WingDbId GetDefId(AdventureWingDef def)
	{
		return def.GetWingId();
	}

	protected override WingDbfRecord GetRecordForDefId(WingDbId id)
	{
		return GameDbf.Wing.GetRecord((int)id);
	}
}
