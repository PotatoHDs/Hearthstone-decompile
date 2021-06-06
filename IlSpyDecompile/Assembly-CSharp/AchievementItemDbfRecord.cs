using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchievementItemDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_achievementSectionId;

	[SerializeField]
	private int m_achievementId;

	[SerializeField]
	private int m_sortOrder;

	[DbfField("ACHIEVEMENT_SECTION_ID")]
	public int AchievementSectionId => m_achievementSectionId;

	[DbfField("ACHIEVEMENT")]
	public int Achievement => m_achievementId;

	public AchievementDbfRecord AchievementRecord => GameDbf.Achievement.GetRecord(m_achievementId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	public void SetAchievementSectionId(int v)
	{
		m_achievementSectionId = v;
	}

	public void SetAchievement(int v)
	{
		m_achievementId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ACHIEVEMENT_SECTION_ID" => m_achievementSectionId, 
			"ACHIEVEMENT" => m_achievementId, 
			"SORT_ORDER" => m_sortOrder, 
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
		case "ACHIEVEMENT_SECTION_ID":
			m_achievementSectionId = (int)val;
			break;
		case "ACHIEVEMENT":
			m_achievementId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ACHIEVEMENT_SECTION_ID" => typeof(int), 
			"ACHIEVEMENT" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementItemDbfRecords loadRecords = new LoadAchievementItemDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementItemDbfAsset achievementItemDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementItemDbfAsset)) as AchievementItemDbfAsset;
		if (achievementItemDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchievementItemDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achievementItemDbfAsset.Records.Count; i++)
		{
			achievementItemDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achievementItemDbfAsset.Records as List<T>;
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
