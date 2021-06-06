using System;
using PegasusUtil;

namespace Networking
{
	// Token: 0x02000FAF RID: 4015
	internal static class FakeUtilHandler
	{
		// Token: 0x0600AF89 RID: 44937 RVA: 0x003659DC File Offset: 0x00363BDC
		internal static bool FakeUtilOutbound(int type, IProtoBuf body, int subId)
		{
			bool success = true;
			if (type <= 240)
			{
				if (type != 201)
				{
					if (type == 239)
					{
						goto IL_9C;
					}
					if (type != 240)
					{
						goto IL_95;
					}
				}
				FakeUtilHandler.FakeProcessPacket(type, body, subId);
				goto IL_9C;
			}
			if (type <= 284)
			{
				if (type == 276 || type == 284)
				{
					goto IL_9C;
				}
			}
			else
			{
				if (type == 305)
				{
					goto IL_9C;
				}
				if (type == 327)
				{
					((GenericRequestList)body).Requests.ForEach(delegate(GenericRequest request)
					{
						success = (FakeUtilHandler.FakeProcessPacket(request.RequestId, body, request.RequestSubId) & success);
					});
					goto IL_9C;
				}
			}
			IL_95:
			success = false;
			IL_9C:
			return success;
		}

		// Token: 0x0600AF8A RID: 44938 RVA: 0x00365A8C File Offset: 0x00363C8C
		private static bool FakeProcessPacket(int type, IProtoBuf body, int subId)
		{
			bool result = true;
			if (type != 201)
			{
				if (type != 340)
				{
					Log.Net.PrintWarning("FakeUtilOutbound: unable to simulate response for requestId={0} subId={1}", new object[]
					{
						type,
						subId
					});
				}
				else
				{
					Network.Get().FakeHandleType(ClientStaticAssetsResponse.PacketID.ID);
				}
			}
			else
			{
				result = FakeUtilHandler.FakeUtilOutboundGetAccountInfo((GetAccountInfo.Request)subId);
			}
			return result;
		}

		// Token: 0x0600AF8B RID: 44939 RVA: 0x00365AF8 File Offset: 0x00363CF8
		private static bool FakeUtilOutboundGetAccountInfo(GetAccountInfo.Request request)
		{
			Enum @enum = null;
			switch (request)
			{
			case GetAccountInfo.Request.DECK_LIST:
				@enum = DeckList.PacketID.ID;
				break;
			case GetAccountInfo.Request.MEDAL_INFO:
				@enum = MedalInfo.PacketID.ID;
				break;
			case GetAccountInfo.Request.CARD_BACKS:
				@enum = CardBacks.PacketID.ID;
				break;
			case GetAccountInfo.Request.PLAYER_RECORD:
				@enum = PlayerRecords.PacketID.ID;
				break;
			case GetAccountInfo.Request.DECK_LIMIT:
				@enum = ProfileDeckLimit.PacketID.ID;
				break;
			case GetAccountInfo.Request.CAMPAIGN_INFO:
				@enum = ProfileProgress.PacketID.ID;
				break;
			case GetAccountInfo.Request.CARD_VALUES:
				@enum = CardValues.PacketID.ID;
				break;
			case GetAccountInfo.Request.FEATURES:
				@enum = GuardianVars.PacketID.ID;
				break;
			case GetAccountInfo.Request.REWARD_PROGRESS:
				@enum = RewardProgress.PacketID.ID;
				break;
			case GetAccountInfo.Request.HERO_XP:
				@enum = HeroXP.PacketID.ID;
				break;
			case GetAccountInfo.Request.TAVERN_BRAWL_INFO:
				@enum = TavernBrawlInfo.PacketID.ID;
				break;
			case GetAccountInfo.Request.TAVERN_BRAWL_RECORD:
				@enum = TavernBrawlPlayerRecordResponse.PacketID.ID;
				break;
			case GetAccountInfo.Request.FAVORITE_HEROES:
				@enum = FavoriteHeroesResponse.PacketID.ID;
				break;
			case GetAccountInfo.Request.ACCOUNT_LICENSES:
				@enum = AccountLicensesInfoResponse.PacketID.ID;
				break;
			case GetAccountInfo.Request.COINS:
				@enum = Coins.PacketID.ID;
				break;
			}
			return @enum != null && Network.Get().FakeHandleType(@enum);
		}
	}
}
