using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020002FD RID: 765
[CustomEditClass]
public class BigCard : MonoBehaviour
{
	// Token: 0x0600289B RID: 10395 RVA: 0x000CBF38 File Offset: 0x000CA138
	public BigCard()
	{
		this.PLATFORM_SCALING_FACTOR = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 1.3f,
			MiniTablet = 1f
		};
		this.ENCHANTMENT_SCALING_FACTOR = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 1.5f,
			MiniTablet = 1f
		};
	}

	// Token: 0x0600289C RID: 10396 RVA: 0x000CBFE0 File Offset: 0x000CA1E0
	private void Awake()
	{
		BigCard.s_instance = this;
		this.m_initialBannerHeight = this.m_EnchantmentBanner.GetComponent<Renderer>().bounds.size.z;
		this.m_initialBannerScale = this.m_EnchantmentBanner.transform.localScale;
		this.m_initialBannerBottomScale = this.m_EnchantmentBannerBottom.transform.localScale;
		this.m_initialBannerTextScale = this.m_EnchantmentBannerText.transform.localScale;
		this.m_enchantmentPool.SetCreateItemCallback(new Pool<BigCardEnchantmentPanel>.CreateItemCallback(this.CreateEnchantmentPanel));
		this.m_enchantmentPool.SetDestroyItemCallback(new Pool<BigCardEnchantmentPanel>.DestroyItemCallback(this.DestroyEnchantmentPanel));
		this.m_enchantmentPool.SetExtensionCount(1);
		this.m_enchantmentPool.SetMaxReleasedItemCount(2);
		this.ResetEnchantments();
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x000CC0A4 File Offset: 0x000CA2A4
	private void OnDestroy()
	{
		BigCard.s_instance = null;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x000CC0AC File Offset: 0x000CA2AC
	public static BigCard Get()
	{
		return BigCard.s_instance;
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x000CC0B3 File Offset: 0x000CA2B3
	public Card GetCard()
	{
		return this.m_card;
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x000CC0BC File Offset: 0x000CA2BC
	public void Show(Card card)
	{
		this.m_card = card;
		if (GameState.Get() != null && !GameState.Get().GetGameEntity().NotifyOfCardTooltipDisplayShow(card))
		{
			return;
		}
		Zone zone = card.GetZone();
		if (!UniversalInputManager.UsePhoneUI || !(zone is ZoneSecret))
		{
			this.LoadAndDisplayBigCard();
			return;
		}
		if (card.GetEntity().IsSideQuest())
		{
			this.LoadAndDisplayTooltipPhoneSideQuests();
			return;
		}
		if (card.GetEntity().IsSigil())
		{
			this.LoadAndDisplayTooltipPhoneSigils();
			return;
		}
		this.LoadAndDisplayTooltipPhoneSecrets();
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x000CC13A File Offset: 0x000CA33A
	public void Hide()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardTooltipDisplayHide(this.m_card);
		}
		this.HideBigCard();
		this.HideTooltipPhoneSecrets();
		this.HideTooltipPhoneSideQuests();
		this.HideTooltipPhoneSigils();
		this.m_card = null;
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x000CC177 File Offset: 0x000CA377
	public bool Hide(Card card)
	{
		if (this.m_card != card)
		{
			return false;
		}
		this.Hide();
		return true;
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x000CC190 File Offset: 0x000CA390
	public void ShowSecretDeaths(Map<Player, DeadSecretGroup> deadSecretMap)
	{
		if (deadSecretMap == null)
		{
			return;
		}
		if (deadSecretMap.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (DeadSecretGroup deadSecretGroup in deadSecretMap.Values)
		{
			Card mainCard = deadSecretGroup.GetMainCard();
			List<Card> cards = deadSecretGroup.GetCards();
			List<Actor> list = new List<Actor>();
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				Actor item = this.LoadPhoneSecret(card);
				list.Add(item);
			}
			this.DisplayPhoneSecrets(mainCard, list, true);
			num++;
		}
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x000CC240 File Offset: 0x000CA440
	private void LoadAndDisplayBigCard()
	{
		if (this.m_bigCardActor)
		{
			this.m_bigCardActor.Destroy();
		}
		if (ActorNames.ShouldDisplayTooltipInsteadOfBigCard(this.m_card.GetEntity()))
		{
			this.DisplayBigCardAsTooltip();
			return;
		}
		string text = ActorNames.GetBigCardActor(this.m_card.GetEntity());
		if (text == "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9")
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_bigCardActor = gameObject.GetComponent<Actor>();
		this.SetupActor(this.m_card, this.m_bigCardActor);
		int tag = this.m_card.GetEntity().GetTag(GAME_TAG.DISGUISED_TWIN);
		if (tag > 0)
		{
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(tag))
			{
				text = ActorNames.GetHandActor((fullDef != null) ? fullDef.EntityDef : null, this.m_card.GetEntity().GetPremiumType());
				GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
				this.m_twinCardActor = gameObject2.GetComponent<Actor>();
				SceneUtils.SetLayer(this.m_twinCardActor, GameLayer.Tooltip);
				this.m_twinCardActor.SetFullDef(fullDef);
				this.m_twinCardActor.SetPremium(this.m_card.GetEntity().GetPremiumType());
				this.m_twinCardActor.SetCardBackSideOverride(new Player.Side?(this.m_card.GetEntity().GetControllerSide()));
				this.m_twinCardActor.SetWatermarkCardSetOverride(this.m_card.GetEntity().GetWatermarkCardSetOverride());
				this.m_twinCardActor.UpdateAllComponents();
			}
		}
		this.DisplayBigCard();
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x000CC3D4 File Offset: 0x000CA5D4
	private void HideBigCard()
	{
		if (this.m_bigCardActor)
		{
			this.ResetEnchantments();
			iTween.Stop(this.m_bigCardActor.gameObject);
			this.m_bigCardActor.Destroy();
			this.m_bigCardActor = null;
			TooltipPanelManager.Get().HideKeywordHelp();
		}
		if (this.m_bigCardAsTooltip)
		{
			UnityEngine.Object.Destroy(this.m_bigCardAsTooltip);
		}
		if (this.m_twinCardActor)
		{
			iTween.Stop(this.m_twinCardActor.gameObject);
			this.m_twinCardActor.Destroy();
			this.m_twinCardActor = null;
		}
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x000CC468 File Offset: 0x000CA668
	private void DisplayBigCardAsTooltip()
	{
		if (this.m_bigCardAsTooltip != null)
		{
			UnityEngine.Object.Destroy(this.m_bigCardAsTooltip);
		}
		Vector3 b;
		if (this.ShowBigCardOnRight())
		{
			b = new Vector3(2f, 0f, 0f);
		}
		else
		{
			b = new Vector3(-2f, 0f, 0f);
		}
		Vector3 position = this.m_card.transform.position + b;
		this.m_bigCardAsTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		this.m_bigCardAsTooltip.Reset();
		this.m_bigCardAsTooltip.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		this.m_bigCardAsTooltip.Initialize(this.m_card.GetEntity().GetName(), this.m_card.GetEntity().GetCardTextInHand());
		this.m_bigCardAsTooltip.transform.position = position;
		RenderUtils.SetAlpha(this.m_bigCardAsTooltip.gameObject, 0f);
		iTween.FadeTo(this.m_bigCardAsTooltip.gameObject, iTween.Hash(new object[]
		{
			"alpha",
			1,
			"time",
			0.1f
		}));
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
	private void DisplayBigCard()
	{
		Entity entity = this.m_card.GetEntity();
		bool flag = entity.GetController().IsFriendlySide();
		Zone zone = this.m_card.GetZone();
		Bounds bounds = this.m_bigCardActor.GetMeshRenderer(false).bounds;
		Vector3 position = this.m_card.GetActor().transform.position;
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		Vector3 vector3 = new Vector3(1.1f, 1.1f, 1.1f);
		float? overrideScale = null;
		if (zone is ZoneHero)
		{
			if (flag)
			{
				vector = new Vector3(0f, 4f, 0f);
			}
			else
			{
				vector = new Vector3(0f, 4f, -bounds.size.z * 0.7f);
			}
		}
		else if (zone is ZoneWeapon)
		{
			if (UniversalInputManager.UsePhoneUI)
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
				if (flag)
				{
					vector = new Vector3(0f, 0f, bounds.size.z * 0.9f);
				}
				else
				{
					vector = new Vector3(-1.57f, 0f, -1f);
				}
			}
			vector3 *= this.PLATFORM_SCALING_FACTOR;
		}
		else if (zone is ZoneHeroPower)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				vector3 = new Vector3(1.3f, 1f, 1.3f);
				if (flag)
				{
					vector = new Vector3(-3.5f, 8f, 3.5f);
				}
				else
				{
					vector = new Vector3(-3.5f, 8f, -3.35f);
				}
			}
			else if (flag)
			{
				vector = new Vector3(0f, 4f, 2.69f);
			}
			else
			{
				vector = new Vector3(0f, 4f, -2.6f);
			}
			overrideScale = new float?(0.6f);
		}
		else if (zone is ZoneSecret)
		{
			vector3 = new Vector3(1.65f, 1.65f, 1.65f);
			vector = new Vector3(bounds.size.x + 0.3f, 0f, 0f);
		}
		else if (zone is ZoneHand)
		{
			vector = new Vector3(bounds.size.x * 0.7f, 4f, -bounds.size.z * 0.8f);
			vector3 = new Vector3(1.65f, 1.65f, 1.65f);
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				vector3 = new Vector3(2f, 2f, 2f);
				if (this.ShowBigCardOnRight())
				{
					vector = new Vector3(bounds.size.x + 2.2f, 0f, 0f);
				}
				else
				{
					vector = new Vector3(-bounds.size.x - 2.2f, 0f, 0f);
				}
			}
			else
			{
				vector3 = new Vector3(1.65f, 1.65f, 1.65f);
				if (this.m_twinCardActor)
				{
					if (UnityEngine.Random.Range(0, 2) == 0)
					{
						vector = new Vector3(bounds.size.x + 0.7f, 0f, 0f);
						vector2 = new Vector3(-bounds.size.x - 0.7f, 0f, 0f);
					}
					else
					{
						vector = new Vector3(-bounds.size.x - 0.7f, 0f, 0f);
						vector2 = new Vector3(bounds.size.x + 0.7f, 0f, 0f);
					}
				}
				else if (this.ShowBigCardOnRight())
				{
					vector = new Vector3(bounds.size.x + 0.7f, 0f, 0f);
				}
				else
				{
					vector = new Vector3(-bounds.size.x - 0.7f, 0f, 0f);
				}
			}
			if (zone is ZonePlay)
			{
				vector += new Vector3(0f, 0.1f, 0f);
				vector2 += new Vector3(0f, 0.1f, 0f);
				vector3 *= this.PLATFORM_SCALING_FACTOR;
			}
		}
		Vector3 b = new Vector3(0.02f, 0.02f, 0.02f);
		Vector3 position2 = position + vector + b;
		Vector3 localScale = new Vector3(1f, 1f, 1f);
		Transform parent = this.m_bigCardActor.transform.parent;
		this.m_bigCardActor.transform.localScale = vector3;
		this.m_bigCardActor.transform.position = position2;
		this.m_bigCardActor.transform.parent = null;
		Transform parent2 = null;
		if (this.m_twinCardActor)
		{
			parent2 = this.m_twinCardActor.transform.parent;
		}
		Vector3 position3 = position + vector2 + b;
		if (this.m_card.GetEntity().GetTag(GAME_TAG.DISGUISED_TWIN) > 0 && this.m_twinCardActor != null)
		{
			this.m_twinCardActor.transform.localScale = vector3;
			this.m_twinCardActor.transform.position = position3;
			this.m_twinCardActor.transform.parent = null;
		}
		if (zone is ZoneHand)
		{
			this.m_bigCardActor.SetEntity(entity);
			this.m_bigCardActor.UpdateTextComponents(entity);
		}
		else
		{
			if (this.m_twinCardActor == null)
			{
				this.UpdateEnchantments();
			}
			else
			{
				this.ResetEnchantments();
			}
			if (UniversalInputManager.UsePhoneUI && this.m_EnchantmentBanner.activeInHierarchy)
			{
				float d = (this.m_enchantmentPool.GetActiveList().Count > 1) ? 0.75f : 0.85f;
				vector3 *= d;
				this.m_bigCardActor.transform.localScale = vector3;
			}
		}
		this.FitInsideScreen();
		this.m_bigCardActor.transform.parent = parent;
		this.m_bigCardActor.transform.localScale = localScale;
		if (this.m_twinCardActor)
		{
			this.m_twinCardActor.transform.parent = parent2;
			this.m_twinCardActor.transform.localScale = localScale;
		}
		Vector3 position4 = this.m_bigCardActor.transform.position;
		this.m_bigCardActor.transform.position -= b;
		Vector3 position5 = new Vector3(0f, 0f, 0f);
		if (this.m_twinCardActor)
		{
			position5 = this.m_twinCardActor.transform.position;
			this.m_twinCardActor.transform.position -= b;
		}
		BigCard.KeywordArgs keywordArgs = default(BigCard.KeywordArgs);
		keywordArgs.card = this.m_card;
		keywordArgs.actor = this.m_bigCardActor;
		keywordArgs.showOnRight = this.ShowBigCardOnRight();
		if (zone is ZoneHand)
		{
			object[] array = new object[8];
			array[0] = "scale";
			array[1] = vector3;
			array[2] = "time";
			array[3] = this.m_LayoutData.m_ScaleSec;
			array[4] = "oncompleteparams";
			array[5] = keywordArgs;
			array[6] = "oncomplete";
			array[7] = new Action<object>(delegate(object obj)
			{
				BigCard.KeywordArgs keywordArgs3 = (BigCard.KeywordArgs)obj;
				TooltipPanelManager.Get().UpdateKeywordHelp(keywordArgs3.card, keywordArgs3.actor, keywordArgs3.showOnRight, null, null);
			});
			Hashtable args = iTween.Hash(array);
			iTween.ScaleTo(this.m_bigCardActor.gameObject, args);
		}
		else
		{
			iTween.ScaleTo(this.m_bigCardActor.gameObject, vector3, this.m_LayoutData.m_ScaleSec);
			if (this.m_twinCardActor != null)
			{
				BigCard.KeywordArgs keywordArgs2 = default(BigCard.KeywordArgs);
				keywordArgs2.card = this.m_card;
				keywordArgs2.actor = this.m_twinCardActor;
				keywordArgs2.showOnRight = this.ShowBigCardOnRight();
				iTween.ScaleTo(this.m_twinCardActor.gameObject, vector3, this.m_LayoutData.m_ScaleSec);
				iTween.MoveTo(this.m_twinCardActor.gameObject, position5, this.m_LayoutData.m_DriftSec);
				this.m_twinCardActor.transform.rotation = Quaternion.identity;
				this.m_twinCardActor.Show();
			}
			else
			{
				TooltipPanelManager.Get().UpdateKeywordHelp(keywordArgs.card, keywordArgs.actor, keywordArgs.showOnRight, overrideScale, null);
			}
		}
		iTween.MoveTo(this.m_bigCardActor.gameObject, position4, this.m_LayoutData.m_DriftSec);
		this.m_bigCardActor.transform.rotation = Quaternion.identity;
		this.m_bigCardActor.Show();
		if (UniversalInputManager.UsePhoneUI)
		{
			TooltipPanelManager.Get().UpdateKeywordHelp(this.m_card, this.m_bigCardActor, this.ShowKeywordOnRight(), overrideScale, null);
		}
		if (entity.IsSilenced())
		{
			this.m_bigCardActor.ActivateSpellBirthState(SpellType.SILENCE);
			if (this.m_twinCardActor != null)
			{
				this.m_twinCardActor.ActivateSpellBirthState(SpellType.SILENCE);
			}
		}
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x000CCF76 File Offset: 0x000CB176
	private bool ShowBigCardOnRight()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return this.ShowBigCardOnRightTouch();
		}
		return this.ShowBigCardOnRightMouse();
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x000CCF94 File Offset: 0x000CB194
	private bool ShowBigCardOnRightMouse()
	{
		if (this.m_card.GetEntity().IsHero() || this.m_card.GetEntity().IsHeroPower() || this.m_card.GetEntity().IsSecret())
		{
			return true;
		}
		if (this.m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = this.m_card.GetZone() as ZonePlay;
		if (zonePlay != null)
		{
			Actor actor = this.m_card.GetActor();
			if (actor != null)
			{
				MeshRenderer meshRenderer = actor.GetMeshRenderer(false);
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

	// Token: 0x060028AA RID: 10410 RVA: 0x000CD070 File Offset: 0x000CB270
	private bool ShowBigCardOnRightTouch()
	{
		if (this.m_card.GetEntity().IsHero() || this.m_card.GetEntity().IsHeroPower() || this.m_card.GetEntity().IsSecret())
		{
			return false;
		}
		if (this.m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = this.m_card.GetZone() as ZonePlay;
		if (zonePlay != null)
		{
			float num = UniversalInputManager.UsePhoneUI ? 0f : -2.5f;
			return this.m_card.GetActor().GetMeshRenderer(false).bounds.center.x < zonePlay.GetComponent<BoxCollider>().bounds.center.x + num;
		}
		return false;
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x000CD148 File Offset: 0x000CB348
	private bool ShowKeywordOnRight()
	{
		if (this.m_card.GetEntity().IsHeroPower())
		{
			return true;
		}
		if (this.m_card.GetEntity().IsWeapon())
		{
			return false;
		}
		if (this.m_card.GetEntity().IsHero() || this.m_card.GetEntity().IsSecretOrQuestOrSideQuestOrSigil())
		{
			return false;
		}
		if (this.m_card.GetEntity().GetCardId() == "TU4c_007")
		{
			return false;
		}
		ZonePlay zonePlay = this.m_card.GetZone() as ZonePlay;
		if (!(zonePlay != null))
		{
			return false;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			return this.m_card.GetActor().GetMeshRenderer(false).bounds.center.x > zonePlay.GetComponent<BoxCollider>().bounds.center.x;
		}
		return this.m_card.GetActor().GetMeshRenderer(false).bounds.center.x < zonePlay.GetComponent<BoxCollider>().bounds.center.x + 0.03f;
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x000CD270 File Offset: 0x000CB470
	private void UpdateEnchantments()
	{
		this.ResetEnchantments();
		GameObject gameObject = this.m_bigCardActor.FindBone("EnchantmentTooltip");
		if (gameObject == null)
		{
			return;
		}
		Entity entity = this.m_card.GetEntity();
		bool flag = GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS);
		List<Entity> displayedEnchantments = entity.GetDisplayedEnchantments(false);
		List<Entity> displayedEnchantments2 = entity.GetDisplayedEnchantments(true);
		List<BigCardEnchantmentPanel> activeList = this.m_enchantmentPool.GetActiveList();
		this.m_uniqueEnchantmentLookup.Clear();
		int num = flag ? displayedEnchantments2.Count : displayedEnchantments.Count;
		int count = activeList.Count;
		int num2 = num - count;
		if (num2 > 0)
		{
			this.m_enchantmentPool.AcquireBatch(num2);
		}
		else if (num2 < 0)
		{
			this.m_enchantmentPool.ReleaseBatch(num, -num2);
		}
		if (num == 0 && !this.m_card.GetEntity().HasTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT) && !this.m_card.GetEntity().IsSideQuest())
		{
			return;
		}
		for (int i = 0; i < activeList.Count; i++)
		{
			BigCardEnchantmentPanel bigCardEnchantmentPanel = activeList[i];
			bigCardEnchantmentPanel.SetEnchantment(flag ? displayedEnchantments2[i] : displayedEnchantments[i]);
			if (flag)
			{
				this.m_uniqueEnchantmentLookup.Add(bigCardEnchantmentPanel.GetEnchantmentId(), bigCardEnchantmentPanel);
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
					this.m_uniqueEnchantmentLookup[displayedEnchantments[j].GetCardId()].IncrementEnchantmentMultiplier((uint)Mathf.Max(displayedEnchantments[j].GetTag(GAME_TAG.SPAWN_TIME_COUNT), 1));
				}
			}
		}
		this.LayoutEnchantments(gameObject);
		SceneUtils.SetLayer(gameObject, GameLayer.Tooltip);
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x000CD454 File Offset: 0x000CB654
	private void ResetEnchantments()
	{
		this.m_EnchantmentBanner.SetActive(false);
		this.m_EnchantmentBannerBottom.SetActive(false);
		this.m_EnchantmentBannerText.gameObject.SetActive(false);
		this.m_EnchantmentBanner.transform.parent = base.transform;
		this.m_EnchantmentBannerBottom.transform.parent = base.transform;
		this.m_EnchantmentBannerText.transform.parent = base.transform;
		foreach (BigCardEnchantmentPanel bigCardEnchantmentPanel in this.m_enchantmentPool.GetActiveList())
		{
			bigCardEnchantmentPanel.transform.parent = base.transform;
			bigCardEnchantmentPanel.ResetScale();
			bigCardEnchantmentPanel.Hide();
		}
	}

	// Token: 0x060028AE RID: 10414 RVA: 0x000CD52C File Offset: 0x000CB72C
	private void LayoutEnchantments(GameObject bone)
	{
		float num = 0.1f;
		List<BigCardEnchantmentPanel> activeList = this.m_enchantmentPool.GetActiveList();
		BigCardEnchantmentPanel bigCardEnchantmentPanel = null;
		for (int i = 0; i < activeList.Count; i++)
		{
			BigCardEnchantmentPanel bigCardEnchantmentPanel2 = activeList[i];
			bigCardEnchantmentPanel2.Show();
			bigCardEnchantmentPanel2.transform.localScale *= this.PLATFORM_SCALING_FACTOR * this.ENCHANTMENT_SCALING_FACTOR;
			if (i == 0)
			{
				TransformUtil.SetPoint(bigCardEnchantmentPanel2.gameObject, new Vector3(0.5f, 0f, 1f), this.m_bigCardActor.GetMeshRenderer(false).gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0.01f, 0.01f, 0f));
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
		if (this.m_card != null && this.m_card.GetEntity() != null && this.m_card.GetEntity().HasTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT))
		{
			string clientString = GameDbf.GetIndex().GetClientString(this.m_card.GetEntity().GetTag(GAME_TAG.ENCHANTMENT_BANNER_TEXT));
			this.UpdateEnchantmentBannerText(bone, bigCardEnchantmentPanel, clientString);
			num += this.m_EnchantmentBannerText.Height;
		}
		else if (this.m_card != null && this.m_card.GetEntity() != null && this.m_card.GetEntity().IsSideQuest())
		{
			string customBannerTextString = GameStrings.Format("GLUE_SIDEQUEST_PROGRESS_BANNER", new object[]
			{
				this.m_card.GetEntity().GetTag(GAME_TAG.QUEST_PROGRESS),
				this.m_card.GetEntity().GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL)
			});
			this.UpdateEnchantmentBannerText(bone, bigCardEnchantmentPanel, customBannerTextString);
			num += this.m_EnchantmentBannerText.Height;
		}
		else
		{
			this.m_EnchantmentBannerText.gameObject.SetActive(false);
		}
		this.m_EnchantmentBanner.SetActive(true);
		this.m_EnchantmentBannerBottom.SetActive(true);
		this.m_EnchantmentBannerBottom.transform.localScale = this.m_initialBannerBottomScale * this.PLATFORM_SCALING_FACTOR * this.ENCHANTMENT_SCALING_FACTOR;
		this.m_EnchantmentBanner.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		this.m_EnchantmentBannerBottom.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		TransformUtil.SetPoint(this.m_EnchantmentBanner, new Vector3(0.5f, 0f, 1f), this.m_bigCardActor.GetMeshRenderer(false).gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, 0.2f));
		this.m_EnchantmentBanner.transform.localScale = this.m_initialBannerScale * this.PLATFORM_SCALING_FACTOR * this.ENCHANTMENT_SCALING_FACTOR;
		TransformUtil.SetLocalScaleZ(this.m_EnchantmentBanner.gameObject, num / this.m_initialBannerHeight / this.m_initialBannerScale.z);
		this.m_EnchantmentBanner.transform.parent = bone.transform;
		TransformUtil.SetPoint(this.m_EnchantmentBannerBottom, Anchor.FRONT, this.m_EnchantmentBanner, Anchor.BACK);
		this.m_EnchantmentBannerBottom.transform.parent = bone.transform;
		this.m_EnchantmentBannerBottom.transform.position += new Vector3(0f, -0.01f, 0.01f);
	}

	// Token: 0x060028AF RID: 10415 RVA: 0x000CD940 File Offset: 0x000CBB40
	private void UpdateEnchantmentBannerText(GameObject bone, BigCardEnchantmentPanel prevPanel, string customBannerTextString)
	{
		this.m_EnchantmentBannerText.transform.localScale = this.m_initialBannerTextScale * this.PLATFORM_SCALING_FACTOR * this.ENCHANTMENT_SCALING_FACTOR;
		this.m_EnchantmentBannerText.transform.parent = bone.transform;
		if (prevPanel == null)
		{
			this.m_EnchantmentBannerText.transform.localPosition = new Vector3(0f, 0f, -0.25f);
		}
		else
		{
			TransformUtil.SetPoint(this.m_EnchantmentBannerText.gameObject, new Vector3(0.5f, 0f, 1f), prevPanel.gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, -0.05f));
		}
		this.m_EnchantmentBannerText.gameObject.SetActive(true);
		this.m_EnchantmentBannerText.Text = customBannerTextString;
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x000CDA37 File Offset: 0x000CBC37
	private void FitInsideScreen()
	{
		this.FitInsideScreenBottom();
		this.FitInsideScreenTop();
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x000CDA48 File Offset: 0x000CBC48
	private bool FitInsideScreenBottom()
	{
		Bounds bounds = this.m_EnchantmentBanner.activeInHierarchy ? this.m_EnchantmentBannerBottom.GetComponent<Renderer>().bounds : this.m_bigCardActor.GetMeshRenderer(false).bounds;
		Vector3 center = bounds.center;
		if (UniversalInputManager.UsePhoneUI)
		{
			center.z -= 0.4f;
		}
		Vector3 vector = new Vector3(center.x, center.y, center.z - bounds.extents.z);
		Ray ray = new Ray(vector, vector - center);
		Plane plane = CameraUtils.CreateBottomPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
		float num = 0f;
		if (plane.Raycast(ray, out num))
		{
			return false;
		}
		if (Mathf.Approximately(num, 0f))
		{
			return false;
		}
		TransformUtil.SetPosZ(this.m_bigCardActor.gameObject, this.m_bigCardActor.transform.position.z - num);
		return true;
	}

	// Token: 0x060028B2 RID: 10418 RVA: 0x000CDB3C File Offset: 0x000CBD3C
	private Bounds CalculateMeshBoundsIncludingGem(Actor actor = null)
	{
		if (actor == null)
		{
			actor = this.m_bigCardActor;
		}
		Bounds bounds = actor.GetMeshRenderer(false).bounds;
		if (actor != null && actor.GetEntity() != null && (actor.GetEntity().IsSideQuest() || actor.GetEntity().IsSigil()))
		{
			foreach (MeshRenderer meshRenderer in actor.GetRootObject().GetComponentsInChildren<MeshRenderer>())
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

	// Token: 0x060028B3 RID: 10419 RVA: 0x000CDBDC File Offset: 0x000CBDDC
	private bool FitInsideScreenTop()
	{
		Bounds bounds = this.CalculateMeshBoundsIncludingGem(null);
		Vector3 center = bounds.center;
		if (UniversalInputManager.UsePhoneUI && !(this.m_card.GetZone() is ZoneHeroPower))
		{
			center.z += 1f;
		}
		Vector3 vector = new Vector3(center.x, center.y, center.z + bounds.extents.z);
		Ray ray = new Ray(vector, vector - center);
		Plane plane = CameraUtils.CreateTopPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
		float num = 0f;
		if (plane.Raycast(ray, out num))
		{
			return false;
		}
		if (Mathf.Approximately(num, 0f))
		{
			return false;
		}
		TransformUtil.SetPosZ(this.m_bigCardActor.gameObject, this.m_bigCardActor.transform.position.z + num);
		return true;
	}

	// Token: 0x060028B4 RID: 10420 RVA: 0x000CDCB8 File Offset: 0x000CBEB8
	private BigCardEnchantmentPanel CreateEnchantmentPanel(int index)
	{
		BigCardEnchantmentPanel bigCardEnchantmentPanel = UnityEngine.Object.Instantiate<BigCardEnchantmentPanel>(this.m_EnchantmentPanelPrefab);
		bigCardEnchantmentPanel.name = string.Format("{0}{1}", typeof(BigCardEnchantmentPanel).ToString(), index);
		SceneUtils.SetRenderQueue(bigCardEnchantmentPanel.gameObject, this.m_RenderQueueEnchantmentPanel, false);
		return bigCardEnchantmentPanel;
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x000CDD07 File Offset: 0x000CBF07
	private void DestroyEnchantmentPanel(BigCardEnchantmentPanel panel)
	{
		UnityEngine.Object.Destroy(panel.gameObject);
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x000CDD14 File Offset: 0x000CBF14
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

	// Token: 0x060028B7 RID: 10423 RVA: 0x000CDD7C File Offset: 0x000CBF7C
	private void LoadAndDisplayTooltipPhoneSigils()
	{
		if (this.m_phoneSigilActors == null)
		{
			this.m_phoneSigilActors = new List<Actor>();
		}
		else
		{
			foreach (Actor actor in this.m_phoneSigilActors)
			{
				actor.Destroy();
			}
			this.m_phoneSigilActors.Clear();
		}
		ZoneSecret zoneSecret = this.m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSigils() called for a card that is not in a Secret Zone.", Array.Empty<object>());
			return;
		}
		List<Card> sigilCards = zoneSecret.GetSigilCards();
		for (int i = 0; i < sigilCards.Count; i++)
		{
			Actor item = this.LoadPhoneSecret(sigilCards[i]);
			this.m_phoneSigilActors.Add(item);
		}
		this.DisplayPhoneSecrets(this.m_card, this.m_phoneSigilActors, false);
	}

	// Token: 0x060028B8 RID: 10424 RVA: 0x000CDE64 File Offset: 0x000CC064
	private void HideTooltipPhoneSigils()
	{
		if (this.m_phoneSigilActors == null)
		{
			return;
		}
		foreach (Actor actor in this.m_phoneSigilActors)
		{
			this.HidePhoneSecret(actor);
		}
		this.m_phoneSigilActors.Clear();
	}

	// Token: 0x060028B9 RID: 10425 RVA: 0x000CDECC File Offset: 0x000CC0CC
	private void LoadAndDisplayTooltipPhoneSecrets()
	{
		if (this.m_phoneSecretActors == null)
		{
			this.m_phoneSecretActors = new List<Actor>();
		}
		else
		{
			foreach (Actor actor in this.m_phoneSecretActors)
			{
				actor.Destroy();
			}
			this.m_phoneSecretActors.Clear();
		}
		ZoneSecret zoneSecret = this.m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSecrets() called for a card that is not in a Secret Zone.", Array.Empty<object>());
			return;
		}
		List<Card> secretCards = zoneSecret.GetSecretCards();
		for (int i = 0; i < secretCards.Count; i++)
		{
			Actor item = this.LoadPhoneSecret(secretCards[i]);
			this.m_phoneSecretActors.Add(item);
		}
		this.DisplayPhoneSecrets(this.m_card, this.m_phoneSecretActors, false);
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x000CDFB4 File Offset: 0x000CC1B4
	private void HideTooltipPhoneSecrets()
	{
		if (this.m_phoneSecretActors == null)
		{
			return;
		}
		foreach (Actor actor in this.m_phoneSecretActors)
		{
			this.HidePhoneSecret(actor);
		}
		this.m_phoneSecretActors.Clear();
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x000CE01C File Offset: 0x000CC21C
	private void LoadAndDisplayTooltipPhoneSideQuests()
	{
		if (this.m_phoneSideQuestActors == null)
		{
			this.m_phoneSideQuestActors = new List<Actor>();
		}
		else
		{
			foreach (Actor actor in this.m_phoneSideQuestActors)
			{
				actor.Destroy();
			}
			this.m_phoneSideQuestActors.Clear();
		}
		ZoneSecret zoneSecret = this.m_card.GetZone() as ZoneSecret;
		if (zoneSecret == null)
		{
			Log.Gameplay.PrintError("BigCard.LoadAndDisplayTooltipPhoneSideQuests() called for a card that is not in a Secret Zone.", Array.Empty<object>());
			return;
		}
		List<Card> sideQuestCards = zoneSecret.GetSideQuestCards();
		for (int i = 0; i < sideQuestCards.Count; i++)
		{
			Actor item = this.LoadPhoneSecret(sideQuestCards[i]);
			this.m_phoneSideQuestActors.Add(item);
		}
		this.DisplayPhoneSecrets(this.m_card, this.m_phoneSideQuestActors, false);
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x000CE104 File Offset: 0x000CC304
	private void HideTooltipPhoneSideQuests()
	{
		if (this.m_phoneSideQuestActors == null)
		{
			return;
		}
		foreach (Actor actor in this.m_phoneSideQuestActors)
		{
			this.HidePhoneSecret(actor);
		}
		this.m_phoneSideQuestActors.Clear();
	}

	// Token: 0x060028BD RID: 10429 RVA: 0x000CE16C File Offset: 0x000CC36C
	private Actor LoadPhoneSecret(Card card)
	{
		string bigCardActor = ActorNames.GetBigCardActor(card.GetEntity());
		Actor component = AssetLoader.Get().InstantiatePrefab(bigCardActor, AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
		this.SetupActor(card, component);
		return component;
	}

	// Token: 0x060028BE RID: 10430 RVA: 0x000CE1A8 File Offset: 0x000CC3A8
	private Vector3 PhoneMoveSideQuestBigCardToTopOfScreen(Actor actor, Vector3 initialPosition)
	{
		if (actor == null || !UniversalInputManager.UsePhoneUI)
		{
			return initialPosition;
		}
		Vector3 position = actor.transform.position;
		Vector3 result;
		try
		{
			actor.transform.position = initialPosition;
			Bounds bounds = this.CalculateMeshBoundsIncludingGem(actor);
			Vector3 center = bounds.center;
			Vector3 vector = new Vector3(center.x, center.y, center.z + bounds.extents.z);
			Ray ray = new Ray(vector, vector - center);
			Plane plane = CameraUtils.CreateTopPlane(CameraUtils.FindFirstByLayer(GameLayer.Tooltip));
			float z = 0f;
			plane.Raycast(ray, out z);
			result = initialPosition + new Vector3(0f, 0f, z);
		}
		finally
		{
			actor.transform.position = position;
		}
		return result;
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x000CE284 File Offset: 0x000CC484
	private void DisplayPhoneSecrets(Card mainCard, List<Actor> actors, bool showDeath)
	{
		Vector3 b;
		Vector3 vector;
		Vector3 b2;
		this.DetermineSecretLayoutOffsets(mainCard, actors, out b, out vector, out b2);
		bool flag = GeneralUtils.IsOdd(actors.Count);
		Player controller = mainCard.GetController();
		Component secretZone = controller.GetSecretZone();
		Actor actor = mainCard.GetActor();
		Vector3 vector2 = secretZone.transform.position + b;
		for (int i = 0; i < actors.Count; i++)
		{
			Actor actor2 = actors[i];
			Vector3 vector3;
			if (i == 0 && flag)
			{
				if (actors.Count == 1 && actor2.GetCard().GetEntity().IsSideQuest() && controller.IsFriendlySide())
				{
					vector3 = this.PhoneMoveSideQuestBigCardToTopOfScreen(actor2, vector2);
				}
				else
				{
					vector3 = vector2;
				}
			}
			else
			{
				bool flag2 = GeneralUtils.IsOdd(i);
				bool flag3 = flag == flag2;
				float num = flag ? Mathf.Ceil(0.5f * (float)i) : Mathf.Floor(0.5f * (float)i);
				float num2 = num * vector.x;
				if (!flag)
				{
					num2 += 0.5f * vector.x;
				}
				if (flag3)
				{
					num2 = -num2;
				}
				float num3 = num * vector.z;
				vector3 = new Vector3(vector2.x + num2, vector2.y, vector2.z + num3);
			}
			actor2.transform.position = actor.transform.position;
			actor2.transform.rotation = actor.transform.rotation;
			actor2.transform.localScale = BigCard.INVISIBLE_SCALE;
			float num4 = showDeath ? this.m_SecretLayoutData.m_DeathShowAnimTime : this.m_SecretLayoutData.m_ShowAnimTime;
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				vector3 - b2,
				"time",
				num4,
				"easeType",
				iTween.EaseType.easeOutExpo
			});
			iTween.MoveTo(actor2.gameObject, args);
			Hashtable args2 = iTween.Hash(new object[]
			{
				"position",
				vector3,
				"delay",
				num4,
				"time",
				this.m_SecretLayoutData.m_DriftSec,
				"easeType",
				iTween.EaseType.easeOutExpo
			});
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
				this.ShowPhoneSecretDeath(actor2);
			}
		}
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x000CE528 File Offset: 0x000CC728
	private void DetermineSecretLayoutOffsets(Card mainCard, List<Actor> actors, out Vector3 initialOffset, out Vector3 spacing, out Vector3 drift)
	{
		Player controller = mainCard.GetController();
		bool flag = controller.IsFriendlySide();
		bool flag2 = controller.IsRevealed();
		float minCardThreshold = (float)this.m_SecretLayoutData.m_MinCardThreshold;
		int maxCardThreshold = this.m_SecretLayoutData.m_MaxCardThreshold;
		BigCard.SecretLayoutOffsets minCardOffsets = this.m_SecretLayoutData.m_MinCardOffsets;
		BigCard.SecretLayoutOffsets maxCardOffsets = this.m_SecretLayoutData.m_MaxCardOffsets;
		float t = Mathf.InverseLerp(minCardThreshold, (float)maxCardThreshold, (float)actors.Count);
		if (flag2)
		{
			if (flag)
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_InitialOffset, maxCardOffsets.m_InitialOffset, t);
			}
			else
			{
				initialOffset = Vector3.Lerp(minCardOffsets.m_OpponentInitialOffset, maxCardOffsets.m_OpponentInitialOffset, t);
			}
			spacing = this.m_SecretLayoutData.m_Spacing;
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
			spacing = this.m_SecretLayoutData.m_HiddenSpacing;
		}
		if (flag)
		{
			spacing.z = -spacing.z;
			drift = this.m_SecretLayoutData.m_DriftOffset;
			return;
		}
		drift = -this.m_SecretLayoutData.m_DriftOffset;
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x000CE65C File Offset: 0x000CC85C
	private void ShowPhoneSecretDeath(Actor actor)
	{
		Spell.StateFinishedCallback deathSpellStateFinished = delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				return;
			}
			actor.Destroy();
		};
		Action<object> action = delegate(object obj)
		{
			Spell spell = actor.GetSpell(SpellType.DEATH);
			spell.AddStateFinishedCallback(deathSpellStateFinished);
			spell.Activate();
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_SecretLayoutData.m_TimeUntilDeathSpell,
			"oncomplete",
			action
		});
		iTween.Timer(actor.gameObject, args);
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x000CE6DC File Offset: 0x000CC8DC
	private void HidePhoneSecret(Actor actor)
	{
		Actor actor2 = this.m_card.GetActor();
		iTween.MoveTo(actor.gameObject, actor2.transform.position, this.m_SecretLayoutData.m_HideAnimTime);
		iTween.ScaleTo(actor.gameObject, BigCard.INVISIBLE_SCALE, this.m_SecretLayoutData.m_HideAnimTime);
		Action<object> action = delegate(object obj)
		{
			actor.Destroy();
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_SecretLayoutData.m_HideAnimTime,
			"oncomplete",
			action
		});
		iTween.Timer(actor.gameObject, args);
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x000CE79C File Offset: 0x000CC99C
	private void SetupActor(Card card, Actor actor)
	{
		bool ignoreHideStats = false;
		Entity entity = card.GetEntity();
		if (this.ShouldActorUseEntity(entity))
		{
			actor.SetEntity(entity);
			ignoreHideStats = (entity.HasTag(GAME_TAG.IGNORE_HIDE_STATS_FOR_BIG_CARD) || (entity.IsDormant() && !entity.HasTag(GAME_TAG.HIDE_STATS)));
		}
		GhostCard.Type ghostType = (GhostCard.Type)entity.GetTag(GAME_TAG.MOUSE_OVER_CARD_APPEARANCE);
		if (card.GetEntity().IsDormant())
		{
			ghostType = GhostCard.Type.DORMANT;
		}
		actor.GhostCardEffect(ghostType, entity.GetPremiumType(), false);
		EntityDef entityDef = entity.GetEntityDef();
		DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef();
		int tag = entity.GetTag(GAME_TAG.ALTERNATE_MOUSE_OVER_CARD);
		if (tag != 0)
		{
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef2 == null)
			{
				Log.Gameplay.PrintError("BigCard.SetupActor(): Unable to load EntityDef for card ID {0}.", new object[]
				{
					tag
				});
			}
			else
			{
				entityDef = entityDef2;
			}
			DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(tag);
			if (cardDef == null)
			{
				Log.Spells.PrintError("BigCard.SetupActor(): Unable to load CardDef for card ID {0}.", new object[]
				{
					tag
				});
			}
			else
			{
				if (disposableCardDef != null)
				{
					disposableCardDef.Dispose();
				}
				disposableCardDef = cardDef;
			}
		}
		using (disposableCardDef)
		{
			if (this.ShouldActorUseEntityDef(entity))
			{
				actor.SetEntityDef(entityDef);
				ignoreHideStats = (entityDef.HasTag(GAME_TAG.IGNORE_HIDE_STATS_FOR_BIG_CARD) || entityDef.IsDormant());
			}
			actor.SetPremium(entity.GetPremiumType());
			actor.SetCard(card);
			actor.SetCardDef(disposableCardDef);
			actor.SetIgnoreHideStats(ignoreHideStats);
			actor.SetWatermarkCardSetOverride(entity.GetWatermarkCardSetOverride());
			actor.UpdateAllComponents();
			this.ActivateBigCardStateSpells(entity, card.GetActor(), actor);
			BoxCollider componentInChildren = actor.GetComponentInChildren<BoxCollider>();
			if (componentInChildren != null)
			{
				componentInChildren.enabled = false;
			}
			actor.name = "BigCard_" + actor.name;
			SceneUtils.SetLayer(actor, GameLayer.Tooltip);
		}
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x000CE970 File Offset: 0x000CCB70
	private bool ShouldActorUseEntity(Entity entity)
	{
		return entity.IsHidden() || ((entity.GetZone() == TAG_ZONE.PLAY || entity.GetZone() == TAG_ZONE.SECRET) && entity.GetCardTextBuilder().ShouldUseEntityForTextInPlay()) || entity.IsDormant() || (entity.IsSideQuest() || entity.IsSigil() || entity.IsSecret()) || entity.IsHeroPowerOrGameModeButton() || (GameMgr.Get().IsSpectator() && entity.GetZone() == TAG_ZONE.HAND && entity.GetController().IsOpposingSide());
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x000CE9FC File Offset: 0x000CCBFC
	private bool ShouldActorUseEntityDef(Entity entity)
	{
		return !entity.IsHidden() && !entity.IsHeroPowerOrGameModeButton() && !entity.IsDormant() && entity.GetZone() != TAG_ZONE.SECRET && (!GameMgr.Get().IsSpectator() || entity.GetZone() != TAG_ZONE.HAND || !entity.GetController().IsOpposingSide());
	}

	// Token: 0x04001726 RID: 5926
	public BigCardEnchantmentPanel m_EnchantmentPanelPrefab;

	// Token: 0x04001727 RID: 5927
	public GameObject m_EnchantmentBanner;

	// Token: 0x04001728 RID: 5928
	public GameObject m_EnchantmentBannerBottom;

	// Token: 0x04001729 RID: 5929
	public UberText m_EnchantmentBannerText;

	// Token: 0x0400172A RID: 5930
	public int m_RenderQueueEnchantmentBanner = 1;

	// Token: 0x0400172B RID: 5931
	public int m_RenderQueueEnchantmentPanel = 2;

	// Token: 0x0400172C RID: 5932
	public BigCard.LayoutData m_LayoutData;

	// Token: 0x0400172D RID: 5933
	public BigCard.SecretLayoutData m_SecretLayoutData;

	// Token: 0x0400172E RID: 5934
	private static readonly Vector3 INVISIBLE_SCALE = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x0400172F RID: 5935
	private static BigCard s_instance;

	// Token: 0x04001730 RID: 5936
	private Card m_card;

	// Token: 0x04001731 RID: 5937
	private Actor m_bigCardActor;

	// Token: 0x04001732 RID: 5938
	private TooltipPanel m_bigCardAsTooltip;

	// Token: 0x04001733 RID: 5939
	private Actor m_twinCardActor;

	// Token: 0x04001734 RID: 5940
	private List<Actor> m_phoneSecretActors;

	// Token: 0x04001735 RID: 5941
	private List<Actor> m_phoneSideQuestActors;

	// Token: 0x04001736 RID: 5942
	private List<Actor> m_phoneSigilActors;

	// Token: 0x04001737 RID: 5943
	private float m_initialBannerHeight;

	// Token: 0x04001738 RID: 5944
	private Vector3 m_initialBannerScale;

	// Token: 0x04001739 RID: 5945
	private Vector3 m_initialBannerBottomScale;

	// Token: 0x0400173A RID: 5946
	private Vector3 m_initialBannerTextScale;

	// Token: 0x0400173B RID: 5947
	private Pool<BigCardEnchantmentPanel> m_enchantmentPool = new Pool<BigCardEnchantmentPanel>();

	// Token: 0x0400173C RID: 5948
	private Map<string, BigCardEnchantmentPanel> m_uniqueEnchantmentLookup = new Map<string, BigCardEnchantmentPanel>();

	// Token: 0x0400173D RID: 5949
	private readonly PlatformDependentValue<float> PLATFORM_SCALING_FACTOR;

	// Token: 0x0400173E RID: 5950
	private readonly PlatformDependentValue<float> ENCHANTMENT_SCALING_FACTOR;

	// Token: 0x02001629 RID: 5673
	[Serializable]
	public class LayoutData
	{
		// Token: 0x0400AFFA RID: 45050
		public float m_ScaleSec = 0.15f;

		// Token: 0x0400AFFB RID: 45051
		public float m_DriftSec = 10f;
	}

	// Token: 0x0200162A RID: 5674
	[Serializable]
	public class SecretLayoutOffsets
	{
		// Token: 0x0400AFFC RID: 45052
		public Vector3 m_InitialOffset = new Vector3(0.1f, 5f, 3.3f);

		// Token: 0x0400AFFD RID: 45053
		public Vector3 m_OpponentInitialOffset = new Vector3(0.1f, 5f, -3.3f);

		// Token: 0x0400AFFE RID: 45054
		public Vector3 m_HiddenInitialOffset = new Vector3(0f, 4f, 4f);

		// Token: 0x0400AFFF RID: 45055
		public Vector3 m_HiddenOpponentInitialOffset = new Vector3(0f, 4f, -4f);
	}

	// Token: 0x0200162B RID: 5675
	[Serializable]
	public class SecretLayoutData
	{
		// Token: 0x0400B000 RID: 45056
		public float m_ShowAnimTime = 0.15f;

		// Token: 0x0400B001 RID: 45057
		public float m_HideAnimTime = 0.15f;

		// Token: 0x0400B002 RID: 45058
		public float m_DeathShowAnimTime = 1f;

		// Token: 0x0400B003 RID: 45059
		public float m_TimeUntilDeathSpell = 1.5f;

		// Token: 0x0400B004 RID: 45060
		public float m_DriftSec = 5f;

		// Token: 0x0400B005 RID: 45061
		public Vector3 m_DriftOffset = new Vector3(0f, 0f, 0.05f);

		// Token: 0x0400B006 RID: 45062
		public Vector3 m_Spacing = new Vector3(2.1f, 0f, 0.7f);

		// Token: 0x0400B007 RID: 45063
		public Vector3 m_HiddenSpacing = new Vector3(2.4f, 0f, 0.7f);

		// Token: 0x0400B008 RID: 45064
		public int m_MinCardThreshold = 1;

		// Token: 0x0400B009 RID: 45065
		public int m_MaxCardThreshold = 5;

		// Token: 0x0400B00A RID: 45066
		public BigCard.SecretLayoutOffsets m_MinCardOffsets = new BigCard.SecretLayoutOffsets();

		// Token: 0x0400B00B RID: 45067
		public BigCard.SecretLayoutOffsets m_MaxCardOffsets = new BigCard.SecretLayoutOffsets();
	}

	// Token: 0x0200162C RID: 5676
	private struct KeywordArgs
	{
		// Token: 0x0400B00C RID: 45068
		public Card card;

		// Token: 0x0400B00D RID: 45069
		public Actor actor;

		// Token: 0x0400B00E RID: 45070
		public bool showOnRight;
	}
}
