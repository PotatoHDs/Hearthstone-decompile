using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200020B RID: 523
[Serializable]
public class LoginPopupSequenceDbfRecord : DbfRecord
{
	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00094AB2 File Offset: 0x00092CB2
	[DbfField("EVENT_TIMING")]
	public string EventTiming
	{
		get
		{
			return this.m_eventTiming;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00094ABA File Offset: 0x00092CBA
	public List<LoginPopupSequencePopupDbfRecord> LoginPopupSequencePopup
	{
		get
		{
			return GameDbf.LoginPopupSequencePopup.GetRecords((LoginPopupSequencePopupDbfRecord r) => r.LoginPopupSequenceId == base.ID, -1);
		}
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00094AD3 File Offset: 0x00092CD3
	public void SetEventTiming(string v)
	{
		this.m_eventTiming = v;
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x00094ADC File Offset: 0x00092CDC
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "EVENT_TIMING"))
		{
			return null;
		}
		return this.m_eventTiming;
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x00094B0E File Offset: 0x00092D0E
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "EVENT_TIMING"))
		{
			return;
		}
		this.m_eventTiming = (string)val;
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x00094B44 File Offset: 0x00092D44
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "EVENT_TIMING"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x00094B79 File Offset: 0x00092D79
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginPopupSequenceDbfRecords loadRecords = new LoadLoginPopupSequenceDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00094B90 File Offset: 0x00092D90
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginPopupSequenceDbfAsset loginPopupSequenceDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginPopupSequenceDbfAsset)) as LoginPopupSequenceDbfAsset;
		if (loginPopupSequenceDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LoginPopupSequenceDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < loginPopupSequenceDbfAsset.Records.Count; i++)
		{
			loginPopupSequenceDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (loginPopupSequenceDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001112 RID: 4370
	[SerializeField]
	private string m_eventTiming = "never";
}
