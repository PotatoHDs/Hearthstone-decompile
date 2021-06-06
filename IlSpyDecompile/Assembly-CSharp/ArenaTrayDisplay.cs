using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class ArenaTrayDisplay : MonoBehaviour
{
	[Serializable]
	public class ArenaKeyVisualData
	{
		public Mesh m_Mesh;

		public Material m_Material;

		public Mesh m_KeyHoleGlowMesh;

		public Texture m_EffectGlowTexture;

		public Texture m_SelectionGlowTexture;

		public GameObject m_ParticlePrefab;

		public string m_IdleEffectsPrefabPath;
	}

	public int m_Rank;

	public PlayMakerFSM m_RewardPlaymaker;

	[CustomEditField(Sections = "Keys")]
	public GameObject m_TheKeyMesh;

	public GameObject m_TheKeyGlowPlane;

	public GameObject m_TheKeyGlowHoleMesh;

	public GameObject m_TheKeySelectionGlow;

	public GameObject m_TheKeyOldSelectionGlow;

	public float m_TheKeyTransitionDelay = 0.5f;

	public float m_TheKeyTransitionFadeInTime = 1.5f;

	public float m_TheKeyTransitionFadeOutTime = 2f;

	public ParticleSystem m_TheKeyTransitionParticles;

	public string m_TheKeyTransitionSound = "arena_key_transition.prefab:7b4c3a5222405834abd921cbf53bf689";

	[CustomEditField(Sections = "Reward Panel")]
	public UberText m_WinCountUberText;

	public GameObject m_RewardDoorPlates;

	public GameObject m_BehindTheDoors;

	public GameObject m_RewardPaperBone;

	public GameObject m_PaperMain;

	public GameObject m_RewardBoxesBone;

	public GameObject m_InstructionText;

	public GameObject m_InstructionDetailText;

	public List<ArenaKeyVisualData> m_ArenaKeyVisualData;

	private RewardBoxesDisplay m_RewardBoxes;

	private GameObject m_TheKeyIdleEffects;

	private bool m_isTheKeyIdleEffectsLoading;

	private ArenaRewardPaper m_RewardPaper;

	private GameObject m_Paper;

	private AssetHandle<Texture> m_paperTexture;

	private bool m_isReady;

	private static ArenaTrayDisplay s_Instance;

	private void Awake()
	{
		s_Instance = this;
		AssetReference draftPaperTexture = DraftManager.Get().GetDraftPaperTexture();
		AssetLoader.Get().LoadAsset(draftPaperTexture, delegate(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
		{
			if (loadedTexture == null)
			{
				Debug.LogWarningFormat("ArenaTrayDisplay: Failed to load {0}.", assetRef.ToString());
			}
			else
			{
				AssetHandle.Take(ref m_paperTexture, loadedTexture);
				m_PaperMain.GetComponent<Renderer>().GetMaterial().mainTexture = m_paperTexture;
			}
		});
		AssetReference rewardPaperPrefab = DraftManager.Get().GetRewardPaperPrefab();
		AssetLoader.Get().InstantiatePrefab(rewardPaperPrefab, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (go == null)
			{
				Debug.LogWarning($"ArenaTrayDisplay: Failed to load {assetRef.ToString()}.");
			}
			else
			{
				s_Instance.m_RewardPaper = go.GetComponent<ArenaRewardPaper>();
				go.transform.parent = base.gameObject.transform;
				if (s_Instance.m_RewardPaperBone != null)
				{
					go.transform.position = s_Instance.m_RewardPaperBone.transform.position;
					go.transform.localScale = s_Instance.m_RewardPaperBone.transform.localScale;
				}
				else
				{
					Debug.LogWarning("ArenaTrayDisplay: m_RewardPaperBone is not set, so ArenaRewardPaper may look wrong.");
				}
				s_Instance.m_Paper = go;
				ShowPlainPaperBackground();
				if (s_Instance.m_RewardPaper == null)
				{
					Debug.LogWarning($"ArenaTrayDisplay: m_RewardPaper is null! Check the prefab you're loading, {assetRef.ToString()}.");
					s_Instance.m_isReady = true;
				}
				else if (s_Instance.m_RewardPaper.m_WinsUberText == null || s_Instance.m_RewardPaper.m_LossesUberText == null)
				{
					Debug.LogWarning($"ArenaTrayDisplay: m_WinsUberText or m_LossesUberText is null! Check the prefab you're loading, {assetRef.ToString()}");
					s_Instance.m_isReady = true;
				}
				else
				{
					s_Instance.m_RewardPaper.m_WinsUberText.Text = GameStrings.Get("GLUE_DRAFT_WINS_LABEL");
					s_Instance.m_RewardPaper.m_LossesUberText.Text = GameStrings.Get("GLUE_DRAFT_LOSSES_LABEL");
					if (s_Instance.m_BehindTheDoors == null)
					{
						Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
						s_Instance.m_isReady = true;
					}
					else
					{
						s_Instance.m_BehindTheDoors.SetActive(value: false);
						if (s_Instance.m_RewardDoorPlates == null)
						{
							Debug.LogWarning("ArenaTrayDisplay: m_RewardDoorPlates is null!");
							s_Instance.m_isReady = true;
						}
						else
						{
							s_Instance.m_RewardDoorPlates.SetActive(value: false);
							SceneUtils.EnableColliders(s_Instance.m_TheKeyMesh, enable: false);
							s_Instance.m_isReady = true;
						}
					}
				}
			}
		});
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_paperTexture);
	}

	public static ArenaTrayDisplay Get()
	{
		return s_Instance;
	}

	public bool IsReady()
	{
		return m_isReady;
	}

	public void UpdateTray()
	{
		UpdateTray(showNewKey: true);
	}

	public void UpdateTray(bool showNewKey)
	{
		ShowPlainPaper();
		if (m_InstructionText != null)
		{
			m_InstructionText.SetActive(value: false);
		}
		if (m_RewardDoorPlates != null && !m_RewardDoorPlates.activeSelf)
		{
			m_RewardDoorPlates.SetActive(value: true);
		}
		bool flag = false;
		DraftManager draftManager = DraftManager.Get();
		if (draftManager == null)
		{
			Debug.LogError("ArenaTrayDisplay: DraftManager.Get() == null!");
			return;
		}
		int wins = draftManager.GetWins();
		int losses = draftManager.GetLosses();
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && GameMgr.Get().WasArena() && draftManager.GetIsNewKey())
		{
			flag = true;
		}
		m_WinCountUberText.Text = wins.ToString();
		m_RewardPaper.m_Xmark1.GetComponent<Renderer>().enabled = losses > 0;
		m_RewardPaper.m_Xmark2.GetComponent<Renderer>().enabled = losses > 1;
		m_RewardPaper.m_Xmark3.GetComponent<Renderer>().enabled = losses > 2;
		UpdateXBoxes();
		if (flag && wins > 0 && showNewKey)
		{
			UpdateKeyArt(wins - 1);
			StartCoroutine(AnimateKeyTransition(wins));
		}
		else
		{
			UpdateKeyArt(wins);
		}
	}

	public void ShowPlainPaperBackground()
	{
		ShowPlainPaper();
		if (m_InstructionText != null)
		{
			m_InstructionText.SetActive(value: true);
		}
		if (m_RewardDoorPlates != null && m_RewardDoorPlates.activeSelf)
		{
			m_RewardDoorPlates.SetActive(value: false);
		}
	}

	public void ActivateKey()
	{
		SceneUtils.EnableColliders(m_TheKeyMesh, enable: true);
		Renderer component = m_TheKeySelectionGlow.GetComponent<Renderer>();
		component.enabled = true;
		Material sharedMaterial = component.GetSharedMaterial();
		Color color = sharedMaterial.color;
		color.a = 0f;
		sharedMaterial.color = color;
		sharedMaterial.SetFloat("_FxIntensity", 1f);
		iTween.FadeTo(m_TheKeySelectionGlow, iTween.Hash("alpha", 0.8f, "time", 2f, "easetype", iTween.EaseType.easeInOutBack));
		Material KeyGlowMat = component.GetMaterial();
		KeyGlowMat.SetFloat("_FxIntensity", 0f);
		Action<object> action = delegate(object amount)
		{
			KeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args = iTween.Hash("time", 2f, "from", 0f, "to", 1f, "easetype", iTween.EaseType.easeInOutBack, "onupdate", action, "onupdatetarget", m_TheKeySelectionGlow);
		iTween.ValueTo(m_TheKeySelectionGlow, args);
		PegUIElement component2 = m_TheKeyMesh.GetComponent<PegUIElement>();
		if (component2 == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: PegUIElement missing on the Key!");
			return;
		}
		component2.AddEventListener(UIEventType.PRESS, OpenRewardBox);
		Navigation.PushBlockBackingOut();
	}

	public void ShowRewardsOpenAtStart()
	{
		if (m_RewardPlaymaker == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: Missing Playmaker FSM!");
			return;
		}
		HidePaper();
		if (m_InstructionText != null)
		{
			m_InstructionText.SetActive(value: false);
		}
		if (m_InstructionDetailText != null)
		{
			m_InstructionDetailText.SetActive(value: false);
		}
		if (m_WinCountUberText != null)
		{
			m_WinCountUberText.gameObject.SetActive(value: false);
		}
		if (m_RewardPaper.m_WinsUberText != null)
		{
			m_RewardPaper.m_WinsUberText.gameObject.SetActive(value: false);
		}
		if (m_RewardPaper.m_LossesUberText != null)
		{
			m_RewardPaper.m_LossesUberText.gameObject.SetActive(value: false);
		}
		if (m_RewardPaper.m_XmarksRoot != null)
		{
			m_RewardPaper.m_XmarksRoot.SetActive(value: false);
		}
		if (m_TheKeySelectionGlow != null)
		{
			m_TheKeySelectionGlow.SetActive(value: false);
		}
		m_RewardPaper.m_WinsUberText.gameObject.SetActive(value: false);
		m_RewardPaper.m_LossesUberText.gameObject.SetActive(value: false);
		m_TheKeyMesh.gameObject.SetActive(value: false);
		if (m_BehindTheDoors == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
			return;
		}
		m_BehindTheDoors.SetActive(value: true);
		if (DraftManager.Get() == null)
		{
			Debug.LogError("ArenaTrayDisplay: DraftManager.Get() == null!");
			return;
		}
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			m_RewardBoxes = go.GetComponent<RewardBoxesDisplay>();
			m_RewardBoxes.SetRewards(DraftManager.Get().GetRewards());
			m_RewardBoxes.RegisterDoneCallback(OnRewardBoxesDone);
			TransformUtil.AttachAndPreserveLocalTransform(m_RewardBoxes.transform, m_RewardBoxesBone.transform);
			m_RewardBoxes.DebugLogRewards();
			m_RewardBoxes.ShowAlreadyOpenedRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback);
		m_RewardPlaymaker.gameObject.SetActive(value: true);
		m_RewardPlaymaker.SendEvent("Death");
		if (m_TheKeyMesh.GetComponent<PegUIElement>() == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: PegUIElement missing on the Key!");
		}
	}

	public void ShowOpenedRewards()
	{
	}

	public void AnimateRewards()
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			m_RewardBoxes = go.GetComponent<RewardBoxesDisplay>();
			m_RewardBoxes.SetRewards(DraftManager.Get().GetRewards());
			m_RewardBoxes.RegisterDoneCallback(OnRewardBoxesDone);
			TransformUtil.AttachAndPreserveLocalTransform(m_RewardBoxes.transform, m_RewardBoxesBone.transform);
			m_RewardBoxes.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback);
	}

	public void KeyFXCancel()
	{
		if ((bool)m_TheKeyIdleEffects)
		{
			PlayMakerFSM componentInChildren = m_TheKeyIdleEffects.GetComponentInChildren<PlayMakerFSM>();
			if ((bool)componentInChildren)
			{
				componentInChildren.SendEvent("Cancel");
			}
		}
	}

	private void UpdateKeyArt(int rank)
	{
		if (m_TheKeyMesh == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: key mesh missing!");
			return;
		}
		ShowRewardPaper();
		ArenaKeyVisualData arenaKeyVisualData = m_ArenaKeyVisualData[rank];
		if (arenaKeyVisualData.m_Mesh != null)
		{
			MeshFilter component = m_TheKeyMesh.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.mesh = UnityEngine.Object.Instantiate(arenaKeyVisualData.m_Mesh);
			}
		}
		if (arenaKeyVisualData.m_Material != null)
		{
			m_TheKeyMesh.GetComponent<Renderer>().SetSharedMaterial(arenaKeyVisualData.m_Material);
		}
		if (arenaKeyVisualData.m_IdleEffectsPrefabPath != string.Empty)
		{
			m_isTheKeyIdleEffectsLoading = true;
			AssetLoader.Get().InstantiatePrefab(arenaKeyVisualData.m_IdleEffectsPrefabPath, OnIdleEffectsLoaded);
		}
		if (arenaKeyVisualData.m_ParticlePrefab != null)
		{
			GameObject obj = UnityEngine.Object.Instantiate(arenaKeyVisualData.m_ParticlePrefab);
			Transform transform = obj.transform.Find("FX_Motes");
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				gameObject.transform.parent = m_TheKeyMesh.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes").Value = gameObject;
			}
			Transform transform2 = obj.transform.Find("FX_Motes_glow");
			if (transform2 != null)
			{
				GameObject gameObject2 = transform2.gameObject;
				gameObject2.transform.parent = m_TheKeyMesh.transform;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes_glow").Value = gameObject2;
			}
			Transform transform3 = obj.transform.Find("FX_Motes_trail");
			if (transform3 != null)
			{
				GameObject gameObject3 = transform3.gameObject;
				gameObject3.transform.parent = m_TheKeyMesh.transform;
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localRotation = Quaternion.identity;
				m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes_trail").Value = gameObject3;
			}
		}
		if (m_TheKeyGlowPlane != null && arenaKeyVisualData.m_EffectGlowTexture != null)
		{
			m_TheKeyGlowPlane.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_EffectGlowTexture;
		}
		if (arenaKeyVisualData.m_KeyHoleGlowMesh != null)
		{
			MeshFilter component2 = m_TheKeyGlowHoleMesh.GetComponent<MeshFilter>();
			if (component2 != null)
			{
				component2.mesh = UnityEngine.Object.Instantiate(arenaKeyVisualData.m_KeyHoleGlowMesh);
			}
		}
		if (m_TheKeySelectionGlow != null && arenaKeyVisualData.m_SelectionGlowTexture != null)
		{
			m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_SelectionGlowTexture;
		}
		SceneUtils.SetLayer(m_TheKeyMesh.transform.parent.gameObject, GameLayer.Default);
	}

	private void OnIdleEffectsLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_isTheKeyIdleEffectsLoading = false;
		if ((bool)m_TheKeyIdleEffects)
		{
			UnityEngine.Object.Destroy(m_TheKeyIdleEffects);
		}
		m_TheKeyIdleEffects = go;
		go.SetActive(value: true);
		go.transform.parent = m_TheKeyMesh.transform;
		go.transform.localPosition = Vector3.zero;
	}

	private IEnumerator AnimateKeyTransition(int rank)
	{
		yield return new WaitForSeconds(m_TheKeyTransitionDelay);
		while (m_isTheKeyIdleEffectsLoading)
		{
			yield return null;
		}
		int index = rank - 1;
		ArenaKeyVisualData arenaKeyVisualData = m_ArenaKeyVisualData[index];
		ArenaKeyVisualData keyData = m_ArenaKeyVisualData[rank];
		if (m_TheKeyOldSelectionGlow != null && arenaKeyVisualData.m_EffectGlowTexture != null)
		{
			m_TheKeyOldSelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_SelectionGlowTexture;
		}
		m_TheKeyOldSelectionGlow.GetComponent<Renderer>().enabled = true;
		Material prevKeyGlowMat = m_TheKeyOldSelectionGlow.GetComponent<Renderer>().GetMaterial();
		prevKeyGlowMat.SetFloat("_FxIntensity", 0f);
		Action<object> action = delegate(object amount)
		{
			prevKeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args = iTween.Hash("time", m_TheKeyTransitionFadeInTime, "from", 0f, "to", 1.5f, "easetype", iTween.EaseType.easeInCubic, "onupdate", action, "onupdatetarget", m_TheKeyOldSelectionGlow);
		iTween.ValueTo(m_TheKeyOldSelectionGlow, args);
		if (m_TheKeyTransitionSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(m_TheKeyTransitionSound);
		}
		yield return new WaitForSeconds(m_TheKeyTransitionFadeInTime);
		m_TheKeyTransitionParticles.Play();
		UpdateKeyArt(rank);
		m_TheKeyOldSelectionGlow.GetComponent<Renderer>().enabled = false;
		if (m_TheKeySelectionGlow != null && keyData.m_EffectGlowTexture != null)
		{
			m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = keyData.m_SelectionGlowTexture;
		}
		m_TheKeySelectionGlow.GetComponent<Renderer>().enabled = true;
		prevKeyGlowMat.SetFloat("_FxIntensity", 0f);
		Material KeyGlowMat = m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial();
		KeyGlowMat.SetFloat("_FxIntensity", 1.5f);
		Action<object> action2 = delegate(object amount)
		{
			KeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args2 = iTween.Hash("time", m_TheKeyTransitionFadeOutTime, "from", 1.5f, "to", 0f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", action2, "onupdatetarget", m_TheKeySelectionGlow);
		iTween.ValueTo(m_TheKeySelectionGlow, args2);
		yield return new WaitForSeconds(m_TheKeyTransitionFadeOutTime);
		m_TheKeySelectionGlow.GetComponent<Renderer>().enabled = false;
	}

	private void UpdateXBoxes()
	{
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			m_RewardPaper.m_XmarkBox[0].SetActive(value: true);
			m_RewardPaper.m_XmarkBox[1].SetActive(value: false);
			m_RewardPaper.m_XmarkBox[2].SetActive(value: false);
		}
	}

	private void OpenRewardBox(UIEvent e)
	{
		OpenRewardBox();
	}

	private void OpenRewardBox()
	{
		if (m_RewardPlaymaker == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: Missing Playmaker FSM!");
			return;
		}
		if (m_RewardPaper.m_EventEndsText != null)
		{
			m_RewardPaper.m_EventEndsText.Hide();
		}
		if (m_RewardPaper.m_XmarksRoot != null)
		{
			m_RewardPaper.m_XmarksRoot.SetActive(value: false);
		}
		if (m_TheKeySelectionGlow != null)
		{
			m_TheKeySelectionGlow.SetActive(value: false);
		}
		m_RewardPaper.m_WinsUberText.gameObject.SetActive(value: false);
		m_RewardPaper.m_LossesUberText.gameObject.SetActive(value: false);
		SceneUtils.EnableColliders(m_TheKeyMesh, enable: false);
		SceneUtils.SetLayer(m_TheKeyMesh.transform.parent.gameObject, GameLayer.Default);
		if ((bool)m_TheKeyIdleEffects)
		{
			PlayMakerFSM componentInChildren = m_TheKeyIdleEffects.GetComponentInChildren<PlayMakerFSM>();
			if ((bool)componentInChildren)
			{
				componentInChildren.SendEvent("Death");
			}
		}
		if (m_BehindTheDoors == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
			return;
		}
		m_BehindTheDoors.SetActive(value: true);
		m_RewardPlaymaker.SendEvent("Birth");
		StartCoroutine(m_RewardPaper.PlayRewardBurnAway(m_RewardPlaymaker));
		m_RewardPaper.PlayEmberWipeFX();
	}

	private void OnRewardBoxesDone()
	{
		if (!(this == null) && !(base.gameObject == null))
		{
			DraftManager draftManager = DraftManager.Get();
			if (draftManager.GetDraftDeck() == null)
			{
				Log.All.Print("bug 8052, null exception");
			}
			else
			{
				Network.Get().AckDraftRewards(draftManager.GetDraftDeck().ID, draftManager.GetSlot());
			}
			DraftDisplay.Get().OnOpenRewardsComplete();
		}
	}

	private void ShowPlainPaper()
	{
		m_Paper.SetActive(value: false);
		if (m_PaperMain != null)
		{
			m_PaperMain.SetActive(value: true);
		}
		m_RewardPaper.m_XmarksRoot.SetActive(value: false);
		m_RewardPaper.m_WinsUberText.Hide();
		m_RewardPaper.m_LossesUberText.Hide();
	}

	private void ShowRewardPaper()
	{
		m_Paper.SetActive(value: true);
		if (m_PaperMain != null)
		{
			m_PaperMain.SetActive(value: false);
		}
		m_RewardPaper.m_XmarksRoot.SetActive(value: true);
		m_RewardPaper.m_WinsUberText.Show();
		m_RewardPaper.m_LossesUberText.Show();
		if (m_RewardPaper.m_EventEndsText != null)
		{
			if (DraftManager.Get().CurrentSeasonId == 0)
			{
				m_RewardPaper.m_EventEndsText.Text = string.Empty;
				return;
			}
			TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
			{
				m_seconds = "GLUE_ARENA_LABEL_SEASON_ENDING_SECONDS",
				m_minutes = "GLUE_ARENA_LABEL_SEASON_ENDING_MINUTES",
				m_hours = "GLUE_ARENA_LABEL_SEASON_ENDING_HOURS",
				m_yesterday = null,
				m_days = "GLUE_ARENA_LABEL_SEASON_ENDING_DAYS",
				m_weeks = "GLUE_ARENA_LABEL_SEASON_ENDING_WEEKS",
				m_monthAgo = "GLUE_ARENA_LABEL_SEASON_ENDING_OVER_1_MONTH"
			};
			long secondsUntilEndOfSeason = (long)DraftManager.Get().SecondsUntilEndOfSeason;
			m_RewardPaper.m_EventEndsText.Text = TimeUtils.GetElapsedTimeString(secondsUntilEndOfSeason, stringSet, roundUp: true);
		}
	}

	private void HidePaper()
	{
		m_Paper.SetActive(value: false);
	}
}
