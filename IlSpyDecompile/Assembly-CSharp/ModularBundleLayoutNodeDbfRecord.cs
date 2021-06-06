using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ModularBundleLayoutNodeDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_nodeLayoutId;

	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private ModularBundleLayoutNode.DisplayType m_displayType = ModularBundleLayoutNode.ParseDisplayTypeValue("invalid");

	[SerializeField]
	private int m_displayData;

	[SerializeField]
	private string m_displayPrefab;

	[SerializeField]
	private DbfLocValue m_displayText;

	[SerializeField]
	private string m_displayTextGlowSize;

	[SerializeField]
	private int m_displayCount;

	[SerializeField]
	private string m_entryAnimation;

	[SerializeField]
	private string m_exitAnimation;

	[SerializeField]
	private string m_entrySound;

	[SerializeField]
	private string m_landingSound;

	[SerializeField]
	private string m_exitSound;

	[SerializeField]
	private int m_nodeIndex;

	[SerializeField]
	private double m_entryDelay;

	[SerializeField]
	private double m_animSpeedMultiplier = 1.0;

	[SerializeField]
	private int m_shakeWeight;

	[DbfField("NODE_LAYOUT_ID")]
	public int NodeLayoutId => m_nodeLayoutId;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("DISPLAY_TYPE")]
	public ModularBundleLayoutNode.DisplayType DisplayType => m_displayType;

	[DbfField("DISPLAY_DATA")]
	public int DisplayData => m_displayData;

	[DbfField("DISPLAY_PREFAB")]
	public string DisplayPrefab => m_displayPrefab;

	[DbfField("DISPLAY_TEXT")]
	public DbfLocValue DisplayText => m_displayText;

	[DbfField("DISPLAY_TEXT_GLOW_SIZE")]
	public string DisplayTextGlowSize => m_displayTextGlowSize;

	[DbfField("DISPLAY_COUNT")]
	public int DisplayCount => m_displayCount;

	[DbfField("ENTRY_ANIMATION")]
	public string EntryAnimation => m_entryAnimation;

	[DbfField("EXIT_ANIMATION")]
	public string ExitAnimation => m_exitAnimation;

	[DbfField("ENTRY_SOUND")]
	public string EntrySound => m_entrySound;

	[DbfField("LANDING_SOUND")]
	public string LandingSound => m_landingSound;

	[DbfField("EXIT_SOUND")]
	public string ExitSound => m_exitSound;

	[DbfField("NODE_INDEX")]
	public int NodeIndex => m_nodeIndex;

	[DbfField("ENTRY_DELAY")]
	public double EntryDelay => m_entryDelay;

	[DbfField("ANIM_SPEED_MULTIPLIER")]
	public double AnimSpeedMultiplier => m_animSpeedMultiplier;

	[DbfField("SHAKE_WEIGHT")]
	public int ShakeWeight => m_shakeWeight;

	public void SetNodeLayoutId(int v)
	{
		m_nodeLayoutId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetDisplayType(ModularBundleLayoutNode.DisplayType v)
	{
		m_displayType = v;
	}

	public void SetDisplayData(int v)
	{
		m_displayData = v;
	}

	public void SetDisplayPrefab(string v)
	{
		m_displayPrefab = v;
	}

	public void SetDisplayText(DbfLocValue v)
	{
		m_displayText = v;
		v.SetDebugInfo(base.ID, "DISPLAY_TEXT");
	}

	public void SetDisplayTextGlowSize(string v)
	{
		m_displayTextGlowSize = v;
	}

	public void SetDisplayCount(int v)
	{
		m_displayCount = v;
	}

	public void SetEntryAnimation(string v)
	{
		m_entryAnimation = v;
	}

	public void SetExitAnimation(string v)
	{
		m_exitAnimation = v;
	}

	public void SetEntrySound(string v)
	{
		m_entrySound = v;
	}

	public void SetLandingSound(string v)
	{
		m_landingSound = v;
	}

	public void SetExitSound(string v)
	{
		m_exitSound = v;
	}

	public void SetNodeIndex(int v)
	{
		m_nodeIndex = v;
	}

	public void SetEntryDelay(double v)
	{
		m_entryDelay = v;
	}

	public void SetAnimSpeedMultiplier(double v)
	{
		m_animSpeedMultiplier = v;
	}

	public void SetShakeWeight(int v)
	{
		m_shakeWeight = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NODE_LAYOUT_ID" => m_nodeLayoutId, 
			"NOTE_DESC" => m_noteDesc, 
			"DISPLAY_TYPE" => m_displayType, 
			"DISPLAY_DATA" => m_displayData, 
			"DISPLAY_PREFAB" => m_displayPrefab, 
			"DISPLAY_TEXT" => m_displayText, 
			"DISPLAY_TEXT_GLOW_SIZE" => m_displayTextGlowSize, 
			"DISPLAY_COUNT" => m_displayCount, 
			"ENTRY_ANIMATION" => m_entryAnimation, 
			"EXIT_ANIMATION" => m_exitAnimation, 
			"ENTRY_SOUND" => m_entrySound, 
			"LANDING_SOUND" => m_landingSound, 
			"EXIT_SOUND" => m_exitSound, 
			"NODE_INDEX" => m_nodeIndex, 
			"ENTRY_DELAY" => m_entryDelay, 
			"ANIM_SPEED_MULTIPLIER" => m_animSpeedMultiplier, 
			"SHAKE_WEIGHT" => m_shakeWeight, 
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
		case "NODE_LAYOUT_ID":
			m_nodeLayoutId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "DISPLAY_TYPE":
			if (val == null)
			{
				m_displayType = ModularBundleLayoutNode.DisplayType.INVALID;
			}
			else if (val is ModularBundleLayoutNode.DisplayType || val is int)
			{
				m_displayType = (ModularBundleLayoutNode.DisplayType)val;
			}
			else if (val is string)
			{
				m_displayType = ModularBundleLayoutNode.ParseDisplayTypeValue((string)val);
			}
			break;
		case "DISPLAY_DATA":
			m_displayData = (int)val;
			break;
		case "DISPLAY_PREFAB":
			m_displayPrefab = (string)val;
			break;
		case "DISPLAY_TEXT":
			m_displayText = (DbfLocValue)val;
			break;
		case "DISPLAY_TEXT_GLOW_SIZE":
			m_displayTextGlowSize = (string)val;
			break;
		case "DISPLAY_COUNT":
			m_displayCount = (int)val;
			break;
		case "ENTRY_ANIMATION":
			m_entryAnimation = (string)val;
			break;
		case "EXIT_ANIMATION":
			m_exitAnimation = (string)val;
			break;
		case "ENTRY_SOUND":
			m_entrySound = (string)val;
			break;
		case "LANDING_SOUND":
			m_landingSound = (string)val;
			break;
		case "EXIT_SOUND":
			m_exitSound = (string)val;
			break;
		case "NODE_INDEX":
			m_nodeIndex = (int)val;
			break;
		case "ENTRY_DELAY":
			m_entryDelay = (double)val;
			break;
		case "ANIM_SPEED_MULTIPLIER":
			m_animSpeedMultiplier = (double)val;
			break;
		case "SHAKE_WEIGHT":
			m_shakeWeight = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NODE_LAYOUT_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"DISPLAY_TYPE" => typeof(ModularBundleLayoutNode.DisplayType), 
			"DISPLAY_DATA" => typeof(int), 
			"DISPLAY_PREFAB" => typeof(string), 
			"DISPLAY_TEXT" => typeof(DbfLocValue), 
			"DISPLAY_TEXT_GLOW_SIZE" => typeof(string), 
			"DISPLAY_COUNT" => typeof(int), 
			"ENTRY_ANIMATION" => typeof(string), 
			"EXIT_ANIMATION" => typeof(string), 
			"ENTRY_SOUND" => typeof(string), 
			"LANDING_SOUND" => typeof(string), 
			"EXIT_SOUND" => typeof(string), 
			"NODE_INDEX" => typeof(int), 
			"ENTRY_DELAY" => typeof(double), 
			"ANIM_SPEED_MULTIPLIER" => typeof(double), 
			"SHAKE_WEIGHT" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleLayoutNodeDbfRecords loadRecords = new LoadModularBundleLayoutNodeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleLayoutNodeDbfAsset modularBundleLayoutNodeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleLayoutNodeDbfAsset)) as ModularBundleLayoutNodeDbfAsset;
		if (modularBundleLayoutNodeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ModularBundleLayoutNodeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < modularBundleLayoutNodeDbfAsset.Records.Count; i++)
		{
			modularBundleLayoutNodeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = modularBundleLayoutNodeDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_displayText.StripUnusedLocales();
	}
}
