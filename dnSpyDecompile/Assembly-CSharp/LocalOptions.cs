using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using Hearthstone.Core.Streaming;
using UnityEngine;

// Token: 0x020009BF RID: 2495
public class LocalOptions
{
	// Token: 0x06008836 RID: 34870 RVA: 0x002BDF6C File Offset: 0x002BC16C
	public static LocalOptions Get()
	{
		if (LocalOptions.s_instance == null)
		{
			LocalOptions.s_instance = new LocalOptions();
		}
		return LocalOptions.s_instance;
	}

	// Token: 0x06008837 RID: 34871 RVA: 0x002BDF84 File Offset: 0x002BC184
	public static void Reset()
	{
		LocalOptions.s_instance = new LocalOptions();
	}

	// Token: 0x17000798 RID: 1944
	// (get) Token: 0x06008838 RID: 34872 RVA: 0x002BDF90 File Offset: 0x002BC190
	public static string OptionsPath
	{
		get
		{
			string text = string.Format("{0}/{1}", FileUtils.ExternalDataPath, Vars.GetOptionsFileName());
			if (!File.Exists(text))
			{
				text = string.Format("{0}/{1}", FileUtils.PersistentDataPath, Vars.GetOptionsFileName());
			}
			return text;
		}
	}

	// Token: 0x06008839 RID: 34873 RVA: 0x002BDFD0 File Offset: 0x002BC1D0
	public void Initialize()
	{
		this.m_path = LocalOptions.OptionsPath;
		this.m_currentLineVersion = 2;
		if (this.Load())
		{
			OptionsMigration.UpgradeClientOptions();
		}
		LaunchArguments.AddEnabledLogInOptions(null);
	}

	// Token: 0x0600883A RID: 34874 RVA: 0x002BDFF8 File Offset: 0x002BC1F8
	public void Clear()
	{
		this.m_dirty = false;
		this.m_options.Clear();
		this.m_sortedKeys.Clear();
	}

	// Token: 0x0600883B RID: 34875 RVA: 0x002BE017 File Offset: 0x002BC217
	public bool Has(string key)
	{
		return this.m_options.ContainsKey(key);
	}

	// Token: 0x0600883C RID: 34876 RVA: 0x002BE025 File Offset: 0x002BC225
	public void Delete(string key)
	{
		if (!this.m_options.Remove(key))
		{
			return;
		}
		this.m_sortedKeys.Remove(key);
		this.m_dirty = true;
		this.Save(key);
	}

	// Token: 0x0600883D RID: 34877 RVA: 0x002BE054 File Offset: 0x002BC254
	public T Get<T>(string key)
	{
		object obj;
		if (!this.m_options.TryGetValue(key, out obj))
		{
			return default(T);
		}
		return (T)((object)obj);
	}

	// Token: 0x0600883E RID: 34878 RVA: 0x002BE081 File Offset: 0x002BC281
	public bool GetBool(string key)
	{
		return this.Get<bool>(key);
	}

	// Token: 0x0600883F RID: 34879 RVA: 0x002BE08A File Offset: 0x002BC28A
	public int GetInt(string key)
	{
		return this.Get<int>(key);
	}

	// Token: 0x06008840 RID: 34880 RVA: 0x002BE093 File Offset: 0x002BC293
	public long GetLong(string key)
	{
		return this.Get<long>(key);
	}

	// Token: 0x06008841 RID: 34881 RVA: 0x002BE09C File Offset: 0x002BC29C
	public ulong GetULong(string key)
	{
		return this.Get<ulong>(key);
	}

	// Token: 0x06008842 RID: 34882 RVA: 0x002BE0A5 File Offset: 0x002BC2A5
	public float GetFloat(string key)
	{
		return this.Get<float>(key);
	}

	// Token: 0x06008843 RID: 34883 RVA: 0x002BE0AE File Offset: 0x002BC2AE
	public string GetString(string key)
	{
		return this.Get<string>(key);
	}

	// Token: 0x06008844 RID: 34884 RVA: 0x002BE0B7 File Offset: 0x002BC2B7
	public void Set(string key, object val)
	{
		this.Set(key, val, true);
	}

