using System;
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

// Token: 0x020008E0 RID: 2272
public class HttpCheater
{
	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x06007DD0 RID: 32208 RVA: 0x0028CEAD File Offset: 0x0028B0AD
	private string m_baseUrl
	{
		get
		{
			return string.Format("http://{0}:{1}", this.m_address, this.m_port);
		}
	}

	// Token: 0x06007DD1 RID: 32209 RVA: 0x0028CECA File Offset: 0x0028B0CA
	public static HttpCheater Get()
	{
		if (HttpCheater.s_instance == null)
		{
			HttpCheater.s_instance = new HttpCheater();
			Network.Get().RegisterNetHandler(LocateCheatServerResponse.PacketID.ID, new Network.NetHandler(HttpCheater.s_instance.OnLocateCheatServerResponse), null);
		}
		return HttpCheater.s_instance;
	}

	// Token: 0x06007DD2 RID: 32210 RVA: 0x0028CF08 File Offset: 0x0028B108
	public void OnLocateCheatServerResponse()
	{
		LocateCheatServerResponse locateCheatServerResponse = Network.Get().GetLocateCheatServerResponse();
		this.Initialize(locateCheatServerResponse.Address, locateCheatServerResponse.Port);
	}

	// Token: 0x06007DD3 RID: 32211 RVA: 0x0028CF32 File Offset: 0x0028B132
	public void Initialize(string address, int port)
	{
		this.m_address = address;
		this.m_port = port;
		this.m_isReady = true;
	}

