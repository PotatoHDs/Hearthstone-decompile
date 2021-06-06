using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ThirdPartyUserIdUpdated : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasValidChange;

		private bool _ValidChange;

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

		public bool ValidChange
		{
			get
			{
				return _ValidChange;
			}
			set
			{
				_ValidChange = value;
				HasValidChange = true;
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
			if (HasValidChange)
			{
				num ^= ValidChange.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ThirdPartyUserIdUpdated thirdPartyUserIdUpdated = obj as ThirdPartyUserIdUpdated;
			if (thirdPartyUserIdUpdated == null)
			{
				return false;
			}
			if (HasPlayer != thirdPartyUserIdUpdated.HasPlayer || (HasPlayer && !Player.Equals(thirdPartyUserIdUpdated.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != thirdPartyUserIdUpdated.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(thirdPartyUserIdUpdated.DeviceInfo)))
			{
				return false;
			}
			if (HasValidChange != thirdPartyUserIdUpdated.HasValidChange || (HasValidChange && !ValidChange.Equals(thirdPartyUserIdUpdated.ValidChange)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyUserIdUpdated Deserialize(Stream stream, ThirdPartyUserIdUpdated instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyUserIdUpdated DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyUserIdUpdated thirdPartyUserIdUpdated = new ThirdPartyUserIdUpdated();
			DeserializeLengthDelimited(stream, thirdPartyUserIdUpdated);
			return thirdPartyUserIdUpdated;
		}

		public static ThirdPartyUserIdUpdated DeserializeLengthDelimited(Stream stream, ThirdPartyUserIdUpdated instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyUserIdUpdated Deserialize(Stream stream, ThirdPartyUserIdUpdated instance, long limit)
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
				case 24:
					instance.ValidChange = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ThirdPartyUserIdUpdated instance)
		{
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
			if (instance.HasValidChange)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ValidChange);
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
			if (HasValidChange)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
