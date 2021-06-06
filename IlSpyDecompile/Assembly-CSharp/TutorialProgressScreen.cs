using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgressScreen : MonoBehaviour
{
	private class LessonAsset
	{
		public string m_asset;

		public string m_phoneAsset;
	}

	public HeroCoin m_coinPrefab;

	public UberText m_lessonTitle;

	public UberText m_missionProgressTitle;

	public GameObject m_currentLessonBone;

	public PegUIElement m_exitButton;

	public UberText m_exitButtonLabel;

	private static TutorialProgressScreen s_instance;

	private List<HeroCoin> m_heroCoins = new List<HeroCoin>();

	private HeroCoin.CoinPressCallback m_coinPressCallback;

	private bool m_showProgressSavedMessage;

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

	private readonly Map<ScenarioDbId, LessonAsset> m_missionIdToLessonAssetMap = new Map<ScenarioDbId, LessonAsset>
	{
		{
			ScenarioDbId.TUTORIAL_HOGGER,
			null
		},
		{
			ScenarioDbId.TUTORIAL_MILLHOUSE,
			new LessonAsset
			{
				m_asset = "Tutorial_Lesson1.prefab:51767358bb10afc4aac7ccb7a3b1e650"
			}
		},
		{
			ScenarioDbId.TUTORIAL_CHO,
			new LessonAsset
			{
				m_asset = "Tutorial_Lesson2.prefab:e97505bb5b8f67d409a10f827bd6043b",
				m_phoneAsset = "Tutorial_Lesson2_phone.prefab:be0cc750f6cbe4dc8b2606e5cb2249ed"
			}
		},
		{
			ScenarioDbId.TUTORIAL_MUKLA,
			new LessonAsset
			{
				m_asset = "Tutorial_Lesson3.prefab:cf99927ebaeabe14d862587afce9545a"
			}
		},
		{
			ScenarioDbId.TUTORIAL_NESINGWARY,
			new LessonAsset
			{
				m_asset = "Tutorial_Lesson4.prefab:caee3936e34d4e2469626c4e523f1b09"
			}
		},
		{
			ScenarioDbId.TUTORIAL_ILLIDAN,
			new LessonAsset
			{
				m_asset = "Tutorial_Lesson5.prefab:847a9f6b271b15e4ca5363dc29d2a590"
			}
		}
	};

	private List<ScenarioDbfRecord> m_sortedMissionRecords = new List<ScenarioDbfRecord>();

	private const float START_SCALE_VAL = 0.5f;

	private Vector3 START_SCALE = new Vector3(0.5f, 0.5f, 0.5f);

	private Vector3 FINAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(7f, 1f, 7f),
		Phone = new Vector3(6.1f, 1f, 6.1f)
	};

	private Vector3 FINAL_SCALE_OVER_BOX = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(92.5f, 14f, 92.5f),
		Phone = new Vector3(106f, 16f, 106f)
	};

	private PlatformDependentValue<Vector3> FINAL_POS = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-8f, 5f, -5f),
		Phone = new Vector3(-8f, 5f, -4.58f)
	};

	private PlatformDependentValue<Vector3> FINAL_POS_OVER_BOX = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 5f, -0.2f),
		Phone = new Vector3(0f, 5f, -2.06f)
	};

	private Vector3 HERO_COIN_START;

	private const float HERO_SPACING = -0.2f;

	private bool IS_TESTING;

	private void Awake()
	{
		s_instance = this;
		FullScreenFXMgr.Get().Vignette(1f, 0.5f, iTween.EaseType.easeInOutQuad);
		m_lessonTitle.Text = GameStrings.Get("TUTORIAL_PROGRESS_LESSON_TITLE");
		m_missionProgressTitle.Text = GameStrings.Get("TUTORIAL_PROGRESS_TITLE");
		m_exitButton.gameObject.SetActive(value: false);
		InitMissionRecords();
	}

	private void OnDestroy()
	{
		NetCache.Get()?.UnregisterNetCacheHandler(UpdateProgress);
		s_instance = null;
	}

	public static TutorialProgressScreen Get()
	{
		return s_instance;
	}

	public void StartTutorialProgress()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			if (GameState.Get().GetFriendlySidePlayer().GetTag<TAG_PLAYSTATE>(GAME_TAG.PLAYSTATE) == TAG_PLAYSTATE.WON)
			{
				GameState.Get().GetOpposingSidePlayer().GetHeroCard()
					.GetActorSpell(SpellType.ENDGAME_WIN)
					.ActivateState(SpellStateType.DEATH);
				m_showProgressSavedMessage = true;
			}
			Gameplay.Get().RemoveGamePlayNameBannerPhone();
		}
		LoadAllTutorialHeroEntities();
	}

	public void SetCoinPressCallback(HeroCoin.CoinPressCallback callback)
	{
		if (callback != null)
		{
			m_coinPressCallback = delegate
			{
				Hide();
				callback();
			};
		}
	}

	private void InitMissionRecords()
	{
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords())
		{
			if (record.AdventureId == 1)
			{
				int iD = record.ID;
				if (Enum.IsDefined(typeof(ScenarioDbId), iD))
				{
					m_sortedMissionRecords.Add(record);
				}
			}
		}
		m_sortedMissionRecords.Sort(GameUtils.MissionSortComparison);
	}

	private void LoadAllTutorialHeroEntities()
	{
		for (int i = 0; i < m_sortedMissionRecords.Count; i++)
		{
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(m_sortedMissionRecords[i].ID);
			if (DefLoader.Get().GetEntityDef(missionHeroCardId) == null)
			{
				Debug.LogError($"TutorialProgress.OnTutorialHeroEntityDefLoaded() - failed to load {missionHeroCardId}");
			}
		}
		SetupCoins();
		Show();
	}

	private void SetupCoins()
	{
		HERO_COIN_START = new Vector3(0.5f, 0.1f, 0.32f);
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < m_sortedMissionRecords.Count; i++)
		{
			int iD = m_sortedMissionRecords[i].ID;
			HeroCoin heroCoin = UnityEngine.Object.Instantiate(m_coinPrefab);
			heroCoin.transform.parent = base.transform;
			heroCoin.gameObject.SetActive(value: false);
			heroCoin.SetCoinPressCallback(m_coinPressCallback);
			Vector2 crackTexture = UnityEngine.Random.Range(0, 3) switch
			{
				1 => new Vector2(0.25f, -1f), 
				2 => new Vector2(0.5f, -1f), 
				_ => new Vector2(0f, -1f), 
			};
			if (i == 0)
			{
				heroCoin.transform.localPosition = HERO_COIN_START;
			}
			else
			{
				heroCoin.transform.localPosition = new Vector3(vector.x + -0.2f, vector.y, vector.z);
			}
			string text = null;
			m_missionIdToLessonAssetMap.TryGetValue((ScenarioDbId)iD, out var value);
			if (value != null)
			{
				text = ((!UniversalInputManager.UsePhoneUI || string.IsNullOrEmpty(value.m_phoneAsset)) ? value.m_asset : value.m_phoneAsset);
			}
			if (!string.IsNullOrEmpty(text))
			{
				heroCoin.SetLessonAsset(text);
			}
			m_heroCoins.Add(heroCoin);
			Vector2 goldTexture = Vector2.zero;
			Vector2 grayTexture = Vector2.zero;
			switch (iD)
			{
			case 201:
				goldTexture = new Vector2(0f, 0f);
				grayTexture = new Vector2(0.25f, 0f);
				break;
			case 4:
				goldTexture = new Vector2(0.5f, 0f);
				grayTexture = new Vector2(0.75f, 0f);
				break;
			case 3:
				goldTexture = new Vector2(0f, -0.25f);
				grayTexture = new Vector2(0.25f, -0.25f);
				break;
			case 181:
				goldTexture = new Vector2(0.5f, -0.25f);
				grayTexture = new Vector2(0.75f, -0.25f);
				break;
			case 248:
				goldTexture = new Vector2(0f, -0.5f);
				grayTexture = new Vector2(0.25f, -0.5f);
				break;
			case 249:
				goldTexture = new Vector2(0.5f, -0.5f);
				grayTexture = new Vector2(0.75f, -0.5f);
				break;
			}
			heroCoin.SetCoinInfo(goldTexture, grayTexture, crackTexture, iD);
			vector = heroCoin.transform.localPosition;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
	}

	private void Show()
	{
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		bool flag = SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY;
		base.transform.position = (flag ? FINAL_POS : FINAL_POS_OVER_BOX);
		base.transform.localScale = START_SCALE;
		Hashtable args = iTween.Hash("scale", flag ? FINAL_SCALE : FINAL_SCALE_OVER_BOX, "time", 0.5f, "oncomplete", "OnScaleAnimComplete", "oncompletetarget", base.gameObject);
		iTween.ScaleTo(base.gameObject, args);
	}

	private void Hide()
	{
		Hashtable args = iTween.Hash("scale", START_SCALE, "time", 0.5f, "oncomplete", "OnHideAnimComplete", "oncompletetarget", base.gameObject);
		iTween.ScaleTo(base.gameObject, args);
		args = iTween.Hash("alpha", 0f, "time", 0.25f, "delay", 0.25f);
		iTween.FadeTo(base.gameObject, args);
	}

	private void OnScaleAnimComplete()
	{
		if (IS_TESTING)
		{
			UpdateProgress();
		}
		else
		{
			NetCache.Get().RegisterTutorialEndGameScreen(UpdateProgress, NetCache.DefaultErrorHandler);
		}
		foreach (HeroCoin heroCoin in m_heroCoins)
		{
			heroCoin.FinishIntroScaling();
		}
	}

	private void OnHideAnimComplete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void UpdateProgress()
	{
		ScenarioDbId nextMissionId;
		if (IS_TESTING)
		{
			nextMissionId = m_progressToNextMissionIdMap[TutorialProgress.HOGGER_COMPLETE];
		}
		else
		{
			NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
			nextMissionId = m_progressToNextMissionIdMap[netObject.CampaignProgress];
		}
		int num = m_heroCoins.FindIndex((HeroCoin coin) => coin.GetMissionId() == (int)nextMissionId);
		for (int i = 0; i < m_heroCoins.Count; i++)
		{
			HeroCoin heroCoin = m_heroCoins[i];
			if (i == num - 1)
			{
				StartCoroutine(SetActiveToDefeated(heroCoin));
			}
			else if (i < num)
			{
				heroCoin.SetProgress(HeroCoin.CoinStatus.DEFEATED);
			}
			else if (i == num)
			{
				StartCoroutine(SetUnrevealedToActive(heroCoin));
				string lessonAsset = heroCoin.GetLessonAsset();
				if (!string.IsNullOrEmpty(lessonAsset))
				{
					AssetLoader.Get().InstantiatePrefab(lessonAsset, OnTutorialImageLoaded);
				}
			}
			else
			{
				heroCoin.SetProgress(HeroCoin.CoinStatus.UNREVEALED);
			}
		}
		if (m_showProgressSavedMessage)
		{
			UIStatus.Get().AddInfo(GameStrings.Get("TUTORIAL_PROGRESS_SAVED"));
			m_showProgressSavedMessage = false;
		}
	}

	private void OnTutorialImageLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		SetupTutorialImage(go);
	}

	private void SetupTutorialImage(GameObject go)
	{
		SceneUtils.SetLayer(go, GameLayer.IgnoreFullScreenEffects);
		go.transform.parent = m_currentLessonBone.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localEulerAngles = Vector3.zero;
		go.transform.localPosition = Vector3.zero;
	}

	private IEnumerator SetActiveToDefeated(HeroCoin coin)
	{
		coin.SetProgress(HeroCoin.CoinStatus.ACTIVE);
		coin.m_inputEnabled = false;
		yield return new WaitForSeconds(1f);
		coin.SetProgress(HeroCoin.CoinStatus.ACTIVE_TO_DEFEATED);
	}

	private IEnumerator SetUnrevealedToActive(HeroCoin coin)
	{
		coin.SetProgress(HeroCoin.CoinStatus.UNREVEALED);
		coin.m_inputEnabled = false;
		yield return new WaitForSeconds(2f);
		coin.SetProgress(HeroCoin.CoinStatus.UNREVEALED_TO_ACTIVE);
	}

	private void ExitButtonPress(UIEvent e)
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		FullScreenFXMgr.Get().Vignette(0f, 0.5f, iTween.EaseType.easeInOutQuad);
	}
}
