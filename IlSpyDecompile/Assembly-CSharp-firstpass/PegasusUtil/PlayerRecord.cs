using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class PlayerRecord : IProtoBuf
	{
		public bool HasData;

		private int _Data;

		public bool HasTies;

		private int _Ties;

		public GameType Type { get; set; }

		public int Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = true;
			}
		}

		public int Wins { get; set; }

		public int Losses { get; set; }

		public int Ties
		{
			get
			{
				return _Ties;
			}
			set
			{
				_Ties = value;
				HasTies = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			if (HasData)
			{
				hashCode ^= Data.GetHashCode();
			}
			hashCode ^= Wins.GetHashCode();
			hashCode ^= Losses.GetHashCode();
			if (HasTies)
			{
				hashCode ^= Ties.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PlayerRecord playerRecord = obj as PlayerRecord;
			if (playerRecord == null)
			{
				return false;
			}
			if (!Type.Equals(playerRecord.Type))
			{
				return false;
			}
			if (HasData != playerRecord.HasData || (HasData && !Data.Equals(playerRecord.Data)))
			{
				return false;
			}
			if (!Wins.Equals(playerRecord.Wins))
			{
				return false;
			}
			if (!Losses.Equals(playerRecord.Losses))
			{
				return false;
			}
			if (HasTies != playerRecord.HasTies || (HasTies && !Ties.Equals(playerRecord.Ties)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerRecord Deserialize(Stream stream, PlayerRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			PlayerRecord playerRecord = new PlayerRecord();
			DeserializeLengthDelimited(stream, playerRecord);
			return playerRecord;
		}

		public static PlayerRecord DeserializeLengthDelimited(Stream stream, PlayerRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerRecord Deserialize(Stream stream, PlayerRecord instance, long limit)
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
					instance.Type = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Data = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Ties = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PlayerRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			if (instance.HasData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Wins);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Losses);
			if (instance.HasTies)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Ties);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type);
			if (HasData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Data);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)Wins);
			num += ProtocolParser.SizeOfUInt64((ulong)Losses);
			if (HasTies)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Ties);
			}
			return num + 3;
		}
	}
}
