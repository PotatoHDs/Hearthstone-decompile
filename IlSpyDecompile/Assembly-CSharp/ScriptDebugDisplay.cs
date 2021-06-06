using System;
using System.Collections.Generic;
using Hearthstone;
using PegasusGame;
using UnityEngine;

public class ScriptDebugDisplay : MonoBehaviour
{
	private static ScriptDebugDisplay s_instance;

	private List<ScriptDebugInformation> m_debugInformation = new List<ScriptDebugInformation>();

	public bool m_isDisplayed;

	private float m_currentDumpScrollBarValue = 1f;

	private float m_currentStatementScrollBarValue;

	public static ScriptDebugDisplay Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<ScriptDebugDisplay>();
			obj.name = "ScriptDebugDisplay (Dynamically created)";
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
		m_debugInformation.Clear();
	}

	public bool ToggleDebugDisplay(bool shouldDisplay)
	{
		m_isDisplayed = shouldDisplay;
		return true;
	}

	private void Update()
	{
		if (!HearthstoneApplication.IsPublic() && GameState.Get() != null && m_isDisplayed)
		{
			ScriptDebugInformation scriptDebugInformation = null;
			if (m_debugInformation.Count > 0)
			{
				scriptDebugInformation = m_debugInformation[GetCurrentDumpIndex()];
			}
			if (scriptDebugInformation != null)
			{
				UpdateDisplay(scriptDebugInformation);
			}
		}
	}

	private int GetCurrentDumpIndex()
	{
		int num = (int)(m_currentDumpScrollBarValue * (float)m_debugInformation.Count);
		if (num >= m_debugInformation.Count)
		{
			num = m_debugInformation.Count - 1;
		}
		return num;
	}

	private void UpdateDisplay(ScriptDebugInformation debugInfo)
	{
		string text = $"Script Debug: {debugInfo.EntityName} (ID{debugInfo.EntityID})\n";
		Vector3 position = new Vector3(Screen.width, Screen.height, 0f);
		Vector3 position2 = new Vector3(0f, Screen.height, 0f);
		int num = (int)(m_currentStatementScrollBarValue * (float)debugInfo.Calls.Count);
		if (num >= debugInfo.Calls.Count)
		{
			num = debugInfo.Calls.Count - 1;
		}
		int num2 = 0;
		foreach (ScriptDebugCall call in debugInfo.Calls)
		{
			string text2 = call.OpcodeName;
			if (num2 == num)
			{
				text = ((call.ErrorStrings.Count <= 0) ? AppendLine(text, $"<color=#00ff00ff>{text2}</color>") : AppendLine(text, $"<color=#ffff00ff>{text2}</color>"));
				string text3 = "Inputs";
				int num3 = 0;
				foreach (ScriptDebugVariable input in call.Inputs)
				{
					text3 = AppendVariable(text3, input, $"Input Variable {num3}");
					num3++;
				}
				if (call.ErrorStrings.Count > 0)
				{
					text3 = AppendLine(text3, "\n<color=#ff0000ff>ERRORS</color>");
					foreach (string errorString in call.ErrorStrings)
					{
						string stringToAppend = $"<color=#ff0000ff>{errorString}</color>";
						text3 = AppendLine(text3, stringToAppend);
					}
				}
				text3 = AppendLine(text3, "\nOutput");
				if (call.Output.IntValue.Count > 0 || call.Output.StringValue.Count > 0)
				{
					text3 = AppendVariable(text3, call.Output, $"Output Variable");
				}
				text3 = AppendLine(text3, "\nOther variables");
				num3 = 0;
				foreach (ScriptDebugVariable variable in call.Variables)
				{
					text3 = AppendVariable(text3, variable, $"Other Variable {num3}");
					num3++;
				}
				DebugTextManager.Get().DrawDebugText(text3, position2, 0f, screenSpace: true);
			}
			else
			{
				if (call.ErrorStrings.Count > 0)
				{
					text2 = $"<color=#ff0000ff>{text2}</color>";
				}
				text = AppendLine(text, text2);
			}
			num2++;
		}
		DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true, "ScriptDebugDisplayCallLog");
	}

	private string AppendVariable(string inspectString, ScriptDebugVariable variable, string defaultVariableName)
	{
		string text = "";
		string text2 = variable.VariableName;
		if (text2 == "")
		{
			text2 = defaultVariableName;
		}
		if (variable.IntValue.Count == 1)
		{
			text = $"{text2} ({variable.VariableType}): {variable.IntValue[0]}";
		}
		else if (variable.StringValue.Count == 1)
		{
			text = $"{text2} ({variable.VariableType}): {variable.StringValue[0]}";
		}
		else if (variable.IntValue.Count > 1)
		{
			text = $"{text2} ({variable.VariableType}): {variable.IntValue[0]}";
			for (int i = 1; i < variable.IntValue.Count; i++)
			{
				text = $"{text}, {variable.IntValue[i]}";
			}
		}
		else if (variable.StringValue.Count > 1)
		{
			text = $"{text2} ({variable.VariableType}):";
			for (int j = 0; j < variable.StringValue.Count; j++)
			{
				text = $"{text}\n{variable.StringValue[j]}";
			}
		}
		if (text != "")
		{
			inspectString = AppendLine(inspectString, text);
		}
		return inspectString;
	}

	private string AppendLine(string inputString, string stringToAppend)
	{
		return $"{inputString}\n{stringToAppend}";
	}

	private void OnGUI()
	{
		if (HearthstoneApplication.IsPublic() || GameState.Get() == null || !m_isDisplayed)
		{
			return;
		}
		GUI.Label(new Rect(5f, Screen.height - 100, 200f, 50f), "Statement to Inspect");
		if (m_debugInformation.Count > 1)
		{
			int currentDumpIndex = GetCurrentDumpIndex();
			ScriptDebugInformation scriptDebugInformation = m_debugInformation[currentDumpIndex];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			foreach (ScriptDebugInformation item in m_debugInformation)
			{
				if (!(item.PowerGUID == scriptDebugInformation.PowerGUID))
				{
					continue;
				}
				num++;
				if (!flag)
				{
					if (item == scriptDebugInformation)
					{
						flag = true;
					}
					else
					{
						num2++;
					}
				}
			}
			string text = $"{m_debugInformation.Count} Script dumps available, showing entry {num2 + 1}/{num} for {scriptDebugInformation.EntityName}\n(power {scriptDebugInformation.PowerGUID})";
			GUI.Label(new Rect(5f, Screen.height - 200, 400f, 50f), text);
			m_currentDumpScrollBarValue = GUI.HorizontalSlider(new Rect(5f, Screen.height - 160, 350f, 50f), m_currentDumpScrollBarValue, 0f, 1f);
		}
		int num3 = 400;
		if (m_debugInformation.Count > 0)
		{
			ScriptDebugInformation scriptDebugInformation2 = m_debugInformation[GetCurrentDumpIndex()];
			if (scriptDebugInformation2 != null)
			{
				num3 += scriptDebugInformation2.Calls.Count * 5;
			}
		}
		num3 = Math.Min(num3, 1500);
		m_currentStatementScrollBarValue = GUI.HorizontalSlider(new Rect(5f, Screen.height - 75, num3, 50f), m_currentStatementScrollBarValue, 0f, 1f);
	}

	public void OnScriptDebugInfo(ScriptDebugInformation debugInfo)
	{
		m_debugInformation.Add(debugInfo);
	}
}
