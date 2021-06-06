using UnityEngine;

public class QuestProgressUI : MonoBehaviour
{
	public Transform m_QuestCardBone;

	public Transform m_QuestRewardBone;

	public UberText m_ProgressText;

	public UberText m_QuestDetailText;

	[Header("Reward Overlay Reference Settings")]
	public MeshRenderer m_RewardOverlayRenderer;

	public Texture m_MinionRewardOverlayTexture;

	public Texture m_LegendaryMinionRewardOverlayTexture;

	public Texture m_SpellRewardOverlayTexture;

	public Texture m_GoldenSpellRewardOverlayTexture;

	public Texture m_WeaponRewardOverlayTexture;

	public Texture m_LegendaryWeaponRewardOverlayTexture;

	public Texture m_HeroPowerRewardOverlayTexture;

	[Header("Reward Background Glow Reference Settings")]
	public MeshRenderer m_RewardBackGlowRenderer;

	public Material m_DefaultRewardBackGlowMaterial;

	public Material m_HeroPowerRewardBackGlowMaterial;

	private Actor m_originalQuestActor;

	private Actor m_questCardActor;

	private Actor m_questRewardActor;

	private bool m_isShown;

	private bool m_isResaturating;

	private void OnDestroy()
	{
		if (m_isResaturating && FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().ClearDesaturateListener();
		}
	}

	public void SetOriginalQuestActor(Actor actor)
	{
		m_originalQuestActor = actor;
	}

	public void Show()
	{
		m_isShown = true;
		base.gameObject.SetActive(value: true);
		UpdateActors();
		DesaturateBoard();
	}

	public void Hide()
	{
		m_isShown = false;
		base.gameObject.SetActive(value: false);
		StopDesaturate();
	}

	public void UpdateText(int currentQuestProgress, int questProgressTotal)
	{
		UpdateProgressText(currentQuestProgress, questProgressTotal);
		UpdateQuestDetailText();
	}

	private void UpdateProgressText(int currentQuestProgress, int questProgressTotal)
	{
		m_ProgressText.Text = $"{currentQuestProgress}/{questProgressTotal}";
	}

	private void UpdateQuestDetailText()
	{
		Entity entity = m_originalQuestActor.GetEntity();
		if (entity.HasTag(GAME_TAG.QUEST_CONTRIBUTOR))
		{
			int dbId = entity.GetTag(GAME_TAG.QUEST_CONTRIBUTOR);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(dbId);
			if (entityDef != null)
			{
				m_QuestDetailText.Text = entityDef.GetName();
				m_QuestDetailText.gameObject.SetActive(value: true);
				return;
			}
		}
		m_QuestDetailText.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (!m_isShown || m_originalQuestActor.GetEntity().GetControllerSide() != Player.Side.FRIENDLY)
		{
			return;
		}
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards())
		{
			if (card.GetEntity().HasTag(GAME_TAG.QUEST_CONTRIBUTOR))
			{
				SceneUtils.SetLayer(card.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
	}

	private void DesaturateBoard()
	{
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad);
	}

	private void StopDesaturate()
	{
		m_isResaturating = true;
		FullScreenFXMgr.Get().StopDesaturate(0.4f, iTween.EaseType.easeInOutQuad, OnStopDesaturateFinished);
	}

	private void OnStopDesaturateFinished()
	{
		if (m_originalQuestActor.GetEntity().GetControllerSide() == Player.Side.FRIENDLY)
		{
			foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.GetCards())
			{
				if (!card.IsMousedOver())
				{
					SceneUtils.SetLayer(card.gameObject, GameLayer.Default);
				}
			}
		}
		m_isResaturating = false;
	}

	private void UpdateActors()
	{
		UpdateQuestActor();
		UpdateRewardActor();
	}

	private void UpdateQuestActor()
	{
		if (m_questCardActor == null || m_questCardActor.GetEntityDef() != m_originalQuestActor.GetEntityDef())
		{
			if (m_questCardActor != null)
			{
				m_questCardActor.Destroy();
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(m_originalQuestActor.GetEntity()), AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Log.Gameplay.PrintError("QuestProgressUI.UpdateQuestCard(): Unable to load hand actor for entity {0}.", m_originalQuestActor);
				return;
			}
			SceneUtils.SetLayer(gameObject, m_QuestCardBone.gameObject.layer);
			gameObject.transform.parent = m_QuestCardBone;
			TransformUtil.Identity(gameObject);
			m_questCardActor = gameObject.GetComponentInChildren<Actor>();
			m_questCardActor.SetEntityDef(m_originalQuestActor.GetEntity().GetEntityDef());
			m_questCardActor.SetCardDefFromActor(m_originalQuestActor);
			m_questCardActor.SetPremium(m_originalQuestActor.GetEntity().GetPremiumType());
			m_questCardActor.SetWatermarkCardSetOverride(m_originalQuestActor.GetEntity().GetWatermarkCardSetOverride());
			m_questCardActor.UpdateAllComponents();
		}
	}

