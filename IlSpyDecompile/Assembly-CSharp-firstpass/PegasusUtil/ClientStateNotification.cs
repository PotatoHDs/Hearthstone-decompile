using System.IO;

namespace PegasusUtil
{
	public class ClientStateNotification : IProtoBuf
	{
		public enum PacketID
		{
			ID = 333,
			System = 0
		}

		public bool HasAchievementNotifications;

		private AchievementNotifications _AchievementNotifications;

		public bool HasNoticeNotifications;

		private NoticeNotifications _NoticeNotifications;

		public bool HasCollectionModifications;

		private CollectionModifications _CollectionModifications;

		public bool HasCurrencyState;

		private GameCurrencyStates _CurrencyState;

		public bool HasBoosterModifications;

		private BoosterModifications _BoosterModifications;

		public bool HasHeroXp;

		private HeroXP _HeroXp;

		public bool HasPlayerRecords;

		private PlayerRecords _PlayerRecords;

		public bool HasArenaSessionResponse;

		private ArenaSessionResponse _ArenaSessionResponse;

		public bool HasCardBackModifications;

		private CardBackModifications _CardBackModifications;

		public bool HasPlayerDraftTickets;

		private PlayerDraftTickets _PlayerDraftTickets;

		public AchievementNotifications AchievementNotifications
		{
			get
			{
				return _AchievementNotifications;
			}
			set
			{
				_AchievementNotifications = value;
				HasAchievementNotifications = value != null;
			}
		}

		public NoticeNotifications NoticeNotifications
		{
			get
			{
				return _NoticeNotifications;
			}
			set
			{
				_NoticeNotifications = value;
				HasNoticeNotifications = value != null;
			}
		}

		public CollectionModifications CollectionModifications
		{
			get
			{
				return _CollectionModifications;
			}
			set
			{
				_CollectionModifications = value;
				HasCollectionModifications = value != null;
			}
		}

		public GameCurrencyStates CurrencyState
		{
			get
			{
				return _CurrencyState;
			}
			set
			{
				_CurrencyState = value;
				HasCurrencyState = value != null;
			}
		}

		public BoosterModifications BoosterModifications
		{
			get
			{
				return _BoosterModifications;
			}
			set
			{
				_BoosterModifications = value;
				HasBoosterModifications = value != null;
			}
		}

		public HeroXP HeroXp
		{
			get
			{
				return _HeroXp;
			}
			set
			{
				_HeroXp = value;
				HasHeroXp = value != null;
			}
		}

		public PlayerRecords PlayerRecords
		{
			get
			{
				return _PlayerRecords;
			}
			set
			{
				_PlayerRecords = value;
				HasPlayerRecords = value != null;
			}
		}

		public ArenaSessionResponse ArenaSessionResponse
		{
			get
			{
				return _ArenaSessionResponse;
			}
			set
			{
				_ArenaSessionResponse = value;
				HasArenaSessionResponse = value != null;
			}
		}

		public CardBackModifications CardBackModifications
		{
			get
			{
				return _CardBackModifications;
			}
			set
			{
				_CardBackModifications = value;
				HasCardBackModifications = value != null;
			}
		}

