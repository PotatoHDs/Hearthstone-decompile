using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
public class Dbf<T> where T : DbfRecord, new()
{
	// Token: 0x14000074 RID: 116
	// (add) Token: 0x06006D41 RID: 27969 RVA: 0x00233D88 File Offset: 0x00231F88
	// (remove) Token: 0x06006D42 RID: 27970 RVA: 0x00233DC0 File Offset: 0x00231FC0
	private event Dbf<T>.RecordAddedListener m_recordAddedListener;

	// Token: 0x14000075 RID: 117
	// (add) Token: 0x06006D43 RID: 27971 RVA: 0x00233DF8 File Offset: 0x00231FF8
	// (remove) Token: 0x06006D44 RID: 27972 RVA: 0x00233E30 File Offset: 0x00232030
	private event Dbf<T>.RecordsRemovedListener m_recordsRemovedListener;

	// Token: 0x06006D45 RID: 27973 RVA: 0x00233E65 File Offset: 0x00232065
	public Dbf(string name)
	{
		this.m_name = name;
	}

	// Token: 0x06006D46 RID: 27974 RVA: 0x00233E8A File Offset: 0x0023208A
	public void SetName(string name)
	{
		this.m_name = name;
	}

	// Token: 0x06006D47 RID: 27975 RVA: 0x00233E94 File Offset: 0x00232094
	public void CopyRecords(Dbf<T> other)
	{
		this.m_records.AddRange(other.m_records);
		foreach (KeyValuePair<int, T> keyValuePair in other.m_recordsById)
		{
			this.m_recordsById.Add(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x06006D48 RID: 27976 RVA: 0x00233F0C File Offset: 0x0023210C
	public void AddListeners(Dbf<T>.RecordAddedListener addedListener, Dbf<T>.RecordsRemovedListener removedListener)
	{
		if (addedListener != null)
		{
			this.m_recordAddedListener += addedListener;
		}
		if (removedListener != null)
		{
			this.m_recordsRemovedListener += removedListener;
		}
	}

	// Token: 0x06006D49 RID: 27977 RVA: 0x00233F22 File Offset: 0x00232122
	public DbfRecord CreateNewRecord()
	{
		return Activator.CreateInstance<T>();
	}

	// Token: 0x06006D4A RID: 27978 RVA: 0x00233F30 File Offset: 0x00232130
	public void AddRecord(DbfRecord record)
	{
		T t = (T)((object)record);
		this.m_records.Add(t);
		this.m_recordsById[record.ID] = t;
		if (this.m_recordAddedListener != null)
		{
			this.m_recordAddedListener(t);
		}
	}

	// Token: 0x06006D4B RID: 27979 RVA: 0x00233F76 File Offset: 0x00232176
	public Type GetRecordType()
	{
		return typeof(T);
	}

	// Token: 0x06006D4C RID: 27980 RVA: 0x00233F82 File Offset: 0x00232182
	public List<T> GetRecords()
	{
		return this.m_records;
	}

	// Token: 0x06006D4D RID: 27981 RVA: 0x00233F8A File Offset: 0x0023218A
	public List<T> GetRecords(Predicate<T> predicate, int limit = -1)
	{
		if (limit >= 0)
		{
			return this.m_records.FindAll(predicate).GetRange(0, limit);
		}
		return this.m_records.FindAll(predicate);
	}

	// Token: 0x06006D4E RID: 27982 RVA: 0x00233FB0 File Offset: 0x002321B0
	public static Dbf<T> Load(string name, DbfFormat format)
	{
		string assetPath = (format == DbfFormat.XML) ? Dbf<T>.GetXmlPath(name) : Dbf<T>.GetAssetPath(name);
		return Dbf<T>.Load(name, assetPath, format);
	}

	// Token: 0x06006D4F RID: 27983 RVA: 0x00233FD8 File Offset: 0x002321D8
	public static Dbf<T> Load(string name, string assetPath, DbfFormat format)
	{
		Dbf<T> dbf = new Dbf<T>(name);
		dbf.Clear();
		bool flag = format == DbfFormat.XML;
		bool flag2;
		if (flag)
		{
			flag2 = DbfXml.Load<T>(assetPath, dbf);
		}
		else
		{
			flag2 = dbf.LoadScriptableObject(assetPath);
		}
		if (!flag2)
		{
			dbf.Clear();
			Log.Dbf.Print(string.Format("Dbf.Load[{0}] - failed to load {1} at {2}", flag ? "Xml" : "ScriptableObject", name, assetPath), Array.Empty<object>());
		}
		return dbf;
	}

	// Token: 0x06006D50 RID: 27984 RVA: 0x00234044 File Offset: 0x00232244
	public static JobDefinition CreateLoadAsyncJob(string name, DbfFormat format, ref Dbf<T> dbf)
	{
		string assetPath = (format == DbfFormat.XML) ? Dbf<T>.GetXmlPath(name) : Dbf<T>.GetAssetPath(name);
		return Dbf<T>.CreateLoadAsyncJob(name, assetPath, format, ref dbf);
	}

	// Token: 0x06006D51 RID: 27985 RVA: 0x00234070 File Offset: 0x00232270
	public static JobDefinition CreateLoadAsyncJob(string name, string assetPath, DbfFormat format, ref Dbf<T> dbf)
	{
		dbf = new Dbf<T>(name);
		dbf.Clear();
		JobDefinition result;
		if (format == DbfFormat.XML)
		{
			result = new JobDefinition(Dbf<T>.MakeJobName(typeof(T)), DbfXml.Job_LoadAsync<T>(assetPath, dbf), JobFlags.StartImmediately, Array.Empty<IJobDependency>());
		}
		else
		{
			result = new JobDefinition(Dbf<T>.MakeJobName(typeof(T)), dbf.Job_LoadScriptableObjectAsync(assetPath), Array.Empty<IJobDependency>());
		}
		return result;
	}

	// Token: 0x06006D52 RID: 27986 RVA: 0x002340DA File Offset: 0x002322DA
	private static string MakeJobName(Type t)
	{
		return string.Format("Dbf.LoadAsync[{0}]", t.ToString());
	}

	// Token: 0x06006D53 RID: 27987 RVA: 0x002340EC File Offset: 0x002322EC
	public string GetName()
	{
		return this.m_name;
	}

	// Token: 0x06006D54 RID: 27988 RVA: 0x002340F4 File Offset: 0x002322F4
	public void Clear()
	{
		this.m_records.Clear();
		this.m_recordsById.Clear();
	}

	// Token: 0x06006D55 RID: 27989 RVA: 0x0023410C File Offset: 0x0023230C
	public T GetRecord(int id)
	{
		T result;
		if (this.m_recordsById.TryGetValue(id, out result))
		{
			return result;
		}
		return this.m_records.Find((T r) => r.ID == id);
	}

	// Token: 0x06006D56 RID: 27990 RVA: 0x00234154 File Offset: 0x00232354
	public T GetRecord(Predicate<T> match)
	{
		return this.m_records.Find(match);
	}

	// Token: 0x06006D57 RID: 27991 RVA: 0x00234164 File Offset: 0x00232364
	public bool HasRecord(int id)
	{
		T t = default(T);
		if (!this.m_recordsById.TryGetValue(id, out t))
		{
			t = this.m_records.Find((T r) => r.ID == id);
		}
		return t != null;
	}

	// Token: 0x06006D58 RID: 27992 RVA: 0x002341BC File Offset: 0x002323BC
	public bool HasRecord(Predicate<T> match)
	{
		return this.GetRecord(match) != null;
	}

	// Token: 0x06006D59 RID: 27993 RVA: 0x002341D0 File Offset: 0x002323D0
	public void ReplaceRecordByRecordId(T record)
	{
		int num = this.m_records.FindIndex((T r) => r.ID == record.ID);
		if (num == -1)
		{
			this.AddRecord(record);
			return;
		}
		T t = this.m_records[num];
		bool flag = t != record;
		if (flag && this.m_recordsRemovedListener != null)
		{
			List<T> list = new List<T>();
			list.Add(t);
			this.m_recordsRemovedListener(list);
		}
		this.m_records[num] = record;
		this.m_recordsById[record.ID] = record;
		if (flag && this.m_recordAddedListener != null)
		{
			this.m_recordAddedListener(record);
		}
	}

	// Token: 0x06006D5A RID: 27994 RVA: 0x002342B0 File Offset: 0x002324B0
	public void RemoveRecordsWhere(Predicate<T> match)
	{
		List<int> list = null;
		for (int i = 0; i < this.m_records.Count; i++)
		{
			if (match(this.m_records[i]))
			{
				if (list == null)
				{
					list = new List<int>();
				}
				list.Add(i);
			}
		}
		if (list != null)
		{
			List<T> list2 = null;
			if (this.m_recordsRemovedListener != null)
			{
				list2 = new List<T>(list.Count);
			}
			for (int j = list.Count - 1; j >= 0; j--)
			{
				int index = list[j];
				T t = this.m_records[index];
				if (list2 != null && t != null)
				{
					list2.Add(t);
				}
				T t2;
				if (this.m_recordsById.TryGetValue(t.ID, out t2))
				{
					this.m_recordsById.Remove(t2.ID);
				}
				this.m_records.RemoveAt(index);
			}
			if (this.m_recordsRemovedListener != null)
			{
				this.m_recordsRemovedListener(list2);
			}
		}
	}

	// Token: 0x06006D5B RID: 27995 RVA: 0x002340EC File Offset: 0x002322EC
	public override string ToString()
	{
		return this.m_name;
	}

	// Token: 0x06006D5C RID: 27996 RVA: 0x002343A8 File Offset: 0x002325A8
	private static string GetXmlPath(string name)
	{
		string text = string.Format("UnimportedAssets/DBF/{0}.xml", name);
		string result;
		if (HearthstoneApplication.TryGetStandaloneLocalDataPath(text, out result))
		{
			return result;
		}
		if (!Application.isEditor)
		{
			text = string.Format("DBF/{0}.xml", name);
		}
		return text;
	}

	// Token: 0x06006D5D RID: 27997 RVA: 0x002343E1 File Offset: 0x002325E1
	private static string GetAssetPath(string name)
	{
		return string.Format("Assets/Game/DBF-Asset/{0}.asset", name);
	}

	// Token: 0x06006D5E RID: 27998 RVA: 0x002343F0 File Offset: 0x002325F0
	public bool LoadScriptableObject(string resourcePath)
	{
		if (!Activator.CreateInstance<T>().LoadRecordsFromAsset<T>(resourcePath, out this.m_records))
		{
			return false;
		}
		if (this.m_records.Count < 1 && this.m_name != "SUBSET_CARD")
		{
			Debug.LogErrorFormat("{0} DBF Asset has 0 records! Something went wrong generating it. Try checking the generated XMLs in the DBF folder.", new object[]
			{
				this.m_name
			});
		}
		for (int i = 0; i < this.m_records.Count; i++)
		{
			this.m_recordsById[this.m_records[i].ID] = this.m_records[i];
			if (this.m_recordAddedListener != null)
			{
				this.m_recordAddedListener(this.m_records[i]);
			}
		}
		return true;
	}

	// Token: 0x06006D5F RID: 27999 RVA: 0x002344B4 File Offset: 0x002326B4
	public IEnumerator<IAsyncJobResult> Job_LoadScriptableObjectAsync(string resourcePath)
	{
		Action<List<T>> resultHandler = delegate(List<T> records)
		{
			this.m_records = (records ?? new List<T>());
			if (this.m_records.Count < 1 && this.m_name != "SUBSET_CARD")
			{
				Debug.LogErrorFormat("{0} DBF Asset has 0 records! Something went wrong generating it. Try checking the generated XMLs in the DBF folder.", new object[]
				{
					this.m_name
				});
			}
			for (int i = 0; i < this.m_records.Count; i++)
			{
				this.m_recordsById[this.m_records[i].ID] = this.m_records[i];
				if (this.m_recordAddedListener != null)
				{
					this.m_recordAddedListener(this.m_records[i]);
				}
			}
		};
		return Activator.CreateInstance<T>().Job_LoadRecordsFromAssetAsync<T>(resourcePath, resultHandler);
	}

	// Token: 0x06006D60 RID: 28000 RVA: 0x002344E0 File Offset: 0x002326E0
	public string SaveScriptableObject()
	{
		string assetPath = Dbf<T>.GetAssetPath(this.m_name);
		if (Activator.CreateInstance<T>().SaveRecordsToAsset<T>(assetPath, this.m_records))
		{
			return assetPath;
		}
		return null;
	}

	// Token: 0x040057EC RID: 22508
	private string m_name;

	// Token: 0x040057ED RID: 22509
	private List<T> m_records = new List<T>();

	// Token: 0x040057EE RID: 22510
	private Map<int, T> m_recordsById = new Map<int, T>();

	// Token: 0x02002359 RID: 9049
	// (Invoke) Token: 0x06012AD1 RID: 76497
	public delegate void RecordAddedListener(T record);

	// Token: 0x0200235A RID: 9050
	// (Invoke) Token: 0x06012AD5 RID: 76501
	public delegate void RecordsRemovedListener(List<T> removedRecords);
}
