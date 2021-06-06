using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class DebugTextManager : MonoBehaviour
{
	// Token: 0x060021AF RID: 8623 RVA: 0x000A5910 File Offset: 0x000A3B10
	public static DebugTextManager Get()
	{
		if (DebugTextManager.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			DebugTextManager.s_instance = gameObject.AddComponent<DebugTextManager>();
			gameObject.name = "DebugTextManager (Dynamically created)";
			DebugTextManager.s_instance.debugTextStyle = new GUIStyle("box");
			DebugTextManager.s_instance.debugTextStyle.fontSize = 12;
			DebugTextManager.s_instance.debugTextStyle.fontStyle = FontStyle.Bold;
			DebugTextManager.s_instance.debugTextStyle.normal.textColor = Color.white;
			DebugTextManager.s_instance.debugTextStyle.alignment = TextAnchor.MiddleCenter;
		}
		return DebugTextManager.s_instance;
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x000A59AC File Offset: 0x000A3BAC
	public static Vector2 WorldPosToScreenPos(Vector3 position)
	{
		return Camera.main.WorldToScreenPoint(position);
	}

	// Token: 0x060021B1 RID: 8625 RVA: 0x000A59BE File Offset: 0x000A3BBE
	public Vector2 TextSize(string text)
	{
		return this.debugTextStyle.CalcSize(new GUIContent(text));
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x000A59D1 File Offset: 0x000A3BD1
	public void DrawDebugText(string text, Vector3 position, float duration = 5f, bool screenSpace = false, string requestIdentifier = "", GUIStyle textStyle = null)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			this.m_textRequests.Add(new DebugTextManager.DebugTextRequest(text, position, duration, screenSpace, requestIdentifier, textStyle));
		}
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x000A59F4 File Offset: 0x000A3BF4
	private void LateUpdate()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.m_textRequests.RemoveAll((DebugTextManager.DebugTextRequest x) => x.m_remainingDuration < 0f);
		this.m_textRequests.ForEach(delegate(DebugTextManager.DebugTextRequest x)
		{
			x.m_remainingDuration -= Time.deltaTime;
		});
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x000A5A60 File Offset: 0x000A3C60
	private void OnGUI()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		foreach (DebugTextManager.DebugTextRequest debugTextRequest in this.m_textRequests)
		{
			Vector3 vector;
			if (!debugTextRequest.m_screenSpace)
			{
				vector = Camera.main.WorldToScreenPoint(debugTextRequest.m_position);
			}
			else
			{
				vector = debugTextRequest.m_position;
			}
			Vector2 vector2;
			if (debugTextRequest.m_textStyle == null)
			{
				vector2 = this.debugTextStyle.CalcSize(new GUIContent(debugTextRequest.m_text));
			}
			else
			{
				vector2 = debugTextRequest.m_textStyle.CalcSize(new GUIContent(debugTextRequest.m_text));
			}
			Rect position = new Rect(vector.x - vector2.x / 2f, (float)Screen.height - vector.y - vector2.y / 2f, vector2.x, vector2.y);
			if (debugTextRequest.m_fitOnScreen)
			{
				if (position.x < 0f)
				{
					position.x = 0f;
				}
				else if (position.x + vector2.x > (float)Screen.width)
				{
					position.x = (float)Screen.width - vector2.x;
				}
				if (position.y < 0f)
				{
					position.y = 0f;
				}
				else if (position.y + vector2.y > (float)Screen.height)
				{
					position.y = (float)Screen.height - vector2.y;
				}
				if (vector2.y > (float)Screen.height)
				{
					float value = 0f;
					int hashCode;
					if (string.IsNullOrEmpty(debugTextRequest.m_requestIdentifier))
					{
						hashCode = debugTextRequest.m_text.GetHashCode();
					}
					else
					{
						hashCode = debugTextRequest.m_requestIdentifier.GetHashCode();
					}
					if (this.m_scrollBarValues.ContainsKey(hashCode))
					{
						value = this.m_scrollBarValues[hashCode];
					}
					int num = (int)position.x - 50;
					if (num <= 0)
					{
						num = (int)position.x + (int)vector2.x + 50;
					}
					this.m_scrollBarValues[hashCode] = GUI.VerticalSlider(new Rect((float)num, position.y + 10f, 100f, (float)(Screen.height - 100)), value, 0f, 1f);
					float num2 = vector2.y - (float)Screen.height;
					position.y -= num2 * this.m_scrollBarValues[hashCode];
				}
			}
			if (debugTextRequest.m_textStyle == null)
			{
				GUI.Box(position, debugTextRequest.m_text, this.debugTextStyle);
			}
			else
			{
				GUI.Box(position, debugTextRequest.m_text, debugTextRequest.m_textStyle);
			}
		}
	}

	// Token: 0x040012A0 RID: 4768
	private static DebugTextManager s_instance;

	// Token: 0x040012A1 RID: 4769
	private GUIStyle debugTextStyle;

	// Token: 0x040012A2 RID: 4770
	private List<DebugTextManager.DebugTextRequest> m_textRequests = new List<DebugTextManager.DebugTextRequest>();

	// Token: 0x040012A3 RID: 4771
	private Map<int, float> m_scrollBarValues = new Map<int, float>();

	// Token: 0x02001570 RID: 5488
	private class DebugTextRequest
	{
		// Token: 0x0600E034 RID: 57396 RVA: 0x003FCCA8 File Offset: 0x003FAEA8
		public DebugTextRequest(string text, Vector3 position, float duration, bool screenSpace, string requestIdentifier, GUIStyle textStyle = null)
		{
			this.m_text = text;
			this.m_position = position;
			this.m_remainingDuration = duration;
			this.m_screenSpace = screenSpace;
			this.m_fitOnScreen = true;
			this.m_requestIdentifier = requestIdentifier;
			this.m_textStyle = textStyle;
		}

		// Token: 0x0400ADD9 RID: 44505
		public string m_text;

		// Token: 0x0400ADDA RID: 44506
		public Vector3 m_position;

		// Token: 0x0400ADDB RID: 44507
		public float m_remainingDuration;

		// Token: 0x0400ADDC RID: 44508
		public bool m_screenSpace;

		// Token: 0x0400ADDD RID: 44509
		public bool m_fitOnScreen;

		// Token: 0x0400ADDE RID: 44510
		public string m_requestIdentifier;

		// Token: 0x0400ADDF RID: 44511
		public GUIStyle m_textStyle;
	}
}
