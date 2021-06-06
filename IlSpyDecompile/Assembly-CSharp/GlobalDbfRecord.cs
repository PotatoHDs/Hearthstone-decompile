using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class GlobalDbfRecord : DbfRecord
{
	[SerializeField]
	private Global.AssetFlags m_assetFlags = Global.AssetFlags.NOT_PACKAGED_IN_CLIENT;

	[SerializeField]
	private Global.PresenceStatus m_presenceStatus;

	[SerializeField]
	private Global.Region m_region;

	[SerializeField]
	private Global.FormatType m_formatType;

	[SerializeField]
	private Global.RewardType m_rewardType;

	[SerializeField]
	private Global.CardPremiumLevel m_cardPremiumLevel;

	[SerializeField]
	private Global.BnetGameType m_bnetGameType;

	[SerializeField]
	private Global.SoundCategory m_soundCategory;

	[SerializeField]
	private Global.GameStringCategory m_gameStringCategory;

	[DbfField("ASSET_FLAGS")]
	public Global.AssetFlags AssetFlags => m_assetFlags;

	[DbfField("PRESENCE_STATUS")]
	public Global.PresenceStatus PresenceStatus => m_presenceStatus;

	[DbfField("REGION")]
	public Global.Region Region => m_region;

	[DbfField("FORMAT_TYPE")]
	public Global.FormatType FormatType => m_formatType;

	[DbfField("REWARD_TYPE")]
	public Global.RewardType RewardType => m_rewardType;

	[DbfField("CARD_PREMIUM_LEVEL")]
	public Global.CardPremiumLevel CardPremiumLevel => m_cardPremiumLevel;

	[DbfField("BNET_GAME_TYPE")]
	public Global.BnetGameType BnetGameType => m_bnetGameType;

	[DbfField("SOUND_CATEGORY")]
	public Global.SoundCategory SoundCategory => m_soundCategory;

	[DbfField("GAME_STRING_CATEGORY")]
	public Global.GameStringCategory GameStringCategory => m_gameStringCategory;

	public void SetAssetFlags(Global.AssetFlags v)
	{
		m_assetFlags = v;
	}

	public void SetPresenceStatus(Global.PresenceStatus v)
	{
		m_presenceStatus = v;
	}

	public void SetRegion(Global.Region v)
	{
		m_region = v;
	}

	public void SetFormatType(Global.FormatType v)
	{
		m_formatType = v;
	}

	public void SetRewardType(Global.RewardType v)
	{
		m_rewardType = v;
	}

	public void SetCardPremiumLevel(Global.CardPremiumLevel v)
	{
		m_cardPremiumLevel = v;
	}

	public void SetBnetGameType(Global.BnetGameType v)
	{
		m_bnetGameType = v;
	}

	public void SetSoundCategory(Global.SoundCategory v)
	{
		m_soundCategory = v;
	}

	public void SetGameStringCategory(Global.GameStringCategory v)
	{
		m_gameStringCategory = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ASSET_FLAGS" => m_assetFlags, 
			"PRESENCE_STATUS" => m_presenceStatus, 
			"REGION" => m_region, 
			"FORMAT_TYPE" => m_formatType, 
			"REWARD_TYPE" => m_rewardType, 
			"CARD_PREMIUM_LEVEL" => m_cardPremiumLevel, 
			"BNET_GAME_TYPE" => m_bnetGameType, 
			"SOUND_CATEGORY" => m_soundCategory, 
			"GAME_STRING_CATEGORY" => m_gameStringCategory, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ASSET_FLAGS":
			if (val == null)
			{
				m_assetFlags = Global.AssetFlags.NONE;
			}
			else if (val is Global.AssetFlags || val is int)
			{
				m_assetFlags = (Global.AssetFlags)val;
			}
			else if (val is string)
			{
				m_assetFlags = Global.ParseAssetFlagsValue((string)val);
			}
			break;
		case "PRESENCE_STATUS":
			if (val == null)
			{
				m_presenceStatus = Global.PresenceStatus.LOGIN;
			}
			else if (val is Global.PresenceStatus || val is int)
			{
				m_presenceStatus = (Global.PresenceStatus)val;
			}
			else if (val is string)
			{
				m_presenceStatus = Global.ParsePresenceStatusValue((string)val);
			}
			break;
		case "REGION":
			if (val == null)
			{
				m_region = Global.Region.REGION_UNKNOWN;
			}
			else if (val is Global.Region || val is int)
			{
				m_region = (Global.Region)val;
			}
			else if (val is string)
			{
				m_region = Global.ParseRegionValue((string)val);
			}
			break;
		case "FORMAT_TYPE":
			if (val == null)
			{
				m_formatType = Global.FormatType.FT_UNKNOWN;
			}
			else if (val is Global.FormatType || val is int)
			{
				m_formatType = (Global.FormatType)val;
			}
			else if (val is string)
			{
				m_formatType = Global.ParseFormatTypeValue((string)val);
			}
			break;
		case "REWARD_TYPE":
			if (val == null)
			{
				m_rewardType = Global.RewardType.NONE;
			}
			else if (val is Global.RewardType || val is int)
			{
				m_rewardType = (Global.RewardType)val;
			}
			else if (val is string)
			{
				m_rewardType = Global.ParseRewardTypeValue((string)val);
			}
			break;
		case "CARD_PREMIUM_LEVEL":
			if (val == null)
			{
				m_cardPremiumLevel = Global.CardPremiumLevel.NORMAL;
			}
			else if (val is Global.CardPremiumLevel || val is int)
			{
				m_cardPremiumLevel = (Global.CardPremiumLevel)val;
			}
			else if (val is string)
			{
				m_cardPremiumLevel = Global.ParseCardPremiumLevelValue((string)val);
			}
			break;
		case "BNET_GAME_TYPE":
			if (val == null)
			{
				m_bnetGameType = Global.BnetGameType.BGT_UNKNOWN;
			}
			else if (val is Global.BnetGameType || val is int)
			{
				m_bnetGameType = (Global.BnetGameType)val;
			}
			else if (val is string)
			{
				m_bnetGameType = Global.ParseBnetGameTypeValue((string)val);
			}
			break;
		case "SOUND_CATEGORY":
			if (val == null)
			{
				m_soundCategory = Global.SoundCategory.NONE;
			}
			else if (val is Global.SoundCategory || val is int)
			{
				m_soundCategory = (Global.SoundCategory)val;
			}
			else if (val is string)
			{
				m_soundCategory = Global.ParseSoundCategoryValue((string)val);
			}
			break;
		case "GAME_STRING_CATEGORY":
			if (val == null)
			{
				m_gameStringCategory = Global.GameStringCategory.INVALID;
			}
			else if (val is Global.GameStringCategory || val is int)
			{
				m_gameStringCategory = (Global.GameStringCategory)val;
			}
			else if (val is string)
			{
				m_gameStringCategory = Global.ParseGameStringCategoryValue((string)val);
			}
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ASSET_FLAGS" => typeof(Global.AssetFlags), 
			"PRESENCE_STATUS" => typeof(Global.PresenceStatus), 
			"REGION" => typeof(Global.Region), 
			"FORMAT_TYPE" => typeof(Global.FormatType), 
			"REWARD_TYPE" => typeof(Global.RewardType), 
			"CARD_PREMIUM_LEVEL" => typeof(Global.CardPremiumLevel), 
			"BNET_GAME_TYPE" => typeof(Global.BnetGameType), 
			"SOUND_CATEGORY" => typeof(Global.SoundCategory), 
			"GAME_STRING_CATEGORY" => typeof(Global.GameStringCategory), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGlobalDbfRecords loadRecords = new LoadGlobalDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GlobalDbfAsset globalDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GlobalDbfAsset)) as GlobalDbfAsset;
		if (globalDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"GlobalDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < globalDbfAsset.Records.Count; i++)
		{
			globalDbfAsset.Records[i].StripUnusedLocales();
		}
		records = globalDbfAsset.Records as List<T>;
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
