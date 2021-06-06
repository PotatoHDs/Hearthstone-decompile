using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class FixedRewardDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private Assets.FixedReward.Type m_type = Assets.FixedReward.ParseTypeValue("unknown");

	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_cardPremium;

	[SerializeField]
	private int m_cardBackId;

	[SerializeField]
	private int m_metaActionId;

	[SerializeField]
	private ulong m_metaActionFlags;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("TYPE")]
	public Assets.FixedReward.Type Type => m_type;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	[DbfField("CARD_PREMIUM")]
	public int CardPremium => m_cardPremium;

	[DbfField("CARD_BACK_ID")]
	public int CardBackId => m_cardBackId;

	public CardBackDbfRecord CardBackRecord => GameDbf.CardBack.GetRecord(m_cardBackId);

	[DbfField("META_ACTION_ID")]
	public int MetaActionId => m_metaActionId;

	public FixedRewardActionDbfRecord MetaActionRecord => GameDbf.FixedRewardAction.GetRecord(m_metaActionId);

	[DbfField("META_ACTION_FLAGS")]
	public ulong MetaActionFlags => m_metaActionFlags;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetType(Assets.FixedReward.Type v)
	{
		m_type = v;
	}

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetCardPremium(int v)
	{
		m_cardPremium = v;
	}

	public void SetCardBackId(int v)
	{
		m_cardBackId = v;
	}

	public void SetMetaActionId(int v)
	{
		m_metaActionId = v;
	}

	public void SetMetaActionFlags(ulong v)
	{
		m_metaActionFlags = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"TYPE" => m_type, 
			"CARD_ID" => m_cardId, 
			"CARD_PREMIUM" => m_cardPremium, 
			"CARD_BACK_ID" => m_cardBackId, 
			"META_ACTION_ID" => m_metaActionId, 
			"META_ACTION_FLAGS" => m_metaActionFlags, 
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
				m_type = Assets.FixedReward.Type.UNKNOWN;
			}
			else if (val is Assets.FixedReward.Type || val is int)
			{
				m_type = (Assets.FixedReward.Type)val;
			}
			else if (val is string)
			{
				m_type = Assets.FixedReward.ParseTypeValue((string)val);
			}
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "CARD_PREMIUM":
			m_cardPremium = (int)val;
			break;
		case "CARD_BACK_ID":
			m_cardBackId = (int)val;
			break;
		case "META_ACTION_ID":
			m_metaActionId = (int)val;
			break;
		case "META_ACTION_FLAGS":
			m_metaActionFlags = (ulong)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"TYPE" => typeof(Assets.FixedReward.Type), 
			"CARD_ID" => typeof(int), 
			"CARD_PREMIUM" => typeof(int), 
			"CARD_BACK_ID" => typeof(int), 
			"META_ACTION_ID" => typeof(int), 
			"META_ACTION_FLAGS" => typeof(ulong), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadFixedRewardDbfRecords loadRecords = new LoadFixedRewardDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		FixedRewardDbfAsset fixedRewardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(FixedRewardDbfAsset)) as FixedRewardDbfAsset;
		if (fixedRewardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"FixedRewardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < fixedRewardDbfAsset.Records.Count; i++)
		{
			fixedRewardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = fixedRewardDbfAsset.Records as List<T>;
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
