using PegasusGame;
using UnityEngine;

public class DebugMessageManager : MonoBehaviour
{
	private static DebugMessageManager s_instance;

	public static DebugMessageManager Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<DebugMessageManager>();
			obj.name = "DebugMessageManager (Dynamically created)";
		}
		return s_instance;
	}

	public void OnDebugMessage(DebugMessage debugMessage)
	{
		Log.Gameplay.PrintAndForcePrintToScreen(Log.LogLevel.Info, verbose: false, debugMessage.Message);
	}
}
