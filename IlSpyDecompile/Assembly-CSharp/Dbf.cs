using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone;
using UnityEngine;

public class Dbf<T> where T : DbfRecord, new()
{
	public delegate void RecordAddedListener(T record);

	public delegate void RecordsRemovedListener(List<T> removedRecords);

	private string m_name;

	private List<T> m_records = new List<T>();

	private Map<int, T> m_recordsById = new Map<int, T>();

	private event RecordAddedListener m_recordAddedListener;

	private event RecordsRemovedListener m_recordsRemovedListener;

	public Dbf(string name)
	{
		m_name = name;
	}

	public void SetName(string name)
	{
		m_name = name;
	}

	public void CopyRecords(Dbf<T> other)
	{
		m_records.AddRange(other.m_records);
		foreach (KeyValuePair<int, T> item in other.m_recordsById)
		{
			m_recordsById.Add(item.Key, item.Value);
		}
	}

	public void AddListeners(RecordAddedListener addedListener, RecordsRemovedListener removedListener)
	{
		if (addedListener != null)
		{
			m_recordAddedListener += addedListener;
		}
		if (removedListener != null)
		{
			m_recordsRemovedListener += removedListener;
		}
	}

	public DbfRecord CreateNewRecord()
	{
		return new T();
	}

	public void AddRecord(DbfRecord record)
	{
		T val = (T)record;
		m_records.Add(val);
		m_recordsById[record.ID] = val;
		if (this.m_recordAddedListener != null)
		{
			this.m_recordAddedListener(val);
		}
	}

	public Type GetRecordType()
	{
		return typeof(T);
	}

	public List<T> GetRecords()
	{
		return m_records;
	}

	public List<T> GetRecords(Predicate<T> predicate, int limit = -1)
	{
		if (limit >= 0)
		{
			return m_records.FindAll(predicate).GetRange(0, limit);
		}
		return m_records.FindAll(predicate);
	}

	public static Dbf<T> Load(string name, DbfFormat format)
	{
		string assetPath = ((format == DbfFormat.XML) ? GetXmlPath(name) : GetAssetPath(name));
		return Load(name, assetPath, format);
	}

	public static Dbf<T> Load(string name, string assetPath, DbfFormat format)
	{
		Dbf<T> dbf = new Dbf<T>(name);
		dbf.Clear();
		bool flag = false;
		bool flag2 = format == DbfFormat.XML;
		if (!((!flag2) ? dbf.LoadScriptableObject(assetPath) : DbfXml.Load(assetPath, dbf)))
		{
			dbf.Clear();
			Log.Dbf.Print(string.Format("Dbf.Load[{0}] - failed to load {1} at {2}", flag2 ? "Xml" : "ScriptableObject", name, assetPath));
		}
		return dbf;
	}

	public static JobDefinition CreateLoadAsyncJob(string name, DbfFormat format, ref Dbf<T> dbf)
	{
		string assetPath = ((format == DbfFormat.XML) ? GetXmlPath(name) : GetAssetPath(name));
		return CreateLoadAsyncJob(name, assetPath, format, ref dbf);
	}

	public static JobDefinition CreateLoadAsyncJob(string name, string assetPath, DbfFormat format, ref Dbf<T> dbf)
	{
		dbf = new Dbf<T>(name);
		dbf.Clear();
		if (format == DbfFormat.XML)
		{
			return new JobDefinition(MakeJobName(typeof(T)), DbfXml.Job_LoadAsync(assetPath, dbf), JobFlags.StartImmediately);
		}
		return new JobDefinition(MakeJobName(typeof(T)), dbf.Job_LoadScriptableObjectAsync(assetPath));
	}

	private static string MakeJobName(Type t)
	{
		return $"Dbf.LoadAsync[{t.ToString()}]";
	}

	public string GetName()
	{
		return m_name;
	}

	public void Clear()
	{
		m_records.Clear();
		m_recordsById.Clear();
	}

	public T GetRecord(int id)
	{
		if (m_recordsById.TryGetValue(id, out var value))
		{
			return value;
		}
		return m_records.Find((T r) => r.ID == id);
	}

	public T GetRecord(Predicate<T> match)
	{
		return m_records.Find(match);
	}

