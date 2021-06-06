using System;
using System.Collections.Generic;
using System.Reflection;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020007B7 RID: 1975
public abstract class DbfRecord
{
	// Token: 0x17000674 RID: 1652
	// (get) Token: 0x06006D6E RID: 28014 RVA: 0x002347ED File Offset: 0x002329ED
	[DbfField("ID")]
	public int ID
	{
		get
		{
			return this.m_ID;
		}
	}

	// Token: 0x06006D6F RID: 28015 RVA: 0x002347F5 File Offset: 0x002329F5
	public void SetID(int id)
	{
		this.m_ID = id;
	}

	// Token: 0x06006D70 RID: 28016 RVA: 0x00234800 File Offset: 0x00232A00
	public DbfFieldAttribute GetDbfFieldAttribute(string propertyName)
	{
		PropertyInfo property = base.GetType().GetProperty(propertyName);
		if (property != null)
		{
			object[] customAttributes = property.GetCustomAttributes(typeof(DbfFieldAttribute), true);
			if (customAttributes.Length != 0)
			{
				return (DbfFieldAttribute)customAttributes[0];
			}
		}
		return null;
	}

	// Token: 0x06006D71 RID: 28017
	public abstract object GetVar(string varName);

	// Token: 0x06006D72 RID: 28018
	public abstract void SetVar(string varName, object value);

	// Token: 0x06006D73 RID: 28019
	public abstract Type GetVarType(string varName);

	// Token: 0x06006D74 RID: 28020
	public abstract bool LoadRecordsFromAsset<T>(string assetPath, out List<T> records);

	// Token: 0x06006D75 RID: 28021
	public abstract IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler);

	// Token: 0x06006D76 RID: 28022
	public abstract bool SaveRecordsToAsset<T>(string assetPath, List<T> records) where T : DbfRecord, new();

	// Token: 0x06006D77 RID: 28023
	public abstract void StripUnusedLocales();

	// Token: 0x040057F7 RID: 22519
	[SerializeField]
	private int m_ID;
}
