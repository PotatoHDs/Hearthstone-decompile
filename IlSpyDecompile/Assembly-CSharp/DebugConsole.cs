using System;
using System.Collections.Generic;
using System.ComponentModel;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using BobNetProto;
using Hearthstone.Core;

public class DebugConsole : IService
{
	private class CommandParamDecl
	{
		public enum ParamType
		{
			[Description("string")]
			STR,
			[Description("int32")]
			I32,
			[Description("float32")]
			F32,
			[Description("bool")]
			BOOL
		}

		public string Name;

		public ParamType Type;

		public CommandParamDecl(ParamType type, string name)
		{
			Type = type;
			Name = name;
		}
	}

	private delegate void ConsoleCallback(List<string> commandParams);

	private class ConsoleCallbackInfo
	{
		public bool DisplayInCommandList;

		public List<CommandParamDecl> ParamList;

		public ConsoleCallback Callback;

		public ConsoleCallbackInfo(bool displayInCmdList, ConsoleCallback callback, CommandParamDecl[] commandParams)
		{
			DisplayInCommandList = displayInCmdList;
			ParamList = new List<CommandParamDecl>(commandParams);
			Callback = callback;
		}

		public ConsoleCallbackInfo(bool displayInCmdList, ConsoleCallback callback, List<CommandParamDecl> commandParams)
			: this(displayInCmdList, callback, commandParams.ToArray())
		{
		}

		public int GetNumParams()
		{
			return ParamList.Count;
		}
	}

	private enum DebugConsoleResponseType
	{
		CONSOLE_OUTPUT,
		LOG_MESSAGE
	}

	private static DebugConsole s_instance;

	private static Map<string, ConsoleCallbackInfo> s_serverConsoleCallbackMap;

