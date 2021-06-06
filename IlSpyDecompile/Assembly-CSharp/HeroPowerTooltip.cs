using UnityEngine;

public class HeroPowerTooltip : MonoBehaviour
{
	public static readonly float FAKE_HERO_POWER_APPEARANCE_DELAY = 0.55f;

	public static readonly float FAKE_HERO_POWER_APPEARANCE_DURATION = 0.125f;

	public static readonly float FAKE_HERO_POWER_SCALE = 1.3f;

	public static readonly Vector3 s_fakeHeroPowerLeftOffset = new Vector3(-3.1f, -0.1f, 0f);

	public static readonly Vector3 s_fakeHeroPowerRightOffset = new Vector3(3.1f, -0.1f, 0f);

	public static readonly Vector3 s_fakeHeroPowerAdditionalOffsetDuringMulligan = new Vector3(0f, 0.4f, 0f);

	public static readonly Vector3 s_fakeHeroPowerRightOffsetForSidekickInPlay = new Vector3(1.6f, -0.1f, 0f);

	private Actor m_fakeHeroPowerActor;

	private Card m_ownerCard;

	public void NotifyMousedOver()
	{
		ShowFakeHeroPowerActorAfterDelay();
	}

	public void NotifyMousedOut()
	{
		HideFakeHeroPowerActor();
	}

	public void NotifyPickedUp()
	{
		HideFakeHeroPowerActor();
	}

	private void OnDestroy()
	{
		if (m_fakeHeroPowerActor != null && m_fakeHeroPowerActor.gameObject != null)
		{
			Object.Destroy(m_fakeHeroPowerActor.gameObject);
		}
		m_fakeHeroPowerActor = null;
		m_ownerCard = null;
	}

	public void Setup(Card ownerCard)
	{
		if (ownerCard == null || ownerCard.GetEntity() == null)
		{
			Log.Spells.PrintError("HeroPowerTooltip.Setup(): Invalid card was passed in.");
			return;
		}
		m_ownerCard = ownerCard;
		Entity entity = ownerCard.GetEntity();
		int num = entity.GetTag(GAME_TAG.HERO_POWER);
		if (num == 0)
		{
			num = entity.GetTag(GAME_TAG.DISPLAY_CARD_ON_MOUSEOVER);
		}
		using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(num);
		if (disposableFullDef?.EntityDef == null || disposableFullDef?.CardDef == null)
		{
			Log.Spells.PrintError("HeroPowerTooltip.Setup(): Unable to load def for card ID {0}.", num);
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(disposableFullDef.EntityDef, entity.GetPremiumType()), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Spells.PrintError("Card.LoadFakeHeroPowerActor(): Unable to load Hand Actor for entity def {0}.", disposableFullDef.EntityDef);
			return;
		}
		m_fakeHeroPowerActor = gameObject.GetComponentInChildren<Actor>();
		SceneUtils.SetLayer(m_fakeHeroPowerActor, GameLayer.Tooltip);
		m_fakeHeroPowerActor.SetFullDef(disposableFullDef);
		m_fakeHeroPowerActor.SetPremium(entity.GetPremiumType());
		m_fakeHeroPowerActor.SetCardBackSideOverride(entity.GetControllerSide());
		m_fakeHeroPowerActor.SetWatermarkCardSetOverride(entity.GetWatermarkCardSetOverride());
		m_fakeHeroPowerActor.UpdateAllComponents();
		if (m_fakeHeroPowerActor.UseCoinManaGem())
		{
			m_fakeHeroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
		}
		m_fakeHeroPowerActor.Hide();
	}

	private void Update()
	{
		if (m_fakeHeroPowerActor != null && m_fakeHeroPowerActor.IsShown() && !iTween.HasName(m_fakeHeroPowerActor.gameObject, "Appearing"))
		{
			m_fakeHeroPowerActor.transform.position = base.gameObject.transform.position + GetDesiredFakeHeroPowerOffset();
		}
	}

	private Vector3 GetDesiredFakeHeroPowerOffset()
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (GameState.Get().IsMulliganManagerActive() && GameState.Get().GetBooleanGameOption(GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN))
		{
			vector = s_fakeHeroPowerAdditionalOffsetDuringMulligan + new Vector3(-1f, 0f, 0f);
			vector2 = s_fakeHeroPowerAdditionalOffsetDuringMulligan + new Vector3(1f, 0f, 0f);
		}
		Vector3 vector3 = s_fakeHeroPowerRightOffset;
		ZoneHand zoneHand = m_ownerCard.GetZone() as ZoneHand;
		if (zoneHand == null)
		{
			if (m_ownerCard.GetEntity().IsSidekickHero())
			{
				vector3 = s_fakeHeroPowerRightOffsetForSidekickInPlay;
			}
		}
		else if (!zoneHand.ShouldShowCardTooltipOnRight(m_ownerCard))
		{
			return s_fakeHeroPowerLeftOffset + vector2;
		}
		return vector3 + vector;
	}

	private void OnFakeHeroPowerAppearUpdate(float newValue)
	{
		if (!(m_fakeHeroPowerActor == null))
		{
			if (!m_fakeHeroPowerActor.IsShown())
			{
				m_fakeHeroPowerActor.Show();
			}
			float num = FAKE_HERO_POWER_SCALE * newValue;
			m_fakeHeroPowerActor.transform.localScale = new Vector3(num, num, num);
			m_fakeHeroPowerActor.transform.position = base.gameObject.transform.position + GetDesiredFakeHeroPowerOffset() * newValue;
		}
	}

	private void ShowFakeHeroPowerActorAfterDelay()
	{
		if (!(m_fakeHeroPowerActor == null))
		{
			iTween.Stop(m_fakeHeroPowerActor.gameObject);
			if (m_fakeHeroPowerActor.UseCoinManaGem())
			{
				m_fakeHeroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
			}
			iTween.ValueTo(m_fakeHeroPowerActor.gameObject, iTween.Hash("onupdatetarget", base.gameObject, "onupdate", "OnFakeHeroPowerAppearUpdate", "time", FAKE_HERO_POWER_APPEARANCE_DURATION, "delay", GameState.Get().GetGameEntity().ShouldDelayShowingFakeHeroPowerTooltip() ? FAKE_HERO_POWER_APPEARANCE_DELAY : 0f, "to", 1f, "from", 0f, "name", "Appearing"));
			m_fakeHeroPowerActor.SetUnlit();
		}
	}

	private void HideFakeHeroPowerActor()
	{
		if (!(m_fakeHeroPowerActor == null))
		{
			iTween.Stop(m_fakeHeroPowerActor.gameObject);
			m_fakeHeroPowerActor.Hide();
			if (m_fakeHeroPowerActor.UseCoinManaGem())
			{
				m_fakeHeroPowerActor.ActivateSpellDeathState(SpellType.COIN_MANA_GEM);
			}
		}
	}
}
