using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using UnityEngine;

public class ConfigFile
{
	public enum LineType
	{
		UNKNOWN,
		COMMENT,
		SECTION,
		ENTRY
	}

	public class Line
	{
		public string m_raw = string.Empty;

		public LineType m_type;

		public string m_sectionName = string.Empty;

		public string m_lineKey = string.Empty;

		public string m_fullKey = string.Empty;

		public string m_value = string.Empty;

		public bool m_quoteValue;
	}

	private string m_path;

	private List<Line> m_lines = new List<Line>();

	public string GetPath()
	{
		return m_path;
	}

	public bool LightLoad(string path)
	{
		return Load(path, ignoreUselessLines: true);
	}

	public bool FullLoad(string path)
	{
		return Load(path, ignoreUselessLines: false);
	}

	public bool Save(string path = null)
	{
		if (path == null)
		{
			path = m_path;
		}
		if (path == null)
		{
			Debug.LogError("ConfigFile.Save() - no path given");
			return false;
		}
		string contents = GenerateText();
		try
		{
			FileUtils.SetFileWritableFlag(path, setWritable: true);
			File.WriteAllText(path, contents);
		}
		catch (Exception ex)
		{
			Debug.LogError($"ConfigFile.Save() - Failed to write file at {path}. Exception={ex.Message}");
			return false;
		}
		m_path = path;
		return true;
	}

	public bool Has(string key)
	{
		return FindEntry(key) != null;
	}

	public bool Delete(string key, bool removeEmptySections = true)
	{
		int num = FindEntryIndex(key);
		if (num < 0)
		{
			return false;
		}
		m_lines.RemoveAt(num);
		if (removeEmptySections)
		{
			int num2;
			for (num2 = num - 1; num2 >= 0; num2--)
			{
				Line line = m_lines[num2];
				if (line.m_type == LineType.SECTION)
				{
					break;
				}
				if (!string.IsNullOrEmpty(line.m_raw.Trim()))
				{
					return true;
				}
			}
			int i;
			for (i = num; i < m_lines.Count; i++)
			{
				Line line2 = m_lines[i];
				if (line2.m_type == LineType.SECTION)
				{
					break;
				}
				if (!string.IsNullOrEmpty(line2.m_raw.Trim()))
				{
					return true;
				}
			}
			int count = i - num2;
			m_lines.RemoveRange(num2, count);
		}
		return true;
	}

	public void Clear()
	{
		m_lines.Clear();
	}

	public string Get(string key, string defaultVal = "")
	{
		Line line = FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return line.m_value;
	}

	public bool Get(string key, bool defaultVal = false)
	{
		Line line = FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceBool(line.m_value);
	}

	public int Get(string key, int defaultVal = 0)
	{
		Line line = FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceInt(line.m_value);
	}

	public float Get(string key, float defaultVal = 0f)
	{
		Line line = FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceFloat(line.m_value);
	}

	public bool Set(string key, object val)
	{
		string val2 = ((val == null) ? string.Empty : val.ToString());
		return Set(key, val2);
	}

	public bool Set(string key, bool val)
	{
		string val2 = (val ? "true" : "false");
		return Set(key, val2);
	}

	public bool Set(string key, string val)
	{
		Line line = RegisterEntry(key);
		if (line == null)
		{
			return false;
		}
		line.m_value = val;
		return true;
	}

	public List<Line> GetLines()
	{
		return m_lines;
	}

	public string GenerateText()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < m_lines.Count; i++)
		{
			Line line = m_lines[i];
			switch (line.m_type)
			{
			case LineType.SECTION:
				if (i > 0 && m_lines[i - 1].m_type != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendFormat("[{0}]", line.m_sectionName);
				break;
			case LineType.ENTRY:
				if (line.m_quoteValue)
				{
					stringBuilder.AppendFormat("{0} = \"{1}\"", line.m_lineKey, line.m_value);
				}
				else
				{
					stringBuilder.AppendFormat("{0} = {1}", line.m_lineKey, line.m_value);
				}
				break;
			default:
				stringBuilder.Append(line.m_raw);
				break;
			}
			stringBuilder.AppendLine();
		}
		return stringBuilder.ToString();
	}

	private bool Load(string path, bool ignoreUselessLines)
	{
		m_path = null;
		m_lines.Clear();
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
				if (ignoreUselessLines && text3.Length <= 0)
				{
					continue;
				}
				bool flag = text3.Length > 0 && text3[0] == ';';
				if (ignoreUselessLines && flag)
				{
					continue;
				}
				Line line = new Line();
				line.m_raw = text2;
				line.m_sectionName = text;
				if (flag)
				{
					line.m_type = LineType.COMMENT;
				}
				else if (text3.Length > 0)
				{
					if (text3[0] == '[')
					{
						if (text3.Length < 2 || text3[text3.Length - 1] != ']')
						{
							Debug.LogWarning($"ConfigFile.Load() - invalid section \"{text2}\" on line {num} in file {path}");
							if (!ignoreUselessLines)
							{
								m_lines.Add(line);
							}
						}
						else
						{
							line.m_type = LineType.SECTION;
							text = (line.m_sectionName = text3.Substring(1, text3.Length - 2));
							m_lines.Add(line);
						}
						continue;
					}
					int num2 = text3.IndexOf('=');
					if (num2 < 0)
					{
						Debug.LogWarning($"ConfigFile.Load() - invalid entry \"{text2}\" on line {num} in file {path}");
						if (!ignoreUselessLines)
						{
							m_lines.Add(line);
						}
						continue;
					}
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
					line.m_type = LineType.ENTRY;
					line.m_fullKey = (string.IsNullOrEmpty(text) ? text4 : $"{text}.{text4}");
					line.m_lineKey = text4;
					line.m_value = text5;
				}
				m_lines.Add(line);
			}
		}
		m_path = path;
		return true;
	}

	private int FindSectionIndex(string sectionName)
	{
		for (int i = 0; i < m_lines.Count; i++)
		{
			Line line = m_lines[i];
			if (line.m_type == LineType.SECTION && line.m_sectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	private Line FindEntry(string fullKey)
	{
		int num = FindEntryIndex(fullKey);
		if (num < 0)
		{
			return null;
		}
		return m_lines[num];
	}

	private int FindEntryIndex(string fullKey)
	{
		for (int i = 0; i < m_lines.Count; i++)
		{
			Line line = m_lines[i];
			if (line.m_type == LineType.ENTRY && line.m_fullKey.Equals(fullKey, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	private Line RegisterEntry(string fullKey)
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
		Line line = null;
		int num2 = FindSectionIndex(sectionName);
		if (num2 < 0)
		{
			Line line2 = new Line();
			line2.m_type = LineType.SECTION;
			line2.m_sectionName = sectionName;
			m_lines.Add(line2);
			line = new Line();
			line.m_type = LineType.ENTRY;
			line.m_sectionName = sectionName;
			line.m_lineKey = text;
			line.m_fullKey = fullKey;
			m_lines.Add(line);
		}
		else
		{
			int i;
			for (i = num2 + 1; i < m_lines.Count; i++)
			{
				Line line3 = m_lines[i];
				if (line3.m_type == LineType.SECTION)
				{
					break;
				}
				if (line3.m_type == LineType.ENTRY && line3.m_lineKey.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					line = line3;
					break;
				}
			}
			if (line == null)
			{
				line = new Line();
				line.m_type = LineType.ENTRY;
				line.m_sectionName = sectionName;
				line.m_lineKey = text;
				line.m_fullKey = fullKey;
				m_lines.Insert(i, line);
			}
		}
		return line;
	}
}
