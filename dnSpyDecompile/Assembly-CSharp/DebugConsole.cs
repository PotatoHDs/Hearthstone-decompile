using System;
using System.Collections.Generic;
using System.ComponentModel;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using BobNetProto;
using Hearthstone.Core;

// Token: 0x02000604 RID: 1540
public class DebugConsole : IService
{
	// Token: 0x06005413 RID: 21523 RVA: 0x001B7830 File Offset: 0x001B5A30
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		if (network.ShouldBeConnectedToAurora_NONSTATIC())
		{
			Processor.QueueJob("InitializeDebugConsole", this.Job_InitializeAfterBGSInits(network), null);
		}
		else
		{
			this.InitializeConsole(network);
		}
		yield break;
	}

	// Token: 0x06005414 RID: 21524 RVA: 0x001B7846 File Offset: 0x001B5A46
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network)
		};
	}

	// Token: 0x06005415 RID: 21525 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06005416 RID: 21526 RVA: 0x001B785B File Offset: 0x001B5A5B
	private IEnumerator<IAsyncJobResult> Job_InitializeAfterBGSInits(Network net)
	{
		while (!BattleNet.IsInitialized())
		{
			yield return null;
		}
		this.InitializeConsole(net);
		yield break;
	}

	// Token: 0x06005417 RID: 21527 RVA: 0x001B7871 File Offset: 0x001B5A71
	private void InitializeConsole(Network net)
	{
		this.InitConsoleCallbackMaps();
		net.RegisterNetHandler(DebugConsoleCommand.PacketID.ID, new Network.NetHandler(this.OnCommandReceived), null);
		net.RegisterNetHandler(DebugConsoleResponse.PacketID.ID, new Network.NetHandler(this.OnCommandResponseReceived), null);
	}

	// Token: 0x06005418 RID: 21528 RVA: 0x001B78B0 File Offset: 0x001B5AB0
	private static List<DebugConsole.CommandParamDecl> CreateParamDeclList(params DebugConsole.CommandParamDecl[] paramDecls)
	{
		List<DebugConsole.CommandParamDecl> list = new List<DebugConsole.CommandParamDecl>();
		foreach (DebugConsole.CommandParamDecl item in paramDecls)
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06005419 RID: 21529 RVA: 0x001B78DF File Offset: 0x001B5ADF
	private void InitConsoleCallbackMaps()
	{
		this.InitClientConsoleCallbackMap();
		this.InitServerConsoleCallbackMap();
	}

	// Token: 0x0600541A RID: 21530 RVA: 0x001B78F0 File Offset: 0x001B5AF0
	private void InitServerConsoleCallbackMap()
	{
		if (DebugConsole.s_serverConsoleCallbackMap != null)
		{
			return;
		}
		DebugConsole.s_serverConsoleCallbackMap = new global::Map<string, DebugConsole.ConsoleCallbackInfo>();
		DebugConsole.s_serverConsoleCallbackMap.Add("spawncard", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.STR, "cardGUID"),
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID"),
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.STR, "zoneName"),
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "premium")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("loadcard", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.STR, "cardGUID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("drawcard", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("shuffle", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("cyclehand", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("nuke", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("damage", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID"),
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "damage")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("addmana", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("readymana", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("maxmana", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("nocosts", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(Array.Empty<DebugConsole.CommandParamDecl>())));
		DebugConsole.s_serverConsoleCallbackMap.Add("healhero", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "playerID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("healentity", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("ready", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("exhaust", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("freeze", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("move", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(new DebugConsole.CommandParamDecl[]
		{
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "entityID"),
			new DebugConsole.CommandParamDecl(DebugConsole.CommandParamDecl.ParamType.I32, "zoneID")
		})));
		DebugConsole.s_serverConsoleCallbackMap.Add("tiegame", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(Array.Empty<DebugConsole.CommandParamDecl>())));
		DebugConsole.s_serverConsoleCallbackMap.Add("aiplaylastspawnedcard", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(Array.Empty<DebugConsole.CommandParamDecl>())));
		DebugConsole.s_serverConsoleCallbackMap.Add("forcestallingprevention", new DebugConsole.ConsoleCallbackInfo(true, null, DebugConsole.CreateParamDeclList(Array.Empty<DebugConsole.CommandParamDecl>())));
	}

	// Token: 0x0600541B RID: 21531 RVA: 0x001B7CC5 File Offset: 0x001B5EC5
	private void InitClientConsoleCallbackMap()
	{
		if (DebugConsole.s_clientConsoleCallbackMap != null)
		{
			return;
		}
		DebugConsole.s_clientConsoleCallbackMap = new global::Map<string, DebugConsole.ConsoleCallbackInfo>();
	}

	// Token: 0x0600541C RID: 21532 RVA: 0x001B7CD9 File Offset: 0x001B5ED9
	private void SendDebugConsoleResponse(DebugConsole.DebugConsoleResponseType type, string message)
	{
		Network.Get().SendDebugConsoleResponse((int)type, message);
	}

	// Token: 0x0600541D RID: 21533 RVA: 0x001B7CE8 File Offset: 0x001B5EE8
	private void SendConsoleCmdToServer(string commandName, List<string> commandParams)
	{
		if (!DebugConsole.s_serverConsoleCallbackMap.ContainsKey(commandName))
		{
			return;
		}
		string text = commandName;
		foreach (string str in commandParams)
		{
			text = text + " " + str;
		}
		if (Network.Get().SendDebugConsoleCommand(text))
		{
			return;
		}
		this.SendDebugConsoleResponse(DebugConsole.DebugConsoleResponseType.CONSOLE_OUTPUT, string.Format("Cannot send command '{0}'; not currently connected to a game server.", commandName));
	}

	// Token: 0x0600541E RID: 21534 RVA: 0x001B7D6C File Offset: 0x001B5F6C
	private void OnCommandReceived()
	{
		string[] array = Network.Get().GetDebugConsoleCommand().Split(new char[]
		{
			' '
		});
		if (array.Length == 0)
		{
			global::Log.All.Print("Received empty command from debug console!", Array.Empty<object>());
			return;
		}
		string text = array[0];
		List<string> list = new List<string>();
		for (int i = 1; i < array.Length; i++)
		{
			list.Add(array[i]);
		}
		if (DebugConsole.s_serverConsoleCallbackMap.ContainsKey(text))
		{
			this.SendConsoleCmdToServer(text, list);
			return;
		}
		if (!DebugConsole.s_clientConsoleCallbackMap.ContainsKey(text))
		{
			this.SendDebugConsoleResponse(DebugConsole.DebugConsoleResponseType.CONSOLE_OUTPUT, string.Format("Unknown command '{0}'.", text));
			return;
		}
		DebugConsole.ConsoleCallbackInfo consoleCallbackInfo = DebugConsole.s_clientConsoleCallbackMap[text];
		if (consoleCallbackInfo.GetNumParams() != list.Count)
		{
			this.SendDebugConsoleResponse(DebugConsole.DebugConsoleResponseType.CONSOLE_OUTPUT, string.Format("Invalid params for command '{0}'.", text));
			return;
		}
		global::Log.All.Print(string.Format("Processing command '{0}' from debug console.", text), Array.Empty<object>());
		consoleCallbackInfo.Callback(list);
	}

	// Token: 0x0600541F RID: 21535 RVA: 0x001B7E5C File Offset: 0x001B605C
	private void OnCommandResponseReceived()
	{
		Network.DebugConsoleResponse debugConsoleResponse = Network.Get().GetDebugConsoleResponse();
		if (debugConsoleResponse != null)
		{
			this.SendDebugConsoleResponse((DebugConsole.DebugConsoleResponseType)debugConsoleResponse.Type, debugConsoleResponse.Response);
		}
		global::Log.All.Print("DebugConsoleResponse: {0}", new object[]
		{
			string.IsNullOrEmpty(debugConsoleResponse.Response) ? "<empty>" : debugConsoleResponse.Response
		});
		if (!string.IsNullOrEmpty(debugConsoleResponse.Response))
		{
			UIStatus.Get().AddInfo(debugConsoleResponse.Response);
		}
	}

	// Token: 0x06005420 RID: 21536 RVA: 0x001B7ED8 File Offset: 0x001B60D8
	public static DebugConsole Get()
	{
		if (DebugConsole.s_instance == null)
		{
			DebugConsole.s_instance = new DebugConsole();
		}
		return DebugConsole.s_instance;
	}

	// Token: 0x04004A4B RID: 19019
	private static DebugConsole s_instance;

	// Token: 0x04004A4C RID: 19020
	private static global::Map<string, DebugConsole.ConsoleCallbackInfo> s_serverConsoleCallbackMap;

	// Token: 0x04004A4D RID: 19021
	private static global::Map<string, DebugConsole.ConsoleCallbackInfo> s_clientConsoleCallbackMap;

	// Token: 0x02002053 RID: 8275
	private class CommandParamDecl
	{
		// Token: 0x06011CE0 RID: 72928 RVA: 0x004F9A02 File Offset: 0x004F7C02
		public CommandParamDecl(DebugConsole.CommandParamDecl.ParamType type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		// Token: 0x0400DCCC RID: 56524
		public string Name;

		// Token: 0x0400DCCD RID: 56525
		public DebugConsole.CommandParamDecl.ParamType Type;

		// Token: 0x02002985 RID: 10629
		public enum ParamType
		{
			// Token: 0x0400FCF7 RID: 64759
			[Description("string")]
			STR,
			// Token: 0x0400FCF8 RID: 64760
			[Description("int32")]
			I32,
			// Token: 0x0400FCF9 RID: 64761
			[Description("float32")]
			F32,
			// Token: 0x0400FCFA RID: 64762
			[Description("bool")]
			BOOL
		}
	}

	// Token: 0x02002054 RID: 8276
	// (Invoke) Token: 0x06011CE2 RID: 72930
	private delegate void ConsoleCallback(List<string> commandParams);

	// Token: 0x02002055 RID: 8277
	private class ConsoleCallbackInfo
	{
		// Token: 0x06011CE5 RID: 72933 RVA: 0x004F9A18 File Offset: 0x004F7C18
		public ConsoleCallbackInfo(bool displayInCmdList, DebugConsole.ConsoleCallback callback, DebugConsole.CommandParamDecl[] commandParams)
		{
			this.DisplayInCommandList = displayInCmdList;
			this.ParamList = new List<DebugConsole.CommandParamDecl>(commandParams);
			this.Callback = callback;
		}

		// Token: 0x06011CE6 RID: 72934 RVA: 0x004F9A3A File Offset: 0x004F7C3A
		public ConsoleCallbackInfo(bool displayInCmdList, DebugConsole.ConsoleCallback callback, List<DebugConsole.CommandParamDecl> commandParams) : this(displayInCmdList, callback, commandParams.ToArray())
		{
		}

		// Token: 0x06011CE7 RID: 72935 RVA: 0x004F9A4A File Offset: 0x004F7C4A
		public int GetNumParams()
		{
			return this.ParamList.Count;
		}

		// Token: 0x0400DCCE RID: 56526
		public bool DisplayInCommandList;

		// Token: 0x0400DCCF RID: 56527
		public List<DebugConsole.CommandParamDecl> ParamList;

		// Token: 0x0400DCD0 RID: 56528
		public DebugConsole.ConsoleCallback Callback;
	}

	// Token: 0x02002056 RID: 8278
	private enum DebugConsoleResponseType
	{
		// Token: 0x0400DCD2 RID: 56530
		CONSOLE_OUTPUT,
		// Token: 0x0400DCD3 RID: 56531
		LOG_MESSAGE
	}
}
