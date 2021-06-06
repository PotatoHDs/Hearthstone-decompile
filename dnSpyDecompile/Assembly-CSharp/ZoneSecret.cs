using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class ZoneSecret : Zone
{
	// Token: 0x060032FC RID: 13052 RVA: 0x00105A06 File Offset: 0x00103C06
	private void Awake()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x00105A27 File Offset: 0x00103C27
	public override void UpdateLayout()
	{
		this.m_updatingLayout++;
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.UpdateLayout_Phone();
			return;
		}
		this.UpdateLayout_Default();
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x00105A60 File Offset: 0x00103C60
	public List<Card> GetSecretCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSecret())
			{
				list.Add(card);
			}
		}
		return list;
	}

	// Token: 0x060032FF RID: 13055 RVA: 0x00105AD0 File Offset: 0x00103CD0
	public List<Card> GetSideQuestCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSideQuest())
			{
				list.Add(card);
			}
		}
		return list;
	}

	// Token: 0x06003300 RID: 13056 RVA: 0x00105B40 File Offset: 0x00103D40
	public List<Card> GetSigilCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSigil())
			{
				list.Add(card);
			}
		}
		return list;
	}

	// Token: 0x06003301 RID: 13057 RVA: 0x00105BB0 File Offset: 0x00103DB0
	public Entity GetPuzzleEntity()
	{
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsPuzzle())
			{
				return card.GetEntity();
			}
		}
		return null;
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x00105C20 File Offset: 0x00103E20
	public int GetSecretCount()
	{
		int num = 0;
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSecret())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x00105C88 File Offset: 0x00103E88
	public int GetSideQuestCount()
	{
		int num = 0;
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSideQuest())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x06003305 RID: 13061 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x06003306 RID: 13062 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x00105CF0 File Offset: 0x00103EF0
	private void UpdateLayout_Default()
	{
		this.SortQuestsToTop();
		Vector2 vector = new Vector2(1f, 2f);
		if (this.m_controller != null)
		{
			Card heroCard = this.m_controller.GetHeroCard();
			if (heroCard != null && heroCard.GetActor() != null)
			{
				Bounds bounds = heroCard.GetActor().GetMeshRenderer(false).bounds;
				vector.x = bounds.extents.x;
				vector.y = bounds.extents.z * 0.9f;
			}
		}
		float num = 0.6f * vector.y;
		int num2 = 0;
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (this.CanAnimateCard(card))
			{
				card.ShowCard();
				Vector3 position = base.transform.position;
				float num3 = (float)(i + 1 >> 1);
				bool flag = (i & 1) != 0;
				float num4;
				if (num3 > 2f)
				{
					num4 = 1f;
				}
				else if (Mathf.Approximately(num3, 1f))
				{
					num4 = 0.6f;
				}
				else
				{
					num4 = num3 / 2f;
				}
				if (!flag)
				{
					position.x += vector.x * num4;
				}
				else
				{
					position.x -= vector.x * num4;
				}
				position.z -= vector.y * (num4 * num4);
				if (num3 > 2f)
				{
					position.z -= num * (num3 - 2f);
				}
				iTween.Stop(card.gameObject);
				int transitionStyle = (int)card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle == 3)
				{
					card.EnableTransitioningZones(false);
					card.transform.position = position;
					card.transform.rotation = base.transform.rotation;
					card.transform.localScale = base.transform.localScale;
				}
				else
				{
					card.EnableTransitioningZones(true);
					num2++;
					iTween.MoveTo(card.gameObject, position, 1f);
					iTween.RotateTo(card.gameObject, base.transform.localEulerAngles, 1f);
					iTween.ScaleTo(card.gameObject, base.transform.localScale, 1f);
				}
			}
		}
		if (num2 > 0)
		{
			base.StartFinishLayoutTimer(1f);
			return;
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x00105F50 File Offset: 0x00104150
	private void UpdateLayout_Phone()
	{
		int num = 0;
		this.SortQuestsToTop();
		bool flag = this.HaveMainQuest();
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		this.GetZoneInfo(ref num5, ref num2, ref num3, ref num4);
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			Entity entity = card.GetEntity();
			if (this.CanAnimateCard(card))
			{
				iTween.Stop(card.gameObject);
				if (entity.IsSecret() && this.GetSecretCards().IndexOf(card) == 0)
				{
					if (!card.IsShown())
					{
						card.ShowExhaustedChange(entity.IsExhausted());
						card.ShowCard();
					}
					Actor actor = card.GetActor();
					if (actor != null)
					{
						actor.UpdateAllComponents();
					}
				}
				if (entity.IsSideQuest() && this.GetSideQuestCards().IndexOf(card) == 0)
				{
					if (!card.IsShown())
					{
						card.ShowExhaustedChange(entity.IsExhausted());
						card.ShowCard();
					}
					Actor actor2 = card.GetActor();
					if (actor2 != null)
					{
						actor2.UpdateAllComponents();
					}
				}
				if (entity.IsSigil() && this.GetSigilCards().IndexOf(card) == 0)
				{
					if (!card.IsShown())
					{
						card.ShowExhaustedChange(entity.IsExhausted());
						card.ShowCard();
					}
					Actor actor3 = card.GetActor();
					if (actor3 != null)
					{
						actor3.UpdateAllComponents();
					}
				}
				Vector3 vector = base.transform.position;
				if (num5 == 2 && !flag)
				{
					Vector3[] array = new Vector3[]
					{
						new Vector3(-0.5f, 0f, -0.1f),
						new Vector3(0.5f, 0f, -0.1f)
					};
					if (entity.IsSecret())
					{
						if (num2 >= array.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Secret Position overflow, use position 0 instead", Array.Empty<object>());
							num2 = 0;
						}
						vector += array[num2];
					}
					else if (entity.IsSigil())
					{
						if (num3 >= array.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Sigil Position overflow, use position 0 instead", Array.Empty<object>());
							num3 = 0;
						}
						vector += array[num3];
					}
					else if (entity.IsSideQuest())
					{
						if (num4 >= array.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Sidequest Position overflow, use position 0 instead", Array.Empty<object>());
							num4 = 0;
						}
						vector += array[num4];
					}
				}
				else if (num5 > 1)
				{
					Vector3[] array2 = new Vector3[]
					{
						new Vector3(0f, 0f, 0f),
						new Vector3(-0.7f, 0f, -0.2f),
						new Vector3(0.7f, 0f, -0.2f),
						new Vector3(-0.9f, 0f, -0.85f),
						new Vector3(0.9f, 0f, -0.85f)
					};
					if (entity.IsSecret())
					{
						if (num2 >= array2.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Secret Position overflow, use position 0 instead", Array.Empty<object>());
							num2 = 0;
						}
						vector += array2[num2];
					}
					else if (entity.IsSigil())
					{
						if (num3 >= array2.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Sigil Position overflow, use position 0 instead", Array.Empty<object>());
							num3 = 0;
						}
						vector += array2[num3];
					}
					else if (entity.IsSideQuest())
					{
						if (num4 >= array2.Length)
						{
							Log.Gameplay.PrintError("UpdateLayout_Phone() - Sidequest Position overflow, use position 0 instead", Array.Empty<object>());
							num4 = 0;
						}
						vector += array2[num4];
					}
				}
				int transitionStyle = (int)card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle == 3)
				{
					card.EnableTransitioningZones(false);
					card.transform.position = vector;
				}
				else
				{
					card.EnableTransitioningZones(true);
					num++;
					iTween.MoveTo(card.gameObject, vector, 1f);
				}
				card.transform.rotation = base.transform.rotation;
				card.transform.localScale = base.transform.localScale;
			}
		}
		if (num > 0)
		{
			base.StartFinishLayoutTimer(1f);
			return;
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x001063A8 File Offset: 0x001045A8
	private void SortQuestsToTop()
	{
		int num = 0;
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (card.GetEntity().IsQuest())
			{
				if (i > num)
				{
					this.m_cards.RemoveAt(i);
					this.m_cards.Insert(num, card);
				}
				num++;
			}
		}
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x00106408 File Offset: 0x00104608
	private void GetZoneInfo(ref int numCardTypes, ref int secretPos, ref int sigilPos, ref int sideQuestPos)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		numCardTypes = (secretPos = (sigilPos = (sideQuestPos = 0)));
		foreach (Card card in this.m_cards)
		{
			if (card.GetEntity().IsQuest() && !flag)
			{
				flag = true;
				numCardTypes++;
				secretPos++;
				sigilPos++;
				sideQuestPos++;
			}
			else if (card.GetEntity().IsSecret() && !flag2)
			{
				flag2 = true;
				numCardTypes++;
				sigilPos++;
				sideQuestPos++;
			}
			else if (card.GetEntity().IsSigil() && !flag3)
			{
				flag3 = true;
				numCardTypes++;
				sideQuestPos++;
			}
			else if (card.GetEntity().IsSideQuest() && !flag4)
			{
				flag4 = true;
				numCardTypes++;
			}
			else
			{
				Debug.LogWarningFormat("GetZoneInfo() - Unknown secret zone card type", Array.Empty<object>());
			}
		}
	}

	// Token: 0x0600330E RID: 13070 RVA: 0x00106524 File Offset: 0x00104724
	private bool HaveMainQuest()
	{
		using (List<Card>.Enumerator enumerator = this.m_cards.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetEntity().IsQuest())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600330F RID: 13071 RVA: 0x001059AB File Offset: 0x00103BAB
	private bool CanAnimateCard(Card card)
	{
		return !card.IsDoNotSort();
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x00106584 File Offset: 0x00104784
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		if (base.GetController().GetTag<TAG_PLAYSTATE>(GAME_TAG.PLAYSTATE) == TAG_PLAYSTATE.WON)
		{
			return;
		}
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (this.CanAnimateCard(card))
			{
				card.HideCard();
			}
		}
	}

	// Token: 0x04001C0C RID: 7180
	private const float MAX_LAYOUT_PYRAMID_LEVEL = 2f;

	// Token: 0x04001C0D RID: 7181
	private const float LAYOUT_ANIM_SEC = 1f;
}
