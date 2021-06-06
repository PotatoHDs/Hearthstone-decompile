using System.Collections;
using UnityEngine;

public class BTA_Prologue_Cenarius_Death_Spell : Spell
{
	private enum FakeDeathState
	{
		EXPLODING_CENARIUS
	}

	public Spell m_ExplodeReformSpell;

	private Card m_CenariusCard;

	private Spell m_explodeReformSpellInstance;

	private BTA_Prologue_Fight_04 m_missionEntity;

	private FakeDeathState m_fakeDeathState;

	public override bool AddPowerTargets()
	{
		base.AddPowerTargets();
		if (m_missionEntity == null)
		{
			m_missionEntity = GameState.Get().GetGameEntity() as BTA_Prologue_Fight_04;
			if (m_missionEntity == null)
			{
				Log.Spells.PrintError("BTA_Prologue_Cenarius_Death_Spell.AddPowerTargets(): GameEntity is not an instance of BTA_Prologue_Fight_04!");
			}
		}
		FindHeroCards();
		return true;
	}

	private void FindHeroCards()
	{
		if (m_CenariusCard == null)
		{
			m_CenariusCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		}
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
		if (m_fakeDeathState == FakeDeathState.EXPLODING_CENARIUS)
		{
			yield return StartCoroutine(ExplodeCenarius());
		}
		OnSpellFinished();
		OnStateFinished();
	}

	private IEnumerator ExplodeCenarius()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Card EnemyHeroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		EnemyHeroCard.ActivateCharacterDeathEffects();
		m_explodeReformSpellInstance = Object.Instantiate(m_ExplodeReformSpell);
		SpellUtils.SetCustomSpellParent(m_explodeReformSpellInstance, EnemyHeroCard.GetActor());
		m_explodeReformSpellInstance.ActivateState(SpellStateType.ACTION);
		while (m_explodeReformSpellInstance.GetActiveState() != 0)
		{
			yield return null;
		}
		StartCoroutine(m_missionEntity.PlayVictoryLines());
		yield return new WaitForSeconds(10f);
		m_explodeReformSpellInstance.ActivateState(SpellStateType.DEATH);
		while (!m_explodeReformSpellInstance.IsFinished())
		{
			yield return null;
		}
		EnemyHeroCard.ShowCard();
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
