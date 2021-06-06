using System;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class RopeTimerDebugDisplay : MonoBehaviour
{
	// Token: 0x060021DC RID: 8668 RVA: 0x000A6C65 File Offset: 0x000A4E65
	public static RopeTimerDebugDisplay Get()
	{
		if (RopeTimerDebugDisplay.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			RopeTimerDebugDisplay.s_instance = gameObject.AddComponent<RopeTimerDebugDisplay>();
			gameObject.name = "RopeTimerDebugDisplay (Dynamically created)";
		}
		return RopeTimerDebugDisplay.s_instance;
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000A6C93 File Offset: 0x000A4E93
	private void Start()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.GameState_CreateGameEvent), null);
		}
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x000A6CBC File Offset: 0x000A4EBC
	public bool EnableDebugDisplay(string func, string[] args, string rawArgs)
	{
		Network.Get().DebugRopeTimer();
		this.m_isDisplayed = true;
		return true;
	}

	// Token: 0x060021E0 RID: 8672 RVA: 0x000A6CD0 File Offset: 0x000A4ED0
	public bool DisableDebugDisplay(string func, string[] args, string rawArgs)
	{
		Network.Get().DisableDebugRopeTimer();
		this.m_isDisplayed = false;
		return true;
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x000A6CE4 File Offset: 0x000A4EE4
	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (GameState.Get() == null)
		{
			return;
		}
		if (!this.m_isDisplayed)
		{
			return;
		}
		this.UpdateDisplay();
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x000A4B3B File Offset: 0x000A2D3B
	private string AppendLine(string inputString, string stringToAppend)
	{
		return string.Format("{0}\n{1}", inputString, stringToAppend);
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x000A6D08 File Offset: 0x000A4F08
	private void UpdateDisplay()
	{
		if (this.m_debugInformation == null)
		{
			return;
		}
		float num = (float)this.m_debugInformation.MicrosecondsRemainingInTurn / 1000000f;
		float num2 = (float)this.m_debugInformation.BaseMicrosecondsInTurn / 1000000f;
		float num3 = (float)this.m_debugInformation.SlushTimeInMicroseconds / 1000000f;
		float num4 = (float)this.m_debugInformation.TotalMicrosecondsInTurn / 1000000f;
		float num5 = (float)this.m_debugInformation.OpponentSlushTimeInMicroseconds / 1000000f;
		string text = string.Format("Rope Timer\n Time remaining in turn: {0:F1}\n Base turn time: {1:F1}\n SlushTime: {2:F1}\n Total turn time: {3:F1}\nSlush time for opponent: {4:F1}", new object[]
		{
			num,
			num2,
			num3,
			num4,
			num5
		});
		Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
		DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x000A6DF0 File Offset: 0x000A4FF0
	public void OnRopeTimerDebugInformation(RopeTimerDebugInformation debugInfo)
	{
		this.m_debugInformation = debugInfo;
	}

	// Token: 0x040012B7 RID: 4791
	private static RopeTimerDebugDisplay s_instance;

	// Token: 0x040012B8 RID: 4792
	private RopeTimerDebugInformation m_debugInformation;

	// Token: 0x040012B9 RID: 4793
	public bool m_isDisplayed;

	// Token: 0x040012BA RID: 4794
	private const float MICROSECONDS_IN_SECOND = 1000000f;
}
