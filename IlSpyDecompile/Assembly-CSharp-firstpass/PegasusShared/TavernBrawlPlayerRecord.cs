using System.IO;

namespace PegasusShared
{
	public class TavernBrawlPlayerRecord : IProtoBuf
	{
		public bool HasGamesPlayed;

		private int _GamesPlayed;

		public bool HasWinStreak;

		private int _WinStreak;

		public bool HasSessionStatus;

		private TavernBrawlStatus _SessionStatus;

		public bool HasNumTicketsOwned;

		private int _NumTicketsOwned;

		public bool HasSession;

		private TavernBrawlPlayerSession _Session;

		public bool HasNumSessionsPurchasable;

		private int _NumSessionsPurchasable;

		public bool HasBrawlType;

		private BrawlType _BrawlType;

		public int RewardProgress { get; set; }

		public int GamesPlayed
		{
			get
			{
				return _GamesPlayed;
			}
			set
			{
				_GamesPlayed = value;
				HasGamesPlayed = true;
			}
		}

		public int GamesWon { get; set; }

		public int WinStreak
		{
			get
			{
				return _WinStreak;
			}
			set
			{
				_WinStreak = value;
				HasWinStreak = true;
			}
		}

		public TavernBrawlStatus SessionStatus
		{
			get
			{
				return _SessionStatus;
			}
			set
			{
				_SessionStatus = value;
				HasSessionStatus = true;
			}
		}

		public int NumTicketsOwned
		{
			get
			{
				return _NumTicketsOwned;
			}
			set
			{
				_NumTicketsOwned = value;
				HasNumTicketsOwned = true;
			}
		}

		public TavernBrawlPlayerSession Session
		{
			get
			{
				return _Session;
			}
			set
			{
				_Session = value;
				HasSession = value != null;
			}
		}

		public int NumSessionsPurchasable
		{
			get
			{
				return _NumSessionsPurchasable;
			}
			set
			{
				_NumSessionsPurchasable = value;
				HasNumSessionsPurchasable = true;
			}
		}

		public BrawlType BrawlType
		{
			get
			{
				return _BrawlType;
			}
			set
			{
				_BrawlType = value;
				HasBrawlType = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RewardProgress.GetHashCode();
			if (HasGamesPlayed)
			{
				hashCode ^= GamesPlayed.GetHashCode();
			}
			hashCode ^= GamesWon.GetHashCode();
			if (HasWinStreak)
			{
				hashCode ^= WinStreak.GetHashCode();
			}
			if (HasSessionStatus)
			{
				hashCode ^= SessionStatus.GetHashCode();
			}
			if (HasNumTicketsOwned)
			{
				hashCode ^= NumTicketsOwned.GetHashCode();
			}
			if (HasSession)
			{
				hashCode ^= Session.GetHashCode();
			}
			if (HasNumSessionsPurchasable)
			{
				hashCode ^= NumSessionsPurchasable.GetHashCode();
			}
			if (HasBrawlType)
			{
				hashCode ^= BrawlType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlPlayerRecord tavernBrawlPlayerRecord = obj as TavernBrawlPlayerRecord;
			if (tavernBrawlPlayerRecord == null)
			{
				return false;
			}
			if (!RewardProgress.Equals(tavernBrawlPlayerRecord.RewardProgress))
			{
				return false;
			}
			if (HasGamesPlayed != tavernBrawlPlayerRecord.HasGamesPlayed || (HasGamesPlayed && !GamesPlayed.Equals(tavernBrawlPlayerRecord.GamesPlayed)))
			{
				return false;
			}
			if (!GamesWon.Equals(tavernBrawlPlayerRecord.GamesWon))
			{
				return false;
			}
			if (HasWinStreak != tavernBrawlPlayerRecord.HasWinStreak || (HasWinStreak && !WinStreak.Equals(tavernBrawlPlayerRecord.WinStreak)))
			{
				return false;
			}
			if (HasSessionStatus != tavernBrawlPlayerRecord.HasSessionStatus || (HasSessionStatus && !SessionStatus.Equals(tavernBrawlPlayerRecord.SessionStatus)))
			{
				return false;
			}
			if (HasNumTicketsOwned != tavernBrawlPlayerRecord.HasNumTicketsOwned || (HasNumTicketsOwned && !NumTicketsOwned.Equals(tavernBrawlPlayerRecord.NumTicketsOwned)))
			{
				return false;
			}
			if (HasSession != tavernBrawlPlayerRecord.HasSession || (HasSession && !Session.Equals(tavernBrawlPlayerRecord.Session)))
			{
				return false;
			}
			if (HasNumSessionsPurchasable != tavernBrawlPlayerRecord.HasNumSessionsPurchasable || (HasNumSessionsPurchasable && !NumSessionsPurchasable.Equals(tavernBrawlPlayerRecord.NumSessionsPurchasable)))
			{
				return false;
			}
			if (HasBrawlType != tavernBrawlPlayerRecord.HasBrawlType || (HasBrawlType && !BrawlType.Equals(tavernBrawlPlayerRecord.BrawlType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlPlayerRecord Deserialize(Stream stream, TavernBrawlPlayerRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerRecord tavernBrawlPlayerRecord = new TavernBrawlPlayerRecord();
			DeserializeLengthDelimited(stream, tavernBrawlPlayerRecord);
			return tavernBrawlPlayerRecord;
		}

		public static TavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlPlayerRecord Deserialize(Stream stream, TavernBrawlPlayerRecord instance, long limit)
		{
			instance.SessionStatus = TavernBrawlStatus.TB_STATUS_INVALID;
			instance.BrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
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
					instance.RewardProgress = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GamesPlayed = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.GamesWon = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.WinStreak = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.SessionStatus = (TavernBrawlStatus)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.NumTicketsOwned = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					if (instance.Session == null)
					{
						instance.Session = TavernBrawlPlayerSession.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerSession.DeserializeLengthDelimited(stream, instance.Session);
					}
					continue;
				case 64:
					instance.NumSessionsPurchasable = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, TavernBrawlPlayerRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardProgress);
			if (instance.HasGamesPlayed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GamesPlayed);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GamesWon);
			if (instance.HasWinStreak)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.WinStreak);
			}
			if (instance.HasSessionStatus)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SessionStatus);
			}
			if (instance.HasNumTicketsOwned)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumTicketsOwned);
			}
			if (instance.HasSession)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				TavernBrawlPlayerSession.Serialize(stream, instance.Session);
			}
			if (instance.HasNumSessionsPurchasable)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumSessionsPurchasable);
			}
			if (instance.HasBrawlType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)RewardProgress);
			if (HasGamesPlayed)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GamesPlayed);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)GamesWon);
			if (HasWinStreak)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)WinStreak);
			}
			if (HasSessionStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SessionStatus);
			}
			if (HasNumTicketsOwned)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumTicketsOwned);
			}
			if (HasSession)
			{
				num++;
				uint serializedSize = Session.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasNumSessionsPurchasable)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumSessionsPurchasable);
			}
			if (HasBrawlType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlType);
			}
			return num + 2;
		}
	}
}
