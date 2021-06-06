using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class HiddenLicenseDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_accountLicenseId;

	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private bool m_isBlocking = true;

	[DbfField("ACCOUNT_LICENSE_ID")]
	public int AccountLicenseId => m_accountLicenseId;

	public AccountLicenseDbfRecord AccountLicenseRecord => GameDbf.AccountLicense.GetRecord(m_accountLicenseId);

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("IS_BLOCKING")]
	public bool IsBlocking => m_isBlocking;

	public void SetAccountLicenseId(int v)
	{
		m_accountLicenseId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetIsBlocking(bool v)
	{
		m_isBlocking = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACCOUNT_LICENSE_ID" => m_accountLicenseId, 
			"NOTE_DESC" => m_noteDesc, 
			"IS_BLOCKING" => m_isBlocking, 
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
		case "ACCOUNT_LICENSE_ID":
			m_accountLicenseId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "IS_BLOCKING":
			m_isBlocking = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACCOUNT_LICENSE_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"IS_BLOCKING" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadHiddenLicenseDbfRecords loadRecords = new LoadHiddenLicenseDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		HiddenLicenseDbfAsset hiddenLicenseDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(HiddenLicenseDbfAsset)) as HiddenLicenseDbfAsset;
		if (hiddenLicenseDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"HiddenLicenseDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < hiddenLicenseDbfAsset.Records.Count; i++)
		{
			hiddenLicenseDbfAsset.Records[i].StripUnusedLocales();
		}
		records = hiddenLicenseDbfAsset.Records as List<T>;
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