		public PlayerDraftTickets PlayerDraftTickets
		{
			get
			{
				return _PlayerDraftTickets;
			}
			set
			{
				_PlayerDraftTickets = value;
				HasPlayerDraftTickets = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAchievementNotifications)
			{
				num ^= AchievementNotifications.GetHashCode();
			}
			if (HasNoticeNotifications)
			{
				num ^= NoticeNotifications.GetHashCode();
			}
			if (HasCollectionModifications)
			{
				num ^= CollectionModifications.GetHashCode();
			}
			if (HasCurrencyState)
			{
				num ^= CurrencyState.GetHashCode();
			}
			if (HasBoosterModifications)
			{
				num ^= BoosterModifications.GetHashCode();
			}
			if (HasHeroXp)
			{
				num ^= HeroXp.GetHashCode();
			}
			if (HasPlayerRecords)
			{
				num ^= PlayerRecords.GetHashCode();
			}
			if (HasArenaSessionResponse)
			{
				num ^= ArenaSessionResponse.GetHashCode();
			}
			if (HasCardBackModifications)
			{
				num ^= CardBackModifications.GetHashCode();
			}
			if (HasPlayerDraftTickets)
			{
				num ^= PlayerDraftTickets.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientStateNotification clientStateNotification = obj as ClientStateNotification;
			if (clientStateNotification == null)
			{
				return false;
			}
			if (HasAchievementNotifications != clientStateNotification.HasAchievementNotifications || (HasAchievementNotifications && !AchievementNotifications.Equals(clientStateNotification.AchievementNotifications)))
			{
				return false;
			}
			if (HasNoticeNotifications != clientStateNotification.HasNoticeNotifications || (HasNoticeNotifications && !NoticeNotifications.Equals(clientStateNotification.NoticeNotifications)))
			{
				return false;
			}
			if (HasCollectionModifications != clientStateNotification.HasCollectionModifications || (HasCollectionModifications && !CollectionModifications.Equals(clientStateNotification.CollectionModifications)))
			{
				return false;
			}
			if (HasCurrencyState != clientStateNotification.HasCurrencyState || (HasCurrencyState && !CurrencyState.Equals(clientStateNotification.CurrencyState)))
			{
				return false;
			}
			if (HasBoosterModifications != clientStateNotification.HasBoosterModifications || (HasBoosterModifications && !BoosterModifications.Equals(clientStateNotification.BoosterModifications)))
			{
				return false;
			}
			if (HasHeroXp != clientStateNotification.HasHeroXp || (HasHeroXp && !HeroXp.Equals(clientStateNotification.HeroXp)))
			{
				return false;
			}
			if (HasPlayerRecords != clientStateNotification.HasPlayerRecords || (HasPlayerRecords && !PlayerRecords.Equals(clientStateNotification.PlayerRecords)))
			{
				return false;
			}
			if (HasArenaSessionResponse != clientStateNotification.HasArenaSessionResponse || (HasArenaSessionResponse && !ArenaSessionResponse.Equals(clientStateNotification.ArenaSessionResponse)))
			{
				return false;
			}
			if (HasCardBackModifications != clientStateNotification.HasCardBackModifications || (HasCardBackModifications && !CardBackModifications.Equals(clientStateNotification.CardBackModifications)))
			{
				return false;
			}
			if (HasPlayerDraftTickets != clientStateNotification.HasPlayerDraftTickets || (HasPlayerDraftTickets && !PlayerDraftTickets.Equals(clientStateNotification.PlayerDraftTickets)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientStateNotification Deserialize(Stream stream, ClientStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientStateNotification DeserializeLengthDelimited(Stream stream)
		{
			ClientStateNotification clientStateNotification = new ClientStateNotification();
			DeserializeLengthDelimited(stream, clientStateNotification);
			return clientStateNotification;
		}

		public static ClientStateNotification DeserializeLengthDelimited(Stream stream, ClientStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientStateNotification Deserialize(Stream stream, ClientStateNotification instance, long limit)
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
				case 10:
					if (instance.AchievementNotifications == null)
					{
						instance.AchievementNotifications = AchievementNotifications.DeserializeLengthDelimited(stream);
					}
					else
					{
						AchievementNotifications.DeserializeLengthDelimited(stream, instance.AchievementNotifications);
					}
					continue;
				case 18:
					if (instance.NoticeNotifications == null)
					{
						instance.NoticeNotifications = NoticeNotifications.DeserializeLengthDelimited(stream);
					}
					else
					{
						NoticeNotifications.DeserializeLengthDelimited(stream, instance.NoticeNotifications);
					}
					continue;
				case 26:
					if (instance.CollectionModifications == null)
					{
						instance.CollectionModifications = CollectionModifications.DeserializeLengthDelimited(stream);
					}
					else
					{
						CollectionModifications.DeserializeLengthDelimited(stream, instance.CollectionModifications);
					}
					continue;
				case 34:
					if (instance.CurrencyState == null)
					{
						instance.CurrencyState = GameCurrencyStates.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCurrencyStates.DeserializeLengthDelimited(stream, instance.CurrencyState);
					}
					continue;
				case 42:
					if (instance.BoosterModifications == null)
					{
						instance.BoosterModifications = BoosterModifications.DeserializeLengthDelimited(stream);
					}
					else
					{
						BoosterModifications.DeserializeLengthDelimited(stream, instance.BoosterModifications);
					}
					continue;
				case 50:
					if (instance.HeroXp == null)
					{
						instance.HeroXp = HeroXP.DeserializeLengthDelimited(stream);
					}
					else
					{
						HeroXP.DeserializeLengthDelimited(stream, instance.HeroXp);
					}
					continue;
				case 58:
					if (instance.PlayerRecords == null)
					{
						instance.PlayerRecords = PlayerRecords.DeserializeLengthDelimited(stream);
					}
					else
					{
						PlayerRecords.DeserializeLengthDelimited(stream, instance.PlayerRecords);
					}
					continue;
				case 66:
					if (instance.ArenaSessionResponse == null)
					{
						instance.ArenaSessionResponse = ArenaSessionResponse.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSessionResponse.DeserializeLengthDelimited(stream, instance.ArenaSessionResponse);
					}
					continue;
				case 74:
					if (instance.CardBackModifications == null)
					{
						instance.CardBackModifications = CardBackModifications.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardBackModifications.DeserializeLengthDelimited(stream, instance.CardBackModifications);
					}
					continue;
				case 82:
					if (instance.PlayerDraftTickets == null)
					{
						instance.PlayerDraftTickets = PlayerDraftTickets.DeserializeLengthDelimited(stream);
					}
					else
					{
						PlayerDraftTickets.DeserializeLengthDelimited(stream, instance.PlayerDraftTickets);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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

		public static void Serialize(Stream stream, ClientStateNotification instance)
		{
			if (instance.HasAchievementNotifications)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AchievementNotifications.GetSerializedSize());
				AchievementNotifications.Serialize(stream, instance.AchievementNotifications);
			}
			if (instance.HasNoticeNotifications)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.NoticeNotifications.GetSerializedSize());
				NoticeNotifications.Serialize(stream, instance.NoticeNotifications);
			}
			if (instance.HasCollectionModifications)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CollectionModifications.GetSerializedSize());
				CollectionModifications.Serialize(stream, instance.CollectionModifications);
			}
			if (instance.HasCurrencyState)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.CurrencyState.GetSerializedSize());
				GameCurrencyStates.Serialize(stream, instance.CurrencyState);
			}
			if (instance.HasBoosterModifications)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.BoosterModifications.GetSerializedSize());
				BoosterModifications.Serialize(stream, instance.BoosterModifications);
			}
			if (instance.HasHeroXp)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.HeroXp.GetSerializedSize());
				HeroXP.Serialize(stream, instance.HeroXp);
			}
			if (instance.HasPlayerRecords)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecords.GetSerializedSize());
				PlayerRecords.Serialize(stream, instance.PlayerRecords);
			}
			if (instance.HasArenaSessionResponse)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.ArenaSessionResponse.GetSerializedSize());
				ArenaSessionResponse.Serialize(stream, instance.ArenaSessionResponse);
			}
			if (instance.HasCardBackModifications)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.CardBackModifications.GetSerializedSize());
				CardBackModifications.Serialize(stream, instance.CardBackModifications);
			}
			if (instance.HasPlayerDraftTickets)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.PlayerDraftTickets.GetSerializedSize());
				PlayerDraftTickets.Serialize(stream, instance.PlayerDraftTickets);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAchievementNotifications)
			{
				num++;
				uint serializedSize = AchievementNotifications.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasNoticeNotifications)
			{
				num++;
				uint serializedSize2 = NoticeNotifications.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasCollectionModifications)
			{
				num++;
				uint serializedSize3 = CollectionModifications.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasCurrencyState)
			{
				num++;
				uint serializedSize4 = CurrencyState.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasBoosterModifications)
			{
				num++;
				uint serializedSize5 = BoosterModifications.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasHeroXp)
			{
				num++;
				uint serializedSize6 = HeroXp.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasPlayerRecords)
			{
				num++;
				uint serializedSize7 = PlayerRecords.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (HasArenaSessionResponse)
			{
				num++;
				uint serializedSize8 = ArenaSessionResponse.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			if (HasCardBackModifications)
			{
				num++;
				uint serializedSize9 = CardBackModifications.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (HasPlayerDraftTickets)
			{
				num++;
				uint serializedSize10 = PlayerDraftTickets.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			return num;
		}
	}
}
