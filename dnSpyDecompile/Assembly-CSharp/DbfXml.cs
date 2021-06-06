using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
public class DbfXml
{
	// Token: 0x06006D81 RID: 28033 RVA: 0x00234E84 File Offset: 0x00233084
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
						DbfXml.LoadRecord<T>(xmlReader.ReadSubtree(), dbf, false);
					}
				}
				return true;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06006D82 RID: 28034 RVA: 0x00234EF4 File Offset: 0x002330F4
	public static IEnumerator<IAsyncJobResult> Job_LoadAsync<T>(string xmlFile, Dbf<T> dbf) where T : DbfRecord, new()
	{
		string name = dbf.GetName();
		Dbf<T> threadSafeDBF = new Dbf<T>(name);
		yield return new JobDefinition(string.Format("DbfXml.LoadAsyncFromDisk[{0}]", name), DbfXml.Job_LoadAsyncFromDisk<T>(xmlFile, threadSafeDBF), JobFlags.StartImmediately | JobFlags.UseWorkerThread, Array.Empty<IJobDependency>());
		Dbf<T> obj = threadSafeDBF;
		lock (obj)
		{
			dbf.CopyRecords(threadSafeDBF);
			yield break;
		}
		yield break;
	}

	// Token: 0x06006D83 RID: 28035 RVA: 0x00234F0A File Offset: 0x0023310A
	public static IEnumerator<IAsyncJobResult> Job_LoadAsyncFromDisk<T>(string xmlFile, Dbf<T> dbf) where T : DbfRecord, new()
	{
		lock (dbf)
		{
			DbfXml.Load<T>(xmlFile, dbf);
			yield break;
		}
		yield break;
	}

	// Token: 0x06006D84 RID: 28036 RVA: 0x00234F20 File Offset: 0x00233120
	public static void LoadRecord<T>(XmlReader reader, Dbf<T> dbf, bool hideDbfLocDebugInfo = false) where T : DbfRecord, new()
	{
		DbfRecord dbfRecord = dbf.CreateNewRecord();
		while (reader.Read())
		{
			if (reader.NodeType == XmlNodeType.Element && !(reader.Name != "Field") && !reader.IsEmptyElement)
			{
				string text = reader["column"];
				Type varType = dbfRecord.GetVarType(text);
				if (varType != null)
				{
					try
					{
						if (varType == typeof(DbfLocValue))
						{
							dbfRecord.SetVar(text, DbfXml.LoadLocalizedString(reader["loc_ID"], reader.ReadSubtree(), hideDbfLocDebugInfo));
						}
						else if (varType == typeof(bool))
						{
							string strVal = reader.ReadElementContentAsString();
							dbfRecord.SetVar(text, GeneralUtils.ForceBool(strVal));
						}
						else if (varType.IsEnum)
						{
							string text2 = reader.ReadElementContentAs(typeof(string), null) as string;
							Type underlyingType = Enum.GetUnderlyingType(varType);
							ulong num4;
							if (underlyingType == typeof(int))
							{
								int num;
								if (int.TryParse(text2, out num))
								{
									dbfRecord.SetVar(text, num);
									continue;
								}
							}
							else if (underlyingType == typeof(uint))
							{
								uint num2;
								if (uint.TryParse(text2, out num2))
								{
									dbfRecord.SetVar(text, num2);
									continue;
								}
							}
							else if (underlyingType == typeof(long))
							{
								long num3;
								if (long.TryParse(text2, out num3))
								{
									dbfRecord.SetVar(text, num3);
									continue;
								}
							}
							else if (underlyingType == typeof(ulong) && ulong.TryParse(text2, out num4))
							{
								dbfRecord.SetVar(text, num4);
								continue;
							}
							dbfRecord.SetVar(text, text2);
						}
						else if (varType == typeof(ulong))
						{
							dbfRecord.SetVar(text, ulong.Parse(reader.ReadElementContentAsString()));
						}
						else
						{
							dbfRecord.SetVar(text, reader.ReadElementContentAs(varType, null));
						}
						continue;
					}
					catch (Exception ex)
					{
						Debug.LogErrorFormat("Failed to read record id={0} column={1} with varType={2} exception={3}", new object[]
						{
							dbfRecord.ID,
							text,
							varType,
							ex.ToString()
						});
						throw;
					}
				}
				Debug.LogErrorFormat("Type is not defined for column {0}, dbf={1}. Try \"Build->Generate DBFs and Code\"", new object[]
				{
					text,
					dbfRecord.GetType().Name
				});
			}
		}
		dbf.AddRecord(dbfRecord);
	}

	// Token: 0x06006D85 RID: 28037 RVA: 0x002351A8 File Offset: 0x002333A8
	public static DbfLocValue LoadLocalizedString(string locIdStr, XmlReader reader, bool hideDebugInfo = false)
	{
		reader.Read();
		DbfLocValue dbfLocValue = new DbfLocValue(hideDebugInfo);
		if (!string.IsNullOrEmpty(locIdStr))
		{
			int locId = 0;
			if (int.TryParse(locIdStr, out locId))
			{
				dbfLocValue.SetLocId(locId);
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
