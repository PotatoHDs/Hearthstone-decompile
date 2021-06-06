using System;
using System.IO;

namespace PegasusUtil
{
	public class TriggerPlayedNearbyPlayerOnSubnet : IProtoBuf
	{
		public enum PacketID
		{
			ID = 298,
			System = 0
		}

		public NearbyPlayer LastPlayed { get; set; }

		public NearbyPlayer OtherPlayer { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ LastPlayed.GetHashCode() ^ OtherPlayer.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			TriggerPlayedNearbyPlayerOnSubnet triggerPlayedNearbyPlayerOnSubnet = obj as TriggerPlayedNearbyPlayerOnSubnet;
			if (triggerPlayedNearbyPlayerOnSubnet == null)
			{
				return false;
			}
			if (!LastPlayed.Equals(triggerPlayedNearbyPlayerOnSubnet.LastPlayed))
			{
				return false;
			}
			if (!OtherPlayer.Equals(triggerPlayedNearbyPlayerOnSubnet.OtherPlayer))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TriggerPlayedNearbyPlayerOnSubnet Deserialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TriggerPlayedNearbyPlayerOnSubnet DeserializeLengthDelimited(Stream stream)
		{
			TriggerPlayedNearbyPlayerOnSubnet triggerPlayedNearbyPlayerOnSubnet = new TriggerPlayedNearbyPlayerOnSubnet();
			DeserializeLengthDelimited(stream, triggerPlayedNearbyPlayerOnSubnet);
			return triggerPlayedNearbyPlayerOnSubnet;
		}

		public static TriggerPlayedNearbyPlayerOnSubnet DeserializeLengthDelimited(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TriggerPlayedNearbyPlayerOnSubnet Deserialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance, long limit)
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
					if (instance.LastPlayed == null)
					{
						instance.LastPlayed = NearbyPlayer.DeserializeLengthDelimited(stream);
					}
					else
					{
						NearbyPlayer.DeserializeLengthDelimited(stream, instance.LastPlayed);
					}
					continue;
				case 18:
					if (instance.OtherPlayer == null)
					{
						instance.OtherPlayer = NearbyPlayer.DeserializeLengthDelimited(stream);
					}
					else
					{
						NearbyPlayer.DeserializeLengthDelimited(stream, instance.OtherPlayer);
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

		public static void Serialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			if (instance.LastPlayed == null)
			{
				throw new ArgumentNullException("LastPlayed", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.LastPlayed.GetSerializedSize());
			NearbyPlayer.Serialize(stream, instance.LastPlayed);
			if (instance.OtherPlayer == null)
			{
				throw new ArgumentNullException("OtherPlayer", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.OtherPlayer.GetSerializedSize());
			NearbyPlayer.Serialize(stream, instance.OtherPlayer);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = LastPlayed.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = OtherPlayer.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
