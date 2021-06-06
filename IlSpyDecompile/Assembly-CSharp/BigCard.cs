using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

[CustomEditClass]
public class BigCard : MonoBehaviour
{
	[Serializable]
	public class LayoutData
	{
		public float m_ScaleSec = 0.15f;

		public float m_DriftSec = 10f;
	}

	[Serializable]
	public class SecretLayoutOffsets
	{
		public Vector3 m_InitialOffset = new Vector3(0.1f, 5f, 3.3f);

		public Vector3 m_OpponentInitialOffset = new Vector3(0.1f, 5f, -3.3f);

		public Vector3 m_HiddenInitialOffset = new Vector3(0f, 4f, 4f);

		public Vector3 m_HiddenOpponentInitialOffset = new Vector3(0f, 4f, -4f);
	}

	[Serializable]
	public class SecretLayoutData
	{
		public float m_ShowAnimTime = 0.15f;

		public float m_HideAnimTime = 0.15f;

		public float m_DeathShowAnimTime = 1f;

		public float m_TimeUntilDeathSpell = 1.5f;

		public float m_DriftSec = 5f;

		public Vector3 m_DriftOffset = new Vector3(0f, 0f, 0.05f);

		public Vector3 m_Spacing = new Vector3(2.1f, 0f, 0.7f);

		public Vector3 m_HiddenSpacing = new Vector3(2.4f, 0f, 0.7f);

		public int m_MinCardThreshold = 1;

		public int m_MaxCardThreshold = 5;

		public SecretLayoutOffsets m_MinCardOffsets = new SecretLayoutOffsets();

		public SecretLayoutOffsets m_MaxCardOffsets = new SecretLayoutOffsets();
	}

	private struct KeywordArgs
	{
		public Card card;

		public Actor actor;

		public bool showOnRight;
	}

	public BigCardEnchantmentPanel m_EnchantmentPanelPrefab;

	public GameObject m_EnchantmentBanner;

	public GameObject m_EnchantmentBannerBottom;

	public UberText m_EnchantmentBannerText;

	public int m_RenderQueueEnchantmentBanner = 1;

	public int m_RenderQueueEnchantmentPanel = 2;

	public LayoutData m_LayoutData;

	public SecretLayoutData m_SecretLayoutData;

	private static readonly Vector3 INVISIBLE_SCALE = new Vector3(0.0001f, 0.0001f, 0.0001f);

	private static BigCard s_instance;

	private Card m_card;

	private Actor m_bigCardActor;

	private TooltipPanel m_bigCardAsTooltip;

	private Actor m_twinCardActor;

	private List<Actor> m_phoneSecretActors;

	private List<Actor> m_phoneSideQuestActors;

	private List<Actor> m_phoneSigilActors;

	private float m_initialBannerHeight;

	private Vector3 m_initialBannerScale;

	private Vector3 m_initialBannerBottomScale;

	private Vector3 m_initialBannerTextScale;

	private Pool<BigCardEnchantmentPanel> m_enchantmentPool = new Pool<BigCardEnchantmentPanel>();

	private Map<string, BigCardEnchantmentPanel> m_uniqueEnchantmentLookup = new Map<string, BigCardEnchantmentPanel>();

	private readonly PlatformDependentValue<float> PLATFORM_SCALING_FACTOR;

	private readonly PlatformDependentValue<float> ENCHANTMENT_SCALING_FACTOR;

