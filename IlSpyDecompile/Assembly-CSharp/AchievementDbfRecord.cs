using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchievementDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_achievementSectionId;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private bool m_enabled = true;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private Assets.Achievement.AchievementVisibility m_achievementVisibility;

	[SerializeField]
	private int m_quota;

	[SerializeField]
	private bool m_allowExceedQuota;

	[SerializeField]
	private int m_triggerId;

	[SerializeField]
	private int m_points = 1;

	[SerializeField]
	private int m_rewardTrackXp;

	[SerializeField]
	private int m_rewardListId;

	[SerializeField]
	private int m_nextTierId;

	[DbfField("ACHIEVEMENT_SECTION")]
	public int AchievementSection => m_achievementSectionId;

	public AchievementSectionDbfRecord AchievementSectionRecord => GameDbf.AchievementSection.GetRecord(m_achievementSectionId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("ENABLED")]
	public bool Enabled => m_enabled;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("ACHIEVEMENT_VISIBILITY")]
	public Assets.Achievement.AchievementVisibility AchievementVisibility => m_achievementVisibility;

	[DbfField("QUOTA")]
	public int Quota => m_quota;

	[DbfField("ALLOW_EXCEED_QUOTA")]
	public bool AllowExceedQuota => m_allowExceedQuota;

	[DbfField("TRIGGER")]
	public int Trigger => m_triggerId;

	public TriggerDbfRecord TriggerRecord => GameDbf.Trigger.GetRecord(m_triggerId);

	[DbfField("POINTS")]
	public int Points => m_points;

	[DbfField("REWARD_TRACK_XP")]
	public int RewardTrackXp => m_rewardTrackXp;

	[DbfField("REWARD_LIST")]
	public int RewardList => m_rewardListId;

	public RewardListDbfRecord RewardListRecord => GameDbf.RewardList.GetRecord(m_rewardListId);

	[DbfField("NEXT_TIER")]
	public int NextTier => m_nextTierId;

	public AchievementDbfRecord NextTierRecord => GameDbf.Achievement.GetRecord(m_nextTierId);

	public void SetAchievementSection(int v)
	{
		m_achievementSectionId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetEnabled(bool v)
	{
		m_enabled = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetAchievementVisibility(Assets.Achievement.AchievementVisibility v)
	{
		m_achievementVisibility = v;
	}

	public void SetQuota(int v)
	{
		m_quota = v;
	}

	public void SetAllowExceedQuota(bool v)
	{
		m_allowExceedQuota = v;
	}

	public void SetTrigger(int v)
	{
		m_triggerId = v;
	}

	public void SetPoints(int v)
	{
		m_points = v;
	}

	public void SetRewardTrackXp(int v)
	{
		m_rewardTrackXp = v;
	}

	public void SetRewardList(int v)
	{
		m_rewardListId = v;
	}

	public void SetNextTier(int v)
	{
		m_nextTierId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACHIEVEMENT_SECTION" => m_achievementSectionId, 
			"SORT_ORDER" => m_sortOrder, 
			"ENABLED" => m_enabled, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"ACHIEVEMENT_VISIBILITY" => m_achievementVisibility, 
			"QUOTA" => m_quota, 
			"ALLOW_EXCEED_QUOTA" => m_allowExceedQuota, 
			"TRIGGER" => m_triggerId, 
			"POINTS" => m_points, 
			"REWARD_TRACK_XP" => m_rewardTrackXp, 
			"REWARD_LIST" => m_rewardListId, 
			"NEXT_TIER" => m_nextTierId, 
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
		case "ACHIEVEMENT_SECTION":
			m_achievementSectionId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "ENABLED":
			m_enabled = (bool)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "ACHIEVEMENT_VISIBILITY":
			if (val == null)
			{
				m_achievementVisibility = Assets.Achievement.AchievementVisibility.VISIBLE;
			}
			else if (val is Assets.Achievement.AchievementVisibility || val is int)
			{
				m_achievementVisibility = (Assets.Achievement.AchievementVisibility)val;
			}
			else if (val is string)
			{
				m_achievementVisibility = Assets.Achievement.ParseAchievementVisibilityValue((string)val);
			}
			break;
		case "QUOTA":
			m_quota = (int)val;
			break;
		case "ALLOW_EXCEED_QUOTA":
			m_allowExceedQuota = (bool)val;
			break;
		case "TRIGGER":
			m_triggerId = (int)val;
			break;
		case "POINTS":
			m_points = (int)val;
			break;
		case "REWARD_TRACK_XP":
			m_rewardTrackXp = (int)val;
			break;
		case "REWARD_LIST":
			m_rewardListId = (int)val;
			break;
		case "NEXT_TIER":
			m_nextTierId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACHIEVEMENT_SECTION" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			"ENABLED" => typeof(bool), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"ACHIEVEMENT_VISIBILITY" => typeof(Assets.Achievement.AchievementVisibility), 
			"QUOTA" => typeof(int), 
			"ALLOW_EXCEED_QUOTA" => typeof(bool), 
			"TRIGGER" => typeof(int), 
			"POINTS" => typeof(int), 
			"REWARD_TRACK_XP" => typeof(int), 
			"REWARD_LIST" => typeof(int), 
			"NEXT_TIER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementDbfRecords loadRecords = new LoadAchievementDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementDbfAsset achievementDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementDbfAsset)) as AchievementDbfAsset;
		if (achievementDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchievementDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achievementDbfAsset.Records.Count; i++)
		{
			achievementDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achievementDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
	}
}
