using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class GameToConnectNotification : IProtoBuf
	{
		public enum PacketID
		{
			ID = 363
		}

		public GameConnectionInfo Info { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Info.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameToConnectNotification gameToConnectNotification = obj as GameToConnectNotification;
			if (gameToConnectNotification == null)
			{
				return false;
			}
			if (!Info.Equals(gameToConnectNotification.Info))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameToConnectNotification Deserialize(Stream stream, GameToConnectNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameToConnectNotification DeserializeLengthDelimited(Stream stream)
		{
			GameToConnectNotification gameToConnectNotification = new GameToConnectNotification();
			DeserializeLengthDelimited(stream, gameToConnectNotification);
			return gameToConnectNotification;
		}

		public static GameToConnectNotification DeserializeLengthDelimited(Stream stream, GameToConnectNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameToConnectNotification Deserialize(Stream stream, GameToConnectNotification instance, long limit)
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
					if (instance.Info == null)
					{
						instance.Info = GameConnectionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameConnectionInfo.DeserializeLengthDelimited(stream, instance.Info);
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

		public static void Serialize(Stream stream, GameToConnectNotification instance)
		{
			if (instance.Info == null)
			{
				throw new ArgumentNullException("Info", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
			GameConnectionInfo.Serialize(stream, instance.Info);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Info.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
