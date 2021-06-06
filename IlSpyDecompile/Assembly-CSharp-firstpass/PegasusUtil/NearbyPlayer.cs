using System.IO;

namespace PegasusUtil
{
	public class NearbyPlayer : IProtoBuf
	{
		public ulong BnetIdHi { get; set; }

		public ulong BnetIdLo { get; set; }

		public ulong SessionStartTime { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ BnetIdHi.GetHashCode() ^ BnetIdLo.GetHashCode() ^ SessionStartTime.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			NearbyPlayer nearbyPlayer = obj as NearbyPlayer;
			if (nearbyPlayer == null)
			{
				return false;
			}
			if (!BnetIdHi.Equals(nearbyPlayer.BnetIdHi))
			{
				return false;
			}
			if (!BnetIdLo.Equals(nearbyPlayer.BnetIdLo))
			{
				return false;
			}
			if (!SessionStartTime.Equals(nearbyPlayer.SessionStartTime))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NearbyPlayer Deserialize(Stream stream, NearbyPlayer instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NearbyPlayer DeserializeLengthDelimited(Stream stream)
		{
			NearbyPlayer nearbyPlayer = new NearbyPlayer();
			DeserializeLengthDelimited(stream, nearbyPlayer);
			return nearbyPlayer;
		}

		public static NearbyPlayer DeserializeLengthDelimited(Stream stream, NearbyPlayer instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NearbyPlayer Deserialize(Stream stream, NearbyPlayer instance, long limit)
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
					instance.BnetIdHi = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.BnetIdLo = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.SessionStartTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, NearbyPlayer instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.BnetIdHi);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.BnetIdLo);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.SessionStartTime);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64(BnetIdHi) + ProtocolParser.SizeOfUInt64(BnetIdLo) + ProtocolParser.SizeOfUInt64(SessionStartTime) + 3;
		}
	}
}
