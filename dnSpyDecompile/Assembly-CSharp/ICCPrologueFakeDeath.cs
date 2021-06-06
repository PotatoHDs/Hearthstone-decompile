using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FC RID: 2044
public class ICCPrologueFakeDeath : Spell
{
	// Token: 0x06006F07 RID: 28423 RVA: 0x0023C4F0 File Offset: 0x0023A6F0
	public override bool AddPowerTargets()
	{
		base.AddPowerTargets();
		if (this.m_missionEntity == null)
		{
			this.m_missionEntity = (GameState.Get().GetGameEntity() as ICC_01_LICHKING);
			if (this.m_missionEntity == null)
			{
				Log.Spells.PrintError("ICCPrologueFakeDeath.AddPowerTargets(): GameEntity is not an instance of ICC_01_LICHKING!", Array.Empty<object>());
			}
		}
		this.FindHeroCards();
		return true;
	}

	// Token: 0x06006F08 RID: 28424 RVA: 0x0023C544 File Offset: 0x0023A744
	private void FindHeroCards()
	{
		if (this.m_lichKingCard == null)
		{
			this.m_lichKingCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		}
		if (this.m_frostLichJainaCard == null)
		{
			List<PowerTask> taskList = this.m_taskList.GetTaskList();
			for (int i = 0; i < taskList.Count; i++)
			{
				Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
				if (histFullEntity != null)
				{
					Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
					if (entity.GetControllerSide() == Player.Side.FRIENDLY && entity.IsHero())
					{
						this.m_frostLichJainaCard = entity.GetCard();
						this.m_frostLichJainaEnterTaskIndex = i;
						break;
					}
				}
			}
		}
		if (this.m_tirionCard == null)
		{
			List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
			for (int j = 0; j < taskList2.Count; j++)
			{
				Network.HistTagChange histTagChange = taskList2[j].GetPower() as Network.HistTagChange;
				if (histTagChange != null)
				{
					Entity entity2 = GameState.Get().GetEntity(histTagChange.Entity);
					if (entity2.GetControllerSide() == Player.Side.OPPOSING && entity2.IsHero() && histTagChange.Tag == 262)
					{
						this.m_tirionCard = entity2.GetCard();
						this.m_tirionEnterTaskIndex = j;
						return;
					}
				}
			}
		}
	}

	// Token: 0x06006F09 RID: 28425 RVA: 0x0023C687 File Offset: 0x0023A887
	public override bool CanPurge()
	{
		return this.m_fakeDeathState == ICCPrologueFakeDeath.FakeDeathState.COMPLETE && base.CanPurge();
	}