	// Token: 0x06007DD4 RID: 32212 RVA: 0x0028CF49 File Offset: 0x0028B149
	private IEnumerator LocateServerCoroutine(int timeoutMilliseconds)
	{
		if (this.m_isReady)
		{
			yield break;
		}
		if (HearthstoneApplication.IsPublic())
		{
			yield break;
		}
		Network.Get().SendLocateCheatServerRequest();
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		while (!this.m_isReady)
		{
			if (stopwatch.ElapsedMilliseconds > (long)timeoutMilliseconds)
			{
				yield break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06007DD5 RID: 32213 RVA: 0x0028CF5F File Offset: 0x0028B15F
	private Coroutine LocateServer(int timeoutMilliseconds = 5000)
	{
		return Processor.RunCoroutine(this.LocateServerCoroutine(timeoutMilliseconds), null);
	}

	// Token: 0x06007DD6 RID: 32214 RVA: 0x0028CF70 File Offset: 0x0028B170
	public void RunSetResourceCommand(string[] args)
	{
		CheatResource cheatResource;
		string message;
		if (!CheatResourceParser.TryParse(args, out cheatResource, out message))
		{
			UIStatus.Get().AddError(message, -1f);
			return;
		}
		TutorialCheatResource tutorialCheatResource = cheatResource as TutorialCheatResource;
		if (tutorialCheatResource != null)
		{
			this.UpdateTutorial(tutorialCheatResource.Progress);
			return;
		}
		HeroCheatResource heroCheatResource = cheatResource as HeroCheatResource;
		if (heroCheatResource != null)
		{
			this.UpdateHero(heroCheatResource.ClassName, heroCheatResource.Level, heroCheatResource.Wins, heroCheatResource.Gametype);
			return;
		}
		ArenaCheatResource arenaCheatResource = cheatResource as ArenaCheatResource;
		if (arenaCheatResource != null)
		{
			this.UpdateArenaRecord(arenaCheatResource.Win, arenaCheatResource.Loss);
			return;
		}
	}

	// Token: 0x06007DD7 RID: 32215 RVA: 0x0028D000 File Offset: 0x0028B200
	public void RunSkipResourceCommand(string[] args)
	{
		CheatResource cheatResource;
		string message;
		if (!CheatResourceParser.TryParse(args, out cheatResource, out message))
		{
			UIStatus.Get().AddError(message, -1f);
			return;
		}
		if (cheatResource is TutorialCheatResource)
		{
			this.UpdateTutorial(null);
			return;
		}
	}

	// Token: 0x06007DD8 RID: 32216 RVA: 0x0028D044 File Offset: 0x0028B244
	public void RunUnlockResourceCommand(string[] args)
	{
		CheatResource cheatResource;
		string message;
		if (!CheatResourceParser.TryParse(args, out cheatResource, out message))
		{
			UIStatus.Get().AddError(message, -1f);
			return;
		}
		HeroCheatResource heroCheatResource = cheatResource as HeroCheatResource;
		if (heroCheatResource != null)
		{
			this.UnlockHero(heroCheatResource.ClassName, heroCheatResource.Premium);
			return;
		}
	}

	// Token: 0x06007DD9 RID: 32217 RVA: 0x0028D08C File Offset: 0x0028B28C
	public void RunAddResourceCommand(string[] args)
	{
		CheatResource cheatResource;
		string message;
		if (!CheatResourceParser.TryParse(args, out cheatResource, out message))
		{
			UIStatus.Get().AddError(message, -1f);
			return;
		}
		GoldCheatResource goldCheatResource = cheatResource as GoldCheatResource;
		if (goldCheatResource != null)
		{
			this.UpdateGold(goldCheatResource.Amount);
			return;
		}
		DustCheatResource dustCheatResource = cheatResource as DustCheatResource;
		if (dustCheatResource != null)
		{
			this.UpdateDust(dustCheatResource.Amount);
			return;
		}
		if (cheatResource is FullCardCollectionCheatResource)
		{
			this.GrantCardCollection();
			return;
		}
		ArenaTicketCheatResource arenaTicketCheatResource = cheatResource as ArenaTicketCheatResource;
		if (arenaTicketCheatResource != null)
		{
			this.GrantArenaTicket(arenaTicketCheatResource.TicketCount);
			return;
		}
		PackCheatResource packCheatResource = cheatResource as PackCheatResource;
		if (packCheatResource != null)
		{
			this.GrantBoosterPack(packCheatResource.PackCount, packCheatResource.TypeID);
			return;
		}
	}

	// Token: 0x06007DDA RID: 32218 RVA: 0x0028D134 File Offset: 0x0028B334
	public void RunRemoveResourceCommand(string[] args)
	{
		CheatResource cheatResource;
		string message;
		if (!CheatResourceParser.TryParse(args, out cheatResource, out message))
		{
			UIStatus.Get().AddError(message, -1f);
			return;
		}
		GoldCheatResource goldCheatResource = cheatResource as GoldCheatResource;
		if (goldCheatResource != null)
		{
			if (goldCheatResource.Amount != null)
			{
				this.UpdateGold(-goldCheatResource.Amount);
				return;
			}
			this.RemoveAllGold();
			return;
		}
		else
		{
			DustCheatResource dustCheatResource = cheatResource as DustCheatResource;
			if (dustCheatResource != null)
			{
				if (dustCheatResource.Amount != null)
				{
					this.UpdateDust(-dustCheatResource.Amount);
					return;
				}
				this.RemoveAllDust();
				return;
			}
			else
			{
				HeroCheatResource heroCheatResource = cheatResource as HeroCheatResource;
				if (heroCheatResource != null)
				{
					this.RemoveHero(heroCheatResource.ClassName);
					return;
				}
				if (cheatResource is FullCardCollectionCheatResource)
				{
					this.RemoveCardCollection();
					return;
				}
				ArenaTicketCheatResource arenaTicketCheatResource = cheatResource as ArenaTicketCheatResource;
				if (arenaTicketCheatResource != null)
				{
					this.RemoveArenaTicket(arenaTicketCheatResource.TicketCount);
					return;
				}
				PackCheatResource packCheatResource = cheatResource as PackCheatResource;
				if (packCheatResource != null)
				{
					this.RemoveBoosterPack(packCheatResource.PackCount, packCheatResource.TypeID);
					return;
				}
				if (cheatResource is AllAdventureOwnershipCheatResource)
				{
					Processor.RunCoroutine(this.RemoveResourceCoroutine("adventureownership", Array.Empty<KeyValuePair<string, string>>()), null);
					return;
				}
				return;
			}
		}
	}

	// Token: 0x06007DDB RID: 32219 RVA: 0x0028D288 File Offset: 0x0028B488
	public void RunGrantWingCommand(int wingID)
	{
		this.GrantWing(new int?(wingID));
	}

	// Token: 0x06007DDC RID: 32220 RVA: 0x0028D297 File Offset: 0x0028B497
	public Coroutine GrantCardCollection()
	{
		return Processor.RunCoroutine(this.GrantCardCollectionCoroutine(), null);
	}

	// Token: 0x06007DDD RID: 32221 RVA: 0x0028D2A5 File Offset: 0x0028B4A5
	public Coroutine RemoveCardCollection()
	{
		return Processor.RunCoroutine(this.RemoveCardCollectionCoroutine(), null);
	}

	// Token: 0x06007DDE RID: 32222 RVA: 0x0028D2B3 File Offset: 0x0028B4B3
	public Coroutine UpdateGold(int? deltaAmount)
	{
		return Processor.RunCoroutine(this.UpdateGoldCoroutine(deltaAmount), null);
	}

	// Token: 0x06007DDF RID: 32223 RVA: 0x0028D2C2 File Offset: 0x0028B4C2
	public Coroutine RemoveAllGold()
	{
		return Processor.RunCoroutine(this.RemoveAllGoldCoroutine(), null);
	}

	// Token: 0x06007DE0 RID: 32224 RVA: 0x0028D2D0 File Offset: 0x0028B4D0
	public Coroutine UpdateDust(int? deltaAmount)
	{
		return Processor.RunCoroutine(this.UpdateDustCoroutine(deltaAmount), null);
	}

	// Token: 0x06007DE1 RID: 32225 RVA: 0x0028D2DF File Offset: 0x0028B4DF
	public Coroutine RemoveAllDust()
	{
		return Processor.RunCoroutine(this.RemoveAllDustCoroutine(), null);
	}

	// Token: 0x06007DE2 RID: 32226 RVA: 0x0028D2ED File Offset: 0x0028B4ED
	public Coroutine UpdateTutorial(int? progressValue)
	{
		return Processor.RunCoroutine(this.UpdateTutorialCoroutine(progressValue), null);
	}

	// Token: 0x06007DE3 RID: 32227 RVA: 0x0028D2FC File Offset: 0x0028B4FC
	public Coroutine UpdateHero(string className, int? heroLevel, int? wins, string gameType)
	{
		return Processor.RunCoroutine(this.UpdateHeroCoroutine(className, heroLevel, wins, gameType), null);
	}

	// Token: 0x06007DE4 RID: 32228 RVA: 0x0028D30F File Offset: 0x0028B50F
	public Coroutine UnlockHero(string className, TAG_PREMIUM? premium)
	{
		return Processor.RunCoroutine(this.UnlockHeroCoroutine(className, premium), null);
	}

	// Token: 0x06007DE5 RID: 32229 RVA: 0x0028D31F File Offset: 0x0028B51F
	public Coroutine RemoveHero(string className)
	{
		return Processor.RunCoroutine(this.RemoveHeroCoroutine(className), null);
	}

	// Token: 0x06007DE6 RID: 32230 RVA: 0x0028D32E File Offset: 0x0028B52E
	public Coroutine GrantArenaTicket(int? ticketCount)
	{
		return Processor.RunCoroutine(this.GrantArenaTicketCoroutine(ticketCount), null);
	}

	// Token: 0x06007DE7 RID: 32231 RVA: 0x0028D33D File Offset: 0x0028B53D
	public Coroutine RemoveArenaTicket(int? ticketCount)
	{
		return Processor.RunCoroutine(this.RemoveArenaTicketCoroutine(ticketCount), null);
	}

	// Token: 0x06007DE8 RID: 32232 RVA: 0x0028D34C File Offset: 0x0028B54C
	public Coroutine UpdateArenaRecord(int? wins, int? losses)
	{
		return Processor.RunCoroutine(this.UpdateArenaRecordCoroutine(wins, losses), null);
	}

	// Token: 0x06007DE9 RID: 32233 RVA: 0x0028D35C File Offset: 0x0028B55C
	public Coroutine GrantBoosterPack(int? packCount, int? typeID)
	{
		return Processor.RunCoroutine(this.GrantBoosterPackCoroutine(packCount, typeID), null);
	}

	// Token: 0x06007DEA RID: 32234 RVA: 0x0028D36C File Offset: 0x0028B56C
	public Coroutine RemoveBoosterPack(int? packCount, int? typeID)
	{
		return Processor.RunCoroutine(this.RemoveBoosterPackCoroutine(packCount, typeID), null);
	}

	// Token: 0x06007DEB RID: 32235 RVA: 0x0028D37C File Offset: 0x0028B57C
	public Coroutine GrantWing(int? wingID)
	{
		return Processor.RunCoroutine(this.GrantWingCoroutine(wingID), null);
	}

	// Token: 0x06007DEC RID: 32236 RVA: 0x0028D38B File Offset: 0x0028B58B
	private IEnumerator GrantCardCollectionCoroutine()
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = string.Format("{0}/cheat/cards?accountId={1}", this.m_baseUrl, lo);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(url);
		yield break;
	}

	// Token: 0x06007DED RID: 32237 RVA: 0x0028D39A File Offset: 0x0028B59A
	private IEnumerator RemoveCardCollectionCoroutine()
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = string.Format("{0}/cheat/cards?accountId={1}", this.m_baseUrl, lo);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
		yield break;
	}

	// Token: 0x06007DEE RID: 32238 RVA: 0x0028D3A9 File Offset: 0x0028B5A9
	private IEnumerator UpdateGoldCoroutine(int? deltaAmount)
	{
		int? num = deltaAmount;
		int num2 = 0;
		if (num.GetValueOrDefault() == num2 & num != null)
		{
			yield break;
		}
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/gold?accountId={1}", this.m_baseUrl, lo);
		if (deltaAmount != null)
		{
			stringBuilder.AppendFormat("&amount={0}", deltaAmount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DEF RID: 32239 RVA: 0x0028D3BF File Offset: 0x0028B5BF
	public IEnumerator RemoveAllGoldCoroutine()
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = string.Format("{0}/cheat/gold?accountId={1}", this.m_baseUrl, lo);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
		yield break;
	}

	// Token: 0x06007DF0 RID: 32240 RVA: 0x0028D3CE File Offset: 0x0028B5CE
	private IEnumerator UpdateDustCoroutine(int? deltaAmount)
	{
		int? num = deltaAmount;
		int num2 = 0;
		if (num.GetValueOrDefault() == num2 & num != null)
		{
			yield break;
		}
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/dust?accountId={1}", this.m_baseUrl, lo);
		if (deltaAmount != null)
		{
			stringBuilder.AppendFormat("&amount={0}", deltaAmount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF1 RID: 32241 RVA: 0x0028D3E4 File Offset: 0x0028B5E4
	public IEnumerator RemoveAllDustCoroutine()
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		string url = string.Format("{0}/cheat/dust?accountId={1}", this.m_baseUrl, lo);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(url);
		yield break;
	}

	// Token: 0x06007DF2 RID: 32242 RVA: 0x0028D3F3 File Offset: 0x0028B5F3
	private IEnumerator UpdateTutorialCoroutine(int? progress)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/tutorial?accountId={1}", this.m_baseUrl, lo);
		if (progress != null)
		{
			stringBuilder.AppendFormat("&progress={0}", progress);
		}
		CheatRequest request = new CheatRequest();
		yield return request.SendGetRequest(stringBuilder.ToString());
		if (request.IsSuccessful)
		{
			HearthstoneApplication.Get().Reset();
		}
		yield break;
	}

	// Token: 0x06007DF3 RID: 32243 RVA: 0x0028D409 File Offset: 0x0028B609
	private IEnumerator UpdateHeroCoroutine(string className, int? heroLevel, int? wins, string gameType)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", this.m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		if (heroLevel != null)
		{
			stringBuilder.AppendFormat("&level={0}", heroLevel);
		}
		if (wins != null)
		{
			stringBuilder.AppendFormat("&wins={0}", wins);
		}
		if (!string.IsNullOrEmpty(gameType))
		{
			stringBuilder.AppendFormat("&gametype={0}", gameType);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF4 RID: 32244 RVA: 0x0028D435 File Offset: 0x0028B635
	private IEnumerator UnlockHeroCoroutine(string className, TAG_PREMIUM? premium)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", this.m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		TAG_PREMIUM? tag_PREMIUM = premium;
		TAG_PREMIUM tag_PREMIUM2 = TAG_PREMIUM.GOLDEN;
		if (tag_PREMIUM.GetValueOrDefault() == tag_PREMIUM2 & tag_PREMIUM != null)
		{
			stringBuilder.AppendFormat("&wins=500", Array.Empty<object>());
		}
		int maxHeroLevel = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().MaxHeroLevel;
		stringBuilder.AppendFormat("&level={0}", maxHeroLevel);
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF5 RID: 32245 RVA: 0x0028D452 File Offset: 0x0028B652
	private IEnumerator RemoveHeroCoroutine(string className)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/hero?accountId={1}", this.m_baseUrl, lo);
		if (!string.IsNullOrEmpty(className))
		{
			stringBuilder.AppendFormat("&class={0}", className);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF6 RID: 32246 RVA: 0x0028D468 File Offset: 0x0028B668
	private IEnumerator GrantArenaTicketCoroutine(int? ticketCount)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arenaticket?accountId={1}", this.m_baseUrl, lo);
		if (ticketCount != null)
		{
			stringBuilder.AppendFormat("&ticketCount={0}", ticketCount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF7 RID: 32247 RVA: 0x0028D47E File Offset: 0x0028B67E
	private IEnumerator RemoveArenaTicketCoroutine(int? ticketCount)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arenaticket?accountId={1}", this.m_baseUrl, lo);
		if (ticketCount != null)
		{
			stringBuilder.AppendFormat("&ticketCount={0}", ticketCount);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DF8 RID: 32248 RVA: 0x0028D494 File Offset: 0x0028B694
	private IEnumerator UpdateArenaRecordCoroutine(int? wins, int? losses)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/arena?accountId={1}", this.m_baseUrl, lo);
		if (wins != null)
		{
			stringBuilder.AppendFormat("&win={0}", wins);
		}
		if (losses != null)
		{
			stringBuilder.AppendFormat("&loss={0}", losses);
		}
		CheatRequest request = new CheatRequest();
		yield return request.SendGetRequest(stringBuilder.ToString());
		if (request.IsSuccessful && UnityEngine.Object.FindObjectOfType<ArenaTrayDisplay>())
		{
			yield return new WaitForSeconds(1f);
			ArenaTrayDisplay.Get().UpdateTray();
		}
		yield break;
	}

	// Token: 0x06007DF9 RID: 32249 RVA: 0x0028D4B1 File Offset: 0x0028B6B1
	private IEnumerator GrantBoosterPackCoroutine(int? packCount, int? typeID)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/pack?accountId={1}", this.m_baseUrl, lo);
		if (packCount != null)
		{
			stringBuilder.AppendFormat("&count={0}", packCount);
		}
		if (typeID != null)
		{
			stringBuilder.AppendFormat("&typeID={0}", typeID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DFA RID: 32250 RVA: 0x0028D4CE File Offset: 0x0028B6CE
	private IEnumerator RemoveBoosterPackCoroutine(int? packCount, int? typeID)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/pack?accountId={1}", this.m_baseUrl, lo);
		if (packCount != null)
		{
			stringBuilder.AppendFormat("&count={0}", packCount);
		}
		if (typeID != null)
		{
			stringBuilder.AppendFormat("&typeID={0}", typeID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DFB RID: 32251 RVA: 0x0028D4EB File Offset: 0x0028B6EB
	private IEnumerator GrantWingCoroutine(int? wingID)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/adventureownership?accountId={1}", this.m_baseUrl, lo);
		if (wingID != null)
		{
			stringBuilder.AppendFormat("&wingid={0}", wingID);
		}
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendGetRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DFC RID: 32252 RVA: 0x0028D501 File Offset: 0x0028B701
	private IEnumerator RemoveResourceCoroutine(string resourceName, params KeyValuePair<string, string>[] paramValuePairs)
	{
		yield return this.LocateServer(5000);
		if (!this.m_isReady)
		{
			HttpCheater.LogError("Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.");
			yield break;
		}
		ulong lo = BattleNet.GetMyGameAccountId().lo;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}/cheat/{1}?accountId={2}", this.m_baseUrl, resourceName, lo);
		StringBuilder stringBuilder2 = new StringBuilder();
		foreach (KeyValuePair<string, string> keyValuePair in paramValuePairs)
		{
			stringBuilder2.AppendFormat("&{0}={1}", keyValuePair.Key, keyValuePair.Value);
		}
		stringBuilder.Append(stringBuilder2.ToString());
		CheatRequest cheatRequest = new CheatRequest();
		yield return cheatRequest.SendDeleteRequest(stringBuilder.ToString());
		yield break;
	}

	// Token: 0x06007DFD RID: 32253 RVA: 0x0025E45E File Offset: 0x0025C65E
	public static void LogError(string message)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			UIStatus.Get().AddError(message, -1f);
			UnityEngine.Debug.LogError(message);
		}
	}

	// Token: 0x06007DFE RID: 32254 RVA: 0x0028D51E File Offset: 0x0028B71E
	public static void LogError(HttpStatusCode statusCode, string message)
	{
		HttpCheater.LogError(string.Format("{0} (status code: {1})", message, (int)statusCode));
	}

	// Token: 0x040065EE RID: 26094
	private const string LOCATE_FAILURE_ERR_MSG = "Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.";

	// Token: 0x040065EF RID: 26095
	private const string CONNECT_FAILURE_ERR_MSG = "Failed to initiate cheat request. Cheat server is unreachable.";

	// Token: 0x040065F0 RID: 26096
	private bool m_isReady;

	// Token: 0x040065F1 RID: 26097
	private string m_address;

	// Token: 0x040065F2 RID: 26098
	private int m_port;

	// Token: 0x040065F3 RID: 26099
	private static HttpCheater s_instance;
}
