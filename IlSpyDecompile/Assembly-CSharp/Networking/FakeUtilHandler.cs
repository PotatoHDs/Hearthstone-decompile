using System;
using PegasusUtil;

namespace Networking
{
	internal static class FakeUtilHandler
	{
		internal static bool FakeUtilOutbound(int type, IProtoBuf body, int subId)
		{
			bool success = true;
			switch (type)
			{
			case 201:
			case 240:
				FakeProcessPacket(type, body, subId);
				break;
			case 327:
				((GenericRequestList)body).Requests.ForEach(delegate(GenericRequest request)
				{
					success = FakeProcessPacket(request.RequestId, body, request.RequestSubId) && success;
				});
				break;
			default:
				success = false;
				break;
			case 239:
			case 276:
			case 284:
			case 305:
				break;
			}
			return success;
		}

		private static bool FakeProcessPacket(int type, IProtoBuf body, int subId)
		{
			bool result = true;
			switch (type)
			{
			case 201:
				result = FakeUtilOutboundGetAccountInfo((GetAccountInfo.Request)subId);
				break;
			case 340:
				Network.Get().FakeHandleType(ClientStaticAssetsResponse.PacketID.ID);
				break;
			default:
				Log.Net.PrintWarning("FakeUtilOutbound: unable to simulate response for requestId={0} subId={1}", type, subId);
				break;
			}
			return result;
		}

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
			if (@enum != null)
			{
				return Network.Get().FakeHandleType(@enum);
			}
			return false;
		}
	}
}
