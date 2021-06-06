using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using Hearthstone.Core.Streaming;
using UnityEngine;

public class LocalOptions
{
	private enum LoadResult
	{
		INVALID,
		SUCCESS,
		FAIL
	}

	public const int MAX_OPTIONS_LINE_LENGTH = 4096;

	private const int LOAD_LINE_FAIL_THRESHOLD = 4;

	private static LocalOptions s_instance;

	private string m_path;

	private LoadResult m_loadResult;

	private int m_currentLineVersion;

	private Map<string, object> m_options = new Map<string, object>();

	private List<string> m_sortedKeys = new List<string>();

	private List<string> m_temporaryKeys = new List<string>();

	private bool m_dirty;

	public static string OptionsPath
	{
		get
		{
			string text = $"{FileUtils.ExternalDataPath}/{Vars.GetOptionsFileName()}";
			if (!File.Exists(text))
			{
				text = $"{FileUtils.PersistentDataPath}/{Vars.GetOptionsFileName()}";
			}
			return text;
		}
	}

	public static LocalOptions Get()
	{
		if (s_instance == null)
		{
			s_instance = new LocalOptions();
		}
		return s_instance;
	}

	public static void Reset()
	{
		s_instance = new LocalOptions();
	}

	public void Initialize()
	{
		m_path = OptionsPath;
		m_currentLineVersion = 2;
		if (Load())
		{
			OptionsMigration.UpgradeClientOptions();
		}
		LaunchArguments.AddEnabledLogInOptions(null);
	}

	public void Clear()
	{
		m_dirty = false;
		m_options.Clear();
		m_sortedKeys.Clear();
	}

	public bool Has(string key)
	{
		return m_options.ContainsKey(key);
	}

	public void Delete(string key)
	{
		if (m_options.Remove(key))
		{
			m_sortedKeys.Remove(key);
			m_dirty = true;
			Save(key);
		}
	}

	public T Get<T>(string key)
	{
		if (!m_options.TryGetValue(key, out var value))
		{
			return default(T);
		}
		return (T)value;
	}

	public bool GetBool(string key)
	{
		return Get<bool>(key);
	}

	public int GetInt(string key)
	{
		return Get<int>(key);
	}

	public long GetLong(string key)
	{
		return Get<long>(key);
	}

	public ulong GetULong(string key)
	{
		return Get<ulong>(key);
	}

	public float GetFloat(string key)
	{
		return Get<float>(key);
	}

	public string GetString(string key)
	{
		return Get<string>(key);
	}

	public void Set(string key, object val)
	{
		Set(key, val, permanent: true);
	}

	public void Set(string key, object val, bool permanent)
	{
		if (m_options.TryGetValue(key, out var value))
		{
			if (value == val || (value != null && value.Equals(val)))
			{
				return;
			}
		}
		else
		{
			m_sortedKeys.Add(key);
			SortKeys();
		}
		m_options[key] = val;
		if (permanent)
		{
			m_temporaryKeys.Remove(key);
			m_dirty = true;
			Save(key);
		}
		else
		{
			m_temporaryKeys.Add(key);
		}
	}

	public void SetByLine(string line, bool permanent)
	{
		if (!LoadLine(line, out var _, out var key, out var val, out var _))
		{
			Log.ConfigFile.PrintError("LoadLine failed with '{0}'", line);
		}
		else
		{
			Set(key, val, permanent);
		}
	}

	private bool Load()
	{
		Clear();
		Log.ConfigFile.Print("Loading Options File: {0}", m_path);
		if (!File.Exists(m_path))
		{
			m_loadResult = LoadResult.SUCCESS;
			return true;
		}
		if (!LoadFile(out var lines))
		{
			m_loadResult = LoadResult.FAIL;
			return false;
		}
		bool formatChanged = false;
		if (!LoadAllLines(lines, out formatChanged))
		{
			m_loadResult = LoadResult.FAIL;
			return false;
		}
		foreach (string key in m_options.Keys)
		{
			m_sortedKeys.Add(key);
		}
		SortKeys();
		m_loadResult = LoadResult.SUCCESS;
		if (formatChanged)
		{
			m_dirty = true;
			Save();
		}
		return true;
	}

