using System.IO;

namespace PegasusShared
{
	public class TavernBrawlPlayerSession : IProtoBuf
	{
		public bool HasErrorCode;

		private ErrorCode _ErrorCode;

		public bool HasChest;

		private RewardChest _Chest;

		public bool HasSessionCount;

		private uint _SessionCount;

		public ErrorCode ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public int SeasonId { get; set; }

		public int Wins { get; set; }

		public int Losses { get; set; }

		public RewardChest Chest
		{
			get
			{
				return _Chest;
			}
			set
			{
				_Chest = value;
				HasChest = value != null;
			}
		}

		public bool DeckLocked { get; set; }

		public uint SessionCount
		{
			get
			{
				return _SessionCount;
			}
			set
			{
				_SessionCount = value;
				HasSessionCount = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			num ^= SeasonId.GetHashCode();
			num ^= Wins.GetHashCode();
			num ^= Losses.GetHashCode();
			if (HasChest)
			{
				num ^= Chest.GetHashCode();
			}
			num ^= DeckLocked.GetHashCode();
			if (HasSessionCount)
			{
				num ^= SessionCount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlPlayerSession tavernBrawlPlayerSession = obj as TavernBrawlPlayerSession;
			if (tavernBrawlPlayerSession == null)
			{
				return false;
			}
			if (HasErrorCode != tavernBrawlPlayerSession.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(tavernBrawlPlayerSession.ErrorCode)))
			{
				return false;
			}
			if (!SeasonId.Equals(tavernBrawlPlayerSession.SeasonId))
			{
				return false;
			}
			if (!Wins.Equals(tavernBrawlPlayerSession.Wins))
			{
				return false;
			}
			if (!Losses.Equals(tavernBrawlPlayerSession.Losses))
			{
				return false;
			}
			if (HasChest != tavernBrawlPlayerSession.HasChest || (HasChest && !Chest.Equals(tavernBrawlPlayerSession.Chest)))
			{
				return false;
			}
			if (!DeckLocked.Equals(tavernBrawlPlayerSession.DeckLocked))
			{
				return false;
			}
			if (HasSessionCount != tavernBrawlPlayerSession.HasSessionCount || (HasSessionCount && !SessionCount.Equals(tavernBrawlPlayerSession.SessionCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlPlayerSession Deserialize(Stream stream, TavernBrawlPlayerSession instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlPlayerSession DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerSession tavernBrawlPlayerSession = new TavernBrawlPlayerSession();
			DeserializeLengthDelimited(stream, tavernBrawlPlayerSession);
			return tavernBrawlPlayerSession;
		}

		public static TavernBrawlPlayerSession DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerSession instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlPlayerSession Deserialize(Stream stream, TavernBrawlPlayerSession instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					if (instance.Chest == null)
					{
						instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
					}
					continue;
				case 48:
					instance.DeckLocked = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.SessionCount = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, TavernBrawlPlayerSession instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Wins);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Losses);
			if (instance.HasChest)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.DeckLocked);
			if (instance.HasSessionCount)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.SessionCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			num += ProtocolParser.SizeOfUInt64((ulong)Wins);
			num += ProtocolParser.SizeOfUInt64((ulong)Losses);
			if (HasChest)
			{
				num++;
				uint serializedSize = Chest.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num++;
			if (HasSessionCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(SessionCount);
			}
			return num + 4;
		}
	}
}
