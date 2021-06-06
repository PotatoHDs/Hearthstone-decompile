using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using UnityEngine;

public class DbfXml
{
	public static bool Load<T>(string xmlFile, Dbf<T> dbf) where T : DbfRecord, new()
	{
		if (File.Exists(xmlFile))
		{
			using (XmlReader xmlReader = XmlReader.Create(xmlFile))
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Record")
					{
						LoadRecord(xmlReader.ReadSubtree(), dbf);
					}
				}
				return true;
			}
		}
		return false;
	}

	public static IEnumerator<IAsyncJobResult> Job_LoadAsync<T>(string xmlFile, Dbf<T> dbf) where T : DbfRecord, new()
	{
		string name = dbf.GetName();
		Dbf<T> threadSafeDBF = new Dbf<T>(name);
		yield return new JobDefinition($"DbfXml.LoadAsyncFromDisk[{name}]", Job_LoadAsyncFromDisk(xmlFile, threadSafeDBF), JobFlags.StartImmediately | JobFlags.UseWorkerThread);
		lock (threadSafeDBF)
		{
			dbf.CopyRecords(threadSafeDBF);
		}
	}

	public static IEnumerator<IAsyncJobResult> Job_LoadAsyncFromDisk<T>(string xmlFile, Dbf<T> dbf) where T : DbfRecord, new()
	{
		lock (dbf)
		{
			Load(xmlFile, dbf);
		}
		yield break;
	}

	public static void LoadRecord<T>(XmlReader reader, Dbf<T> dbf, bool hideDbfLocDebugInfo = false) where T : DbfRecord, new()
	{
		DbfRecord dbfRecord = dbf.CreateNewRecord();
		while (reader.Read())
		{
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "Field" || reader.IsEmptyElement)
			{
				continue;
			}
			string text = reader["column"];
			Type varType = dbfRecord.GetVarType(text);
			if (varType != null)
			{
				try
				{
					string text2;
					if (varType == typeof(DbfLocValue))
					{
						dbfRecord.SetVar(text, LoadLocalizedString(reader["loc_ID"], reader.ReadSubtree(), hideDbfLocDebugInfo));
					}
					else if (varType == typeof(bool))
					{
						string strVal = reader.ReadElementContentAsString();
						dbfRecord.SetVar(text, GeneralUtils.ForceBool(strVal));
					}
					else if (varType.IsEnum)
					{
						text2 = reader.ReadElementContentAs(typeof(string), null) as string;
						Type underlyingType = Enum.GetUnderlyingType(varType);
						if (underlyingType == typeof(int))
						{
							if (!int.TryParse(text2, out var result))
							{
								goto IL_01af;
							}
							dbfRecord.SetVar(text, result);
						}
						else if (underlyingType == typeof(uint))
						{
							if (!uint.TryParse(text2, out var result2))
							{
								goto IL_01af;
							}
							dbfRecord.SetVar(text, result2);
						}
						else if (underlyingType == typeof(long))
						{
							if (!long.TryParse(text2, out var result3))
							{
								goto IL_01af;
							}
							dbfRecord.SetVar(text, result3);
						}
						else
						{
							if (!(underlyingType == typeof(ulong)) || !ulong.TryParse(text2, out var result4))
							{
								goto IL_01af;
							}
							dbfRecord.SetVar(text, result4);
						}
					}
					else if (varType == typeof(ulong))
					{
						dbfRecord.SetVar(text, ulong.Parse(reader.ReadElementContentAsString()));
					}
					else
					{
						dbfRecord.SetVar(text, reader.ReadElementContentAs(varType, null));
					}
					goto end_IL_0058;
					IL_01af:
					dbfRecord.SetVar(text, text2);
					end_IL_0058:;
				}
				catch (Exception ex)
				{
					Debug.LogErrorFormat("Failed to read record id={0} column={1} with varType={2} exception={3}", dbfRecord.ID, text, varType, ex.ToString());
					throw;
				}
			}
			else
			{
				Debug.LogErrorFormat("Type is not defined for column {0}, dbf={1}. Try \"Build->Generate DBFs and Code\"", text, dbfRecord.GetType().Name);
			}
		}
		dbf.AddRecord(dbfRecord);
	}

	public static DbfLocValue LoadLocalizedString(string locIdStr, XmlReader reader, bool hideDebugInfo = false)
	{
		reader.Read();
		DbfLocValue dbfLocValue = new DbfLocValue(hideDebugInfo);
		if (!string.IsNullOrEmpty(locIdStr))
		{
			int result = 0;
			if (int.TryParse(locIdStr, out result))
			{
				dbfLocValue.SetLocId(result);
			}
		}
		while (reader.Read())
		{
			if (reader.NodeType == XmlNodeType.Element)
			{
				string name = reader.Name;
				string text = reader.ReadElementContentAsString();
				Locale @enum;
				try
				{
					@enum = EnumUtils.GetEnum<Locale>(name);
				}
				catch (ArgumentException)
				{
					continue;
				}
				dbfLocValue.SetString(@enum, TextUtils.DecodeWhitespaces(text));
			}
		}
		return dbfLocValue;
	}
}
