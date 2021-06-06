using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class FixedRewardActionDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private FixedRewardAction.Type m_type = FixedRewardAction.ParseTypeValue("tutorial_progress");

	[SerializeField]
	private int m_wingId;

	[SerializeField]
	private int m_wingProgress;

	[SerializeField]
	private ulong m_wingFlags;

	[SerializeField]
	private int m_classId;

	[SerializeField]
	private int m_totalHeroLevel;

	[SerializeField]
	private int m_heroLevel;

	[SerializeField]
	private int m_tutorialProgress;

	[SerializeField]
	private ulong m_metaActionFlags;

	[SerializeField]
	private int m_achieveId;

	[SerializeField]
	private long m_accountLicenseId;

	[SerializeField]
	private ulong m_accountLicenseFlags;

	[SerializeField]
	private string m_activeEvent = "always";

	[SerializeField]
	private int m_cardId;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("TYPE")]
	public FixedRewardAction.Type Type => m_type;

	[DbfField("WING_ID")]
	public int WingId => m_wingId;

	public WingDbfRecord WingRecord => GameDbf.Wing.GetRecord(m_wingId);

	[DbfField("WING_PROGRESS")]
	public int WingProgress => m_wingProgress;

	[DbfField("WING_FLAGS")]
	public ulong WingFlags => m_wingFlags;

	[DbfField("CLASS_ID")]
	public int ClassId => m_classId;

	public ClassDbfRecord ClassRecord => GameDbf.Class.GetRecord(m_classId);

	[DbfField("TOTAL_HERO_LEVEL")]
	public int TotalHeroLevel => m_totalHeroLevel;

	[DbfField("HERO_LEVEL")]
	public int HeroLevel => m_heroLevel;

	[DbfField("TUTORIAL_PROGRESS")]
	public int TutorialProgress => m_tutorialProgress;

	[DbfField("META_ACTION_FLAGS")]
	public ulong MetaActionFlags => m_metaActionFlags;

	[DbfField("ACHIEVE_ID")]
	public int AchieveId => m_achieveId;

	public AchieveDbfRecord AchieveRecord => GameDbf.Achieve.GetRecord(m_achieveId);

	[DbfField("ACCOUNT_LICENSE_ID")]
	public long AccountLicenseId => m_accountLicenseId;

	[DbfField("ACCOUNT_LICENSE_FLAGS")]
	public ulong AccountLicenseFlags => m_accountLicenseFlags;

	[DbfField("ACTIVE_EVENT")]
	public string ActiveEvent => m_activeEvent;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetType(FixedRewardAction.Type v)
	{
		m_type = v;
	}

	public void SetWingId(int v)
	{
		m_wingId = v;
	}

	public void SetWingProgress(int v)
	{
		m_wingProgress = v;
	}

	public void SetWingFlags(ulong v)
	{
		m_wingFlags = v;
	}

	public void SetClassId(int v)
	{
		m_classId = v;
	}

	public void SetTotalHeroLevel(int v)
	{
		m_totalHeroLevel = v;
	}

	public void SetHeroLevel(int v)
	{
		m_heroLevel = v;
	}

	public void SetTutorialProgress(int v)
	{
		m_tutorialProgress = v;
	}

	public void SetMetaActionFlags(ulong v)
	{
		m_metaActionFlags = v;
	}

	public void SetAchieveId(int v)
	{
		m_achieveId = v;
	}

	public void SetAccountLicenseId(long v)
	{
		m_accountLicenseId = v;
	}

	public void SetAccountLicenseFlags(ulong v)
	{
		m_accountLicenseFlags = v;
	}

	public void SetActiveEvent(string v)
	{
		m_activeEvent = v;
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"TYPE" => m_type, 
			"WING_ID" => m_wingId, 
			"WING_PROGRESS" => m_wingProgress, 
			"WING_FLAGS" => m_wingFlags, 
			"CLASS_ID" => m_classId, 
			"TOTAL_HERO_LEVEL" => m_totalHeroLevel, 
			"HERO_LEVEL" => m_heroLevel, 
			"TUTORIAL_PROGRESS" => m_tutorialProgress, 
			"META_ACTION_FLAGS" => m_metaActionFlags, 
			"ACHIEVE_ID" => m_achieveId, 
			"ACCOUNT_LICENSE_ID" => m_accountLicenseId, 
			"ACCOUNT_LICENSE_FLAGS" => m_accountLicenseFlags, 
			"ACTIVE_EVENT" => m_activeEvent, 
			"CARD_ID" => m_cardId, 
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
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "TYPE":
			if (val == null)
			{
				m_type = FixedRewardAction.Type.TUTORIAL_PROGRESS;
			}
			else if (val is FixedRewardAction.Type || val is int)
			{
				m_type = (FixedRewardAction.Type)val;
			}
			else if (val is string)
			{
				m_type = FixedRewardAction.ParseTypeValue((string)val);
			}
			break;
		case "WING_ID":
			m_wingId = (int)val;
			break;
		case "WING_PROGRESS":
			m_wingProgress = (int)val;
			break;
		case "WING_FLAGS":
			m_wingFlags = (ulong)val;
			break;
		case "CLASS_ID":
			m_classId = (int)val;
			break;
		case "TOTAL_HERO_LEVEL":
			m_totalHeroLevel = (int)val;
			break;
		case "HERO_LEVEL":
			m_heroLevel = (int)val;
			break;
		case "TUTORIAL_PROGRESS":
			m_tutorialProgress = (int)val;
			break;
		case "META_ACTION_FLAGS":
			m_metaActionFlags = (ulong)val;
			break;
		case "ACHIEVE_ID":
			m_achieveId = (int)val;
			break;
		case "ACCOUNT_LICENSE_ID":
			m_accountLicenseId = (long)val;
			break;
		case "ACCOUNT_LICENSE_FLAGS":
			m_accountLicenseFlags = (ulong)val;
			break;
		case "ACTIVE_EVENT":
			m_activeEvent = (string)val;
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"TYPE" => typeof(FixedRewardAction.Type), 
			"WING_ID" => typeof(int), 
			"WING_PROGRESS" => typeof(int), 
			"WING_FLAGS" => typeof(ulong), 
			"CLASS_ID" => typeof(int), 
			"TOTAL_HERO_LEVEL" => typeof(int), 
			"HERO_LEVEL" => typeof(int), 
			"TUTORIAL_PROGRESS" => typeof(int), 
			"META_ACTION_FLAGS" => typeof(ulong), 
			"ACHIEVE_ID" => typeof(int), 
			"ACCOUNT_LICENSE_ID" => typeof(long), 
			"ACCOUNT_LICENSE_FLAGS" => typeof(ulong), 
			"ACTIVE_EVENT" => typeof(string), 
			"CARD_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadFixedRewardActionDbfRecords loadRecords = new LoadFixedRewardActionDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		FixedRewardActionDbfAsset fixedRewardActionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(FixedRewardActionDbfAsset)) as FixedRewardActionDbfAsset;
		if (fixedRewardActionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"FixedRewardActionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < fixedRewardActionDbfAsset.Records.Count; i++)
		{
			fixedRewardActionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = fixedRewardActionDbfAsset.Records as List<T>;
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
