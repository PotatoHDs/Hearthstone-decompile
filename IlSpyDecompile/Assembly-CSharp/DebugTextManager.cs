using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class DebugTextManager : MonoBehaviour
{
	private class DebugTextRequest
	{
		public string m_text;

		public Vector3 m_position;

		public float m_remainingDuration;

		public bool m_screenSpace;

		public bool m_fitOnScreen;

		public string m_requestIdentifier;

		public GUIStyle m_textStyle;

		public DebugTextRequest(string text, Vector3 position, float duration, bool screenSpace, string requestIdentifier, GUIStyle textStyle = null)
		{
			m_text = text;
			m_position = position;
			m_remainingDuration = duration;
			m_screenSpace = screenSpace;
			m_fitOnScreen = true;
			m_requestIdentifier = requestIdentifier;
			m_textStyle = textStyle;
		}
	}

	private static DebugTextManager s_instance;

	private GUIStyle debugTextStyle;

	private List<DebugTextRequest> m_textRequests = new List<DebugTextRequest>();

	private Map<int, float> m_scrollBarValues = new Map<int, float>();

	public static DebugTextManager Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<DebugTextManager>();
			obj.name = "DebugTextManager (Dynamically created)";
			s_instance.debugTextStyle = new GUIStyle("box");
			s_instance.debugTextStyle.fontSize = 12;
			s_instance.debugTextStyle.fontStyle = FontStyle.Bold;
			s_instance.debugTextStyle.normal.textColor = Color.white;
			s_instance.debugTextStyle.alignment = TextAnchor.MiddleCenter;
		}
		return s_instance;
	}

	public static Vector2 WorldPosToScreenPos(Vector3 position)
	{
		return Camera.main.WorldToScreenPoint(position);
	}

	public Vector2 TextSize(string text)
	{
		return debugTextStyle.CalcSize(new GUIContent(text));
	}

	public void DrawDebugText(string text, Vector3 position, float duration = 5f, bool screenSpace = false, string requestIdentifier = "", GUIStyle textStyle = null)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_textRequests.Add(new DebugTextRequest(text, position, duration, screenSpace, requestIdentifier, textStyle));
		}
	}

	private void LateUpdate()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_textRequests.RemoveAll((DebugTextRequest x) => x.m_remainingDuration < 0f);
			m_textRequests.ForEach(delegate(DebugTextRequest x)
			{
				x.m_remainingDuration -= Time.deltaTime;
			});
		}
	}

	private void OnGUI()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		foreach (DebugTextRequest textRequest in m_textRequests)
		{
			Vector3 vector = (textRequest.m_screenSpace ? textRequest.m_position : Camera.main.WorldToScreenPoint(textRequest.m_position));
			Vector2 vector2 = ((textRequest.m_textStyle != null) ? textRequest.m_textStyle.CalcSize(new GUIContent(textRequest.m_text)) : debugTextStyle.CalcSize(new GUIContent(textRequest.m_text)));
			Rect position = new Rect(vector.x - vector2.x / 2f, (float)Screen.height - vector.y - vector2.y / 2f, vector2.x, vector2.y);
			if (textRequest.m_fitOnScreen)
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
					int num = 0;
					num = ((!string.IsNullOrEmpty(textRequest.m_requestIdentifier)) ? textRequest.m_requestIdentifier.GetHashCode() : textRequest.m_text.GetHashCode());
					if (m_scrollBarValues.ContainsKey(num))
					{
						value = m_scrollBarValues[num];
					}
					int num2 = (int)position.x - 50;
					if (num2 <= 0)
					{
						num2 = (int)position.x + (int)vector2.x + 50;
					}
					m_scrollBarValues[num] = GUI.VerticalSlider(new Rect(num2, position.y + 10f, 100f, Screen.height - 100), value, 0f, 1f);
					float num3 = vector2.y - (float)Screen.height;
					position.y -= num3 * m_scrollBarValues[num];
				}
			}
			if (textRequest.m_textStyle == null)
			{
				GUI.Box(position, textRequest.m_text, debugTextStyle);
			}
			else
			{
				GUI.Box(position, textRequest.m_text, textRequest.m_textStyle);
			}
		}
	}
}
