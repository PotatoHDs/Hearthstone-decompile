using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class TwinspellHoldSpell : Spell
{
	// Token: 0x0600704C RID: 28748 RVA: 0x002437E9 File Offset: 0x002419E9
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		base.StartCoroutine(this.DoUpdate());
	}

	// Token: 0x0600704D RID: 28749 RVA: 0x002437FF File Offset: 0x002419FF
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		base.StopAllCoroutines();
		this.HideFakeTwinspellActor();
	}

	// Token: 0x0600704E RID: 28750 RVA: 0x00243814 File Offset: 0x00241A14
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StopAllCoroutines();
	}

	// Token: 0x0600704F RID: 28751 RVA: 0x00243823 File Offset: 0x00241A23
	private IEnumerator DoUpdate()
	{
		for (;;)
		{
			if (this.m_fakeActorLoaded && this.m_fakeTwinspellActor != null)
			{
				if (this.m_fakeTwinspellActor.GetSpell(SpellType.TWINSPELLPENDING).GetActiveState() == SpellStateType.NONE)
				{
					this.ShowFakeTwinspellActor();
				}
				ZoneHand friendlyHand = InputManager.Get().GetFriendlyHand();
				this.m_fakeTwinspellActor.transform.position = friendlyHand.GetCardPosition(this.m_fakeTwinspellHandSlot, -1);
				this.m_fakeTwinspellActor.transform.localEulerAngles = friendlyHand.GetCardRotation(this.m_fakeTwinspellHandSlot, -1);
				this.m_fakeTwinspellActor.transform.localScale = friendlyHand.GetCardScale();
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06007050 RID: 28752 RVA: 0x00243834 File Offset: 0x00241A34
	public bool Initialize(int heldEntityId, int zonePosition)
	{
		this.m_fakeActorLoaded = false;
		this.m_originalSpellEntity = GameState.Get().GetEntity(heldEntityId);
		if (this.m_originalSpellEntity == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): Unable to find Entity for Entity ID {0}.", new object[]
			{
				heldEntityId
			});
			return false;
		}
		if (!this.m_originalSpellEntity.IsTwinspell())
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): TwinspellHoldSpell has been hooked up to a Card that is not a Twinspell!", Array.Empty<object>());
			return false;
		}
		if (!this.LoadFakeTwinspellActor())
		{
			Log.Spells.PrintError("TwinspellHoldSpell.Initialize(): Failed to load the fake Twinspell actor", new object[]
			{
				heldEntityId
			});
			return false;
		}
		this.m_fakeTwinspellHandSlot = zonePosition - 1;
		return true;
	}

	// Token: 0x06007051 RID: 28753 RVA: 0x002438D6 File Offset: 0x00241AD6
	public int GetOriginalSpellEntityId()
	{
		if (this.m_originalSpellEntity == null)
		{
			return -1;
		}
		return this.m_originalSpellEntity.GetEntityId();
	}

	// Token: 0x06007052 RID: 28754 RVA: 0x002438ED File Offset: 0x00241AED
	public int GetFakeTwinspellZonePosition()
	{
		return this.m_fakeTwinspellHandSlot + 1;
	}

	// Token: 0x06007053 RID: 28755 RVA: 0x002438F8 File Offset: 0x00241AF8
	private bool LoadFakeTwinspellActor()
	{
		if (this.m_fakeTwinspellActor != null)
		{
			this.m_fakeTwinspellActor.DeactivateAllSpells();
			this.m_fakeTwinspellActor.Destroy();
		}
		if (this.m_originalSpellEntity == null)
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): m_originalSpellEntity is null. Has TwinspellHoldSpell.Initialize() been called?", Array.Empty<object>());
			return false;
		}
		if (!this.m_originalSpellEntity.HasTag(GAME_TAG.TWINSPELL_COPY))
		{
			Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): m_originalSpellEntity does not have the TWINSPELL_COPY tag", Array.Empty<object>());
			return false;
		}
		int tag = this.m_originalSpellEntity.GetTag(GAME_TAG.TWINSPELL_COPY);
		bool result;
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(tag))
		{
			if (((fullDef != null) ? fullDef.EntityDef : null) == null)
			{
				Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): Unable to load EntityDef for card ID {0}.", new object[]
				{
					tag
				});
				result = false;
			}
			else if (((fullDef != null) ? fullDef.CardDef : null) == null)
			{
				Log.Spells.PrintError("TwinspellHoldSpell.LoadFakeTwinspellActor(): Unable to load CardDef for card ID {0}.", new object[]
				{
					tag
				});
				result = false;
			}
			else
			{
				string twinspellCardId = GameUtils.TranslateDbIdToCardId(tag, false);
				AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, this.m_originalSpellEntity.GetPremiumType()), delegate(AssetReference actorName, GameObject actorGameObject, object data)
				{
					this.OnFakeTwinspellActorLoaded(actorName, actorGameObject, twinspellCardId, this.m_originalSpellEntity.GetPremiumType());
				}, twinspellCardId, AssetLoadingOptions.IgnorePrefabPosition);
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06007054 RID: 28756 RVA: 0x00243A68 File Offset: 0x00241C68
	private void OnFakeTwinspellActorLoaded(AssetReference assetRef, GameObject actorGameObject, string fakeTwinspellCardId, TAG_PREMIUM premium)
	{
		if (actorGameObject == null)
		{
			Debug.LogError(string.Format("TwinspellHoldSpell.OnFakeTwinspellActorLoaded: Unable to load fake actor for card: {0}", assetRef));
			return;
		}
		if (this.m_fakeTwinspellActor != null)
		{
			this.m_fakeTwinspellActor.DeactivateAllSpells();
			this.m_fakeTwinspellActor.Destroy();
		}
		this.m_fakeTwinspellActor = actorGameObject.GetComponent<Actor>();
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(fakeTwinspellCardId, this.m_fakeTwinspellActor.CardPortraitQuality))
		{
			this.m_fakeTwinspellActor.SetFullDef(fullDef);
		}
		this.m_fakeTwinspellActor.SetPremium(this.m_originalSpellEntity.GetPremiumType());
		this.m_fakeTwinspellActor.SetCardBackSideOverride(new Player.Side?(this.m_originalSpellEntity.GetControllerSide()));
		this.m_fakeTwinspellActor.SetWatermarkCardSetOverride(this.m_originalSpellEntity.GetWatermarkCardSetOverride());
		this.m_fakeTwinspellActor.UpdateAllComponents();
		this.m_fakeTwinspellActor.Hide();
		this.m_fakeActorLoaded = true;
	}

	// Token: 0x06007055 RID: 28757 RVA: 0x00243B64 File Offset: 0x00241D64
	private void ShowFakeTwinspellActor()
	{
		if (this.m_fakeTwinspellActor != null && this.m_fakeTwinspellActor.IsShown())
		{
			return;
		}
		ZoneHand friendlyHand = InputManager.Get().GetFriendlyHand();
		this.m_fakeTwinspellActor.transform.position = friendlyHand.GetCardPosition(this.m_fakeTwinspellHandSlot, -1);
		this.m_fakeTwinspellActor.transform.localEulerAngles = friendlyHand.GetCardRotation(this.m_fakeTwinspellHandSlot, -1);
		this.m_fakeTwinspellActor.transform.localScale = friendlyHand.GetCardScale();
		this.m_fakeTwinspellActor.ActivateSpellBirthState(SpellType.TWINSPELLPENDING);
	}

	// Token: 0x06007056 RID: 28758 RVA: 0x00243BF9 File Offset: 0x00241DF9
	private void HideFakeTwinspellActor()
	{
		if (this.m_fakeTwinspellActor == null)
		{
			return;
		}
		this.m_fakeTwinspellActor.ActivateSpellDeathState(SpellType.TWINSPELLPENDING);
		this.m_fakeTwinspellActor.Hide();
	}

	// Token: 0x04005A2F RID: 23087
	private Entity m_originalSpellEntity;

	// Token: 0x04005A30 RID: 23088
	private Actor m_fakeTwinspellActor;

	// Token: 0x04005A31 RID: 23089
	private int m_fakeTwinspellHandSlot;

	// Token: 0x04005A32 RID: 23090
	private bool m_fakeActorLoaded;
}