	public bool HasRecord(int id)
	{
		T value = null;
		if (!m_recordsById.TryGetValue(id, out value))
		{
			value = m_records.Find((T r) => r.ID == id);
		}
		return value != null;
	}

	public bool HasRecord(Predicate<T> match)
	{
		return GetRecord(match) != null;
	}

	public void ReplaceRecordByRecordId(T record)
	{
		int num = m_records.FindIndex((T r) => r.ID == record.ID);
		if (num == -1)
		{
			AddRecord(record);
			return;
		}
		T val = m_records[num];
		bool num2 = val != record;
		if (num2 && this.m_recordsRemovedListener != null)
		{
			List<T> removedRecords = new List<T> { val };
			this.m_recordsRemovedListener(removedRecords);
		}
		m_records[num] = record;
		m_recordsById[record.ID] = record;
		if (num2 && this.m_recordAddedListener != null)
		{
			this.m_recordAddedListener(record);
		}
	}

	public void RemoveRecordsWhere(Predicate<T> match)
	{
		List<int> list = null;
		for (int i = 0; i < m_records.Count; i++)
		{
			if (match(m_records[i]))
			{
				if (list == null)
				{
					list = new List<int>();
				}
				list.Add(i);
			}
		}
		if (list == null)
		{
			return;
		}
		List<T> list2 = null;
		if (this.m_recordsRemovedListener != null)
		{
			list2 = new List<T>(list.Count);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			int index = list[num];
			T val = m_records[index];
			if (list2 != null && val != null)
			{
				list2.Add(val);
			}
			if (m_recordsById.TryGetValue(val.ID, out var value))
			{
				m_recordsById.Remove(value.ID);
			}
			m_records.RemoveAt(index);
		}
		if (this.m_recordsRemovedListener != null)
		{
			this.m_recordsRemovedListener(list2);
		}
	}

	public override string ToString()
	{
		return m_name;
	}

	private static string GetXmlPath(string name)
	{
		string text = $"UnimportedAssets/DBF/{name}.xml";
		if (HearthstoneApplication.TryGetStandaloneLocalDataPath(text, out var outPath))
		{
			return outPath;
		}
		if (!Application.isEditor)
		{
			text = $"DBF/{name}.xml";
		}
		return text;
	}

	private static string GetAssetPath(string name)
	{
		return $"Assets/Game/DBF-Asset/{name}.asset";
	}

	public bool LoadScriptableObject(string resourcePath)
	{
		if (!new T().LoadRecordsFromAsset(resourcePath, out m_records))
		{
			return false;
		}
		if (m_records.Count < 1 && m_name != "SUBSET_CARD")
		{
			Debug.LogErrorFormat("{0} DBF Asset has 0 records! Something went wrong generating it. Try checking the generated XMLs in the DBF folder.", m_name);
		}
		for (int i = 0; i < m_records.Count; i++)
		{
			m_recordsById[m_records[i].ID] = m_records[i];
			if (this.m_recordAddedListener != null)
			{
				this.m_recordAddedListener(m_records[i]);
			}
		}
		return true;
	}

	public IEnumerator<IAsyncJobResult> Job_LoadScriptableObjectAsync(string resourcePath)
	{
		Action<List<T>> resultHandler = delegate(List<T> records)
		{
			m_records = records ?? new List<T>();
			if (m_records.Count < 1 && m_name != "SUBSET_CARD")
			{
				Debug.LogErrorFormat("{0} DBF Asset has 0 records! Something went wrong generating it. Try checking the generated XMLs in the DBF folder.", m_name);
			}
			for (int i = 0; i < m_records.Count; i++)
			{
				m_recordsById[m_records[i].ID] = m_records[i];
				if (this.m_recordAddedListener != null)
				{
					this.m_recordAddedListener(m_records[i]);
				}
			}
		};
		return new T().Job_LoadRecordsFromAssetAsync(resourcePath, resultHandler);
	}

	public string SaveScriptableObject()
	{
		string assetPath = GetAssetPath(m_name);
		if (new T().SaveRecordsToAsset(assetPath, m_records))
		{
			return assetPath;
		}
		return null;
	}
}
