using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000220 RID: 544
[Serializable]
public class ModularBundleLayoutNodeDbfRecord : DbfRecord
{
	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00097286 File Offset: 0x00095486
	[DbfField("NODE_LAYOUT_ID")]
	public int NodeLayoutId
	{
		get
		{
			return this.m_nodeLayoutId;
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0009728E File Offset: 0x0009548E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00097296 File Offset: 0x00095496
	[DbfField("DISPLAY_TYPE")]
	public ModularBundleLayoutNode.DisplayType DisplayType
	{
		get
		{
			return this.m_displayType;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0009729E File Offset: 0x0009549E
	[DbfField("DISPLAY_DATA")]
	public int DisplayData
	{
		get
		{
			return this.m_displayData;
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001D7F RID: 7551 RVA: 0x000972A6 File Offset: 0x000954A6
	[DbfField("DISPLAY_PREFAB")]
	public string DisplayPrefab
	{
		get
		{
			return this.m_displayPrefab;
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000972AE File Offset: 0x000954AE
	[DbfField("DISPLAY_TEXT")]
	public DbfLocValue DisplayText
	{
		get
		{
			return this.m_displayText;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000972B6 File Offset: 0x000954B6
	[DbfField("DISPLAY_TEXT_GLOW_SIZE")]
	public string DisplayTextGlowSize
	{
		get
		{
			return this.m_displayTextGlowSize;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000972BE File Offset: 0x000954BE
	[DbfField("DISPLAY_COUNT")]
	public int DisplayCount
	{
		get
		{
			return this.m_displayCount;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06001D83 RID: 7555 RVA: 0x000972C6 File Offset: 0x000954C6
	[DbfField("ENTRY_ANIMATION")]
	public string EntryAnimation
	{
		get
		{
			return this.m_entryAnimation;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000972CE File Offset: 0x000954CE
	[DbfField("EXIT_ANIMATION")]
	public string ExitAnimation
	{
		get
		{
			return this.m_exitAnimation;
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000972D6 File Offset: 0x000954D6
	[DbfField("ENTRY_SOUND")]
	public string EntrySound
	{
		get
		{
			return this.m_entrySound;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000972DE File Offset: 0x000954DE
	[DbfField("LANDING_SOUND")]
	public string LandingSound
	{
		get
		{
			return this.m_landingSound;
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000972E6 File Offset: 0x000954E6
	[DbfField("EXIT_SOUND")]
	public string ExitSound
	{
		get
		{
			return this.m_exitSound;
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06001D88 RID: 7560 RVA: 0x000972EE File Offset: 0x000954EE
	[DbfField("NODE_INDEX")]
	public int NodeIndex
	{
		get
		{
			return this.m_nodeIndex;
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06001D89 RID: 7561 RVA: 0x000972F6 File Offset: 0x000954F6
	[DbfField("ENTRY_DELAY")]
	public double EntryDelay
	{
		get
		{
			return this.m_entryDelay;
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000972FE File Offset: 0x000954FE
	[DbfField("ANIM_SPEED_MULTIPLIER")]
	public double AnimSpeedMultiplier
	{
		get
		{
			return this.m_animSpeedMultiplier;
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00097306 File Offset: 0x00095506
	[DbfField("SHAKE_WEIGHT")]
	public int ShakeWeight
	{
		get
		{
			return this.m_shakeWeight;
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x0009730E File Offset: 0x0009550E
	public void SetNodeLayoutId(int v)
	{
		this.m_nodeLayoutId = v;
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x00097317 File Offset: 0x00095517
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x00097320 File Offset: 0x00095520
	public void SetDisplayType(ModularBundleLayoutNode.DisplayType v)
	{
		this.m_displayType = v;
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x00097329 File Offset: 0x00095529
	public void SetDisplayData(int v)
	{
		this.m_displayData = v;
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x00097332 File Offset: 0x00095532
	public void SetDisplayPrefab(string v)
	{
		this.m_displayPrefab = v;
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x0009733B File Offset: 0x0009553B
	public void SetDisplayText(DbfLocValue v)
	{
		this.m_displayText = v;
		v.SetDebugInfo(base.ID, "DISPLAY_TEXT");
	}

	// Token: 0x06001D92 RID: 7570 RVA: 0x00097355 File Offset: 0x00095555
	public void SetDisplayTextGlowSize(string v)
	{
		this.m_displayTextGlowSize = v;
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x0009735E File Offset: 0x0009555E
	public void SetDisplayCount(int v)
	{
		this.m_displayCount = v;
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00097367 File Offset: 0x00095567
	public void SetEntryAnimation(string v)
	{
		this.m_entryAnimation = v;
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x00097370 File Offset: 0x00095570
	public void SetExitAnimation(string v)
	{
		this.m_exitAnimation = v;
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00097379 File Offset: 0x00095579
	public void SetEntrySound(string v)
	{
		this.m_entrySound = v;
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x00097382 File Offset: 0x00095582
	public void SetLandingSound(string v)
	{
		this.m_landingSound = v;
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x0009738B File Offset: 0x0009558B
	public void SetExitSound(string v)
	{
		this.m_exitSound = v;
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x00097394 File Offset: 0x00095594
	public void SetNodeIndex(int v)
	{
		this.m_nodeIndex = v;
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x0009739D File Offset: 0x0009559D
	public void SetEntryDelay(double v)
	{
		this.m_entryDelay = v;
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x000973A6 File Offset: 0x000955A6
	public void SetAnimSpeedMultiplier(double v)
	{
		this.m_animSpeedMultiplier = v;
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x000973AF File Offset: 0x000955AF
	public void SetShakeWeight(int v)
	{
		this.m_shakeWeight = v;
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x000973B8 File Offset: 0x000955B8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2452196822U)
		{
			if (num <= 1180807003U)
			{
				if (num <= 568925309U)
				{
					if (num != 233364829U)
					{
						if (num == 568925309U)
						{
							if (name == "DISPLAY_TEXT")
							{
								return this.m_displayText;
							}
						}
					}
					else if (name == "DISPLAY_COUNT")
					{
						return this.m_displayCount;
					}
				}
				else if (num != 1083713952U)
				{
					if (num == 1180807003U)
					{
						if (name == "ENTRY_SOUND")
						{
							return this.m_entrySound;
						}
					}
				}
				else if (name == "LANDING_SOUND")
				{
					return this.m_landingSound;
				}
			}
			else if (num <= 2054175731U)
			{
				if (num != 1458105184U)
				{
					if (num == 2054175731U)
					{
						if (name == "ENTRY_DELAY")
						{
							return this.m_entryDelay;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (num != 2277209517U)
			{
				if (num != 2396003294U)
				{
					if (num == 2452196822U)
					{
						if (name == "ANIM_SPEED_MULTIPLIER")
						{
							return this.m_animSpeedMultiplier;
						}
					}
				}
				else if (name == "DISPLAY_TYPE")
				{
					return this.m_displayType;
				}
			}
			else if (name == "EXIT_SOUND")
			{
				return this.m_exitSound;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2706375718U)
			{
				if (num != 2588884954U)
				{
					if (num == 2706375718U)
					{
						if (name == "SHAKE_WEIGHT")
						{
							return this.m_shakeWeight;
						}
					}
				}
				else if (name == "NODE_INDEX")
				{
					return this.m_nodeIndex;
				}
			}
			else if (num != 2923745662U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return this.m_noteDesc;
					}
				}
			}
			else if (name == "DISPLAY_DATA")
			{
				return this.m_displayData;
			}
		}
		else if (num <= 3360314162U)
		{
			if (num != 3107145325U)
			{
				if (num == 3360314162U)
				{
					if (name == "ENTRY_ANIMATION")
					{
						return this.m_entryAnimation;
					}
				}
			}
			else if (name == "DISPLAY_TEXT_GLOW_SIZE")
			{
				return this.m_displayTextGlowSize;
			}
		}
		else if (num != 3837921320U)
		{
			if (num != 4068007548U)
			{
				if (num == 4202061068U)
				{
					if (name == "DISPLAY_PREFAB")
					{
						return this.m_displayPrefab;
					}
				}
			}
			else if (name == "EXIT_ANIMATION")
			{
				return this.m_exitAnimation;
			}
		}
		else if (name == "NODE_LAYOUT_ID")
		{
			return this.m_nodeLayoutId;
		}
		return null;
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x00097714 File Offset: 0x00095914
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2452196822U)
		{
			if (num <= 1180807003U)
			{
				if (num <= 568925309U)
				{
					if (num != 233364829U)
					{
						if (num != 568925309U)
						{
							return;
						}
						if (!(name == "DISPLAY_TEXT"))
						{
							return;
						}
						this.m_displayText = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "DISPLAY_COUNT"))
						{
							return;
						}
						this.m_displayCount = (int)val;
						return;
					}
				}
				else if (num != 1083713952U)
				{
					if (num != 1180807003U)
					{
						return;
					}
					if (!(name == "ENTRY_SOUND"))
					{
						return;
					}
					this.m_entrySound = (string)val;
					return;
				}
				else
				{
					if (!(name == "LANDING_SOUND"))
					{
						return;
					}
					this.m_landingSound = (string)val;
					return;
				}
			}
			else if (num <= 2054175731U)
			{
				if (num != 1458105184U)
				{
					if (num != 2054175731U)
					{
						return;
					}
					if (!(name == "ENTRY_DELAY"))
					{
						return;
					}
					this.m_entryDelay = (double)val;
					return;
				}
				else
				{
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
			}
			else if (num != 2277209517U)
			{
				if (num != 2396003294U)
				{
					if (num != 2452196822U)
					{
						return;
					}
					if (!(name == "ANIM_SPEED_MULTIPLIER"))
					{
						return;
					}
					this.m_animSpeedMultiplier = (double)val;
					return;
				}
				else
				{
					if (!(name == "DISPLAY_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_displayType = ModularBundleLayoutNode.DisplayType.INVALID;
						return;
					}
					if (val is ModularBundleLayoutNode.DisplayType || val is int)
					{
						this.m_displayType = (ModularBundleLayoutNode.DisplayType)val;
						return;
					}
					if (val is string)
					{
						this.m_displayType = ModularBundleLayoutNode.ParseDisplayTypeValue((string)val);
						return;
					}
				}
			}
			else
			{
				if (!(name == "EXIT_SOUND"))
				{
					return;
				}
				this.m_exitSound = (string)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2706375718U)
			{
				if (num != 2588884954U)
				{
					if (num != 2706375718U)
					{
						return;
					}
					if (!(name == "SHAKE_WEIGHT"))
					{
						return;
					}
					this.m_shakeWeight = (int)val;
				}
				else
				{
					if (!(name == "NODE_INDEX"))
					{
						return;
					}
					this.m_nodeIndex = (int)val;
					return;
				}
			}
			else if (num != 2923745662U)
			{
				if (num != 3022554311U)
				{
					return;
				}
				if (!(name == "NOTE_DESC"))
				{
					return;
				}
				this.m_noteDesc = (string)val;
				return;
			}
			else
			{
				if (!(name == "DISPLAY_DATA"))
				{
					return;
				}
				this.m_displayData = (int)val;
				return;
			}
		}
		else if (num <= 3360314162U)
		{
			if (num != 3107145325U)
			{
				if (num != 3360314162U)
				{
					return;
				}
				if (!(name == "ENTRY_ANIMATION"))
				{
					return;
				}
				this.m_entryAnimation = (string)val;
				return;
			}
			else
			{
				if (!(name == "DISPLAY_TEXT_GLOW_SIZE"))
				{
					return;
				}
				this.m_displayTextGlowSize = (string)val;
				return;
			}
		}
		else if (num != 3837921320U)
		{
			if (num != 4068007548U)
			{
				if (num != 4202061068U)
				{
					return;
				}
				if (!(name == "DISPLAY_PREFAB"))
				{
					return;
				}
				this.m_displayPrefab = (string)val;
				return;
			}
			else
			{
				if (!(name == "EXIT_ANIMATION"))
				{
					return;
				}
				this.m_exitAnimation = (string)val;
				return;
			}
		}
		else
		{
			if (!(name == "NODE_LAYOUT_ID"))
			{
				return;
			}
			this.m_nodeLayoutId = (int)val;
			return;
		}
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x00097A78 File Offset: 0x00095C78
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2452196822U)
		{
			if (num <= 1180807003U)
			{
				if (num <= 568925309U)
				{
					if (num != 233364829U)
					{
						if (num == 568925309U)
						{
							if (name == "DISPLAY_TEXT")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "DISPLAY_COUNT")
					{
						return typeof(int);
					}
				}
				else if (num != 1083713952U)
				{
					if (num == 1180807003U)
					{
						if (name == "ENTRY_SOUND")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "LANDING_SOUND")
				{
					return typeof(string);
				}
			}
			else if (num <= 2054175731U)
			{
				if (num != 1458105184U)
				{
					if (num == 2054175731U)
					{
						if (name == "ENTRY_DELAY")
						{
							return typeof(double);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (num != 2277209517U)
			{
				if (num != 2396003294U)
				{
					if (num == 2452196822U)
					{
						if (name == "ANIM_SPEED_MULTIPLIER")
						{
							return typeof(double);
						}
					}
				}
				else if (name == "DISPLAY_TYPE")
				{
					return typeof(ModularBundleLayoutNode.DisplayType);
				}
			}
			else if (name == "EXIT_SOUND")
			{
				return typeof(string);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2706375718U)
			{
				if (num != 2588884954U)
				{
					if (num == 2706375718U)
					{
						if (name == "SHAKE_WEIGHT")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "NODE_INDEX")
				{
					return typeof(int);
				}
			}
			else if (num != 2923745662U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "DISPLAY_DATA")
			{
				return typeof(int);
			}
		}
		else if (num <= 3360314162U)
		{
			if (num != 3107145325U)
			{
				if (num == 3360314162U)
				{
					if (name == "ENTRY_ANIMATION")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "DISPLAY_TEXT_GLOW_SIZE")
			{
				return typeof(string);
			}
		}
		else if (num != 3837921320U)
		{
			if (num != 4068007548U)
			{
				if (num == 4202061068U)
				{
					if (name == "DISPLAY_PREFAB")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "EXIT_ANIMATION")
			{
				return typeof(string);
			}
		}
		else if (name == "NODE_LAYOUT_ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001DA0 RID: 7584 RVA: 0x00097DED File Offset: 0x00095FED
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleLayoutNodeDbfRecords loadRecords = new LoadModularBundleLayoutNodeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x00097E04 File Offset: 0x00096004
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleLayoutNodeDbfAsset modularBundleLayoutNodeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleLayoutNodeDbfAsset)) as ModularBundleLayoutNodeDbfAsset;
		if (modularBundleLayoutNodeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ModularBundleLayoutNodeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < modularBundleLayoutNodeDbfAsset.Records.Count; i++)
		{
			modularBundleLayoutNodeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (modularBundleLayoutNodeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x00097E83 File Offset: 0x00096083
	public override void StripUnusedLocales()
	{
		this.m_displayText.StripUnusedLocales();
	}

	// Token: 0x0400114D RID: 4429
	[SerializeField]
	private int m_nodeLayoutId;

	// Token: 0x0400114E RID: 4430
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400114F RID: 4431
	[SerializeField]
	private ModularBundleLayoutNode.DisplayType m_displayType = ModularBundleLayoutNode.ParseDisplayTypeValue("invalid");

	// Token: 0x04001150 RID: 4432
	[SerializeField]
	private int m_displayData;

	// Token: 0x04001151 RID: 4433
	[SerializeField]
	private string m_displayPrefab;

	// Token: 0x04001152 RID: 4434
	[SerializeField]
	private DbfLocValue m_displayText;

	// Token: 0x04001153 RID: 4435
	[SerializeField]
	private string m_displayTextGlowSize;

	// Token: 0x04001154 RID: 4436
	[SerializeField]
	private int m_displayCount;

	// Token: 0x04001155 RID: 4437
	[SerializeField]
	private string m_entryAnimation;

	// Token: 0x04001156 RID: 4438
	[SerializeField]
	private string m_exitAnimation;

	// Token: 0x04001157 RID: 4439
	[SerializeField]
	private string m_entrySound;

	// Token: 0x04001158 RID: 4440
	[SerializeField]
	private string m_landingSound;

	// Token: 0x04001159 RID: 4441
	[SerializeField]
	private string m_exitSound;

	// Token: 0x0400115A RID: 4442
	[SerializeField]
	private int m_nodeIndex;

	// Token: 0x0400115B RID: 4443
	[SerializeField]
	private double m_entryDelay;

	// Token: 0x0400115C RID: 4444
	[SerializeField]
	private double m_animSpeedMultiplier = 1.0;

	// Token: 0x0400115D RID: 4445
	[SerializeField]
	private int m_shakeWeight;
}
