using System;
using System.Collections.Generic;
using System.IO;
using Assets;
using Hearthstone;
using UnityEngine;

// Token: 0x020008CB RID: 2251
public class GameStringTable
{
	// Token: 0x06007C95 RID: 31893 RVA: 0x00288650 File Offset: 0x00286850
	public bool Load(Global.GameStringCategory cat, bool native = false)
	{
		string filePathWithLoadOrder = GameStringTable.GetFilePathWithLoadOrder(cat, native, new GameStringTable.FilePathFromCategoryCallback(GameStringTable.GetFilePathFromCategory));
		string filePathWithLoadOrder2 = GameStringTable.GetFilePathWithLoadOrder(cat, false, new GameStringTable.FilePathFromCategoryCallback(GameStringTable.GetAudioFilePathFromCategory));
		return this.Load(cat, filePathWithLoadOrder, filePathWithLoadOrder2);
	}

	// Token: 0x06007C96 RID: 31894 RVA: 0x00288690 File Offset: 0x00286890
	public bool Load(Global.GameStringCategory cat, Locale locale, bool native = false)
	{
		string filePathFromCategory = GameStringTable.GetFilePathFromCategory(cat, locale, native);
		string audioFilePathFromCategory = GameStringTable.GetAudioFilePathFromCategory(cat, locale, native);
		return this.Load(cat, filePathFromCategory, audioFilePathFromCategory);
	}

