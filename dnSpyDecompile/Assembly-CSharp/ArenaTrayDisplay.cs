using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020002B3 RID: 691
[CustomEditClass]
public class ArenaTrayDisplay : MonoBehaviour
{
	// Token: 0x060023C9 RID: 9161 RVA: 0x000B25D0 File Offset: 0x000B07D0
	private void Awake()
	{
		ArenaTrayDisplay.s_Instance = this;
		AssetReference draftPaperTexture = DraftManager.Get().GetDraftPaperTexture();
		AssetLoader.Get().LoadAsset<Texture>(draftPaperTexture, delegate(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
		{
			if (loadedTexture == null)
			{
				Debug.LogWarningFormat("ArenaTrayDisplay: Failed to load {0}.", new object[]
				{
					assetRef.ToString()
				});
				return;
			}
			AssetHandle.Take<Texture>(ref this.m_paperTexture, loadedTexture);
			this.m_PaperMain.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_paperTexture;
		}, null, AssetLoadingOptions.None);
		AssetReference rewardPaperPrefab = DraftManager.Get().GetRewardPaperPrefab();
		AssetLoader.Get().InstantiatePrefab(rewardPaperPrefab, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("ArenaTrayDisplay: Failed to load {0}.", assetRef.ToString()));
				return;
			}
			ArenaTrayDisplay.s_Instance.m_RewardPaper = go.GetComponent<ArenaRewardPaper>();
			go.transform.parent = base.gameObject.transform;
			if (ArenaTrayDisplay.s_Instance.m_RewardPaperBone != null)
			{
				go.transform.position = ArenaTrayDisplay.s_Instance.m_RewardPaperBone.transform.position;
				go.transform.localScale = ArenaTrayDisplay.s_Instance.m_RewardPaperBone.transform.localScale;
			}
			else
			{
				Debug.LogWarning("ArenaTrayDisplay: m_RewardPaperBone is not set, so ArenaRewardPaper may look wrong.");
			}
			ArenaTrayDisplay.s_Instance.m_Paper = go;
			this.ShowPlainPaperBackground();
			if (ArenaTrayDisplay.s_Instance.m_RewardPaper == null)
			{
				Debug.LogWarning(string.Format("ArenaTrayDisplay: m_RewardPaper is null! Check the prefab you're loading, {0}.", assetRef.ToString()));
				ArenaTrayDisplay.s_Instance.m_isReady = true;
				return;
			}
			if (ArenaTrayDisplay.s_Instance.m_RewardPaper.m_WinsUberText == null || ArenaTrayDisplay.s_Instance.m_RewardPaper.m_LossesUberText == null)
			{
				Debug.LogWarning(string.Format("ArenaTrayDisplay: m_WinsUberText or m_LossesUberText is null! Check the prefab you're loading, {0}", assetRef.ToString()));
				ArenaTrayDisplay.s_Instance.m_isReady = true;
				return;
			}
			ArenaTrayDisplay.s_Instance.m_RewardPaper.m_WinsUberText.Text = GameStrings.Get("GLUE_DRAFT_WINS_LABEL");
			ArenaTrayDisplay.s_Instance.m_RewardPaper.m_LossesUberText.Text = GameStrings.Get("GLUE_DRAFT_LOSSES_LABEL");
			if (ArenaTrayDisplay.s_Instance.m_BehindTheDoors == null)
			{
				Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
				ArenaTrayDisplay.s_Instance.m_isReady = true;
				return;
			}
			ArenaTrayDisplay.s_Instance.m_BehindTheDoors.SetActive(false);
			if (ArenaTrayDisplay.s_Instance.m_RewardDoorPlates == null)
			{
				Debug.LogWarning("ArenaTrayDisplay: m_RewardDoorPlates is null!");
				ArenaTrayDisplay.s_Instance.m_isReady = true;
				return;
			}
			ArenaTrayDisplay.s_Instance.m_RewardDoorPlates.SetActive(false);
			SceneUtils.EnableColliders(ArenaTrayDisplay.s_Instance.m_TheKeyMesh, false);
			ArenaTrayDisplay.s_Instance.m_isReady = true;
		}, null, AssetLoadingOptions.None);
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x000B262D File Offset: 0x000B082D
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_paperTexture);
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x000B263A File Offset: 0x000B083A
	public static ArenaTrayDisplay Get()
	{
		return ArenaTrayDisplay.s_Instance;
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x000B2641 File Offset: 0x000B0841
	public bool IsReady()
	{
		return this.m_isReady;
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x000B2649 File Offset: 0x000B0849
	public void UpdateTray()
	{
		this.UpdateTray(true);
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x000B2654 File Offset: 0x000B0854
	public void UpdateTray(bool showNewKey)
	{
		this.ShowPlainPaper();
		if (this.m_InstructionText != null)
		{
			this.m_InstructionText.SetActive(false);
		}
		if (this.m_RewardDoorPlates != null && !this.m_RewardDoorPlates.activeSelf)
		{
			this.m_RewardDoorPlates.SetActive(true);
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
		this.m_WinCountUberText.Text = wins.ToString();
		this.m_RewardPaper.m_Xmark1.GetComponent<Renderer>().enabled = (losses > 0);
		this.m_RewardPaper.m_Xmark2.GetComponent<Renderer>().enabled = (losses > 1);
		this.m_RewardPaper.m_Xmark3.GetComponent<Renderer>().enabled = (losses > 2);
		this.UpdateXBoxes();
		if (flag && wins > 0 && showNewKey)
		{
			this.UpdateKeyArt(wins - 1);
			base.StartCoroutine(this.AnimateKeyTransition(wins));
			return;
		}
		this.UpdateKeyArt(wins);
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x000B2780 File Offset: 0x000B0980
	public void ShowPlainPaperBackground()
	{
		this.ShowPlainPaper();
		if (this.m_InstructionText != null)
		{
			this.m_InstructionText.SetActive(true);
		}
		if (this.m_RewardDoorPlates != null && this.m_RewardDoorPlates.activeSelf)
		{
			this.m_RewardDoorPlates.SetActive(false);
		}
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x000B27D4 File Offset: 0x000B09D4
	public void ActivateKey()
	{
		SceneUtils.EnableColliders(this.m_TheKeyMesh, true);
		Renderer component = this.m_TheKeySelectionGlow.GetComponent<Renderer>();
		component.enabled = true;
		Material sharedMaterial = component.GetSharedMaterial();
		Color color = sharedMaterial.color;
		color.a = 0f;
		sharedMaterial.color = color;
		sharedMaterial.SetFloat("_FxIntensity", 1f);
		iTween.FadeTo(this.m_TheKeySelectionGlow, iTween.Hash(new object[]
		{
			"alpha",
			0.8f,
			"time",
			2f,
			"easetype",
			iTween.EaseType.easeInOutBack
		}));
		Material KeyGlowMat = component.GetMaterial();
		KeyGlowMat.SetFloat("_FxIntensity", 0f);
		Action<object> action = delegate(object amount)
		{
			KeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			2f,
			"from",
			0f,
			"to",
			1f,
			"easetype",
			iTween.EaseType.easeInOutBack,
			"onupdate",
			action,
			"onupdatetarget",
			this.m_TheKeySelectionGlow
		});
		iTween.ValueTo(this.m_TheKeySelectionGlow, args);
		PegUIElement component2 = this.m_TheKeyMesh.GetComponent<PegUIElement>();
		if (component2 == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: PegUIElement missing on the Key!");
			return;
		}
		component2.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OpenRewardBox));
		Navigation.PushBlockBackingOut();
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x000B2980 File Offset: 0x000B0B80
	public void ShowRewardsOpenAtStart()
	{
		if (this.m_RewardPlaymaker == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: Missing Playmaker FSM!");
			return;
		}
		this.HidePaper();
		if (this.m_InstructionText != null)
		{
			this.m_InstructionText.SetActive(false);
		}
		if (this.m_InstructionDetailText != null)
		{
			this.m_InstructionDetailText.SetActive(false);
		}
		if (this.m_WinCountUberText != null)
		{
			this.m_WinCountUberText.gameObject.SetActive(false);
		}
		if (this.m_RewardPaper.m_WinsUberText != null)
		{
			this.m_RewardPaper.m_WinsUberText.gameObject.SetActive(false);
		}
		if (this.m_RewardPaper.m_LossesUberText != null)
		{
			this.m_RewardPaper.m_LossesUberText.gameObject.SetActive(false);
		}
		if (this.m_RewardPaper.m_XmarksRoot != null)
		{
			this.m_RewardPaper.m_XmarksRoot.SetActive(false);
		}
		if (this.m_TheKeySelectionGlow != null)
		{
			this.m_TheKeySelectionGlow.SetActive(false);
		}
		this.m_RewardPaper.m_WinsUberText.gameObject.SetActive(false);
		this.m_RewardPaper.m_LossesUberText.gameObject.SetActive(false);
		this.m_TheKeyMesh.gameObject.SetActive(false);
		if (this.m_BehindTheDoors == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
			return;
		}
		this.m_BehindTheDoors.SetActive(true);
		if (DraftManager.Get() == null)
		{
			Debug.LogError("ArenaTrayDisplay: DraftManager.Get() == null!");
			return;
		}
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			this.m_RewardBoxes = go.GetComponent<RewardBoxesDisplay>();
			this.m_RewardBoxes.SetRewards(DraftManager.Get().GetRewards());
			this.m_RewardBoxes.RegisterDoneCallback(new Action(this.OnRewardBoxesDone));
			TransformUtil.AttachAndPreserveLocalTransform(this.m_RewardBoxes.transform, this.m_RewardBoxesBone.transform);
			this.m_RewardBoxes.DebugLogRewards();
			this.m_RewardBoxes.ShowAlreadyOpenedRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, null, AssetLoadingOptions.None);
		this.m_RewardPlaymaker.gameObject.SetActive(true);
		this.m_RewardPlaymaker.SendEvent("Death");
		if (this.m_TheKeyMesh.GetComponent<PegUIElement>() == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: PegUIElement missing on the Key!");
			return;
		}
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void ShowOpenedRewards()
	{
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x000B2B68 File Offset: 0x000B0D68
	public void AnimateRewards()
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			this.m_RewardBoxes = go.GetComponent<RewardBoxesDisplay>();
			this.m_RewardBoxes.SetRewards(DraftManager.Get().GetRewards());
			this.m_RewardBoxes.RegisterDoneCallback(new Action(this.OnRewardBoxesDone));
			TransformUtil.AttachAndPreserveLocalTransform(this.m_RewardBoxes.transform, this.m_RewardBoxesBone.transform);
			this.m_RewardBoxes.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x000B2B9C File Offset: 0x000B0D9C
	public void KeyFXCancel()
	{
		if (this.m_TheKeyIdleEffects)
		{
			PlayMakerFSM componentInChildren = this.m_TheKeyIdleEffects.GetComponentInChildren<PlayMakerFSM>();
			if (componentInChildren)
			{
				componentInChildren.SendEvent("Cancel");
			}
		}
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x000B2BD8 File Offset: 0x000B0DD8
	private void UpdateKeyArt(int rank)
	{
		if (this.m_TheKeyMesh == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: key mesh missing!");
			return;
		}
		this.ShowRewardPaper();
		ArenaTrayDisplay.ArenaKeyVisualData arenaKeyVisualData = this.m_ArenaKeyVisualData[rank];
		if (arenaKeyVisualData.m_Mesh != null)
		{
			MeshFilter component = this.m_TheKeyMesh.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.mesh = UnityEngine.Object.Instantiate<Mesh>(arenaKeyVisualData.m_Mesh);
			}
		}
		if (arenaKeyVisualData.m_Material != null)
		{
			this.m_TheKeyMesh.GetComponent<Renderer>().SetSharedMaterial(arenaKeyVisualData.m_Material);
		}
		if (arenaKeyVisualData.m_IdleEffectsPrefabPath != string.Empty)
		{
			this.m_isTheKeyIdleEffectsLoading = true;
			AssetLoader.Get().InstantiatePrefab(arenaKeyVisualData.m_IdleEffectsPrefabPath, new PrefabCallback<GameObject>(this.OnIdleEffectsLoaded), null, AssetLoadingOptions.None);
		}
		if (arenaKeyVisualData.m_ParticlePrefab != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(arenaKeyVisualData.m_ParticlePrefab);
			Transform transform = gameObject.transform.Find("FX_Motes");
			if (transform != null)
			{
				GameObject gameObject2 = transform.gameObject;
				gameObject2.transform.parent = this.m_TheKeyMesh.transform;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				this.m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes").Value = gameObject2;
			}
			Transform transform2 = gameObject.transform.Find("FX_Motes_glow");
			if (transform2 != null)
			{
				GameObject gameObject3 = transform2.gameObject;
				gameObject3.transform.parent = this.m_TheKeyMesh.transform;
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localRotation = Quaternion.identity;
				this.m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes_glow").Value = gameObject3;
			}
			Transform transform3 = gameObject.transform.Find("FX_Motes_trail");
			if (transform3 != null)
			{
				GameObject gameObject4 = transform3.gameObject;
				gameObject4.transform.parent = this.m_TheKeyMesh.transform;
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.transform.localRotation = Quaternion.identity;
				this.m_RewardPlaymaker.FsmVariables.GetFsmGameObject("FX_Motes_trail").Value = gameObject4;
			}
		}
		if (this.m_TheKeyGlowPlane != null && arenaKeyVisualData.m_EffectGlowTexture != null)
		{
			this.m_TheKeyGlowPlane.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_EffectGlowTexture;
		}
		if (arenaKeyVisualData.m_KeyHoleGlowMesh != null)
		{
			MeshFilter component2 = this.m_TheKeyGlowHoleMesh.GetComponent<MeshFilter>();
			if (component2 != null)
			{
				component2.mesh = UnityEngine.Object.Instantiate<Mesh>(arenaKeyVisualData.m_KeyHoleGlowMesh);
			}
		}
		if (this.m_TheKeySelectionGlow != null && arenaKeyVisualData.m_SelectionGlowTexture != null)
		{
			this.m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_SelectionGlowTexture;
		}
		SceneUtils.SetLayer(this.m_TheKeyMesh.transform.parent.gameObject, GameLayer.Default);
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x000B2EEC File Offset: 0x000B10EC
	private void OnIdleEffectsLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_isTheKeyIdleEffectsLoading = false;
		if (this.m_TheKeyIdleEffects)
		{
			UnityEngine.Object.Destroy(this.m_TheKeyIdleEffects);
		}
		this.m_TheKeyIdleEffects = go;
		go.SetActive(true);
		go.transform.parent = this.m_TheKeyMesh.transform;
		go.transform.localPosition = Vector3.zero;
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x000B2F4C File Offset: 0x000B114C
	private IEnumerator AnimateKeyTransition(int rank)
	{
		yield return new WaitForSeconds(this.m_TheKeyTransitionDelay);
		while (this.m_isTheKeyIdleEffectsLoading)
		{
			yield return null;
		}
		int index = rank - 1;
		ArenaTrayDisplay.ArenaKeyVisualData arenaKeyVisualData = this.m_ArenaKeyVisualData[index];
		ArenaTrayDisplay.ArenaKeyVisualData keyData = this.m_ArenaKeyVisualData[rank];
		if (this.m_TheKeyOldSelectionGlow != null && arenaKeyVisualData.m_EffectGlowTexture != null)
		{
			this.m_TheKeyOldSelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = arenaKeyVisualData.m_SelectionGlowTexture;
		}
		this.m_TheKeyOldSelectionGlow.GetComponent<Renderer>().enabled = true;
		Material prevKeyGlowMat = this.m_TheKeyOldSelectionGlow.GetComponent<Renderer>().GetMaterial();
		prevKeyGlowMat.SetFloat("_FxIntensity", 0f);
		Action<object> action = delegate(object amount)
		{
			prevKeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_TheKeyTransitionFadeInTime,
			"from",
			0f,
			"to",
			1.5f,
			"easetype",
			iTween.EaseType.easeInCubic,
			"onupdate",
			action,
			"onupdatetarget",
			this.m_TheKeyOldSelectionGlow
		});
		iTween.ValueTo(this.m_TheKeyOldSelectionGlow, args);
		if (this.m_TheKeyTransitionSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(this.m_TheKeyTransitionSound);
		}
		yield return new WaitForSeconds(this.m_TheKeyTransitionFadeInTime);
		this.m_TheKeyTransitionParticles.Play();
		this.UpdateKeyArt(rank);
		this.m_TheKeyOldSelectionGlow.GetComponent<Renderer>().enabled = false;
		if (this.m_TheKeySelectionGlow != null && keyData.m_EffectGlowTexture != null)
		{
			this.m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial().mainTexture = keyData.m_SelectionGlowTexture;
		}
		this.m_TheKeySelectionGlow.GetComponent<Renderer>().enabled = true;
		prevKeyGlowMat.SetFloat("_FxIntensity", 0f);
		Material KeyGlowMat = this.m_TheKeySelectionGlow.GetComponent<Renderer>().GetMaterial();
		KeyGlowMat.SetFloat("_FxIntensity", 1.5f);
		Action<object> action2 = delegate(object amount)
		{
			KeyGlowMat.SetFloat("_FxIntensity", (float)amount);
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			this.m_TheKeyTransitionFadeOutTime,
			"from",
			1.5f,
			"to",
			0f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"onupdate",
			action2,
			"onupdatetarget",
			this.m_TheKeySelectionGlow
		});
		iTween.ValueTo(this.m_TheKeySelectionGlow, args2);
		yield return new WaitForSeconds(this.m_TheKeyTransitionFadeOutTime);
		this.m_TheKeySelectionGlow.GetComponent<Renderer>().enabled = false;
		yield break;
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x000B2F64 File Offset: 0x000B1164
	private void UpdateXBoxes()
	{
		if (!DemoMgr.Get().ArenaIs1WinMode())
		{
			return;
		}
		this.m_RewardPaper.m_XmarkBox[0].SetActive(true);
		this.m_RewardPaper.m_XmarkBox[1].SetActive(false);
		this.m_RewardPaper.m_XmarkBox[2].SetActive(false);
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x000B2FC3 File Offset: 0x000B11C3
	private void OpenRewardBox(UIEvent e)
	{
		this.OpenRewardBox();
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x000B2FCC File Offset: 0x000B11CC
	private void OpenRewardBox()
	{
		if (this.m_RewardPlaymaker == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: Missing Playmaker FSM!");
			return;
		}
		if (this.m_RewardPaper.m_EventEndsText != null)
		{
			this.m_RewardPaper.m_EventEndsText.Hide();
		}
		if (this.m_RewardPaper.m_XmarksRoot != null)
		{
			this.m_RewardPaper.m_XmarksRoot.SetActive(false);
		}
		if (this.m_TheKeySelectionGlow != null)
		{
			this.m_TheKeySelectionGlow.SetActive(false);
		}
		this.m_RewardPaper.m_WinsUberText.gameObject.SetActive(false);
		this.m_RewardPaper.m_LossesUberText.gameObject.SetActive(false);
		SceneUtils.EnableColliders(this.m_TheKeyMesh, false);
		SceneUtils.SetLayer(this.m_TheKeyMesh.transform.parent.gameObject, GameLayer.Default);
		if (this.m_TheKeyIdleEffects)
		{
			PlayMakerFSM componentInChildren = this.m_TheKeyIdleEffects.GetComponentInChildren<PlayMakerFSM>();
			if (componentInChildren)
			{
				componentInChildren.SendEvent("Death");
			}
		}
		if (this.m_BehindTheDoors == null)
		{
			Debug.LogWarning("ArenaTrayDisplay: m_BehindTheDoors is null!");
			return;
		}
		this.m_BehindTheDoors.SetActive(true);
		this.m_RewardPlaymaker.SendEvent("Birth");
		base.StartCoroutine(this.m_RewardPaper.PlayRewardBurnAway(this.m_RewardPlaymaker));
		this.m_RewardPaper.PlayEmberWipeFX();
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x000B312C File Offset: 0x000B132C
	private void OnRewardBoxesDone()
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		DraftManager draftManager = DraftManager.Get();
		if (draftManager.GetDraftDeck() == null)
		{
			Log.All.Print("bug 8052, null exception", Array.Empty<object>());
		}
		else
		{
			Network.Get().AckDraftRewards(draftManager.GetDraftDeck().ID, draftManager.GetSlot());
		}
		DraftDisplay.Get().OnOpenRewardsComplete();
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x000B319C File Offset: 0x000B139C
	private void ShowPlainPaper()
	{
		this.m_Paper.SetActive(false);
		if (this.m_PaperMain != null)
		{
			this.m_PaperMain.SetActive(true);
		}
		this.m_RewardPaper.m_XmarksRoot.SetActive(false);
		this.m_RewardPaper.m_WinsUberText.Hide();
		this.m_RewardPaper.m_LossesUberText.Hide();
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x000B3200 File Offset: 0x000B1400
	private void ShowRewardPaper()
	{
		this.m_Paper.SetActive(true);
		if (this.m_PaperMain != null)
		{
			this.m_PaperMain.SetActive(false);
		}
		this.m_RewardPaper.m_XmarksRoot.SetActive(true);
		this.m_RewardPaper.m_WinsUberText.Show();
		this.m_RewardPaper.m_LossesUberText.Show();
		if (this.m_RewardPaper.m_EventEndsText != null)
		{
			if (DraftManager.Get().CurrentSeasonId == 0)
			{
				this.m_RewardPaper.m_EventEndsText.Text = string.Empty;
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
			this.m_RewardPaper.m_EventEndsText.Text = TimeUtils.GetElapsedTimeString(secondsUntilEndOfSeason, stringSet, true);
		}
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x000B330E File Offset: 0x000B150E
	private void HidePaper()
	{
		this.m_Paper.SetActive(false);
	}

	// Token: 0x040013EF RID: 5103
	public int m_Rank;

	// Token: 0x040013F0 RID: 5104
	public PlayMakerFSM m_RewardPlaymaker;

	// Token: 0x040013F1 RID: 5105
	[CustomEditField(Sections = "Keys")]
	public GameObject m_TheKeyMesh;

	// Token: 0x040013F2 RID: 5106
	public GameObject m_TheKeyGlowPlane;

	// Token: 0x040013F3 RID: 5107
	public GameObject m_TheKeyGlowHoleMesh;

	// Token: 0x040013F4 RID: 5108
	public GameObject m_TheKeySelectionGlow;

	// Token: 0x040013F5 RID: 5109
	public GameObject m_TheKeyOldSelectionGlow;

	// Token: 0x040013F6 RID: 5110
	public float m_TheKeyTransitionDelay = 0.5f;

	// Token: 0x040013F7 RID: 5111
	public float m_TheKeyTransitionFadeInTime = 1.5f;

	// Token: 0x040013F8 RID: 5112
	public float m_TheKeyTransitionFadeOutTime = 2f;

	// Token: 0x040013F9 RID: 5113
	public ParticleSystem m_TheKeyTransitionParticles;

	// Token: 0x040013FA RID: 5114
	public string m_TheKeyTransitionSound = "arena_key_transition.prefab:7b4c3a5222405834abd921cbf53bf689";

	// Token: 0x040013FB RID: 5115
	[CustomEditField(Sections = "Reward Panel")]
	public UberText m_WinCountUberText;

	// Token: 0x040013FC RID: 5116
	public GameObject m_RewardDoorPlates;

	// Token: 0x040013FD RID: 5117
	public GameObject m_BehindTheDoors;

	// Token: 0x040013FE RID: 5118
	public GameObject m_RewardPaperBone;

	// Token: 0x040013FF RID: 5119
	public GameObject m_PaperMain;

	// Token: 0x04001400 RID: 5120
	public GameObject m_RewardBoxesBone;

	// Token: 0x04001401 RID: 5121
	public GameObject m_InstructionText;

	// Token: 0x04001402 RID: 5122
	public GameObject m_InstructionDetailText;

	// Token: 0x04001403 RID: 5123
	public List<ArenaTrayDisplay.ArenaKeyVisualData> m_ArenaKeyVisualData;

	// Token: 0x04001404 RID: 5124
	private RewardBoxesDisplay m_RewardBoxes;

	// Token: 0x04001405 RID: 5125
	private GameObject m_TheKeyIdleEffects;

	// Token: 0x04001406 RID: 5126
	private bool m_isTheKeyIdleEffectsLoading;

	// Token: 0x04001407 RID: 5127
	private ArenaRewardPaper m_RewardPaper;

	// Token: 0x04001408 RID: 5128
	private GameObject m_Paper;

	// Token: 0x04001409 RID: 5129
	private AssetHandle<Texture> m_paperTexture;

	// Token: 0x0400140A RID: 5130
	private bool m_isReady;

	// Token: 0x0400140B RID: 5131
	private static ArenaTrayDisplay s_Instance;

	// Token: 0x020015AA RID: 5546
	[Serializable]
	public class ArenaKeyVisualData
	{
		// Token: 0x0400AE9C RID: 44700
		public Mesh m_Mesh;

		// Token: 0x0400AE9D RID: 44701
		public Material m_Material;

		// Token: 0x0400AE9E RID: 44702
		public Mesh m_KeyHoleGlowMesh;

		// Token: 0x0400AE9F RID: 44703
		public Texture m_EffectGlowTexture;

		// Token: 0x0400AEA0 RID: 44704
		public Texture m_SelectionGlowTexture;

		// Token: 0x0400AEA1 RID: 44705
		public GameObject m_ParticlePrefab;

		// Token: 0x0400AEA2 RID: 44706
		public string m_IdleEffectsPrefabPath;
	}
}
