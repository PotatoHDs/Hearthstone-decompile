using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class ArenaSessionResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 351
		}

		public bool HasSession;

		private ArenaSession _Session;

		public bool HasCurrentSeason;

		private ArenaSeasonInfo _CurrentSeason;

		public ErrorCode ErrorCode { get; set; }

		public ArenaSession Session
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

		public ArenaSeasonInfo CurrentSeason
		{
			get
			{
				return _CurrentSeason;
			}
			set
			{
				_CurrentSeason = value;
				HasCurrentSeason = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasSession)
			{
				hashCode ^= Session.GetHashCode();
			}
			if (HasCurrentSeason)
			{
				hashCode ^= CurrentSeason.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ArenaSessionResponse arenaSessionResponse = obj as ArenaSessionResponse;
			if (arenaSessionResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(arenaSessionResponse.ErrorCode))
			{
				return false;
			}
			if (HasSession != arenaSessionResponse.HasSession || (HasSession && !Session.Equals(arenaSessionResponse.Session)))
			{
				return false;
			}
			if (HasCurrentSeason != arenaSessionResponse.HasCurrentSeason || (HasCurrentSeason && !CurrentSeason.Equals(arenaSessionResponse.CurrentSeason)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ArenaSessionResponse Deserialize(Stream stream, ArenaSessionResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ArenaSessionResponse DeserializeLengthDelimited(Stream stream)
		{
			ArenaSessionResponse arenaSessionResponse = new ArenaSessionResponse();
			DeserializeLengthDelimited(stream, arenaSessionResponse);
			return arenaSessionResponse;
		}

		public static ArenaSessionResponse DeserializeLengthDelimited(Stream stream, ArenaSessionResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ArenaSessionResponse Deserialize(Stream stream, ArenaSessionResponse instance, long limit)
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Session == null)
					{
						instance.Session = ArenaSession.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSession.DeserializeLengthDelimited(stream, instance.Session);
					}
					continue;
				case 26:
					if (instance.CurrentSeason == null)
					{
						instance.CurrentSeason = ArenaSeasonInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSeasonInfo.DeserializeLengthDelimited(stream, instance.CurrentSeason);
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

		public static void Serialize(Stream stream, ArenaSessionResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.HasSession)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.Session);
			}
			if (instance.HasCurrentSeason)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSeason.GetSerializedSize());
				ArenaSeasonInfo.Serialize(stream, instance.CurrentSeason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (HasSession)
			{
				num++;
				uint serializedSize = Session.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCurrentSeason)
			{
				num++;
				uint serializedSize2 = CurrentSeason.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
