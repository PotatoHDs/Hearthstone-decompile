using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class SmartDeckCompleteFailed : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasRequestMessageSize;

		private int _RequestMessageSize;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public int RequestMessageSize
		{
			get
			{
				return _RequestMessageSize;
			}
			set
			{
				_RequestMessageSize = value;
				HasRequestMessageSize = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasRequestMessageSize)
			{
				num ^= RequestMessageSize.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SmartDeckCompleteFailed smartDeckCompleteFailed = obj as SmartDeckCompleteFailed;
			if (smartDeckCompleteFailed == null)
			{
				return false;
			}
			if (HasPlayer != smartDeckCompleteFailed.HasPlayer || (HasPlayer && !Player.Equals(smartDeckCompleteFailed.Player)))
			{
				return false;
			}
			if (HasRequestMessageSize != smartDeckCompleteFailed.HasRequestMessageSize || (HasRequestMessageSize && !RequestMessageSize.Equals(smartDeckCompleteFailed.RequestMessageSize)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SmartDeckCompleteFailed Deserialize(Stream stream, SmartDeckCompleteFailed instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SmartDeckCompleteFailed DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckCompleteFailed smartDeckCompleteFailed = new SmartDeckCompleteFailed();
			DeserializeLengthDelimited(stream, smartDeckCompleteFailed);
			return smartDeckCompleteFailed;
		}

		public static SmartDeckCompleteFailed DeserializeLengthDelimited(Stream stream, SmartDeckCompleteFailed instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SmartDeckCompleteFailed Deserialize(Stream stream, SmartDeckCompleteFailed instance, long limit)
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 16:
					instance.RequestMessageSize = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SmartDeckCompleteFailed instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasRequestMessageSize)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestMessageSize);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRequestMessageSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestMessageSize);
			}
			return num;
		}
	}
}
