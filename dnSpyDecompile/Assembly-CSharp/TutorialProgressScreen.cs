using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000749 RID: 1865
public class TutorialProgressScreen : MonoBehaviour
{
	// Token: 0x0600696F RID: 26991 RVA: 0x00225BF0 File Offset: 0x00223DF0
	private void Awake()
	{
		TutorialProgressScreen.s_instance = this;
		FullScreenFXMgr.Get().Vignette(1f, 0.5f, iTween.EaseType.easeInOutQuad, null, null);
		this.m_lessonTitle.Text = GameStrings.Get("TUTORIAL_PROGRESS_LESSON_TITLE");
		this.m_missionProgressTitle.Text = GameStrings.Get("TUTORIAL_PROGRESS_TITLE");
		this.m_exitButton.gameObject.SetActive(false);
		this.InitMissionRecords();
	}

	// Token: 0x06006970 RID: 26992 RVA: 0x00225C5C File Offset: 0x00223E5C
	private void OnDestroy()
	{
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.UpdateProgress));
		}
		TutorialProgressScreen.s_instance = null;
	}

	// Token: 0x06006971 RID: 26993 RVA: 0x00225C8A File Offset: 0x00223E8A
	public static TutorialProgressScreen Get()
	{
		return TutorialProgressScreen.s_instance;
	}

	// Token: 0x06006972 RID: 26994 RVA: 0x00225C94 File Offset: 0x00223E94
	public void StartTutorialProgress()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			if (GameState.Get().GetFriendlySidePlayer().GetTag<TAG_PLAYSTATE>(GAME_TAG.PLAYSTATE) == TAG_PLAYSTATE.WON)
			{
				GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActorSpell(SpellType.ENDGAME_WIN, true).ActivateState(SpellStateType.DEATH);
				this.m_showProgressSavedMessage = true;
			}
			Gameplay.Get().RemoveGamePlayNameBannerPhone();
		}
		this.LoadAllTutorialHeroEntities();
	}

	// Token: 0x06006973 RID: 26995 RVA: 0x00225CF8 File Offset: 0x00223EF8
	public void SetCoinPressCallback(HeroCoin.CoinPressCallback callback)
	{
		if (callback == null)
		{
			return;
		}
		this.m_coinPressCallback = delegate()
		{
			this.Hide();
			callback();
		};
	}

	// Token: 0x06006974 RID: 26996 RVA: 0x00225D34 File Offset: 0x00223F34
	private void InitMissionRecords()
	{
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords())
		{
			if (scenarioDbfRecord.AdventureId == 1)
			{
				int id = scenarioDbfRecord.ID;
				if (Enum.IsDefined(typeof(ScenarioDbId), id))
				{
					this.m_sortedMissionRecords.Add(scenarioDbfRecord);
				}
			}
		}
		this.m_sortedMissionRecords.Sort(new Comparison<ScenarioDbfRecord>(GameUtils.MissionSortComparison));
	}

	// Token: 0x06006975 RID: 26997 RVA: 0x00225DD0 File Offset: 0x00223FD0
	private void LoadAllTutorialHeroEntities()
	{
		for (int i = 0; i < this.m_sortedMissionRecords.Count; i++)
		{
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(this.m_sortedMissionRecords[i].ID);
			if (DefLoader.Get().GetEntityDef(missionHeroCardId) == null)
			{
				Debug.LogError(string.Format("TutorialProgress.OnTutorialHeroEntityDefLoaded() - failed to load {0}", missionHeroCardId));
			}
		}
		this.SetupCoins();
		this.Show();
	}

	// Token: 0x06006976 RID: 26998 RVA: 0x00225E34 File Offset: 0x00224034
	private void SetupCoins()
	{
		this.HERO_COIN_START = new Vector3(0.5f, 0.1f, 0.32f);
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < this.m_sortedMissionRecords.Count; i++)
		{
			int id = this.m_sortedMissionRecords[i].ID;
			HeroCoin heroCoin = UnityEngine.Object.Instantiate<HeroCoin>(this.m_coinPrefab);
			heroCoin.transform.parent = base.transform;
			heroCoin.gameObject.SetActive(false);
			heroCoin.SetCoinPressCallback(this.m_coinPressCallback);
			int num = UnityEngine.Random.Range(0, 3);
			Vector2 crackTexture;
			if (num == 1)
			{
				crackTexture = new Vector2(0.25f, -1f);
			}
			else if (num == 2)
			{
				crackTexture = new Vector2(0.5f, -1f);
			}
			else
			{
				crackTexture = new Vector2(0f, -1f);
			}
			if (i == 0)
			{
				heroCoin.transform.localPosition = this.HERO_COIN_START;
			}
			else
			{
				heroCoin.transform.localPosition = new Vector3(vector.x + -0.2f, vector.y, vector.z);
			}
			string text = null;
			TutorialProgressScreen.LessonAsset lessonAsset;
			this.m_missionIdToLessonAssetMap.TryGetValue((ScenarioDbId)id, out lessonAsset);
			if (lessonAsset != null)
			{
				if (UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(lessonAsset.m_phoneAsset))
				{
					text = lessonAsset.m_phoneAsset;
				}
				else
				{
					text = lessonAsset.m_asset;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				heroCoin.SetLessonAsset(text);
			}
			this.m_heroCoins.Add(heroCoin);
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			if (id <= 181)
			{
				if (id != 3)
				{
					if (id != 4)
					{
						if (id == 181)
						{
							zero = new Vector2(0.5f, -0.25f);
							zero2 = new Vector2(0.75f, -0.25f);
						}
					}
					else
					{
						zero = new Vector2(0.5f, 0f);
						zero2 = new Vector2(0.75f, 0f);
					}
				}
				else
				{
					zero = new Vector2(0f, -0.25f);
					zero2 = new Vector2(0.25f, -0.25f);
				}
			}
			else if (id != 201)
			{
				if (id != 248)
				{
					if (id == 249)
					{
						zero = new Vector2(0.5f, -0.5f);
						zero2 = new Vector2(0.75f, -0.5f);
					}
				}
				else
				{
					zero = new Vector2(0f, -0.5f);
					zero2 = new Vector2(0.25f, -0.5f);
				}
			}
			else
			{
				zero = new Vector2(0f, 0f);
				zero2 = new Vector2(0.25f, 0f);
			}
			heroCoin.SetCoinInfo(zero, zero2, crackTexture, id);
			vector = heroCoin.transform.localPosition;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
	}

	// Token: 0x06006977 RID: 26999 RVA: 0x00226104 File Offset: 0x00224304
	private void Show()
	{
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		bool flag = SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY;
		base.transform.position = (flag ? this.FINAL_POS : this.FINAL_POS_OVER_BOX);
		base.transform.localScale = this.START_SCALE;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			flag ? this.FINAL_SCALE : this.FINAL_SCALE_OVER_BOX,
			"time",
			0.5f,
			"oncomplete",
			"OnScaleAnimComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06006978 RID: 27000 RVA: 0x002261D8 File Offset: 0x002243D8
	private void Hide()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			this.START_SCALE,
			"time",
			0.5f,
			"oncomplete",
			"OnHideAnimComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(base.gameObject, args);
		args = iTween.Hash(new object[]
		{
			"alpha",
			0f,
			"time",
			0.25f,
			"delay",
			0.25f
		});
		iTween.FadeTo(base.gameObject, args);
	}

	// Token: 0x06006979 RID: 27001 RVA: 0x002262A0 File Offset: 0x002244A0
	private void OnScaleAnimComplete()
	{
		if (this.IS_TESTING)
		{
			this.UpdateProgress();
		}
		else
		{
			NetCache.Get().RegisterTutorialEndGameScreen(new NetCache.NetCacheCallback(this.UpdateProgress), new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
		}
		foreach (HeroCoin heroCoin in this.m_heroCoins)
		{
			heroCoin.FinishIntroScaling();
		}
	}

	// Token: 0x0600697A RID: 27002 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnHideAnimComplete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600697B RID: 27003 RVA: 0x00226324 File Offset: 0x00224524
	private void UpdateProgress()
	{
		ScenarioDbId nextMissionId;
		if (this.IS_TESTING)
		{
			nextMissionId = this.m_progressToNextMissionIdMap[TutorialProgress.HOGGER_COMPLETE];
		}
		else
		{
			NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
			nextMissionId = this.m_progressToNextMissionIdMap[netObject.CampaignProgress];
		}
		int num = this.m_heroCoins.FindIndex((HeroCoin coin) => coin.GetMissionId() == (int)nextMissionId);
		for (int i = 0; i < this.m_heroCoins.Count; i++)
		{
			HeroCoin heroCoin = this.m_heroCoins[i];
			if (i == num - 1)
			{
				base.StartCoroutine(this.SetActiveToDefeated(heroCoin));
			}
			else if (i < num)
			{
				heroCoin.SetProgress(HeroCoin.CoinStatus.DEFEATED);
			}
			else if (i == num)
			{
				base.StartCoroutine(this.SetUnrevealedToActive(heroCoin));
				string lessonAsset = heroCoin.GetLessonAsset();
				if (!string.IsNullOrEmpty(lessonAsset))
				{
					AssetLoader.Get().InstantiatePrefab(lessonAsset, new PrefabCallback<GameObject>(this.OnTutorialImageLoaded), null, AssetLoadingOptions.None);
				}
			}
			else
			{
				heroCoin.SetProgress(HeroCoin.CoinStatus.UNREVEALED);
			}
		}
		if (this.m_showProgressSavedMessage)
		{
			UIStatus.Get().AddInfo(GameStrings.Get("TUTORIAL_PROGRESS_SAVED"));
			this.m_showProgressSavedMessage = false;
		}
	}

	// Token: 0x0600697C RID: 27004 RVA: 0x0022644E File Offset: 0x0022464E
	private void OnTutorialImageLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.SetupTutorialImage(go);
	}

	// Token: 0x0600697D RID: 27005 RVA: 0x00226458 File Offset: 0x00224658
	private void SetupTutorialImage(GameObject go)
	{
		SceneUtils.SetLayer(go, GameLayer.IgnoreFullScreenEffects);
		go.transform.parent = this.m_currentLessonBone.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localEulerAngles = Vector3.zero;
		go.transform.localPosition = Vector3.zero;
	}

	// Token: 0x0600697E RID: 27006 RVA: 0x002264B3 File Offset: 0x002246B3
	private IEnumerator SetActiveToDefeated(HeroCoin coin)
	{
		coin.SetProgress(HeroCoin.CoinStatus.ACTIVE);
		coin.m_inputEnabled = false;
		yield return new WaitForSeconds(1f);
		coin.SetProgress(HeroCoin.CoinStatus.ACTIVE_TO_DEFEATED);
		yield break;
	}

	// Token: 0x0600697F RID: 27007 RVA: 0x002264C2 File Offset: 0x002246C2
	private IEnumerator SetUnrevealedToActive(HeroCoin coin)
	{
		coin.SetProgress(HeroCoin.CoinStatus.UNREVEALED);
		coin.m_inputEnabled = false;
		yield return new WaitForSeconds(2f);
		coin.SetProgress(HeroCoin.CoinStatus.UNREVEALED_TO_ACTIVE);
		yield break;
	}

	// Token: 0x06006980 RID: 27008 RVA: 0x002264D1 File Offset: 0x002246D1
	private void ExitButtonPress(UIEvent e)
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		FullScreenFXMgr.Get().Vignette(0f, 0.5f, iTween.EaseType.easeInOutQuad, null, null);
	}

	// Token: 0x04005649 RID: 22089
	public HeroCoin m_coinPrefab;

	// Token: 0x0400564A RID: 22090
	public UberText m_lessonTitle;

	// Token: 0x0400564B RID: 22091
	public UberText m_missionProgressTitle;

	// Token: 0x0400564C RID: 22092
	public GameObject m_currentLessonBone;

	// Token: 0x0400564D RID: 22093
	public PegUIElement m_exitButton;

	// Token: 0x0400564E RID: 22094
	public UberText m_exitButtonLabel;

	// Token: 0x0400564F RID: 22095
	private static TutorialProgressScreen s_instance;

	// Token: 0x04005650 RID: 22096
	private List<HeroCoin> m_heroCoins = new List<HeroCoin>();

	// Token: 0x04005651 RID: 22097
	private HeroCoin.CoinPressCallback m_coinPressCallback;

	// Token: 0x04005652 RID: 22098
	private bool m_showProgressSavedMessage;

	// Token: 0x04005653 RID: 22099
	private readonly Map<TutorialProgress, ScenarioDbId> m_progressToNextMissionIdMap = new Map<TutorialProgress, ScenarioDbId>
	{
		{
			TutorialProgress.NOTHING_COMPLETE,
			ScenarioDbId.TUTORIAL_HOGGER
		},
		{
			TutorialProgress.HOGGER_COMPLETE,
			ScenarioDbId.TUTORIAL_MILLHOUSE
		},
		{
			TutorialProgress.MILLHOUSE_COMPLETE,
			ScenarioDbId.TUTORIAL_CHO
		},
		{
			TutorialProgress.CHO_COMPLETE,
			ScenarioDbId.TUTORIAL_MUKLA
		},
		{
			TutorialProgress.MUKLA_COMPLETE,
			ScenarioDbId.TUTORIAL_NESINGWARY
		},
		{
			TutorialProgress.NESINGWARY_COMPLETE,
			ScenarioDbId.TUTORIAL_ILLIDAN
		}
	};

	// Token: 0x04005654 RID: 22100
	private readonly Map<ScenarioDbId, TutorialProgressScreen.LessonAsset> m_missionIdToLessonAssetMap = new Map<ScenarioDbId, TutorialProgressScreen.LessonAsset>
	{
		{
			ScenarioDbId.TUTORIAL_HOGGER,
			null
		},
		{
			ScenarioDbId.TUTORIAL_MILLHOUSE,
			new TutorialProgressScreen.LessonAsset
			{
				m_asset = "Tutorial_Lesson1.prefab:51767358bb10afc4aac7ccb7a3b1e650"
			}
		},
		{
			ScenarioDbId.TUTORIAL_CHO,
			new TutorialProgressScreen.LessonAsset
			{
				m_asset = "Tutorial_Lesson2.prefab:e97505bb5b8f67d409a10f827bd6043b",
				m_phoneAsset = "Tutorial_Lesson2_phone.prefab:be0cc750f6cbe4dc8b2606e5cb2249ed"
			}
		},
		{
			ScenarioDbId.TUTORIAL_MUKLA,
			new TutorialProgressScreen.LessonAsset
			{
				m_asset = "Tutorial_Lesson3.prefab:cf99927ebaeabe14d862587afce9545a"
			}
		},
		{
			ScenarioDbId.TUTORIAL_NESINGWARY,
			new TutorialProgressScreen.LessonAsset
			{
				m_asset = "Tutorial_Lesson4.prefab:caee3936e34d4e2469626c4e523f1b09"
			}
		},
		{
			ScenarioDbId.TUTORIAL_ILLIDAN,
			new TutorialProgressScreen.LessonAsset
			{
				m_asset = "Tutorial_Lesson5.prefab:847a9f6b271b15e4ca5363dc29d2a590"
			}
		}
	};

	// Token: 0x04005655 RID: 22101
	private List<ScenarioDbfRecord> m_sortedMissionRecords = new List<ScenarioDbfRecord>();

	// Token: 0x04005656 RID: 22102
	private const float START_SCALE_VAL = 0.5f;

	// Token: 0x04005657 RID: 22103
	private Vector3 START_SCALE = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x04005658 RID: 22104
	private Vector3 FINAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(7f, 1f, 7f),
		Phone = new Vector3(6.1f, 1f, 6.1f)
	};

	// Token: 0x04005659 RID: 22105
	private Vector3 FINAL_SCALE_OVER_BOX = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(92.5f, 14f, 92.5f),
		Phone = new Vector3(106f, 16f, 106f)
	};

	// Token: 0x0400565A RID: 22106
	private PlatformDependentValue<Vector3> FINAL_POS = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-8f, 5f, -5f),
		Phone = new Vector3(-8f, 5f, -4.58f)
	};

	// Token: 0x0400565B RID: 22107
	private PlatformDependentValue<Vector3> FINAL_POS_OVER_BOX = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 5f, -0.2f),
		Phone = new Vector3(0f, 5f, -2.06f)
	};

	// Token: 0x0400565C RID: 22108
	private Vector3 HERO_COIN_START;

	// Token: 0x0400565D RID: 22109
	private const float HERO_SPACING = -0.2f;

	// Token: 0x0400565E RID: 22110
	private bool IS_TESTING;

	// Token: 0x02002320 RID: 8992
	private class LessonAsset
	{
		// Token: 0x0400E5D7 RID: 58839
		public string m_asset;

		// Token: 0x0400E5D8 RID: 58840
		public string m_phoneAsset;
	}
}