	private void UpdateRewardActor()
	{
		Entity entity = m_originalQuestActor.GetEntity();
		string rewardCardIDFromQuestCardID = QuestController.GetRewardCardIDFromQuestCardID(entity);
		if (string.IsNullOrEmpty(rewardCardIDFromQuestCardID))
		{
			Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): No reward card ID found for quest card ID {0}.", entity.GetCardId());
		}
		else
		{
			if (!(m_questRewardActor == null) && !(m_questRewardActor.GetEntityDef().GetCardId() != rewardCardIDFromQuestCardID))
			{
				return;
			}
			if (m_questRewardActor != null)
			{
				m_questRewardActor.Destroy();
			}
			using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(rewardCardIDFromQuestCardID);
			if (disposableCardDef == null)
			{
				Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load CardDef for card ID {0}.", rewardCardIDFromQuestCardID);
				return;
			}
			EntityDef entityDef = DefLoader.Get().GetEntityDef(rewardCardIDFromQuestCardID);
			if (entityDef == null)
			{
				Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load EntityDef for card ID {0}.", rewardCardIDFromQuestCardID);
				return;
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, entity.GetPremiumType()), AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load Hand Actor for entity def {0}.", entityDef);
				return;
			}
			SceneUtils.SetLayer(gameObject, m_QuestRewardBone.gameObject.layer);
			gameObject.transform.parent = m_QuestRewardBone;
			TransformUtil.Identity(gameObject);
			m_questRewardActor = gameObject.GetComponentInChildren<Actor>();
			m_questRewardActor.SetEntityDef(entityDef);
			m_questRewardActor.SetCardDef(disposableCardDef);
			m_questRewardActor.SetPremium(m_originalQuestActor.GetEntity().GetPremiumType());
			m_questRewardActor.SetWatermarkCardSetOverride(m_originalQuestActor.GetEntity().GetWatermarkCardSetOverride());
			m_questRewardActor.UpdateAllComponents();
			UpdateRewardOverlayTexture(entityDef);
			UpdateRewardBackgroundGlowTexture(entityDef);
		}
	}

	private void UpdateRewardOverlayTexture(EntityDef questRewardEntityDef)
	{
		if (!(m_RewardOverlayRenderer == null))
		{
			Texture texture = null;
			if (questRewardEntityDef.IsMinion())
			{
				texture = (questRewardEntityDef.IsElite() ? m_LegendaryMinionRewardOverlayTexture : m_MinionRewardOverlayTexture);
			}
			else if (questRewardEntityDef.IsSpell())
			{
				texture = ((m_questRewardActor.GetPremium() == TAG_PREMIUM.NORMAL) ? m_SpellRewardOverlayTexture : m_GoldenSpellRewardOverlayTexture);
			}
			else if (questRewardEntityDef.IsWeapon())
			{
				texture = (questRewardEntityDef.IsElite() ? m_LegendaryWeaponRewardOverlayTexture : m_WeaponRewardOverlayTexture);
			}
			else if (questRewardEntityDef.IsHeroPower())
			{
				texture = m_HeroPowerRewardOverlayTexture;
			}
			if (!(texture == null))
			{
				Material material = m_RewardOverlayRenderer.GetMaterial();
				material.SetTexture("_MainTex", texture);
				material.SetTexture("_AddTex", texture);
			}
		}
	}

	private void UpdateRewardBackgroundGlowTexture(EntityDef questRewardEntityDef)
	{
		if (!(m_RewardBackGlowRenderer == null))
		{
			Material material = m_DefaultRewardBackGlowMaterial;
			if (questRewardEntityDef.IsHeroPower())
			{
				material = m_HeroPowerRewardBackGlowMaterial;
			}
			m_RewardBackGlowRenderer.SetMaterial(material);
		}
	}
}
