using System;
using System.Collections.Generic;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class ScriptDebugDisplay : MonoBehaviour
{
	// Token: 0x060021E7 RID: 8679 RVA: 0x000A6DF9 File Offset: 0x000A4FF9
	public static ScriptDebugDisplay Get()
	{
		if (ScriptDebugDisplay.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			ScriptDebugDisplay.s_instance = gameObject.AddComponent<ScriptDebugDisplay>();
			gameObject.name = "ScriptDebugDisplay (Dynamically created)";
		}
		return ScriptDebugDisplay.s_instance;
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x000A6E27 File Offset: 0x000A5027
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

	// Token: 0x060021E9 RID: 8681 RVA: 0x000A6E50 File Offset: 0x000A5050
	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
		this.m_debugInformation.Clear();
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x000A6E5D File Offset: 0x000A505D
	public bool ToggleDebugDisplay(bool shouldDisplay)
	{
		this.m_isDisplayed = shouldDisplay;
		return true;
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x000A6E68 File Offset: 0x000A5068
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
		ScriptDebugInformation scriptDebugInformation = null;
		if (this.m_debugInformation.Count > 0)
		{
			scriptDebugInformation = this.m_debugInformation[this.GetCurrentDumpIndex()];
		}
		if (scriptDebugInformation == null)
		{
			return;
		}
		this.UpdateDisplay(scriptDebugInformation);
	}

	// Token: 0x060021EC RID: 8684 RVA: 0x000A6EBC File Offset: 0x000A50BC
	private int GetCurrentDumpIndex()
	{
		int num = (int)(this.m_currentDumpScrollBarValue * (float)this.m_debugInformation.Count);
		if (num >= this.m_debugInformation.Count)
		{
			num = this.m_debugInformation.Count - 1;
		}
		return num;
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x000A6EFC File Offset: 0x000A50FC
	private void UpdateDisplay(ScriptDebugInformation debugInfo)
	{
		string text = string.Format("Script Debug: {0} (ID{1})\n", debugInfo.EntityName, debugInfo.EntityID);
		Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
		Vector3 position2 = new Vector3(0f, (float)Screen.height, 0f);
		int num = (int)(this.m_currentStatementScrollBarValue * (float)debugInfo.Calls.Count);
		if (num >= debugInfo.Calls.Count)
		{
			num = debugInfo.Calls.Count - 1;
		}
		int num2 = 0;
		foreach (ScriptDebugCall scriptDebugCall in debugInfo.Calls)
		{
			string text2 = scriptDebugCall.OpcodeName;
			if (num2 == num)
			{
				if (scriptDebugCall.ErrorStrings.Count > 0)
				{
					text = this.AppendLine(text, string.Format("<color=#ffff00ff>{0}</color>", text2));
				}
				else
				{
					text = this.AppendLine(text, string.Format("<color=#00ff00ff>{0}</color>", text2));
				}
				string text3 = "Inputs";
				int num3 = 0;
				foreach (ScriptDebugVariable variable in scriptDebugCall.Inputs)
				{
					text3 = this.AppendVariable(text3, variable, string.Format("Input Variable {0}", num3));
					num3++;
				}
				if (scriptDebugCall.ErrorStrings.Count > 0)
				{
					text3 = this.AppendLine(text3, "\n<color=#ff0000ff>ERRORS</color>");
					foreach (string arg in scriptDebugCall.ErrorStrings)
					{
						string stringToAppend = string.Format("<color=#ff0000ff>{0}</color>", arg);
						text3 = this.AppendLine(text3, stringToAppend);
					}
				}
				text3 = this.AppendLine(text3, "\nOutput");
				if (scriptDebugCall.Output.IntValue.Count > 0 || scriptDebugCall.Output.StringValue.Count > 0)
				{
					text3 = this.AppendVariable(text3, scriptDebugCall.Output, string.Format("Output Variable", Array.Empty<object>()));
				}
				text3 = this.AppendLine(text3, "\nOther variables");
				num3 = 0;
				foreach (ScriptDebugVariable variable2 in scriptDebugCall.Variables)
				{
					text3 = this.AppendVariable(text3, variable2, string.Format("Other Variable {0}", num3));
					num3++;
				}
				DebugTextManager.Get().DrawDebugText(text3, position2, 0f, true, "", null);
			}
			else
			{
				if (scriptDebugCall.ErrorStrings.Count > 0)
				{
					text2 = string.Format("<color=#ff0000ff>{0}</color>", text2);
				}
				text = this.AppendLine(text, text2);
			}
			num2++;
		}
		DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "ScriptDebugDisplayCallLog", null);
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x000A7254 File Offset: 0x000A5454
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
			text = string.Format("{0} ({1}): {2}", text2, variable.VariableType, variable.IntValue[0]);
		}
		else if (variable.StringValue.Count == 1)
		{
			text = string.Format("{0} ({1}): {2}", text2, variable.VariableType, variable.StringValue[0]);
		}
		else if (variable.IntValue.Count > 1)
		{
			text = string.Format("{0} ({1}): {2}", text2, variable.VariableType, variable.IntValue[0]);
			for (int i = 1; i < variable.IntValue.Count; i++)
			{
				text = string.Format("{0}, {1}", text, variable.IntValue[i]);
			}
		}
		else if (variable.StringValue.Count > 1)
		{
			text = string.Format("{0} ({1}):", text2, variable.VariableType);
			for (int j = 0; j < variable.StringValue.Count; j++)
			{
				text = string.Format("{0}\n{1}", text, variable.StringValue[j]);
			}
		}
		if (text != "")
		{
			inspectString = this.AppendLine(inspectString, text);
		}
		return inspectString;
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x000A4B3B File Offset: 0x000A2D3B
	private string AppendLine(string inputString, string stringToAppend)
	{
		return string.Format("{0}\n{1}", inputString, stringToAppend);
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x000A73B0 File Offset: 0x000A55B0
	private void OnGUI()
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
		GUI.Label(new Rect(5f, (float)(Screen.height - 100), 200f, 50f), "Statement to Inspect");
		if (this.m_debugInformation.Count > 1)
		{
			int currentDumpIndex = this.GetCurrentDumpIndex();
			ScriptDebugInformation scriptDebugInformation = this.m_debugInformation[currentDumpIndex];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			foreach (ScriptDebugInformation scriptDebugInformation2 in this.m_debugInformation)
			{
				if (scriptDebugInformation2.PowerGUID == scriptDebugInformation.PowerGUID)
				{
					num++;
					if (!flag)
					{
						if (scriptDebugInformation2 == scriptDebugInformation)
						{
							flag = true;
						}
						else
						{
							num2++;
						}
					}
				}
			}
			string text = string.Format("{0} Script dumps available, showing entry {1}/{2} for {3}\n(power {4})", new object[]
			{
				this.m_debugInformation.Count,
				num2 + 1,
				num,
				scriptDebugInformation.EntityName,
				scriptDebugInformation.PowerGUID
			});
			GUI.Label(new Rect(5f, (float)(Screen.height - 200), 400f, 50f), text);
			this.m_currentDumpScrollBarValue = GUI.HorizontalSlider(new Rect(5f, (float)(Screen.height - 160), 350f, 50f), this.m_currentDumpScrollBarValue, 0f, 1f);
		}
		int num3 = 400;
		if (this.m_debugInformation.Count > 0)
		{
			ScriptDebugInformation scriptDebugInformation3 = this.m_debugInformation[this.GetCurrentDumpIndex()];
			if (scriptDebugInformation3 != null)
			{
				num3 += scriptDebugInformation3.Calls.Count * 5;
			}
		}
		num3 = Math.Min(num3, 1500);
		this.m_currentStatementScrollBarValue = GUI.HorizontalSlider(new Rect(5f, (float)(Screen.height - 75), (float)num3, 50f), this.m_currentStatementScrollBarValue, 0f, 1f);
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000A75C4 File Offset: 0x000A57C4
	public void OnScriptDebugInfo(ScriptDebugInformation debugInfo)
	{
		this.m_debugInformation.Add(debugInfo);
	}

	// Token: 0x040012BB RID: 4795
	private static ScriptDebugDisplay s_instance;

	// Token: 0x040012BC RID: 4796
	private List<ScriptDebugInformation> m_debugInformation = new List<ScriptDebugInformation>();

	// Token: 0x040012BD RID: 4797
	public bool m_isDisplayed;

	// Token: 0x040012BE RID: 4798
	private float m_currentDumpScrollBarValue = 1f;

	// Token: 0x040012BF RID: 4799
	private float m_currentStatementScrollBarValue;
}
