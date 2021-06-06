using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class GameDebugDisplay : MonoBehaviour
{
	private static GameDebugDisplay s_instance;

	private bool m_showEntities;

	private bool m_hideZeroTags;

	private List<GAME_TAG> m_tagsToDisplay = new List<GAME_TAG>();

	public static GameDebugDisplay Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<GameDebugDisplay>();
			obj.name = "GameDebugDisplay (Dynamically created)";
		}
		return s_instance;
	}

	public bool ToggleEntityCount(string func, string[] args, string rawArgs)
	{
		m_showEntities = !m_showEntities;
		return true;
	}

	public bool ToggleHideZeroTags(string func, string[] args, string rawArgs)
	{
		m_hideZeroTags = !m_hideZeroTags;
		return true;
	}

	public bool AddTagToDisplay(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		for (int i = 0; i < args.Length; i++)
		{
			int result = 0;
			if (!int.TryParse(args[i], out result))
			{
				string text = args[i].Trim();
				if (text.Length > 0)
				{
					foreach (int value in Enum.GetValues(typeof(GAME_TAG)))
					{
						GAME_TAG gAME_TAG = (GAME_TAG)value;
						if (gAME_TAG.ToString().ToLower().CompareTo(text.ToLower()) == 0)
						{
							result = value;
							break;
						}
					}
				}
			}
			if (result != 0 && !m_tagsToDisplay.Contains((GAME_TAG)result))
			{
				m_tagsToDisplay.Add((GAME_TAG)result);
			}
		}
		return true;
	}

	public bool RemoveTagToDisplay(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		for (int i = 0; i < args.Length; i++)
		{
			int item = int.Parse(args[i]);
			if (m_tagsToDisplay.Contains((GAME_TAG)item))
			{
				m_tagsToDisplay.Remove((GAME_TAG)item);
			}
		}
		return true;
	}

	public bool RemoveAllTags(string func, string[] args, string rawArgs)
	{
		m_tagsToDisplay.Clear();
		return true;
	}

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
		foreach (KeyValuePair<int, Entity> item in entityMap)
		{
			Entity value = item.Value;
			if (value != null)
			{
				text = HandleCornerDisplay(value, GAME_TAG.DEBUG_DISPLAY_TAG_TOP_RIGHT, text);
				text2 = HandleCornerDisplay(value, GAME_TAG.DEBUG_DISPLAY_TAG_BOTTOM_RIGHT, text2);
			}
		}
		if (text != "")
		{
			DebugTextManager.Get().DrawDebugText(text, new Vector3((float)Screen.width - 150f, (float)Screen.height - 100f, 0f), 0f, screenSpace: true);
		}
		if (text2 != "")
		{
			DebugTextManager.Get().DrawDebugText(text2, new Vector3((float)Screen.width - 150f, 100f, 0f), 0f, screenSpace: true);
		}
		if (m_showEntities)
		{
			string text3 = "Entities: " + entityMap.Count;
			DebugTextManager.Get().DrawDebugText(text3, new Vector3(100f, 100f, 0f), 0f, screenSpace: true);
		}
		if (m_tagsToDisplay.Count == 0)
		{
			return;
		}
		Card mousedOverCard = InputManager.Get().GetMousedOverCard();
		Entity entity = null;
		RaycastHit hitInfo;
		if (mousedOverCard != null && mousedOverCard.GetEntity() != null)
		{
			entity = mousedOverCard.GetEntity();
		}
		else if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out hitInfo))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButton>() != null || gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				entity = gameState.GetGameEntity();
			}
		}
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag != TAG_ZONE.HAND && zone.m_ServerTag != TAG_ZONE.PLAY && zone.m_ServerTag != TAG_ZONE.SECRET)
			{
				continue;
			}
			foreach (Card card in zone.GetCards())
			{
				Entity entity2 = card.GetEntity();
				if (entity != null && entity != entity2)
				{
					continue;
				}
				Vector3 position = card.transform.position;
				if (zone.m_ServerTag == TAG_ZONE.HAND)
				{
					Vector3 vector = card.transform.forward;
					if (card.GetControllerSide() == Player.Side.OPPOSING)
					{
						vector *= -1.5f;
						if (card.GetController().IsRevealed())
						{
							vector = -vector;
						}
					}
					position += vector;
				}
				if (entity != null)
				{
					DrawDebugTextForHighlightedCard(entity2, DebugTextManager.WorldPosToScreenPos(position));
				}
				else
				{
					DrawDebugTextForCard(entity2, position);
				}
				if (entity != null)
				{
					if (entity.IsHero())
					{
						Entity entity3 = ((!entity.IsControlledByFriendlySidePlayer()) ? GameState.Get().GetOpposingSidePlayer() : GameState.Get().GetFriendlySidePlayer());
						Vector2 vector2 = DebugTextManager.WorldPosToScreenPos(entity3.GetHeroCard().transform.position) + new Vector2(-300f, 0f);
						DrawDebugTextForHighlightedCard(entity3, vector2);
					}
					return;
				}
			}
		}
		if (entity == gameState.GetGameEntity())
		{
			DrawDebugTextForHighlightedCard(entity, DebugTextManager.WorldPosToScreenPos(EndTurnButton.Get().transform.position));
			return;
		}
		DrawDebugTextForCard(gameState.GetGameEntity(), EndTurnButton.Get().transform.position);
		foreach (Player value2 in GameState.Get().GetPlayerMap().Values)
		{
			if (value2 != null && !(value2.GetHeroCard() == null))
			{
				Vector2 vector3 = DebugTextManager.WorldPosToScreenPos(value2.GetHeroCard().transform.position) + new Vector2(-300f, 0f);
				DrawDebugTextForCard(value2, vector3, screenSpace: true);
			}
		}
	}

	private string HandleCornerDisplay(Entity ent, GAME_TAG tag, string currentString)
	{
		if (ent.HasTag(tag))
		{
			if (currentString != "")
			{
				currentString += "\n";
			}
			GAME_TAG enumTag = (GAME_TAG)ent.GetTag(tag);
			string text = enumTag.ToString();
			text = (int.TryParse(text, out var _) ? "" : $"{text}: ");
			currentString = $"{currentString}{ent.GetName()}\n{text}{ent.GetTag(enumTag)}";
		}
		return currentString;
	}

	private void DrawDebugTextForHighlightedCard(Entity ent, Vector3 pos)
	{
		string text = DrawDebugTextForCard(ent, pos, screenSpace: true, forceShowZeroTags: true);
		Vector3 vector = new Vector3(0f, DebugTextManager.Get().TextSize(text).y + 5f, 0f);
		if (ent.IsGame())
		{
			List<Entity> attachments = ent.GetAttachments();
			for (int i = 0; i < attachments.Count; i++)
			{
				Vector3 vector2 = pos;
				DrawDebugTextForCard(pos: (i % 2 != 0) ? (pos - vector * (i / 2 + 1)) : (pos + vector * (i / 2 + 1)), ent: attachments[i], screenSpace: true);
			}
			return;
		}
		if (ent.IsControlledByOpposingSidePlayer())
		{
			vector.y = 0f - vector.y;
		}
		foreach (Entity attachment in ent.GetAttachments())
		{
			pos += vector;
			DrawDebugTextForCard(attachment, pos, screenSpace: true);
		}
	}

	private string DrawDebugTextForCard(Entity ent, Vector3 pos, bool screenSpace = false, bool forceShowZeroTags = false)
	{
		string text = "";
		for (int i = 0; i < m_tagsToDisplay.Count; i++)
		{
			GAME_TAG enumTag = m_tagsToDisplay[i];
			int num = ent.GetTag(enumTag);
			if (forceShowZeroTags || !m_hideZeroTags || num != 0)
			{
				text = $"{text}\n{enumTag.ToString()}: {num}";
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = ent.GetName() + text;
			DebugTextManager.Get().DrawDebugText(text, pos, 0f, screenSpace);
		}
		return text;
	}
}
