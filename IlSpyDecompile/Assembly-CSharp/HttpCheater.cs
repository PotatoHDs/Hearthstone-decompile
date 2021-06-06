using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using bgs;
using Hearthstone;
using Hearthstone.Core;
using PegasusUtil;
using UnityEngine;

public class HttpCheater
{
	private const string LOCATE_FAILURE_ERR_MSG = "Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.";

	private const string CONNECT_FAILURE_ERR_MSG = "Failed to initiate cheat request. Cheat server is unreachable.";

	private bool m_isReady;

	private string m_address;

	private int m_port;

	private static HttpCheater s_instance;

	private string m_baseUrl => $"http://{m_address}:{m_port}";

	public static HttpCheater Get()
	{
		if (s_instance == null)
		{
			s_instance = new HttpCheater();
			Network.Get().RegisterNetHandler(LocateCheatServerResponse.PacketID.ID, s_instance.OnLocateCheatServerResponse);
		}
		return s_instance;
	}

	public void OnLocateCheatServerResponse()
	{
		LocateCheatServerResponse locateCheatServerResponse = Network.Get().GetLocateCheatServerResponse();
		Initialize(locateCheatServerResponse.Address, locateCheatServerResponse.Port);
	}

	public void Initialize(string address, int port)
	{
		m_address = address;
		m_port = port;
		m_isReady = true;
	}

	private IEnumerator LocateServerCoroutine(int timeoutMilliseconds)
	{
		if (!m_isReady && !HearthstoneApplication.IsPublic())
		{
			Network.Get().SendLocateCheatServerRequest();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (!m_isReady && stopwatch.ElapsedMilliseconds <= timeoutMilliseconds)
			{
				yield return null;
			}
		}
	}

	private Coroutine LocateServer(int timeoutMilliseconds = 5000)
	{
		return Processor.RunCoroutine(LocateServerCoroutine(timeoutMilliseconds));
	}

	public void RunSetResourceCommand(string[] args)
	{
		if (!CheatResourceParser.TryParse(args, out var resource, out var errMsg))
		{
			UIStatus.Get().AddError(errMsg);
			return;
		}
		TutorialCheatResource tutorialCheatResource = resource as TutorialCheatResource;
		if (tutorialCheatResource != null)
		{
			UpdateTutorial(tutorialCheatResource.Progress);
			return;
		}
		HeroCheatResource heroCheatResource = resource as HeroCheatResource;
		if (heroCheatResource != null)
		{
			UpdateHero(heroCheatResource.ClassName, heroCheatResource.Level, heroCheatResource.Wins, heroCheatResource.Gametype);
			return;
		}
		ArenaCheatResource arenaCheatResource = resource as ArenaCheatResource;
		if (arenaCheatResource != null)
		{
			UpdateArenaRecord(arenaCheatResource.Win, arenaCheatResource.Loss);
		}
	}

	public void RunSkipResourceCommand(string[] args)
	{
		if (!CheatResourceParser.TryParse(args, out var resource, out var errMsg))
		{
			UIStatus.Get().AddError(errMsg);
		}
		else if (resource is TutorialCheatResource)
		{
			UpdateTutorial(null);
		}
	}

	public void RunUnlockResourceCommand(string[] args)
	{
		if (!CheatResourceParser.TryParse(args, out var resource, out var errMsg))
		{
			UIStatus.Get().AddError(errMsg);
			return;
		}
		HeroCheatResource heroCheatResource = resource as HeroCheatResource;
		if (heroCheatResource != null)
		{
			UnlockHero(heroCheatResource.ClassName, heroCheatResource.Premium);
		}
	}

	public void RunAddResourceCommand(string[] args)
	{
		if (!CheatResourceParser.TryParse(args, out var resource, out var errMsg))
		{
			UIStatus.Get().AddError(errMsg);
			return;
		}
		GoldCheatResource goldCheatResource = resource as GoldCheatResource;
		if (goldCheatResource != null)
		{
			UpdateGold(goldCheatResource.Amount);
			return;
		}
		DustCheatResource dustCheatResource = resource as DustCheatResource;
		if (dustCheatResource != null)
		{
			UpdateDust(dustCheatResource.Amount);
			return;
		}
		if (resource is FullCardCollectionCheatResource)
		{
			GrantCardCollection();
			return;
		}
		ArenaTicketCheatResource arenaTicketCheatResource = resource as ArenaTicketCheatResource;
		if (arenaTicketCheatResource != null)
		{
			GrantArenaTicket(arenaTicketCheatResource.TicketCount);
			return;
		}
		PackCheatResource packCheatResource = resource as PackCheatResource;
		if (packCheatResource != null)
		{
			GrantBoosterPack(packCheatResource.PackCount, packCheatResource.TypeID);
		}
	}

