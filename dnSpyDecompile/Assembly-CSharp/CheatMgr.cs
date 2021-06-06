using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020009A5 RID: 2469
public class CheatMgr : IService
{
	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x060086BB RID: 34491 RVA: 0x002B7CED File Offset: 0x002B5EED
	public Map<string, string> cheatDesc
	{
		get
		{
			return this.m_cheatDesc;
		}
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x060086BC RID: 34492 RVA: 0x002B7CF5 File Offset: 0x002B5EF5
	public Map<string, string> cheatArgs
	{
		get
		{
			return this.m_cheatArgs;
		}
	}

	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x060086BD RID: 34493 RVA: 0x002B7CFD File Offset: 0x002B5EFD
	public Map<string, string> cheatExamples
	{
		get
		{
			return this.m_cheatExamples;
		}
	}

	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x060086BE RID: 34494 RVA: 0x002B7D05 File Offset: 0x002B5F05
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("CheatMgrSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x060086BF RID: 34495 RVA: 0x002B7D3E File Offset: 0x002B5F3E
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_cheatHistory = new List<string>();
		if (!HearthstoneApplication.IsPublic())
		{
			Processor.RegisterOnGUIDelegate(new Action(this.OnGUI));
		}
		yield break;
	}

	// Token: 0x060086C0 RID: 34496 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060086C1 RID: 34497 RVA: 0x002B7D4D File Offset: 0x002B5F4D
	public void Shutdown()
	{
		CheatMgr.s_instance = null;
	}

	// Token: 0x060086C2 RID: 34498 RVA: 0x002B7D55 File Offset: 0x002B5F55
	public static CheatMgr Get()
	{
		if (CheatMgr.s_instance == null)
		{
			CheatMgr.s_instance = HearthstoneServices.Get<CheatMgr>();
		}
		return CheatMgr.s_instance;
	}

	// Token: 0x060086C3 RID: 34499 RVA: 0x002B7D6D File Offset: 0x002B5F6D
	public IEnumerable<string> GetCheatCommands()
	{
		return this.m_funcMap.Keys;
	}

	// Token: 0x060086C4 RID: 34500 RVA: 0x002B7D7A File Offset: 0x002B5F7A
	public IEnumerable<string> GetCheatCategories()
	{
		return this.m_categoryList;
	}

	// Token: 0x060086C5 RID: 34501 RVA: 0x002B7D82 File Offset: 0x002B5F82
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
		this.ShowConsole();
		return true;
	}

