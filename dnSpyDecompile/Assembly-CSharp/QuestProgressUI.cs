using System;
using UnityEngine;

// Token: 0x02000904 RID: 2308
public class QuestProgressUI : MonoBehaviour
{
	// Token: 0x0600806F RID: 32879 RVA: 0x0029BE26 File Offset: 0x0029A026
	private void OnDestroy()
	{
		if (this.m_isResaturating && FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().ClearDesaturateListener();
		}
	}

	// Token: 0x06008070 RID: 32880 RVA: 0x0029BE41 File Offset: 0x0029A041
	public void SetOriginalQuestActor(Actor actor)
	{
		this.m_originalQuestActor = actor;
	}

	// Token: 0x06008071 RID: 32881 RVA: 0x0029BE4A File Offset: 0x0029A04A
	public void Show()
	{
		this.m_isShown = true;
		base.gameObject.SetActive(true);
		this.UpdateActors();
		this.DesaturateBoard();
	}

	// Token: 0x06008072 RID: 32882 RVA: 0x0029BE6B File Offset: 0x0029A06B
	public void Hide()
	{
		this.m_isShown = false;
		base.gameObject.SetActive(false);
		this.StopDesaturate();
	}

	// Token: 0x06008073 RID: 32883 RVA: 0x0029BE86 File Offset: 0x0029A086
	public void UpdateText(int currentQuestProgress, int questProgressTotal)
	{
		this.UpdateProgressText(currentQuestProgress, questProgressTotal);
		this.UpdateQuestDetailText();
	}

	// Token: 0x06008074 RID: 32884 RVA: 0x0029BE96 File Offset: 0x0029A096
	private void UpdateProgressText(int currentQuestProgress, int questProgressTotal)
	{
		this.m_ProgressText.Text = string.Format("{0}/{1}", currentQuestProgress, questProgressTotal);
	}

