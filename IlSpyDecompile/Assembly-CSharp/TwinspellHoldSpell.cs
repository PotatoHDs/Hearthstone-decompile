using System.Collections;
using UnityEngine;

public class TwinspellHoldSpell : Spell
{
	private Entity m_originalSpellEntity;

	private Actor m_fakeTwinspellActor;

	private int m_fakeTwinspellHandSlot;

	private bool m_fakeActorLoaded;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		StartCoroutine(DoUpdate());
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		StopAllCoroutines();
		HideFakeTwinspellActor();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StopAllCoroutines();
	}

	private IEnumerator DoUpdate()
	{
		while (true)
		{
			if (m_fakeActorLoaded && m_fakeTwinspellActor != null)
			{
				if (m_fakeTwinspellActor.GetSpell(SpellType.TWINSPELLPENDING).GetActiveState() == SpellStateType.NONE)
				{
					ShowFakeTwinspellActor();
				}
				ZoneHand friendlyHand = InputManager.Get().GetFriendlyHand();
				m_fakeTwinspellActor.transform.position = friendlyHand.GetCardPosition(m_fakeTwinspellHandSlot, -1);
				m_fakeTwinspellActor.transform.localEulerAngles = friendlyHand.GetCardRotation(m_fakeTwinspellHandSlot, -1);
				m_fakeTwinspellActor.transform.localScale = friendlyHand.GetCardScale();
			}
			yield return null;
		}
	}

	public bool Initialize(int heldEntityId, int zonePosition)
	{
		m_fakeActorLoaded = false;
		m_originalSpellEntity = GameState.Get().GetEntity(heldEntityId);
		if (m_originalSpellEntity == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): Unable to find Entity for Entity ID {0}.", heldEntityId);
			return false;
		}
		if (!m_originalSpellEntity.IsTwinspell())
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): TwinspellHoldSpell has been hooked up to a Card that is not a Twinspell!");
			return false;
		}
		if (!LoadFakeTwinspellActor())
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): Failed to load the fake Twinspell actor", heldEntityId);
			return false;
		}
		m_fakeTwinspellHandSlot = zonePosition - 1;
		return true;
	}

	public int GetOriginalSpellEntityId()
	{
		if (m_originalSpellEntity == null)
		{
			return -1;
		}
		return m_originalSpellEntity.GetEntityId();
	}

	public int GetFakeTwinspellZonePosition()
	{
		return m_fakeTwinspellHandSlot + 1;
	}

	private bool LoadFakeTwinspellActor()
	{
		if (m_fakeTwinspellActor != null)
		{
			m_fakeTwinspellActor.DeactivateAllSpells();
			m_fakeTwinspellActor.Destroy();
		}
		if (m_originalSpellEntity == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): m_originalSpellEntity is null. Has TwinspellHoldSpell.Initialize() been called?");
			return false;
		}
		if (!m_originalSpellEntity.HasTag(GAME_TAG.TWINSPELL_COPY))
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): m_originalSpellEntity does not have the TWINSPELL_COPY tag");
			return false;
		}
		int num = m_originalSpellEntity.GetTag(GAME_TAG.TWINSPELL_COPY);
		using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(num);
		if (disposableFullDef?.EntityDef == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): Unable to load EntityDef for card ID {0}.", num);
			return false;
		}
		if (disposableFullDef?.CardDef == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): Unable to load CardDef for card ID {0}.", num);
			return false;
		}
		string twinspellCardId = GameUtils.TranslateDbIdToCardId(num);
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(disposableFullDef.EntityDef, m_originalSpellEntity.GetPremiumType()), delegate(AssetReference actorName, GameObject actorGameObject, object data)
		{
			OnFakeTwinspellActorLoaded(actorName, actorGameObject, twinspellCardId, m_originalSpellEntity.GetPremiumType());
		}, twinspellCardId, AssetLoadingOptions.IgnorePrefabPosition);
		return true;
	}

	private void OnFakeTwinspellActorLoaded(AssetReference assetRef, GameObject actorGameObject, string fakeTwinspellCardId, TAG_PREMIUM premium)
	{
		if (actorGameObject == null)
		{
			Debug.LogError($"TwinspellHoldSpell.OnFakeTwinspellActorLoaded: Unable to load fake actor for card: {assetRef}");
			return;
		}
		if (m_fakeTwinspellActor != null)
		{
			m_fakeTwinspellActor.DeactivateAllSpells();
			m_fakeTwinspellActor.Destroy();
		}
		m_fakeTwinspellActor = actorGameObject.GetComponent<Actor>();
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(fakeTwinspellCardId, m_fakeTwinspellActor.CardPortraitQuality))
		{
			m_fakeTwinspellActor.SetFullDef(fullDef);
		}
		m_fakeTwinspellActor.SetPremium(m_originalSpellEntity.GetPremiumType());
		m_fakeTwinspellActor.SetCardBackSideOverride(m_originalSpellEntity.GetControllerSide());
		m_fakeTwinspellActor.SetWatermarkCardSetOverride(m_originalSpellEntity.GetWatermarkCardSetOverride());
		m_fakeTwinspellActor.UpdateAllComponents();
		m_fakeTwinspellActor.Hide();
		m_fakeActorLoaded = true;
	}

	private void ShowFakeTwinspellActor()
	{
		if (!(m_fakeTwinspellActor != null) || !m_fakeTwinspellActor.IsShown())
		{
			ZoneHand friendlyHand = InputManager.Get().GetFriendlyHand();
			m_fakeTwinspellActor.transform.position = friendlyHand.GetCardPosition(m_fakeTwinspellHandSlot, -1);
			m_fakeTwinspellActor.transform.localEulerAngles = friendlyHand.GetCardRotation(m_fakeTwinspellHandSlot, -1);
			m_fakeTwinspellActor.transform.localScale = friendlyHand.GetCardScale();
			m_fakeTwinspellActor.ActivateSpellBirthState(SpellType.TWINSPELLPENDING);
		}
	}

	private void HideFakeTwinspellActor()
	{
		if (!(m_fakeTwinspellActor == null))
		{
			m_fakeTwinspellActor.ActivateSpellDeathState(SpellType.TWINSPELLPENDING);
			m_fakeTwinspellActor.Hide();
		}
	}
}
