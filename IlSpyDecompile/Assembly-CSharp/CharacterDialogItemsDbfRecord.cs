using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CharacterDialogItemsDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_characterDialogId;

	[SerializeField]
	private int m_playOrder;

	[SerializeField]
	private bool m_useInnkeeperQuote;

	[SerializeField]
	private string m_prefabName;

	[SerializeField]
	private string m_audioName;

	[SerializeField]
	private DbfLocValue m_bubbleText;

	[SerializeField]
	private bool m_altBubblePosition;

	[SerializeField]
	private double m_waitBefore;

	[SerializeField]
	private double m_waitAfter;

	[SerializeField]
	private bool m_persistPrefab;

	[SerializeField]
	private string m_achieveEventType;

	[SerializeField]
	private bool m_altPosition;

	[SerializeField]
	private double m_minimumDurationSeconds;

	[SerializeField]
	private double m_localeExtraSeconds;

	[DbfField("CHARACTER_DIALOG_ID")]
	public int CharacterDialogId => m_characterDialogId;

	[DbfField("PLAY_ORDER")]
	public int PlayOrder => m_playOrder;

	[DbfField("USE_INNKEEPER_QUOTE")]
	public bool UseInnkeeperQuote => m_useInnkeeperQuote;

	[DbfField("PREFAB_NAME")]
	public string PrefabName => m_prefabName;

	[DbfField("AUDIO_NAME")]
	public string AudioName => m_audioName;

	[DbfField("BUBBLE_TEXT")]
	public DbfLocValue BubbleText => m_bubbleText;

	[DbfField("ALT_BUBBLE_POSITION")]
	public bool AltBubblePosition => m_altBubblePosition;

	[DbfField("WAIT_BEFORE")]
	public double WaitBefore => m_waitBefore;

	[DbfField("WAIT_AFTER")]
	public double WaitAfter => m_waitAfter;

	[DbfField("PERSIST_PREFAB")]
	public bool PersistPrefab => m_persistPrefab;

	[DbfField("ACHIEVE_EVENT_TYPE")]
	public string AchieveEventType => m_achieveEventType;

	[DbfField("ALT_POSITION")]
	public bool AltPosition => m_altPosition;

	[DbfField("MINIMUM_DURATION_SECONDS")]
	public double MinimumDurationSeconds => m_minimumDurationSeconds;

	[DbfField("LOCALE_EXTRA_SECONDS")]
	public double LocaleExtraSeconds => m_localeExtraSeconds;

	public void SetCharacterDialogId(int v)
	{
		m_characterDialogId = v;
	}

	public void SetPlayOrder(int v)
	{
		m_playOrder = v;
	}

	public void SetUseInnkeeperQuote(bool v)
	{
		m_useInnkeeperQuote = v;
	}

	public void SetPrefabName(string v)
	{
		m_prefabName = v;
	}

	public void SetAudioName(string v)
	{
		m_audioName = v;
	}

	public void SetBubbleText(DbfLocValue v)
	{
		m_bubbleText = v;
		v.SetDebugInfo(base.ID, "BUBBLE_TEXT");
	}

	public void SetAltBubblePosition(bool v)
	{
		m_altBubblePosition = v;
	}

	public void SetWaitBefore(double v)
	{
		m_waitBefore = v;
	}

	public void SetWaitAfter(double v)
	{
		m_waitAfter = v;
	}

	public void SetPersistPrefab(bool v)
	{
		m_persistPrefab = v;
	}

	public void SetAchieveEventType(string v)
	{
		m_achieveEventType = v;
	}

	public void SetAltPosition(bool v)
	{
		m_altPosition = v;
	}

	public void SetMinimumDurationSeconds(double v)
	{
		m_minimumDurationSeconds = v;
	}

	public void SetLocaleExtraSeconds(double v)
	{
		m_localeExtraSeconds = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"CHARACTER_DIALOG_ID" => m_characterDialogId, 
			"PLAY_ORDER" => m_playOrder, 
			"USE_INNKEEPER_QUOTE" => m_useInnkeeperQuote, 
			"PREFAB_NAME" => m_prefabName, 
			"AUDIO_NAME" => m_audioName, 
			"BUBBLE_TEXT" => m_bubbleText, 
			"ALT_BUBBLE_POSITION" => m_altBubblePosition, 
			"WAIT_BEFORE" => m_waitBefore, 
			"WAIT_AFTER" => m_waitAfter, 
			"PERSIST_PREFAB" => m_persistPrefab, 
			"ACHIEVE_EVENT_TYPE" => m_achieveEventType, 
			"ALT_POSITION" => m_altPosition, 
			"MINIMUM_DURATION_SECONDS" => m_minimumDurationSeconds, 
			"LOCALE_EXTRA_SECONDS" => m_localeExtraSeconds, 
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
		case "CHARACTER_DIALOG_ID":
			m_characterDialogId = (int)val;
			break;
		case "PLAY_ORDER":
			m_playOrder = (int)val;
			break;
		case "USE_INNKEEPER_QUOTE":
			m_useInnkeeperQuote = (bool)val;
			break;
		case "PREFAB_NAME":
			m_prefabName = (string)val;
			break;
		case "AUDIO_NAME":
			m_audioName = (string)val;
			break;
		case "BUBBLE_TEXT":
			m_bubbleText = (DbfLocValue)val;
			break;
		case "ALT_BUBBLE_POSITION":
			m_altBubblePosition = (bool)val;
			break;
		case "WAIT_BEFORE":
			m_waitBefore = (double)val;
			break;
		case "WAIT_AFTER":
			m_waitAfter = (double)val;
			break;
		case "PERSIST_PREFAB":
			m_persistPrefab = (bool)val;
			break;
		case "ACHIEVE_EVENT_TYPE":
			m_achieveEventType = (string)val;
			break;
		case "ALT_POSITION":
			m_altPosition = (bool)val;
			break;
		case "MINIMUM_DURATION_SECONDS":
			m_minimumDurationSeconds = (double)val;
			break;
		case "LOCALE_EXTRA_SECONDS":
			m_localeExtraSeconds = (double)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"CHARACTER_DIALOG_ID" => typeof(int), 
			"PLAY_ORDER" => typeof(int), 
			"USE_INNKEEPER_QUOTE" => typeof(bool), 
			"PREFAB_NAME" => typeof(string), 
			"AUDIO_NAME" => typeof(string), 
			"BUBBLE_TEXT" => typeof(DbfLocValue), 
			"ALT_BUBBLE_POSITION" => typeof(bool), 
			"WAIT_BEFORE" => typeof(double), 
			"WAIT_AFTER" => typeof(double), 
			"PERSIST_PREFAB" => typeof(bool), 
			"ACHIEVE_EVENT_TYPE" => typeof(string), 
			"ALT_POSITION" => typeof(bool), 
			"MINIMUM_DURATION_SECONDS" => typeof(double), 
			"LOCALE_EXTRA_SECONDS" => typeof(double), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCharacterDialogItemsDbfRecords loadRecords = new LoadCharacterDialogItemsDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CharacterDialogItemsDbfAsset characterDialogItemsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CharacterDialogItemsDbfAsset)) as CharacterDialogItemsDbfAsset;
		if (characterDialogItemsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CharacterDialogItemsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < characterDialogItemsDbfAsset.Records.Count; i++)
		{
			characterDialogItemsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = characterDialogItemsDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_bubbleText.StripUnusedLocales();
	}
}