	// Token: 0x06008075 RID: 32885 RVA: 0x0029BEBC File Offset: 0x0029A0BC
	private void UpdateQuestDetailText()
	{
		Entity entity = this.m_originalQuestActor.GetEntity();
		if (entity.HasTag(GAME_TAG.QUEST_CONTRIBUTOR))
		{
			int tag = entity.GetTag(GAME_TAG.QUEST_CONTRIBUTOR);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				this.m_QuestDetailText.Text = entityDef.GetName();
				this.m_QuestDetailText.gameObject.SetActive(true);
				return;
			}
		}
		this.m_QuestDetailText.gameObject.SetActive(false);
	}

	// Token: 0x06008076 RID: 32886 RVA: 0x0029BF34 File Offset: 0x0029A134
	private void Update()
	{
		if (!this.m_isShown)
		{
			return;
		}
		if (this.m_originalQuestActor.GetEntity().GetControllerSide() != Player.Side.FRIENDLY)
		{
			return;
		}
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards())
		{
			if (card.GetEntity().HasTag(GAME_TAG.QUEST_CONTRIBUTOR))
			{
				SceneUtils.SetLayer(card.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
	}

	// Token: 0x06008077 RID: 32887 RVA: 0x0029BFCC File Offset: 0x0029A1CC
	private void DesaturateBoard()
	{
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad, null, null);
	}

	// Token: 0x06008078 RID: 32888 RVA: 0x0029BFE5 File Offset: 0x0029A1E5
	private void StopDesaturate()
	{
		this.m_isResaturating = true;
		FullScreenFXMgr.Get().StopDesaturate(0.4f, iTween.EaseType.easeInOutQuad, new Action(this.OnStopDesaturateFinished), null);
	}

	// Token: 0x06008079 RID: 32889 RVA: 0x0029C00C File Offset: 0x0029A20C
	private void OnStopDesaturateFinished()
	{
		if (this.m_originalQuestActor.GetEntity().GetControllerSide() == Player.Side.FRIENDLY)
		{
			foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards())
			{
				if (!card.IsMousedOver())
				{
					SceneUtils.SetLayer(card.gameObject, GameLayer.Default);
				}
			}
		}
		this.m_isResaturating = false;
	}

	// Token: 0x0600807A RID: 32890 RVA: 0x0029C094 File Offset: 0x0029A294
	private void UpdateActors()
	{
		this.UpdateQuestActor();
		this.UpdateRewardActor();
	}

	// Token: 0x0600807B RID: 32891 RVA: 0x0029C0A4 File Offset: 0x0029A2A4
	private void UpdateQuestActor()
	{
		if (this.m_questCardActor == null || this.m_questCardActor.GetEntityDef() != this.m_originalQuestActor.GetEntityDef())
		{
			if (this.m_questCardActor != null)
			{
				this.m_questCardActor.Destroy();
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(this.m_originalQuestActor.GetEntity()), AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Log.Gameplay.PrintError("QuestProgressUI.UpdateQuestCard(): Unable to load hand actor for entity {0}.", new object[]
				{
					this.m_originalQuestActor
				});
				return;
			}
			SceneUtils.SetLayer(gameObject, this.m_QuestCardBone.gameObject.layer, null);
			gameObject.transform.parent = this.m_QuestCardBone;
			TransformUtil.Identity(gameObject);
			this.m_questCardActor = gameObject.GetComponentInChildren<Actor>();
			this.m_questCardActor.SetEntityDef(this.m_originalQuestActor.GetEntity().GetEntityDef());
			this.m_questCardActor.SetCardDefFromActor(this.m_originalQuestActor);
			this.m_questCardActor.SetPremium(this.m_originalQuestActor.GetEntity().GetPremiumType());
			this.m_questCardActor.SetWatermarkCardSetOverride(this.m_originalQuestActor.GetEntity().GetWatermarkCardSetOverride());
			this.m_questCardActor.UpdateAllComponents();
		}
	}

	// Token: 0x0600807C RID: 32892 RVA: 0x0029C1EC File Offset: 0x0029A3EC
	private void UpdateRewardActor()
	{
		Entity entity = this.m_originalQuestActor.GetEntity();
		string rewardCardIDFromQuestCardID = QuestController.GetRewardCardIDFromQuestCardID(entity);
		if (string.IsNullOrEmpty(rewardCardIDFromQuestCardID))
		{
			Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): No reward card ID found for quest card ID {0}.", new object[]
			{
				entity.GetCardId()
			});
			return;
		}
		if (this.m_questRewardActor == null || this.m_questRewardActor.GetEntityDef().GetCardId() != rewardCardIDFromQuestCardID)
		{
			if (this.m_questRewardActor != null)
			{
				this.m_questRewardActor.Destroy();
			}
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(rewardCardIDFromQuestCardID, null))
			{
				if (cardDef == null)
				{
					Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load CardDef for card ID {0}.", new object[]
					{
						rewardCardIDFromQuestCardID
					});
				}
				else
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef(rewardCardIDFromQuestCardID);
					if (entityDef == null)
					{
						Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load EntityDef for card ID {0}.", new object[]
						{
							rewardCardIDFromQuestCardID
						});
					}
					else
					{
						GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, entity.GetPremiumType()), AssetLoadingOptions.IgnorePrefabPosition);
						if (gameObject == null)
						{
							Log.Gameplay.PrintError("QuestProgressUI.UpdateRewardCard(): Unable to load Hand Actor for entity def {0}.", new object[]
							{
								entityDef
							});
						}
						else
						{
							SceneUtils.SetLayer(gameObject, this.m_QuestRewardBone.gameObject.layer, null);
							gameObject.transform.parent = this.m_QuestRewardBone;
							TransformUtil.Identity(gameObject);
							this.m_questRewardActor = gameObject.GetComponentInChildren<Actor>();
							this.m_questRewardActor.SetEntityDef(entityDef);
							this.m_questRewardActor.SetCardDef(cardDef);
							this.m_questRewardActor.SetPremium(this.m_originalQuestActor.GetEntity().GetPremiumType());
							this.m_questRewardActor.SetWatermarkCardSetOverride(this.m_originalQuestActor.GetEntity().GetWatermarkCardSetOverride());
							this.m_questRewardActor.UpdateAllComponents();
							this.UpdateRewardOverlayTexture(entityDef);
							this.UpdateRewardBackgroundGlowTexture(entityDef);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600807D RID: 32893 RVA: 0x0029C3EC File Offset: 0x0029A5EC
	private void UpdateRewardOverlayTexture(EntityDef questRewardEntityDef)
	{
		if (this.m_RewardOverlayRenderer == null)
		{
			return;
		}
		Texture texture = null;
		if (questRewardEntityDef.IsMinion())
		{
			texture = (questRewardEntityDef.IsElite() ? this.m_LegendaryMinionRewardOverlayTexture : this.m_MinionRewardOverlayTexture);
		}
		else if (questRewardEntityDef.IsSpell())
		{
			texture = ((this.m_questRewardActor.GetPremium() == TAG_PREMIUM.NORMAL) ? this.m_SpellRewardOverlayTexture : this.m_GoldenSpellRewardOverlayTexture);
		}
		else if (questRewardEntityDef.IsWeapon())
		{
			texture = (questRewardEntityDef.IsElite() ? this.m_LegendaryWeaponRewardOverlayTexture : this.m_WeaponRewardOverlayTexture);
		}
		else if (questRewardEntityDef.IsHeroPower())
		{
			texture = this.m_HeroPowerRewardOverlayTexture;
		}
		if (texture == null)
		{
			return;
		}
		Material material = this.m_RewardOverlayRenderer.GetMaterial();
		material.SetTexture("_MainTex", texture);
		material.SetTexture("_AddTex", texture);
	}

	// Token: 0x0600807E RID: 32894 RVA: 0x0029C4B0 File Offset: 0x0029A6B0
	private void UpdateRewardBackgroundGlowTexture(EntityDef questRewardEntityDef)
	{
		if (this.m_RewardBackGlowRenderer == null)
		{
			return;
		}
		Material material = this.m_DefaultRewardBackGlowMaterial;
		if (questRewardEntityDef.IsHeroPower())
		{
			material = this.m_HeroPowerRewardBackGlowMaterial;
		}
		this.m_RewardBackGlowRenderer.SetMaterial(material);
	}

	// Token: 0x04006960 RID: 26976
	public Transform m_QuestCardBone;

	// Token: 0x04006961 RID: 26977
	public Transform m_QuestRewardBone;

	// Token: 0x04006962 RID: 26978
	public UberText m_ProgressText;

	// Token: 0x04006963 RID: 26979
	public UberText m_QuestDetailText;

	// Token: 0x04006964 RID: 26980
	[Header("Reward Overlay Reference Settings")]
	public MeshRenderer m_RewardOverlayRenderer;

	// Token: 0x04006965 RID: 26981
	public Texture m_MinionRewardOverlayTexture;

	// Token: 0x04006966 RID: 26982
	public Texture m_LegendaryMinionRewardOverlayTexture;

	// Token: 0x04006967 RID: 26983
	public Texture m_SpellRewardOverlayTexture;

	// Token: 0x04006968 RID: 26984
	public Texture m_GoldenSpellRewardOverlayTexture;

	// Token: 0x04006969 RID: 26985
	public Texture m_WeaponRewardOverlayTexture;

	// Token: 0x0400696A RID: 26986
	public Texture m_LegendaryWeaponRewardOverlayTexture;

	// Token: 0x0400696B RID: 26987
	public Texture m_HeroPowerRewardOverlayTexture;

	// Token: 0x0400696C RID: 26988
	[Header("Reward Background Glow Reference Settings")]
	public MeshRenderer m_RewardBackGlowRenderer;

	// Token: 0x0400696D RID: 26989
	public Material m_DefaultRewardBackGlowMaterial;

	// Token: 0x0400696E RID: 26990
	public Material m_HeroPowerRewardBackGlowMaterial;

	// Token: 0x0400696F RID: 26991
	private Actor m_originalQuestActor;

	// Token: 0x04006970 RID: 26992
	private Actor m_questCardActor;

	// Token: 0x04006971 RID: 26993
	private Actor m_questRewardActor;

	// Token: 0x04006972 RID: 26994
	private bool m_isShown;

	// Token: 0x04006973 RID: 26995
	private bool m_isResaturating;
}