	public void RunRemoveResourceCommand(string[] args)
	{
		if (!CheatResourceParser.TryParse(args, out var resource, out var errMsg))
		{
			UIStatus.Get().AddError(errMsg);
			return;
		}
		GoldCheatResource goldCheatResource = resource as GoldCheatResource;
		if (goldCheatResource != null)
		{
			if (goldCheatResource.Amount.HasValue)
			{
				UpdateGold(-goldCheatResource.Amount);
			}
			else
			{
				RemoveAllGold();
			}
			return;
		}
		DustCheatResource dustCheatResource = resource as DustCheatResource;
		if (dustCheatResource != null)
		{
			if (dustCheatResource.Amount.HasValue)
			{
				UpdateDust(-dustCheatResource.Amount);
			}
			else
			{
				RemoveAllDust();
			}
			return;
		}
		HeroCheatResource heroCheatResource = resource as HeroCheatResource;
		if (heroCheatResource != null)
		{
			RemoveHero(heroCheatResource.ClassName);
			return;
		}
		if (resource is FullCardCollectionCheatResource)
		{
			RemoveCardCollection();
			return;
		}
		ArenaTicketCheatResource arenaTicketCheatResource = resource as ArenaTicketCheatResource;
		if (arenaTicketCheatResource != null)
		{
			RemoveArenaTicket(arenaTicketCheatResource.TicketCount);
			return;
		}
		PackCheatResource packCheatResource = resource as PackCheatResource;
		if (packCheatResource != null)
		{
			RemoveBoosterPack(packCheatResource.PackCount, packCheatResource.TypeID);
		}
		else if (resource is AllAdventureOwnershipCheatResource)
		{
			Processor.RunCoroutine(RemoveResourceCoroutine("adventureownership"));
		}
	}

	public void RunGrantWingCommand(int wingID)
	{
		GrantWing(wingID);
	}

	public Coroutine GrantCardCollection()
	{
		return Processor.RunCoroutine(GrantCardCollectionCoroutine());
	}

	public Coroutine RemoveCardCollection()
	{
		return Processor.RunCoroutine(RemoveCardCollectionCoroutine());
	}

	public Coroutine UpdateGold(int? deltaAmount)
	{
		return Processor.RunCoroutine(UpdateGoldCoroutine(deltaAmount));
	}

	public Coroutine RemoveAllGold()
	{
		return Processor.RunCoroutine(RemoveAllGoldCoroutine());
	}

	public Coroutine UpdateDust(int? deltaAmount)
	{
		return Processor.RunCoroutine(UpdateDustCoroutine(deltaAmount));
	}

	public Coroutine RemoveAllDust()
	{
		return Processor.RunCoroutine(RemoveAllDustCoroutine());
	}

	public Coroutine UpdateTutorial(int? progressValue)
	{
		return Processor.RunCoroutine(UpdateTutorialCoroutine(progressValue));
	}

	public Coroutine UpdateHero(string className, int? heroLevel, int? wins, string gameType)
	{
		return Processor.RunCoroutine(UpdateHeroCoroutine(className, heroLevel, wins, gameType));
	}

	public Coroutine UnlockHero(string className, TAG_PREMIUM? premium)
	{
		return Processor.RunCoroutine(UnlockHeroCoroutine(className, premium));
	}

	public Coroutine RemoveHero(string className)
	{
		return Processor.RunCoroutine(RemoveHeroCoroutine(className));
	}

	public Coroutine GrantArenaTicket(int? ticketCount)
	{
		return Processor.RunCoroutine(GrantArenaTicketCoroutine(ticketCount));
	}

	public Coroutine RemoveArenaTicket(int? ticketCount)
	{
		return Processor.RunCoroutine(RemoveArenaTicketCoroutine(ticketCount));
	}

	public Coroutine UpdateArenaRecord(int? wins, int? losses)
	{
		return Processor.RunCoroutine(UpdateArenaRecordCoroutine(wins, losses));
	}

	public Coroutine GrantBoosterPack(int? packCount, int? typeID)
	{
		return Processor.RunCoroutine(GrantBoosterPackCoroutine(packCount, typeID));
	}

	public Coroutine RemoveBoosterPack(int? packCount, int? typeID)
	{
		return Processor.RunCoroutine(RemoveBoosterPackCoroutine(packCount, typeID));
	}

	public Coroutine GrantWing(int? wingID)
	{
		return Processor.RunCoroutine(GrantWingCoroutine(wingID));
	}

