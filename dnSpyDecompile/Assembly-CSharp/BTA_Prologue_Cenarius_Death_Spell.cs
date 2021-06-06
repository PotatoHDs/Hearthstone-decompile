using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000507 RID: 1287
public class BTA_Prologue_Cenarius_Death_Spell : Spell
{
	// Token: 0x0600455A RID: 17754 RVA: 0x00177330 File Offset: 0x00175530
	public override bool AddPowerTargets()
	{
		base.AddPowerTargets();
		if (this.m_missionEntity == null)
		{
			this.m_missionEntity = (GameState.Get().GetGameEntity() as BTA_Prologue_Fight_04);
			if (this.m_missionEntity == null)
			{
				Log.Spells.PrintError("BTA_Prologue_Cenarius_Death_Spell.AddPowerTargets(): GameEntity is not an instance of BTA_Prologue_Fight_04!", Array.Empty<object>());
			}
		}
		this.FindHeroCards();
		return true;
	}

	// Token: 0x0600455B RID: 17755 RVA: 0x00177384 File Offset: 0x00175584
	private void FindHeroCards()
	{
		if (this.m_CenariusCard == null)
		{
			this.m_CenariusCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		}
	}

	// Token: 0x0600455C RID: 17756 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	// Token: 0x0600455D RID: 17757 RVA: 0x001773A9 File Offset: 0x001755A9
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoEffects());
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x001773BF File Offset: 0x001755BF
	private IEnumerator DoEffects()
	{
		if (this.m_fakeDeathState == BTA_Prologue_Cenarius_Death_Spell.FakeDeathState.EXPLODING_CENARIUS)
		{
			yield return base.StartCoroutine(this.ExplodeCenarius());
		}
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x001773CE File Offset: 0x001755CE
	private IEnumerator ExplodeCenarius()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Card EnemyHeroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		EnemyHeroCard.ActivateCharacterDeathEffects();
		this.m_explodeReformSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_ExplodeReformSpell);
		SpellUtils.SetCustomSpellParent(this.m_explodeReformSpellInstance, EnemyHeroCard.GetActor());
		this.m_explodeReformSpellInstance.ActivateState(SpellStateType.ACTION);
		while (this.m_explodeReformSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		base.StartCoroutine(this.m_missionEntity.PlayVictoryLines());
		yield return new WaitForSeconds(10f);
		this.m_explodeReformSpellInstance.ActivateState(SpellStateType.DEATH);
		while (!this.m_explodeReformSpellInstance.IsFinished())
		{
			yield return null;
		}
		EnemyHeroCard.ShowCard();
		yield break;
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x001773DD File Offset: 0x001755DD
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

	// Token: 0x04003830 RID: 14384
	public Spell m_ExplodeReformSpell;

	// Token: 0x04003831 RID: 14385
	private Card m_CenariusCard;

	// Token: 0x04003832 RID: 14386
	private Spell m_explodeReformSpellInstance;

	// Token: 0x04003833 RID: 14387
	private BTA_Prologue_Fight_04 m_missionEntity;

	// Token: 0x04003834 RID: 14388
	private BTA_Prologue_Cenarius_Death_Spell.FakeDeathState m_fakeDeathState;

	// Token: 0x02001BC9 RID: 7113
	private enum FakeDeathState
	{
		// Token: 0x0400C7BA RID: 51130
		EXPLODING_CENARIUS
	}
}
