using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class GameDebugDisplay : MonoBehaviour
{
	// Token: 0x060021BB RID: 8635 RVA: 0x000A5E25 File Offset: 0x000A4025
	public static GameDebugDisplay Get()
	{
		if (GameDebugDisplay.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			GameDebugDisplay.s_instance = gameObject.AddComponent<GameDebugDisplay>();
			gameObject.name = "GameDebugDisplay (Dynamically created)";
		}
		return GameDebugDisplay.s_instance;
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000A5E53 File Offset: 0x000A4053
	public bool ToggleEntityCount(string func, string[] args, string rawArgs)
	{
		this.m_showEntities = !this.m_showEntities;
		return true;
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000A5E65 File Offset: 0x000A4065
	public bool ToggleHideZeroTags(string func, string[] args, string rawArgs)
	{
		this.m_hideZeroTags = !this.m_hideZeroTags;
		return true;
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000A5E78 File Offset: 0x000A4078
	public bool AddTagToDisplay(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		for (int i = 0; i < args.Length; i++)
		{
			int num = 0;
			if (!int.TryParse(args[i], out num))
			{
				string text = args[i].Trim();
				if (text.Length > 0)
				{
					foreach (object obj in Enum.GetValues(typeof(GAME_TAG)))
					{
						int num2 = (int)obj;
						GAME_TAG game_TAG = (GAME_TAG)num2;
						if (game_TAG.ToString().ToLower().CompareTo(text.ToLower()) == 0)
						{
							num = num2;
							break;
						}
					}
				}
			}
			if (num != 0 && !this.m_tagsToDisplay.Contains((GAME_TAG)num))
			{
				this.m_tagsToDisplay.Add((GAME_TAG)num);
			}
		}
		return true;
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000A5F5C File Offset: 0x000A415C
	public bool RemoveTagToDisplay(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		for (int i = 0; i < args.Length; i++)
		{
			int item = int.Parse(args[i]);
			if (this.m_tagsToDisplay.Contains((GAME_TAG)item))
			{
				this.m_tagsToDisplay.Remove((GAME_TAG)item);
			}
		}
		return true;
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000A5FA4 File Offset: 0x000A41A4
	public bool RemoveAllTags(string func, string[] args, string rawArgs)
	{
		this.m_tagsToDisplay.Clear();
		return true;
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x000A5FB4 File Offset: 0x000A41B4
	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		Map<int, Entity> entityMap = gameState.GetEntityMap();
		string text = "";
		string text2 = "";
		foreach (KeyValuePair<int, Entity> keyValuePair in entityMap)
		{
			Entity value = keyValuePair.Value;
			if (value != null)
			{
				text = this.HandleCornerDisplay(value, GAME_TAG.DEBUG_DISPLAY_TAG_TOP_RIGHT, text);
				text2 = this.HandleCornerDisplay(value, GAME_TAG.DEBUG_DISPLAY_TAG_BOTTOM_RIGHT, text2);
			}
		}
		if (text != "")
		{
			DebugTextManager.Get().DrawDebugText(text, new Vector3((float)Screen.width - 150f, (float)Screen.height - 100f, 0f), 0f, true, "", null);
		}
		if (text2 != "")
		{
			DebugTextManager.Get().DrawDebugText(text2, new Vector3((float)Screen.width - 150f, 100f, 0f), 0f, true, "", null);
		}
		if (this.m_showEntities)
		{
			string text3 = "Entities: " + entityMap.Count;
			DebugTextManager.Get().DrawDebugText(text3, new Vector3(100f, 100f, 0f), 0f, true, "", null);
		}
		if (this.m_tagsToDisplay.Count == 0)
		{
			return;
		}
		Card mousedOverCard = InputManager.Get().GetMousedOverCard();
		Entity entity = null;
		RaycastHit raycastHit;
		if (mousedOverCard != null && mousedOverCard.GetEntity() != null)
		{
			entity = mousedOverCard.GetEntity();
		}
		else if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			GameObject gameObject = raycastHit.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButton>() != null || gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				entity = gameState.GetGameEntity();
			}
		}
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag == TAG_ZONE.HAND || zone.m_ServerTag == TAG_ZONE.PLAY || zone.m_ServerTag == TAG_ZONE.SECRET)
			{
				foreach (Card card in zone.GetCards())
				{
					Entity entity2 = card.GetEntity();
					if (entity == null || entity == entity2)
					{
						Vector3 vector = card.transform.position;
						if (zone.m_ServerTag == TAG_ZONE.HAND)
						{
							Vector3 vector2 = card.transform.forward;
							if (card.GetControllerSide() == Player.Side.OPPOSING)
							{
								vector2 *= -1.5f;
								if (card.GetController().IsRevealed())
								{
									vector2 = -vector2;
								}
							}
							vector += vector2;
						}
						if (entity != null)
						{
							this.DrawDebugTextForHighlightedCard(entity2, DebugTextManager.WorldPosToScreenPos(vector));
						}
						else
						{
							this.DrawDebugTextForCard(entity2, vector, false, false);
						}
						if (entity != null)
						{
							if (entity.IsHero())
							{
								Entity entity3;
								if (entity.IsControlledByFriendlySidePlayer())
								{
									entity3 = GameState.Get().GetFriendlySidePlayer();
								}
								else
								{
									entity3 = GameState.Get().GetOpposingSidePlayer();
								}
								Vector2 v = DebugTextManager.WorldPosToScreenPos(entity3.GetHeroCard().transform.position) + new Vector2(-300f, 0f);
								this.DrawDebugTextForHighlightedCard(entity3, v);
							}
							return;
						}
					}
				}
			}
		}
		if (entity == gameState.GetGameEntity())
		{
			this.DrawDebugTextForHighlightedCard(entity, DebugTextManager.WorldPosToScreenPos(EndTurnButton.Get().transform.position));
			return;
		}
		this.DrawDebugTextForCard(gameState.GetGameEntity(), EndTurnButton.Get().transform.position, false, false);
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			if (player != null && !(player.GetHeroCard() == null))
			{
				Vector2 v2 = DebugTextManager.WorldPosToScreenPos(player.GetHeroCard().transform.position) + new Vector2(-300f, 0f);
				this.DrawDebugTextForCard(player, v2, true, false);
			}
		}
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x000A6448 File Offset: 0x000A4648
	private string HandleCornerDisplay(Entity ent, GAME_TAG tag, string currentString)
	{
		if (ent.HasTag(tag))
		{
			if (currentString != "")
			{
				currentString += "\n";
			}
			GAME_TAG tag2 = (GAME_TAG)ent.GetTag(tag);
			string text = tag2.ToString();
			int num;
			if (!int.TryParse(text, out num))
			{
				text = string.Format("{0}: ", text);
			}
			else
			{
				text = "";
			}
			currentString = string.Format("{0}{1}\n{2}{3}", new object[]
			{
				currentString,
				ent.GetName(),
				text,
				ent.GetTag(tag2)
			});
		}
		return currentString;
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x000A64E4 File Offset: 0x000A46E4
	private void DrawDebugTextForHighlightedCard(Entity ent, Vector3 pos)
	{
		string text = this.DrawDebugTextForCard(ent, pos, true, true);
		Vector2 vector = DebugTextManager.Get().TextSize(text);
		Vector3 vector2 = new Vector3(0f, vector.y + 5f, 0f);
		if (ent.IsGame())
		{
			List<Entity> attachments = ent.GetAttachments();
			for (int i = 0; i < attachments.Count; i++)
			{
				Vector3 pos2;
				if (i % 2 == 0)
				{
					pos2 = pos + vector2 * (float)(i / 2 + 1);
				}
				else
				{
					pos2 = pos - vector2 * (float)(i / 2 + 1);
				}
				this.DrawDebugTextForCard(attachments[i], pos2, true, false);
			}
			return;
		}
		if (ent.IsControlledByOpposingSidePlayer())
		{
			vector2.y = -vector2.y;
		}
		foreach (Entity ent2 in ent.GetAttachments())
		{
			pos += vector2;
			this.DrawDebugTextForCard(ent2, pos, true, false);
		}
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x000A6600 File Offset: 0x000A4800
	private string DrawDebugTextForCard(Entity ent, Vector3 pos, bool screenSpace = false, bool forceShowZeroTags = false)
	{
		string text = "";
		for (int i = 0; i < this.m_tagsToDisplay.Count; i++)
		{
			GAME_TAG enumTag = this.m_tagsToDisplay[i];
			int tag = ent.GetTag(enumTag);
			if (forceShowZeroTags || !this.m_hideZeroTags || tag != 0)
			{
				text = string.Format("{0}\n{1}: {2}", text, enumTag.ToString(), tag);
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = ent.GetName() + text;
			DebugTextManager.Get().DrawDebugText(text, pos, 0f, screenSpace, "", null);
		}
		return text;
	}

	// Token: 0x040012A5 RID: 4773
	private static GameDebugDisplay s_instance;

	// Token: 0x040012A6 RID: 4774
	private bool m_showEntities;

	// Token: 0x040012A7 RID: 4775
	private bool m_hideZeroTags;

	// Token: 0x040012A8 RID: 4776
	private List<GAME_TAG> m_tagsToDisplay = new List<GAME_TAG>();
}
