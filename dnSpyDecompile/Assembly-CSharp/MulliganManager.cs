using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class MulliganManager : MonoBehaviour
{
	// Token: 0x06002DEA RID: 11754 RVA: 0x000E966D File Offset: 0x000E786D
	private void Awake()
	{
		MulliganManager.s_instance = this;
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x000E9678 File Offset: 0x000E7878
	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
			GameState.Get().UnregisterMulliganTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnMulliganTimerUpdate));
			GameState.Get().UnregisterEntitiesChosenReceivedListener(new GameState.EntitiesChosenReceivedCallback(this.OnEntitiesChosenReceived));
			GameState.Get().UnregisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
		MulliganManager.s_instance = null;
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x000E96F0 File Offset: 0x000E78F0
	private void Start()
	{
		if (GameState.Get() == null)
		{
			Debug.LogError(string.Format("MulliganManager.Start() - GameState already Shutdown before MulliganManager was loaded.", Array.Empty<object>()));
			return;
		}
		if (GameState.Get().IsGameCreatedOrCreating())
		{
			this.HandleGameStart();
		}
		else
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		}
		GameState.Get().RegisterMulliganTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnMulliganTimerUpdate));
		GameState.Get().RegisterEntitiesChosenReceivedListener(new GameState.EntitiesChosenReceivedCallback(this.OnEntitiesChosenReceived));
		GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.myheroAnimatesToPosition = this.myheroAnimatesToPosition_iPhone;
			this.hisheroAnimatesToPosition = this.hisheroAnimatesToPosition_iPhone;
			this.cardAnimatesFromBoardToDeck = this.cardAnimatesFromBoardToDeck_iPhone;
		}
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x000E97BA File Offset: 0x000E79BA
	public static MulliganManager Get()
	{
		return MulliganManager.s_instance;
	}

	// Token: 0x06002DEE RID: 11758 RVA: 0x000E97C1 File Offset: 0x000E79C1
	public bool IsCustomIntroActive()
	{
		return this.m_customIntroCoroutine != null;
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x000E97CC File Offset: 0x000E79CC
	public bool IsMulliganActive()
	{
		return this.mulliganActive;
	}

	// Token: 0x06002DF0 RID: 11760 RVA: 0x000E97D4 File Offset: 0x000E79D4
	public bool IsMulliganIntroActive()
	{
		return !this.introComplete;
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x000E97DF File Offset: 0x000E79DF
	public void ForceMulliganActive(bool active)
	{
		this.mulliganActive = active;
		if (this.mulliganActive)
		{
			GameState.Get().HideZzzEffects();
			return;
		}
		GameState.Get().UnhideZzzEffects();
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x000E9805 File Offset: 0x000E7A05
	public void LoadMulliganButton()
	{
		if (this.m_WaitForBoardThenLoadButton != null)
		{
			base.StopCoroutine(this.m_WaitForBoardThenLoadButton);
		}
		this.m_WaitForBoardThenLoadButton = this.WaitForBoardThenLoadButton();
		base.StartCoroutine(this.m_WaitForBoardThenLoadButton);
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x000E9834 File Offset: 0x000E7A34
	private IEnumerator WaitForBoardThenLoadButton()
	{
		while (Gameplay.Get().GetBoardLayout() == null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			AssetLoader.Get().InstantiatePrefab("MulliganButton.prefab:f58c065fc711b604c891cefd1faf722a", new PrefabCallback<GameObject>(this.OnMulliganButtonLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		if (this.conditionalHelperTextLabel != null)
		{
			this.conditionalHelperTextLabel.transform.position = Board.Get().FindBone("MulliganHelperTextPosition").position;
		}
		yield break;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x000E9844 File Offset: 0x000E7A44
	private void OnMulliganButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("MulliganManager.OnMulliganButtonLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		this.mulliganButton = go.GetComponent<NormalButton>();
		if (this.mulliganButton == null)
		{
			Debug.LogError(string.Format("MulliganManager.OnMulliganButtonLoaded() - ERROR \"{0}\" has no {1} component", assetRef, typeof(NormalButton)));
			return;
		}
		this.mulliganButton.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		this.mulliganButtonWidget.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			this.mulliganButton.SetEnabled(false, false);
			this.mulliganButton.gameObject.SetActive(false);
			this.mulliganButtonWidget.SetEnabled(false);
			return;
		}
		this.mulliganButtonWidget.gameObject.SetActive(false);
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x000E9914 File Offset: 0x000E7B14
	private void OnVersusVoLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.waitingForVersusVo = false;
		if (go == null)
		{
			Debug.LogError(string.Format("MulliganManager.OnVersusVoLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		this.versusVo = go.GetComponent<AudioSource>();
		if (this.versusVo == null)
		{
			Debug.LogError(string.Format("MulliganManager.OnVersusVoLoaded() - ERROR \"{0}\" has no {1} component", assetRef, typeof(AudioSource)));
			return;
		}
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x000E9978 File Offset: 0x000E7B78
	private void OnVersusTextLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.waitingForVersusText = false;
		if (go == null)
		{
			Debug.LogError(string.Format("MulliganManager.OnVersusTextLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		this.versusText = go.GetComponent<GameStartVsLetters>();
		if (this.versusText == null)
		{
			Log.All.PrintError("MulliganManager.OnVersusTextLoaded() object loaded does not have a GameStartVsLetters component", Array.Empty<object>());
		}
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x000E99D4 File Offset: 0x000E7BD4
	private IEnumerator WaitForHeroesAndStartAnimations()
	{
		MulliganManager.<>c__DisplayClass118_0 CS$<>8__locals1 = new MulliganManager.<>c__DisplayClass118_0();
		CS$<>8__locals1.<>4__this = this;
		Log.LoadingScreen.Print("MulliganManager.WaitForHeroesAndStartAnimations()", Array.Empty<object>());
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Player friendlyPlayer;
		for (friendlyPlayer = GameState.Get().GetFriendlySidePlayer(); friendlyPlayer == null; friendlyPlayer = GameState.Get().GetFriendlySidePlayer())
		{
			yield return null;
		}
		Player opposingPlayer;
		for (opposingPlayer = GameState.Get().GetOpposingSidePlayer(); opposingPlayer == null; opposingPlayer = GameState.Get().GetOpposingSidePlayer())
		{
			yield return null;
		}
		global::Card myHeroCard = null;
		while (this.myHeroCardActor == null)
		{
			myHeroCard = friendlyPlayer.GetHeroCard();
			if (myHeroCard != null)
			{
				this.myHeroCardActor = myHeroCard.GetActor();
			}
			yield return null;
		}
		global::Card hisHeroCard = null;
		while (this.hisHeroCardActor == null)
		{
			hisHeroCard = opposingPlayer.GetHeroCard();
			if (hisHeroCard != null)
			{
				this.hisHeroCardActor = hisHeroCard.GetActor();
			}
			yield return null;
		}
		while (friendlyPlayer.GetHeroPower() != null)
		{
			if (!(this.myHeroPowerCardActor == null))
			{
				break;
			}
			global::Card heroPowerCard = friendlyPlayer.GetHeroPowerCard();
			if (heroPowerCard != null)
			{
				this.myHeroPowerCardActor = heroPowerCard.GetActor();
				if (this.myHeroPowerCardActor != null)
				{
					this.myHeroPowerCardActor.TurnOffCollider();
				}
			}
			yield return null;
		}
		while (opposingPlayer.GetHeroPower() != null)
		{
			if (!(this.hisHeroPowerCardActor == null))
			{
				break;
			}
			global::Card heroPowerCard2 = opposingPlayer.GetHeroPowerCard();
			if (heroPowerCard2 != null)
			{
				this.hisHeroPowerCardActor = heroPowerCard2.GetActor();
				if (this.hisHeroPowerCardActor != null)
				{
					this.hisHeroPowerCardActor.TurnOffCollider();
				}
			}
			yield return null;
		}
		while (GameState.Get() == null || GameState.Get().GetGameEntity().IsPreloadingAssets())
		{
			yield return null;
		}
		while (!this.myHeroCardActor.HasCardDef)
		{
			yield return null;
		}
		while (!this.hisHeroCardActor.HasCardDef)
		{
			yield return null;
		}
		this.LoadMyHeroSkinSocketInEffect(this.myHeroCardActor);
		this.LoadHisHeroSkinSocketInEffect(this.hisHeroCardActor);
		while (this.m_isLoadingMyCustomSocketIn || this.m_isLoadingHisCustomSocketIn)
		{
			yield return null;
		}
		List<Material> materials = this.myHeroCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		CS$<>8__locals1.myHeroMat = materials[this.myHeroCardActor.m_portraitMatIdx];
		CS$<>8__locals1.myHeroFrameMat = materials[this.myHeroCardActor.m_portraitFrameMatIdx];
		if (CS$<>8__locals1.myHeroMat != null && CS$<>8__locals1.myHeroMat.HasProperty("_LightingBlend"))
		{
			CS$<>8__locals1.myHeroMat.SetFloat("_LightingBlend", 0f);
		}
		if (CS$<>8__locals1.myHeroFrameMat != null && CS$<>8__locals1.myHeroFrameMat.HasProperty("_LightingBlend"))
		{
			CS$<>8__locals1.myHeroFrameMat.SetFloat("_LightingBlend", 0f);
		}
		float value = GameState.Get().GetBooleanGameOption(GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN) ? 1f : 0f;
		List<Material> materials2 = this.hisHeroCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		CS$<>8__locals1.hisHeroMat = materials2[this.hisHeroCardActor.m_portraitMatIdx];
		CS$<>8__locals1.hisHeroFrameMat = materials2[this.hisHeroCardActor.m_portraitFrameMatIdx];
		if (CS$<>8__locals1.hisHeroMat != null && CS$<>8__locals1.hisHeroMat.HasProperty("_LightingBlend"))
		{
			CS$<>8__locals1.hisHeroMat.SetFloat("_LightingBlend", value);
		}
		if (CS$<>8__locals1.hisHeroFrameMat != null && CS$<>8__locals1.hisHeroFrameMat.HasProperty("_LightingBlend"))
		{
			CS$<>8__locals1.hisHeroFrameMat.SetFloat("_LightingBlend", value);
		}
		if (this.myHeroPowerCardActor != null && this.myHeroPowerCardActor.m_portraitMesh != null)
		{
			List<Material> materials3 = this.myHeroPowerCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			Material material = materials3[this.myHeroPowerCardActor.m_portraitMatIdx];
			if (material != null && material.HasProperty("_LightingBlend"))
			{
				material.SetFloat("_LightingBlend", 1f);
			}
			Material material2 = materials3[this.myHeroPowerCardActor.m_portraitFrameMatIdx];
			if (material2 != null && material2.HasProperty("_LightingBlend"))
			{
				material2.SetFloat("_LightingBlend", 1f);
			}
		}
		if (this.hisHeroPowerCardActor != null && this.hisHeroPowerCardActor.m_portraitMesh != null)
		{
			List<Material> materials4 = this.hisHeroPowerCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			Material material3 = materials4[this.hisHeroPowerCardActor.m_portraitMatIdx];
			if (material3 != null && material3.HasProperty("_LightingBlend"))
			{
				material3.SetFloat("_LightingBlend", 1f);
			}
			Material material4 = materials4[this.hisHeroPowerCardActor.m_portraitFrameMatIdx];
			if (material4 != null && material4.HasProperty("_LightingBlend"))
			{
				material4.SetFloat("_LightingBlend", 1f);
			}
		}
		this.myHeroCardActor.TurnOffCollider();
		this.hisHeroCardActor.TurnOffCollider();
		gameEntity.NotifyOfMulliganInitialized();
		if (GameState.Get().GetGameEntity().DoAlternateMulliganIntro())
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
			{
				this.myHeroCardActor.Hide();
			}
			this.introComplete = true;
			yield break;
		}
		while (this.waitingForVersusText || this.waitingForVersusVo)
		{
			yield return null;
		}
		Log.LoadingScreen.Print("MulliganManager.WaitForHeroesAndStartAnimations() - NotifySceneLoaded()", Array.Empty<object>());
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		GameObject myHero = this.myHeroCardActor.gameObject;
		GameObject hisHero = this.hisHeroCardActor.gameObject;
		this.myHeroCardActor.GetHealthObject().Hide();
		this.hisHeroCardActor.GetHealthObject().Hide();
		if (this.myHeroCardActor.GetAttackObject() != null)
		{
			this.myHeroCardActor.GetAttackObject().Hide();
		}
		if (this.hisHeroCardActor.GetAttackObject() != null)
		{
			this.hisHeroCardActor.GetAttackObject().Hide();
		}
		if (this.versusText)
		{
			this.versusText.transform.position = Board.Get().FindBone("VS_Position").position;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.heroLabelPrefab);
		this.myheroLabel = gameObject.GetComponent<HeroLabel>();
		this.myheroLabel.transform.parent = this.myHeroCardActor.GetMeshRenderer(false).transform;
		this.myheroLabel.transform.localPosition = new Vector3(0f, 0f, 0f);
		TAG_CLASS @class = this.myHeroCardActor.GetEntity().GetClass();
		string classText = "";
		if (@class != TAG_CLASS.NEUTRAL && gameEntity.ShouldShowHeroClassDuringMulligan(Player.Side.FRIENDLY))
		{
			classText = GameStrings.GetClassName(@class).ToUpper();
		}
		this.myheroLabel.UpdateText(this.myHeroCardActor.GetEntity().GetName(), classText);
		gameObject = UnityEngine.Object.Instantiate<GameObject>(this.heroLabelPrefab);
		this.hisheroLabel = gameObject.GetComponent<HeroLabel>();
		this.hisheroLabel.transform.parent = this.hisHeroCardActor.GetMeshRenderer(false).transform;
		this.hisheroLabel.transform.localPosition = new Vector3(0f, 0f, 0f);
		@class = this.hisHeroCardActor.GetEntity().GetClass();
		classText = "";
		if (@class != TAG_CLASS.NEUTRAL && gameEntity.ShouldShowHeroClassDuringMulligan(Player.Side.OPPOSING))
		{
			classText = GameStrings.GetClassName(@class).ToUpper();
		}
		this.hisheroLabel.UpdateText(this.hisHeroCardActor.GetEntity().GetName(), classText);
		if (GameState.Get().WasConcedeRequested())
		{
			yield break;
		}
		gameEntity.StartMulliganSoundtracks(false);
		Animation cardAnim = myHero.GetComponent<Animation>();
		if (cardAnim == null)
		{
			cardAnim = myHero.AddComponent<Animation>();
		}
		cardAnim.AddClip(this.hisheroAnimatesToPosition, "hisHeroAnimateToPosition");
		base.StartCoroutine(this.SampleAnimFrame(cardAnim, "hisHeroAnimateToPosition", 0f));
		Animation oppCardAnim = hisHero.GetComponent<Animation>();
		if (oppCardAnim == null)
		{
			oppCardAnim = hisHero.AddComponent<Animation>();
		}
		oppCardAnim.AddClip(this.myheroAnimatesToPosition, "myHeroAnimateToPosition");
		base.StartCoroutine(this.SampleAnimFrame(oppCardAnim, "myHeroAnimateToPosition", 0f));
		this.m_customIntroCoroutine = base.StartCoroutine(GameState.Get().GetGameEntity().DoCustomIntro(myHeroCard, hisHeroCard, this.myheroLabel, this.hisheroLabel, this.versusText));
		yield return this.m_customIntroCoroutine;
		this.m_customIntroCoroutine = null;
		while (LoadingScreen.Get().IsTransitioning())
		{
			yield return null;
		}
		AudioSource myHeroLine = gameEntity.GetAnnouncerLine(myHeroCard, global::Card.AnnouncerLineType.BEFORE_VERSUS);
		AudioSource hisHeroLine = gameEntity.GetAnnouncerLine(hisHeroCard, global::Card.AnnouncerLineType.AFTER_VERSUS);
		if (this.versusVo && myHeroLine && hisHeroLine)
		{
			SoundManager.Get().Play(myHeroLine, null, null, null);
			while (SoundManager.Get().IsActive(myHeroLine) && !SoundManager.Get().IsPlaybackFinished(myHeroLine))
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.05f);
			SoundManager.Get().PlayPreloaded(this.versusVo);
			while (SoundManager.Get().IsActive(this.versusVo) && !SoundManager.Get().IsPlaybackFinished(this.versusVo))
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.05f);
			if (hisHeroLine != null && hisHeroLine.clip != null)
			{
				SoundManager.Get().Play(hisHeroLine, null, null, null);
				while (SoundManager.Get().IsActive(hisHeroLine))
				{
					if (SoundManager.Get().IsPlaybackFinished(hisHeroLine))
					{
						break;
					}
					yield return null;
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(0.6f);
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().PlayMissionIntroLineAndWait());
		this.myheroLabel.transform.parent = null;
		this.hisheroLabel.transform.parent = null;
		this.myheroLabel.FadeOut();
		this.hisheroLabel.FadeOut();
		yield return new WaitForSeconds(0.5f);
		if (this.m_MyCustomSocketInSpell != null)
		{
			this.m_MyCustomSocketInSpell.m_Location = SpellLocation.NONE;
			this.m_MyCustomSocketInSpell.gameObject.SetActive(true);
			if (this.myHeroCardActor.SocketInParentEffectToHero)
			{
				Vector3 localScale = this.myHeroCardActor.transform.localScale;
				this.myHeroCardActor.transform.localScale = Vector3.one;
				this.m_MyCustomSocketInSpell.transform.parent = this.myHeroCardActor.transform;
				this.m_MyCustomSocketInSpell.transform.localPosition = Vector3.zero;
				this.myHeroCardActor.transform.localScale = localScale;
			}
			this.m_MyCustomSocketInSpell.SetSource(this.myHeroCardActor.GetCard().gameObject);
			this.m_MyCustomSocketInSpell.RemoveAllTargets();
			GameObject myHeroSocketBone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).gameObject;
			this.m_MyCustomSocketInSpell.AddTarget(myHeroSocketBone);
			this.m_MyCustomSocketInSpell.ActivateState(SpellStateType.BIRTH);
			this.m_MyCustomSocketInSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				CS$<>8__locals1.<>4__this.myHeroCardActor.transform.position = myHeroSocketBone.transform.position;
				CS$<>8__locals1.<>4__this.myHeroCardActor.transform.localScale = Vector3.one;
			});
			if (!this.myHeroCardActor.SocketInOverrideHeroAnimation)
			{
				cardAnim.Play("hisHeroAnimateToPosition");
			}
		}
		else
		{
			cardAnim.Play("hisHeroAnimateToPosition");
		}
		if (this.m_HisCustomSocketInSpell != null)
		{
			MulliganManager.<>c__DisplayClass118_2 CS$<>8__locals3 = new MulliganManager.<>c__DisplayClass118_2();
			CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals1;
			if (this.m_MyCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(this.m_HisCustomSocketInSpell, 0f, false);
			}
			this.m_HisCustomSocketInSpell.m_Location = SpellLocation.NONE;
			if (this.hisHeroCardActor.SocketInOverrideHeroAnimation)
			{
				yield return new WaitForSeconds(0.25f);
			}
			this.m_HisCustomSocketInSpell.gameObject.SetActive(true);
			if (this.hisHeroCardActor.SocketInParentEffectToHero)
			{
				Vector3 localScale2 = this.hisHeroCardActor.transform.localScale;
				this.hisHeroCardActor.transform.localScale = Vector3.one;
				this.m_HisCustomSocketInSpell.transform.parent = this.hisHeroCardActor.transform;
				this.m_HisCustomSocketInSpell.transform.localPosition = Vector3.zero;
				this.hisHeroCardActor.transform.localScale = localScale2;
			}
			this.m_HisCustomSocketInSpell.SetSource(this.hisHeroCardActor.GetCard().gameObject);
			this.m_HisCustomSocketInSpell.RemoveAllTargets();
			CS$<>8__locals3.hisHeroSocketBone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).gameObject;
			this.m_HisCustomSocketInSpell.AddTarget(CS$<>8__locals3.hisHeroSocketBone);
			this.m_HisCustomSocketInSpell.ActivateState(SpellStateType.BIRTH);
			this.m_HisCustomSocketInSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				CS$<>8__locals3.CS$<>8__locals2.<>4__this.hisHeroCardActor.transform.position = CS$<>8__locals3.hisHeroSocketBone.transform.position;
				CS$<>8__locals3.CS$<>8__locals2.<>4__this.hisHeroCardActor.transform.localScale = Vector3.one;
			});
			if (!this.hisHeroCardActor.SocketInOverrideHeroAnimation)
			{
				oppCardAnim.Play("myHeroAnimateToPosition");
			}
			CS$<>8__locals3 = null;
		}
		else
		{
			oppCardAnim.Play("myHeroAnimateToPosition");
		}
		SoundManager.Get().LoadAndPlay("FX_MulliganCoin01_HeroCoinDrop.prefab:c46488739eda9f94eb0160290e35f321", this.hisHeroCardActor.GetCard().gameObject);
		if (this.versusText)
		{
			yield return new WaitForSeconds(0.1f);
			this.versusText.FadeOut();
			yield return new WaitForSeconds(0.32f);
		}
		if (this.m_MyCustomSocketInSpell == null)
		{
			this.myWeldEffect = UnityEngine.Object.Instantiate<GameObject>(this.weldPrefab);
			this.myWeldEffect.transform.position = myHero.transform.position;
			if (this.m_HisCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(this.myWeldEffect, 0f, false);
			}
			this.myWeldEffect.GetComponent<HeroWeld>().DoAnim();
		}
		if (this.m_HisCustomSocketInSpell == null)
		{
			this.hisWeldEffect = UnityEngine.Object.Instantiate<GameObject>(this.weldPrefab);
			this.hisWeldEffect.transform.position = hisHero.transform.position;
			if (this.m_MyCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(this.hisWeldEffect, 0f, false);
			}
			this.hisWeldEffect.GetComponent<HeroWeld>().DoAnim();
		}
		yield return new WaitForSeconds(0.05f);
		iTween.ShakePosition(Camera.main.gameObject, iTween.Hash(new object[]
		{
			"time",
			0.6f,
			"amount",
			new Vector3(0.03f, 0.01f, 0.03f)
		}));
		Action<object> action = delegate(object amount)
		{
			if (CS$<>8__locals1.myHeroMat != null)
			{
				CS$<>8__locals1.myHeroMat.SetFloat("_LightingBlend", (float)amount);
			}
			if (CS$<>8__locals1.myHeroFrameMat != null)
			{
				CS$<>8__locals1.myHeroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		action(0f);
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			1f,
			"from",
			0f,
			"to",
			1f,
			"delay",
			2f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"name",
			"MyHeroLightBlend"
		});
		iTween.ValueTo(base.gameObject, args);
		Action<object> action2 = delegate(object amount)
		{
			if (CS$<>8__locals1.hisHeroMat != null)
			{
				CS$<>8__locals1.hisHeroMat.SetFloat("_LightingBlend", (float)amount);
			}
			if (CS$<>8__locals1.hisHeroFrameMat != null)
			{
				CS$<>8__locals1.hisHeroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		action2(0f);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			1f,
			"from",
			0f,
			"to",
			1f,
			"delay",
			2f,
			"onupdate",
			action2,
			"onupdatetarget",
			base.gameObject,
			"name",
			"HisHeroLightBlend"
		});
		iTween.ValueTo(base.gameObject, args2);
		yield return GameState.Get().GetGameEntity().DoGameSpecificPostIntroActions();
		this.introComplete = true;
		GameState.Get().GetGameEntity().NotifyOfHeroesFinishedAnimatingInMulligan();
		ScreenEffectsMgr.Get().SetActive(true);
		yield break;
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x000E99E4 File Offset: 0x000E7BE4
	public void BeginMulligan()
	{
		bool flag = this.mulliganActive;
		this.ForceMulliganActive(true);
		if (GameState.Get().WasConcedeRequested())
		{
			this.HandleGameOverDuringMulligan();
			return;
		}
		if (flag && SpectatorManager.Get().IsSpectatingOpposingSide())
		{
			return;
		}
		this.m_ContinueMulliganWhenBoardLoads = this.ContinueMulliganWhenBoardLoads();
		base.StartCoroutine(this.m_ContinueMulliganWhenBoardLoads);
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x000E9A3B File Offset: 0x000E7C3B
	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		this.HandleGameStart();
	}

	// Token: 0x06002DFA RID: 11770 RVA: 0x000E9A5C File Offset: 0x000E7C5C
	private void HandleGameStart()
	{
		Log.LoadingScreen.Print("MulliganManager.HandleGameStart() - IsPastBeginPhase()={0}", new object[]
		{
			GameState.Get().IsPastBeginPhase()
		});
		bool flag = GameMgr.Get().IsSpectator() && GameState.Get().GetGameEntity().HasTag(GAME_TAG.PUZZLE_MODE);
		if (GameState.Get().IsPastBeginPhase() || flag)
		{
			this.m_SkipMulliganForResume = this.SkipMulliganForResume();
			base.StartCoroutine(this.m_SkipMulliganForResume);
			return;
		}
		this.InitZones();
		this.m_DimLightsOnceBoardLoads = this.DimLightsOnceBoardLoads();
		base.StartCoroutine(this.m_DimLightsOnceBoardLoads);
		if (!GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			this.m_xLabels = new GameObject[4];
			this.coinObject = UnityEngine.Object.Instantiate<GameObject>(this.coinPrefab);
			this.coinObject.SetActive(false);
			if (!Cheats.Get().ShouldSkipMulligan())
			{
				if (Cheats.Get().IsLaunchingQuickGame())
				{
					TimeScaleMgr.Get().SetTimeScaleMultiplier(SceneDebugger.GetDevTimescaleMultiplier());
				}
				this.waitingForVersusVo = true;
				SoundLoader.LoadSound("VO_ANNOUNCER_VERSUS_21.prefab:acc34acb15f07ff4ba08025a57a9a458", new PrefabCallback<GameObject>(this.OnVersusVoLoaded), null, null);
			}
			this.waitingForVersusText = true;
			AssetLoader.Get().InstantiatePrefab("GameStart_VS_Letters.prefab:3cb2cbed6d44a694eb23fb8791684003", new PrefabCallback<GameObject>(this.OnVersusTextLoaded), null, AssetLoadingOptions.None);
			if (this.m_WaitForBoardThenLoadButton != null)
			{
				base.StopCoroutine(this.m_WaitForBoardThenLoadButton);
			}
			this.m_WaitForBoardThenLoadButton = this.WaitForBoardThenLoadButton();
			base.StartCoroutine(this.m_WaitForBoardThenLoadButton);
		}
		else
		{
			this.waitingForVersusVo = true;
			SoundLoader.LoadSound("VO_ANNOUNCER_VERSUS_21.prefab:acc34acb15f07ff4ba08025a57a9a458", new PrefabCallback<GameObject>(this.OnVersusVoLoaded), null, null);
			this.waitingForVersusText = true;
			AssetLoader.Get().InstantiatePrefab("GameStart_VS_Letters.prefab:3cb2cbed6d44a694eb23fb8791684003", new PrefabCallback<GameObject>(this.OnVersusTextLoaded), null, AssetLoadingOptions.None);
		}
		this.m_WaitForHeroesAndStartAnimations = this.WaitForHeroesAndStartAnimations();
		base.StartCoroutine(this.m_WaitForHeroesAndStartAnimations);
		Log.LoadingScreen.Print("MulliganManager.HandleGameStart() - IsMulliganPhase()={0}", new object[]
		{
			GameState.Get().IsMulliganPhase()
		});
		if (GameState.Get().IsMulliganPhase())
		{
			this.m_ResumeMulligan = this.ResumeMulligan();
			base.StartCoroutine(this.m_ResumeMulligan);
		}
	}

	// Token: 0x06002DFB RID: 11771 RVA: 0x000E9C91 File Offset: 0x000E7E91
	private IEnumerator DimLightsOnceBoardLoads()
	{
		while (Board.Get() == null)
		{
			yield return null;
		}
		Board.Get().SetMulliganLighting();
		yield break;
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x000E9C99 File Offset: 0x000E7E99
	private IEnumerator ResumeMulligan()
	{
		this.m_resuming = true;
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			if (player.GetTag<TAG_MULLIGAN>(GAME_TAG.MULLIGAN_STATE) == TAG_MULLIGAN.DONE)
			{
				if (player.IsFriendlySide())
				{
					this.friendlyPlayerHasReplacementCards = true;
				}
				else
				{
					this.opponentPlayerHasReplacementCards = true;
				}
			}
		}
		if (this.friendlyPlayerHasReplacementCards)
		{
			this.SkipCardChoosing();
		}
		else
		{
			while (GameState.Get().GetResponseMode() != GameState.ResponseMode.CHOICE)
			{
				yield return null;
			}
		}
		this.BeginMulligan();
		yield break;
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x000E9CA8 File Offset: 0x000E7EA8
	private void OnMulliganTimerUpdate(TurnTimerUpdate update, object userData)
	{
		if (update.GetSecondsRemaining() <= Mathf.Epsilon)
		{
			GameState.Get().UnregisterMulliganTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnMulliganTimerUpdate));
			this.AutomaticContinueMulligan();
			return;
		}
		if (update.ShouldShow())
		{
			this.BeginMulliganCountdown(update.GetEndTimestamp());
			return;
		}
		this.StopMulliganCountdown();
	}

	// Token: 0x06002DFE RID: 11774 RVA: 0x000E9CFC File Offset: 0x000E7EFC
	private bool OnEntitiesChosenReceived(Network.EntitiesChosen chosen, object userData)
	{
		if (!GameMgr.Get().IsSpectator())
		{
			return false;
		}
		int playerId = chosen.PlayerId;
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		if (playerId == friendlyPlayerId)
		{
			this.m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen = this.Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen(chosen);
			base.StartCoroutine(this.m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen);
			return true;
		}
		return false;
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x000E9D48 File Offset: 0x000E7F48
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		this.HandleGameOverDuringMulligan();
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x000E9D50 File Offset: 0x000E7F50
	private IEnumerator Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		while (!this.m_waitingForUserInput)
		{
			if (GameState.Get().IsGameOver())
			{
				yield break;
			}
			if (this.skipCardChoosing)
			{
				yield break;
			}
			yield return null;
		}
		for (int i = 0; i < this.m_startingCards.Count; i++)
		{
			int entityId = this.m_startingCards[i].GetEntity().GetEntityId();
			bool flag = !chosen.Entities.Contains(entityId);
			if (this.m_handCardsMarkedForReplace[i] != flag)
			{
				this.ToggleHoldState(i, false);
			}
		}
		GameState.Get().OnEntitiesChosenProcessed(chosen);
		this.BeginDealNewCards();
		yield break;
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x000E9D66 File Offset: 0x000E7F66
	private IEnumerator ContinueMulliganWhenBoardLoads()
	{
		while (ZoneMgr.Get() == null)
		{
			yield return null;
		}
		Board board = Board.Get();
		this.startingHandZone = board.FindBone("StartingHandZone").gameObject;
		this.InitZones();
		if (this.m_resuming)
		{
			while (this.ShouldWaitForMulliganCardsToBeProcessed())
			{
				yield return null;
			}
		}
		this.SortHand(this.friendlySideHandZone);
		this.SortHand(this.opposingSideHandZone);
		board.CombinedSurface();
		board.FindCollider("DragPlane").enabled = false;
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			this.m_ShowMultiplayerWaitingArea = this.ShowMultiplayerWaitingArea();
			base.StartCoroutine(this.m_ShowMultiplayerWaitingArea);
		}
		else
		{
			this.m_DealStartingCards = this.DealStartingCards();
			base.StartCoroutine(this.m_DealStartingCards);
		}
		yield break;
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x000E9D78 File Offset: 0x000E7F78
	private void InitZones()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneHand)
			{
				if (zone.m_Side == Player.Side.FRIENDLY)
				{
					this.friendlySideHandZone = (ZoneHand)zone;
				}
				else
				{
					this.opposingSideHandZone = (ZoneHand)zone;
				}
			}
			if (zone is ZoneDeck)
			{
				if (zone.m_Side == Player.Side.FRIENDLY)
				{
					this.friendlySideDeck = (ZoneDeck)zone;
					this.friendlySideDeck.SetSuppressEmotes(true);
					this.friendlySideDeck.UpdateLayout();
				}
				else
				{
					this.opposingSideDeck = (ZoneDeck)zone;
					this.opposingSideDeck.SetSuppressEmotes(true);
					this.opposingSideDeck.UpdateLayout();
				}
			}
		}
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x000E9E54 File Offset: 0x000E8054
	private bool ShouldWaitForMulliganCardsToBeProcessed()
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		bool receivedEndOfMulligan = false;
		powerProcessor.ForEachTaskList(delegate(int index, PowerTaskList taskList)
		{
			if (this.IsTaskListPuttingUsPastMulligan(taskList))
			{
				receivedEndOfMulligan = true;
				return;
			}
		});
		return !receivedEndOfMulligan && powerProcessor.HasTaskLists();
	}

	// Token: 0x06002E04 RID: 11780 RVA: 0x000E9EA4 File Offset: 0x000E80A4
	private bool IsTaskListPuttingUsPastMulligan(PowerTaskList taskList)
	{
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 198 && GameUtils.IsPastBeginPhase((TAG_STEP)histTagChange.Value))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x000E9F28 File Offset: 0x000E8128
	private void GetStartingLists()
	{
		List<global::Card> cards = this.friendlySideHandZone.GetCards();
		List<global::Card> cards2 = this.opposingSideHandZone.GetCards();
		int num;
		if (this.ShouldHandleCoinCard())
		{
			if (this.friendlyPlayerGoesFirst)
			{
				num = cards.Count;
				this.m_bonusCardIndex = cards2.Count - 2;
				this.m_coinCardIndex = cards2.Count - 1;
			}
			else
			{
				num = cards.Count - 1;
				this.m_bonusCardIndex = cards.Count - 2;
			}
		}
		else
		{
			num = cards.Count;
			if (this.friendlyPlayerGoesFirst)
			{
				this.m_bonusCardIndex = cards2.Count - 1;
			}
			else
			{
				this.m_bonusCardIndex = cards.Count - 1;
			}
		}
		this.m_startingCards = new List<global::Card>();
		for (int i = 0; i < num; i++)
		{
			this.m_startingCards.Add(cards[i]);
		}
		this.m_startingOppCards = new List<global::Card>();
		for (int j = 0; j < cards2.Count; j++)
		{
			this.m_startingOppCards.Add(cards2[j]);
		}
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x000EA024 File Offset: 0x000E8224
	private IEnumerator PlayStartingTaunts()
	{
		global::Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		global::Card heroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
		iTween.StopByName(base.gameObject, "HisHeroLightBlend");
		if (heroPowerCard != null)
		{
			while (!heroPowerCard.GetActor().IsShown())
			{
				yield return null;
			}
			GameState.Get().GetGameEntity().FadeInActor(heroPowerCard.GetActor(), 0.4f);
		}
		while (!heroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(heroCard.GetActor());
		EmoteEntry emoteEntry = heroCard.GetEmoteEntry(EmoteType.START);
		bool flag = true;
		if (emoteEntry != null)
		{
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell(true);
			if (soundSpell != null && soundSpell.DetermineBestAudioSource() == null)
			{
				flag = false;
			}
		}
		CardSoundSpell emoteSpell = null;
		if (flag)
		{
			emoteSpell = heroCard.PlayEmote(EmoteType.START);
		}
		if (emoteSpell != null)
		{
			while (emoteSpell.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(MulliganManager.DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(heroCard.GetActor());
		if (heroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(heroPowerCard.GetActor());
		}
		global::Card myHeroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		global::Card myHeroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (MulliganManager.Get() == null)
		{
			yield break;
		}
		iTween.StopByName(base.gameObject, "MyHeroLightBlend");
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeInActor(myHeroPowerCard.GetActor(), 0.4f);
		}
		EmoteType emoteToPlay = EmoteType.START;
		EmoteEntry emoteEntry2 = myHeroCard.GetEmoteEntry(EmoteType.START);
		if (emoteEntry2 != null && !string.IsNullOrEmpty(emoteEntry2.GetGameStringKey()))
		{
			EmoteEntry emoteEntry3 = heroCard.GetEmoteEntry(EmoteType.START);
			if (emoteEntry3 != null && emoteEntry2.GetGameStringKey() == emoteEntry3.GetGameStringKey())
			{
				emoteToPlay = EmoteType.MIRROR_START;
			}
		}
		while (!myHeroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(myHeroCard.GetActor());
		emoteSpell = myHeroCard.PlayEmote(emoteToPlay, Notification.SpeechBubbleDirection.BottomRight);
		if (emoteSpell != null)
		{
			while (emoteSpell.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(MulliganManager.DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(myHeroCard.GetActor());
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(myHeroPowerCard.GetActor());
		}
		yield break;
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x000EA033 File Offset: 0x000E8233
	private IEnumerator ShowMultiplayerWaitingArea()
	{
		yield return new WaitForSeconds(1f);
		while (!this.introComplete)
		{
			yield return null;
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterIntroBeforeMulligan());
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DO_OPENING_TAUNTS) && !Cheats.Get().ShouldSkipMulligan())
		{
			this.m_PlayStartingTaunts = this.PlayStartingTaunts();
			base.StartCoroutine(this.m_PlayStartingTaunts);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.friendlyPlayerGoesFirst = friendlySidePlayer.HasTag(GAME_TAG.FIRST_PLAYER);
		this.GetStartingLists();
		bool isMulliganOver = false;
		bool shouldSendTelemetry = true;
		if (this.m_startingCards.Count == 0)
		{
			while (GameState.Get().GetFriendlySidePlayer().GetHeroCard() == null)
			{
				if (shouldSendTelemetry)
				{
					TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", "No hero card set for friendly side player");
					shouldSendTelemetry = false;
				}
				yield return null;
			}
			this.m_startingCards.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
			isMulliganOver = true;
		}
		shouldSendTelemetry = false;
		foreach (global::Card card2 in this.m_startingCards)
		{
			if (card2 != null && card2.GetActor() != null)
			{
				card2.GetActor().SetActorState(ActorStateType.CARD_IDLE);
				card2.GetActor().TurnOffCollider();
				card2.GetActor().GetMeshRenderer(false).gameObject.layer = 8;
				if (card2.GetActor().m_nameTextMesh != null)
				{
					card2.GetActor().m_nameTextMesh.UpdateNow(false);
				}
			}
			else if (card2 == null)
			{
				shouldSendTelemetry = true;
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				this.pendingHeroCount++;
				if (card2 != null && card2.GetActor() != null)
				{
					card2.GetActor().gameObject.SetActive(false);
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), new PrefabCallback<GameObject>(this.OnHeroActorLoaded), card2, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
		if (shouldSendTelemetry)
		{
			string text = "ShowMultiplayerWaitingArea - Found a null card within starting hero cards during initialization. Starting Cards: ";
			for (int j = 0; j < this.m_startingCards.Count; j++)
			{
				text += ((this.m_startingCards[j] == null) ? "NULL" : this.m_startingCards[j].GetEntity().GetName());
				text += ((j == this.m_startingCards.Count - 1) ? "." : ", ");
			}
			TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", text);
			Log.MulliganManager.PrintWarning(text, Array.Empty<object>());
			shouldSendTelemetry = false;
		}
		while (this.pendingHeroCount > 0)
		{
			yield return null;
		}
		float zoneWidth = this.startingHandZone.GetComponent<Collider>().bounds.size.x;
		if (UniversalInputManager.UsePhoneUI)
		{
			zoneWidth *= 0.55f;
		}
		int numFakeCardsOnLeft = GameState.Get().GetGameEntity().GetNumberOfFakeMulliganCardsToShowOnLeft(this.m_startingCards.Count);
		int numFakeCardsOnRight = GameState.Get().GetGameEntity().GetNumberOfFakeMulliganCardsToShowOnRight(this.m_startingCards.Count);
		if (!isMulliganOver)
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				this.pendingFakeHeroCount = numFakeCardsOnLeft + numFakeCardsOnRight;
				for (int k = 0; k < numFakeCardsOnLeft; k++)
				{
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), new PrefabCallback<GameObject>(this.OnFakeHeroActorLoaded), this.fakeCardsOnLeft, AssetLoadingOptions.IgnorePrefabPosition);
				}
				for (int l = 0; l < numFakeCardsOnRight; l++)
				{
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), new PrefabCallback<GameObject>(this.OnFakeHeroActorLoaded), this.fakeCardsOnRight, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
			while (this.pendingFakeHeroCount > 0)
			{
				yield return null;
			}
		}
		else
		{
			numFakeCardsOnLeft = 0;
			numFakeCardsOnRight = 0;
		}
		float spaceForEachCard = zoneWidth / (float)Mathf.Max(this.m_startingCards.Count + numFakeCardsOnLeft + numFakeCardsOnRight, 1);
		float spacingToUse = spaceForEachCard;
		float leftSideOfZone = this.startingHandZone.transform.position.x - zoneWidth / 2f;
		float rightSideOfZone = this.startingHandZone.transform.position.x + zoneWidth / 2f;
		float timingBonus = 0.1f;
		int numCardsToDealExcludingBonusCard = this.m_startingCards.Count;
		this.opposingSideHandZone.SetDoNotUpdateLayout(false);
		this.opposingSideHandZone.UpdateLayout(null, true, 3);
		float cardHeightOffset = 0f;
		if (UniversalInputManager.UsePhoneUI)
		{
			cardHeightOffset = 7f;
		}
		float cardZpos = this.startingHandZone.transform.position.z - 0.3f;
		if (UniversalInputManager.UsePhoneUI)
		{
			cardZpos = this.startingHandZone.transform.position.z - 0.2f;
		}
		float xOffset = spacingToUse / 2f;
		foreach (Actor actor in this.fakeCardsOnLeft)
		{
			if (actor != null)
			{
				GameObject card = actor.gameObject;
				iTween.Stop(card);
				iTween.MoveTo(card, iTween.Hash(new object[]
				{
					"path",
					new Vector3[]
					{
						card.transform.position,
						new Vector3(card.transform.position.x, card.transform.position.y + 3.6f, card.transform.position.z),
						new Vector3(leftSideOfZone + xOffset, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
					},
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"easetype",
					iTween.EaseType.easeInSineOutExpo
				}));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card, iTween.Hash(new object[]
				{
					"rotation",
					new Vector3(0f, 0f, 0f),
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"delay",
					MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
				}));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card);
				xOffset += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
				card = null;
			}
		}
		List<Actor>.Enumerator enumerator2 = default(List<Actor>.Enumerator);
		int num;
		for (int i = 0; i < numCardsToDealExcludingBonusCard; i = num + 1)
		{
			if (!(this.m_startingCards[i] == null))
			{
				GameObject card = this.m_startingCards[i].gameObject;
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && this.choiceHeroActors.ContainsKey(this.m_startingCards[i]))
				{
					card = this.choiceHeroActors[this.m_startingCards[i]].transform.parent.gameObject;
				}
				iTween.Stop(card);
				iTween.MoveTo(card, iTween.Hash(new object[]
				{
					"path",
					new Vector3[]
					{
						card.transform.position,
						new Vector3(card.transform.position.x, card.transform.position.y + 3.6f, card.transform.position.z),
						new Vector3(leftSideOfZone + xOffset, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
					},
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"easetype",
					iTween.EaseType.easeInSineOutExpo
				}));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card, iTween.Hash(new object[]
				{
					"rotation",
					new Vector3(0f, 0f, 0f),
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"delay",
					MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
				}));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card);
				xOffset += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
				card = null;
			}
			num = i;
		}
		foreach (Actor actor2 in this.fakeCardsOnRight)
		{
			if (actor2 != null)
			{
				GameObject card = actor2.gameObject;
				iTween.Stop(card);
				iTween.MoveTo(card, iTween.Hash(new object[]
				{
					"path",
					new Vector3[]
					{
						card.transform.position,
						new Vector3(card.transform.position.x, card.transform.position.y + 3.6f, card.transform.position.z),
						new Vector3(leftSideOfZone + xOffset, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
					},
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"easetype",
					iTween.EaseType.easeInSineOutExpo
				}));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card, iTween.Hash(new object[]
				{
					"rotation",
					new Vector3(0f, 0f, 0f),
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"delay",
					MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
				}));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card);
				xOffset += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
				card = null;
			}
		}
		enumerator2 = default(List<Actor>.Enumerator);
		if (this.skipCardChoosing)
		{
			this.mulliganChooseBanner = UnityEngine.Object.Instantiate<GameObject>(this.mulliganChooseBannerPrefab);
			this.SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"));
			Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
			this.mulliganChooseBanner.transform.position = position;
			Vector3 localScale = this.mulliganChooseBanner.transform.localScale;
			this.mulliganChooseBanner.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			iTween.ScaleTo(this.mulliganChooseBanner, localScale, 0.5f);
			this.m_ShrinkStartingHandBanner = this.ShrinkStartingHandBanner(this.mulliganChooseBanner);
			base.StartCoroutine(this.m_ShrinkStartingHandBanner);
			this.ShowMulliganDetail();
		}
		yield return new WaitForSeconds(1.1f);
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		if (this.friendlyPlayerGoesFirst)
		{
			xOffset = 0f;
			for (int m = this.m_startingCards.Count - 1; m >= 0; m--)
			{
				if (this.m_startingCards[m] != null)
				{
					GameObject gameObject = this.m_startingCards[m].gameObject;
					if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && this.choiceHeroActors.ContainsKey(this.m_startingCards[m]))
					{
						gameObject = this.choiceHeroActors[this.m_startingCards[m]].gameObject;
					}
					iTween.Stop(gameObject);
					iTween.MoveTo(gameObject, iTween.Hash(new object[]
					{
						"position",
						new Vector3(rightSideOfZone - spaceForEachCard - xOffset + spaceForEachCard / 2f, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos),
						"time",
						0.93333334f,
						"easetype",
						iTween.EaseType.easeInOutCubic
					}));
					xOffset += spaceForEachCard;
				}
			}
		}
		GameState.Get().GetGameEntity().OnMulliganCardsDealt(this.m_startingCards);
		yield return new WaitForSeconds(0.6f);
		if (this.skipCardChoosing)
		{
			if (GameState.Get().IsMulliganPhase() || GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
			{
				if (GameState.Get().IsFriendlySidePlayerTurn())
				{
					TurnStartManager.Get().BeginListeningForTurnEvents(false);
				}
				this.m_WaitForOpponentToFinishMulligan = this.WaitForOpponentToFinishMulligan();
				base.StartCoroutine(this.m_WaitForOpponentToFinishMulligan);
			}
			else
			{
				yield return new WaitForSeconds(2f);
				this.EndMulligan();
			}
			yield break;
		}
		foreach (global::Card card3 in this.m_startingCards)
		{
			if (card3 != null)
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && this.choiceHeroActors.ContainsKey(card3))
				{
					this.choiceHeroActors[card3].TurnOnCollider();
				}
				else
				{
					card3.GetActor().TurnOnCollider();
				}
			}
			else
			{
				shouldSendTelemetry = true;
			}
		}
		if (shouldSendTelemetry)
		{
			string text2 = "ShowMultiplayerWaitingArea - Found a null card in starting cards while enabling colliders. Starting cards: ";
			for (int n = 0; n < this.m_startingCards.Count; n++)
			{
				text2 += ((this.m_startingCards[n] == null) ? "NULL" : this.m_startingCards[n].GetEntity().GetName());
				text2 += ((n == this.m_startingCards.Count - 1) ? "." : ", ");
			}
			TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", text2);
			Log.MulliganManager.PrintWarning(text2, Array.Empty<object>());
			shouldSendTelemetry = false;
		}
		string mulliganBannerText = GameState.Get().GetGameEntity().GetMulliganBannerText();
		string mulliganBannerSubtitleText = GameState.Get().GetGameEntity().GetMulliganBannerSubtitleText();
		this.mulliganChooseBanner = UnityEngine.Object.Instantiate<GameObject>(this.mulliganChooseBannerPrefab, Board.Get().FindBone("ChoiceBanner").position, new Quaternion(0f, 0f, 0f, 0f));
		this.SetMulliganBannerText(mulliganBannerText, mulliganBannerSubtitleText);
		this.ShowMulliganDetail();
		if (GameState.Get().IsInChoiceMode() && GameMgr.Get().IsSpectator())
		{
			this.m_replaceLabels = new List<MulliganReplaceLabel>();
			for (int num2 = 0; num2 < this.m_startingCards.Count; num2++)
			{
				if (this.m_startingCards[num2] != null)
				{
					InputManager.Get().DoNetworkResponse(this.m_startingCards[num2].GetEntity(), true);
				}
				this.m_replaceLabels.Add(null);
			}
		}
		while (this.mulliganButton == null && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			this.mulliganButton.transform.position = new Vector3(this.startingHandZone.transform.position.x, this.friendlySideHandZone.transform.position.y, this.myHeroCardActor.transform.position.z);
			this.mulliganButton.transform.localEulerAngles = new Vector3(90f, 90f, 90f);
			this.mulliganButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMulliganButtonReleased));
			this.mulliganButtonWidget.transform.position = new Vector3(this.startingHandZone.transform.position.x, this.friendlySideHandZone.transform.position.y, this.myHeroCardActor.transform.position.z);
			this.mulliganButtonWidget.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMulliganButtonReleased));
			this.m_WaitAFrameBeforeSendingEventToMulliganButton = this.WaitAFrameBeforeSendingEventToMulliganButton();
			base.StartCoroutine(this.m_WaitAFrameBeforeSendingEventToMulliganButton);
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_MULLIGAN, false) && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.DealStartingCards:" + Option.HAS_SEEN_MULLIGAN))
			{
				this.innkeeperMulliganDialog = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_MULLIGAN_13"), "VO_INNKEEPER_MULLIGAN_13.prefab:3ec6b2e741ac16d4ca519bdfd26d10e3", 0f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_MULLIGAN, true);
				this.mulliganButton.GetComponent<Collider>().enabled = false;
			}
		}
		GameState.Get().GetGameEntity().StartMulliganSoundtracks(true);
		this.m_waitingForUserInput = true;
		while (this.innkeeperMulliganDialog != null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			this.mulliganButton.GetComponent<Collider>().enabled = true;
		}
		if (this.skipCardChoosing || Cheats.Get().ShouldSkipMulligan())
		{
			this.BeginDealNewCards();
		}
		yield break;
		yield break;
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x000EA042 File Offset: 0x000E8242
	private IEnumerator DealStartingCards()
	{
		yield return new WaitForSeconds(1f);
		while (!this.introComplete)
		{
			yield return null;
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterIntroBeforeMulligan());
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DO_OPENING_TAUNTS) && !Cheats.Get().ShouldSkipMulligan())
		{
			this.m_PlayStartingTaunts = this.PlayStartingTaunts();
			base.StartCoroutine(this.m_PlayStartingTaunts);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.friendlyPlayerGoesFirst = friendlySidePlayer.HasTag(GAME_TAG.FIRST_PLAYER);
		this.GetStartingLists();
		if (this.m_startingCards.Count == 0)
		{
			this.SkipCardChoosing();
		}
		foreach (global::Card card in this.m_startingCards)
		{
			card.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			card.GetActor().TurnOffCollider();
			card.GetActor().GetMeshRenderer(false).gameObject.layer = 8;
			card.GetActor().m_nameTextMesh.UpdateNow(false);
		}
		float num = this.startingHandZone.GetComponent<Collider>().bounds.size.x;
		if (UniversalInputManager.UsePhoneUI)
		{
			num *= 0.55f;
		}
		float spaceForEachCard = num / (float)this.m_startingCards.Count;
		float num2 = num / (float)(this.m_startingCards.Count + 1);
		float spacingToUse = num2;
		float leftSideOfZone = this.startingHandZone.transform.position.x - num / 2f;
		float rightSideOfZone = this.startingHandZone.transform.position.x + num / 2f;
		float timingBonus = 0.1f;
		int numCardsToDealExcludingBonusCard = this.m_startingCards.Count;
		if (!this.friendlyPlayerGoesFirst)
		{
			numCardsToDealExcludingBonusCard = this.m_bonusCardIndex;
			spacingToUse = spaceForEachCard;
		}
		else if (this.m_startingOppCards.Count > 0)
		{
			this.m_startingOppCards[this.m_bonusCardIndex].SetDoNotSort(true);
			if (this.m_coinCardIndex >= 0)
			{
				this.m_startingOppCards[this.m_coinCardIndex].SetDoNotSort(true);
			}
		}
		this.opposingSideHandZone.SetDoNotUpdateLayout(false);
		this.opposingSideHandZone.UpdateLayout(null, true, 3);
		float cardHeightOffset = 0f;
		if (UniversalInputManager.UsePhoneUI)
		{
			cardHeightOffset = 7f;
		}
		float cardZpos = this.startingHandZone.transform.position.z - 0.3f;
		if (UniversalInputManager.UsePhoneUI)
		{
			cardZpos = this.startingHandZone.transform.position.z - 0.2f;
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeDealingBaseMulliganCards());
		float xOffset = spacingToUse / 2f;
		int num3;
		for (int i = 0; i < numCardsToDealExcludingBonusCard; i = num3 + 1)
		{
			GameObject topCard = this.m_startingCards[i].gameObject;
			iTween.Stop(topCard);
			iTween.MoveTo(topCard, iTween.Hash(new object[]
			{
				"path",
				new Vector3[]
				{
					topCard.transform.position,
					new Vector3(topCard.transform.position.x, topCard.transform.position.y + 3.6f, topCard.transform.position.z),
					new Vector3(leftSideOfZone + xOffset, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
				},
				"time",
				MulliganManager.ANIMATION_TIME_DEAL_CARD,
				"easetype",
				iTween.EaseType.easeInSineOutExpo
			}));
			iTween.ScaleTo(topCard, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
			iTween.RotateTo(topCard, iTween.Hash(new object[]
			{
				"rotation",
				new Vector3(0f, 0f, 0f),
				"time",
				MulliganManager.ANIMATION_TIME_DEAL_CARD,
				"delay",
				MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
			}));
			yield return new WaitForSeconds(0.04f);
			SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", topCard);
			xOffset += spacingToUse;
			yield return new WaitForSeconds(0.05f + timingBonus);
			timingBonus = 0f;
			topCard = null;
			num3 = i;
		}
		if (this.skipCardChoosing)
		{
			this.mulliganChooseBanner = UnityEngine.Object.Instantiate<GameObject>(this.mulliganChooseBannerPrefab);
			this.SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"));
			Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
			this.mulliganChooseBanner.transform.position = position;
			Vector3 localScale = this.mulliganChooseBanner.transform.localScale;
			this.mulliganChooseBanner.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			iTween.ScaleTo(this.mulliganChooseBanner, localScale, 0.5f);
			this.m_ShrinkStartingHandBanner = this.ShrinkStartingHandBanner(this.mulliganChooseBanner);
			base.StartCoroutine(this.m_ShrinkStartingHandBanner);
		}
		yield return new WaitForSeconds(1.1f);
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterDealingBaseMulliganCards());
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeCoinFlip());
		if (this.coinObject != null)
		{
			Transform transform = Board.Get().FindBone("MulliganCoinPosition");
			this.coinObject.transform.position = transform.position;
			this.coinObject.transform.localEulerAngles = transform.localEulerAngles;
			this.coinObject.SetActive(true);
			this.coinObject.GetComponent<CoinEffect>().DoAnim(this.friendlyPlayerGoesFirst);
			SoundManager.Get().LoadAndPlay("FX_MulliganCoin03_CoinFlip.prefab:07015cb3f02713a45aa03fc3aa798778", this.coinObject);
			this.coinLocation = transform.position;
			AssetLoader.Get().InstantiatePrefab("MulliganResultText.prefab:0369b435afd2e344db21e58648f8636c", new PrefabCallback<GameObject>(this.CoinTossTextCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
			yield return new WaitForSeconds(2f);
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterCoinFlip());
		if (!this.friendlyPlayerGoesFirst)
		{
			GameObject topCard = this.m_startingCards[this.m_bonusCardIndex].gameObject;
			iTween.MoveTo(topCard, iTween.Hash(new object[]
			{
				"path",
				new Vector3[]
				{
					topCard.transform.position,
					new Vector3(topCard.transform.position.x, topCard.transform.position.y + 3.6f, topCard.transform.position.z),
					new Vector3(leftSideOfZone + xOffset, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
				},
				"time",
				MulliganManager.ANIMATION_TIME_DEAL_CARD,
				"easetype",
				iTween.EaseType.easeInSineOutExpo
			}));
			iTween.ScaleTo(topCard, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
			iTween.RotateTo(topCard, iTween.Hash(new object[]
			{
				"rotation",
				new Vector3(0f, 0f, 0f),
				"time",
				MulliganManager.ANIMATION_TIME_DEAL_CARD,
				"delay",
				MulliganManager.ANIMATION_TIME_DEAL_CARD / 8f
			}));
			yield return new WaitForSeconds(0.04f);
			SoundManager.Get().LoadAndPlay("FX_GameStart20_CardDealSingle.prefab:0da693603ca05d846b9cfe26e9f0e3c7", topCard);
			topCard = null;
		}
		else if (this.m_startingOppCards.Count > 0)
		{
			this.m_startingOppCards[this.m_bonusCardIndex].SetDoNotSort(false);
			this.opposingSideHandZone.UpdateLayout(null, true, 4);
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterDealingBonusCard());
		yield return new WaitForSeconds(1.75f);
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeSpreadingMulliganCards());
		if (this.friendlyPlayerGoesFirst)
		{
			xOffset = 0f;
			for (int j = this.m_startingCards.Count - 1; j >= 0; j--)
			{
				GameObject gameObject = this.m_startingCards[j].gameObject;
				iTween.Stop(gameObject);
				iTween.MoveTo(gameObject, iTween.Hash(new object[]
				{
					"position",
					new Vector3(rightSideOfZone - spaceForEachCard - xOffset + spaceForEachCard / 2f, this.friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos),
					"time",
					0.93333334f,
					"easetype",
					iTween.EaseType.easeInOutCubic
				}));
				xOffset += spaceForEachCard;
			}
		}
		GameState.Get().GetGameEntity().OnMulliganCardsDealt(this.m_startingCards);
		yield return new WaitForSeconds(0.6f);
		yield return base.StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterSpreadingMulliganCards());
		if (this.skipCardChoosing)
		{
			if (GameState.Get().IsMulliganPhase())
			{
				if (GameState.Get().IsFriendlySidePlayerTurn())
				{
					TurnStartManager.Get().BeginListeningForTurnEvents(false);
				}
				this.m_WaitForOpponentToFinishMulligan = this.WaitForOpponentToFinishMulligan();
				base.StartCoroutine(this.m_WaitForOpponentToFinishMulligan);
			}
			else
			{
				yield return new WaitForSeconds(2f);
				this.EndMulligan();
			}
			yield break;
		}
		foreach (global::Card card2 in this.m_startingCards)
		{
			card2.GetActor().TurnOnCollider();
		}
		this.mulliganChooseBanner = UnityEngine.Object.Instantiate<GameObject>(this.mulliganChooseBannerPrefab, Board.Get().FindBone("ChoiceBanner").position, new Quaternion(0f, 0f, 0f, 0f));
		this.SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"), GameStrings.Get("GAMEPLAY_MULLIGAN_SUBTITLE"));
		if (GameState.Get().IsInChoiceMode())
		{
			this.m_replaceLabels = new List<MulliganReplaceLabel>();
			for (int k = 0; k < this.m_startingCards.Count; k++)
			{
				InputManager.Get().DoNetworkResponse(this.m_startingCards[k].GetEntity(), true);
				this.m_replaceLabels.Add(null);
			}
		}
		while (this.mulliganButton == null && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			this.mulliganButton.transform.position = new Vector3(this.startingHandZone.transform.position.x, this.friendlySideHandZone.transform.position.y, this.myHeroCardActor.transform.position.z);
			this.mulliganButton.transform.localEulerAngles = new Vector3(90f, 90f, 90f);
			this.mulliganButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMulliganButtonReleased));
			this.mulliganButtonWidget.transform.position = new Vector3(this.startingHandZone.transform.position.x, this.friendlySideHandZone.transform.position.y, this.myHeroCardActor.transform.position.z);
			this.mulliganButtonWidget.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMulliganButtonReleased));
			this.m_WaitAFrameBeforeSendingEventToMulliganButton = this.WaitAFrameBeforeSendingEventToMulliganButton();
			base.StartCoroutine(this.m_WaitAFrameBeforeSendingEventToMulliganButton);
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_MULLIGAN, false) && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.DealStartingCards:" + Option.HAS_SEEN_MULLIGAN))
			{
				this.innkeeperMulliganDialog = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_MULLIGAN_13"), "VO_INNKEEPER_MULLIGAN_13.prefab:3ec6b2e741ac16d4ca519bdfd26d10e3", 0f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_MULLIGAN, true);
				this.mulliganButton.GetComponent<Collider>().enabled = false;
			}
		}
		GameState.Get().GetGameEntity().StartMulliganSoundtracks(true);
		this.m_waitingForUserInput = true;
		while (this.innkeeperMulliganDialog != null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			this.mulliganButton.GetComponent<Collider>().enabled = true;
		}
		if (this.skipCardChoosing || Cheats.Get().ShouldSkipMulligan())
		{
			this.BeginDealNewCards();
		}
		yield break;
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x000EA051 File Offset: 0x000E8251
	private IEnumerator WaitAFrameBeforeSendingEventToMulliganButton()
	{
		yield return null;
		this.mulliganButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		yield break;
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x000EA060 File Offset: 0x000E8260
	public bool IsMulliganTimerActive()
	{
		return this.m_mulliganTimer != null;
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x000EA070 File Offset: 0x000E8270
	private void BeginMulliganCountdown(float endTimeStamp)
	{
		if (!this.m_waitingForUserInput && !GameState.Get().GetBooleanGameOption(GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER))
		{
			return;
		}
		if (this.m_mulliganTimer == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mulliganTimerPrefab);
			this.m_mulliganTimer = gameObject.GetComponent<MulliganTimer>();
			if (this.m_mulliganTimer == null)
			{
				UnityEngine.Object.Destroy(gameObject);
				return;
			}
		}
		this.m_mulliganTimer.SetEndTime(endTimeStamp);
	}

	// Token: 0x06002E0C RID: 11788 RVA: 0x000EA0DA File Offset: 0x000E82DA
	private void StopMulliganCountdown()
	{
		this.DestroyMulliganTimer();
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x000EA0E2 File Offset: 0x000E82E2
	public GameObject GetMulliganBanner()
	{
		return this.mulliganChooseBanner;
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x000EA0EA File Offset: 0x000E82EA
	public GameObject GetMulliganButton()
	{
		if (this.mulliganButton != null)
		{
			return this.mulliganButton.gameObject;
		}
		return null;
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x000EA107 File Offset: 0x000E8307
	public Vector3 GetMulliganTimerPosition()
	{
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION))
		{
			return GameState.Get().GetGameEntity().GetMulliganTimerAlternatePosition();
		}
		return this.mulliganButton.transform.position;
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x000EA138 File Offset: 0x000E8338
	private void CoinTossTextCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.coinTossText = go;
		RenderUtils.SetAlpha(go, 1f);
		go.transform.position = this.coinLocation + new Vector3(0f, 0f, -1f);
		go.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		UberText componentInChildren = go.transform.GetComponentInChildren<UberText>();
		string text;
		if (this.friendlyPlayerGoesFirst)
		{
			text = GameStrings.Get("GAMEPLAY_COIN_TOSS_WON");
		}
		else
		{
			text = GameStrings.Get("GAMEPLAY_COIN_TOSS_LOST");
		}
		componentInChildren.Text = text;
		GameState.Get().GetGameEntity().NotifyOfCoinFlipResult();
		this.m_AnimateCoinTossText = this.AnimateCoinTossText();
		base.StartCoroutine(this.m_AnimateCoinTossText);
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x000EA1F9 File Offset: 0x000E83F9
	private IEnumerator AnimateCoinTossText()
	{
		yield return new WaitForSeconds(1.8f);
		if (this.coinTossText == null)
		{
			yield break;
		}
		iTween.FadeTo(this.coinTossText, 1f, 0.25f);
		iTween.MoveTo(this.coinTossText, this.coinTossText.transform.position + new Vector3(0f, 0.5f, 0f), 2f);
		yield return new WaitForSeconds(1.9f);
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		if (this.coinTossText == null)
		{
			yield break;
		}
		iTween.FadeTo(this.coinTossText, 0f, 1f);
		yield return new WaitForSeconds(0.1f);
		UnityEngine.Object.Destroy(this.coinTossText);
		yield break;
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x000EA208 File Offset: 0x000E8408
	private MulliganReplaceLabel CreateNewUILabelAtCardPosition(MulliganReplaceLabel prefab, int cardPosition)
	{
		MulliganReplaceLabel mulliganReplaceLabel = UnityEngine.Object.Instantiate<MulliganReplaceLabel>(prefab);
		if (UniversalInputManager.UsePhoneUI)
		{
			mulliganReplaceLabel.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			mulliganReplaceLabel.transform.position = new Vector3(this.m_startingCards[cardPosition].transform.position.x, this.m_startingCards[cardPosition].transform.position.y + 0.3f, this.m_startingCards[cardPosition].transform.position.z - 1.1f);
		}
		else
		{
			mulliganReplaceLabel.transform.position = new Vector3(this.m_startingCards[cardPosition].transform.position.x, this.m_startingCards[cardPosition].transform.position.y + 0.3f, this.m_startingCards[cardPosition].transform.position.z - this.startingHandZone.GetComponent<Collider>().bounds.size.z / 2.6f);
		}
		return mulliganReplaceLabel;
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x000EA348 File Offset: 0x000E8548
	public void SetAllMulliganCardsToHold()
	{
		foreach (global::Card card in this.friendlySideHandZone.GetCards())
		{
			InputManager.Get().DoNetworkResponse(card.GetEntity(), true);
		}
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x000EA3AC File Offset: 0x000E85AC
	private void ToggleHoldState(int startingCardsIndex, bool forceDisable = false)
	{
		if (!GameState.Get().IsInChoiceMode())
		{
			return;
		}
		if (startingCardsIndex >= this.m_startingCards.Count)
		{
			return;
		}
		if ((!forceDisable || (forceDisable && this.m_handCardsMarkedForReplace[startingCardsIndex])) && !InputManager.Get().DoNetworkResponse(this.m_startingCards[startingCardsIndex].GetEntity(), true))
		{
			return;
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			if (forceDisable)
			{
				this.m_handCardsMarkedForReplace[startingCardsIndex] = false;
			}
			else
			{
				this.m_handCardsMarkedForReplace[startingCardsIndex] = !this.m_handCardsMarkedForReplace[startingCardsIndex];
			}
			if (!this.m_handCardsMarkedForReplace[startingCardsIndex])
			{
				SoundManager.Get().LoadAndPlay("GM_ChatWarning.prefab:41baa28576a71664eabd8712a198b67f");
				if (this.m_xLabels != null && this.m_xLabels[startingCardsIndex] != null)
				{
					UnityEngine.Object.Destroy(this.m_xLabels[startingCardsIndex]);
				}
				UnityEngine.Object.Destroy(this.m_replaceLabels[startingCardsIndex].gameObject);
			}
			else
			{
				SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
				if (this.m_xLabels != null && this.m_xLabels[startingCardsIndex] != null)
				{
					UnityEngine.Object.Destroy(this.m_xLabels[startingCardsIndex]);
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mulliganXlabelPrefab);
				gameObject.transform.position = this.m_startingCards[startingCardsIndex].transform.position;
				gameObject.transform.rotation = this.m_startingCards[startingCardsIndex].transform.rotation;
				if (this.m_xLabels != null)
				{
					this.m_xLabels[startingCardsIndex] = gameObject;
				}
				if (this.m_replaceLabels != null)
				{
					this.m_replaceLabels[startingCardsIndex] = this.CreateNewUILabelAtCardPosition(this.mulliganReplaceLabelPrefab, startingCardsIndex);
				}
			}
		}
		else
		{
			if (forceDisable)
			{
				this.m_handCardsMarkedForReplace[startingCardsIndex] = false;
			}
			else
			{
				this.m_handCardsMarkedForReplace[startingCardsIndex] = !this.m_handCardsMarkedForReplace[startingCardsIndex];
			}
			if (!this.m_handCardsMarkedForReplace[startingCardsIndex])
			{
				SoundManager.Get().LoadAndPlay("GM_ChatWarning.prefab:41baa28576a71664eabd8712a198b67f");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(this.m_startingCards[startingCardsIndex], this.m_handCardsMarkedForReplace[startingCardsIndex]);
			}
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			this.BeginDealNewCards();
		}
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x000EA5EC File Offset: 0x000E87EC
	private void DestroyXobjects()
	{
		if (this.m_xLabels == null)
		{
			return;
		}
		for (int i = 0; i < this.m_xLabels.Length; i++)
		{
			UnityEngine.Object.Destroy(this.m_xLabels[i]);
		}
		this.m_xLabels = null;
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x000EA629 File Offset: 0x000E8829
	private void DestroyChooseBanner()
	{
		if (this.mulliganChooseBanner == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.mulliganChooseBanner);
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x000EA645 File Offset: 0x000E8845
	private void DestroyDetailLabel()
	{
		if (this.mulliganDetailLabel != null)
		{
			UnityEngine.Object.Destroy(this.mulliganDetailLabel);
			this.mulliganDetailLabel = null;
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x000EA667 File Offset: 0x000E8867
	private void DestroyMulliganTimer()
	{
		if (this.m_mulliganTimer == null)
		{
			return;
		}
		this.m_mulliganTimer.SelfDestruct();
		this.m_mulliganTimer = null;
	}

	// Token: 0x06002E19 RID: 11801 RVA: 0x000EA68C File Offset: 0x000E888C
	public void ToggleHoldState(Actor toggleActor)
	{
		bool flag = false;
		GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE);
		List<Actor> list = new List<Actor>(this.fakeCardsOnLeft.Count + this.fakeCardsOnRight.Count);
		list.AddRange(this.fakeCardsOnLeft);
		list.AddRange(this.fakeCardsOnRight);
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
		{
			foreach (Actor actor in list)
			{
				if (toggleActor == actor)
				{
					flag = GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(actor, null);
				}
				else
				{
					GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(actor, new bool?(false));
				}
			}
		}
		if (flag)
		{
			for (int i = 0; i < this.m_startingCards.Count; i++)
			{
				this.ToggleHoldState(i, true);
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
			{
				if (this.mulliganButtonWidget != null)
				{
					this.mulliganButtonWidget.SetEnabled(false);
					this.mulliganButtonWidget.gameObject.SetActive(false);
				}
			}
			else if (this.mulliganButton != null)
			{
				this.mulliganButton.SetEnabled(false, false);
				this.mulliganButton.gameObject.SetActive(false);
			}
			if (this.conditionalHelperTextLabel != null)
			{
				this.conditionalHelperTextLabel.gameObject.SetActive(true);
				return;
			}
		}
		else
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
			{
				if (this.mulliganButtonWidget != null)
				{
					this.mulliganButtonWidget.gameObject.SetActive(true);
				}
			}
			else if (this.mulliganButton != null)
			{
				this.mulliganButton.gameObject.SetActive(true);
			}
			if (this.conditionalHelperTextLabel != null)
			{
				this.conditionalHelperTextLabel.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002E1A RID: 11802 RVA: 0x000EA87C File Offset: 0x000E8A7C
	public void ToggleHoldState(global::Card toggleCard)
	{
		bool flag = false;
		bool booleanGameOption = GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE);
		for (int i = 0; i < this.m_startingCards.Count; i++)
		{
			if (this.m_startingCards[i] == toggleCard)
			{
				this.ToggleHoldState(i, false);
			}
			else if (booleanGameOption)
			{
				this.ToggleHoldState(i, true);
			}
			flag |= this.m_handCardsMarkedForReplace[i];
		}
		List<Actor> list = new List<Actor>(this.fakeCardsOnLeft.Count + this.fakeCardsOnRight.Count);
		list.AddRange(this.fakeCardsOnLeft);
		list.AddRange(this.fakeCardsOnRight);
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			if (this.mulliganButtonWidget != null)
			{
				this.mulliganButtonWidget.gameObject.SetActive(true);
			}
		}
		else if (this.mulliganButton != null)
		{
			this.mulliganButton.gameObject.SetActive(true);
		}
		if (this.conditionalHelperTextLabel != null)
		{
			this.conditionalHelperTextLabel.gameObject.SetActive(false);
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
		{
			foreach (Actor actor in list)
			{
				GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(actor, new bool?(false));
			}
		}
		if (booleanGameOption && this.mulliganButton != null)
		{
			if (!flag)
			{
				this.mulliganButton.SetEnabled(false, false);
				this.mulliganButtonWidget.SetEnabled(false);
				return;
			}
			this.mulliganButton.SetEnabled(true, false);
			this.mulliganButtonWidget.SetEnabled(true);
		}
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x000EAA2C File Offset: 0x000E8C2C
	public void ServerHasDealtReplacementCards(bool isFriendlySide)
	{
		if (isFriendlySide)
		{
			this.friendlyPlayerHasReplacementCards = true;
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents(false);
				return;
			}
		}
		else
		{
			this.opponentPlayerHasReplacementCards = true;
		}
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x000EAA58 File Offset: 0x000E8C58
	public void AutomaticContinueMulligan()
	{
		if (this.m_waitingForUserInput)
		{
			if (this.mulliganButton != null)
			{
				this.mulliganButton.SetEnabled(false, false);
			}
			if (this.mulliganButtonWidget != null)
			{
				this.mulliganButtonWidget.SetEnabled(false);
			}
			this.DestroyMulliganTimer();
			this.BeginDealNewCards();
			return;
		}
		this.SkipCardChoosing();
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x000EAAB8 File Offset: 0x000E8CB8
	private void OnMulliganButtonReleased(UIEvent e)
	{
		if (!InputManager.Get().PermitDecisionMakingInput())
		{
			return;
		}
		if (this.mulliganButton != null)
		{
			this.mulliganButton.SetEnabled(false, false);
		}
		if (this.mulliganButtonWidget != null)
		{
			this.mulliganButtonWidget.SetEnabled(false);
		}
		this.BeginDealNewCards();
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x000EAB0D File Offset: 0x000E8D0D
	private void BeginDealNewCards()
	{
		GameState.Get().GetGameEntity().OnMulliganBeginDealNewCards();
		if (this.m_waitingForUserInput)
		{
			this.m_waitingForUserInput = false;
			this.m_RemoveOldCardsAnimation = this.RemoveOldCardsAnimation();
			base.StartCoroutine(this.m_RemoveOldCardsAnimation);
		}
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x000EAB46 File Offset: 0x000E8D46
	private IEnumerator RemoveOldCardsAnimation()
	{
		this.m_waitingForUserInput = false;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			this.DestroyMulliganTimer();
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			SoundManager.Get().LoadAndPlay("FX_GameStart28_CardDismissWoosh2_v2.prefab:6eb21cb332351ea419772cb5ae32772a");
			this.DestroyXobjects();
		}
		else
		{
			SoundManager.Get().LoadAndPlay("BG_SelectHero.prefab:40cb8c418fca5f44391df4df2e9660cd");
		}
		Vector3 mulliganedCardsPosition = Board.Get().FindBone("MulliganedCardsPosition").position;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			this.DestroyChooseBanner();
			this.DestroyDetailLabel();
		}
		else
		{
			this.m_UpdateChooseBanner = this.UpdateChooseBanner();
			base.StartCoroutine(this.m_UpdateChooseBanner);
		}
		if (!UniversalInputManager.UsePhoneUI || GameState.Get().GetBooleanGameOption(GameEntityOption.SUPPRESS_CLASS_NAMES))
		{
			Gameplay.Get().RemoveClassNames();
		}
		foreach (global::Card card in this.m_startingCards)
		{
			card.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			card.GetActor().ToggleForceIdle(true);
			card.GetActor().TurnOffCollider();
		}
		this.hisHeroCardActor.SetActorState(ActorStateType.CARD_IDLE);
		this.hisHeroCardActor.ToggleForceIdle(true);
		global::Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (heroPowerCard != null && heroPowerCard.GetActor() != null)
		{
			heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			heroPowerCard.GetActor().ToggleForceIdle(true);
		}
		if (this.m_RemoveUIButtons != null)
		{
			base.StopCoroutine(this.m_RemoveUIButtons);
		}
		this.m_RemoveUIButtons = this.RemoveUIButtons();
		base.StartCoroutine(this.m_RemoveUIButtons);
		float TO_DECK_ANIMATION_TIME = 1.5f;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			int num;
			for (int i = 0; i < this.m_startingCards.Count; i = num + 1)
			{
				if (this.m_handCardsMarkedForReplace[i])
				{
					GameObject gameObject = this.m_startingCards[i].gameObject;
					iTween.MoveTo(gameObject, iTween.Hash(new object[]
					{
						"path",
						new Vector3[]
						{
							gameObject.transform.position,
							new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y - 1.7f, gameObject.transform.position.z),
							new Vector3(mulliganedCardsPosition.x, mulliganedCardsPosition.y, mulliganedCardsPosition.z),
							this.friendlySideDeck.transform.position
						},
						"time",
						TO_DECK_ANIMATION_TIME,
						"easetype",
						iTween.EaseType.easeOutCubic
					}));
					Animation animation = gameObject.GetComponent<Animation>();
					if (animation == null)
					{
						animation = gameObject.AddComponent<Animation>();
					}
					animation.AddClip(this.cardAnimatesFromBoardToDeck, "putCardBack");
					animation.Play("putCardBack");
					yield return new WaitForSeconds(0.5f);
				}
				num = i;
			}
		}
		if (!EndTurnButton.Get().IsDisabled)
		{
			InputManager.Get().DoEndTurnButton();
		}
		else
		{
			GameState.Get().SendChoices();
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			this.friendlySideHandZone.AddInputBlocker();
			while (!this.friendlyPlayerHasReplacementCards)
			{
				yield return null;
			}
			this.friendlySideHandZone.RemoveInputBlocker();
			this.SortHand(this.friendlySideHandZone);
			List<global::Card> handZoneCards = this.friendlySideHandZone.GetCards();
			foreach (global::Card card2 in handZoneCards)
			{
				if (!this.IsCoinCard(card2))
				{
					card2.GetActor().SetActorState(ActorStateType.CARD_IDLE);
					card2.GetActor().ToggleForceIdle(true);
					card2.GetActor().TurnOffCollider();
				}
			}
			float num2 = this.startingHandZone.GetComponent<Collider>().bounds.size.x;
			if (UniversalInputManager.UsePhoneUI)
			{
				num2 *= 0.55f;
			}
			float spaceForEachCard = num2 / (float)this.m_startingCards.Count;
			float leftSideOfZone = this.startingHandZone.transform.position.x - num2 / 2f;
			float xOffset = 0f;
			float cardHeightOffset = 0f;
			if (UniversalInputManager.UsePhoneUI)
			{
				cardHeightOffset = 7f;
			}
			float cardZpos = this.startingHandZone.transform.position.z - 0.3f;
			if (UniversalInputManager.UsePhoneUI)
			{
				cardZpos = this.startingHandZone.transform.position.z - 0.2f;
			}
			int num;
			for (int i = 0; i < this.m_startingCards.Count; i = num + 1)
			{
				if (this.m_handCardsMarkedForReplace[i])
				{
					GameObject topCard = handZoneCards[i].gameObject;
					iTween.Stop(topCard);
					iTween.MoveTo(topCard, iTween.Hash(new object[]
					{
						"position",
						new Vector3(leftSideOfZone + spaceForEachCard + xOffset - spaceForEachCard / 2f, this.friendlySideHandZone.GetComponent<Collider>().bounds.center.y, this.startingHandZone.transform.position.z),
						"time",
						3f
					}));
					Vector3[] array = new Vector3[]
					{
						topCard.transform.position,
						new Vector3(mulliganedCardsPosition.x, mulliganedCardsPosition.y, mulliganedCardsPosition.z),
						default(Vector3),
						new Vector3(leftSideOfZone + spaceForEachCard + xOffset - spaceForEachCard / 2f, this.friendlySideHandZone.GetComponent<Collider>().bounds.center.y + cardHeightOffset, cardZpos)
					};
					array[2] = new Vector3(array[3].x + 2f, array[3].y - 1.7f, array[3].z);
					iTween.MoveTo(topCard, iTween.Hash(new object[]
					{
						"path",
						array,
						"time",
						TO_DECK_ANIMATION_TIME,
						"easetype",
						iTween.EaseType.easeInCubic
					}));
					iTween.ScaleTo(topCard, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
					Animation animation2 = topCard.GetComponent<Animation>();
					if (animation2 == null)
					{
						animation2 = topCard.AddComponent<Animation>();
					}
					string text = "putCardBack";
					animation2.AddClip(this.cardAnimatesFromBoardToDeck, text);
					animation2[text].normalizedTime = 1f;
					animation2[text].speed = -1f;
					animation2.Play(text);
					yield return new WaitForSeconds(0.5f);
					if (topCard.GetComponent<AudioSource>() == null)
					{
						topCard.AddComponent<AudioSource>();
					}
					SoundManager.Get().LoadAndPlay("FX_GameStart30_CardReplaceSingle.prefab:aa2b215965bf6484da413a795c17e995", topCard);
					topCard = null;
				}
				xOffset += spaceForEachCard;
				num = i;
			}
			yield return new WaitForSeconds(1f);
			this.ShuffleDeck();
			yield return new WaitForSeconds(1.5f);
			handZoneCards = null;
		}
		if (this.opponentPlayerHasReplacementCards && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			this.EndMulligan();
		}
		else
		{
			this.m_WaitForOpponentToFinishMulligan = this.WaitForOpponentToFinishMulligan();
			base.StartCoroutine(this.WaitForOpponentToFinishMulligan());
		}
		yield break;
	}

	// Token: 0x06002E20 RID: 11808 RVA: 0x000EAB55 File Offset: 0x000E8D55
	private IEnumerator UpdateChooseBanner()
	{
		yield break;
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x000EAB5D File Offset: 0x000E8D5D
	private IEnumerator WaitForOpponentToFinishMulligan()
	{
		this.DestroyChooseBanner();
		this.DestroyDetailLabel();
		Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
		Vector3 position2;
		Vector3 scale;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(position.x, this.friendlySideHandZone.transform.position.y + 1f, this.myHeroCardActor.transform.position.z + 6.8f);
				scale = new Vector3(2.5f, 2.5f, 2.5f);
			}
			else
			{
				position2 = new Vector3(position.x, this.friendlySideHandZone.transform.position.y, this.myHeroCardActor.transform.position.z + 0.4f);
				scale = new Vector3(1.4f, 1.4f, 1.4f);
			}
		}
		else
		{
			position2 = position;
			scale = new Vector3(1.4f, 1.4f, 1.4f);
		}
		this.mulliganChooseBanner = UnityEngine.Object.Instantiate<GameObject>(this.mulliganChooseBannerPrefab, position2, new Quaternion(0f, 0f, 0f, 0f));
		this.mulliganChooseBanner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		iTween.ScaleTo(this.mulliganChooseBanner, scale, 0.4f);
		Actor yourHeroActor = null;
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			string mulliganWaitingText = GameState.Get().GetGameEntity().GetMulliganWaitingText();
			string mulliganWaitingSubtitleText = GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
			while (GameState.Get().GetPlayerInfoMap()[GameState.Get().GetFriendlyPlayerId()].GetPlayerHero() == null)
			{
				mulliganWaitingText = GameState.Get().GetGameEntity().GetMulliganWaitingText();
				mulliganWaitingSubtitleText = GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
				this.SetMulliganBannerText(mulliganWaitingText, mulliganWaitingSubtitleText);
				yield return new WaitForSeconds(0.5f);
			}
			if (this.m_startingCards.Count == 0)
			{
				this.m_startingCards.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
				using (List<global::Card>.Enumerator enumerator = this.m_startingCards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						global::Card card = enumerator.Current;
						card.GetActor().SetActorState(ActorStateType.CARD_IDLE);
						card.GetActor().TurnOffCollider();
						card.GetActor().GetMeshRenderer(false).gameObject.layer = 8;
						if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
						{
							this.pendingHeroCount++;
							card.GetActor().gameObject.SetActive(false);
							AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), new PrefabCallback<GameObject>(this.OnHeroActorLoaded), card, AssetLoadingOptions.IgnorePrefabPosition);
						}
					}
					goto IL_378;
				}
				IL_35C:
				yield return null;
				IL_378:
				if (this.pendingHeroCount > 0)
				{
					goto IL_35C;
				}
			}
			foreach (global::Card card2 in this.m_startingCards)
			{
				if (card2.GetEntity().GetCardId() == GameState.Get().GetPlayerInfoMap()[GameState.Get().GetFriendlyPlayerId()].GetPlayerHero().GetCardId())
				{
					float num = this.startingHandZone.GetComponent<Collider>().bounds.size.x;
					if (UniversalInputManager.UsePhoneUI)
					{
						num *= 0.55f;
					}
					float num2 = num;
					float num3 = this.startingHandZone.transform.position.x - num / 2f;
					float num4 = 0f;
					if (UniversalInputManager.UsePhoneUI)
					{
						num4 = 7f;
					}
					float z = this.startingHandZone.transform.position.z - 0.3f;
					if (UniversalInputManager.UsePhoneUI)
					{
						z = this.startingHandZone.transform.position.z - 0.2f;
					}
					float num5 = num2 / 2f;
					GameObject gameObject = card2.gameObject;
					if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
					{
						gameObject = this.choiceHeroActors[card2].gameObject.transform.parent.gameObject;
						yourHeroActor = this.choiceHeroActors[card2];
						yourHeroActor.GetCard().SetActor(yourHeroActor);
						yourHeroActor.GetCard().GetActor().Show();
						GameState.Get().GetGameEntity().ApplyMulliganActorLobbyStateChanges(yourHeroActor);
						((PlayerLeaderboardMainCardActor)yourHeroActor).UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(GameState.Get().GetFriendlySidePlayer().GetPlayerId()));
						this.myHeroCardActor = yourHeroActor;
					}
					iTween.Stop(gameObject);
					iTween.MoveTo(gameObject, iTween.Hash(new object[]
					{
						"path",
						new Vector3[]
						{
							gameObject.transform.position,
							new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3.6f, gameObject.transform.position.z),
							new Vector3(num3 + num5, this.friendlySideHandZone.transform.position.y + num4, z)
						},
						"time",
						MulliganManager.ANIMATION_TIME_DEAL_CARD,
						"easetype",
						iTween.EaseType.easeInSineOutExpo
					}));
					if (UniversalInputManager.UsePhoneUI)
					{
						iTween.ScaleTo(gameObject, new Vector3(0.9f, 1.1f, 0.9f), MulliganManager.ANIMATION_TIME_DEAL_CARD);
					}
					else
					{
						iTween.ScaleTo(gameObject, new Vector3(1.2f, 1.1f, 1.2f), MulliganManager.ANIMATION_TIME_DEAL_CARD);
					}
					iTween.RotateTo(gameObject, iTween.Hash(new object[]
					{
						"rotation",
						new Vector3(0f, 0f, 0f),
						"time",
						MulliganManager.ANIMATION_TIME_DEAL_CARD,
						"delay",
						MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
					}));
				}
				else if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					this.choiceHeroActors[card2].ActivateSpellBirthState(SpellType.DEATH);
					((PlayerLeaderboardMainCardActor)this.choiceHeroActors[card2]).m_fullSelectionHighlight.SetActive(false);
				}
				else
				{
					card2.FakeDeath();
				}
			}
			this.CleanupFakeCards();
			bool heroPowerCreated = false;
			do
			{
				if (!heroPowerCreated)
				{
					global::Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
					if (heroPowerCard != null && heroPowerCard.GetActor() != null)
					{
						heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
						heroPowerCard.GetActor().ToggleForceIdle(true);
						heroPowerCard.GetActor().TurnOffCollider();
						heroPowerCreated = true;
					}
				}
				mulliganWaitingText = GameState.Get().GetGameEntity().GetMulliganWaitingText();
				mulliganWaitingSubtitleText = GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
				this.SetMulliganBannerText(mulliganWaitingText, mulliganWaitingSubtitleText);
				yield return null;
			}
			while (!GameState.Get().GetGameEntity().IsHeroMulliganLobbyFinished());
			foreach (SharedPlayerInfo sph in GameState.Get().GetPlayerInfoMap().Values)
			{
				if (sph.GetPlayerId() != GameState.Get().GetFriendlyPlayerId())
				{
					this.pendingHeroCount++;
					while (sph.GetPlayerHero() == null)
					{
						yield return null;
					}
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME), new PrefabCallback<GameObject>(this.OnOpponentHeroActorLoaded), sph.GetPlayerHero().GetCard(), AssetLoadingOptions.IgnorePrefabPosition);
				}
				sph = null;
			}
			Map<int, SharedPlayerInfo>.ValueCollection.Enumerator enumerator2 = default(Map<int, SharedPlayerInfo>.ValueCollection.Enumerator);
			while (this.pendingHeroCount > 0)
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.5f);
			this.DestroyMulliganTimer();
			this.DestroyChooseBanner();
			this.DestroyDetailLabel();
			Transform rootTransform = yourHeroActor.gameObject.transform.parent.parent;
			Transform yourHeroRoot = yourHeroActor.gameObject.transform.parent;
			Vector3 vsPosition = Board.Get().FindBone("VS_Position").position;
			yield return new WaitForSeconds(1f);
			iTween.Stop(yourHeroRoot.gameObject);
			int num6 = 1;
			foreach (Actor actor in this.opponentHeroActors.Values)
			{
				actor.gameObject.transform.parent = rootTransform;
				actor.gameObject.transform.localScale = new Vector3(1.0506f, 1.0506f, 1.0506f);
				actor.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				Vector3 position3 = Board.Get().FindBone("HeroSpawnLineUp0" + num6++.ToString()).position;
				actor.gameObject.transform.position = position3;
				((PlayerLeaderboardMainCardActor)actor).SetAlternateNameTextActive(false);
				SharedPlayerInfo playerForCard = this.GetPlayerForCard(actor.GetCard());
				if (playerForCard != null)
				{
					((PlayerLeaderboardMainCardActor)actor).UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(playerForCard.GetPlayerId()));
				}
			}
			yourHeroActor.transform.parent = null;
			yourHeroRoot.position = new Vector3(-7.7726f, 0.0055918f, -8.054f);
			yourHeroRoot.localScale = new Vector3(1.134f, 1.134f, 1.134f);
			yourHeroActor.transform.parent = yourHeroRoot;
			yourHeroActor.GetComponent<PlayMakerFSM>().SendEvent(UniversalInputManager.UsePhoneUI ? "SlotInHeroAfterFlyIn_Phone" : "SlotInHeroAfterFlyIn");
			yield return new WaitForSeconds(1f);
			if (this.versusText)
			{
				this.versusText.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				this.versusText.transform.position = vsPosition;
			}
			yield return new WaitForSeconds(1.5f);
			int num7 = 1;
			foreach (Actor actor2 in this.opponentHeroActors.Values)
			{
				PlayMakerFSM component = actor2.GetComponent<PlayMakerFSM>();
				component.FsmVariables.GetFsmInt("Player").Value = num7++;
				component.SendEvent(UniversalInputManager.UsePhoneUI ? "Spawn_Phone" : "Spawn");
			}
			yield return new WaitForSeconds(1.5f);
			if (this.versusText)
			{
				yield return new WaitForSeconds(0.1f);
				this.versusText.FadeOut();
				yield return new WaitForSeconds(0.32f);
			}
			foreach (Actor actor3 in this.opponentHeroActors.Values)
			{
				actor3.GetComponent<PlayMakerFSM>().SendEvent(UniversalInputManager.UsePhoneUI ? "FlyIn_Phone" : "FlyIn");
			}
			PlayerLeaderboardManager.Get().UpdateLayout(false);
			yield return new WaitForSeconds(1.5f);
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ClearMulliganActorStateChanges(yourHeroActor);
			}
			foreach (Actor actor4 in this.opponentHeroActors.Values)
			{
				actor4.gameObject.SetActive(false);
			}
			rootTransform = null;
			yourHeroRoot = null;
			vsPosition = default(Vector3);
		}
		else
		{
			this.SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_WAITING"));
			this.mulliganChooseBanner.GetComponent<Banner>().MoveGlowForBottomPlacement();
			while (!this.opponentPlayerHasReplacementCards && !GameState.Get().IsGameOver())
			{
				yield return null;
			}
		}
		this.EndMulligan();
		yield break;
		yield break;
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x000EAB6C File Offset: 0x000E8D6C
	private SharedPlayerInfo GetPlayerForCard(global::Card card)
	{
		foreach (SharedPlayerInfo sharedPlayerInfo in GameState.Get().GetPlayerInfoMap().Values)
		{
			if (card.GetEntity().GetCardId() == sharedPlayerInfo.GetPlayerHero().GetCardId())
			{
				return sharedPlayerInfo;
			}
		}
		return null;
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x000EABE8 File Offset: 0x000E8DE8
	private void SetMulliganBannerText(string title)
	{
		this.SetMulliganBannerText(title, null);
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x000EABF2 File Offset: 0x000E8DF2
	private void SetMulliganBannerText(string title, string subtitle)
	{
		if (this.mulliganChooseBanner == null)
		{
			return;
		}
		if (subtitle != null)
		{
			this.mulliganChooseBanner.GetComponent<Banner>().SetText(title, subtitle);
			return;
		}
		this.mulliganChooseBanner.GetComponent<Banner>().SetText(title);
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x000EAC2A File Offset: 0x000E8E2A
	private void SetMulliganDetailLabelText(string title)
	{
		if (this.mulliganDetailLabel == null)
		{
			return;
		}
		this.mulliganDetailLabel.GetComponent<UberText>().Text = title;
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x000EAC4C File Offset: 0x000E8E4C
	private void ShowMulliganDetail()
	{
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL))
		{
			string mulliganDetailText = GameState.Get().GetGameEntity().GetMulliganDetailText();
			if (mulliganDetailText != null)
			{
				this.mulliganDetailLabel = UnityEngine.Object.Instantiate<GameObject>(this.mulliganDetailLabelPrefab);
				this.mulliganDetailLabel.transform.position = Board.Get().FindBone("MulliganDetail").position;
				this.SetMulliganDetailLabelText(mulliganDetailText);
			}
		}
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x000EACB6 File Offset: 0x000E8EB6
	private IEnumerator RemoveUIButtons()
	{
		if (this.mulliganButton != null)
		{
			this.mulliganButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Death");
		}
		if (this.mulliganButtonWidget != null)
		{
			this.mulliganButtonWidget.gameObject.SetActive(false);
		}
		if (this.m_replaceLabels != null)
		{
			int num;
			for (int i = 0; i < this.m_replaceLabels.Count; i = num + 1)
			{
				if (this.m_replaceLabels[i] != null)
				{
					iTween.RotateTo(this.m_replaceLabels[i].gameObject, iTween.Hash(new object[]
					{
						"rotation",
						new Vector3(0f, 0f, 0f),
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeInExpo
					}));
					iTween.ScaleTo(this.m_replaceLabels[i].gameObject, iTween.Hash(new object[]
					{
						"scale",
						new Vector3(0.001f, 0.001f, 0.001f),
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeInExpo,
						"oncomplete",
						"DestroyButton",
						"oncompletetarget",
						base.gameObject,
						"oncompleteparams",
						this.m_replaceLabels[i]
					}));
					yield return new WaitForSeconds(0.05f);
				}
				num = i;
			}
		}
		yield return new WaitForSeconds(3.5f);
		if (this.mulliganButton != null)
		{
			UnityEngine.Object.Destroy(this.mulliganButton.gameObject);
		}
		if (this.mulliganButtonWidget != null)
		{
			UnityEngine.Object.Destroy(this.mulliganButtonWidget.gameObject);
		}
		yield break;
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x000A6C55 File Offset: 0x000A4E55
	private void DestroyButton(UnityEngine.Object buttonToDestroy)
	{
		UnityEngine.Object.Destroy(buttonToDestroy);
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x000EACC8 File Offset: 0x000E8EC8
	private void HandleGameOverDuringMulligan()
	{
		if (this.m_WaitForBoardThenLoadButton != null)
		{
			base.StopCoroutine(this.m_WaitForBoardThenLoadButton);
		}
		this.m_WaitForBoardThenLoadButton = null;
		if (this.m_WaitForHeroesAndStartAnimations != null)
		{
			base.StopCoroutine(this.m_WaitForHeroesAndStartAnimations);
		}
		this.m_WaitForHeroesAndStartAnimations = null;
		if (this.m_ResumeMulligan != null)
		{
			base.StopCoroutine(this.m_ResumeMulligan);
		}
		this.m_ResumeMulligan = null;
		if (this.m_DealStartingCards != null)
		{
			base.StopCoroutine(this.m_DealStartingCards);
		}
		this.m_DealStartingCards = null;
		if (this.m_ShowMultiplayerWaitingArea != null)
		{
			base.StopCoroutine(this.m_ShowMultiplayerWaitingArea);
		}
		this.m_ShowMultiplayerWaitingArea = null;
		if (this.m_RemoveOldCardsAnimation != null)
		{
			base.StopCoroutine(this.m_RemoveOldCardsAnimation);
		}
		this.m_RemoveOldCardsAnimation = null;
		if (this.m_PlayStartingTaunts != null)
		{
			base.StopCoroutine(this.m_PlayStartingTaunts);
		}
		this.m_PlayStartingTaunts = null;
		if (this.m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen != null)
		{
			base.StopCoroutine(this.m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen);
		}
		this.m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen = null;
		if (this.m_ContinueMulliganWhenBoardLoads != null)
		{
			base.StopCoroutine(this.m_ContinueMulliganWhenBoardLoads);
		}
		this.m_ContinueMulliganWhenBoardLoads = null;
		if (this.m_WaitAFrameBeforeSendingEventToMulliganButton != null)
		{
			base.StopCoroutine(this.m_WaitAFrameBeforeSendingEventToMulliganButton);
		}
		this.m_WaitAFrameBeforeSendingEventToMulliganButton = null;
		if (this.m_ShrinkStartingHandBanner != null)
		{
			base.StopCoroutine(this.m_ShrinkStartingHandBanner);
		}
		this.m_ShrinkStartingHandBanner = null;
		if (this.m_AnimateCoinTossText != null)
		{
			base.StopCoroutine(this.m_AnimateCoinTossText);
		}
		this.m_AnimateCoinTossText = null;
		if (this.m_WaitForOpponentToFinishMulligan != null)
		{
			base.StopCoroutine(this.m_WaitForOpponentToFinishMulligan);
		}
		this.m_WaitForOpponentToFinishMulligan = null;
		if (this.m_EndMulliganWithTiming != null)
		{
			base.StopCoroutine(this.m_EndMulliganWithTiming);
		}
		this.m_EndMulliganWithTiming = null;
		if (this.m_HandleCoinCard != null)
		{
			base.StopCoroutine(this.m_HandleCoinCard);
		}
		this.m_HandleCoinCard = null;
		if (this.m_EnableHandCollidersAfterCardsAreDealt != null)
		{
			base.StopCoroutine(this.m_EnableHandCollidersAfterCardsAreDealt);
		}
		this.m_EnableHandCollidersAfterCardsAreDealt = null;
		if (this.m_SkipMulliganForResume != null)
		{
			base.StopCoroutine(this.m_SkipMulliganForResume);
		}
		this.m_SkipMulliganForResume = null;
		if (this.m_SkipMulliganWhenIntroComplete != null)
		{
			base.StopCoroutine(this.m_SkipMulliganWhenIntroComplete);
		}
		this.m_SkipMulliganWhenIntroComplete = null;
		if (this.m_WaitForBoardAnimToCompleteThenStartTurn != null)
		{
			base.StopCoroutine(this.m_WaitForBoardAnimToCompleteThenStartTurn);
		}
		this.m_WaitForBoardAnimToCompleteThenStartTurn = null;
		if (this.m_customIntroCoroutine != null)
		{
			base.StopCoroutine(this.m_customIntroCoroutine);
			GameState.Get().GetGameEntity().OnCustomIntroCancelled(this.myHeroCardActor.GetCard(), this.hisHeroCardActor.GetCard(), this.myheroLabel, this.hisheroLabel, this.versusText);
			this.m_customIntroCoroutine = null;
		}
		this.m_waitingForUserInput = false;
		this.DestroyXobjects();
		this.DestroyChooseBanner();
		this.DestroyDetailLabel();
		this.DestroyMulliganTimer();
		if (this.coinObject != null)
		{
			UnityEngine.Object.Destroy(this.coinObject);
		}
		if (this.versusText != null)
		{
			UnityEngine.Object.Destroy(this.versusText.gameObject);
		}
		if (this.versusVo != null)
		{
			SoundManager.Get().Destroy(this.versusVo);
		}
		if (this.coinTossText != null)
		{
			UnityEngine.Object.Destroy(this.coinTossText);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			Gameplay.Get().RemoveNameBanners();
		}
		else
		{
			Gameplay.Get().RemoveClassNames();
		}
		if (this.m_RemoveUIButtons != null)
		{
			base.StopCoroutine(this.m_RemoveUIButtons);
		}
		this.m_RemoveUIButtons = this.RemoveUIButtons();
		base.StartCoroutine(this.m_RemoveUIButtons);
		if (this.mulliganButton != null)
		{
			this.mulliganButton.SetEnabled(false, false);
		}
		if (this.mulliganButtonWidget != null)
		{
			this.mulliganButtonWidget.SetEnabled(false);
		}
		this.DestoryHeroSkinSocketInEffects();
		if (this.myheroLabel != null && this.myheroLabel.isActiveAndEnabled)
		{
			this.myheroLabel.FadeOut();
		}
		if (this.hisheroLabel != null && this.hisheroLabel.isActiveAndEnabled)
		{
			this.hisheroLabel.FadeOut();
		}
		if (this.friendlySideHandZone != null)
		{
			foreach (global::Card card in this.friendlySideHandZone.GetCards())
			{
				Actor actor = card.GetActor();
				actor.SetActorState(ActorStateType.CARD_IDLE);
				actor.ToggleForceIdle(true);
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
				{
					actor.ActivateSpellBirthState(SpellType.DEATH);
					((PlayerLeaderboardMainCardActor)actor).m_fullSelectionHighlight.SetActive(false);
				}
			}
			if (this.hisHeroCardActor != null)
			{
				this.hisHeroCardActor.SetActorState(ActorStateType.CARD_IDLE);
				this.hisHeroCardActor.ToggleForceIdle(true);
			}
			global::Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
			if (heroPowerCard != null && heroPowerCard.GetActor() != null)
			{
				heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
				heroPowerCard.GetActor().ToggleForceIdle(true);
			}
			if (!this.friendlyPlayerGoesFirst && this.ShouldHandleCoinCard())
			{
				global::Card coinCardFromFriendlyHand = this.GetCoinCardFromFriendlyHand();
				coinCardFromFriendlyHand.SetDoNotSort(false);
				coinCardFromFriendlyHand.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				this.PutCoinCardInSpawnPosition(coinCardFromFriendlyHand);
				coinCardFromFriendlyHand.GetActor().Show();
			}
			this.friendlySideHandZone.ForceStandInUpdate();
			this.friendlySideHandZone.SetDoNotUpdateLayout(false);
			this.friendlySideHandZone.UpdateLayout();
		}
		this.CleanupFakeCards();
		Board board = Board.Get();
		if (board != null)
		{
			board.RaiseTheLightsQuickly();
		}
		if (this.myHeroCardActor != null)
		{
			Animation component = this.myHeroCardActor.gameObject.GetComponent<Animation>();
			if (component != null)
			{
				component.Stop();
			}
			this.myHeroCardActor.transform.localScale = Vector3.one;
			this.myHeroCardActor.transform.rotation = Quaternion.identity;
			this.myHeroCardActor.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
		}
		if (this.hisHeroCardActor != null)
		{
			Animation component2 = this.hisHeroCardActor.gameObject.GetComponent<Animation>();
			if (component2 != null)
			{
				component2.Stop();
			}
			this.hisHeroCardActor.transform.localScale = Vector3.one;
			this.hisHeroCardActor.transform.rotation = Quaternion.identity;
			this.hisHeroCardActor.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
		}
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x000EB304 File Offset: 0x000E9504
	private void CleanupFakeCards()
	{
		List<Actor> list = new List<Actor>(this.fakeCardsOnLeft.Count + this.fakeCardsOnRight.Count);
		list.AddRange(this.fakeCardsOnLeft);
		list.AddRange(this.fakeCardsOnRight);
		foreach (Actor actor in list)
		{
			actor.ActivateSpellBirthState(SpellType.DEATH);
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ConfigureFakeMulliganCardActor(actor, false);
			}
		}
		if (this.conditionalHelperTextLabel != null)
		{
			this.conditionalHelperTextLabel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002E2B RID: 11819 RVA: 0x000EB3C4 File Offset: 0x000E95C4
	public void EndMulligan()
	{
		this.m_waitingForUserInput = false;
		if (this.m_replaceLabels != null)
		{
			for (int i = 0; i < this.m_replaceLabels.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_replaceLabels[i]);
			}
		}
		if (this.mulliganButton != null)
		{
			UnityEngine.Object.Destroy(this.mulliganButton.gameObject);
		}
		if (this.mulliganButtonWidget != null)
		{
			UnityEngine.Object.Destroy(this.mulliganButtonWidget.gameObject);
		}
		this.DestroyXobjects();
		this.DestroyChooseBanner();
		this.DestroyDetailLabel();
		if (this.versusText != null)
		{
			UnityEngine.Object.Destroy(this.versusText.gameObject);
		}
		if (this.versusVo != null)
		{
			SoundManager.Get().Destroy(this.versusVo);
		}
		if (this.coinTossText != null)
		{
			UnityEngine.Object.Destroy(this.coinTossText);
		}
		if (this.hisheroLabel != null)
		{
			this.hisheroLabel.FadeOut();
		}
		if (this.myheroLabel != null)
		{
			this.myheroLabel.FadeOut();
		}
		this.DestoryHeroSkinSocketInEffects();
		this.myHeroCardActor.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.hisHeroCardActor.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.myHeroCardActor.Show();
		if (GameState.Get().IsGameOver())
		{
			return;
		}
		this.myHeroCardActor.GetHealthObject().Show();
		this.hisHeroCardActor.GetHealthObject().Show();
		if (this.myHeroCardActor.GetAttackObject() != null)
		{
			this.myHeroCardActor.GetAttackObject().Show();
		}
		if (this.hisHeroCardActor.GetAttackObject() != null)
		{
			this.hisHeroCardActor.GetAttackObject().Show();
		}
		this.friendlySideHandZone.ForceStandInUpdate();
		this.friendlySideHandZone.SetDoNotUpdateLayout(false);
		this.friendlySideHandZone.UpdateLayout();
		if (this.m_startingOppCards != null && this.m_startingOppCards.Count > 0)
		{
			this.m_startingOppCards[this.m_startingOppCards.Count - 1].SetDoNotSort(false);
		}
		this.opposingSideHandZone.SetDoNotUpdateLayout(false);
		this.opposingSideHandZone.UpdateLayout();
		this.friendlySideDeck.SetSuppressEmotes(false);
		this.opposingSideDeck.SetSuppressEmotes(false);
		Board.Get().SplitSurface();
		if (UniversalInputManager.UsePhoneUI)
		{
			Gameplay.Get().RemoveNameBanners();
			Gameplay.Get().AddGamePlayNameBannerPhone();
		}
		if (this.m_MyCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_MyCustomSocketInSpell);
		}
		if (this.m_HisCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_HisCustomSocketInSpell);
		}
		this.m_EndMulliganWithTiming = this.EndMulliganWithTiming();
		base.StartCoroutine(this.m_EndMulliganWithTiming);
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x000EB6A2 File Offset: 0x000E98A2
	private IEnumerator EndMulliganWithTiming()
	{
		if (this.ShouldHandleCoinCard())
		{
			this.m_HandleCoinCard = this.HandleCoinCard();
			yield return base.StartCoroutine(this.m_HandleCoinCard);
		}
		else
		{
			UnityEngine.Object.Destroy(this.coinObject);
		}
		this.myHeroCardActor.TurnOnCollider();
		this.hisHeroCardActor.TurnOnCollider();
		this.FadeOutMulliganMusicAndStartGameplayMusic();
		foreach (global::Card card in this.friendlySideHandZone.GetCards())
		{
			card.GetActor().TurnOnCollider();
			card.GetActor().ToggleForceIdle(false);
		}
		this.myHeroCardActor.ToggleForceIdle(false);
		this.hisHeroCardActor.ToggleForceIdle(false);
		global::Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (heroPowerCard != null && heroPowerCard.GetActor() != null)
		{
			heroPowerCard.GetActor().ToggleForceIdle(false);
		}
		if (!this.friendlyPlayerHasReplacementCards)
		{
			this.m_EnableHandCollidersAfterCardsAreDealt = this.EnableHandCollidersAfterCardsAreDealt();
			base.StartCoroutine(this.m_EnableHandCollidersAfterCardsAreDealt);
		}
		Board.Get().FindCollider("DragPlane").enabled = true;
		this.ForceMulliganActive(false);
		Board.Get().RaiseTheLights();
		this.FadeHeroPowerIn(GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard());
		this.FadeHeroPowerIn(GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard());
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		this.m_WaitForBoardAnimToCompleteThenStartTurn = this.WaitForBoardAnimToCompleteThenStartTurn();
		base.StartCoroutine(this.m_WaitForBoardAnimToCompleteThenStartTurn);
		yield break;
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x000EB6B1 File Offset: 0x000E98B1
	private IEnumerator HandleCoinCard()
	{
		if (!this.friendlyPlayerGoesFirst)
		{
			if (this.coinObject != null && this.coinObject.activeSelf)
			{
				yield return new WaitForSeconds(0.5f);
				this.coinObject.GetComponentInChildren<PlayMakerFSM>().SendEvent("Birth");
				yield return new WaitForSeconds(0.1f);
			}
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_THE_COIN, false) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.HandleCoinCard:" + Option.HAS_SEEN_THE_COIN))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_COIN_INTRO"), "VO_INNKEEPER_COIN_INTRO.prefab:6fb1b3b124d474c4c84e392646caada4", 0f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_THE_COIN, true);
			}
			global::Card coinCardFromFriendlyHand = this.GetCoinCardFromFriendlyHand();
			this.PutCoinCardInSpawnPosition(coinCardFromFriendlyHand);
			coinCardFromFriendlyHand.ActivateActorSpell(SpellType.SUMMON_IN, new Spell.FinishedCallback(this.CoinCardSummonFinishedCallback));
			yield return new WaitForSeconds(1f);
		}
		else
		{
			UnityEngine.Object.Destroy(this.coinObject);
			if (this.m_coinCardIndex >= 0)
			{
				this.m_startingOppCards[this.m_coinCardIndex].SetDoNotSort(false);
			}
			this.opposingSideHandZone.UpdateLayout();
		}
		yield break;
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x000EB6C0 File Offset: 0x000E98C0
	private bool IsCoinCard(global::Card card)
	{
		return card.GetEntity().GetCardId() == CoinManager.Get().GetFavoriteCoinCardId();
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x000EB6DC File Offset: 0x000E98DC
	private global::Card GetCoinCardFromFriendlyHand()
	{
		List<global::Card> cards = this.friendlySideHandZone.GetCards();
		if (cards.Count > 0)
		{
			return cards[cards.Count - 1];
		}
		Debug.LogError("GetCoinCardFromFriendlyHand() failed. friendlySideHandZone is empty.");
		return null;
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x000EB718 File Offset: 0x000E9918
	private void PutCoinCardInSpawnPosition(global::Card coinCard)
	{
		coinCard.transform.position = Board.Get().FindBone("MulliganCoinCardSpawnPosition").position;
		coinCard.transform.localScale = Board.Get().FindBone("MulliganCoinCardSpawnPosition").localScale;
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x000EB758 File Offset: 0x000E9958
	private bool ShouldHandleCoinCard()
	{
		return GameState.Get().IsMulliganPhase() && GameState.Get().GetBooleanGameOption(GameEntityOption.HANDLE_COIN);
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x000EB778 File Offset: 0x000E9978
	private void CoinCardSummonFinishedCallback(Spell spell, object userData)
	{
		global::Card card = SceneUtils.FindComponentInParents<global::Card>(spell);
		card.RefreshActor();
		card.UpdateActorComponents();
		card.SetDoNotSort(false);
		UnityEngine.Object.Destroy(this.coinObject);
		card.SetTransitionStyle(ZoneTransitionStyle.VERY_SLOW);
		this.friendlySideHandZone.UpdateLayout(null, true);
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x000EB7B1 File Offset: 0x000E99B1
	private IEnumerator EnableHandCollidersAfterCardsAreDealt()
	{
		while (!this.friendlyPlayerHasReplacementCards)
		{
			yield return null;
		}
		using (List<global::Card>.Enumerator enumerator = this.friendlySideHandZone.GetCards().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				global::Card card = enumerator.Current;
				card.GetActor().TurnOnCollider();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x000EB7C0 File Offset: 0x000E99C0
	public void SkipCardChoosing()
	{
		this.skipCardChoosing = true;
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x000EB7CC File Offset: 0x000E99CC
	public void SkipMulliganForDev()
	{
		if (this.m_WaitForBoardThenLoadButton != null)
		{
			base.StopCoroutine(this.m_WaitForBoardThenLoadButton);
		}
		this.m_WaitForBoardThenLoadButton = null;
		if (this.m_WaitForHeroesAndStartAnimations != null)
		{
			base.StopCoroutine(this.m_WaitForHeroesAndStartAnimations);
		}
		this.m_WaitForHeroesAndStartAnimations = null;
		if (this.m_DealStartingCards != null)
		{
			base.StopCoroutine(this.m_DealStartingCards);
		}
		this.m_DealStartingCards = null;
		if (this.m_ShowMultiplayerWaitingArea != null)
		{
			base.StopCoroutine(this.m_ShowMultiplayerWaitingArea);
		}
		this.m_ShowMultiplayerWaitingArea = null;
		this.EndMulligan();
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x000EB84B File Offset: 0x000E9A4B
	private IEnumerator SkipMulliganForResume()
	{
		this.introComplete = true;
		this.ForceMulliganActive(false);
		SoundDucker ducker = null;
		if (!GameMgr.Get().IsSpectator())
		{
			ducker = base.gameObject.AddComponent<SoundDucker>();
			ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
			foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
			{
				Global.SoundCategory soundCategory = (Global.SoundCategory)obj;
				if (soundCategory != Global.SoundCategory.AMBIENCE && soundCategory != Global.SoundCategory.MUSIC)
				{
					SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
					soundDuckedCategoryDef.m_Category = soundCategory;
					soundDuckedCategoryDef.m_Volume = 0f;
					soundDuckedCategoryDef.m_RestoreSec = 5f;
					soundDuckedCategoryDef.m_BeginSec = 0f;
					ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
				}
			}
			ducker.StartDucking();
		}
		while (Board.Get() == null)
		{
			yield return null;
		}
		Board.Get().RaiseTheLightsQuickly();
		while (ZoneMgr.Get() == null)
		{
			yield return null;
		}
		this.InitZones();
		Collider dragPlane = Board.Get().FindCollider("DragPlane");
		this.friendlySideHandZone.SetDoNotUpdateLayout(false);
		this.opposingSideHandZone.SetDoNotUpdateLayout(false);
		dragPlane.enabled = false;
		this.friendlySideHandZone.AddInputBlocker();
		this.opposingSideHandZone.AddInputBlocker();
		while (!GameState.Get().IsGameCreated())
		{
			yield return null;
		}
		while (ZoneMgr.Get().HasActiveServerChange())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().NotifyOfMulliganInitialized();
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			this.FadeOutMulliganMusicAndStartGameplayMusic();
		}
		dragPlane.enabled = true;
		this.friendlySideHandZone.RemoveInputBlocker();
		this.opposingSideHandZone.RemoveInputBlocker();
		this.friendlySideDeck.SetSuppressEmotes(false);
		this.opposingSideDeck.SetSuppressEmotes(false);
		if (GameState.Get().GetResponseMode() == GameState.ResponseMode.CHOICE)
		{
			GameState.Get().UpdateChoiceHighlights();
		}
		else if (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION)
		{
			GameState.Get().UpdateOptionHighlights();
		}
		GameMgr.Get().UpdatePresence();
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x000EB85A File Offset: 0x000E9A5A
	public void SkipMulligan()
	{
		Gameplay.Get().RemoveClassNames();
		this.m_SkipMulliganWhenIntroComplete = this.SkipMulliganWhenIntroComplete();
		base.StartCoroutine(this.m_SkipMulliganWhenIntroComplete);
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x000EB87F File Offset: 0x000E9A7F
	private IEnumerator SkipMulliganWhenIntroComplete()
	{
		this.m_waitingForUserInput = false;
		while (!this.introComplete)
		{
			yield return null;
		}
		this.myHeroCardActor.TurnOnCollider();
		this.hisHeroCardActor.TurnOnCollider();
		this.FadeOutMulliganMusicAndStartGameplayMusic();
		this.myHeroCardActor.GetHealthObject().Show();
		this.hisHeroCardActor.GetHealthObject().Show();
		Board.Get().FindCollider("DragPlane").enabled = true;
		Board.Get().SplitSurface();
		Board.Get().RaiseTheLights();
		this.FadeHeroPowerIn(GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard());
		this.FadeHeroPowerIn(GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard());
		this.ForceMulliganActive(false);
		this.InitZones();
		this.friendlySideHandZone.SetDoNotUpdateLayout(false);
		this.friendlySideHandZone.UpdateLayout();
		this.opposingSideHandZone.SetDoNotUpdateLayout(false);
		this.opposingSideHandZone.UpdateLayout();
		this.friendlySideDeck.SetSuppressEmotes(false);
		this.opposingSideDeck.SetSuppressEmotes(false);
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		this.m_WaitForBoardAnimToCompleteThenStartTurn = this.WaitForBoardAnimToCompleteThenStartTurn();
		base.StartCoroutine(this.m_WaitForBoardAnimToCompleteThenStartTurn);
		yield break;
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x000EB88E File Offset: 0x000E9A8E
	private void FadeOutMulliganMusicAndStartGameplayMusic()
	{
		GameState.Get().GetGameEntity().StartGameplaySoundtracks();
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x000EB89F File Offset: 0x000E9A9F
	private IEnumerator WaitForBoardAnimToCompleteThenStartTurn()
	{
		yield return new WaitForSeconds(1.5f);
		GameState.Get().SetMulliganBusy(false);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06002E3B RID: 11835 RVA: 0x000EB8B0 File Offset: 0x000E9AB0
	private void ShuffleDeck()
	{
		SoundManager.Get().LoadAndPlay("FX_MulliganCoin09_DeckShuffle.prefab:e80f93eec961ec24485521285a8addf7", this.friendlySideDeck.gameObject);
		Animation animation = this.friendlySideDeck.gameObject.GetComponent<Animation>();
		if (animation == null)
		{
			animation = this.friendlySideDeck.gameObject.AddComponent<Animation>();
		}
		animation.AddClip(this.shuffleDeck, "shuffleDeckAnim");
		animation.Play("shuffleDeckAnim");
		animation = this.opposingSideDeck.gameObject.GetComponent<Animation>();
		if (animation == null)
		{
			animation = this.opposingSideDeck.gameObject.AddComponent<Animation>();
		}
		animation.AddClip(this.shuffleDeck, "shuffleDeckAnim");
		animation.Play("shuffleDeckAnim");
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x000EB96C File Offset: 0x000E9B6C
	private void SlideCard(GameObject topCard)
	{
		iTween.MoveTo(topCard, iTween.Hash(new object[]
		{
			"position",
			new Vector3(topCard.transform.position.x - 0.5f, topCard.transform.position.y, topCard.transform.position.z),
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.linear
		}));
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x000EB9FC File Offset: 0x000E9BFC
	private IEnumerator SampleAnimFrame(Animation animToUse, string animName, float startSec)
	{
		AnimationState state = animToUse[animName];
		state.enabled = true;
		state.time = startSec;
		animToUse.Play(animName);
		yield return null;
		state.enabled = false;
		yield break;
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x000EBA19 File Offset: 0x000E9C19
	private void SortHand(Zone zone)
	{
		zone.GetCards().Sort(new Comparison<global::Card>(Zone.CardSortComparison));
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x000EBA32 File Offset: 0x000E9C32
	private IEnumerator ShrinkStartingHandBanner(GameObject banner)
	{
		yield return new WaitForSeconds(4f);
		if (banner == null)
		{
			yield break;
		}
		iTween.ScaleTo(banner, new Vector3(0f, 0f, 0f), 0.5f);
		yield return new WaitForSeconds(0.5f);
		UnityEngine.Object.Destroy(banner);
		yield break;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x000EBA44 File Offset: 0x000E9C44
	private void FadeHeroPowerIn(global::Card heroPowerCard)
	{
		if (heroPowerCard == null)
		{
			return;
		}
		Actor actor = heroPowerCard.GetActor();
		if (actor == null)
		{
			return;
		}
		actor.TurnOnCollider();
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x000EBA74 File Offset: 0x000E9C74
	private void LoadMyHeroSkinSocketInEffect(Actor myHero)
	{
		if (string.IsNullOrEmpty(myHero.SocketInEffectFriendly) && !UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (string.IsNullOrEmpty(myHero.SocketInEffectFriendlyPhone) && UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_isLoadingMyCustomSocketIn = true;
		string input = myHero.SocketInEffectFriendly;
		if (UniversalInputManager.UsePhoneUI)
		{
			input = myHero.SocketInEffectFriendlyPhone;
		}
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnMyHeroSkinSocketInEffectLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x000EBAF8 File Offset: 0x000E9CF8
	private void OnMyHeroSkinSocketInEffectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Failed to load My custom hero socket in effect!");
			this.m_isLoadingMyCustomSocketIn = false;
			return;
		}
		go.transform.position = Board.Get().FindBone("CustomSocketIn_Friendly").position;
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Debug.LogError("Faild to locate Spell on custom socket in effect!");
			this.m_isLoadingMyCustomSocketIn = false;
			return;
		}
		this.m_MyCustomSocketInSpell = component;
		if (this.m_MyCustomSocketInSpell.HasUsableState(SpellStateType.IDLE))
		{
			this.m_MyCustomSocketInSpell.ActivateState(SpellStateType.IDLE);
		}
		else
		{
			this.m_MyCustomSocketInSpell.gameObject.SetActive(false);
		}
		this.m_isLoadingMyCustomSocketIn = false;
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x000EBB9C File Offset: 0x000E9D9C
	private void LoadHisHeroSkinSocketInEffect(Actor hisHero)
	{
		if (string.IsNullOrEmpty(hisHero.SocketInEffectOpponent) && !UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (string.IsNullOrEmpty(hisHero.SocketInEffectOpponentPhone) && UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_isLoadingHisCustomSocketIn = true;
		string input = hisHero.SocketInEffectOpponent;
		if (UniversalInputManager.UsePhoneUI)
		{
			input = hisHero.SocketInEffectOpponentPhone;
		}
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnHisHeroSkinSocketInEffectLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x000EBC20 File Offset: 0x000E9E20
	private void OnHisHeroSkinSocketInEffectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Failed to load His custom hero socket in effect!");
			this.m_isLoadingHisCustomSocketIn = false;
			return;
		}
		go.transform.position = Board.Get().FindBone("CustomSocketIn_Opposing").position;
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Debug.LogError("Faild to locate Spell on custom socket in effect!");
			this.m_isLoadingHisCustomSocketIn = false;
			return;
		}
		this.m_HisCustomSocketInSpell = component;
		if (this.m_HisCustomSocketInSpell.HasUsableState(SpellStateType.IDLE))
		{
			this.m_HisCustomSocketInSpell.ActivateState(SpellStateType.IDLE);
		}
		else
		{
			this.m_HisCustomSocketInSpell.gameObject.SetActive(false);
		}
		this.m_isLoadingHisCustomSocketIn = false;
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x000EBCC4 File Offset: 0x000E9EC4
	private void DestoryHeroSkinSocketInEffects()
	{
		if (this.m_MyCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_MyCustomSocketInSpell.gameObject);
		}
		if (this.m_HisCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_HisCustomSocketInSpell.gameObject);
		}
	}

	// Token: 0x06002E46 RID: 11846 RVA: 0x000EBD04 File Offset: 0x000E9F04
	private void OnFakeHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnFakeHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef), Array.Empty<object>());
			this.pendingFakeHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnFakeHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef), Array.Empty<object>());
			this.pendingFakeHeroCount--;
			return;
		}
		((List<Actor>)callbackData).Add(component);
		component.SetUnlit();
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer, null);
		component.GetMeshRenderer(false).gameObject.layer = 8;
		GameState.Get().GetGameEntity().ConfigureFakeMulliganCardActor(component, true);
		if (this.m_startingCards.Count > 0)
		{
			component.gameObject.transform.position = new Vector3(this.m_startingCards[0].transform.position.x, this.m_startingCards[0].transform.position.y, this.m_startingCards[0].transform.position.z);
		}
		this.pendingFakeHeroCount--;
	}

	// Token: 0x06002E47 RID: 11847 RVA: 0x000EBE54 File Offset: 0x000EA054
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef), Array.Empty<object>());
			this.pendingHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef), Array.Empty<object>());
			this.pendingHeroCount--;
			return;
		}
		global::Card card = (global::Card)callbackData;
		component.SetCard(card);
		component.SetCardDefFromCard(card);
		component.SetPremium(card.GetPremium());
		component.UpdateAllComponents();
		if (card.GetActor() != null)
		{
			card.GetActor().Destroy();
		}
		card.SetActor(component);
		component.SetEntity(card.GetEntity());
		component.UpdateAllComponents();
		component.SetUnlit();
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer, null);
		component.GetMeshRenderer(false).gameObject.layer = 8;
		component.GetHealthObject().Hide();
		GameState.Get().GetGameEntity().ApplyMulliganActorStateChanges(component);
		this.choiceHeroActors.Add(card, component);
		this.pendingHeroCount--;
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x000EBF94 File Offset: 0x000EA194
	private void OnOpponentHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnOpponentHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef), Array.Empty<object>());
			this.pendingHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning(string.Format("MulliganManager.OnOpponentHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef), Array.Empty<object>());
			this.pendingHeroCount--;
			return;
		}
		global::Card card = (global::Card)callbackData;
		component.SetCard(card);
		component.SetCardDefFromCard(card);
		component.SetPremium(card.GetPremium());
		component.UpdateAllComponents();
		if (card.GetActor() != null)
		{
			card.GetActor().Destroy();
		}
		card.SetActor(component);
		component.SetEntity(card.GetEntity());
		component.UpdateAllComponents();
		component.SetUnlit();
		component.transform.localPosition = new Vector3(component.transform.localPosition.x + 1000f, component.transform.localPosition.y, component.transform.localPosition.z);
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer, null);
		UnityEngine.Object.Destroy(component.m_healthObject);
		UnityEngine.Object.Destroy(component.m_attackObject);
		GameState.Get().GetGameEntity().ApplyMulliganActorLobbyStateChanges(component);
		this.opponentHeroActors.Add(card, component);
		this.pendingHeroCount--;
	}

	// Token: 0x04001953 RID: 6483
	public AnimationClip cardAnimatesFromBoardToDeck;

	// Token: 0x04001954 RID: 6484
	public AnimationClip cardAnimatesFromBoardToDeck_iPhone;

	// Token: 0x04001955 RID: 6485
	public AnimationClip cardAnimatesFromTableToSky;

	// Token: 0x04001956 RID: 6486
	public AnimationClip cardAnimatesFromDeckToBoard;

	// Token: 0x04001957 RID: 6487
	public AnimationClip shuffleDeck;

	// Token: 0x04001958 RID: 6488
	public AnimationClip myheroAnimatesToPosition;

	// Token: 0x04001959 RID: 6489
	public AnimationClip hisheroAnimatesToPosition;

	// Token: 0x0400195A RID: 6490
	public AnimationClip myheroAnimatesToPosition_iPhone;

	// Token: 0x0400195B RID: 6491
	public AnimationClip hisheroAnimatesToPosition_iPhone;

	// Token: 0x0400195C RID: 6492
	public GameObject coinPrefab;

	// Token: 0x0400195D RID: 6493
	public GameObject weldPrefab;

	// Token: 0x0400195E RID: 6494
	public GameObject mulliganChooseBannerPrefab;

	// Token: 0x0400195F RID: 6495
	public GameObject mulliganDetailLabelPrefab;

	// Token: 0x04001960 RID: 6496
	public GameObject mulliganKeepLabelPrefab;

	// Token: 0x04001961 RID: 6497
	public MulliganReplaceLabel mulliganReplaceLabelPrefab;

	// Token: 0x04001962 RID: 6498
	public GameObject mulliganXlabelPrefab;

	// Token: 0x04001963 RID: 6499
	public GameObject mulliganTimerPrefab;

	// Token: 0x04001964 RID: 6500
	public GameObject heroLabelPrefab;

	// Token: 0x04001965 RID: 6501
	public MulliganButton mulliganButtonWidget;

	// Token: 0x04001966 RID: 6502
	public UberText conditionalHelperTextLabel;

	// Token: 0x04001967 RID: 6503
	private const float PHONE_HEIGHT_OFFSET = 7f;

	// Token: 0x04001968 RID: 6504
	private const float PHONE_CARD_Z_OFFSET = 0.2f;

	// Token: 0x04001969 RID: 6505
	private const float PHONE_CARD_SCALE = 0.9f;

	// Token: 0x0400196A RID: 6506
	private const float PHONE_ZONE_SIZE_ADJUST = 0.55f;

	// Token: 0x0400196B RID: 6507
	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_X = -7.7726f;

	// Token: 0x0400196C RID: 6508
	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_Y = 0.0055918f;

	// Token: 0x0400196D RID: 6509
	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_Z = -8.054f;

	// Token: 0x0400196E RID: 6510
	public const float BATTLEGROUNDS_HERO_ENDING_SCALE = 1.134f;

	// Token: 0x0400196F RID: 6511
	public static readonly PlatformDependentValue<Vector3> FRIENDLY_PLAYER_CARD_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(1.1f, 0.28f, 1.1f),
		Phone = new Vector3(0.9f, 0.28f, 0.9f)
	};

	// Token: 0x04001970 RID: 6512
	private static MulliganManager s_instance;

	// Token: 0x04001971 RID: 6513
	private bool mulliganActive;

	// Token: 0x04001972 RID: 6514
	private MulliganTimer m_mulliganTimer;

	// Token: 0x04001973 RID: 6515
	private NormalButton mulliganButton;

	// Token: 0x04001974 RID: 6516
	private GameObject myWeldEffect;

	// Token: 0x04001975 RID: 6517
	private GameObject hisWeldEffect;

	// Token: 0x04001976 RID: 6518
	private GameObject coinObject;

	// Token: 0x04001977 RID: 6519
	private GameObject startingHandZone;

	// Token: 0x04001978 RID: 6520
	private GameObject coinTossText;

	// Token: 0x04001979 RID: 6521
	private ZoneHand friendlySideHandZone;

	// Token: 0x0400197A RID: 6522
	private ZoneHand opposingSideHandZone;

	// Token: 0x0400197B RID: 6523
	private ZoneDeck friendlySideDeck;

	// Token: 0x0400197C RID: 6524
	private ZoneDeck opposingSideDeck;

	// Token: 0x0400197D RID: 6525
	private Actor myHeroCardActor;

	// Token: 0x0400197E RID: 6526
	private Actor hisHeroCardActor;

	// Token: 0x0400197F RID: 6527
	private Actor myHeroPowerCardActor;

	// Token: 0x04001980 RID: 6528
	private Actor hisHeroPowerCardActor;

	// Token: 0x04001981 RID: 6529
	private Map<global::Card, Actor> opponentHeroActors = new Map<global::Card, Actor>();

	// Token: 0x04001982 RID: 6530
	private Map<global::Card, Actor> choiceHeroActors = new Map<global::Card, Actor>();

	// Token: 0x04001983 RID: 6531
	private List<Actor> fakeCardsOnLeft = new List<Actor>();

	// Token: 0x04001984 RID: 6532
	private List<Actor> fakeCardsOnRight = new List<Actor>();

	// Token: 0x04001985 RID: 6533
	private bool waitingForVersusText;

	// Token: 0x04001986 RID: 6534
	private GameStartVsLetters versusText;

	// Token: 0x04001987 RID: 6535
	private bool waitingForVersusVo;

	// Token: 0x04001988 RID: 6536
	private AudioSource versusVo;

	// Token: 0x04001989 RID: 6537
	private bool introComplete;

	// Token: 0x0400198A RID: 6538
	private bool skipCardChoosing;

	// Token: 0x0400198B RID: 6539
	private List<global::Card> m_startingCards;

	// Token: 0x0400198C RID: 6540
	private List<global::Card> m_startingOppCards;

	// Token: 0x0400198D RID: 6541
	private int m_coinCardIndex = -1;

	// Token: 0x0400198E RID: 6542
	private int m_bonusCardIndex = -1;

	// Token: 0x0400198F RID: 6543
	private GameObject mulliganChooseBanner;

	// Token: 0x04001990 RID: 6544
	private GameObject mulliganDetailLabel;

	// Token: 0x04001991 RID: 6545
	private List<MulliganReplaceLabel> m_replaceLabels;

	// Token: 0x04001992 RID: 6546
	private GameObject[] m_xLabels;

	// Token: 0x04001993 RID: 6547
	private bool[] m_handCardsMarkedForReplace = new bool[4];

	// Token: 0x04001994 RID: 6548
	private Vector3 coinLocation;

	// Token: 0x04001995 RID: 6549
	private bool friendlyPlayerGoesFirst;

	// Token: 0x04001996 RID: 6550
	private HeroLabel myheroLabel;

	// Token: 0x04001997 RID: 6551
	private HeroLabel hisheroLabel;

	// Token: 0x04001998 RID: 6552
	private Spell m_MyCustomSocketInSpell;

	// Token: 0x04001999 RID: 6553
	private Spell m_HisCustomSocketInSpell;

	// Token: 0x0400199A RID: 6554
	private bool m_isLoadingMyCustomSocketIn;

	// Token: 0x0400199B RID: 6555
	private bool m_isLoadingHisCustomSocketIn;

	// Token: 0x0400199C RID: 6556
	private int pendingHeroCount;

	// Token: 0x0400199D RID: 6557
	private int pendingFakeHeroCount;

	// Token: 0x0400199E RID: 6558
	public static readonly float ANIMATION_TIME_DEAL_CARD = 1.5f;

	// Token: 0x0400199F RID: 6559
	public static readonly float DEFAULT_STARTING_TAUNT_DURATION = 2.5f;

	// Token: 0x040019A0 RID: 6560
	private bool friendlyPlayerHasReplacementCards;

	// Token: 0x040019A1 RID: 6561
	private bool opponentPlayerHasReplacementCards;

	// Token: 0x040019A2 RID: 6562
	private bool m_waitingForUserInput;

	// Token: 0x040019A3 RID: 6563
	private Notification innkeeperMulliganDialog;

	// Token: 0x040019A4 RID: 6564
	private bool m_resuming;

	// Token: 0x040019A5 RID: 6565
	private Coroutine m_customIntroCoroutine;

	// Token: 0x040019A6 RID: 6566
	private IEnumerator m_DimLightsOnceBoardLoads;

	// Token: 0x040019A7 RID: 6567
	private IEnumerator m_WaitForBoardThenLoadButton;

	// Token: 0x040019A8 RID: 6568
	private IEnumerator m_WaitForHeroesAndStartAnimations;

	// Token: 0x040019A9 RID: 6569
	private IEnumerator m_ResumeMulligan;

	// Token: 0x040019AA RID: 6570
	private IEnumerator m_DealStartingCards;

	// Token: 0x040019AB RID: 6571
	private IEnumerator m_ShowMultiplayerWaitingArea;

	// Token: 0x040019AC RID: 6572
	private IEnumerator m_RemoveOldCardsAnimation;

	// Token: 0x040019AD RID: 6573
	private IEnumerator m_PlayStartingTaunts;

	// Token: 0x040019AE RID: 6574
	private IEnumerator m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen;

	// Token: 0x040019AF RID: 6575
	private IEnumerator m_ContinueMulliganWhenBoardLoads;

	// Token: 0x040019B0 RID: 6576
	private IEnumerator m_WaitAFrameBeforeSendingEventToMulliganButton;

	// Token: 0x040019B1 RID: 6577
	private IEnumerator m_ShrinkStartingHandBanner;

	// Token: 0x040019B2 RID: 6578
	private IEnumerator m_AnimateCoinTossText;

	// Token: 0x040019B3 RID: 6579
	private IEnumerator m_UpdateChooseBanner;

	// Token: 0x040019B4 RID: 6580
	private IEnumerator m_RemoveUIButtons;

	// Token: 0x040019B5 RID: 6581
	private IEnumerator m_WaitForOpponentToFinishMulligan;

	// Token: 0x040019B6 RID: 6582
	private IEnumerator m_EndMulliganWithTiming;

	// Token: 0x040019B7 RID: 6583
	private IEnumerator m_HandleCoinCard;

	// Token: 0x040019B8 RID: 6584
	private IEnumerator m_EnableHandCollidersAfterCardsAreDealt;

	// Token: 0x040019B9 RID: 6585
	private IEnumerator m_SkipMulliganForResume;

	// Token: 0x040019BA RID: 6586
	private IEnumerator m_SkipMulliganWhenIntroComplete;

	// Token: 0x040019BB RID: 6587
	private IEnumerator m_WaitForBoardAnimToCompleteThenStartTurn;
}
