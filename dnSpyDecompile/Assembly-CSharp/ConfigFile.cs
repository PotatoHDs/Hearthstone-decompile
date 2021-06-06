using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020009A8 RID: 2472
public class ConfigFile
{
	// Token: 0x060086E3 RID: 34531 RVA: 0x002B8F37 File Offset: 0x002B7137
	public string GetPath()
	{
		return this.m_path;
	}

	// Token: 0x060086E4 RID: 34532 RVA: 0x002B8F3F File Offset: 0x002B713F
	public bool LightLoad(string path)
	{
		return this.Load(path, true);
	}

	// Token: 0x060086E5 RID: 34533 RVA: 0x002B8F49 File Offset: 0x002B7149
	public bool FullLoad(string path)
	{
		return this.Load(path, false);
	}

	// Token: 0x060086E6 RID: 34534 RVA: 0x002B8F54 File Offset: 0x002B7154
	public bool Save(string path = null)
	{
		if (path == null)
		{
			path = this.m_path;
		}
		if (path == null)
		{
			Debug.LogError("ConfigFile.Save() - no path given");
			return false;
		}
		string contents = this.GenerateText();
		try
		{
			FileUtils.SetFileWritableFlag(path, true);
			File.WriteAllText(path, contents);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("ConfigFile.Save() - Failed to write file at {0}. Exception={1}", path, ex.Message));
			return false;
		}
		this.m_path = path;
		return true;
	}

	// Token: 0x060086E7 RID: 34535 RVA: 0x002B8FC8 File Offset: 0x002B71C8
	public bool Has(string key)
	{
		return this.FindEntry(key) != null;
	}

	// Token: 0x060086E8 RID: 34536 RVA: 0x002B8FD4 File Offset: 0x002B71D4
	public bool Delete(string key, bool removeEmptySections = true)
	{
		int num = this.FindEntryIndex(key);
		if (num < 0)
		{
			return false;
		}
		this.m_lines.RemoveAt(num);
		if (removeEmptySections)
		{
			int i;
			for (i = num - 1; i >= 0; i--)
			{
				ConfigFile.Line line = this.m_lines[i];
				if (line.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				if (!string.IsNullOrEmpty(line.m_raw.Trim()))
				{
					return true;
				}
			}
			int j;
			for (j = num; j < this.m_lines.Count; j++)
			{
				ConfigFile.Line line2 = this.m_lines[j];
				if (line2.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				if (!string.IsNullOrEmpty(line2.m_raw.Trim()))
				{
					return true;
				}
			}
			int count = j - i;
			this.m_lines.RemoveRange(i, count);
		}
		return true;
	}

	// Token: 0x060086E9 RID: 34537 RVA: 0x002B9091 File Offset: 0x002B7291
	public void Clear()
	{
		this.m_lines.Clear();
	}

	// Token: 0x060086EA RID: 34538 RVA: 0x002B90A0 File Offset: 0x002B72A0
	public string Get(string key, string defaultVal = "")
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return line.m_value;
	}

	// Token: 0x060086EB RID: 34539 RVA: 0x002B90C0 File Offset: 0x002B72C0
	public bool Get(string key, bool defaultVal = false)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceBool(line.m_value);
	}

	// Token: 0x060086EC RID: 34540 RVA: 0x002B90E8 File Offset: 0x002B72E8
	public int Get(string key, int defaultVal = 0)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceInt(line.m_value);
	}

	// Token: 0x060086ED RID: 34541 RVA: 0x002B9110 File Offset: 0x002B7310
	public float Get(string key, float defaultVal = 0f)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceFloat(line.m_value);
	}

	// Token: 0x060086EE RID: 34542 RVA: 0x002B9138 File Offset: 0x002B7338
	public bool Set(string key, object val)
	{
		string val2 = (val == null) ? string.Empty : val.ToString();
		return this.Set(key, val2);
	}

	// Token: 0x060086EF RID: 34543 RVA: 0x002B9160 File Offset: 0x002B7360
	public bool Set(string key, bool val)
	{
		string val2 = val ? "true" : "false";
		return this.Set(key, val2);
	}

	// Token: 0x060086F0 RID: 34544 RVA: 0x002B9188 File Offset: 0x002B7388
	public bool Set(string key, string val)
	{
		ConfigFile.Line line = this.RegisterEntry(key);
		if (line == null)
		{
			return false;
		}
		line.m_value = val;
		return true;
	}

	// Token: 0x060086F1 RID: 34545 RVA: 0x002B91AA File Offset: 0x002B73AA
	public List<ConfigFile.Line> GetLines()
	{
		return this.m_lines;
	}

	// Token: 0x060086F2 RID: 34546 RVA: 0x002B91B4 File Offset: 0x002B73B4
	public string GenerateText()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			ConfigFile.LineType type = line.m_type;
			if (type != ConfigFile.LineType.SECTION)
			{
				if (type != ConfigFile.LineType.ENTRY)
				{
					stringBuilder.Append(line.m_raw);
				}
				else if (line.m_quoteValue)
				{
					stringBuilder.AppendFormat("{0} = \"{1}\"", line.m_lineKey, line.m_value);
				}
				else
				{
					stringBuilder.AppendFormat("{0} = {1}", line.m_lineKey, line.m_value);
				}
			}
			else
			{
				if (i > 0 && this.m_lines[i - 1].m_type != ConfigFile.LineType.UNKNOWN)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendFormat("[{0}]", line.m_sectionName);
			}
			stringBuilder.AppendLine();
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060086F3 RID: 34547 RVA: 0x002B928C File Offset: 0x002B748C
	private bool Load(string path, bool ignoreUselessLines)
	{
		this.m_path = null;
		this.m_lines.Clear();
		if (!File.Exists(path))
		{
			if (path.Contains("client.config"))
			{
				Debug.LogError("Error loading config file " + path);
			}
			return false;
		}
		int num = 1;
		using (StreamReader streamReader = File.OpenText(path))
		{
			string text = string.Empty;
			while (streamReader.Peek() != -1)
			{
				string text2 = streamReader.ReadLine();
				string text3 = text2.Trim();
				if (!ignoreUselessLines || text3.Length > 0)
				{
					bool flag = text3.Length > 0 && text3[0] == ';';
					if (!ignoreUselessLines || !flag)
					{
						ConfigFile.Line line = new ConfigFile.Line();
						line.m_raw = text2;
						line.m_sectionName = text;
						if (flag)
						{
							line.m_type = ConfigFile.LineType.COMMENT;
						}
						else if (text3.Length > 0)
						{
							if (text3[0] == '[')
							{
								if (text3.Length >= 2 && text3[text3.Length - 1] == ']')
								{
									line.m_type = ConfigFile.LineType.SECTION;
									text = (line.m_sectionName = text3.Substring(1, text3.Length - 2));
									this.m_lines.Add(line);
									continue;
								}
								Debug.LogWarning(string.Format("ConfigFile.Load() - invalid section \"{0}\" on line {1} in file {2}", text2, num, path));
								if (!ignoreUselessLines)
								{
									this.m_lines.Add(line);
									continue;
								}
								continue;
							}
							else
							{
								int num2 = text3.IndexOf('=');
								if (num2 < 0)
								{
									Debug.LogWarning(string.Format("ConfigFile.Load() - invalid entry \"{0}\" on line {1} in file {2}", text2, num, path));
									if (!ignoreUselessLines)
									{
										this.m_lines.Add(line);
										continue;
									}
									continue;
								}
								else
								{
									string text4 = text3.Substring(0, num2).Trim();
									string text5 = text3.Substring(num2 + 1, text3.Length - num2 - 1).Trim();
									if (text5.Length > 2)
									{
										int index = text5.Length - 1;
										if ((text5[0] == '"' || text5[0] == '“' || text5[0] == '”') && (text5[index] == '"' || text5[index] == '“' || text5[index] == '”'))
										{
											text5 = text5.Substring(1, text5.Length - 2);
											line.m_quoteValue = true;
										}
									}
									line.m_type = ConfigFile.LineType.ENTRY;
									line.m_fullKey = (string.IsNullOrEmpty(text) ? text4 : string.Format("{0}.{1}", text, text4));
									line.m_lineKey = text4;
									line.m_value = text5;
								}
							}
						}
						this.m_lines.Add(line);
					}
				}
			}
		}
		this.m_path = path;
		return true;
	}

	// Token: 0x060086F4 RID: 34548 RVA: 0x002B9568 File Offset: 0x002B7768
	private int FindSectionIndex(string sectionName)
	{
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			if (line.m_type == ConfigFile.LineType.SECTION && line.m_sectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060086F5 RID: 34549 RVA: 0x002B95B4 File Offset: 0x002B77B4
	private ConfigFile.Line FindEntry(string fullKey)
	{
		int num = this.FindEntryIndex(fullKey);
		if (num < 0)
		{
			return null;
		}
		return this.m_lines[num];
	}

	// Token: 0x060086F6 RID: 34550 RVA: 0x002B95DC File Offset: 0x002B77DC
	private int FindEntryIndex(string fullKey)
	{
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			if (line.m_type == ConfigFile.LineType.ENTRY && line.m_fullKey.Equals(fullKey, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060086F7 RID: 34551 RVA: 0x002B9628 File Offset: 0x002B7828
	private ConfigFile.Line RegisterEntry(string fullKey)
	{
		if (string.IsNullOrEmpty(fullKey))
		{
			return null;
		}
		int num = fullKey.IndexOf('.');
		if (num < 0)
		{
			return null;
		}
		string sectionName = fullKey.Substring(0, num);
		string text = string.Empty;
		if (fullKey.Length > num + 1)
		{
			text = fullKey.Substring(num + 1, fullKey.Length - num - 1);
		}
		ConfigFile.Line line = null;
		int num2 = this.FindSectionIndex(sectionName);
		if (num2 < 0)
		{
			ConfigFile.Line line2 = new ConfigFile.Line();
			line2.m_type = ConfigFile.LineType.SECTION;
			line2.m_sectionName = sectionName;
			this.m_lines.Add(line2);
			line = new ConfigFile.Line();
			line.m_type = ConfigFile.LineType.ENTRY;
			line.m_sectionName = sectionName;
			line.m_lineKey = text;
			line.m_fullKey = fullKey;
			this.m_lines.Add(line);
		}
		else
		{
			int i;
			for (i = num2 + 1; i < this.m_lines.Count; i++)
			{
				ConfigFile.Line line3 = this.m_lines[i];
				if (line3.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				if (line3.m_type == ConfigFile.LineType.ENTRY && line3.m_lineKey.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					line = line3;
					break;
				}
			}
			if (line == null)
			{
				line = new ConfigFile.Line();
				line.m_type = ConfigFile.LineType.ENTRY;
				line.m_sectionName = sectionName;
				line.m_lineKey = text;
				line.m_fullKey = fullKey;
				this.m_lines.Insert(i, line);
			}
		}
		return line;
	}

	// Token: 0x04007218 RID: 29208
	private string m_path;

	// Token: 0x04007219 RID: 29209
	private List<ConfigFile.Line> m_lines = new List<ConfigFile.Line>();

	// Token: 0x02002664 RID: 9828
	public enum LineType
	{
		// Token: 0x0400F07B RID: 61563
		UNKNOWN,
		// Token: 0x0400F07C RID: 61564
		COMMENT,
		// Token: 0x0400F07D RID: 61565
		SECTION,
		// Token: 0x0400F07E RID: 61566
		ENTRY
	}

	// Token: 0x02002665 RID: 9829
	public class Line
	{
		// Token: 0x0400F07F RID: 61567
		public string m_raw = string.Empty;

		// Token: 0x0400F080 RID: 61568
		public ConfigFile.LineType m_type;

		// Token: 0x0400F081 RID: 61569
		public string m_sectionName = string.Empty;

		// Token: 0x0400F082 RID: 61570
		public string m_lineKey = string.Empty;

		// Token: 0x0400F083 RID: 61571
		public string m_fullKey = string.Empty;

		// Token: 0x0400F084 RID: 61572
		public string m_value = string.Empty;

		// Token: 0x0400F085 RID: 61573
		public bool m_quoteValue;
	}
}