	private static Map<string, ConsoleCallbackInfo> s_clientConsoleCallbackMap;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		if (network.ShouldBeConnectedToAurora_NONSTATIC())
		{
			Processor.QueueJob("InitializeDebugConsole", Job_InitializeAfterBGSInits(network), (IJobDependency[])null);
		}
		else
		{
			InitializeConsole(network);
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(Network) };
	}

	public void Shutdown()
	{
	}

	private IEnumerator<IAsyncJobResult> Job_InitializeAfterBGSInits(Network net)
	{
		while (!BattleNet.IsInitialized())
		{
			yield return null;
		}
		InitializeConsole(net);
	}

	private void InitializeConsole(Network net)
	{
		InitConsoleCallbackMaps();
		net.RegisterNetHandler(DebugConsoleCommand.PacketID.ID, OnCommandReceived);
		net.RegisterNetHandler(DebugConsoleResponse.PacketID.ID, OnCommandResponseReceived);
	}

	private static List<CommandParamDecl> CreateParamDeclList(params CommandParamDecl[] paramDecls)
	{
		List<CommandParamDecl> list = new List<CommandParamDecl>();
		foreach (CommandParamDecl item in paramDecls)
		{
			list.Add(item);
		}
		return list;
	}

	private void InitConsoleCallbackMaps()
	{
		InitClientConsoleCallbackMap();
		InitServerConsoleCallbackMap();
	}

	private void InitServerConsoleCallbackMap()
	{
		if (s_serverConsoleCallbackMap == null)
		{
			s_serverConsoleCallbackMap = new Map<string, ConsoleCallbackInfo>();
			s_serverConsoleCallbackMap.Add("spawncard", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.STR, "cardGUID"), new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"), new CommandParamDecl(CommandParamDecl.ParamType.STR, "zoneName"), new CommandParamDecl(CommandParamDecl.ParamType.I32, "premium"))));
			s_serverConsoleCallbackMap.Add("loadcard", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.STR, "cardGUID"))));
			s_serverConsoleCallbackMap.Add("drawcard", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("shuffle", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("cyclehand", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("nuke", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("damage", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"), new CommandParamDecl(CommandParamDecl.ParamType.I32, "damage"))));
			s_serverConsoleCallbackMap.Add("addmana", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("readymana", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("maxmana", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("nocosts", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList()));
			s_serverConsoleCallbackMap.Add("healhero", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "playerID"))));
			s_serverConsoleCallbackMap.Add("healentity", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"))));
			s_serverConsoleCallbackMap.Add("ready", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"))));
			s_serverConsoleCallbackMap.Add("exhaust", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"))));
			s_serverConsoleCallbackMap.Add("freeze", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"))));
			s_serverConsoleCallbackMap.Add("move", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList(new CommandParamDecl(CommandParamDecl.ParamType.I32, "entityID"), new CommandParamDecl(CommandParamDecl.ParamType.I32, "zoneID"))));
			s_serverConsoleCallbackMap.Add("tiegame", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList()));
			s_serverConsoleCallbackMap.Add("aiplaylastspawnedcard", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList()));
			s_serverConsoleCallbackMap.Add("forcestallingprevention", new ConsoleCallbackInfo(displayInCmdList: true, null, CreateParamDeclList()));
		}
	}

	private void InitClientConsoleCallbackMap()
	{
		if (s_clientConsoleCallbackMap == null)
		{
			s_clientConsoleCallbackMap = new Map<string, ConsoleCallbackInfo>();
		}
	}

	private void SendDebugConsoleResponse(DebugConsoleResponseType type, string message)
	{
		Network.Get().SendDebugConsoleResponse((int)type, message);
	}

	private void SendConsoleCmdToServer(string commandName, List<string> commandParams)
	{
		if (!s_serverConsoleCallbackMap.ContainsKey(commandName))
		{
			return;
		}
		string text = commandName;
		foreach (string commandParam in commandParams)
		{
			text = text + " " + commandParam;
		}
		if (!Network.Get().SendDebugConsoleCommand(text))
		{
			SendDebugConsoleResponse(DebugConsoleResponseType.CONSOLE_OUTPUT, $"Cannot send command '{commandName}'; not currently connected to a game server.");
		}
	}

	private void OnCommandReceived()
	{
		string[] array = Network.Get().GetDebugConsoleCommand().Split(' ');
		if (array.Length == 0)
		{
			Log.All.Print("Received empty command from debug console!");
			return;
		}
		string text = array[0];
		List<string> list = new List<string>();
		for (int i = 1; i < array.Length; i++)
		{
			list.Add(array[i]);
		}
		if (s_serverConsoleCallbackMap.ContainsKey(text))
		{
			SendConsoleCmdToServer(text, list);
			return;
		}
		if (!s_clientConsoleCallbackMap.ContainsKey(text))
		{
			SendDebugConsoleResponse(DebugConsoleResponseType.CONSOLE_OUTPUT, $"Unknown command '{text}'.");
			return;
		}
		ConsoleCallbackInfo consoleCallbackInfo = s_clientConsoleCallbackMap[text];
		if (consoleCallbackInfo.GetNumParams() != list.Count)
		{
			SendDebugConsoleResponse(DebugConsoleResponseType.CONSOLE_OUTPUT, $"Invalid params for command '{text}'.");
			return;
		}
		Log.All.Print($"Processing command '{text}' from debug console.");
		consoleCallbackInfo.Callback(list);
	}

	private void OnCommandResponseReceived()
	{
		Network.DebugConsoleResponse debugConsoleResponse = Network.Get().GetDebugConsoleResponse();
		if (debugConsoleResponse != null)
		{
			SendDebugConsoleResponse((DebugConsoleResponseType)debugConsoleResponse.Type, debugConsoleResponse.Response);
		}
		Log.All.Print("DebugConsoleResponse: {0}", string.IsNullOrEmpty(debugConsoleResponse.Response) ? "<empty>" : debugConsoleResponse.Response);
		if (!string.IsNullOrEmpty(debugConsoleResponse.Response))
		{
			UIStatus.Get().AddInfo(debugConsoleResponse.Response);
		}
	}

	public static DebugConsole Get()
	{
		if (s_instance == null)
		{
			s_instance = new DebugConsole();
		}
		return s_instance;
	}
}