	// Token: 0x06007C97 RID: 31895 RVA: 0x002886B8 File Offset: 0x002868B8
	public bool Load(Global.GameStringCategory cat, string path, string audioPath)
	{
		this.m_category = Global.GameStringCategory.INVALID;
		this.m_table.Clear();
		List<GameStringTable.Entry> list = null;
		List<GameStringTable.Entry> list2 = null;
		GameStringTable.Header header;
		if (File.Exists(path) && !GameStringTable.LoadFile(path, out header, out list))
		{
			Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - Failed to load {0} for category {1}.", new object[]
			{
				path,
				cat
			});
			return false;
		}
		GameStringTable.Header header2;
		if (File.Exists(audioPath) && !GameStringTable.LoadFile(audioPath, out header2, out list2))
		{
			Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - Failed to load {0} for category {1}.", new object[]
			{
				audioPath,
				cat
			});
			return false;
		}
		if (list != null && list2 != null)
		{
			this.BuildTable(path, list, audioPath, list2);
		}
		else if (list != null)
		{
			this.BuildTable(path, list);
		}
		else
		{
			if (list2 == null)
			{
				Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.Load() - There are no entries for category {0} - path: {1}.", new object[]
				{
					cat,
					path
				});
				return false;
			}
			this.BuildTable(audioPath, list2);
		}
		this.m_category = cat;
		return true;
	}

	// Token: 0x06007C98 RID: 31896 RVA: 0x002887A4 File Offset: 0x002869A4
	public string Get(string key)
	{
		string result;
		this.m_table.TryGetValue(key, out result);
		return result;
	}

	// Token: 0x06007C99 RID: 31897 RVA: 0x002887C1 File Offset: 0x002869C1
	public Map<string, string> GetAll()
	{
		return this.m_table;
	}

	// Token: 0x06007C9A RID: 31898 RVA: 0x002887C9 File Offset: 0x002869C9
	public Global.GameStringCategory GetCategory()
	{
		return this.m_category;
	}

	// Token: 0x06007C9B RID: 31899 RVA: 0x002887D4 File Offset: 0x002869D4
	public static bool LoadFile(string path, out GameStringTable.Header header, out List<GameStringTable.Entry> entries)
	{
		header = null;
		entries = null;
		string[] lines = null;
		try
		{
			lines = File.ReadAllLines(path);
		}
		catch (Exception ex)
		{
			Debug.LogWarning(string.Format("GameStringTable.LoadFile() - Failed to read \"{0}\".\n\nException: {1}", path, ex.Message));
			return false;
		}
		header = GameStringTable.LoadFileHeader(lines);
		if (header == null)
		{
			Debug.LogWarning(string.Format("GameStringTable.LoadFile() - \"{0}\" had a malformed header.", path));
			return false;
		}
		entries = GameStringTable.LoadFileEntries(path, header, lines);
		return true;
	}

	// Token: 0x06007C9C RID: 31900 RVA: 0x0028884C File Offset: 0x00286A4C
	private static string GetFilePathWithLoadOrder(Global.GameStringCategory cat, bool native, GameStringTable.FilePathFromCategoryCallback pathCallback)
	{
		Locale[] loadOrder = Localization.GetLoadOrder(false);
		for (int i = 0; i < loadOrder.Length; i++)
		{
			string text = pathCallback(cat, loadOrder[i], native);
			if (File.Exists(text))
			{
				return text;
			}
		}
		Log.Downloader.PrintDebug("category {0}, native {1}, locale {2}.", new object[]
		{
			cat,
			native,
			Localization.GetLocaleName()
		});
		return null;
	}

	// Token: 0x06007C9D RID: 31901 RVA: 0x002888B4 File Offset: 0x00286AB4
	private static string GetFilePathFromCategory(Global.GameStringCategory cat, Locale locale, bool native)
	{
		string fileName = string.Format("{0}.txt", cat);
		return GameStrings.GetAssetPath(locale, fileName, native);
	}

	// Token: 0x06007C9E RID: 31902 RVA: 0x002888DC File Offset: 0x00286ADC
	private static string GetAudioFilePathFromCategory(Global.GameStringCategory cat, Locale locale, bool native)
	{
		string fileName = string.Format("{0}_AUDIO.txt", cat);
		return GameStrings.GetAssetPath(locale, fileName, native);
	}

	// Token: 0x06007C9F RID: 31903 RVA: 0x00288904 File Offset: 0x00286B04
	private static GameStringTable.Header LoadFileHeader(string[] lines)
	{
		GameStringTable.Header header = new GameStringTable.Header();
		int i = 0;
		while (i < lines.Length)
		{
			string text = lines[i];
			if (text.Length != 0 && !text.StartsWith("#"))
			{
				string[] array = text.Split(new char[]
				{
					'\t'
				});
				for (int j = 0; j < array.Length; j++)
				{
					string a = array[j];
					if (a == "TAG")
					{
						header.m_keyIndex = j;
						if (header.m_valueIndex >= 0)
						{
							break;
						}
					}
					else if (a == "TEXT")
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
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x06007CA0 RID: 31904 RVA: 0x002889D4 File Offset: 0x00286BD4
	private static List<GameStringTable.Entry> LoadFileEntries(string path, GameStringTable.Header header, string[] lines)
	{
		List<GameStringTable.Entry> list = new List<GameStringTable.Entry>(lines.Length);
		int num = Mathf.Max(header.m_keyIndex, header.m_valueIndex);
		for (int i = header.m_entryStartIndex; i < lines.Length; i++)
		{
			string text = lines[i];
			if (text.Length != 0 && !text.StartsWith("#") && !string.IsNullOrEmpty(text.Trim()))
			{
				string[] array = text.Split(new char[]
				{
					'\t'
				});
				if (array.Length <= num)
				{
					Error.AddDevWarningNonRepeating("GameStrings Error", "GameStringTable.LoadFileEntries() - line {0} in \"{1}\" is malformed", new object[]
					{
						i + 1,
						path
					});
				}
				else
				{
					list.Add(new GameStringTable.Entry
					{
						m_key = array[header.m_keyIndex],
						m_value = TextUtils.DecodeWhitespaces(array[header.m_valueIndex])
					});
				}
			}
		}
		return list;
	}

	// Token: 0x06007CA1 RID: 31905 RVA: 0x00288AB8 File Offset: 0x00286CB8
	private void BuildTable(string path, List<GameStringTable.Entry> entries)
	{
		int count = entries.Count;
		this.m_table = new Map<string, string>(count);
		if (count == 0)
		{
			return;
		}
		if (HearthstoneApplication.IsInternal())
		{
			GameStringTable.CheckConflicts(path, entries);
		}
		foreach (GameStringTable.Entry entry in entries)
		{
			this.m_table[entry.m_key] = entry.m_value;
		}
	}

	// Token: 0x06007CA2 RID: 31906 RVA: 0x00288B3C File Offset: 0x00286D3C
	private void BuildTable(string path, List<GameStringTable.Entry> entries, string audioPath, List<GameStringTable.Entry> audioEntries)
	{
		int num = entries.Count + audioEntries.Count;
		this.m_table = new Map<string, string>(num);
		if (num == 0)
		{
			return;
		}
		if (HearthstoneApplication.IsInternal())
		{
			GameStringTable.CheckConflicts(path, entries);
		}
		foreach (GameStringTable.Entry entry in entries)
		{
			this.m_table[entry.m_key] = entry.m_value;
		}
		foreach (GameStringTable.Entry entry2 in audioEntries)
		{
			this.m_table[entry2.m_key] = entry2.m_value;
		}
	}

	// Token: 0x06007CA3 RID: 31907 RVA: 0x00288C18 File Offset: 0x00286E18
	private static void CheckConflicts(string path, List<GameStringTable.Entry> entries)
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
					string message = string.Format("GameStringTable.CheckConflicts() - Tag {0} appears more than once in {1}. All tags must be unique.", key, path);
					Error.AddDevWarningNonRepeating("GameStrings Error", message, Array.Empty<object>());
				}
			}
		}
	}

	// Token: 0x06007CA4 RID: 31908 RVA: 0x00288C98 File Offset: 0x00286E98
	private static void CheckConflicts(string path1, List<GameStringTable.Entry> entries1, string path2, List<GameStringTable.Entry> entries2)
	{
		if (entries1.Count == 0)
		{
			return;
		}
		GameStringTable.CheckConflicts(path1, entries1);
		if (entries2.Count == 0)
		{
			return;
		}
		GameStringTable.CheckConflicts(path2, entries2);
		for (int i = 0; i < entries1.Count; i++)
		{
			string key = entries1[i].m_key;
			for (int j = 0; j < entries2.Count; j++)
			{
				string key2 = entries2[j].m_key;
				if (string.Equals(key, key2, StringComparison.OrdinalIgnoreCase))
				{
					string message = string.Format("GameStringTable.CheckConflicts() - Tag {0} is used in {1} and {2}. All tags must be unique.", key, path1, path2);
					Error.AddDevWarningNonRepeating("GameStrings Error", message, Array.Empty<object>());
				}
			}
		}
	}

	// Token: 0x04006561 RID: 25953
	public const string KEY_FIELD_NAME = "TAG";

	// Token: 0x04006562 RID: 25954
	public const string VALUE_FIELD_NAME = "TEXT";

	// Token: 0x04006563 RID: 25955
	private Global.GameStringCategory m_category;

	// Token: 0x04006564 RID: 25956
	private Map<string, string> m_table = new Map<string, string>();

	// Token: 0x02002551 RID: 9553
	public class Entry
	{
		// Token: 0x0400ED29 RID: 60713
		public string m_key;

		// Token: 0x0400ED2A RID: 60714
		public string m_value;
	}

	// Token: 0x02002552 RID: 9554
	public class Header
	{
		// Token: 0x0400ED2B RID: 60715
		public int m_entryStartIndex = -1;

		// Token: 0x0400ED2C RID: 60716
		public int m_keyIndex = -1;

		// Token: 0x0400ED2D RID: 60717
		public int m_valueIndex = -1;
	}

	// Token: 0x02002553 RID: 9555
	// (Invoke) Token: 0x060132A4 RID: 78500
	private delegate string FilePathFromCategoryCallback(Global.GameStringCategory cat, Locale locale, bool native);
}
