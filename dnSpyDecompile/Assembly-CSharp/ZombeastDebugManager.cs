using System;
using Hearthstone;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class ZombeastDebugManager : MonoBehaviour
{
	// Token: 0x060021FE RID: 8702 RVA: 0x000A7CC5 File Offset: 0x000A5EC5
	public static ZombeastDebugManager Get()
	{
		if (ZombeastDebugManager.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			ZombeastDebugManager.s_instance = gameObject.AddComponent<ZombeastDebugManager>();
			gameObject.name = "ZombeastDebugManager (Dynamically created)";
		}
		return ZombeastDebugManager.s_instance;
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x000A7CF4 File Offset: 0x000A5EF4
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
		int tag = friendlySidePlayer.GetTag(GAME_TAG.ZOMBEAST_DEBUG_CURRENT_BEAST_DATABASE_ID);
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
		string text = string.Format("Zombeast being generated: {0}\nGenerated: {1}/{2}", arg, friendlySidePlayer.GetTag(GAME_TAG.ZOMBEAST_DEBUG_CURRENT_ITERATION), friendlySidePlayer.GetTag(GAME_TAG.ZOMBEAST_DEBUG_MAX_ITERATIONS));
		Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
		DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
	}

	// Token: 0x040012CA RID: 4810
	private static ZombeastDebugManager s_instance;
}
