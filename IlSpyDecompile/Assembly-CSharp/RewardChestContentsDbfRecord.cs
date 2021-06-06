using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardChestContentsDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_rewardChestId;

	[SerializeField]
	private int m_rewardLevel;

	[SerializeField]
	private int m_bag1;

	[SerializeField]
	private int m_bag2;

	[SerializeField]
	private int m_bag3;

	[SerializeField]
	private int m_bag4;

	[SerializeField]
	private int m_bag5;

	[SerializeField]
	private int m_bag6;

	[SerializeField]
	private string m_iconTexture;

	[SerializeField]
	private double m_iconOffsetX;

	[SerializeField]
	private double m_iconOffsetY;

	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId => m_rewardChestId;

	public RewardChestDbfRecord RewardChestRecord => GameDbf.RewardChest.GetRecord(m_rewardChestId);

	[DbfField("REWARD_LEVEL")]
	public int RewardLevel => m_rewardLevel;

	[DbfField("BAG1")]
	public int Bag1 => m_bag1;

	[DbfField("BAG2")]
	public int Bag2 => m_bag2;

	[DbfField("BAG3")]
	public int Bag3 => m_bag3;

	[DbfField("BAG4")]
	public int Bag4 => m_bag4;

	[DbfField("BAG5")]
	public int Bag5 => m_bag5;

	[DbfField("BAG6")]
	public int Bag6 => m_bag6;

	[DbfField("ICON_TEXTURE")]
	public string IconTexture => m_iconTexture;

	[DbfField("ICON_OFFSET_X")]
	public double IconOffsetX => m_iconOffsetX;

	[DbfField("ICON_OFFSET_Y")]
	public double IconOffsetY => m_iconOffsetY;

	public void SetRewardChestId(int v)
	{
		m_rewardChestId = v;
	}

	public void SetRewardLevel(int v)
	{
		m_rewardLevel = v;
	}

	public void SetBag1(int v)
	{
		m_bag1 = v;
	}

	public void SetBag2(int v)
	{
		m_bag2 = v;
	}

	public void SetBag3(int v)
	{
		m_bag3 = v;
	}

	public void SetBag4(int v)
	{
		m_bag4 = v;
	}

	public void SetBag5(int v)
	{
		m_bag5 = v;
	}

	public void SetBag6(int v)
	{
		m_bag6 = v;
	}

	public void SetIconTexture(string v)
	{
		m_iconTexture = v;
	}

	public void SetIconOffsetX(double v)
	{
		m_iconOffsetX = v;
	}

	public void SetIconOffsetY(double v)
	{
		m_iconOffsetY = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"REWARD_CHEST_ID" => m_rewardChestId, 
			"REWARD_LEVEL" => m_rewardLevel, 
			"BAG1" => m_bag1, 
			"BAG2" => m_bag2, 
			"BAG3" => m_bag3, 
			"BAG4" => m_bag4, 
			"BAG5" => m_bag5, 
			"BAG6" => m_bag6, 
			"ICON_TEXTURE" => m_iconTexture, 
			"ICON_OFFSET_X" => m_iconOffsetX, 
			"ICON_OFFSET_Y" => m_iconOffsetY, 
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
		case "REWARD_CHEST_ID":
			m_rewardChestId = (int)val;
			break;
		case "REWARD_LEVEL":
			m_rewardLevel = (int)val;
			break;
		case "BAG1":
			m_bag1 = (int)val;
			break;
		case "BAG2":
			m_bag2 = (int)val;
			break;
		case "BAG3":
			m_bag3 = (int)val;
			break;
		case "BAG4":
			m_bag4 = (int)val;
			break;
		case "BAG5":
			m_bag5 = (int)val;
			break;
		case "BAG6":
			m_bag6 = (int)val;
			break;
		case "ICON_TEXTURE":
			m_iconTexture = (string)val;
			break;
		case "ICON_OFFSET_X":
			m_iconOffsetX = (double)val;
			break;
		case "ICON_OFFSET_Y":
			m_iconOffsetY = (double)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"REWARD_CHEST_ID" => typeof(int), 
			"REWARD_LEVEL" => typeof(int), 
			"BAG1" => typeof(int), 
			"BAG2" => typeof(int), 
			"BAG3" => typeof(int), 
			"BAG4" => typeof(int), 
			"BAG5" => typeof(int), 
			"BAG6" => typeof(int), 
			"ICON_TEXTURE" => typeof(string), 
			"ICON_OFFSET_X" => typeof(double), 
			"ICON_OFFSET_Y" => typeof(double), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardChestContentsDbfRecords loadRecords = new LoadRewardChestContentsDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardChestContentsDbfAsset rewardChestContentsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardChestContentsDbfAsset)) as RewardChestContentsDbfAsset;
		if (rewardChestContentsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardChestContentsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardChestContentsDbfAsset.Records.Count; i++)
		{
			rewardChestContentsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardChestContentsDbfAsset.Records as List<T>;
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
