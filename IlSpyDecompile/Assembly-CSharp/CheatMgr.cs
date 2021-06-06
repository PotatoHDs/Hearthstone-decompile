using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class CheatMgr : IService
{
	public delegate bool ProcessCheatCallback(string func, string[] args, string rawArgs);

	public delegate bool ProcessCheatAutofillCallback(string func, string[] args, string rawArgs, AutofillData autofillData);

	private Map<string, List<Delegate>> m_funcMap = new Map<string, List<Delegate>>();

	private Map<string, string> m_cheatAlias = new Map<string, string>();

	private Map<string, string> m_cheatDesc = new Map<string, string>();

	private Map<string, string> m_cheatArgs = new Map<string, string>();

	private Map<string, string> m_cheatExamples = new Map<string, string>();

	private Map<string, int> m_cheatCategoryIndex = new Map<string, int>();

	private List<string> m_categoryList = new List<string>();

	private Rect m_cheatInputBackground;

	private bool m_inputActive;

	private int m_lastRegisteredCategoryIndex = -1;

	private List<string> m_cheatHistory;

	private int m_cheatHistoryIndex = -1;

	private string m_cheatTextBeforeScrollingThruHistory;

	private string m_cheatTextBeforeAutofill;

	private int m_autofillMatchIndex = -1;

	private string m_lastAutofillParamFunc;

	private string m_lastAutofillParamPrefix;

	private string m_lastAutofillParamMatch;

	private const string DEFAULT_CATEGORY = "other";

	private const int MAX_HISTORY_LINES = 25;

	private GameObject m_sceneObject;

	private static CheatMgr s_instance;

	public Map<string, string> cheatDesc => m_cheatDesc;

	public Map<string, string> cheatArgs => m_cheatArgs;

	public Map<string, string> cheatExamples => m_cheatExamples;

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("CheatMgrSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_cheatHistory = new List<string>();
		if (!HearthstoneApplication.IsPublic())
		{
			Processor.RegisterOnGUIDelegate(OnGUI);
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		s_instance = null;
	}

	public static CheatMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = HearthstoneServices.Get<CheatMgr>();
		}
		return s_instance;
	}

	public IEnumerable<string> GetCheatCommands()
	{
		return m_funcMap.Keys;
	}

	public IEnumerable<string> GetCheatCategories()
	{
		return m_categoryList;
	}

	public bool HandleKeyboardInput()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		if (!InputCollection.GetKeyUp(KeyCode.BackQuote))
		{
			return false;
		}
		ShowConsole();
		return true;
	}

	public void ShowConsole()
	{
		Rect rect = (m_cheatInputBackground = new Rect(0f, 0f, 1f, 0.05f));
		m_cheatInputBackground.x *= (float)Screen.width * 0.95f;
		m_cheatInputBackground.y *= Screen.height;
		m_cheatInputBackground.width *= Screen.width;
		m_cheatInputBackground.height *= (float)Screen.height * 1.03f;
		m_inputActive = true;
		m_cheatHistoryIndex = -1;
		m_cheatTextBeforeAutofill = null;
		m_autofillMatchIndex = -1;
		ReadCheatHistoryOption();
		m_cheatTextBeforeScrollingThruHistory = null;
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = SceneObject,
			m_preprocessCallback = OnInputPreprocess,
			m_rect = rect,
			m_color = Color.white,
			m_completedCallback = OnInputComplete
		};
		UniversalInputManager.Get().UseTextInput(parms);
	}

	public void HideConsole()
	{
		UniversalInputManager.Get().CancelTextInput(SceneObject);
		m_inputActive = false;
	}

	private void ReadCheatHistoryOption()
	{
		string @string = Options.Get().GetString(Option.CHEAT_HISTORY);
		m_cheatHistory = new List<string>(@string.Split(';'));
	}

	private void WriteCheatHistoryOption()
	{
		Options.Get().SetString(Option.CHEAT_HISTORY, string.Join(";", m_cheatHistory.ToArray()));
	}

	private bool OnInputPreprocess(Event e)
	{
		if (e.type != EventType.KeyDown)
		{
			return false;
		}
		KeyCode keyCode = e.keyCode;
		if (keyCode == KeyCode.BackQuote && string.IsNullOrEmpty(UniversalInputManager.Get().GetInputText()))
		{
			UniversalInputManager.Get().CancelTextInput(SceneObject);
			return true;
		}
		if (m_cheatHistory.Count < 1)
		{
			return false;
		}
		if (keyCode == KeyCode.UpArrow)
		{
			if (m_cheatHistoryIndex >= m_cheatHistory.Count - 1)
			{
				return true;
			}
			string inputText = UniversalInputManager.Get().GetInputText();
			if (m_cheatTextBeforeScrollingThruHistory == null)
			{
				m_cheatTextBeforeScrollingThruHistory = inputText;
			}
			string text2 = m_cheatHistory[++m_cheatHistoryIndex];
			UniversalInputManager.Get().SetInputText(text2, moveCursorToEnd: true);
			return true;
		}
		if (keyCode == KeyCode.DownArrow)
		{
			string text3;
			if (m_cheatHistoryIndex <= 0)
			{
				m_cheatHistoryIndex = -1;
				if (m_cheatTextBeforeScrollingThruHistory == null)
				{
					return false;
				}
				text3 = m_cheatTextBeforeScrollingThruHistory;
				m_cheatTextBeforeScrollingThruHistory = null;
			}
			else
			{
				text3 = m_cheatHistory[--m_cheatHistoryIndex];
			}
			UniversalInputManager.Get().SetInputText(text3);
			return true;
		}
		if (keyCode == KeyCode.Tab && HearthstoneApplication.IsInternal())
		{
			string text = UniversalInputManager.Get().GetInputText();
			if (!text.Contains(' '))
			{
				bool flag = true;
				if (m_cheatTextBeforeAutofill != null)
				{
					text = m_cheatTextBeforeAutofill;
					flag = false;
				}
				else
				{
					m_cheatTextBeforeAutofill = text;
				}
				List<string> list = m_funcMap.Keys.Where((string f) => f.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
				if (list.Count > 0)
				{
					list.Sort();
					int index = 0;
					m_autofillMatchIndex += ((!Event.current.shift) ? 1 : (-1));
					if (m_autofillMatchIndex >= list.Count)
					{
						m_autofillMatchIndex = 0;
					}
					else if (m_autofillMatchIndex < 0)
					{
						m_autofillMatchIndex = list.Count - 1;
					}
					if (m_autofillMatchIndex >= 0 && m_autofillMatchIndex < list.Count)
					{
						index = m_autofillMatchIndex;
					}
					text = list[index];
					UniversalInputManager.Get().SetInputText(text, moveCursorToEnd: true);
					if (flag && list.Count > 1)
					{
						float num = 5f + Mathf.Max(0f, list.Count - 3);
						num *= Time.timeScale;
						UIStatus.Get().AddInfo("Available cheats:\n" + string.Join("   ", list.ToArray()), num);
					}
				}
			}
			else
			{
				string[] args;
				string rawArgs;
				string text4 = ParseFuncAndArgs(text, out args, out rawArgs);
				if (text4 == null)
				{
					return false;
				}
				UIStatus.Get().AddInfo("", 0f);
				if (CallCheatCallback(text4, args, rawArgs, isAutofill: true, Event.current.shift))
				{
					string text5;
					if (string.IsNullOrEmpty(m_lastAutofillParamPrefix) && rawArgs.EndsWith(" "))
					{
						text5 = rawArgs + m_lastAutofillParamMatch;
					}
					else
					{
						args[args.Length - 1] = m_lastAutofillParamMatch;
						text5 = string.Join(" ", args);
					}
					UniversalInputManager.Get().SetInputText(text4 + " " + text5, moveCursorToEnd: true);
				}
			}
		}
		else
		{
			bool flag2 = true;
			switch (keyCode)
			{
			case KeyCode.None:
			case KeyCode.RightArrow:
			case KeyCode.LeftArrow:
			case KeyCode.Insert:
			case KeyCode.Home:
			case KeyCode.End:
			case KeyCode.PageUp:
			case KeyCode.PageDown:
			case KeyCode.CapsLock:
			case KeyCode.RightShift:
			case KeyCode.LeftShift:
			case KeyCode.RightControl:
			case KeyCode.LeftControl:
			case KeyCode.RightAlt:
			case KeyCode.LeftAlt:
			case KeyCode.RightCommand:
			case KeyCode.LeftCommand:
			case KeyCode.LeftWindows:
			case KeyCode.RightWindows:
			case KeyCode.Menu:
				flag2 = false;
				break;
			}
			if (flag2)
			{
				if (m_autofillMatchIndex != -1 || m_lastAutofillParamPrefix != null)
				{
					UIStatus.Get().AddInfo("", 0f);
				}
				m_cheatTextBeforeAutofill = null;
				m_autofillMatchIndex = -1;
				m_lastAutofillParamFunc = null;
				m_lastAutofillParamPrefix = null;
				m_lastAutofillParamMatch = null;
			}
		}
		return false;
	}

	public void RegisterCategory(string cat)
	{
		cat = cat.ToLowerInvariant();
		string text = cat;
		while (!string.IsNullOrEmpty(text))
		{
			if (m_categoryList.IndexOf(text) < 0)
			{
				m_categoryList.Count();
				m_categoryList.Add(text);
			}
			int num = text.LastIndexOf(':');
			text = ((num > 0) ? text.Substring(0, num) : null);
		}
		m_lastRegisteredCategoryIndex = m_categoryList.IndexOf(cat);
	}

	public void DefaultCategory()
	{
		RegisterCategory("other");
	}

	public void RegisterCheatHandler(string func, ProcessCheatCallback callback, string desc = null, string argDesc = null, string exampleArgs = null)
	{
		RegisterCheatHandler_(func, callback);
		if (desc != null)
		{
			m_cheatDesc[func] = desc;
		}
		if (argDesc != null)
		{
			m_cheatArgs[func] = argDesc;
		}
		if (exampleArgs != null)
		{
			m_cheatExamples[func] = exampleArgs;
		}
	}

	public void RegisterCheatHandler(string func, ProcessCheatAutofillCallback callback, string desc = null, string argDesc = null, string exampleArgs = null)
	{
		RegisterCheatHandler_(func, callback);
		if (desc != null)
		{
			m_cheatDesc[func] = desc;
		}
		if (argDesc != null)
		{
			m_cheatArgs[func] = argDesc;
		}
		if (exampleArgs != null)
		{
			m_cheatExamples[func] = exampleArgs;
		}
	}

	public void RegisterCheatAlias(string func, params string[] aliases)
	{
		if (!m_funcMap.ContainsKey(func))
		{
			Debug.LogError($"CheatMgr.RegisterCheatAlias() - cannot register aliases for func {func} because it does not exist");
			return;
		}
		foreach (string key in aliases)
		{
			m_cheatAlias[key] = func;
		}
	}

	public void UnregisterCheatHandler(string func, ProcessCheatCallback callback)
	{
		UnregisterCheatHandler_(func, callback);
	}

	public void UnregisterCheatHandler(string func, ProcessCheatAutofillCallback callback)
	{
		UnregisterCheatHandler_(func, callback);
	}

	public void OnGUI()
	{
		if (m_inputActive)
		{
			if (!UniversalInputManager.Get().IsTextInputActive())
			{
				m_inputActive = false;
				return;
			}
			GUI.depth = 1000;
			GUI.backgroundColor = Color.black;
			GUI.Box(m_cheatInputBackground, GUIContent.none);
			GUI.Box(m_cheatInputBackground, GUIContent.none);
			GUI.Box(m_cheatInputBackground, GUIContent.none);
		}
	}

	public string GetCheatCategory(string cheat)
	{
		if (m_cheatCategoryIndex.TryGetValue(cheat, out var value) && value >= 0)
		{
			return m_categoryList[value];
		}
		return "other";
	}

	private void RegisterCheatHandler_(string func, Delegate callback)
	{
		if (string.IsNullOrEmpty(func.Trim()))
		{
			Debug.LogError("CheatMgr.RegisterCheatHandler() - FAILED to register a null, empty, or all-whitespace function name");
			return;
		}
		if (m_funcMap.TryGetValue(func, out var value))
		{
			if (!value.Contains(callback))
			{
				value.Add(callback);
			}
		}
		else
		{
			value = new List<Delegate>();
			m_funcMap.Add(func, value);
			value.Add(callback);
		}
		m_cheatCategoryIndex[func] = m_lastRegisteredCategoryIndex;
	}

	private void UnregisterCheatHandler_(string func, Delegate callback)
	{
		if (m_funcMap.TryGetValue(func, out var value))
		{
			value.Remove(callback);
		}
	}

	private void OnInputComplete(string inputCommand)
	{
		m_inputActive = false;
		inputCommand = inputCommand.TrimStart();
		if (!string.IsNullOrEmpty(inputCommand))
		{
			m_cheatTextBeforeAutofill = null;
			m_autofillMatchIndex = -1;
			string text = ProcessCheat(inputCommand);
			if (!string.IsNullOrEmpty(text))
			{
				UIStatus.Get().AddError(text, 4f);
			}
		}
	}

	private string ParseFuncAndArgs(string inputCommand, out string[] args, out string rawArgs)
	{
		rawArgs = null;
		args = null;
		string text = ExtractFunc(inputCommand);
		if (text == null)
		{
			return null;
		}
		int length = text.Length;
		if (length == inputCommand.Length)
		{
			rawArgs = "";
			args = new string[1];
			args[0] = "";
		}
		else
		{
			rawArgs = inputCommand.Remove(0, length + 1);
			MatchCollection matchCollection = Regex.Matches(rawArgs, "\\S+");
			if (matchCollection.Count == 0)
			{
				args = new string[1];
				args[0] = "";
			}
			else
			{
				args = new string[matchCollection.Count];
				for (int i = 0; i < matchCollection.Count; i++)
				{
					args[i] = matchCollection[i].Value;
				}
			}
		}
		return text;
	}

	public string RunCheatInternally(string inputCommand)
	{
		string[] args;
		string rawArgs;
		string text = ParseFuncAndArgs(inputCommand, out args, out rawArgs);
		if (text == null)
		{
			return "\"" + inputCommand.Split(' ')[0] + "\" cheat command not found!";
		}
		if (!CallCheatCallback(text, args, rawArgs, isAutofill: false, isShiftTab: false))
		{
			return "\"" + text + "\" cheat command executed, but failed!";
		}
		return null;
	}

	public string ProcessCheat(string inputCommand, bool doNotSaveToHistory = false)
	{
		if (!doNotSaveToHistory)
		{
			if (m_cheatHistory.Count < 1 || !m_cheatHistory[0].Equals(inputCommand))
			{
				m_cheatHistory.Remove(inputCommand);
				m_cheatHistory.Insert(0, inputCommand);
			}
			if (m_cheatHistory.Count > 25)
			{
				m_cheatHistory.RemoveRange(24, m_cheatHistory.Count - 25);
			}
			m_cheatHistoryIndex = -1;
			m_cheatTextBeforeScrollingThruHistory = null;
			WriteCheatHistoryOption();
		}
		string[] args;
		string rawArgs;
		string text = ParseFuncAndArgs(inputCommand, out args, out rawArgs);
		if (text == null)
		{
			return "\"" + inputCommand.Split(' ')[0] + "\" cheat command not found!";
		}
		UIStatus.Get().AddInfo("", 0f);
		if (!CallCheatCallback(text, args, rawArgs, isAutofill: false, isShiftTab: false))
		{
			return "\"" + text + "\" cheat command executed, but failed!";
		}
		return null;
	}

	private bool CallCheatCallback(string func, string[] args, string rawArgs, bool isAutofill, bool isShiftTab)
	{
		string originalFunc = GetOriginalFunc(func);
		List<Delegate> list = m_funcMap[originalFunc];
		bool flag = false;
		for (int i = 0; i < list.Count; i++)
		{
			Delegate @delegate = list[i];
			if (@delegate is ProcessCheatCallback && !isAutofill)
			{
				flag = ((ProcessCheatCallback)@delegate)(func, args, rawArgs) || flag;
			}
			else if (@delegate is ProcessCheatAutofillCallback)
			{
				if (isAutofill && func != m_lastAutofillParamFunc)
				{
					m_lastAutofillParamMatch = null;
				}
				ProcessCheatAutofillCallback obj = (ProcessCheatAutofillCallback)@delegate;
				AutofillData autofillData = null;
				if (isAutofill)
				{
					autofillData = new AutofillData
					{
						m_isShiftTab = isShiftTab,
						m_lastAutofillParamPrefix = m_lastAutofillParamPrefix,
						m_lastAutofillParamMatch = m_lastAutofillParamMatch
					};
				}
				flag = obj(func, args, rawArgs, autofillData) || flag;
				if (isAutofill && flag)
				{
					m_lastAutofillParamFunc = func;
					m_lastAutofillParamPrefix = autofillData.m_lastAutofillParamPrefix;
					m_lastAutofillParamMatch = autofillData.m_lastAutofillParamMatch;
				}
			}
		}
		return flag;
	}

	private string ExtractFunc(string inputCommand)
	{
		inputCommand = inputCommand.TrimStart('/');
		inputCommand = inputCommand.Trim();
		int num = 0;
		List<string> list = new List<string>();
		foreach (string key in m_funcMap.Keys)
		{
			list.Add(key);
			if (key.Length > list[num].Length)
			{
				num = list.Count - 1;
			}
		}
		foreach (string key2 in m_cheatAlias.Keys)
		{
			list.Add(key2);
			if (key2.Length > list[num].Length)
			{
				num = list.Count - 1;
			}
		}
		int i;
		for (i = 0; i < inputCommand.Length; i++)
		{
			char c = inputCommand[i];
			int num2 = 0;
			while (num2 < list.Count)
			{
				string text = list[num2];
				if (i == text.Length)
				{
					if (char.IsWhiteSpace(c))
					{
						return text;
					}
					list.RemoveAt(num2);
					if (num2 <= num)
					{
						num = ComputeLongestFuncIndex(list);
					}
				}
				else if (text[i] != c)
				{
					list.RemoveAt(num2);
					if (num2 <= num)
					{
						num = ComputeLongestFuncIndex(list);
					}
				}
				else
				{
					num2++;
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
		}
		if (list.Count > 1)
		{
			foreach (string item in list)
			{
				if (inputCommand == item)
				{
					return item;
				}
			}
			return null;
		}
		string text2 = list[0];
		if (i < text2.Length)
		{
			return null;
		}
		return text2;
	}

	private int ComputeLongestFuncIndex(List<string> funcs)
	{
		int num = 0;
		for (int i = 1; i < funcs.Count; i++)
		{
			if (funcs[i].Length > funcs[num].Length)
			{
				num = i;
			}
		}
		return num;
	}

	private string GetOriginalFunc(string func)
	{
		if (!m_cheatAlias.TryGetValue(func, out var value))
		{
			return func;
		}
		return value;
	}
}
