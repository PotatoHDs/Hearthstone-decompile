using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class BlizzardCheckoutIsReady : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasSecondsShown;

		private double _SecondsShown;

		public bool HasIsReady;

		private bool _IsReady;

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

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public double SecondsShown
		{
			get
			{
				return _SecondsShown;
			}
			set
			{
				_SecondsShown = value;
				HasSecondsShown = true;
			}
		}

		public bool IsReady
		{
			get
			{
				return _IsReady;
			}
			set
			{
				_IsReady = value;
				HasIsReady = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasSecondsShown)
			{
				num ^= SecondsShown.GetHashCode();
			}
			if (HasIsReady)
			{
				num ^= IsReady.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BlizzardCheckoutIsReady blizzardCheckoutIsReady = obj as BlizzardCheckoutIsReady;
			if (blizzardCheckoutIsReady == null)
			{
				return false;
			}
			if (HasPlayer != blizzardCheckoutIsReady.HasPlayer || (HasPlayer && !Player.Equals(blizzardCheckoutIsReady.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != blizzardCheckoutIsReady.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(blizzardCheckoutIsReady.DeviceInfo)))
			{
				return false;
			}
			if (HasSecondsShown != blizzardCheckoutIsReady.HasSecondsShown || (HasSecondsShown && !SecondsShown.Equals(blizzardCheckoutIsReady.SecondsShown)))
			{
				return false;
			}
			if (HasIsReady != blizzardCheckoutIsReady.HasIsReady || (HasIsReady && !IsReady.Equals(blizzardCheckoutIsReady.IsReady)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlizzardCheckoutIsReady Deserialize(Stream stream, BlizzardCheckoutIsReady instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlizzardCheckoutIsReady DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutIsReady blizzardCheckoutIsReady = new BlizzardCheckoutIsReady();
			DeserializeLengthDelimited(stream, blizzardCheckoutIsReady);
			return blizzardCheckoutIsReady;
		}

		public static BlizzardCheckoutIsReady DeserializeLengthDelimited(Stream stream, BlizzardCheckoutIsReady instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlizzardCheckoutIsReady Deserialize(Stream stream, BlizzardCheckoutIsReady instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 25:
					instance.SecondsShown = binaryReader.ReadDouble();
					continue;
				case 32:
					instance.IsReady = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, BlizzardCheckoutIsReady instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasSecondsShown)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.SecondsShown);
			}
			if (instance.HasIsReady)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsReady);
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
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize2 = DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSecondsShown)
			{
				num++;
				num += 8;
			}
			if (HasIsReady)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
