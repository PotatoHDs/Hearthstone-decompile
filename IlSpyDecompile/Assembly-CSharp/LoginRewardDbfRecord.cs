using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LoginRewardDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_message;

	[SerializeField]
	private string m_styleName;

	[DbfField("MESSAGE")]
	public string Message => m_message;

	[DbfField("STYLE_NAME")]
	public string StyleName => m_styleName;

	public void SetMessage(string v)
	{
		m_message = v;
	}

	public void SetStyleName(string v)
	{
		m_styleName = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"MESSAGE" => m_message, 
			"STYLE_NAME" => m_styleName, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "MESSAGE":
			m_message = (string)val;
			break;
		case "STYLE_NAME":
			m_styleName = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"MESSAGE" => typeof(string), 
			"STYLE_NAME" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginRewardDbfRecords loadRecords = new LoadLoginRewardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginRewardDbfAsset loginRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginRewardDbfAsset)) as LoginRewardDbfAsset;
		if (loginRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LoginRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < loginRewardDbfAsset.Records.Count; i++)
		{
			loginRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = loginRewardDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
