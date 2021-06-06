using System;
using UnityEngine;

// Token: 0x020008DD RID: 2269
public class HeroPowerTooltip : MonoBehaviour
{
	// Token: 0x06007DB1 RID: 32177 RVA: 0x0028C4A3 File Offset: 0x0028A6A3
	public void NotifyMousedOver()
	{
		this.ShowFakeHeroPowerActorAfterDelay();
	}

	// Token: 0x06007DB2 RID: 32178 RVA: 0x0028C4AB File Offset: 0x0028A6AB
	public void NotifyMousedOut()
	{
		this.HideFakeHeroPowerActor();
	}

	// Token: 0x06007DB3 RID: 32179 RVA: 0x0028C4AB File Offset: 0x0028A6AB
	public void NotifyPickedUp()
	{
		this.HideFakeHeroPowerActor();
	}

	// Token: 0x06007DB4 RID: 32180 RVA: 0x0028C4B4 File Offset: 0x0028A6B4
	private void OnDestroy()
	{
		if (this.m_fakeHeroPowerActor != null && this.m_fakeHeroPowerActor.gameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_fakeHeroPowerActor.gameObject);
		}
		this.m_fakeHeroPowerActor = null;
		this.m_ownerCard = null;
	}

	// Token: 0x06007DB5 RID: 32181 RVA: 0x0028C500 File Offset: 0x0028A700
	public void Setup(Card ownerCard)
	{
		if (ownerCard == null || ownerCard.GetEntity() == null)
		{
			Log.Spells.PrintError("HeroPowerTooltip.Setup(): Invalid card was passed in.", Array.Empty<object>());
			return;
		}
		this.m_ownerCard = ownerCard;
		Entity entity = ownerCard.GetEntity();
		int tag = entity.GetTag(GAME_TAG.HERO_POWER);
		if (tag == 0)
		{
			tag = entity.GetTag(GAME_TAG.DISPLAY_CARD_ON_MOUSEOVER);
		}
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(tag))
		{
			if (((fullDef != null) ? fullDef.EntityDef : null) == null || ((fullDef != null) ? fullDef.CardDef : null) == null)
			{
				Log.Spells.PrintError("HeroPowerTooltip.Setup(): Unable to load def for card ID {0}.", new object[]
				{
					tag
				});
			}
			else
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, entity.GetPremiumType()), AssetLoadingOptions.IgnorePrefabPosition);
				if (gameObject == null)
				{
					Log.Spells.PrintError("Card.LoadFakeHeroPowerActor(): Unable to load Hand Actor for entity def {0}.", new object[]
					{
						fullDef.EntityDef
					});
				}
				else
				{
					this.m_fakeHeroPowerActor = gameObject.GetComponentInChildren<Actor>();
					SceneUtils.SetLayer(this.m_fakeHeroPowerActor, GameLayer.Tooltip);
					this.m_fakeHeroPowerActor.SetFullDef(fullDef);
					this.m_fakeHeroPowerActor.SetPremium(entity.GetPremiumType());
					this.m_fakeHeroPowerActor.SetCardBackSideOverride(new Player.Side?(entity.GetControllerSide()));
					this.m_fakeHeroPowerActor.SetWatermarkCardSetOverride(entity.GetWatermarkCardSetOverride());
					this.m_fakeHeroPowerActor.UpdateAllComponents();
					if (this.m_fakeHeroPowerActor.UseCoinManaGem())
					{
						this.m_fakeHeroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
					}
					this.m_fakeHeroPowerActor.Hide();
				}
			}
		}
	}

	// Token: 0x06007DB6 RID: 32182 RVA: 0x0028C6B4 File Offset: 0x0028A8B4
	private void Update()
	{
		if (this.m_fakeHeroPowerActor != null && this.m_fakeHeroPowerActor.IsShown() && !iTween.HasName(this.m_fakeHeroPowerActor.gameObject, "Appearing"))
		{
			this.m_fakeHeroPowerActor.transform.position = base.gameObject.transform.position + this.GetDesiredFakeHeroPowerOffset();
		}
	}

	// Token: 0x06007DB7 RID: 32183 RVA: 0x0028C720 File Offset: 0x0028A920
	private Vector3 GetDesiredFakeHeroPowerOffset()
	{
		Vector3 b = Vector3.zero;
		Vector3 b2 = Vector3.zero;
		if (GameState.Get().IsMulliganManagerActive() && GameState.Get().GetBooleanGameOption(GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN))
		{
			b = HeroPowerTooltip.s_fakeHeroPowerAdditionalOffsetDuringMulligan + new Vector3(-1f, 0f, 0f);
			b2 = HeroPowerTooltip.s_fakeHeroPowerAdditionalOffsetDuringMulligan + new Vector3(1f, 0f, 0f);
		}
		Vector3 a = HeroPowerTooltip.s_fakeHeroPowerRightOffset;
		ZoneHand zoneHand = this.m_ownerCard.GetZone() as ZoneHand;
		if (zoneHand == null)
		{
			if (this.m_ownerCard.GetEntity().IsSidekickHero())
			{
				a = HeroPowerTooltip.s_fakeHeroPowerRightOffsetForSidekickInPlay;
			}
		}
		else if (!zoneHand.ShouldShowCardTooltipOnRight(this.m_ownerCard))
		{
			return HeroPowerTooltip.s_fakeHeroPowerLeftOffset + b2;
		}
		return a + b;
	}

	// Token: 0x06007DB8 RID: 32184 RVA: 0x0028C7EC File Offset: 0x0028A9EC
	private void OnFakeHeroPowerAppearUpdate(float newValue)
	{
		if (this.m_fakeHeroPowerActor == null)
		{
			return;
		}
		if (!this.m_fakeHeroPowerActor.IsShown())
		{
			this.m_fakeHeroPowerActor.Show();
		}
		float num = HeroPowerTooltip.FAKE_HERO_POWER_SCALE * newValue;
		this.m_fakeHeroPowerActor.transform.localScale = new Vector3(num, num, num);
		this.m_fakeHeroPowerActor.transform.position = base.gameObject.transform.position + this.GetDesiredFakeHeroPowerOffset() * newValue;
	}

	// Token: 0x06007DB9 RID: 32185 RVA: 0x0028C874 File Offset: 0x0028AA74
	private void ShowFakeHeroPowerActorAfterDelay()
	{
		if (this.m_fakeHeroPowerActor == null)
		{
			return;
		}
		iTween.Stop(this.m_fakeHeroPowerActor.gameObject);
		if (this.m_fakeHeroPowerActor.UseCoinManaGem())
		{
			this.m_fakeHeroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
		}
		iTween.ValueTo(this.m_fakeHeroPowerActor.gameObject, iTween.Hash(new object[]
		{
			"onupdatetarget",
			base.gameObject,
			"onupdate",
			"OnFakeHeroPowerAppearUpdate",
			"time",
			HeroPowerTooltip.FAKE_HERO_POWER_APPEARANCE_DURATION,
			"delay",
			GameState.Get().GetGameEntity().ShouldDelayShowingFakeHeroPowerTooltip() ? HeroPowerTooltip.FAKE_HERO_POWER_APPEARANCE_DELAY : 0f,
			"to",
			1f,
			"from",
			0f,
			"name",
			"Appearing"
		}));
		this.m_fakeHeroPowerActor.SetUnlit();
	}

	// Token: 0x06007DBA RID: 32186 RVA: 0x0028C988 File Offset: 0x0028AB88
	private void HideFakeHeroPowerActor()
	{
		if (this.m_fakeHeroPowerActor == null)
		{
			return;
		}
		iTween.Stop(this.m_fakeHeroPowerActor.gameObject);
		this.m_fakeHeroPowerActor.Hide();
		if (this.m_fakeHeroPowerActor.UseCoinManaGem())
		{
			this.m_fakeHeroPowerActor.ActivateSpellDeathState(SpellType.COIN_MANA_GEM);
		}
	}

	// Token: 0x040065D4 RID: 26068
	public static readonly float FAKE_HERO_POWER_APPEARANCE_DELAY = 0.55f;

	// Token: 0x040065D5 RID: 26069
	public static readonly float FAKE_HERO_POWER_APPEARANCE_DURATION = 0.125f;

	// Token: 0x040065D6 RID: 26070
	public static readonly float FAKE_HERO_POWER_SCALE = 1.3f;

	// Token: 0x040065D7 RID: 26071
	public static readonly Vector3 s_fakeHeroPowerLeftOffset = new Vector3(-3.1f, -0.1f, 0f);

	// Token: 0x040065D8 RID: 26072
	public static readonly Vector3 s_fakeHeroPowerRightOffset = new Vector3(3.1f, -0.1f, 0f);

	// Token: 0x040065D9 RID: 26073
	public static readonly Vector3 s_fakeHeroPowerAdditionalOffsetDuringMulligan = new Vector3(0f, 0.4f, 0f);

	// Token: 0x040065DA RID: 26074
	public static readonly Vector3 s_fakeHeroPowerRightOffsetForSidekickInPlay = new Vector3(1.6f, -0.1f, 0f);

	// Token: 0x040065DB RID: 26075
	private Actor m_fakeHeroPowerActor;

	// Token: 0x040065DC RID: 26076
	private Card m_ownerCard;
}