	private IEnumerator GrantCardCollectionCoroutine()
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = $"{m_baseUrl}/cheat/cards?accountId={lo}";
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(url);
	}

	private IEnumerator RemoveCardCollectionCoroutine()
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = $"{m_baseUrl}/cheat/cards?accountId={lo}";
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
	}

	private IEnumerator UpdateGoldCoroutine(int? deltaAmount)
	{
		if (deltaAmount == 0)
		{
			yield break;
		}
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/gold?accountId={1}", m_baseUrl, lo);
		if (deltaAmount.HasValue)
		{
			stringBuilder.AppendFormat("&amount={0}", deltaAmount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	public IEnumerator RemoveAllGoldCoroutine()
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = $"{m_baseUrl}/cheat/gold?accountId={lo}";
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
	}

	private IEnumerator UpdateDustCoroutine(int? deltaAmount)
	{
		if (deltaAmount == 0)
		{
			yield break;
		}
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/dust?accountId={1}", m_baseUrl, lo);
		if (deltaAmount.HasValue)
		{
			stringBuilder.AppendFormat("&amount={0}", deltaAmount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	public IEnumerator RemoveAllDustCoroutine()
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = $"{m_baseUrl}/cheat/dust?accountId={lo}";
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
	}

	private IEnumerator UpdateTutorialCoroutine(int? progress)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/tutorial?accountId={1}", m_baseUrl, lo);
		if (progress.HasValue)
		{
			stringBuilder.AppendFormat("&progress={0}", progress);
		}
		CheatRequest request = new CheatRequest();
		yield return request.SendGetRequest(stringBuilder.ToString());
		if (request.IsSuccessful)
		{
			HearthstoneApplication.Get().Reset();
		}
	}

	private IEnumerator UpdateHeroCoroutine(string className, int? heroLevel, int? wins, string gameType)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		if (heroLevel.HasValue)
		{
			stringBuilder.AppendFormat("&level={0}", heroLevel);
		}
		if (wins.HasValue)
		{
			stringBuilder.AppendFormat("&wins={0}", wins);
		}
		if (!string.IsNullOrEmpty(gameType))
		{
			stringBuilder.AppendFormat("&gametype={0}", gameType);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	private IEnumerator UnlockHeroCoroutine(string className, TAG_PREMIUM? premium)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		if (premium == TAG_PREMIUM.GOLDEN)
		{
			stringBuilder.AppendFormat("&wins=500");
		}
		int maxHeroLevel = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().MaxHeroLevel;
		stringBuilder.AppendFormat("&level={0}", maxHeroLevel);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	private IEnumerator RemoveHeroCoroutine(string className)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
	}

	private IEnumerator GrantArenaTicketCoroutine(int? ticketCount)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arenaticket?accountId={1}", m_baseUrl, lo);
		if (ticketCount.HasValue)
		{
			stringBuilder.AppendFormat("&ticketCount={0}", ticketCount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	private IEnumerator RemoveArenaTicketCoroutine(int? ticketCount)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arenaticket?accountId={1}", m_baseUrl, lo);
		if (ticketCount.HasValue)
		{
			stringBuilder.AppendFormat("&ticketCount={0}", ticketCount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
	}

	private IEnumerator UpdateArenaRecordCoroutine(int? wins, int? losses)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arena?accountId={1}", m_baseUrl, lo);
		if (wins.HasValue)
		{
			stringBuilder.AppendFormat("&win={0}", wins);
		}
		if (losses.HasValue)
		{
			stringBuilder.AppendFormat("&loss={0}", losses);
		}
		CheatRequest request = new CheatRequest();
		yield return request.SendGetRequest(stringBuilder.ToString());
		if (request.IsSuccessful && (bool)Object.FindObjectOfType<ArenaTrayDisplay>())
		{
			yield return new WaitForSeconds(1f);
			ArenaTrayDisplay.Get().UpdateTray();
		}
	}

	private IEnumerator GrantBoosterPackCoroutine(int? packCount, int? typeID)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/pack?accountId={1}", m_baseUrl, lo);
		if (packCount.HasValue)
		{
			stringBuilder.AppendFormat("&count={0}", packCount);
		}
		if (typeID.HasValue)
		{
			stringBuilder.AppendFormat("&typeID={0}", typeID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	private IEnumerator RemoveBoosterPackCoroutine(int? packCount, int? typeID)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/pack?accountId={1}", m_baseUrl, lo);
		if (packCount.HasValue)
		{
			stringBuilder.AppendFormat("&count={0}", packCount);
		}
		if (typeID.HasValue)
		{
			stringBuilder.AppendFormat("&typeID={0}", typeID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
	}

	private IEnumerator GrantWingCoroutine(int? wingID)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/adventureownership?accountId={1}", m_baseUrl, lo);
		if (wingID.HasValue)
		{
			stringBuilder.AppendFormat("&wingid={0}", wingID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
	}

	private IEnumerator RemoveResourceCoroutine(string resourceName, params KeyValuePair<string, string>[] paramValuePairs)
	{
		yield return LocateServer();
		if (!m_isReady)
		{
			LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/{1}?accountId={2}", m_baseUrl, resourceName, lo);
		StringBuilder stringBuilder2 = new StringBuilder();
		for (int i = 0; i < paramValuePairs.Length; i++)
		{
			KeyValuePair<string, string> keyValuePair = paramValuePairs[i];
			stringBuilder2.AppendFormat("&{0}={1}", keyValuePair.Key, keyValuePair.Value);
		}
		stringBuilder.Append(stringBuilder2.ToString());
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
	}

	public static void LogError(string message)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			UIStatus.Get().AddError(message);
			UnityEngine.Debug.LogError(message);
		}
	}

	public static void LogError(HttpStatusCode statusCode, string message)
	{
		LogError($"{message} (status code: {(int)statusCode})");
	}
}
