using System.Collections.Generic;
using UnityEngine;

public class ZoneSecret : Zone
{
	private const float MAX_LAYOUT_PYRAMID_LEVEL = 2f;

	private const float LAYOUT_ANIM_SEC = 1f;

	private void Awake()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterGameOverListener(OnGameOver);
		}
	}

	public override void UpdateLayout()
	{
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			UpdateLayout_Phone();
		}
		else
		{
			UpdateLayout_Default();
		}
	}

	public List<Card> GetSecretCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSecret())
			{
				list.Add(card);
			}
		}
		return list;
	}

	public List<Card> GetSideQuestCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSideQuest())
			{
				list.Add(card);
			}
		}
		return list;
	}

	public List<Card> GetSigilCards()
	{
		List<Card> list = new List<Card>();
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSigil())
			{
				list.Add(card);
			}
		}
		return list;
	}

	public Entity GetPuzzleEntity()
	{
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsPuzzle())
			{
				return card.GetEntity();
			}
		}
		return null;
	}

	public int GetSecretCount()
	{
		int num = 0;
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSecret())
			{
				num++;
			}
		}
		return num;
	}

	public int GetSideQuestCount()
	{
		int num = 0;
		foreach (Card card in m_cards)
		{
			if (card.GetEntity() != null && card.GetEntity().IsSideQuest())
			{
				num++;
			}
		}
		return num;
	}

	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	private void UpdateLayout_Default()
	{
		SortQuestsToTop();
		Vector2 vector = new Vector2(1f, 2f);
		if (m_controller != null)
		{
			Card heroCard = m_controller.GetHeroCard();
			if (heroCard != null && heroCard.GetActor() != null)
			{
				Bounds bounds = heroCard.GetActor().GetMeshRenderer().bounds;
				vector.x = bounds.extents.x;
				vector.y = bounds.extents.z * 0.9f;
			}
		}
		float num = 0.6f * vector.y;
		int num2 = 0;
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (CanAnimateCard(card))
			{
				card.ShowCard();
				Vector3 position = base.transform.position;
				float num3 = i + 1 >> 1;
				int num4 = i & 1;
				float num5 = ((num3 > 2f) ? 1f : ((!Mathf.Approximately(num3, 1f)) ? (num3 / 2f) : 0.6f));
				if (num4 == 0)
				{
					position.x += vector.x * num5;
				}
				else
				{
					position.x -= vector.x * num5;
				}
				position.z -= vector.y * (num5 * num5);
				if (num3 > 2f)
				{
					position.z -= num * (num3 - 2f);
				}
				iTween.Stop(card.gameObject);
				ZoneTransitionStyle transitionStyle = card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle == ZoneTransitionStyle.INSTANT)
				{
					card.EnableTransitioningZones(enable: false);
					card.transform.position = position;
					card.transform.rotation = base.transform.rotation;
					card.transform.localScale = base.transform.localScale;
				}
				else
				{
					card.EnableTransitioningZones(enable: true);
					num2++;
					iTween.MoveTo(card.gameObject, position, 1f);
					iTween.RotateTo(card.gameObject, base.transform.localEulerAngles, 1f);
					iTween.ScaleTo(card.gameObject, base.transform.localScale, 1f);
				}
			}
		}
		if (num2 > 0)
		{
			StartFinishLayoutTimer(1f);
		}
		else
		{
			UpdateLayoutFinished();
		}
	}

	private void UpdateLayout_Phone()
	{
		int num = 0;
		SortQuestsToTop();
		bool flag = HaveMainQuest();
		int secretPos = 0;
		int sigilPos = 0;
		int sideQuestPos = 0;
		int numCardTypes = 0;
		GetZoneInfo(ref numCardTypes, ref secretPos, ref sigilPos, ref sideQuestPos);
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			Entity entity = card.GetEntity();
			if (!CanAnimateCard(card))
			{
				continue;
			}
			iTween.Stop(card.gameObject);
			if (entity.IsSecret() && GetSecretCards().IndexOf(card) == 0)
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
			if (entity.IsSideQuest() && GetSideQuestCards().IndexOf(card) == 0)
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
			if (entity.IsSigil() && GetSigilCards().IndexOf(card) == 0)
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
			Vector3 position = base.transform.position;
			if (numCardTypes == 2 && !flag)
			{
				Vector3[] array = new Vector3[2]
				{
					new Vector3(-0.5f, 0f, -0.1f),
					new Vector3(0.5f, 0f, -0.1f)
				};
				if (entity.IsSecret())
				{
					if (secretPos >= array.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Secret Position overflow, use position 0 instead");
						secretPos = 0;
					}
					position += array[secretPos];
				}
				else if (entity.IsSigil())
				{
					if (sigilPos >= array.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Sigil Position overflow, use position 0 instead");
						sigilPos = 0;
					}
					position += array[sigilPos];
				}
				else if (entity.IsSideQuest())
				{
					if (sideQuestPos >= array.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Sidequest Position overflow, use position 0 instead");
						sideQuestPos = 0;
					}
					position += array[sideQuestPos];
				}
			}
			else if (numCardTypes > 1)
			{
				Vector3[] array2 = new Vector3[5]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(-0.7f, 0f, -0.2f),
					new Vector3(0.7f, 0f, -0.2f),
					new Vector3(-0.9f, 0f, -0.85f),
					new Vector3(0.9f, 0f, -0.85f)
				};
				if (entity.IsSecret())
				{
					if (secretPos >= array2.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Secret Position overflow, use position 0 instead");
						secretPos = 0;
					}
					position += array2[secretPos];
				}
				else if (entity.IsSigil())
				{
					if (sigilPos >= array2.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Sigil Position overflow, use position 0 instead");
						sigilPos = 0;
					}
					position += array2[sigilPos];
				}
				else if (entity.IsSideQuest())
				{
					if (sideQuestPos >= array2.Length)
					{
						Log.Gameplay.PrintError("UpdateLayout_Phone() - Sidequest Position overflow, use position 0 instead");
						sideQuestPos = 0;
					}
					position += array2[sideQuestPos];
				}
			}
			ZoneTransitionStyle transitionStyle = card.GetTransitionStyle();
			card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
			if (transitionStyle == ZoneTransitionStyle.INSTANT)
			{
				card.EnableTransitioningZones(enable: false);
				card.transform.position = position;
			}
			else
			{
				card.EnableTransitioningZones(enable: true);
				num++;
				iTween.MoveTo(card.gameObject, position, 1f);
			}
			card.transform.rotation = base.transform.rotation;
			card.transform.localScale = base.transform.localScale;
		}
		if (num > 0)
		{
			StartFinishLayoutTimer(1f);
		}
		else
		{
			UpdateLayoutFinished();
		}
	}

	private void SortQuestsToTop()
	{
		int num = 0;
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (card.GetEntity().IsQuest())
			{
				if (i > num)
				{
					m_cards.RemoveAt(i);
					m_cards.Insert(num, card);
				}
				num++;
			}
		}
	}

	private void GetZoneInfo(ref int numCardTypes, ref int secretPos, ref int sigilPos, ref int sideQuestPos)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		numCardTypes = (secretPos = (sigilPos = (sideQuestPos = 0)));
		foreach (Card card in m_cards)
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
				Debug.LogWarningFormat("GetZoneInfo() - Unknown secret zone card type");
			}
		}
	}

	private bool HaveMainQuest()
	{
		foreach (Card card in m_cards)
		{
			if (card.GetEntity().IsQuest())
			{
				return true;
			}
		}
		return false;
	}

	private bool CanAnimateCard(Card card)
	{
		if (card.IsDoNotSort())
		{
			return false;
		}
		return true;
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		if (GetController().GetTag<TAG_PLAYSTATE>(GAME_TAG.PLAYSTATE) == TAG_PLAYSTATE.WON)
		{
			return;
		}
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (CanAnimateCard(card))
			{
				card.HideCard();
			}
		}
	}
}