	// Token: 0x060086C6 RID: 34502 RVA: 0x002B7DA0 File Offset: 0x002B5FA0
	public void ShowConsole()
	{
		Rect rect = new Rect(0f, 0f, 1f, 0.05f);
		this.m_cheatInputBackground = rect;
		this.m_cheatInputBackground.x = this.m_cheatInputBackground.x * ((float)Screen.width * 0.95f);
		this.m_cheatInputBackground.y = this.m_cheatInputBackground.y * (float)Screen.height;
		this.m_cheatInputBackground.width = this.m_cheatInputBackground.width * (float)Screen.width;
		this.m_cheatInputBackground.height = this.m_cheatInputBackground.height * ((float)Screen.height * 1.03f);
		this.m_inputActive = true;
		this.m_cheatHistoryIndex = -1;
		this.m_cheatTextBeforeAutofill = null;
		this.m_autofillMatchIndex = -1;
		this.ReadCheatHistoryOption();
		this.m_cheatTextBeforeScrollingThruHistory = null;
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = this.SceneObject,
			m_preprocessCallback = new UniversalInputManager.TextInputPreprocessCallback(this.OnInputPreprocess),
			m_rect = rect,
			m_color = new Color?(Color.white),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(this.OnInputComplete)
		};
		UniversalInputManager.Get().UseTextInput(parms, false);
	}

	// Token: 0x060086C7 RID: 34503 RVA: 0x002B7EBD File Offset: 0x002B60BD
	public void HideConsole()
	{
		UniversalInputManager.Get().CancelTextInput(this.SceneObject, false);
		this.m_inputActive = false;
	}

	// Token: 0x060086C8 RID: 34504 RVA: 0x002B7ED8 File Offset: 0x002B60D8
	private void ReadCheatHistoryOption()
	{
		string @string = Options.Get().GetString(Option.CHEAT_HISTORY);
		this.m_cheatHistory = new List<string>(@string.Split(new char[]
		{
			';'
		}));
	}

	// Token: 0x060086C9 RID: 34505 RVA: 0x002B7F0E File Offset: 0x002B610E
	private void WriteCheatHistoryOption()
	{
		Options.Get().SetString(Option.CHEAT_HISTORY, string.Join(";", this.m_cheatHistory.ToArray()));
	}

	// Token: 0x060086CA RID: 34506 RVA: 0x002B7F34 File Offset: 0x002B6134
	private bool OnInputPreprocess(Event e)
	{
		if (e.type != EventType.KeyDown)
		{
			return false;
		}
		KeyCode keyCode = e.keyCode;
		if (keyCode == KeyCode.BackQuote && string.IsNullOrEmpty(UniversalInputManager.Get().GetInputText()))
		{
			UniversalInputManager.Get().CancelTextInput(this.SceneObject, false);
			return true;
		}
		if (this.m_cheatHistory.Count < 1)
		{
			return false;
		}
		if (keyCode == KeyCode.UpArrow)
		{
			if (this.m_cheatHistoryIndex >= this.m_cheatHistory.Count - 1)
			{
				return true;
			}
			string inputText = UniversalInputManager.Get().GetInputText();
			if (this.m_cheatTextBeforeScrollingThruHistory == null)
			{
				this.m_cheatTextBeforeScrollingThruHistory = inputText;
			}
			List<string> cheatHistory = this.m_cheatHistory;
			int num = this.m_cheatHistoryIndex + 1;
			this.m_cheatHistoryIndex = num;
			string text5 = cheatHistory[num];
			UniversalInputManager.Get().SetInputText(text5, true);
			return true;
		}
		else
		{
			if (keyCode == KeyCode.DownArrow)
			{
				string text2;
				if (this.m_cheatHistoryIndex <= 0)
				{
					this.m_cheatHistoryIndex = -1;
					if (this.m_cheatTextBeforeScrollingThruHistory == null)
					{
						return false;
					}
					text2 = this.m_cheatTextBeforeScrollingThruHistory;
					this.m_cheatTextBeforeScrollingThruHistory = null;
				}
				else
				{
					List<string> cheatHistory2 = this.m_cheatHistory;
					int num = this.m_cheatHistoryIndex - 1;
					this.m_cheatHistoryIndex = num;
					text2 = cheatHistory2[num];
				}
				UniversalInputManager.Get().SetInputText(text2, false);
				return true;
			}
			if (keyCode == KeyCode.Tab && HearthstoneApplication.IsInternal())
			{
				string text = UniversalInputManager.Get().GetInputText();
				if (!text.Contains(' '))
				{
					bool flag = true;
					if (this.m_cheatTextBeforeAutofill != null)
					{
						text = this.m_cheatTextBeforeAutofill;
						flag = false;
					}
					else
					{
						this.m_cheatTextBeforeAutofill = text;
					}
					List<string> list = (from f in this.m_funcMap.Keys
					where f.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)
					select f).ToList<string>();
					if (list.Count > 0)
					{
						list.Sort();
						int index = 0;
						this.m_autofillMatchIndex += (Event.current.shift ? -1 : 1);
						if (this.m_autofillMatchIndex >= list.Count)
						{
							this.m_autofillMatchIndex = 0;
						}
						else if (this.m_autofillMatchIndex < 0)
						{
							this.m_autofillMatchIndex = list.Count - 1;
						}
						if (this.m_autofillMatchIndex >= 0 && this.m_autofillMatchIndex < list.Count)
						{
							index = this.m_autofillMatchIndex;
						}
						text = list[index];
						UniversalInputManager.Get().SetInputText(text, true);
						if (flag && list.Count > 1)
						{
							float num2 = 5f + Mathf.Max(0f, (float)(list.Count - 3));
							num2 *= Time.timeScale;
							UIStatus.Get().AddInfo("Available cheats:\n" + string.Join("   ", list.ToArray()), num2);
						}
					}
				}
				else
				{
					string[] array;
					string text4;
					string text3 = this.ParseFuncAndArgs(text, out array, out text4);
					if (text3 == null)
					{
						return false;
					}
					UIStatus.Get().AddInfo("", 0f);
					if (this.CallCheatCallback(text3, array, text4, true, Event.current.shift))
					{
						string str;
						if (string.IsNullOrEmpty(this.m_lastAutofillParamPrefix) && text4.EndsWith(" "))
						{
							str = text4 + this.m_lastAutofillParamMatch;
						}
						else
						{
							array[array.Length - 1] = this.m_lastAutofillParamMatch;
							str = string.Join(" ", array);
						}
						UniversalInputManager.Get().SetInputText(text3 + " " + str, true);
					}
				}
			}
			else
			{
				bool flag2 = true;
				if (keyCode != KeyCode.None && keyCode - KeyCode.RightArrow > 6)
				{
					switch (keyCode)
					{
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
						break;
					case KeyCode.ScrollLock:
					case KeyCode.AltGr:
					case (KeyCode)314:
					case KeyCode.Help:
					case KeyCode.Print:
					case KeyCode.SysReq:
					case KeyCode.Break:
						goto IL_3C6;
					default:
						goto IL_3C6;
					}
				}
				flag2 = false;
				IL_3C6:
				if (flag2)
				{
					if (this.m_autofillMatchIndex != -1 || this.m_lastAutofillParamPrefix != null)
					{
						UIStatus.Get().AddInfo("", 0f);
					}
					this.m_cheatTextBeforeAutofill = null;
					this.m_autofillMatchIndex = -1;
					this.m_lastAutofillParamFunc = null;
					this.m_lastAutofillParamPrefix = null;
					this.m_lastAutofillParamMatch = null;
				}
			}
			return false;
		}
	}

	// Token: 0x060086CB RID: 34507 RVA: 0x002B8354 File Offset: 0x002B6554
	public void RegisterCategory(string cat)
	{
		cat = cat.ToLowerInvariant();
		string text = cat;
		while (!string.IsNullOrEmpty(text))
		{
			if (this.m_categoryList.IndexOf(text) < 0)
			{
				this.m_categoryList.Count<string>();
				this.m_categoryList.Add(text);
			}
			int num = text.LastIndexOf(':');
			text = ((num > 0) ? text.Substring(0, num) : null);
		}
		this.m_lastRegisteredCategoryIndex = this.m_categoryList.IndexOf(cat);
	}

	// Token: 0x060086CC RID: 34508 RVA: 0x002B83C7 File Offset: 0x002B65C7
	public void DefaultCategory()
	{
		this.RegisterCategory("other");
	}

	// Token: 0x060086CD RID: 34509 RVA: 0x002B83D4 File Offset: 0x002B65D4
	public void RegisterCheatHandler(string func, CheatMgr.ProcessCheatCallback callback, string desc = null, string argDesc = null, string exampleArgs = null)
	{
		this.RegisterCheatHandler_(func, callback);
		if (desc != null)
		{
			this.m_cheatDesc[func] = desc;
		}
		if (argDesc != null)
		{
			this.m_cheatArgs[func] = argDesc;
		}
		if (exampleArgs != null)
		{
			this.m_cheatExamples[func] = exampleArgs;
		}
	}

	// Token: 0x060086CE RID: 34510 RVA: 0x002B83D4 File Offset: 0x002B65D4
	public void RegisterCheatHandler(string func, CheatMgr.ProcessCheatAutofillCallback callback, string desc = null, string argDesc = null, string exampleArgs = null)
	{
		this.RegisterCheatHandler_(func, callback);
		if (desc != null)
		{
			this.m_cheatDesc[func] = desc;
		}
		if (argDesc != null)
		{
			this.m_cheatArgs[func] = argDesc;
		}
		if (exampleArgs != null)
		{
			this.m_cheatExamples[func] = exampleArgs;
		}
	}

	// Token: 0x060086CF RID: 34511 RVA: 0x002B8414 File Offset: 0x002B6614
	public void RegisterCheatAlias(string func, params string[] aliases)
	{
		if (!this.m_funcMap.ContainsKey(func))
		{
			Debug.LogError(string.Format("CheatMgr.RegisterCheatAlias() - cannot register aliases for func {0} because it does not exist", func));
			return;
		}
		foreach (string key in aliases)
		{
			this.m_cheatAlias[key] = func;
		}
	}

	// Token: 0x060086D0 RID: 34512 RVA: 0x002B8461 File Offset: 0x002B6661
	public void UnregisterCheatHandler(string func, CheatMgr.ProcessCheatCallback callback)
	{
		this.UnregisterCheatHandler_(func, callback);
	}

	// Token: 0x060086D1 RID: 34513 RVA: 0x002B8461 File Offset: 0x002B6661
	public void UnregisterCheatHandler(string func, CheatMgr.ProcessCheatAutofillCallback callback)
	{
		this.UnregisterCheatHandler_(func, callback);
	}

	// Token: 0x060086D2 RID: 34514 RVA: 0x002B846C File Offset: 0x002B666C
	public void OnGUI()
	{
		if (this.m_inputActive)
		{
			if (!UniversalInputManager.Get().IsTextInputActive())
			{
				this.m_inputActive = false;
				return;
			}
			GUI.depth = 1000;
			GUI.backgroundColor = Color.black;
			GUI.Box(this.m_cheatInputBackground, GUIContent.none);
			GUI.Box(this.m_cheatInputBackground, GUIContent.none);
			GUI.Box(this.m_cheatInputBackground, GUIContent.none);
		}
	}

	// Token: 0x060086D3 RID: 34515 RVA: 0x002B84DC File Offset: 0x002B66DC
	public string GetCheatCategory(string cheat)
	{
		int num;
		if (this.m_cheatCategoryIndex.TryGetValue(cheat, out num) && num >= 0)
		{
			return this.m_categoryList[num];
		}
		return "other";
	}

	// Token: 0x060086D4 RID: 34516 RVA: 0x002B8510 File Offset: 0x002B6710
	private void RegisterCheatHandler_(string func, Delegate callback)
	{
		if (string.IsNullOrEmpty(func.Trim()))
		{
			Debug.LogError("CheatMgr.RegisterCheatHandler() - FAILED to register a null, empty, or all-whitespace function name");
			return;
		}
		List<Delegate> list;
		if (this.m_funcMap.TryGetValue(func, out list))
		{
			if (!list.Contains(callback))
			{
				list.Add(callback);
			}
		}
		else
		{
			list = new List<Delegate>();
			this.m_funcMap.Add(func, list);
			list.Add(callback);
		}
		this.m_cheatCategoryIndex[func] = this.m_lastRegisteredCategoryIndex;
	}

	// Token: 0x060086D5 RID: 34517 RVA: 0x002B8584 File Offset: 0x002B6784
	private void UnregisterCheatHandler_(string func, Delegate callback)
	{
		List<Delegate> list;
		if (!this.m_funcMap.TryGetValue(func, out list))
		{
			return;
		}
		list.Remove(callback);
	}

	// Token: 0x060086D6 RID: 34518 RVA: 0x002B85AC File Offset: 0x002B67AC
	private void OnInputComplete(string inputCommand)
	{
		this.m_inputActive = false;
		inputCommand = inputCommand.TrimStart(Array.Empty<char>());
		if (string.IsNullOrEmpty(inputCommand))
		{
			return;
		}
		this.m_cheatTextBeforeAutofill = null;
		this.m_autofillMatchIndex = -1;
		string text = this.ProcessCheat(inputCommand, false);
		if (!string.IsNullOrEmpty(text))
		{
			UIStatus.Get().AddError(text, 4f);
		}
	}

	// Token: 0x060086D7 RID: 34519 RVA: 0x002B8608 File Offset: 0x002B6808
	private string ParseFuncAndArgs(string inputCommand, out string[] args, out string rawArgs)
	{
		rawArgs = null;
		args = null;
		string text = this.ExtractFunc(inputCommand);
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

	// Token: 0x060086D8 RID: 34520 RVA: 0x002B86B8 File Offset: 0x002B68B8
	public string RunCheatInternally(string inputCommand)
	{
		string[] args;
		string rawArgs;
		string text = this.ParseFuncAndArgs(inputCommand, out args, out rawArgs);
		if (text == null)
		{
			return "\"" + inputCommand.Split(new char[]
			{
				' '
			})[0] + "\" cheat command not found!";
		}
		if (!this.CallCheatCallback(text, args, rawArgs, false, false))
		{
			return "\"" + text + "\" cheat command executed, but failed!";
		}
		return null;
	}

	// Token: 0x060086D9 RID: 34521 RVA: 0x002B8718 File Offset: 0x002B6918
	public string ProcessCheat(string inputCommand, bool doNotSaveToHistory = false)
	{
		if (!doNotSaveToHistory)
		{
			if (this.m_cheatHistory.Count < 1 || !this.m_cheatHistory[0].Equals(inputCommand))
			{
				this.m_cheatHistory.Remove(inputCommand);
				this.m_cheatHistory.Insert(0, inputCommand);
			}
			if (this.m_cheatHistory.Count > 25)
			{
				this.m_cheatHistory.RemoveRange(24, this.m_cheatHistory.Count - 25);
			}
			this.m_cheatHistoryIndex = -1;
			this.m_cheatTextBeforeScrollingThruHistory = null;
			this.WriteCheatHistoryOption();
		}
		string[] args;
		string rawArgs;
		string text = this.ParseFuncAndArgs(inputCommand, out args, out rawArgs);
		if (text == null)
		{
			return "\"" + inputCommand.Split(new char[]
			{
				' '
			})[0] + "\" cheat command not found!";
		}
		UIStatus.Get().AddInfo("", 0f);
		if (!this.CallCheatCallback(text, args, rawArgs, false, false))
		{
			return "\"" + text + "\" cheat command executed, but failed!";
		}
		return null;
	}

	// Token: 0x060086DA RID: 34522 RVA: 0x002B8808 File Offset: 0x002B6A08
	private bool CallCheatCallback(string func, string[] args, string rawArgs, bool isAutofill, bool isShiftTab)
	{
		string originalFunc = this.GetOriginalFunc(func);
		List<Delegate> list = this.m_funcMap[originalFunc];
		bool flag = false;
		for (int i = 0; i < list.Count; i++)
		{
			Delegate @delegate = list[i];
			if (@delegate is CheatMgr.ProcessCheatCallback && !isAutofill)
			{
				flag = (((CheatMgr.ProcessCheatCallback)@delegate)(func, args, rawArgs) || flag);
			}
			else if (@delegate is CheatMgr.ProcessCheatAutofillCallback)
			{
				if (isAutofill && func != this.m_lastAutofillParamFunc)
				{
					this.m_lastAutofillParamMatch = null;
				}
				CheatMgr.ProcessCheatAutofillCallback processCheatAutofillCallback = (CheatMgr.ProcessCheatAutofillCallback)@delegate;
				AutofillData autofillData = null;
				if (isAutofill)
				{
					autofillData = new AutofillData();
					autofillData.m_isShiftTab = isShiftTab;
					autofillData.m_lastAutofillParamPrefix = this.m_lastAutofillParamPrefix;
					autofillData.m_lastAutofillParamMatch = this.m_lastAutofillParamMatch;
				}
				flag = (processCheatAutofillCallback(func, args, rawArgs, autofillData) || flag);
				if (isAutofill && flag)
				{
					this.m_lastAutofillParamFunc = func;
					this.m_lastAutofillParamPrefix = autofillData.m_lastAutofillParamPrefix;
					this.m_lastAutofillParamMatch = autofillData.m_lastAutofillParamMatch;
				}
			}
		}
		return flag;
	}

	// Token: 0x060086DB RID: 34523 RVA: 0x002B8904 File Offset: 0x002B6B04
	private string ExtractFunc(string inputCommand)
	{
		inputCommand = inputCommand.TrimStart(new char[]
		{
			'/'
		});
		inputCommand = inputCommand.Trim();
		int num = 0;
		List<string> list = new List<string>();
		foreach (string text in this.m_funcMap.Keys)
		{
			list.Add(text);
			if (text.Length > list[num].Length)
			{
				num = list.Count - 1;
			}
		}
		foreach (string text2 in this.m_cheatAlias.Keys)
		{
			list.Add(text2);
			if (text2.Length > list[num].Length)
			{
				num = list.Count - 1;
			}
		}
		int i;
		for (i = 0; i < inputCommand.Length; i++)
		{
			char c = inputCommand[i];
			int j = 0;
			while (j < list.Count)
			{
				string text3 = list[j];
				if (i == text3.Length)
				{
					if (char.IsWhiteSpace(c))
					{
						return text3;
					}
					list.RemoveAt(j);
					if (j <= num)
					{
						num = this.ComputeLongestFuncIndex(list);
					}
				}
				else if (text3[i] != c)
				{
					list.RemoveAt(j);
					if (j <= num)
					{
						num = this.ComputeLongestFuncIndex(list);
					}
				}
				else
				{
					j++;
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
		}
		if (list.Count > 1)
		{
			foreach (string text4 in list)
			{
				if (inputCommand == text4)
				{
					return text4;
				}
			}
			return null;
		}
		string text5 = list[0];
		if (i < text5.Length)
		{
			return null;
		}
		return text5;
	}

	// Token: 0x060086DC RID: 34524 RVA: 0x002B8B0C File Offset: 0x002B6D0C
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

	// Token: 0x060086DD RID: 34525 RVA: 0x002B8B4C File Offset: 0x002B6D4C
	private string GetOriginalFunc(string func)
	{
		string result;
		if (!this.m_cheatAlias.TryGetValue(func, out result))
		{
			result = func;
		}
		return result;
	}

	// Token: 0x04007201 RID: 29185
	private Map<string, List<Delegate>> m_funcMap = new Map<string, List<Delegate>>();

	// Token: 0x04007202 RID: 29186
	private Map<string, string> m_cheatAlias = new Map<string, string>();

	// Token: 0x04007203 RID: 29187
	private Map<string, string> m_cheatDesc = new Map<string, string>();

	// Token: 0x04007204 RID: 29188
	private Map<string, string> m_cheatArgs = new Map<string, string>();

	// Token: 0x04007205 RID: 29189
	private Map<string, string> m_cheatExamples = new Map<string, string>();

	// Token: 0x04007206 RID: 29190
	private Map<string, int> m_cheatCategoryIndex = new Map<string, int>();

	// Token: 0x04007207 RID: 29191
	private List<string> m_categoryList = new List<string>();

	// Token: 0x04007208 RID: 29192
	private Rect m_cheatInputBackground;

	// Token: 0x04007209 RID: 29193
	private bool m_inputActive;

	// Token: 0x0400720A RID: 29194
	private int m_lastRegisteredCategoryIndex = -1;

	// Token: 0x0400720B RID: 29195
	private List<string> m_cheatHistory;

	// Token: 0x0400720C RID: 29196
	private int m_cheatHistoryIndex = -1;

	// Token: 0x0400720D RID: 29197
	private string m_cheatTextBeforeScrollingThruHistory;

	// Token: 0x0400720E RID: 29198
	private string m_cheatTextBeforeAutofill;

	// Token: 0x0400720F RID: 29199
	private int m_autofillMatchIndex = -1;

	// Token: 0x04007210 RID: 29200
	private string m_lastAutofillParamFunc;

	// Token: 0x04007211 RID: 29201
	private string m_lastAutofillParamPrefix;

	// Token: 0x04007212 RID: 29202
	private string m_lastAutofillParamMatch;

	// Token: 0x04007213 RID: 29203
	private const string DEFAULT_CATEGORY = "other";

	// Token: 0x04007214 RID: 29204
	private const int MAX_HISTORY_LINES = 25;

	// Token: 0x04007215 RID: 29205
	private GameObject m_sceneObject;

	// Token: 0x04007216 RID: 29206
	private static CheatMgr s_instance;

	// Token: 0x02002660 RID: 9824
	// (Invoke) Token: 0x060136F6 RID: 79606
	public delegate bool ProcessCheatCallback(string func, string[] args, string rawArgs);

	// Token: 0x02002661 RID: 9825
	// (Invoke) Token: 0x060136FA RID: 79610
	public delegate bool ProcessCheatAutofillCallback(string func, string[] args, string rawArgs, AutofillData autofillData);
}
