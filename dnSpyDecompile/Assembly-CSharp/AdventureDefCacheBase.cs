using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000033 RID: 51
public abstract class AdventureDefCacheBase<DefType, DefIDType, DbfRecord> where DefType : Component
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A828 File Offset: 0x00008A28
	public IEnumerable<DefType> Values
	{
		get
		{
			return this.m_defCache.Values;
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000A835 File Offset: 0x00008A35
	public AdventureDefCacheBase(bool preloadRecords)
	{
		if (preloadRecords)
		{
			this.LoadFromRecords(this.GetRecords());
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000A858 File Offset: 0x00008A58
	public DefType GetDef(DefIDType defId)
	{
		DefType result = default(DefType);
		this.m_defCache.TryGetValue(defId, out result);
		return result;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000A880 File Offset: 0x00008A80
	public bool LoadDefForId(DefIDType defId)
	{
		if (this.m_defCache.ContainsKey(defId))
		{
			Debug.LogWarningFormat("Attempted to load a {0} that was already loaded for id {1}", new object[]
			{
				typeof(DefType).Name,
				defId
			});
			return true;
		}
		DbfRecord recordForDefId = this.GetRecordForDefId(defId);
		if (recordForDefId != null && !string.IsNullOrEmpty(this.GetPrefabFromRecord(recordForDefId)))
		{
			DefType def = GameUtils.LoadGameObjectWithComponent<DefType>(this.GetPrefabFromRecord(recordForDefId));
			this.AddDef(def, recordForDefId);
			return true;
		}
		return false;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000A900 File Offset: 0x00008B00
	public void Unload()
	{
		foreach (KeyValuePair<DefIDType, DefType> keyValuePair in this.m_defCache)
		{
			if (!(keyValuePair.Value == null))
			{
				GameObject gameObject = keyValuePair.Value.gameObject;
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
		this.m_defCache.Clear();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000A98C File Offset: 0x00008B8C
	private void LoadFromRecords(List<DbfRecord> records)
	{
		if (this.m_defCache.Count > 0)
		{
			Debug.LogErrorFormat("Attempting to load all {0} when they were already loaded!", new object[]
			{
				typeof(DefType).Name
			});
		}
		foreach (DbfRecord record in records)
		{
			DefType defType = GameUtils.LoadGameObjectWithComponent<DefType>(this.GetPrefabFromRecord(record));
			if (!(defType == null))
			{
				this.AddDef(defType, record);
			}
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000AA28 File Offset: 0x00008C28
	private void AddDef(DefType def, DbfRecord record)
	{
		this.InitalizeDef(def, record);
		this.m_defCache.Add(this.GetDefId(def), def);
	}

	// Token: 0x060001D5 RID: 469
	protected abstract List<DbfRecord> GetRecords();

	// Token: 0x060001D6 RID: 470
	protected abstract string GetPrefabFromRecord(DbfRecord record);

	// Token: 0x060001D7 RID: 471
	protected abstract void InitalizeDef(DefType def, DbfRecord record);

	// Token: 0x060001D8 RID: 472
	protected abstract DefIDType GetDefId(DefType def);

	// Token: 0x060001D9 RID: 473
	protected abstract DbfRecord GetRecordForDefId(DefIDType id);

	// Token: 0x04000151 RID: 337
	private Map<DefIDType, DefType> m_defCache = new Map<DefIDType, DefType>();
}
