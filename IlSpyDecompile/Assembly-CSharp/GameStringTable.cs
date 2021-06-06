using System;
using System.Collections.Generic;
using System.IO;
using Assets;
using Hearthstone;
using UnityEngine;

public class GameStringTable
{
	public class Entry
	{
		public string m_key;

		public string m_value;
	}

	public class Header
	{
		public int m_entryStartIndex = -1;

		public int m_keyIndex = -1;

		public int m_valueIndex = -1;
	}

	private delegate string FilePathFromCategoryCallback(Global.GameStringCategory cat, Locale locale, bool native);

	public const string KEY_FIELD_NAME = "TAG";

	public const string VALUE_FIELD_NAME = "TEXT";

	private Global.GameStringCategory m_category;

	private Map<string, string> m_table = new Map<string, string>();

	public bool Load(Global.GameStringCategory cat, bool native = false)
	{
		string filePathWithLoadOrder = GetFilePathWithLoadOrder(cat, native, GetFilePathFromCategory);
		string filePathWithLoadOrder2 = GetFilePathWithLoadOrder(cat, native: false, GetAudioFilePathFromCategory);
		return Load(cat, filePathWithLoadOrder, filePathWithLoadOrder2);
	}

	public bool Load(Global.GameStringCategory cat, Locale locale, bool native = false)
	{
		string filePathFromCategory = GetFilePathFromCategory(cat, locale, native);
		string audioFilePathFromCategory = GetAudioFilePathFromCategory(cat, locale, native);
		return Load(cat, filePathFromCategory, audioFilePathFromCategory);
	}

	public bool Load(Global.GameStringCategory cat, string path, string audioPath)
	{
		m_category = Global.GameStringCategory.INVALID;
		m_table.Clear();
		List<Entry> entries = null;
		List<Entry> entries2 = null;
		if (File.Exists(path) && !LoadFile(path, out var _, out entries))
		{
			Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - Failed to load {0} for category {1}.", path, cat);
			return false;
		}
		if (File.Exists(audioPath) && !LoadFile(audioPath, out var _, out entries2))
		{
			Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - Failed to load {0} for category {1}.", audioPath, cat);
			return false;
		}
		if (entries != null && entries2 != null)
		{
			BuildTable(path, entries, audioPath, entries2);
		}
		else if (entries != null)
		{
			BuildTable(path, entries);
		}
		else
		{
			if (entries2 == null)
			{
				Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - There are no entries for category {0} - path: {1}.", cat, path);
				return false;
			}
			BuildTable(audioPath, entries2);
		}
		m_category = cat;
		return true;
	}

	public string Get(string key)
	{
		m_table.TryGetValue(key, out var value);
		return value;
	}

	public Map<string, string> GetAll()
	{
		return m_table;
	}

	public Global.GameStringCategory GetCategory()
	{
		return m_category;
	}

	public static bool LoadFile(string path, out Header header, out List<Entry> entries)
	{
		header = null;
		entries = null;
		string[] array = null;
		try
		{
			array = File.ReadAllLines(path);
		}
		catch (Exception ex)
		{
			Debug.LogWarning($"GameStringTable.LoadFile() - Failed to read \"{path}\".\n\nException: {ex.Message}");
			return false;
		}
		header = LoadFileHeader(array);
		if (header == null)
		{
			Debug.LogWarning($"GameStringTable.LoadFile() - \"{path}\" had a malformed header.");
			return false;
		}
		entries = LoadFileEntries(path, header, array);
		return true;
	}

	private static string GetFilePathWithLoadOrder(Global.GameStringCategory cat, bool native, FilePathFromCategoryCallback pathCallback)
	{
		Locale[] loadOrder = Localization.GetLoadOrder();
		for (int i = 0; i < loadOrder.Length; i++)
		{
			string text = pathCallback(cat, loadOrder[i], native);
			if (File.Exists(text))
			{
				return text;
			}
		}
		Log.Downloader.PrintDebug("category {0}, native {1}, locale {2}.", cat, native, Localization.GetLocaleName());
		return null;
	}

	private static string GetFilePathFromCategory(Global.GameStringCategory cat, Locale locale, bool native)
	{
		string fileName = $"{cat}.txt";
		return GameStrings.GetAssetPath(locale, fileName, native);
	}

	private static string GetAudioFilePathFromCategory(Global.GameStringCategory cat, Locale locale, bool native)
	{
		string fileName = $"{cat}_AUDIO.txt";
		return GameStrings.GetAssetPath(locale, fileName, native);
	}