	// Token: 0x06008845 RID: 34885 RVA: 0x002BE0C4 File Offset: 0x002BC2C4
	public void Set(string key, object val, bool permanent)
	{
		object obj;
		if (this.m_options.TryGetValue(key, out obj))
		{
			if (obj == val)
			{
				return;
			}
			if (obj != null && obj.Equals(val))
			{
				return;
			}
		}
		else
		{
			this.m_sortedKeys.Add(key);
			this.SortKeys();
		}
		this.m_options[key] = val;
		if (permanent)
		{
			this.m_temporaryKeys.Remove(key);
			this.m_dirty = true;
			this.Save(key);
			return;
		}
		this.m_temporaryKeys.Add(key);
	}

	// Token: 0x06008846 RID: 34886 RVA: 0x002BE140 File Offset: 0x002BC340
	public void SetByLine(string line, bool permanent)
	{
		int num;
		string key;
		object val;
		bool flag;
		if (!this.LoadLine(line, out num, out key, out val, out flag))
		{
			Log.ConfigFile.PrintError("LoadLine failed with '{0}'", new object[]
			{
				line
			});
			return;
		}
		this.Set(key, val, permanent);
	}

	// Token: 0x06008847 RID: 34887 RVA: 0x002BE184 File Offset: 0x002BC384
	private bool Load()
	{
		this.Clear();
		Log.ConfigFile.Print("Loading Options File: {0}", new object[]
		{
			this.m_path
		});
		if (!File.Exists(this.m_path))
		{
			this.m_loadResult = LocalOptions.LoadResult.SUCCESS;
			return true;
		}
		string[] lines;
		if (!this.LoadFile(out lines))
		{
			this.m_loadResult = LocalOptions.LoadResult.FAIL;
			return false;
		}
		bool flag = false;
		if (!this.LoadAllLines(lines, out flag))
		{
			this.m_loadResult = LocalOptions.LoadResult.FAIL;
			return false;
		}
		foreach (string item in this.m_options.Keys)
		{
			this.m_sortedKeys.Add(item);
		}
		this.SortKeys();
		this.m_loadResult = LocalOptions.LoadResult.SUCCESS;
		if (flag)
		{
			this.m_dirty = true;
			this.Save();
		}
		return true;
	}

