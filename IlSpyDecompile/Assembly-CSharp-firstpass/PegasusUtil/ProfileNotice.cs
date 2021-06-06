using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class ProfileNotice : IProtoBuf
	{
		public bool HasMedal;

		private ProfileNoticeMedal _Medal;

		public bool HasRewardBooster;

		private ProfileNoticeRewardBooster _RewardBooster;

		public bool HasRewardCard;

		private ProfileNoticeRewardCard _RewardCard;

		public bool HasPreconDeck;

		private ProfileNoticePreconDeck _PreconDeck;

		public bool HasRewardDust;

		private ProfileNoticeRewardDust _RewardDust;

		public bool HasRewardCurrency;

		private ProfileNoticeRewardCurrency _RewardCurrency;

		public bool HasRewardMount;

		private ProfileNoticeRewardMount _RewardMount;

		public bool HasRewardForge;

		private ProfileNoticeRewardForge _RewardForge;

		public bool HasOriginData;

		private long _OriginData;

		public bool HasPurchase;

		private ProfileNoticePurchase _Purchase;

		public bool HasRewardCardBack;

		private ProfileNoticeCardBack _RewardCardBack;

		public bool HasDcGameResult;

		private ProfileNoticeDisconnectedGameResult _DcGameResult;

		public bool HasBonusStars;

		private ProfileNoticeBonusStars _BonusStars;

		public bool HasAdventureProgress;

		private ProfileNoticeAdventureProgress _AdventureProgress;

		public bool HasLevelUp;

		private ProfileNoticeLevelUp _LevelUp;

		public bool HasAccountLicense;

		private ProfileNoticeAccountLicense _AccountLicense;

		public bool HasTavernBrawlRewards;

		private ProfileNoticeTavernBrawlRewards _TavernBrawlRewards;

		public bool HasTavernBrawlTicket;

		private ProfileNoticeTavernBrawlTicket _TavernBrawlTicket;

		public bool HasGenericRewardChest;

		private ProfileNoticeGenericRewardChest _GenericRewardChest;

		public bool HasLeaguePromotionRewards;

		private ProfileNoticeLeaguePromotionRewards _LeaguePromotionRewards;

		public bool HasDcGameResultNew;

		private ProfileNoticeDisconnectedGameResultNew _DcGameResultNew;

		public bool HasFreeDeckChoice;

		private ProfileNoticeFreeDeckChoice _FreeDeckChoice;

		public bool HasDeckRemoved;

		private ProfileNoticeDeckRemoved _DeckRemoved;

		public bool HasDeckGranted;

		private ProfileNoticeDeckGranted _DeckGranted;

		public bool HasMiniSetGranted;

		private ProfileNoticeMiniSetGranted _MiniSetGranted;

		public bool HasSellableDeckGranted;

		private ProfileNoticeSellableDeckGranted _SellableDeckGranted;

		public long Entry { get; set; }

		public ProfileNoticeMedal Medal
		{
			get
			{
				return _Medal;
			}
			set
			{
				_Medal = value;
				HasMedal = value != null;
			}
		}

		public ProfileNoticeRewardBooster RewardBooster
		{
			get
			{
				return _RewardBooster;
			}
			set
			{
				_RewardBooster = value;
				HasRewardBooster = value != null;
			}
		}

		public ProfileNoticeRewardCard RewardCard
		{
			get
			{
				return _RewardCard;
			}
			set
			{
				_RewardCard = value;
				HasRewardCard = value != null;
			}
		}

		public ProfileNoticePreconDeck PreconDeck
		{
			get
			{
				return _PreconDeck;
			}
			set
			{
				_PreconDeck = value;
				HasPreconDeck = value != null;
			}
		}

		public ProfileNoticeRewardDust RewardDust
		{
			get
			{
				return _RewardDust;
			}
			set
			{
				_RewardDust = value;
				HasRewardDust = value != null;
			}
		}

		public ProfileNoticeRewardCurrency RewardCurrency
		{
			get
			{
				return _RewardCurrency;
			}
			set
			{
				_RewardCurrency = value;
				HasRewardCurrency = value != null;
			}
		}

		public ProfileNoticeRewardMount RewardMount
		{
			get
			{
				return _RewardMount;
			}
			set
			{
				_RewardMount = value;
				HasRewardMount = value != null;
			}
		}

		public ProfileNoticeRewardForge RewardForge
		{
			get
			{
				return _RewardForge;
			}
			set
			{
				_RewardForge = value;
				HasRewardForge = value != null;
			}
		}

		public int Origin { get; set; }

		public long OriginData
		{
			get
			{
				return _OriginData;
			}
			set
			{
				_OriginData = value;
				HasOriginData = true;
			}
		}

		public Date When { get; set; }

		public ProfileNoticePurchase Purchase
		{
			get
			{
				return _Purchase;
			}
			set
			{
				_Purchase = value;
				HasPurchase = value != null;
			}
		}

		public ProfileNoticeCardBack RewardCardBack
		{
			get
			{
				return _RewardCardBack;
			}
			set
			{
				_RewardCardBack = value;
				HasRewardCardBack = value != null;
			}
		}

		public ProfileNoticeDisconnectedGameResult DcGameResult
		{
			get
			{
				return _DcGameResult;
			}
			set
			{
				_DcGameResult = value;
				HasDcGameResult = value != null;
			}
		}

		public ProfileNoticeBonusStars BonusStars
		{
			get
			{
				return _BonusStars;
			}
			set
			{
				_BonusStars = value;
				HasBonusStars = value != null;
			}
		}

		public ProfileNoticeAdventureProgress AdventureProgress
		{
			get
			{
				return _AdventureProgress;
			}
			set
			{
				_AdventureProgress = value;
				HasAdventureProgress = value != null;
			}
		}

		public ProfileNoticeLevelUp LevelUp
		{
			get
			{
				return _LevelUp;
			}
			set
			{
				_LevelUp = value;
				HasLevelUp = value != null;
			}
		}

		public ProfileNoticeAccountLicense AccountLicense
		{
			get
			{
				return _AccountLicense;
			}
			set
			{
				_AccountLicense = value;
				HasAccountLicense = value != null;
			}
		}

		public ProfileNoticeTavernBrawlRewards TavernBrawlRewards
		{
			get
			{
				return _TavernBrawlRewards;
			}
			set
			{
				_TavernBrawlRewards = value;
				HasTavernBrawlRewards = value != null;
			}
		}

		public ProfileNoticeTavernBrawlTicket TavernBrawlTicket
		{
			get
			{
				return _TavernBrawlTicket;
			}
			set
			{
				_TavernBrawlTicket = value;
				HasTavernBrawlTicket = value != null;
			}
		}

		public ProfileNoticeGenericRewardChest GenericRewardChest
		{
			get
			{
				return _GenericRewardChest;
			}
			set
			{
				_GenericRewardChest = value;
				HasGenericRewardChest = value != null;
			}
		}

		public ProfileNoticeLeaguePromotionRewards LeaguePromotionRewards
		{
			get
			{
				return _LeaguePromotionRewards;
			}
			set
			{
				_LeaguePromotionRewards = value;
				HasLeaguePromotionRewards = value != null;
			}
		}

		public ProfileNoticeDisconnectedGameResultNew DcGameResultNew
		{
			get
			{
				return _DcGameResultNew;
			}
			set
			{
				_DcGameResultNew = value;
				HasDcGameResultNew = value != null;
			}
		}

		public ProfileNoticeFreeDeckChoice FreeDeckChoice
		{
			get
			{
				return _FreeDeckChoice;
			}
			set
			{
				_FreeDeckChoice = value;
				HasFreeDeckChoice = value != null;
			}
		}

		public ProfileNoticeDeckRemoved DeckRemoved
		{
			get
			{
				return _DeckRemoved;
			}
			set
			{
				_DeckRemoved = value;
				HasDeckRemoved = value != null;
			}
		}

		public ProfileNoticeDeckGranted DeckGranted
		{
			get
			{
				return _DeckGranted;
			}
			set
			{
				_DeckGranted = value;
				HasDeckGranted = value != null;
			}
		}

		public ProfileNoticeMiniSetGranted MiniSetGranted
		{
			get
			{
				return _MiniSetGranted;
			}
			set
			{
				_MiniSetGranted = value;
				HasMiniSetGranted = value != null;
			}
		}

		public ProfileNoticeSellableDeckGranted SellableDeckGranted
		{
			get
			{
				return _SellableDeckGranted;
			}
			set
			{
				_SellableDeckGranted = value;
				HasSellableDeckGranted = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Entry.GetHashCode();
			if (HasMedal)
			{
				hashCode ^= Medal.GetHashCode();
			}
			if (HasRewardBooster)
			{
				hashCode ^= RewardBooster.GetHashCode();
			}
			if (HasRewardCard)
			{
				hashCode ^= RewardCard.GetHashCode();
			}
			if (HasPreconDeck)
			{
				hashCode ^= PreconDeck.GetHashCode();
			}
			if (HasRewardDust)
			{
				hashCode ^= RewardDust.GetHashCode();
			}
			if (HasRewardCurrency)
			{
				hashCode ^= RewardCurrency.GetHashCode();
			}
			if (HasRewardMount)
			{
				hashCode ^= RewardMount.GetHashCode();
			}
			if (HasRewardForge)
			{
				hashCode ^= RewardForge.GetHashCode();
			}
			hashCode ^= Origin.GetHashCode();
			if (HasOriginData)
			{
				hashCode ^= OriginData.GetHashCode();
			}
			hashCode ^= When.GetHashCode();
			if (HasPurchase)
			{
				hashCode ^= Purchase.GetHashCode();
			}
			if (HasRewardCardBack)
			{
				hashCode ^= RewardCardBack.GetHashCode();
			}
			if (HasDcGameResult)
			{
				hashCode ^= DcGameResult.GetHashCode();
			}
			if (HasBonusStars)
			{
				hashCode ^= BonusStars.GetHashCode();
			}
			if (HasAdventureProgress)
			{
				hashCode ^= AdventureProgress.GetHashCode();
			}
			if (HasLevelUp)
			{
				hashCode ^= LevelUp.GetHashCode();
			}
			if (HasAccountLicense)
			{
				hashCode ^= AccountLicense.GetHashCode();
			}
			if (HasTavernBrawlRewards)
			{
				hashCode ^= TavernBrawlRewards.GetHashCode();
			}
			if (HasTavernBrawlTicket)
			{
				hashCode ^= TavernBrawlTicket.GetHashCode();
			}
			if (HasGenericRewardChest)
			{
				hashCode ^= GenericRewardChest.GetHashCode();
			}
			if (HasLeaguePromotionRewards)
			{
				hashCode ^= LeaguePromotionRewards.GetHashCode();
			}
			if (HasDcGameResultNew)
			{
				hashCode ^= DcGameResultNew.GetHashCode();
			}
			if (HasFreeDeckChoice)
			{
				hashCode ^= FreeDeckChoice.GetHashCode();
			}
			if (HasDeckRemoved)
			{
				hashCode ^= DeckRemoved.GetHashCode();
			}
			if (HasDeckGranted)
			{
				hashCode ^= DeckGranted.GetHashCode();
			}
			if (HasMiniSetGranted)
			{
				hashCode ^= MiniSetGranted.GetHashCode();
			}
			if (HasSellableDeckGranted)
			{
				hashCode ^= SellableDeckGranted.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNotice profileNotice = obj as ProfileNotice;
			if (profileNotice == null)
			{
				return false;
			}
			if (!Entry.Equals(profileNotice.Entry))
			{
				return false;
			}
			if (HasMedal != profileNotice.HasMedal || (HasMedal && !Medal.Equals(profileNotice.Medal)))
			{
				return false;
			}
			if (HasRewardBooster != profileNotice.HasRewardBooster || (HasRewardBooster && !RewardBooster.Equals(profileNotice.RewardBooster)))
			{
				return false;
			}
			if (HasRewardCard != profileNotice.HasRewardCard || (HasRewardCard && !RewardCard.Equals(profileNotice.RewardCard)))
			{
				return false;
			}
			if (HasPreconDeck != profileNotice.HasPreconDeck || (HasPreconDeck && !PreconDeck.Equals(profileNotice.PreconDeck)))
			{
				return false;
			}
			if (HasRewardDust != profileNotice.HasRewardDust || (HasRewardDust && !RewardDust.Equals(profileNotice.RewardDust)))
			{
				return false;
			}
			if (HasRewardCurrency != profileNotice.HasRewardCurrency || (HasRewardCurrency && !RewardCurrency.Equals(profileNotice.RewardCurrency)))
			{
				return false;
			}
			if (HasRewardMount != profileNotice.HasRewardMount || (HasRewardMount && !RewardMount.Equals(profileNotice.RewardMount)))
			{
				return false;
			}
			if (HasRewardForge != profileNotice.HasRewardForge || (HasRewardForge && !RewardForge.Equals(profileNotice.RewardForge)))
			{
				return false;
			}
			if (!Origin.Equals(profileNotice.Origin))
			{
				return false;
			}
			if (HasOriginData != profileNotice.HasOriginData || (HasOriginData && !OriginData.Equals(profileNotice.OriginData)))
			{
				return false;
			}
			if (!When.Equals(profileNotice.When))
			{
				return false;
			}
			if (HasPurchase != profileNotice.HasPurchase || (HasPurchase && !Purchase.Equals(profileNotice.Purchase)))
			{
				return false;
			}
			if (HasRewardCardBack != profileNotice.HasRewardCardBack || (HasRewardCardBack && !RewardCardBack.Equals(profileNotice.RewardCardBack)))
			{
				return false;
			}
			if (HasDcGameResult != profileNotice.HasDcGameResult || (HasDcGameResult && !DcGameResult.Equals(profileNotice.DcGameResult)))
			{
				return false;
			}
			if (HasBonusStars != profileNotice.HasBonusStars || (HasBonusStars && !BonusStars.Equals(profileNotice.BonusStars)))
			{
				return false;
			}
			if (HasAdventureProgress != profileNotice.HasAdventureProgress || (HasAdventureProgress && !AdventureProgress.Equals(profileNotice.AdventureProgress)))
			{
				return false;
			}
			if (HasLevelUp != profileNotice.HasLevelUp || (HasLevelUp && !LevelUp.Equals(profileNotice.LevelUp)))
			{
				return false;
			}
			if (HasAccountLicense != profileNotice.HasAccountLicense || (HasAccountLicense && !AccountLicense.Equals(profileNotice.AccountLicense)))
			{
				return false;
			}
			if (HasTavernBrawlRewards != profileNotice.HasTavernBrawlRewards || (HasTavernBrawlRewards && !TavernBrawlRewards.Equals(profileNotice.TavernBrawlRewards)))
			{
				return false;
			}
			if (HasTavernBrawlTicket != profileNotice.HasTavernBrawlTicket || (HasTavernBrawlTicket && !TavernBrawlTicket.Equals(profileNotice.TavernBrawlTicket)))
			{
				return false;
			}
			if (HasGenericRewardChest != profileNotice.HasGenericRewardChest || (HasGenericRewardChest && !GenericRewardChest.Equals(profileNotice.GenericRewardChest)))
			{
				return false;
			}
			if (HasLeaguePromotionRewards != profileNotice.HasLeaguePromotionRewards || (HasLeaguePromotionRewards && !LeaguePromotionRewards.Equals(profileNotice.LeaguePromotionRewards)))
			{
				return false;
			}
			if (HasDcGameResultNew != profileNotice.HasDcGameResultNew || (HasDcGameResultNew && !DcGameResultNew.Equals(profileNotice.DcGameResultNew)))
			{
				return false;
			}
			if (HasFreeDeckChoice != profileNotice.HasFreeDeckChoice || (HasFreeDeckChoice && !FreeDeckChoice.Equals(profileNotice.FreeDeckChoice)))
			{
				return false;
			}
			if (HasDeckRemoved != profileNotice.HasDeckRemoved || (HasDeckRemoved && !DeckRemoved.Equals(profileNotice.DeckRemoved)))
			{
				return false;
			}
			if (HasDeckGranted != profileNotice.HasDeckGranted || (HasDeckGranted && !DeckGranted.Equals(profileNotice.DeckGranted)))
			{
				return false;
			}
			if (HasMiniSetGranted != profileNotice.HasMiniSetGranted || (HasMiniSetGranted && !MiniSetGranted.Equals(profileNotice.MiniSetGranted)))
			{
				return false;
			}
			if (HasSellableDeckGranted != profileNotice.HasSellableDeckGranted || (HasSellableDeckGranted && !SellableDeckGranted.Equals(profileNotice.SellableDeckGranted)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNotice Deserialize(Stream stream, ProfileNotice instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNotice DeserializeLengthDelimited(Stream stream)
		{
			ProfileNotice profileNotice = new ProfileNotice();
			DeserializeLengthDelimited(stream, profileNotice);
			return profileNotice;
		}

		public static ProfileNotice DeserializeLengthDelimited(Stream stream, ProfileNotice instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNotice Deserialize(Stream stream, ProfileNotice instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.Entry = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Medal == null)
					{
						instance.Medal = ProfileNoticeMedal.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeMedal.DeserializeLengthDelimited(stream, instance.Medal);
					}
					continue;
				case 26:
					if (instance.RewardBooster == null)
					{
						instance.RewardBooster = ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream, instance.RewardBooster);
					}
					continue;
				case 34:
					if (instance.RewardCard == null)
					{
						instance.RewardCard = ProfileNoticeRewardCard.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardCard.DeserializeLengthDelimited(stream, instance.RewardCard);
					}
					continue;
				case 50:
					if (instance.PreconDeck == null)
					{
						instance.PreconDeck = ProfileNoticePreconDeck.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticePreconDeck.DeserializeLengthDelimited(stream, instance.PreconDeck);
					}
					continue;
				case 58:
					if (instance.RewardDust == null)
					{
						instance.RewardDust = ProfileNoticeRewardDust.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardDust.DeserializeLengthDelimited(stream, instance.RewardDust);
					}
					continue;
				case 66:
					if (instance.RewardCurrency == null)
					{
						instance.RewardCurrency = ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream, instance.RewardCurrency);
					}
					continue;
				case 74:
					if (instance.RewardMount == null)
					{
						instance.RewardMount = ProfileNoticeRewardMount.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardMount.DeserializeLengthDelimited(stream, instance.RewardMount);
					}
					continue;
				case 82:
					if (instance.RewardForge == null)
					{
						instance.RewardForge = ProfileNoticeRewardForge.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardForge.DeserializeLengthDelimited(stream, instance.RewardForge);
					}
					continue;
				case 88:
					instance.Origin = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.OriginData = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 106:
					if (instance.When == null)
					{
						instance.When = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.When);
					}
					continue;
				case 114:
					if (instance.Purchase == null)
					{
						instance.Purchase = ProfileNoticePurchase.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticePurchase.DeserializeLengthDelimited(stream, instance.Purchase);
					}
					continue;
				case 122:
					if (instance.RewardCardBack == null)
					{
						instance.RewardCardBack = ProfileNoticeCardBack.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeCardBack.DeserializeLengthDelimited(stream, instance.RewardCardBack);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.DcGameResult == null)
							{
								instance.DcGameResult = ProfileNoticeDisconnectedGameResult.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeDisconnectedGameResult.DeserializeLengthDelimited(stream, instance.DcGameResult);
							}
						}
						break;
					case 17u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.BonusStars == null)
							{
								instance.BonusStars = ProfileNoticeBonusStars.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeBonusStars.DeserializeLengthDelimited(stream, instance.BonusStars);
							}
						}
						break;
					case 18u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.AdventureProgress == null)
							{
								instance.AdventureProgress = ProfileNoticeAdventureProgress.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeAdventureProgress.DeserializeLengthDelimited(stream, instance.AdventureProgress);
							}
						}
						break;
					case 19u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.LevelUp == null)
							{
								instance.LevelUp = ProfileNoticeLevelUp.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeLevelUp.DeserializeLengthDelimited(stream, instance.LevelUp);
							}
						}
						break;
					case 20u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.AccountLicense == null)
							{
								instance.AccountLicense = ProfileNoticeAccountLicense.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeAccountLicense.DeserializeLengthDelimited(stream, instance.AccountLicense);
							}
						}
						break;
					case 21u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.TavernBrawlRewards == null)
							{
								instance.TavernBrawlRewards = ProfileNoticeTavernBrawlRewards.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeTavernBrawlRewards.DeserializeLengthDelimited(stream, instance.TavernBrawlRewards);
							}
						}
						break;
					case 22u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.TavernBrawlTicket == null)
							{
								instance.TavernBrawlTicket = ProfileNoticeTavernBrawlTicket.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeTavernBrawlTicket.DeserializeLengthDelimited(stream, instance.TavernBrawlTicket);
							}
						}
						break;
					case 23u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.GenericRewardChest == null)
							{
								instance.GenericRewardChest = ProfileNoticeGenericRewardChest.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeGenericRewardChest.DeserializeLengthDelimited(stream, instance.GenericRewardChest);
							}
						}
						break;
					case 24u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.LeaguePromotionRewards == null)
							{
								instance.LeaguePromotionRewards = ProfileNoticeLeaguePromotionRewards.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeLeaguePromotionRewards.DeserializeLengthDelimited(stream, instance.LeaguePromotionRewards);
							}
						}
						break;
					case 26u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.DcGameResultNew == null)
							{
								instance.DcGameResultNew = ProfileNoticeDisconnectedGameResultNew.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeDisconnectedGameResultNew.DeserializeLengthDelimited(stream, instance.DcGameResultNew);
							}
						}
						break;
					case 27u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.FreeDeckChoice == null)
							{
								instance.FreeDeckChoice = ProfileNoticeFreeDeckChoice.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeFreeDeckChoice.DeserializeLengthDelimited(stream, instance.FreeDeckChoice);
							}
						}
						break;
					case 28u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.DeckRemoved == null)
							{
								instance.DeckRemoved = ProfileNoticeDeckRemoved.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeDeckRemoved.DeserializeLengthDelimited(stream, instance.DeckRemoved);
							}
						}
						break;
					case 29u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.DeckGranted == null)
							{
								instance.DeckGranted = ProfileNoticeDeckGranted.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeDeckGranted.DeserializeLengthDelimited(stream, instance.DeckGranted);
							}
						}
						break;
					case 30u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.MiniSetGranted == null)
							{
								instance.MiniSetGranted = ProfileNoticeMiniSetGranted.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeMiniSetGranted.DeserializeLengthDelimited(stream, instance.MiniSetGranted);
							}
						}
						break;
					case 31u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.SellableDeckGranted == null)
							{
								instance.SellableDeckGranted = ProfileNoticeSellableDeckGranted.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProfileNoticeSellableDeckGranted.DeserializeLengthDelimited(stream, instance.SellableDeckGranted);
							}
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ProfileNotice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entry);
			if (instance.HasMedal)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Medal.GetSerializedSize());
				ProfileNoticeMedal.Serialize(stream, instance.Medal);
			}
			if (instance.HasRewardBooster)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RewardBooster.GetSerializedSize());
				ProfileNoticeRewardBooster.Serialize(stream, instance.RewardBooster);
			}
			if (instance.HasRewardCard)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RewardCard.GetSerializedSize());
				ProfileNoticeRewardCard.Serialize(stream, instance.RewardCard);
			}
			if (instance.HasPreconDeck)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PreconDeck.GetSerializedSize());
				ProfileNoticePreconDeck.Serialize(stream, instance.PreconDeck);
			}
			if (instance.HasRewardDust)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.RewardDust.GetSerializedSize());
				ProfileNoticeRewardDust.Serialize(stream, instance.RewardDust);
			}
			if (instance.HasRewardCurrency)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.RewardCurrency.GetSerializedSize());
				ProfileNoticeRewardCurrency.Serialize(stream, instance.RewardCurrency);
			}
			if (instance.HasRewardMount)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.RewardMount.GetSerializedSize());
				ProfileNoticeRewardMount.Serialize(stream, instance.RewardMount);
			}
			if (instance.HasRewardForge)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.RewardForge.GetSerializedSize());
				ProfileNoticeRewardForge.Serialize(stream, instance.RewardForge);
			}
			stream.WriteByte(88);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Origin);
			if (instance.HasOriginData)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OriginData);
			}
			if (instance.When == null)
			{
				throw new ArgumentNullException("When", "Required by proto specification.");
			}
			stream.WriteByte(106);
			ProtocolParser.WriteUInt32(stream, instance.When.GetSerializedSize());
			Date.Serialize(stream, instance.When);
			if (instance.HasPurchase)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.Purchase.GetSerializedSize());
				ProfileNoticePurchase.Serialize(stream, instance.Purchase);
			}
			if (instance.HasRewardCardBack)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.RewardCardBack.GetSerializedSize());
				ProfileNoticeCardBack.Serialize(stream, instance.RewardCardBack);
			}
			if (instance.HasDcGameResult)
			{
				stream.WriteByte(130);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DcGameResult.GetSerializedSize());
				ProfileNoticeDisconnectedGameResult.Serialize(stream, instance.DcGameResult);
			}
			if (instance.HasBonusStars)
			{
				stream.WriteByte(138);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.BonusStars.GetSerializedSize());
				ProfileNoticeBonusStars.Serialize(stream, instance.BonusStars);
			}
			if (instance.HasAdventureProgress)
			{
				stream.WriteByte(146);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.AdventureProgress.GetSerializedSize());
				ProfileNoticeAdventureProgress.Serialize(stream, instance.AdventureProgress);
			}
			if (instance.HasLevelUp)
			{
				stream.WriteByte(154);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.LevelUp.GetSerializedSize());
				ProfileNoticeLevelUp.Serialize(stream, instance.LevelUp);
			}
			if (instance.HasAccountLicense)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.AccountLicense.GetSerializedSize());
				ProfileNoticeAccountLicense.Serialize(stream, instance.AccountLicense);
			}
			if (instance.HasTavernBrawlRewards)
			{
				stream.WriteByte(170);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.TavernBrawlRewards.GetSerializedSize());
				ProfileNoticeTavernBrawlRewards.Serialize(stream, instance.TavernBrawlRewards);
			}
			if (instance.HasTavernBrawlTicket)
			{
				stream.WriteByte(178);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.TavernBrawlTicket.GetSerializedSize());
				ProfileNoticeTavernBrawlTicket.Serialize(stream, instance.TavernBrawlTicket);
			}
			if (instance.HasGenericRewardChest)
			{
				stream.WriteByte(186);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.GenericRewardChest.GetSerializedSize());
				ProfileNoticeGenericRewardChest.Serialize(stream, instance.GenericRewardChest);
			}
			if (instance.HasLeaguePromotionRewards)
			{
				stream.WriteByte(194);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.LeaguePromotionRewards.GetSerializedSize());
				ProfileNoticeLeaguePromotionRewards.Serialize(stream, instance.LeaguePromotionRewards);
			}
			if (instance.HasDcGameResultNew)
			{
				stream.WriteByte(210);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DcGameResultNew.GetSerializedSize());
				ProfileNoticeDisconnectedGameResultNew.Serialize(stream, instance.DcGameResultNew);
			}
			if (instance.HasFreeDeckChoice)
			{
				stream.WriteByte(218);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.FreeDeckChoice.GetSerializedSize());
				ProfileNoticeFreeDeckChoice.Serialize(stream, instance.FreeDeckChoice);
			}
			if (instance.HasDeckRemoved)
			{
				stream.WriteByte(226);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DeckRemoved.GetSerializedSize());
				ProfileNoticeDeckRemoved.Serialize(stream, instance.DeckRemoved);
			}
			if (instance.HasDeckGranted)
			{
				stream.WriteByte(234);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DeckGranted.GetSerializedSize());
				ProfileNoticeDeckGranted.Serialize(stream, instance.DeckGranted);
			}
			if (instance.HasMiniSetGranted)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.MiniSetGranted.GetSerializedSize());
				ProfileNoticeMiniSetGranted.Serialize(stream, instance.MiniSetGranted);
			}
			if (instance.HasSellableDeckGranted)
			{
				stream.WriteByte(250);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.SellableDeckGranted.GetSerializedSize());
				ProfileNoticeSellableDeckGranted.Serialize(stream, instance.SellableDeckGranted);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Entry);
			if (HasMedal)
			{
				num++;
				uint serializedSize = Medal.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRewardBooster)
			{
				num++;
				uint serializedSize2 = RewardBooster.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRewardCard)
			{
				num++;
				uint serializedSize3 = RewardCard.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasPreconDeck)
			{
				num++;
				uint serializedSize4 = PreconDeck.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasRewardDust)
			{
				num++;
				uint serializedSize5 = RewardDust.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasRewardCurrency)
			{
				num++;
				uint serializedSize6 = RewardCurrency.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasRewardMount)
			{
				num++;
				uint serializedSize7 = RewardMount.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (HasRewardForge)
			{
				num++;
				uint serializedSize8 = RewardForge.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)Origin);
			if (HasOriginData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OriginData);
			}
			uint serializedSize9 = When.GetSerializedSize();
			num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			if (HasPurchase)
			{
				num++;
				uint serializedSize10 = Purchase.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (HasRewardCardBack)
			{
				num++;
				uint serializedSize11 = RewardCardBack.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (HasDcGameResult)
			{
				num += 2;
				uint serializedSize12 = DcGameResult.GetSerializedSize();
				num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
			}
			if (HasBonusStars)
			{
				num += 2;
				uint serializedSize13 = BonusStars.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (HasAdventureProgress)
			{
				num += 2;
				uint serializedSize14 = AdventureProgress.GetSerializedSize();
				num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
			}
			if (HasLevelUp)
			{
				num += 2;
				uint serializedSize15 = LevelUp.GetSerializedSize();
				num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
			}
			if (HasAccountLicense)
			{
				num += 2;
				uint serializedSize16 = AccountLicense.GetSerializedSize();
				num += serializedSize16 + ProtocolParser.SizeOfUInt32(serializedSize16);
			}
			if (HasTavernBrawlRewards)
			{
				num += 2;
				uint serializedSize17 = TavernBrawlRewards.GetSerializedSize();
				num += serializedSize17 + ProtocolParser.SizeOfUInt32(serializedSize17);
			}
			if (HasTavernBrawlTicket)
			{
				num += 2;
				uint serializedSize18 = TavernBrawlTicket.GetSerializedSize();
				num += serializedSize18 + ProtocolParser.SizeOfUInt32(serializedSize18);
			}
			if (HasGenericRewardChest)
			{
				num += 2;
				uint serializedSize19 = GenericRewardChest.GetSerializedSize();
				num += serializedSize19 + ProtocolParser.SizeOfUInt32(serializedSize19);
			}
			if (HasLeaguePromotionRewards)
			{
				num += 2;
				uint serializedSize20 = LeaguePromotionRewards.GetSerializedSize();
				num += serializedSize20 + ProtocolParser.SizeOfUInt32(serializedSize20);
			}
			if (HasDcGameResultNew)
			{
				num += 2;
				uint serializedSize21 = DcGameResultNew.GetSerializedSize();
				num += serializedSize21 + ProtocolParser.SizeOfUInt32(serializedSize21);
			}
			if (HasFreeDeckChoice)
			{
				num += 2;
				uint serializedSize22 = FreeDeckChoice.GetSerializedSize();
				num += serializedSize22 + ProtocolParser.SizeOfUInt32(serializedSize22);
			}
			if (HasDeckRemoved)
			{
				num += 2;
				uint serializedSize23 = DeckRemoved.GetSerializedSize();
				num += serializedSize23 + ProtocolParser.SizeOfUInt32(serializedSize23);
			}
			if (HasDeckGranted)
			{
				num += 2;
				uint serializedSize24 = DeckGranted.GetSerializedSize();
				num += serializedSize24 + ProtocolParser.SizeOfUInt32(serializedSize24);
			}
			if (HasMiniSetGranted)
			{
				num += 2;
				uint serializedSize25 = MiniSetGranted.GetSerializedSize();
				num += serializedSize25 + ProtocolParser.SizeOfUInt32(serializedSize25);
			}
			if (HasSellableDeckGranted)
			{
				num += 2;
				uint serializedSize26 = SellableDeckGranted.GetSerializedSize();
				num += serializedSize26 + ProtocolParser.SizeOfUInt32(serializedSize26);
			}
			return num + 3;
		}
	}
}
