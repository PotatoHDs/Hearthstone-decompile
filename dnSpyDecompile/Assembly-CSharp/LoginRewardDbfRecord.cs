using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000211 RID: 529
[Serializable]
public class LoginRewardDbfRecord : DbfRecord
{
	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x000956A6 File Offset: 0x000938A6
	[DbfField("MESSAGE")]
	public string Message
	{
		get
		{
			return this.m_message;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000956AE File Offset: 0x000938AE
	[DbfField("STYLE_NAME")]
	public string StyleName
	{
		get
		{
			return this.m_styleName;
		}
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x000956B6 File Offset: 0x000938B6
	public void SetMessage(string v)
	{
		this.m_message = v;
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x000956BF File Offset: 0x000938BF
	public void SetStyleName(string v)
	{
		this.m_styleName = v;
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x000956C8 File Offset: 0x000938C8
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "MESSAGE")
		{
			return this.m_message;
		}
		if (!(name == "STYLE_NAME"))
		{
			return null;
		}
		return this.m_styleName;
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0009571C File Offset: 0x0009391C
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "MESSAGE")
		{
			this.m_message = (string)val;
			return;
		}
		if (!(name == "STYLE_NAME"))
		{
			return;
		}
		this.m_styleName = (string)val;
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x00095778 File Offset: 0x00093978
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "MESSAGE")
		{
			return typeof(string);
		}
		if (!(name == "STYLE_NAME"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x000957D0 File Offset: 0x000939D0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLoginRewardDbfRecords loadRecords = new LoadLoginRewardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x000957E8 File Offset: 0x000939E8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LoginRewardDbfAsset loginRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LoginRewardDbfAsset)) as LoginRewardDbfAsset;
		if (loginRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LoginRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < loginRewardDbfAsset.Records.Count; i++)
		{
			loginRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (loginRewardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001123 RID: 4387
	[SerializeField]
	private string m_message;

	// Token: 0x04001124 RID: 4388
	[SerializeField]
	private string m_styleName;
}