	private bool LoadFile(out string[] lines)
	{
		try
		{
			lines = File.ReadAllLines(m_path);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogError($"LocalOptions.LoadFile() - Failed to read {m_path}. Exception={ex.Message}");
			lines = null;
			return false;
		}
	}

	private bool LoadAllLines(string[] lines, out bool formatChanged)
	{
		formatChanged = false;
		int num = 0;
		for (int i = 0; i < lines.Length; i++)
		{
			string text = lines[i];
			text = text.Trim();
			if (text.Length == 0 || text.StartsWith("#"))
			{
				continue;
			}
			if (!LoadLine(text, out var version, out var key, out var val, out var formatChanged2))
			{
				Debug.LogError($"LocalOptions.LoadAllLines() - Failed to load line {i + 1}\n\"{text}\".");
				num++;
				if (num > 4)
				{
					m_loadResult = LoadResult.FAIL;
					return false;
				}
			}
			else
			{
				m_options[key] = val;
				formatChanged = formatChanged || version != m_currentLineVersion || formatChanged2;
			}
		}
		return true;
	}

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
		string[] array = line.Split(text3[0]);
		if (array.Length >= 2)
		{
			text = array[0].Trim();
			text2 = ((array.Length != 2) ? string.Join(text3, array.Slice(1)).Trim() : array[1].Trim());
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
		if (option == Option.LOCALE && GeneralUtils.TryParseInt(text2, out var val2) && EnumUtils.TryCast<Locale>(val2, out var outVal))
		{
			text2 = outVal.ToString();
			flag2 = true;
		}
		Type type = OptionDataTables.s_typeMap[option];
		if (type == typeof(bool))
		{
			val = GeneralUtils.ForceBool(text2);
		}
		else if (type == typeof(int))
		{
			val = GeneralUtils.ForceInt(text2);
		}
		else if (type == typeof(long))
		{
			val = GeneralUtils.ForceLong(text2);
		}
		else if (type == typeof(ulong))
		{
			val = GeneralUtils.ForceULong(text2);
		}
		else if (type == typeof(float))
		{
			val = GeneralUtils.ForceFloat(text2);
		}
		else
		{
			if (!(type == typeof(string)))
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

	private bool Save(string triggerKey)
	{
		LoadResult loadResult = m_loadResult;
		if (loadResult == LoadResult.INVALID || loadResult == LoadResult.FAIL)
		{
			return false;
		}
		return Save();
	}

	private bool Save()
	{
		if (!m_dirty)
		{
			return true;
		}
		List<string> list = new List<string>();
		for (int i = 0; i < m_sortedKeys.Count; i++)
		{
			string text = m_sortedKeys[i];
			if (!m_temporaryKeys.Contains(text))
			{
				object arg = m_options[text];
				string item = $"{text}={arg}";
				list.Add(item);
			}
		}
		bool flag = WriteOptionsFile($"{FileUtils.ExternalDataPath}/{Vars.GetOptionsFileName()}", list);
		if (!flag)
		{
			flag = WriteOptionsFile($"{FileUtils.PersistentDataPath}/{Vars.GetOptionsFileName()}", list);
		}
		return flag;
	}

	private bool WriteOptionsFile(string optionsFilePath, List<string> lines)
	{
		try
		{
			File.WriteAllLines(optionsFilePath, lines.ToArray(), new UTF8Encoding());
		}
		catch (Exception ex)
		{
			Debug.LogError($"LocalOptions.Save() - Failed to save {optionsFilePath}. Exception={ex.Message}");
			return false;
		}
		m_dirty = false;
		return true;
	}

	private void SortKeys()
	{
		m_sortedKeys.Sort(KeyComparison);
	}

	private int KeyComparison(string key1, string key2)
	{
		return string.Compare(key1, key2, ignoreCase: true);
	}
}
