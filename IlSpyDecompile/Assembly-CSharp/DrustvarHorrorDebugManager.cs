using Hearthstone;
using UnityEngine;

public class DrustvarHorrorDebugManager : MonoBehaviour
{
	private static DrustvarHorrorDebugManager s_instance;

	public static DrustvarHorrorDebugManager Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<DrustvarHorrorDebugManager>();
			obj.name = "DrustvarHorrorDebugManager (Dynamically created)";
		}
		return s_instance;
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
		Player friendlySidePlayer = gameState.GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return;
		}
		int num = friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_CURRENT_SPELL_DATABASE_ID);
		if (num != 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(num);
			string arg = "Unknown";
			if (entityDef != null)
			{
				arg = entityDef.GetName();
			}
			string text = $"Horror being generated: {arg}\nGenerated: {friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_CURRENT_ITERATION)}/{friendlySidePlayer.GetTag(GAME_TAG.DRUSTVAR_HORROR_DEBUG_MAX_ITERATIONS)}";
			Vector3 position = new Vector3(Screen.width, Screen.height, 0f);
			DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true);
		}
	}
}
