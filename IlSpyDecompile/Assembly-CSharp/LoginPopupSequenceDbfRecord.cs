using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LoginPopupSequenceDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_eventTiming = "never";

	[DbfField("EVENT_TIMING")]
	public string EventTiming => m_eventTiming;

	public List<LoginPopupSequencePopupDbfRecord> LoginPopupSequencePopup => GameDbf.LoginPopupSequencePopup.GetRecords((LoginPopupSequencePopupDbfRecord r) => r.LoginPopupSequenceId == base.ID);

	public void SetEventTiming(string v)
	{
		m_eventTiming = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "EVENT_TIMING")
			{
				return m_eventTiming;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "EVENT_TIMING")
			{
				m_eventTiming = (string)val;
			}
		}
		else
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "EVENT_TIMING")
			{
				return typeof(string);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginPopupSequenceDbfRecords loadRecords = new LoadLoginPopupSequenceDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginPopupSequenceDbfAsset loginPopupSequenceDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginPopupSequenceDbfAsset)) as LoginPopupSequenceDbfAsset;
		if (loginPopupSequenceDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LoginPopupSequenceDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < loginPopupSequenceDbfAsset.Records.Count; i++)
		{
			loginPopupSequenceDbfAsset.Records[i].StripUnusedLocales();
		}
		records = loginPopupSequenceDbfAsset.Records as List<T>;
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
