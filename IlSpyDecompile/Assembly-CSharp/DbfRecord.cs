using System;
using System.Collections.Generic;
using System.Reflection;
using Blizzard.T5.Jobs;
using UnityEngine;

public abstract class DbfRecord
{
	[SerializeField]
	private int m_ID;

	[DbfField("ID")]
	public int ID => m_ID;

	public void SetID(int id)
	{
		m_ID = id;
	}

	public DbfFieldAttribute GetDbfFieldAttribute(string propertyName)
	{
		PropertyInfo property = GetType().GetProperty(propertyName);
		if (property != null)
		{
			object[] customAttributes = property.GetCustomAttributes(typeof(DbfFieldAttribute), inherit: true);
			if (customAttributes.Length != 0)
			{
				return (DbfFieldAttribute)customAttributes[0];
			}
		}
		return null;
	}

	public abstract object GetVar(string varName);

	public abstract void SetVar(string varName, object value);

	public abstract Type GetVarType(string varName);

	public abstract bool LoadRecordsFromAsset<T>(string assetPath, out List<T> records);

	public abstract IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler);

	public abstract bool SaveRecordsToAsset<T>(string assetPath, List<T> records) where T : DbfRecord, new();

	public abstract void StripUnusedLocales();
}
