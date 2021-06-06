using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B9 RID: 441
[Serializable]
public class ClassDbfRecord : DbfRecord
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0008A53A File Offset: 0x0008873A
	[DbfField("ASSET_FLAGS")]
	public Class.AssetFlags AssetFlags
	{
		get
		{
			return this.m_assetFlags;
		}
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x0008A542 File Offset: 0x00088742
	public void SetAssetFlags(Class.AssetFlags v)
	{
		this.m_assetFlags = v;
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x0008A54B File Offset: 0x0008874B
	public override object GetVar(string name)
	{
		if (name == "ASSET_FLAGS")
		{
			return this.m_assetFlags;
		}
		return null;
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0008A568 File Offset: 0x00088768
	public override void SetVar(string name, object val)
	{
		if (name == "ASSET_FLAGS")
		{
			if (val == null)
			{
				this.m_assetFlags = Class.AssetFlags.NONE;
				return;
			}
			if (val is Class.AssetFlags || val is int)
			{
				this.m_assetFlags = (Class.AssetFlags)val;
				return;
			}
			if (val is string)
			{
				this.m_assetFlags = Class.ParseAssetFlagsValue((string)val);
			}
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x0008A5C3 File Offset: 0x000887C3
	public override Type GetVarType(string name)
	{
		if (name == "ASSET_FLAGS")
		{
			return typeof(Class.AssetFlags);
		}
		return null;
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x0008A5DE File Offset: 0x000887DE
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadClassDbfRecords loadRecords = new LoadClassDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0008A5F4 File Offset: 0x000887F4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ClassDbfAsset classDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ClassDbfAsset)) as ClassDbfAsset;
		if (classDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ClassDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < classDbfAsset.Records.Count; i++)
		{
			classDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (classDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FCB RID: 4043
	[SerializeField]
	private Class.AssetFlags m_assetFlags;
}
