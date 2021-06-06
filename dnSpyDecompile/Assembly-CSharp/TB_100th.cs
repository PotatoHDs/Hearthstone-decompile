using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class TB_100th : MissionEntity
{
	// Token: 0x06004FB4 RID: 20404 RVA: 0x001A26DC File Offset: 0x001A08DC
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004FB5 RID: 20405 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06004FB6 RID: 20406 RVA: 0x001A26F0 File Offset: 0x001A08F0
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x06004FB7 RID: 20407 RVA: 0x001A2768 File Offset: 0x001A0968
	private Actor GetMedivaActor()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity == null)
		{
			return null;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			return null;
		}
		return card.GetActor();
	}

	// Token: 0x06004FB8 RID: 20408 RVA: 0x001A27B0 File Offset: 0x001A09B0
	public override void PreloadAssets()
	{
		this.last_time_emoted = new bool[100];
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranHeroPower_04.prefab:9bca13673c70fbe4e856fa6cadb44d32");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranSpell_06.prefab:7ee6ed9064bae8b4cae9b616322befa2");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_AranSpell_01.prefab:391ba56a5ee5e0e4b9098147ac14d7bc");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_ChessEmoteOops_01.prefab:4133c951e3b700242b5747520ab4cf2c");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_CuratorBeasts_01.prefab:bd55c69fcafba594b984a9d7792e43e9");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_IllhoofWounded_01.prefab:6eac5d210aeee3048957194c8f610ceb");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_IllhoofWounded_04.prefab:e774579b014394548914674ee10897e6");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_JulianneDeadlyPoison_02.prefab:3facdbce34af76a438defff4a56c6ce2");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_PrologueTurn5_03.prefab:ee16361b04c4daa419e0006a5b5949e8");
		base.PreloadSound("VO_Mediva_03_Male_Night Elf_PrologueTurn5_02.prefab:71fd5f307f54d174e939f2aada7af9bf");
	}

	// Token: 0x06004FB9 RID: 20409 RVA: 0x001A2838 File Offset: 0x001A0A38
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		this.SetPopupPosition();
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			yield return this.ShowPopup(this.friendlySidePlayer.IsCurrentPlayer() ? this.m_popUpInfo[missionEvent][0] : this.m_popUpInfo[missionEvent][1]);
		}
		else if (this.m_medivaInfo.ContainsKey(missionEvent))
		{
			yield return this.ShowMediva(missionEvent, this.m_medivaInfo[missionEvent][0], this.m_medivaInfo[missionEvent][1]);
		}
		yield break;
	}

	// Token: 0x06004FBA RID: 20410 RVA: 0x001A284E File Offset: 0x001A0A4E
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004FBB RID: 20411 RVA: 0x001A2864 File Offset: 0x001A0A64
	private IEnumerator ShowMediva(int missionEvent, string soundPath, string stringName)
	{
		Actor medivaActor = this.GetMedivaActor();
		if (medivaActor == null)
		{
			Debug.LogWarningFormat("No Mediva actor found for mission: {0} in TB_100th.", new object[]
			{
				missionEvent
			});
			yield break;
		}
		if (!this.last_time_emoted[missionEvent])
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString(soundPath, stringName, Notification.SpeechBubbleDirection.TopRight, medivaActor, 1f, true, false));
			GameState.Get().SetBusy(false);
			this.last_time_emoted[missionEvent] = true;
		}
		yield break;
	}

	// Token: 0x040045CA RID: 17866
	private Notification StartPopup;

	// Token: 0x040045CB RID: 17867
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_100TH_RAVENIDOL_YOU",
				"TB_100TH_RAVENIDOL_OPP"
			}
		},
		{
			11,
			new string[]
			{
				"TB_100TH_RANDOMONIUM_YOU",
				"TB_100TH_RANDOMONIUM_OPP"
			}
		},
		{
			12,
			new string[]
			{
				"TB_100TH_BANANA_YOU",
				"TB_100TH_BANANA_OPP"
			}
		},
		{
			13,
			new string[]
			{
				"TB_100TH_YOGG_YOU",
				"TB_100TH_YOGG_OPP"
			}
		},
		{
			14,
			new string[]
			{
				"TB_100TH_PARTYPORTAL_YOU",
				"TB_100TH_PARTYPORTAL_OPP"
			}
		},
		{
			15,
			new string[]
			{
				"TB_100TH_CLONEBALL_YOU",
				"TB_100TH_CLONEBALL_OPP"
			}
		},
		{
			16,
			new string[]
			{
				"TB_100TH_SPELLBAG_YOU",
				"TB_100TH_SPELLBAG_OPP"
			}
		},
		{
			17,
			new string[]
			{
				"TB_100TH_PORTALS_YOU",
				"TB_100TH_PORTALS_OPP"
			}
		},
		{
			18,
			new string[]
			{
				"TB_100TH_SHIFTER_YOU",
				"TB_100TH_SHIFTER_OPP"
			}
		},
		{
			20,
			new string[]
			{
				"TB_100TH_RAVENIDOL_INFO_YOU",
				"TB_100TH_RAVENIDOL_INFO_OPP"
			}
		},
		{
			21,
			new string[]
			{
				"TB_100TH_RANDEMONIUM_INFO_YOU",
				"TB_100TH_RANDEMONIUM_INFO_OPP"
			}
		},
		{
			22,
			new string[]
			{
				"TB_100TH_BANANA_INFO_YOU",
				"TB_100TH_BANANA_INFO_OPP"
			}
		},
		{
			23,
			new string[]
			{
				"TB_100TH_YOGG_INFO_YOU",
				"TB_100TH_YOGG_INFO_OPP"
			}
		},
		{
			24,
			new string[]
			{
				"TB_100TH_PARTYPORTAL_INFO_YOU",
				"TB_100TH_PARTYPORTAL_INFO_OPP"
			}
		},
		{
			25,
			new string[]
			{
				"TB_100TH_CLONEBALL_INFO_YOU",
				"TB_100TH_CLONEBALL_INFO_OPP"
			}
		},
		{
			26,
			new string[]
			{
				"TB_100TH_SPELLBAG_INFO_YOU",
				"TB_100TH_SPELLBAG_INFO_OPP"
			}
		},
		{
			27,
			new string[]
			{
				"TB_100TH_PORTALS_INFO_YOU",
				"TB_100TH_PORTALS_INFO_OPP"
			}
		},
		{
			28,
			new string[]
			{
				"TB_100TH_SHIFTER_INFO_YOU",
				"TB_100TH_SHIFTER_INFO_OPP"
			}
		}
	};

	// Token: 0x040045CC RID: 17868
	private Dictionary<int, string[]> m_medivaInfo = new Dictionary<int, string[]>
	{
		{
			41,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_AranHeroPower_04.prefab:9bca13673c70fbe4e856fa6cadb44d32",
				"VO_TB_MEDIVA3_001"
			}
		},
		{
			42,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_AranSpell_06.prefab:7ee6ed9064bae8b4cae9b616322befa2",
				"VO_TB_MEDIVA3_002"
			}
		},
		{
			43,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_AranSpell_01.prefab:391ba56a5ee5e0e4b9098147ac14d7bc",
				"VO_TB_MEDIVA3_003"
			}
		},
		{
			44,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_ChessEmoteOops_01.prefab:4133c951e3b700242b5747520ab4cf2c",
				"VO_TB_MEDIVA3_004"
			}
		},
		{
			45,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_CuratorBeasts_01.prefab:bd55c69fcafba594b984a9d7792e43e9",
				"VO_TB_MEDIVA3_005"
			}
		},
		{
			46,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_IllhoofWounded_01.prefab:6eac5d210aeee3048957194c8f610ceb",
				"VO_TB_MEDIVA3_006"
			}
		},
		{
			47,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_IllhoofWounded_04.prefab:e774579b014394548914674ee10897e6",
				"VO_TB_MEDIVA3_007"
			}
		},
		{
			48,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_JulianneDeadlyPoison_02.prefab:3facdbce34af76a438defff4a56c6ce2",
				"VO_TB_MEDIVA3_008"
			}
		},
		{
			49,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_PrologueTurn5_03.prefab:ee16361b04c4daa419e0006a5b5949e8",
				"VO_TB_MEDIVA3_009"
			}
		},
		{
			50,
			new string[]
			{
				"VO_Mediva_03_Male_Night Elf_PrologueTurn5_02.prefab:71fd5f307f54d174e939f2aada7af9bf",
				"VO_TB_MEDIVA3_010"
			}
		}
	};

	// Token: 0x040045CD RID: 17869
	private int times_emoted;

	// Token: 0x040045CE RID: 17870
	private bool[] last_time_emoted;

	// Token: 0x040045CF RID: 17871
	private Vector3 popUpPos;

	// Token: 0x040045D0 RID: 17872
	private Player friendlySidePlayer;
}