	public BigCard()
	{
		PLATFORM_SCALING_FACTOR = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 1.3f,
			MiniTablet = 1f
		};
		ENCHANTMENT_SCALING_FACTOR = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 1.5f,
			MiniTablet = 1f
		};
	}

	private void Awake()
	{
		s_instance = this;
		m_initialBannerHeight = m_EnchantmentBanner.GetComponent<Renderer>().bounds.size.z;
		m_initialBannerScale = m_EnchantmentBanner.transform.localScale;
		m_initialBannerBottomScale = m_EnchantmentBannerBottom.transform.localScale;
		m_initialBannerTextScale = m_EnchantmentBannerText.transform.localScale;
		m_enchantmentPool.SetCreateItemCallback(CreateEnchantmentPanel);
		m_enchantmentPool.SetDestroyItemCallback(DestroyEnchantmentPanel);
		m_enchantmentPool.SetExtensionCount(1);
		m_enchantmentPool.SetMaxReleasedItemCount(2);
		ResetEnchantments();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static BigCard Get()
	{
		return s_instance;
	}

	public Card GetCard()
	{
		return m_card;
	}

	public void Show(Card card)
	{
		m_card = card;
		if (GameState.Get() != null && !GameState.Get().GetGameEntity().NotifyOfCardTooltipDisplayShow(card))
		{
			return;
		}
		Zone zone = card.GetZone();
		if ((bool)UniversalInputManager.UsePhoneUI && zone is ZoneSecret)
		{
			if (card.GetEntity().IsSideQuest())
			{
				LoadAndDisplayTooltipPhoneSideQuests();
			}
			else if (card.GetEntity().IsSigil())
			{
				LoadAndDisplayTooltipPhoneSigils();
			}
			else
			{
				LoadAndDisplayTooltipPhoneSecrets();
			}
		}
		else
		{
			LoadAndDisplayBigCard();
		}
	}

	public void Hide()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardTooltipDisplayHide(m_card);
		}
		HideBigCard();
		HideTooltipPhoneSecrets();
		HideTooltipPhoneSideQuests();
		HideTooltipPhoneSigils();
		m_card = null;
	}

	public bool Hide(Card card)
	{
		if (m_card != card)
		{
			return false;
		}
		Hide();
		return true;
	}

	public void ShowSecretDeaths(Map<Player, DeadSecretGroup> deadSecretMap)
	{
		if (deadSecretMap == null || deadSecretMap.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (DeadSecretGroup value in deadSecretMap.Values)
		{
			Card mainCard = value.GetMainCard();
			List<Card> cards = value.GetCards();
			List<Actor> list = new List<Actor>();
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				Actor item = LoadPhoneSecret(card);
				list.Add(item);
			}
			DisplayPhoneSecrets(mainCard, list, showDeath: true);
			num++;
		}
	}

	private void LoadAndDisplayBigCard()
	{
		if ((bool)m_bigCardActor)
		{
			m_bigCardActor.Destroy();
		}
		if (ActorNames.ShouldDisplayTooltipInsteadOfBigCard(m_card.GetEntity()))
		{
			DisplayBigCardAsTooltip();
			return;
		}
		string bigCardActor = ActorNames.GetBigCardActor(m_card.GetEntity());
		if (bigCardActor == "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9")
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(bigCardActor, AssetLoadingOptions.IgnorePrefabPosition);
		m_bigCardActor = gameObject.GetComponent<Actor>();
		SetupActor(m_card, m_bigCardActor);
		int num = m_card.GetEntity().GetTag(GAME_TAG.DISGUISED_TWIN);
		if (num > 0)
		{
			using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(num);
			bigCardActor = ActorNames.GetHandActor(disposableFullDef?.EntityDef, m_card.GetEntity().GetPremiumType());
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(bigCardActor, AssetLoadingOptions.IgnorePrefabPosition);
			m_twinCardActor = gameObject2.GetComponent<Actor>();
			SceneUtils.SetLayer(m_twinCardActor, GameLayer.Tooltip);
			m_twinCardActor.SetFullDef(disposableFullDef);
			m_twinCardActor.SetPremium(m_card.GetEntity().GetPremiumType());
			m_twinCardActor.SetCardBackSideOverride(m_card.GetEntity().GetControllerSide());
			m_twinCardActor.SetWatermarkCardSetOverride(m_card.GetEntity().GetWatermarkCardSetOverride());
			m_twinCardActor.UpdateAllComponents();
		}
		DisplayBigCard();
	}

	private void HideBigCard()
	{
		if ((bool)m_bigCardActor)
		{
			ResetEnchantments();
			iTween.Stop(m_bigCardActor.gameObject);
			m_bigCardActor.Destroy();
			m_bigCardActor = null;
			TooltipPanelManager.Get().HideKeywordHelp();
		}
		if ((bool)m_bigCardAsTooltip)
		{
			UnityEngine.Object.Destroy(m_bigCardAsTooltip);
		}
		if ((bool)m_twinCardActor)
		{
			iTween.Stop(m_twinCardActor.gameObject);
			m_twinCardActor.Destroy();
			m_twinCardActor = null;
		}
	}

	private void DisplayBigCardAsTooltip()
	{
		if (m_bigCardAsTooltip != null)
		{
			UnityEngine.Object.Destroy(m_bigCardAsTooltip);
		}
		Vector3 vector = ((!ShowBigCardOnRight()) ? new Vector3(-2f, 0f, 0f) : new Vector3(2f, 0f, 0f));
		Vector3 position = m_card.transform.position + vector;
		m_bigCardAsTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		m_bigCardAsTooltip.Reset();
		m_bigCardAsTooltip.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		m_bigCardAsTooltip.Initialize(m_card.GetEntity().GetName(), m_card.GetEntity().GetCardTextInHand());
		m_bigCardAsTooltip.transform.position = position;
		RenderUtils.SetAlpha(m_bigCardAsTooltip.gameObject, 0f);
		iTween.FadeTo(m_bigCardAsTooltip.gameObject, iTween.Hash("alpha", 1, "time", 0.1f));
	}

	private void DisplayBigCard()
	{
		Entity entity = m_card.GetEntity();
		bool flag = entity.GetController().IsFriendlySide();
		Zone zone = m_card.GetZone();
		Bounds bounds = m_bigCardActor.GetMeshRenderer().bounds;
		Vector3 position = m_card.GetActor().transform.position;
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		Vector3 vector3 = new Vector3(1.1f, 1.1f, 1.1f);
		float? overrideScale = null;
		if (zone is ZoneHero)
		{
			vector = ((!flag) ? new Vector3(0f, 4f, (0f - bounds.size.z) * 0.7f) : new Vector3(0f, 4f, 0f));
		}
		else if (zone is ZoneWeapon)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				if (flag)
				{
					vector3 = new Vector3(1.98f, 1.27f, 1.98f);
					vector = new Vector3(5.45f, 0f, bounds.size.z * 0.9f);
				}
				else
				{
					vector3 = new Vector3(1.65f, 1.65f, 1.65f);
					vector = new Vector3(-1.57f, 0f, -1f);
				}
			}
			else
			{
				vector3 = new Vector3(1.65f, 1.65f, 1.65f);
				vector = ((!flag) ? new Vector3(-1.57f, 0f, -1f) : new Vector3(0f, 0f, bounds.size.z * 0.9f));
			}
			vector3 *= (float)PLATFORM_SCALING_FACTOR;
		}
		else if (zone is ZoneHeroPower)
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				vector = ((!flag) ? new Vector3(0f, 4f, -2.6f) : new Vector3(0f, 4f, 2.69f));
			}
			else
			{
				vector3 = new Vector3(1.3f, 1f, 1.3f);
				vector = ((!flag) ? new Vector3(-3.5f, 8f, -3.35f) : new Vector3(-3.5f, 8f, 3.5f));
			}
			overrideScale = 0.6f;
		}
		else if (zone is ZoneSecret)
		{
			vector3 = new Vector3(1.65f, 1.65f, 1.65f);
			vector = new Vector3(bounds.size.x + 0.3f, 0f, 0f);
		}
		else if (zone is ZoneHand)
		{
			vector = new Vector3(bounds.size.x * 0.7f, 4f, (0f - bounds.size.z) * 0.8f);
			vector3 = new Vector3(1.65f, 1.65f, 1.65f);
		}
		else
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				vector3 = new Vector3(2f, 2f, 2f);
				vector = ((!ShowBigCardOnRight()) ? new Vector3(0f - bounds.size.x - 2.2f, 0f, 0f) : new Vector3(bounds.size.x + 2.2f, 0f, 0f));
			}
			else
			{
				vector3 = new Vector3(1.65f, 1.65f, 1.65f);
				if (!m_twinCardActor)
				{
					vector = ((!ShowBigCardOnRight()) ? new Vector3(0f - bounds.size.x - 0.7f, 0f, 0f) : new Vector3(bounds.size.x + 0.7f, 0f, 0f));
				}
				else if (UnityEngine.Random.Range(0, 2) == 0)
				{
					vector = new Vector3(bounds.size.x + 0.7f, 0f, 0f);
					vector2 = new Vector3(0f - bounds.size.x - 0.7f, 0f, 0f);
				}
				else
				{
					vector = new Vector3(0f - bounds.size.x - 0.7f, 0f, 0f);
					vector2 = new Vector3(bounds.size.x + 0.7f, 0f, 0f);
				}
			}
			if (zone is ZonePlay)
			{
				vector += new Vector3(0f, 0.1f, 0f);
				vector2 += new Vector3(0f, 0.1f, 0f);
				vector3 *= (float)PLATFORM_SCALING_FACTOR;
			}
		}
		Vector3 vector4 = new Vector3(0.02f, 0.02f, 0.02f);
		Vector3 position2 = position + vector + vector4;
		Vector3 localScale = new Vector3(1f, 1f, 1f);
		Transform parent = m_bigCardActor.transform.parent;
		m_bigCardActor.transform.localScale = vector3;
		m_bigCardActor.transform.position = position2;
		m_bigCardActor.transform.parent = null;
		Transform parent2 = null;
		if ((bool)m_twinCardActor)
		{
			parent2 = m_twinCardActor.transform.parent;
		}
		Vector3 position3 = position + vector2 + vector4;
		if (m_card.GetEntity().GetTag(GAME_TAG.DISGUISED_TWIN) > 0 && m_twinCardActor != null)
		{
			m_twinCardActor.transform.localScale = vector3;
			m_twinCardActor.transform.position = position3;
			m_twinCardActor.transform.parent = null;
		}
		if (zone is ZoneHand)
		{
			m_bigCardActor.SetEntity(entity);
			m_bigCardActor.UpdateTextComponents(entity);
		}
		else
		{
			if (m_twinCardActor == null)
			{
				UpdateEnchantments();
			}
			else
			{
				ResetEnchantments();
			}
			if ((bool)UniversalInputManager.UsePhoneUI && m_EnchantmentBanner.activeInHierarchy)
			{
				float num = ((m_enchantmentPool.GetActiveList().Count > 1) ? 0.75f : 0.85f);
				vector3 *= num;
				m_bigCardActor.transform.localScale = vector3;
			}
		}
		FitInsideScreen();
		m_bigCardActor.transform.parent = parent;
		m_bigCardActor.transform.localScale = localScale;
		if ((bool)m_twinCardActor)
		{
			m_twinCardActor.transform.parent = parent2;
			m_twinCardActor.transform.localScale = localScale;
		}
		Vector3 position4 = m_bigCardActor.transform.position;
		m_bigCardActor.transform.position -= vector4;
		Vector3 position5 = new Vector3(0f, 0f, 0f);
		if ((bool)m_twinCardActor)
		{
			position5 = m_twinCardActor.transform.position;
			m_twinCardActor.transform.position -= vector4;
		}
		KeywordArgs keywordArgs = default(KeywordArgs);
		keywordArgs.card = m_card;
		keywordArgs.actor = m_bigCardActor;
		keywordArgs.showOnRight = ShowBigCardOnRight();
		if (zone is ZoneHand)
		{
			Hashtable args = iTween.Hash("scale", vector3, "time", m_LayoutData.m_ScaleSec, "oncompleteparams", keywordArgs, "oncomplete", (Action<object>)delegate(object obj)
			{
				KeywordArgs keywordArgs3 = (KeywordArgs)obj;
				TooltipPanelManager.Get().UpdateKeywordHelp(keywordArgs3.card, keywordArgs3.actor, keywordArgs3.showOnRight);
			});
			iTween.ScaleTo(m_bigCardActor.gameObject, args);
		}
		else
		{
			iTween.ScaleTo(m_bigCardActor.gameObject, vector3, m_LayoutData.m_ScaleSec);
			if (m_twinCardActor != null)
			{
				KeywordArgs keywordArgs2 = default(KeywordArgs);
				keywordArgs2.card = m_card;
				keywordArgs2.actor = m_twinCardActor;
				keywordArgs2.showOnRight = ShowBigCardOnRight();
				iTween.ScaleTo(m_twinCardActor.gameObject, vector3, m_LayoutData.m_ScaleSec);
				iTween.MoveTo(m_twinCardActor.gameObject, position5, m_LayoutData.m_DriftSec);
				m_twinCardActor.transform.rotation = Quaternion.identity;
				m_twinCardActor.Show();
			}
			else
			{
				TooltipPanelManager.Get().UpdateKeywordHelp(keywordArgs.card, keywordArgs.actor, keywordArgs.showOnRight, overrideScale);
			}
		}
		iTween.MoveTo(m_bigCardActor.gameObject, position4, m_LayoutData.m_DriftSec);
		m_bigCardActor.transform.rotation = Quaternion.identity;
		m_bigCardActor.Show();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			TooltipPanelManager.Get().UpdateKeywordHelp(m_card, m_bigCardActor, ShowKeywordOnRight(), overrideScale);
		}
		if (entity.IsSilenced())
		{
			m_bigCardActor.ActivateSpellBirthState(SpellType.SILENCE);
			if (m_twinCardActor != null)
			{
				m_twinCardActor.ActivateSpellBirthState(SpellType.SILENCE);
			}
		}
	}

	private bool ShowBigCardOnRight()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return ShowBigCardOnRightTouch();
		}
		return ShowBigCardOnRightMouse();
	}

	private bool ShowBigCardOnRightMouse()
	{
		if (m_card.GetEntity().IsHero() || m_card.GetEntity().IsHeroPower() || m_card.GetEntity().IsSecret())
		{
			return true;
		}
		if (m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = m_card.GetZone() as ZonePlay;
		if (zonePlay != null)
		{
			Actor actor = m_card.GetActor();
			if (actor != null)
			{
				MeshRenderer meshRenderer = actor.GetMeshRenderer();
				if (meshRenderer != null)
				{
					float x = meshRenderer.bounds.center.x;
					float num = zonePlay.GetComponent<BoxCollider>().bounds.center.x + 2.5f;
					return x < num;
				}
			}
		}
		return true;
	}

	private bool ShowBigCardOnRightTouch()
	{
		if (m_card.GetEntity().IsHero() || m_card.GetEntity().IsHeroPower() || m_card.GetEntity().IsSecret())
		{
			return false;
		}
		if (m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = m_card.GetZone() as ZonePlay;
		if (zonePlay != null)
		{
			float num = (UniversalInputManager.UsePhoneUI ? 0f : (-2.5f));
			return m_card.GetActor().GetMeshRenderer().bounds.center.x < zonePlay.GetComponent<BoxCollider>().bounds.center.x + num;
		}
		return false;
	}

	private bool ShowKeywordOnRight()
	{
		if (m_card.GetEntity().IsHeroPower())
		{
			return true;
		}
		if (m_card.GetEntity().IsWeapon())
		{
			return false;
		}
		if (m_card.GetEntity().IsHero() || m_card.GetEntity().IsSecretOrQuestOrSideQuestOrSigil())
		{
			return false;
		}
		if (m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = m_card.GetZone() as ZonePlay;
		if (zonePlay != null)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				return m_card.GetActor().GetMeshRenderer().bounds.center.x > zonePlay.GetComponent<BoxCollider>().bounds.center.x;
			}
			return m_card.GetActor().GetMeshRenderer().bounds.center.x < zonePlay.GetComponent<BoxCollider>().bounds.center.x + 0.03f;
		}
		return false;
	}

	private void UpdateEnchantments()
	{
		ResetEnchantments();
		GameObject gameObject = m_bigCardActor.FindBone("EnchantmentTooltip");
		if (gameObject == null)
		{
			return;
		}
		Entity entity = m_card.GetEntity();
		bool flag = GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS);
		List<Entity> displayedEnchantments = entity.GetDisplayedEnchantments();
		List<Entity> displayedEnchantments2 = entity.GetDisplayedEnchantments(unique: true);
		List<BigCardEnchantmentPanel> activeList = m_enchantmentPool.GetActiveList();
		m_uniqueEnchantmentLookup.Clear();
		int num = (flag ? displayedEnchantments2.Count : displayedEnchantments.Count);
		int count = activeList.Count;
		int num2 = num - count;
		if (num2 > 0)
		{
			m_enchantmentPool.AcquireBatch(num2);
		}
		else if (num2 < 0)
		{
			m_enchantmentPool.ReleaseBatch(num, -num2);
		}
		if (num == 0 && !m_card.GetEntity().HasTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT) && !m_card.GetEntity().IsSideQuest())
		{
			return;
		}
		for (int i = 0; i < activeList.Count; i++)
		{
			BigCardEnchantmentPanel bigCardEnchantmentPanel = activeList[i];
			bigCardEnchantmentPanel.SetEnchantment(flag ? displayedEnchantments2[i] : displayedEnchantments[i]);
			if (flag)
			{
				m_uniqueEnchantmentLookup.Add(bigCardEnchantmentPanel.GetEnchantmentId(), bigCardEnchantmentPanel);
			}
		}
		if (flag)
		{
			HashSet<string> hashSet = new HashSet<string>();
			for (int j = 0; j < displayedEnchantments.Count; j++)
			{
				if (!hashSet.Contains(displayedEnchantments[j].GetCardId()))
				{
					hashSet.Add(displayedEnchantments[j].GetCardId());
				}
				else
				{
					m_uniqueEnchantmentLookup[displayedEnchantments[j].GetCardId()].IncrementEnchantmentMultiplier((uint)Mathf.Max(displayedEnchantments[j].GetTag(GAME_TAG.SPAWN_TIME_COUNT), 1));
				}
			}
		}
		LayoutEnchantments(gameObject);
		SceneUtils.SetLayer(gameObject, GameLayer.Tooltip);
	}

	private void ResetEnchantments()
	{
		m_EnchantmentBanner.SetActive(value: false);
		m_EnchantmentBannerBottom.SetActive(value: false);
		m_EnchantmentBannerText.gameObject.SetActive(value: false);
		m_EnchantmentBanner.transform.parent = base.transform;
		m_EnchantmentBannerBottom.transform.parent = base.transform;
		m_EnchantmentBannerText.transform.parent = base.transform;
		foreach (BigCardEnchantmentPanel active in m_enchantmentPool.GetActiveList())
		{
			active.transform.parent = base.transform;
			active.ResetScale();
			active.Hide();
		}
	}

	private void LayoutEnchantments(GameObject bone)
	{
		float num = 0.1f;
		List<BigCardEnchantmentPanel> activeList = m_enchantmentPool.GetActiveList();
		BigCardEnchantmentPanel bigCardEnchantmentPanel = null;
		for (int i = 0; i < activeList.Count; i++)
		{
			BigCardEnchantmentPanel bigCardEnchantmentPanel2 = activeList[i];
			bigCardEnchantmentPanel2.Show();
			bigCardEnchantmentPanel2.transform.localScale *= (float)PLATFORM_SCALING_FACTOR * (float)ENCHANTMENT_SCALING_FACTOR;
			if (i == 0)
			{
				TransformUtil.SetPoint(bigCardEnchantmentPanel2.gameObject, new Vector3(0.5f, 0f, 1f), m_bigCardActor.GetMeshRenderer().gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0.01f, 0.01f, 0f));
			}
			else
			{
				TransformUtil.SetPoint(bigCardEnchantmentPanel2.gameObject, new Vector3(0f, 0f, 1f), bigCardEnchantmentPanel.gameObject, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
			}
			bigCardEnchantmentPanel = bigCardEnchantmentPanel2;
			bigCardEnchantmentPanel2.transform.parent = bone.transform;
			float height = bigCardEnchantmentPanel2.GetHeight();
			num += height;
		}
		if (m_card != null && m_card.GetEntity() != null && m_card.GetEntity().HasTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT))
		{
			string clientString = GameDbf.GetIndex().GetClientString(m_card.GetEntity().GetTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT));
			UpdateEnchantmentBannerText(bone, bigCardEnchantmentPanel, clientString);
			num += m_EnchantmentBannerText.Height;
		}
		else if (m_card != null && m_card.GetEntity() != null && m_card.GetEntity().IsSideQuest())
		{
			string customBannerTextString = GameStrings.Format("GLUE_SIDEQUEST_PROGRESS_BANNER", m_card.GetEntity().GetTag(GAME_TAG.QUEST_PROGRESS), m_card.GetEntity().GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL));
			UpdateEnchantmentBannerText(bone, bigCardEnchantmentPanel, customBannerTextString);
			num += m_EnchantmentBannerText.Height;
		}
		else
		{
			m_EnchantmentBannerText.gameObject.SetActive(value: false);
		}
		m_EnchantmentBanner.SetActive(value: true);
		m_EnchantmentBannerBottom.SetActive(value: true);
		m_EnchantmentBannerBottom.transform.localScale = m_initialBannerBottomScale * PLATFORM_SCALING_FACTOR * ENCHANTMENT_SCALING_FACTOR;
		m_EnchantmentBanner.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		m_EnchantmentBannerBottom.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		TransformUtil.SetPoint(m_EnchantmentBanner, new Vector3(0.5f, 0f, 1f), m_bigCardActor.GetMeshRenderer().gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, 0.2f));
		m_EnchantmentBanner.transform.localScale = m_initialBannerScale * PLATFORM_SCALING_FACTOR * ENCHANTMENT_SCALING_FACTOR;
		TransformUtil.SetLocalScaleZ(m_EnchantmentBanner.gameObject, num / m_initialBannerHeight / m_initialBannerScale.z);
		m_EnchantmentBanner.transform.parent = bone.transform;
		TransformUtil.SetPoint(m_EnchantmentBannerBottom, Anchor.FRONT, m_EnchantmentBanner, Anchor.BACK);
		m_EnchantmentBannerBottom.transform.parent = bone.transform;
		m_EnchantmentBannerBottom.transform.position += new Vector3(0f, -0.01f, 0.01f);
	}

	private void UpdateEnchantmentBannerText(GameObject bone, BigCardEnchantmentPanel prevPanel, string customBannerTextString)
	{
		m_EnchantmentBannerText.transform.localScale = m_initialBannerTextScale * PLATFORM_SCALING_FACTOR * ENCHANTMENT_SCALING_FACTOR;
		m_EnchantmentBannerText.transform.parent = bone.transform;
		if (prevPanel == null)
		{
			m_EnchantmentBannerText.transform.localPosition = new Vector3(0f, 0f, -0.25f);
		}
		else
		{
			TransformUtil.SetPoint(m_EnchantmentBannerText.gameObject, new Vector3(0.5f, 0f, 1f), prevPanel.gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, -0.05f));
		}
		m_EnchantmentBannerText.gameObject.SetActive(value: true);
		m_EnchantmentBannerText.Text = customBannerTextString;
	}

	private void FitInsideScreen()
	{
		FitInsideScreenBottom();
		FitInsideScreenTop();
	}

	private bool FitInsideScreenBottom()
	{
		Bounds bounds = (m_EnchantmentBanner.activeInHierarchy ? m_EnchantmentBannerBottom.GetComponent<Renderer>().bounds : m_bigCardActor.GetMeshRenderer().bounds);
		Vector3 center = bounds.center;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			center.z -= 0.4f;
		}
		Vector3 vector = new Vector3(center.x, center.y, center.z - bounds.extents.z);
		Ray ray = new Ray(vector, vector - center);
		Plane plane = CameraUtils.CreateBottomPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			return false;
		}
		if (Mathf.Approximately(enter, 0f))
		{
			return false;
		}
		TransformUtil.SetPosZ(m_bigCardActor.gameObject, m_bigCardActor.transform.position.z - enter);
		return true;
	}

	private Bounds CalculateMeshBoundsIncludingGem(Actor actor = null)
	{
		if (actor == null)
		{
			actor = m_bigCardActor;
		}
		Bounds bounds = actor.GetMeshRenderer().bounds;
		if (actor != null && actor.GetEntity() != null && (actor.GetEntity().IsSideQuest() || actor.GetEntity().IsSigil()))
		{
			MeshRenderer[] componentsInChildren = actor.GetRootObject().GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				if (meshRenderer.gameObject.name.Equals("gem_mana", StringComparison.InvariantCultureIgnoreCase))
				{
					Bounds bounds2 = meshRenderer.bounds;
					bounds.Encapsulate(bounds2);
					break;
				}
			}
		}
		return bounds;
	}

	private bool FitInsideScreenTop()
	{
		Bounds bounds = CalculateMeshBoundsIncludingGem();
		Vector3 center = bounds.center;
		if ((bool)UniversalInputManager.UsePhoneUI && !(m_card.GetZone() is ZoneHeroPower))
		{
			center.z += 1f;
		}
		Vector3 vector = new Vector3(center.x, center.y, center.z + bounds.extents.z);
		Ray ray = new Ray(vector, vector - center);
		Plane plane = CameraUtils.CreateTopPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			return false;
		}
		if (Mathf.Approximately(enter, 0f))
		{
			return false;
		}
		TransformUtil.SetPosZ(m_bigCardActor.gameObject, m_bigCardActor.transform.position.z + enter);
		return true;
	}

	private BigCardEnchantmentPanel CreateEnchantmentPanel(int index)
	{
		BigCardEnchantmentPanel bigCardEnchantmentPanel = UnityEngine.Object.Instantiate(m_EnchantmentPanelPrefab);
		bigCardEnchantmentPanel.name = $"{typeof(BigCardEnchantmentPanel).ToString()}{index}";
		SceneUtils.SetRenderQueue(bigCardEnchantmentPanel.gameObject, m_RenderQueueEnchantmentPanel);
		return bigCardEnchantmentPanel;
	}

	private void DestroyEnchantmentPanel(BigCardEnchantmentPanel panel)
	{
		UnityEngine.Object.Destroy(panel.gameObject);
	}

	public void ActivateBigCardStateSpells(Entity entity, Actor cardActor, Actor bigCardActor)
	{
		if (cardActor.UseTechLevelManaGem())
		{
			Spell spell = bigCardActor.GetSpell(SpellType.TECH_LEVEL_MANA_GEM);
			if (spell != null)
			{
				spell.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = entity.GetTechLevel();
				spell.ActivateState(SpellStateType.BIRTH);
			}
		}
		if (cardActor.UseCoinManaGem())
		{
			bigCardActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
		}
	}

	private void LoadAndDisplayTooltipPhoneSigils()
	{
		if (m_phoneSigilActors == null)
		{
			m_phoneSigilActors = new List<Actor>();
		}
		else
		{
			foreach (Actor phoneSigilActor in m_phoneSigilActors)
			{
				phoneSigilActor.Destroy();
			}
			m_phoneSigilActors.Clear();
		}
		ZoneSecret zoneSecret = m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSigils() called for a card that is not in a Secret Zone.");
			return;
		}
		List<Card> sigilCards = zoneSecret.GetSigilCards();
		for (int i = 0; i < sigilCards.Count; i++)
		{
			Actor item = LoadPhoneSecret(sigilCards[i]);
			m_phoneSigilActors.Add(item);
		}
		DisplayPhoneSecrets(m_card, m_phoneSigilActors, showDeath: false);
	}

	private void HideTooltipPhoneSigils()
	{
		if (m_phoneSigilActors == null)
		{
			return;
		}
		foreach (Actor phoneSigilActor in m_phoneSigilActors)
		{
			HidePhoneSecret(phoneSigilActor);
		}
		m_phoneSigilActors.Clear();
	}

	private void LoadAndDisplayTooltipPhoneSecrets()
	{
		if (m_phoneSecretActors == null)
		{
			m_phoneSecretActors = new List<Actor>();
		}
		else
		{
			foreach (Actor phoneSecretActor in m_phoneSecretActors)
			{
				phoneSecretActor.Destroy();
			}
			m_phoneSecretActors.Clear();
		}
		ZoneSecret zoneSecret = m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSecrets() called for a card that is not in a Secret Zone.");
			return;
		}
		List<Card> secretCards = zoneSecret.GetSecretCards();
		for (int i = 0; i < secretCards.Count; i++)
		{
			Actor item = LoadPhoneSecret(secretCards[i]);
			m_phoneSecretActors.Add(item);
		}
		DisplayPhoneSecrets(m_card, m_phoneSecretActors, showDeath: false);
	}

	private void HideTooltipPhoneSecrets()
	{
		if (m_phoneSecretActors == null)
		{
			return;
		}
		foreach (Actor phoneSecretActor in m_phoneSecretActors)
		{
			HidePhoneSecret(phoneSecretActor);
		}
		m_phoneSecretActors.Clear();
	}

	private void LoadAndDisplayTooltipPhoneSideQuests()
	{
		if (m_phoneSideQuestActors == null)
		{
			m_phoneSideQuestActors = new List<Actor>();
		}
		else
		{
			foreach (Actor phoneSideQuestActor in m_phoneSideQuestActors)
			{
				phoneSideQuestActor.Destroy();
			}
			m_phoneSideQuestActors.Clear();
		}
		ZoneSecret zoneSecret = m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSideQuests() called for a card that is not in a Secret Zone.");
			return;
		}
		List<Card> sideQuestCards = zoneSecret.GetSideQuestCards();
		for (int i = 0; i < sideQuestCards.Count; i++)
		{
			Actor item = LoadPhoneSecret(sideQuestCards[i]);
			m_phoneSideQuestActors.Add(item);
		}
		DisplayPhoneSecrets(m_card, m_phoneSideQuestActors, showDeath: false);
	}

	private void HideTooltipPhoneSideQuests()
	{
		if (m_phoneSideQuestActors == null)
		{
			return;
		}
		foreach (Actor phoneSideQuestActor in m_phoneSideQuestActors)
		{
			HidePhoneSecret(phoneSideQuestActor);
		}
		m_phoneSideQuestActors.Clear();
	}

	private Actor LoadPhoneSecret(Card card)
	{
		string bigCardActor = ActorNames.GetBigCardActor(card.GetEntity());
		Actor component = AssetLoader.Get().InstantiatePrefab(bigCardActor, AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
		SetupActor(card, component);
		return component;
	}

	private Vector3 PhoneMoveSideQuestBigCardToTopOfScreen(Actor actor, Vector3 initialPosition)
	{
		if (actor == null || !UniversalInputManager.UsePhoneUI)
		{
			return initialPosition;
		}
		Vector3 position = actor.transform.position;
		try
		{
			actor.transform.position = initialPosition;
			Bounds bounds = CalculateMeshBoundsIncludingGem(actor);
			Vector3 center = bounds.center;
			Vector3 vector = new Vector3(center.x, center.y, center.z + bounds.extents.z);
			Ray ray = new Ray(vector, vector - center);
			Plane plane = CameraUtils.CreateTopPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
			float enter = 0f;
			plane.Raycast(ray, out enter);
			return initialPosition + new Vector3(0f, 0f, enter);
		}
		finally
		{
			actor.transform.position = position;
		}
	}

	private void DisplayPhoneSecrets(Card mainCard, List<Actor> actors, bool showDeath)
	{
		DetermineSecretLayoutOffsets(mainCard, actors, out var initialOffset, out var spacing, out var drift);
		bool flag = GeneralUtils.IsOdd(actors.Count);
		Player controller = mainCard.GetController();
		ZoneSecret secretZone = controller.GetSecretZone();
		Actor actor = mainCard.GetActor();
		Vector3 vector = secretZone.transform.position + initialOffset;
		for (int i = 0; i < actors.Count; i++)
		{
			Actor actor2 = actors[i];
			Vector3 vector2;
			if (i == 0 && flag)
			{
				vector2 = ((actors.Count != 1 || !actor2.GetCard().GetEntity().IsSideQuest() || !controller.IsFriendlySide()) ? vector : PhoneMoveSideQuestBigCardToTopOfScreen(actor2, vector));
			}
			else
			{
				bool flag2 = GeneralUtils.IsOdd(i);
				bool flag3 = flag == flag2;
				float num = (flag ? Mathf.Ceil(0.5f * (float)i) : Mathf.Floor(0.5f * (float)i));
				float num2 = num * spacing.x;
				if (!flag)
				{
					num2 += 0.5f * spacing.x;
				}
				if (flag3)
				{
					num2 = 0f - num2;
				}
				float num3 = num * spacing.z;
				vector2 = new Vector3(vector.x + num2, vector.y, vector.z + num3);
			}
			actor2.transform.position = actor.transform.position;
			actor2.transform.rotation = actor.transform.rotation;
			actor2.transform.localScale = INVISIBLE_SCALE;
			float num4 = (showDeath ? m_SecretLayoutData.m_DeathShowAnimTime : m_SecretLayoutData.m_ShowAnimTime);
			Hashtable args = iTween.Hash("position", vector2 - drift, "time", num4, "easeType", iTween.EaseType.easeOutExpo);
			iTween.MoveTo(actor2.gameObject, args);
			Hashtable args2 = iTween.Hash("position", vector2, "delay", num4, "time", m_SecretLayoutData.m_DriftSec, "easeType", iTween.EaseType.easeOutExpo);
			iTween.MoveTo(actor2.gameObject, args2);
			iTween.ScaleTo(actor2.gameObject, base.transform.localScale, num4);
			if (mainCard.GetEntity().IsSideQuest())
			{
				actor2.ShowSideQuestProgressBanner();
			}
			else
			{
				actor2.HideSideQuestProgressBanner();
			}
			if (showDeath)
			{
				ShowPhoneSecretDeath(actor2);
			}
		}
	}

	private void DetermineSecretLayoutOffsets(Card mainCard, List<Actor> actors, out Vector3 initialOffset, out Vector3 spacing, out Vector3 drift)
	{
		Player controller = mainCard.GetController();
		bool flag = controller.IsFriendlySide();
		bool num = controller.IsRevealed();
		int minCardThreshold = m_SecretLayoutData.m_MinCardThreshold;
		int maxCardThreshold = m_SecretLayoutData.m_MaxCardThreshold;
		SecretLayoutOffsets minCardOffsets = m_SecretLayoutData.m_MinCardOffsets;
		SecretLayoutOffsets maxCardOffsets = m_SecretLayoutData.m_MaxCardOffsets;
		float t = Mathf.InverseLerp(minCardThreshold, maxCardThreshold, actors.Count);
		if (num)
		{
			if (flag)
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_InitialOffset, maxCardOffsets.m_InitialOffset, t);
			}
			else
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_OpponentInitialOffset, maxCardOffsets.m_OpponentInitialOffset, t);
			}
			spacing = m_SecretLayoutData.m_Spacing;
		}
		else
		{
			if (flag)
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_HiddenInitialOffset, maxCardOffsets.m_HiddenInitialOffset, t);
			}
			else
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_HiddenOpponentInitialOffset, maxCardOffsets.m_HiddenOpponentInitialOffset, t);
			}
			spacing = m_SecretLayoutData.m_HiddenSpacing;
		}
		if (flag)
		{
			spacing.z = 0f - spacing.z;
			drift = m_SecretLayoutData.m_DriftOffset;
		}
		else
		{
			drift = -m_SecretLayoutData.m_DriftOffset;
		}
	}

	private void ShowPhoneSecretDeath(Actor actor)
	{
		Spell.StateFinishedCallback deathSpellStateFinished = delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() != 0)
			{
				actor.Destroy();
			}
		};
		Action<object> action = delegate
		{
			Spell spell2 = actor.GetSpell(SpellType.DEATH);
			spell2.AddStateFinishedCallback(deathSpellStateFinished);
			spell2.Activate();
		};
		Hashtable args = iTween.Hash("time", m_SecretLayoutData.m_TimeUntilDeathSpell, "oncomplete", action);
		iTween.Timer(actor.gameObject, args);
	}

	private void HidePhoneSecret(Actor actor)
	{
		Actor actor2 = m_card.GetActor();
		iTween.MoveTo(actor.gameObject, actor2.transform.position, m_SecretLayoutData.m_HideAnimTime);
		iTween.ScaleTo(actor.gameObject, INVISIBLE_SCALE, m_SecretLayoutData.m_HideAnimTime);
		Action<object> action = delegate
		{
			actor.Destroy();
		};
		Hashtable args = iTween.Hash("time", m_SecretLayoutData.m_HideAnimTime, "oncomplete", action);
		iTween.Timer(actor.gameObject, args);
	}

	private void SetupActor(Card card, Actor actor)
	{
		bool ignoreHideStats = false;
		Entity entity = card.GetEntity();
		if (ShouldActorUseEntity(entity))
		{
			actor.SetEntity(entity);
			ignoreHideStats = entity.HasTag(GAME_TAG.IGNORE_HIDE_STATS_FOR_BIG_CARD) || (entity.IsDormant() && !entity.HasTag(GAME_TAG.HIDE_STATS));
		}
		GhostCard.Type ghostType = (GhostCard.Type)entity.GetTag(GAME_TAG.MOUSE_OVER_CARD_APPEARANCE);
		if (card.GetEntity().IsDormant())
		{
			ghostType = GhostCard.Type.DORMANT;
		}
		actor.GhostCardEffect(ghostType, entity.GetPremiumType(), update: false);
		EntityDef entityDef = entity.GetEntityDef();
		DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef();
		int num = entity.GetTag(GAME_TAG.ALTERNATE_MOUSE_OVER_CARD);
		if (num != 0)
		{
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(num);
			if (entityDef2 == null)
			{
				Log.Gameplay.PrintError("BigCard.SetupActor(): Unable to load EntityDef for card ID {0}.", num);
			}
			else
			{
				entityDef = entityDef2;
			}
			DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(num);
			if (cardDef == null)
			{
				Log.Spells.PrintError("BigCard.SetupActor(): Unable to load CardDef for card ID {0}.", num);
			}
			else
			{
				disposableCardDef?.Dispose();
				disposableCardDef = cardDef;
			}
		}
		using (disposableCardDef)
		{
			if (ShouldActorUseEntityDef(entity))
			{
				actor.SetEntityDef(entityDef);
				ignoreHideStats = entityDef.HasTag(GAME_TAG.IGNORE_HIDE_STATS_FOR_BIG_CARD) || entityDef.IsDormant();
			}
			actor.SetPremium(entity.GetPremiumType());
			actor.SetCard(card);
			actor.SetCardDef(disposableCardDef);
			actor.SetIgnoreHideStats(ignoreHideStats);
			actor.SetWatermarkCardSetOverride(entity.GetWatermarkCardSetOverride());
			actor.UpdateAllComponents();
			ActivateBigCardStateSpells(entity, card.GetActor(), actor);
			BoxCollider componentInChildren = actor.GetComponentInChildren<BoxCollider>();
			if (componentInChildren != null)
			{
				componentInChildren.enabled = false;
			}
			actor.name = "BigCard_" + actor.name;
			SceneUtils.SetLayer(actor, GameLayer.Tooltip);
		}
	}

	private bool ShouldActorUseEntity(Entity entity)
	{
		if (entity.IsHidden())
		{
			return true;
		}
		if ((entity.GetZone() == TAG_ZONE.PLAY || entity.GetZone() == TAG_ZONE.SECRET) && entity.GetCardTextBuilder().ShouldUseEntityForTextInPlay())
		{
			return true;
		}
		if (entity.IsDormant())
		{
			return true;
		}
		if (entity.IsSideQuest() || entity.IsSigil() || entity.IsSecret())
		{
			return true;
		}
		if (entity.IsHeroPowerOrGameModeButton())
		{
			return true;
		}
		if (GameMgr.Get().IsSpectator() && entity.GetZone() == TAG_ZONE.HAND && entity.GetController().IsOpposingSide())
		{
			return true;
		}
		return false;
	}

	private bool ShouldActorUseEntityDef(Entity entity)
	{
		if (entity.IsHidden())
		{
			return false;
		}
		if (entity.IsHeroPowerOrGameModeButton())
		{
			return false;
		}
		if (entity.IsDormant())
		{
			return false;
		}
		if (entity.GetZone() == TAG_ZONE.SECRET)
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator() && entity.GetZone() == TAG_ZONE.HAND && entity.GetController().IsOpposingSide())
		{
			return false;
		}
		return true;
	}
}
