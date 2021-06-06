using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICCPrologueFakeDeath : Spell
{
	private enum FakeDeathState
	{
		EXPLODING_JAINA,
		FROST_LICH_JAINA_ENTER,
		LICH_KING_EXIT,
		TIRION_ENTER,
		COMPLETE
	}

	public Spell m_ExplodeReformSpell;

	public Spell m_LichKingExitSpell;

	public Spell m_TirionEnterSpell;

	public GameObject m_FakeDefeatScreen;

	public float m_FakeDefeatScreenShowTime = 5f;

	public float m_TirionEnterDelay = 2f;

	private Card m_lichKingCard;

	private Card m_tirionCard;

	private Card m_frostLichJainaCard;

	private int m_tirionEnterTaskIndex;

	private int m_frostLichJainaEnterTaskIndex;

	private Spell m_explodeReformSpellInstance;

	private ICC_01_LICHKING m_missionEntity;

	private FakeDeathState m_fakeDeathState;

	public override bool AddPowerTargets()
	{
		base.AddPowerTargets();
		if (m_missionEntity == null)
		{
			m_missionEntity = GameState.Get().GetGameEntity() as ICC_01_LICHKING;
			if (m_missionEntity == null)
			{
				Log.Spells.PrintError("ICCPrologueFakeDeath.AddPowerTargets(): GameEntity is not an instance of ICC_01_LICHKING!");
			}
		}
		FindHeroCards();
		return true;
	}

	private void FindHeroCards()
	{
		if (m_lichKingCard == null)
		{
			m_lichKingCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		}
		if (m_frostLichJainaCard == null)
		{
			List<PowerTask> taskList = m_taskList.GetTaskList();
			for (int i = 0; i < taskList.Count; i++)
			{
				Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
				if (histFullEntity != null)
				{
					Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
					if (entity.GetControllerSide() == Player.Side.FRIENDLY && entity.IsHero())
					{
						m_frostLichJainaCard = entity.GetCard();
						m_frostLichJainaEnterTaskIndex = i;
						break;
					}
				}
			}
		}
		if (!(m_tirionCard == null))
		{
			return;
		}
		List<PowerTask> taskList2 = m_taskList.GetTaskList();
		for (int j = 0; j < taskList2.Count; j++)
		{
			Network.HistTagChange histTagChange = taskList2[j].GetPower() as Network.HistTagChange;
			if (histTagChange != null)
			{
				Entity entity2 = GameState.Get().GetEntity(histTagChange.Entity);
				if (entity2.GetControllerSide() == Player.Side.OPPOSING && entity2.IsHero() && histTagChange.Tag == 262)
				{
					m_tirionCard = entity2.GetCard();
					m_tirionEnterTaskIndex = j;
					break;
				}
			}
		}
	}

	public override bool CanPurge()
	{
		if (m_fakeDeathState != FakeDeathState.COMPLETE)
		{
			return false;
		}
		return base.CanPurge();
	}

	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(DoEffects());
	}

	private IEnumerator DoEffects()
	{
		if (m_fakeDeathState == FakeDeathState.EXPLODING_JAINA)
		{
			yield return StartCoroutine(ExplodeJaina());
		}
		if (m_fakeDeathState == FakeDeathState.FROST_LICH_JAINA_ENTER)
		{
			yield return StartCoroutine(FrostJainaEnter());
		}
		if (m_fakeDeathState == FakeDeathState.LICH_KING_EXIT)
		{
			yield return StartCoroutine(LichKingExit());
		}
		if (m_fakeDeathState == FakeDeathState.TIRION_ENTER)
		{
			yield return StartCoroutine(TirionEnter());
		}
		OnSpellFinished();
		OnStateFinished();
	}

	private IEnumerator ExplodeJaina()
	{
		EndTurnButton.Get().AddInputBlocker();
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		SoundManager.Get().LoadAndPlay("defeat_jingle.prefab:0744a10f38e92f1438a02349c29a7b76");
		StartCoroutine(HideBoardElements());
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		heroCard.ActivateCharacterDeathEffects();
		m_explodeReformSpellInstance = Object.Instantiate(m_ExplodeReformSpell);
		SpellUtils.SetCustomSpellParent(m_explodeReformSpellInstance, heroCard.GetActor());
		m_explodeReformSpellInstance.ActivateState(SpellStateType.ACTION);
		while (m_explodeReformSpellInstance.GetActiveState() != 0)
		{
			yield return null;
		}
		m_fakeDeathState = FakeDeathState.FROST_LICH_JAINA_ENTER;
	}

	private IEnumerator FrostJainaEnter()
	{
		if (!(m_frostLichJainaCard == null))
		{
			m_taskList.DoTasks(0, m_frostLichJainaEnterTaskIndex);
			GameObject fakeDefeatScreenInstance = Object.Instantiate(m_FakeDefeatScreen);
			DefeatTwoScoop defeatTwoScoop = fakeDefeatScreenInstance.GetComponentInChildren<DefeatTwoScoop>(includeInactive: true);
			while (!defeatTwoScoop.IsLoaded())
			{
				yield return null;
			}
			FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
			FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc);
			defeatTwoScoop.Show(showXPBar: false);
			yield return new WaitForSeconds(m_FakeDefeatScreenShowTime);
			FullScreenFXMgr.Get().StopBlur(0.25f, iTween.EaseType.linear);
			FullScreenFXMgr.Get().SetBlurDesaturation(0f);
			defeatTwoScoop.Hide();
			m_taskList.DoTasks(0, m_frostLichJainaEnterTaskIndex + 1);
			while (m_frostLichJainaCard.GetActor() == null || m_frostLichJainaCard.IsActorLoading())
			{
				yield return null;
			}
			m_frostLichJainaCard.HideCard();
			m_explodeReformSpellInstance.ActivateState(SpellStateType.DEATH);
			if (m_missionEntity != null)
			{
				StartCoroutine(m_missionEntity.PlayLichKingRezLines());
			}
			while (!m_explodeReformSpellInstance.IsFinished())
			{
				yield return null;
			}
			m_frostLichJainaCard.ShowCard();
			m_frostLichJainaCard.GetActor().GetAttackObject().Hide();
			while (m_explodeReformSpellInstance.GetActiveState() != 0)
			{
				yield return null;
			}
			while (GameState.Get().IsBusy())
			{
				yield return null;
			}
			Object.Destroy(fakeDefeatScreenInstance);
			m_fakeDeathState = FakeDeathState.LICH_KING_EXIT;
		}
	}

	private IEnumerator LichKingExit()
	{
		Spell lichKingExitSpellInstance = Object.Instantiate(m_LichKingExitSpell);
		SpellUtils.SetCustomSpellParent(lichKingExitSpellInstance, m_lichKingCard.GetActor());
		lichKingExitSpellInstance.Activate();
		while (lichKingExitSpellInstance.GetActiveState() != 0)
		{
			yield return null;
		}
		yield return new WaitForSeconds(m_TirionEnterDelay);
		m_fakeDeathState = FakeDeathState.TIRION_ENTER;
	}

	private IEnumerator TirionEnter()
	{
		if (!(m_tirionCard == null))
		{
			m_taskList.DoTasks(0, m_tirionEnterTaskIndex + 1);
			m_tirionCard.SetDoNotSort(on: true);
			m_tirionCard.SetDoNotWarpToNewZone(on: true);
			while (m_tirionCard.GetActor() == null || m_tirionCard.IsActorLoading())
			{
				yield return null;
			}
			TransformUtil.CopyWorld(m_tirionCard, m_tirionCard.GetZone().transform);
			m_tirionCard.GetActor().Hide();
			Spell tirionEnterSpellInstance = Object.Instantiate(m_TirionEnterSpell);
			SpellUtils.SetCustomSpellParent(tirionEnterSpellInstance, m_tirionCard.GetActor());
			tirionEnterSpellInstance.Activate();
			while (tirionEnterSpellInstance.GetActiveState() != 0)
			{
				yield return null;
			}
			m_tirionCard.SetDoNotSort(on: false);
			m_tirionCard.SetDoNotWarpToNewZone(on: false);
			NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			nameBannerForSide.UpdateHeroNameBanner();
			nameBannerForSide.UpdateSubtext();
			m_missionEntity.StartGameplaySoundtracks();
			EndTurnButton.Get().RemoveInputBlocker();
			m_fakeDeathState = FakeDeathState.COMPLETE;
		}
	}

	private IEnumerator HideBoardElements()
	{
		yield return new WaitForSeconds(0.5f);
		Player controller = GameState.Get().GetFriendlySidePlayer();
		if (controller.GetHeroPowerCard() != null)
		{
			controller.GetHeroPowerCard().HideCard();
			controller.GetHeroPowerCard().GetActor().ToggleForceIdle(bOn: true);
			controller.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
		}
		if (controller.GetWeaponCard() != null)
		{
			controller.GetWeaponCard().HideCard();
			controller.GetWeaponCard().GetActor().ToggleForceIdle(bOn: true);
			controller.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
		Actor actor = controller.GetHeroCard().GetActor();
		actor.HideArmorSpell();
		actor.GetHealthObject().Hide();
		actor.GetAttackObject().Hide();
		actor.ToggleForceIdle(bOn: true);
		actor.SetActorState(ActorStateType.CARD_IDLE);
		yield return new WaitForSeconds(3f);
		Player firstOpponentPlayer = GameState.Get().GetFirstOpponentPlayer(controller);
		if (firstOpponentPlayer.GetHeroPowerCard() != null)
		{
			firstOpponentPlayer.GetHeroPowerCard().HideCard();
			firstOpponentPlayer.GetHeroPowerCard().GetActor().ToggleForceIdle(bOn: true);
			firstOpponentPlayer.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			firstOpponentPlayer.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
		}
		if (firstOpponentPlayer.GetWeaponCard() != null)
		{
			firstOpponentPlayer.GetWeaponCard().HideCard();
			firstOpponentPlayer.GetWeaponCard().GetActor().ToggleForceIdle(bOn: true);
			firstOpponentPlayer.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			firstOpponentPlayer.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
	}
}