	// Token: 0x06006F0A RID: 28426 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	// Token: 0x06006F0B RID: 28427 RVA: 0x0023C69A File Offset: 0x0023A89A
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoEffects());
	}

	// Token: 0x06006F0C RID: 28428 RVA: 0x0023C6B0 File Offset: 0x0023A8B0
	private IEnumerator DoEffects()
	{
		if (this.m_fakeDeathState == ICCPrologueFakeDeath.FakeDeathState.EXPLODING_JAINA)
		{
			yield return base.StartCoroutine(this.ExplodeJaina());
		}
		if (this.m_fakeDeathState == ICCPrologueFakeDeath.FakeDeathState.FROST_LICH_JAINA_ENTER)
		{
			yield return base.StartCoroutine(this.FrostJainaEnter());
		}
		if (this.m_fakeDeathState == ICCPrologueFakeDeath.FakeDeathState.LICH_KING_EXIT)
		{
			yield return base.StartCoroutine(this.LichKingExit());
		}
		if (this.m_fakeDeathState == ICCPrologueFakeDeath.FakeDeathState.TIRION_ENTER)
		{
			yield return base.StartCoroutine(this.TirionEnter());
		}
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006F0D RID: 28429 RVA: 0x0023C6BF File Offset: 0x0023A8BF
	private IEnumerator ExplodeJaina()
	{
		EndTurnButton.Get().AddInputBlocker();
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		SoundManager.Get().LoadAndPlay("defeat_jingle.prefab:0744a10f38e92f1438a02349c29a7b76");
		base.StartCoroutine(this.HideBoardElements());
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		heroCard.ActivateCharacterDeathEffects();
		this.m_explodeReformSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_ExplodeReformSpell);
		SpellUtils.SetCustomSpellParent(this.m_explodeReformSpellInstance, heroCard.GetActor());
		this.m_explodeReformSpellInstance.ActivateState(SpellStateType.ACTION);
		while (this.m_explodeReformSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		this.m_fakeDeathState = ICCPrologueFakeDeath.FakeDeathState.FROST_LICH_JAINA_ENTER;
		yield break;
	}

	// Token: 0x06006F0E RID: 28430 RVA: 0x0023C6CE File Offset: 0x0023A8CE
	private IEnumerator FrostJainaEnter()
	{
		if (this.m_frostLichJainaCard == null)
		{
			yield break;
		}
		this.m_taskList.DoTasks(0, this.m_frostLichJainaEnterTaskIndex);
		GameObject fakeDefeatScreenInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_FakeDefeatScreen);
		DefeatTwoScoop defeatTwoScoop = fakeDefeatScreenInstance.GetComponentInChildren<DefeatTwoScoop>(true);
		while (!defeatTwoScoop.IsLoaded())
		{
			yield return null;
		}
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
		defeatTwoScoop.Show(false);
		yield return new WaitForSeconds(this.m_FakeDefeatScreenShowTime);
		FullScreenFXMgr.Get().StopBlur(0.25f, iTween.EaseType.linear, null, false);
		FullScreenFXMgr.Get().SetBlurDesaturation(0f);
		defeatTwoScoop.Hide();
		this.m_taskList.DoTasks(0, this.m_frostLichJainaEnterTaskIndex + 1);
		while (this.m_frostLichJainaCard.GetActor() == null || this.m_frostLichJainaCard.IsActorLoading())
		{
			yield return null;
		}
		this.m_frostLichJainaCard.HideCard();
		this.m_explodeReformSpellInstance.ActivateState(SpellStateType.DEATH);
		if (this.m_missionEntity != null)
		{
			base.StartCoroutine(this.m_missionEntity.PlayLichKingRezLines());
		}
		while (!this.m_explodeReformSpellInstance.IsFinished())
		{
			yield return null;
		}
		this.m_frostLichJainaCard.ShowCard();
		this.m_frostLichJainaCard.GetActor().GetAttackObject().Hide();
		while (this.m_explodeReformSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(fakeDefeatScreenInstance);
		this.m_fakeDeathState = ICCPrologueFakeDeath.FakeDeathState.LICH_KING_EXIT;
		yield break;
	}

	// Token: 0x06006F0F RID: 28431 RVA: 0x0023C6DD File Offset: 0x0023A8DD
	private IEnumerator LichKingExit()
	{
		Spell lichKingExitSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_LichKingExitSpell);
		SpellUtils.SetCustomSpellParent(lichKingExitSpellInstance, this.m_lichKingCard.GetActor());
		lichKingExitSpellInstance.Activate();
		while (lichKingExitSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		yield return new WaitForSeconds(this.m_TirionEnterDelay);
		this.m_fakeDeathState = ICCPrologueFakeDeath.FakeDeathState.TIRION_ENTER;
		yield break;
	}

	// Token: 0x06006F10 RID: 28432 RVA: 0x0023C6EC File Offset: 0x0023A8EC
	private IEnumerator TirionEnter()
	{
		if (this.m_tirionCard == null)
		{
			yield break;
		}
		this.m_taskList.DoTasks(0, this.m_tirionEnterTaskIndex + 1);
		this.m_tirionCard.SetDoNotSort(true);
		this.m_tirionCard.SetDoNotWarpToNewZone(true);
		while (this.m_tirionCard.GetActor() == null || this.m_tirionCard.IsActorLoading())
		{
			yield return null;
		}
		TransformUtil.CopyWorld(this.m_tirionCard, this.m_tirionCard.GetZone().transform);
		this.m_tirionCard.GetActor().Hide();
		Spell tirionEnterSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_TirionEnterSpell);
		SpellUtils.SetCustomSpellParent(tirionEnterSpellInstance, this.m_tirionCard.GetActor());
		tirionEnterSpellInstance.Activate();
		while (tirionEnterSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		this.m_tirionCard.SetDoNotSort(false);
		this.m_tirionCard.SetDoNotWarpToNewZone(false);
		NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
		nameBannerForSide.UpdateHeroNameBanner();
		nameBannerForSide.UpdateSubtext();
		this.m_missionEntity.StartGameplaySoundtracks();
		EndTurnButton.Get().RemoveInputBlocker();
		this.m_fakeDeathState = ICCPrologueFakeDeath.FakeDeathState.COMPLETE;
		yield break;
	}

	// Token: 0x06006F11 RID: 28433 RVA: 0x0023C6FB File Offset: 0x0023A8FB
	private IEnumerator HideBoardElements()
	{
		yield return new WaitForSeconds(0.5f);
		Player controller = GameState.Get().GetFriendlySidePlayer();
		if (controller.GetHeroPowerCard() != null)
		{
			controller.GetHeroPowerCard().HideCard();
			controller.GetHeroPowerCard().GetActor().ToggleForceIdle(true);
			controller.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
		}
		if (controller.GetWeaponCard() != null)
		{
			controller.GetWeaponCard().HideCard();
			controller.GetWeaponCard().GetActor().ToggleForceIdle(true);
			controller.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
		Actor actor = controller.GetHeroCard().GetActor();
		actor.HideArmorSpell();
		actor.GetHealthObject().Hide();
		actor.GetAttackObject().Hide();
		actor.ToggleForceIdle(true);
		actor.SetActorState(ActorStateType.CARD_IDLE);
		yield return new WaitForSeconds(3f);
		Player firstOpponentPlayer = GameState.Get().GetFirstOpponentPlayer(controller);
		if (firstOpponentPlayer.GetHeroPowerCard() != null)
		{
			firstOpponentPlayer.GetHeroPowerCard().HideCard();
			firstOpponentPlayer.GetHeroPowerCard().GetActor().ToggleForceIdle(true);
			firstOpponentPlayer.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			firstOpponentPlayer.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
		}
		if (firstOpponentPlayer.GetWeaponCard() != null)
		{
			firstOpponentPlayer.GetWeaponCard().HideCard();
			firstOpponentPlayer.GetWeaponCard().GetActor().ToggleForceIdle(true);
			firstOpponentPlayer.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			firstOpponentPlayer.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
		yield break;
	}

	// Token: 0x0400590B RID: 22795
	public Spell m_ExplodeReformSpell;

	// Token: 0x0400590C RID: 22796
	public Spell m_LichKingExitSpell;

	// Token: 0x0400590D RID: 22797
	public Spell m_TirionEnterSpell;

	// Token: 0x0400590E RID: 22798
	public GameObject m_FakeDefeatScreen;

	// Token: 0x0400590F RID: 22799
	public float m_FakeDefeatScreenShowTime = 5f;

	// Token: 0x04005910 RID: 22800
	public float m_TirionEnterDelay = 2f;

	// Token: 0x04005911 RID: 22801
	private Card m_lichKingCard;

	// Token: 0x04005912 RID: 22802
	private Card m_tirionCard;

	// Token: 0x04005913 RID: 22803
	private Card m_frostLichJainaCard;

	// Token: 0x04005914 RID: 22804
	private int m_tirionEnterTaskIndex;

	// Token: 0x04005915 RID: 22805
	private int m_frostLichJainaEnterTaskIndex;

	// Token: 0x04005916 RID: 22806
	private Spell m_explodeReformSpellInstance;

	// Token: 0x04005917 RID: 22807
	private ICC_01_LICHKING m_missionEntity;

	// Token: 0x04005918 RID: 22808
	private ICCPrologueFakeDeath.FakeDeathState m_fakeDeathState;

	// Token: 0x020023AC RID: 9132
	private enum FakeDeathState
	{
		// Token: 0x0400E7A4 RID: 59300
		EXPLODING_JAINA,
		// Token: 0x0400E7A5 RID: 59301
		FROST_LICH_JAINA_ENTER,
		// Token: 0x0400E7A6 RID: 59302
		LICH_KING_EXIT,
		// Token: 0x0400E7A7 RID: 59303
		TIRION_ENTER,
		// Token: 0x0400E7A8 RID: 59304
		COMPLETE
	}
}