	// Token: 0x06008848 RID: 34888 RVA: 0x002BE264 File Offset: 0x002BC464
	private bool LoadFile(out string[] lines)
	{
		bool result;
		try
		{
			lines = File.ReadAllLines(this.m_path);
			result = true;
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("LocalOptions.LoadFile() - Failed to read {0}. Exception={1}", this.m_path, ex.Message));
			lines = null;
			result = false;
		}
		return result;
	}

	// Token: 0x06008849 RID: 34889 RVA: 0x002BE2B8 File Offset: 0x002BC4B8
	private bool LoadAllLines(string[] lines, out bool formatChanged)
	{
		formatChanged = false;
		int num = 0;
		for (int i = 0; i < lines.Length; i++)
		{
			string text = lines[i];
			text = text.Trim();
			if (text.Length != 0 && !text.StartsWith("#"))
			{
				int num2;
				string key;
				object value;
				bool flag;
				if (!this.LoadLine(text, out num2, out key, out value, out flag))
				{
					Debug.LogError(string.Format("LocalOptions.LoadAllLines() - Failed to load line {0}\n\"{1}\".", i + 1, text));
					num++;
					if (num > 4)
					{
						this.m_loadResult = LocalOptions.LoadResult.FAIL;
						return false;
					}
				}
				else
				{
					this.m_options[key] = value;
					formatChanged = (formatChanged || num2 != this.m_currentLineVersion || flag);
				}
			}
		}
		return true;
	}

	// Token: 0x0600884A RID: 34890 RVA: 0x002BE360 File Offset: 0x002BC560
	private bool LoadLine(string line, out int version, out string key, out object val, out bool formatChanged)
	{
		version = 0;
		key = null;
		val = null;
		formatChanged = false;
		int num = 0;
		string text = null;
		string text2 = null;
		bool flag = false;
		string text3 = "=";
		line = line.Trim();
		string[] array = line.Split(new char[]
		{
			text3[0]
		});
		if (array.Length >= 2)
		{
			text = array[0].Trim();
			if (array.Length == 2)
			{
				text2 = array[1].Trim();
			}
			else
			{
				text2 = string.Join(text3, array.Slice(1)).Trim();
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				flag = true;
			}
			num = 2;
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			return false;
		}
		Option option = Option.INVALID;
		try
		{
			option = EnumUtils.GetEnum<Option>(text, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			version = num;
			key = text;
			val = text2;
			return true;
		}
		bool flag2 = false;
		int num2;
		Locale locale;
		if (option == Option.LOCALE && GeneralUtils.TryParseInt(text2, out num2) && EnumUtils.TryCast<Locale>(num2, out locale))
		{
			text2 = locale.ToString();
			flag2 = true;
		}
		Type left = OptionDataTables.s_typeMap[option];
		if (left == typeof(bool))
		{
			val = GeneralUtils.ForceBool(text2);
		}
		else if (left == typeof(int))
		{
			val = GeneralUtils.ForceInt(text2);
		}
		else if (left == typeof(long))
		{
			val = GeneralUtils.ForceLong(text2);
		}
		else if (left == typeof(ulong))
		{
			val = GeneralUtils.ForceULong(text2);
		}
		else if (left == typeof(float))
		{
			val = GeneralUtils.ForceFloat(text2);
		}
		else
		{
			if (!(left == typeof(string)))
			{
				return false;
			}
			val = text2;
		}
		version = num;
		key = text;
		formatChanged = flag2;
		return true;
	}

	// Token: 0x0600884B RID: 34891 RVA: 0x002BE554 File Offset: 0x002BC754
	private bool Save(string triggerKey)
	{
		LocalOptions.LoadResult loadResult = this.m_loadResult;
		return loadResult != LocalOptions.LoadResult.INVALID && loadResult != LocalOptions.LoadResult.FAIL && this.Save();
	}

	// Token: 0x0600884C RID: 34892 RVA: 0x002BE578 File Offset: 0x002BC778
	private bool Save()
	{
		if (!this.m_dirty)
		{
			return true;
		}
		List<string> list = new List<string>();
		for (int i = 0; i < this.m_sortedKeys.Count; i++)
		{
			string text = this.m_sortedKeys[i];
			if (!this.m_temporaryKeys.Contains(text))
			{
				object arg = this.m_options[text];
				string item = string.Format("{0}={1}", text, arg);
				list.Add(item);
			}
		}
		bool flag = this.WriteOptionsFile(string.Format("{0}/{1}", FileUtils.ExternalDataPath, Vars.GetOptionsFileName()), list);
		if (!flag)
		{
			flag = this.WriteOptionsFile(string.Format("{0}/{1}", FileUtils.PersistentDataPath, Vars.GetOptionsFileName()), list);
		}
		return flag;
	}

	// Token: 0x0600884D RID: 34893 RVA: 0x002BE628 File Offset: 0x002BC828
	private bool WriteOptionsFile(string optionsFilePath, List<string> lines)
	{
		try
		{
			File.WriteAllLines(optionsFilePath, lines.ToArray(), new UTF8Encoding());
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("LocalOptions.Save() - Failed to save {0}. Exception={1}", optionsFilePath, ex.Message));
			return false;
		}
		this.m_dirty = false;
		return true;
	}

	// Token: 0x0600884E RID: 34894 RVA: 0x002BE680 File Offset: 0x002BC880
	private void SortKeys()
	{
		this.m_sortedKeys.Sort(new Comparison<string>(this.KeyComparison));
	}

	// Token: 0x0600884F RID: 34895 RVA: 0x002BE699 File Offset: 0x002BC899
	private int KeyComparison(string key1, string key2)
	{
		return string.Compare(key1, key2, true);
	}

	// Token: 0x0400724F RID: 29263
	public const int MAX_OPTIONS_LINE_LENGTH = 4096;

	// Token: 0x04007250 RID: 29264
	private const int LOAD_LINE_FAIL_THRESHOLD = 4;

	// Token: 0x04007251 RID: 29265
	private static LocalOptions s_instance;

	// Token: 0x04007252 RID: 29266
	private string m_path;

	// Token: 0x04007253 RID: 29267
	private LocalOptions.LoadResult m_loadResult;

	// Token: 0x04007254 RID: 29268
	private int m_currentLineVersion;

	// Token: 0x04007255 RID: 29269
	private Map<string, object> m_options = new Map<string, object>();

	// Token: 0x04007256 RID: 29270
	private List<string> m_sortedKeys = new List<string>();

	// Token: 0x04007257 RID: 29271
	private List<string> m_temporaryKeys = new List<string>();

	// Token: 0x04007258 RID: 29272
	private bool m_dirty;

	// Token: 0x02002678 RID: 9848
	private enum LoadResult
	{
		// Token: 0x0400F0B0 RID: 61616
		INVALID,
		// Token: 0x0400F0B1 RID: 61617
		SUCCESS,
		// Token: 0x0400F0B2 RID: 61618
		FAIL
	}
}
