using Hearthstone;
using PegasusGame;
using UnityEngine;

public class RopeTimerDebugDisplay : MonoBehaviour
{
	private static RopeTimerDebugDisplay s_instance;

	private RopeTimerDebugInformation m_debugInformation;

	public bool m_isDisplayed;

	private const float MICROSECONDS_IN_SECOND = 1000000f;

	public static RopeTimerDebugDisplay Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<RopeTimerDebugDisplay>();
			obj.name = "RopeTimerDebugDisplay (Dynamically created)";
		}
		return s_instance;
	}

	private void Start()
	{
		if (!HearthstoneApplication.IsPublic() && GameState.Get() != null)
		{
			GameState.Get().RegisterCreateGameListener(GameState_CreateGameEvent, null);
		}
	}

	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
	}

	public bool EnableDebugDisplay(string func, string[] args, string rawArgs)
	{
		Network.Get().DebugRopeTimer();
		m_isDisplayed = true;
		return true;
	}

	public bool DisableDebugDisplay(string func, string[] args, string rawArgs)
	{
		Network.Get().DisableDebugRopeTimer();
		m_isDisplayed = false;
		return true;
	}

	private void Update()
	{
		if (!HearthstoneApplication.IsPublic() && GameState.Get() != null && m_isDisplayed)
		{
			UpdateDisplay();
		}
	}

	private string AppendLine(string inputString, string stringToAppend)
	{
		return $"{inputString}\n{stringToAppend}";
	}

	private void UpdateDisplay()
	{
		if (m_debugInformation != null)
		{
			float num = (float)m_debugInformation.MicrosecondsRemainingInTurn / 1000000f;
			float num2 = (float)m_debugInformation.BaseMicrosecondsInTurn / 1000000f;
			float num3 = (float)m_debugInformation.SlushTimeInMicroseconds / 1000000f;
			float num4 = (float)m_debugInformation.TotalMicrosecondsInTurn / 1000000f;
			float num5 = (float)m_debugInformation.OpponentSlushTimeInMicroseconds / 1000000f;
			string text = $"Rope Timer\n Time remaining in turn: {num:F1}\n Base turn time: {num2:F1}\n SlushTime: {num3:F1}\n Total turn time: {num4:F1}\nSlush time for opponent: {num5:F1}";
			Vector3 position = new Vector3(Screen.width, Screen.height, 0f);
			DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true);
		}
	}

	public void OnRopeTimerDebugInformation(RopeTimerDebugInformation debugInfo)
	{
		m_debugInformation = debugInfo;
	}
}
