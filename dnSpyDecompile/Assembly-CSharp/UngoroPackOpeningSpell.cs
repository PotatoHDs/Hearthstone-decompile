using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000837 RID: 2103
public class UngoroPackOpeningSpell : SuperSpell
{
	// Token: 0x06007070 RID: 28784 RVA: 0x00244650 File Offset: 0x00242850
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		this.m_newCards = new List<Entity>();
		this.m_fullEntityTaskIndices = new List<int>();
		this.FindNewCardsFullEntityTask();
		return true;
	}

	// Token: 0x06007071 RID: 28785 RVA: 0x0024467C File Offset: 0x0024287C
	private void FindNewCardsFullEntityTask()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity != null)
			{
				foreach (Network.Entity.Tag tag in histFullEntity.Entity.Tags)
				{
					if (tag.Name == 49 && tag.Value == 3)
					{
						this.m_fullEntityTaskIndices.Add(i);
						this.m_newCards.Add(GameState.Get().GetEntity(histFullEntity.Entity.ID));
						break;
					}
				}
			}
		}
	}

	// Token: 0x06007072 RID: 28786 RVA: 0x0024474C File Offset: 0x0024294C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		UngoroPackOpeningPositioner component = this.m_activeAreaEffectSpell.GetComponent<UngoroPackOpeningPositioner>();
		if (component == null)
		{
			Log.Spells.PrintError("UngoroPackOpeningSpell.OnAction(): No UngoroPackOpeningPositioner found on spell {0}.", new object[]
			{
				this.m_activeAreaEffectSpell.gameObject.name
			});
			this.OnSpellFinished();
			this.OnStateFinished();
			return;
		}
		if (this.m_newCards.Count <= 0)
		{
			this.OnSpellFinished();
			this.OnStateFinished();
			return;
		}
		this.m_effectsPendingFinish++;
		this.m_activeAreaEffectSpell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		this.cardDestinations = component.GetPositioningBonesForCardCount(this.m_newCards.Count);
		this.cardSpawningPosition = component.m_PackSpawningBone;
		base.StartCoroutine(this.SpawnAndHideReceivedCards());
	}

	// Token: 0x06007073 RID: 28787 RVA: 0x0024481A File Offset: 0x00242A1A
	private IEnumerator SpawnAndHideReceivedCards()
	{
		int num = 0;
		Player.Side controllerSide = this.m_newCards[0].GetControllerSide();
		ZoneMgr.Get().FindZoneOfType<ZoneHand>(controllerSide).AddLayoutBlocker();
		int num2;
		for (int i = 0; i < this.m_newCards.Count; i = num2 + 1)
		{
			UngoroPackOpeningSpell.<>c__DisplayClass10_0 CS$<>8__locals1 = new UngoroPackOpeningSpell.<>c__DisplayClass10_0();
			CS$<>8__locals1.complete = false;
			PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
			{
				CS$<>8__locals1.complete = true;
			};
			int count2 = 1 + (this.m_fullEntityTaskIndices[i] - num);
			Card newCard = this.m_newCards[i].GetCard();
			newCard.SetDoNotSort(true);
			newCard.SetDoNotWarpToNewZone(true);
			newCard.SetInputEnabled(false);
			this.m_taskList.DoTasks(num, count2, callback);
			while (newCard.GetActor() == null || newCard.IsActorLoading())
			{
				yield return null;
			}
			newCard.HideCard();
			while (!CS$<>8__locals1.complete)
			{
				yield return null;
			}
			num = this.m_fullEntityTaskIndices[i] + 1;
			CS$<>8__locals1 = null;
			newCard = null;
			num2 = i;
		}
		if (this.m_newCards.Count > 0)
		{
			this.previousLayer = this.m_newCards[0].GetCard().gameObject.layer;
		}
		yield break;
	}

	// Token: 0x06007074 RID: 28788 RVA: 0x00244829 File Offset: 0x00242A29
	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		this.PlayInnkeeperVO();
		base.StartCoroutine(this.SplayOutReceivedCards());
	}

	// Token: 0x06007075 RID: 28789 RVA: 0x00244840 File Offset: 0x00242A40
	private void PlayInnkeeperVO()
	{
		TAG_RARITY tag_RARITY = TAG_RARITY.INVALID;
		TAG_PREMIUM tag_PREMIUM = TAG_PREMIUM.NORMAL;
		foreach (Entity entity in this.m_newCards)
		{
			if (!entity.IsHidden())
			{
				TAG_RARITY rarity = entity.GetRarity();
				TAG_PREMIUM premiumType = entity.GetPremiumType();
				if (rarity > tag_RARITY)
				{
					tag_RARITY = rarity;
					tag_PREMIUM = premiumType;
				}
				else if (rarity == tag_RARITY && premiumType == TAG_PREMIUM.GOLDEN)
				{
					tag_PREMIUM = premiumType;
				}
			}
		}
		switch (tag_RARITY)
		{
		case TAG_RARITY.COMMON:
			if (tag_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_C_29.prefab:69820e4999e4afa439761151e057a526");
				return;
			}
			break;
		case TAG_RARITY.FREE:
			break;
		case TAG_RARITY.RARE:
			if (tag_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_R_30.prefab:f5bf5bfd8e5f4d247aa8a6da966969cf");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_RARE_27.prefab:8ff0de7a4fd144b4b983caea4c54da4d");
			return;
		case TAG_RARITY.EPIC:
			if (tag_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_E_31.prefab:d419d6eca0e2a72469544bae5f11542f");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_EPIC_26.prefab:e76d67f55b976104794c3cf73382e82a");
			return;
		case TAG_RARITY.LEGENDARY:
			if (tag_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_L_32.prefab:caefd66acfc4e2b4f858035c274b257e");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_LEGENDARY_25.prefab:e015c982aec12bc4893f36396d426750");
			break;
		default:
			return;
		}
	}

	// Token: 0x06007076 RID: 28790 RVA: 0x00244984 File Offset: 0x00242B84
	private IEnumerator SplayOutReceivedCards()
	{
		int num;
		for (int i = 0; i < this.m_newCards.Count; i = num + 1)
		{
			Card newCard = this.m_newCards[i].GetCard();
			while (newCard.GetActor() == null || newCard.IsActorLoading())
			{
				yield return null;
			}
			newCard = null;
			num = i;
		}
		for (int j = 0; j < this.m_newCards.Count; j++)
		{
			Card card = this.m_newCards[j].GetCard();
			TransformUtil.CopyWorld(card, this.cardSpawningPosition);
			card.ShowCard();
			SceneUtils.SetLayer(card.gameObject, GameLayer.Tooltip);
			Transform transform = this.cardDestinations[j];
			card.transform.localScale = new Vector3(card.transform.localScale.x * transform.localScale.x, card.transform.localScale.y * transform.localScale.y, card.transform.localScale.z * transform.localScale.z);
			Vector3 position = this.cardDestinations[j].position;
			iTween.MoveTo(card.gameObject, position, this.m_CardFlyOutTime);
		}
		yield return new WaitForSeconds(this.m_CardHangTime);
		for (int k = 0; k < this.m_newCards.Count; k++)
		{
			Card card2 = this.m_newCards[k].GetCard();
			ZoneTransitionStyle transitionStyle = ZoneTransitionStyle.VERY_SLOW;
			card2.SetTransitionStyle(transitionStyle);
			card2.SetDoNotSort(false);
			card2.SetDoNotWarpToNewZone(false);
			card2.SetInputEnabled(true);
			SceneUtils.SetLayer(card2.gameObject, this.previousLayer, null);
		}
		Zone zone = this.m_newCards[0].GetCard().GetZone();
		zone.RemoveLayoutBlocker();
		zone.UpdateLayout();
		this.m_effectsPendingFinish--;
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x04005A5A RID: 23130
	private List<Entity> m_newCards;

	// Token: 0x04005A5B RID: 23131
	private List<int> m_fullEntityTaskIndices;

	// Token: 0x04005A5C RID: 23132
	private List<Transform> cardDestinations = new List<Transform>();

	// Token: 0x04005A5D RID: 23133
	private Transform cardSpawningPosition;

	// Token: 0x04005A5E RID: 23134
	public float m_CardFlyOutTime = 2f;

	// Token: 0x04005A5F RID: 23135
	public float m_CardHangTime = 3f;

	// Token: 0x04005A60 RID: 23136
	private int previousLayer;
}
