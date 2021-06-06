using System.Collections.Generic;

public class AdventureDefCache : AdventureDefCacheBase<AdventureDef, AdventureDbId, AdventureDbfRecord>
{
	public AdventureDefCache(bool preloadRecords)
		: base(preloadRecords)
	{
	}

	protected override List<AdventureDbfRecord> GetRecords()
	{
		return GameUtils.GetAdventureRecordsWithDefPrefab();
	}

	protected override string GetPrefabFromRecord(AdventureDbfRecord record)
	{
		return record.AdventureDefPrefab;
	}

	protected override void InitalizeDef(AdventureDef def, AdventureDbfRecord record)
	{
		def.Init(record, GameUtils.GetAdventureDataRecordsWithSubDefPrefab());
	}

	protected override AdventureDbId GetDefId(AdventureDef def)
	{
		return def.GetAdventureId();
	}

	protected override AdventureDbfRecord GetRecordForDefId(AdventureDbId id)
	{
		return GameDbf.Adventure.GetRecord((int)id);
	}
}