	private static Header LoadFileHeader(string[] lines)
	{
		Header header = new Header();
		for (int i = 0; i < lines.Length; i++)
		{
			string text = lines[i];
			if (text.Length == 0 || text.StartsWith("#"))
			{
				continue;
			}
			string[] array = text.Split('\t');
			for (int j = 0; j < array.Length; j++)
			{
				string text2 = array[j];
				if (text2 == "TAG")
				{
					header.m_keyIndex = j;
					if (header.m_valueIndex >= 0)
					{
						break;
					}
				}
				else if (text2 == "TEXT")
				{
					header.m_valueIndex = j;
					if (header.m_keyIndex >= 0)
					{
						break;
					}
				}
			}
			if (header.m_keyIndex < 0 && header.m_valueIndex < 0)
			{
				return null;
			}
			header.m_entryStartIndex = i + 1;
			return header;
		}
		return null;
	}

	private static List<Entry> LoadFileEntries(string path, Header header, string[] lines)
	{
		List<Entry> list = new List<Entry>(lines.Length);
		int num = Mathf.Max(header.m_keyIndex, header.m_valueIndex);
		for (int i = header.m_entryStartIndex; i < lines.Length; i++)
		{
			string text = lines[i];
			if (text.Length != 0 && !text.StartsWith("#") && !string.IsNullOrEmpty(text.Trim()))
			{
				string[] array = text.Split('\t');
				if (array.Length <= num)
				{
					Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.LoadFileEntries() - line {0} in \"{1}\" is malformed", i + 1, path);
				}
				else
				{
					Entry entry = new Entry();
					entry.m_key = array[header.m_keyIndex];
					entry.m_value = TextUtils.DecodeWhitespaces(array[header.m_valueIndex]);
					list.Add(entry);
				}
			}
		}
		return list;
	}

	private void BuildTable(string path, List<Entry> entries)
	{
		int count = entries.Count;
		m_table = new Map<string, string>(count);
		if (count == 0)
		{
			return;
		}
		if (HearthstoneApplication.IsInternal())
		{
			CheckConflicts(path, entries);
		}
		foreach (Entry entry in entries)
		{
			m_table[entry.m_key] = entry.m_value;
		}
	}

	private void BuildTable(string path, List<Entry> entries, string audioPath, List<Entry> audioEntries)
	{
		int num = entries.Count + audioEntries.Count;
		m_table = new Map<string, string>(num);
		if (num == 0)
		{
			return;
		}
		if (HearthstoneApplication.IsInternal())
		{
			CheckConflicts(path, entries);
		}
		foreach (Entry entry in entries)
		{
			m_table[entry.m_key] = entry.m_value;
		}
		foreach (Entry audioEntry in audioEntries)
		{
			m_table[audioEntry.m_key] = audioEntry.m_value;
		}
	}

	private static void CheckConflicts(string path, List<Entry> entries)
	{
		if (entries.Count == 0)
		{
			return;
		}
		for (int i = 0; i < entries.Count; i++)
		{
			string key = entries[i].m_key;
			for (int j = i + 1; j < entries.Count; j++)
			{
				string key2 = entries[j].m_key;
				if (string.Equals(key, key2, StringComparison.OrdinalIgnoreCase))
				{
					string message = $"GameStringTable.CheckConflicts() - Tag {key} appears more than once in {path}. All tags must be unique.";
					Error.AddDevWarningNonRepeating("GameStrings Error", message);
				}
			}
		}
	}

	private static void CheckConflicts(string path1, List<Entry> entries1, string path2, List<Entry> entries2)
	{
		if (entries1.Count == 0)
		{
			return;
		}
		CheckConflicts(path1, entries1);
		if (entries2.Count == 0)
		{
			return;
		}
		CheckConflicts(path2, entries2);
		for (int i = 0; i < entries1.Count; i++)
		{
			string key = entries1[i].m_key;
			for (int j = 0; j < entries2.Count; j++)
			{
				string key2 = entries2[j].m_key;
				if (string.Equals(key, key2, StringComparison.OrdinalIgnoreCase))
				{
					string message = $"GameStringTable.CheckConflicts() - Tag {key} is used in {path1} and {path2}. All tags must be unique.";
					Error.AddDevWarningNonRepeating("GameStrings Error", message);
				}
			}
		}
	}
}
