using System;
using Hearthstone;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class DrustvarHorrorDebugManager : MonoBehaviour
{
	// Token: 0x060021B7 RID: 8631 RVA: 0x000A5D3E File Offset: 0x000A3F3E
	public static DrustvarHorrorDebugManager Get()
	{
		if (DrustvarHorrorDebugManager.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			DrustvarHorrorDebugManager.s_instance = gameObject.AddComponent<DrustvarHorrorDebugManager>();
			gameObject.name = "DrustvarHorrorDebugManager (Dynamically created)";
		}
		return DrustvarHorrorDebugManager.s_instance;
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x000A5D6C File Offset: 0x000A3F6C
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
		Player friendlySidePlayer = gameState.GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return;
		}
		int tag = friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_CURRENT_SPELL_DATABASE_ID);
		if (tag == 0)
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
		string arg = "Unknown";
		if (entityDef != null)
		{
			arg = entityDef.GetName();
		}
		string text = string.Format("Horror being generated: {0}\nGenerated: {1}/{2}", arg, friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_CURRENT_ITERATION), friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_MAX_ITERATIONS));
		Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
		DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
	}

	// Token: 0x040012A4 RID: 4772
	private static DrustvarHorrorDebugManager s_instance;
}
