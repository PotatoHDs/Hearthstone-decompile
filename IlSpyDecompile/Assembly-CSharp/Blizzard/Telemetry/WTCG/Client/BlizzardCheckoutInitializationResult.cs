using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class BlizzardCheckoutInitializationResult : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasSuccess;

		private bool _Success;

		public bool HasFailureReason;

		private string _FailureReason;

		public bool HasFailureDetails;

		private string _FailureDetails;

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

		public bool Success
		{
			get
			{
				return _Success;
			}
			set
			{
				_Success = value;
				HasSuccess = true;
			}
		}

		public string FailureReason
		{
			get
			{
				return _FailureReason;
			}
			set
			{
				_FailureReason = value;
				HasFailureReason = value != null;
			}
		}

		public string FailureDetails
		{
			get
			{
				return _FailureDetails;
			}
			set
			{
				_FailureDetails = value;
				HasFailureDetails = value != null;
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
			if (HasSuccess)
			{
				num ^= Success.GetHashCode();
			}
			if (HasFailureReason)
			{
				num ^= FailureReason.GetHashCode();
			}
			if (HasFailureDetails)
			{
				num ^= FailureDetails.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BlizzardCheckoutInitializationResult blizzardCheckoutInitializationResult = obj as BlizzardCheckoutInitializationResult;
			if (blizzardCheckoutInitializationResult == null)
			{
				return false;
			}
			if (HasPlayer != blizzardCheckoutInitializationResult.HasPlayer || (HasPlayer && !Player.Equals(blizzardCheckoutInitializationResult.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != blizzardCheckoutInitializationResult.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(blizzardCheckoutInitializationResult.DeviceInfo)))
			{
				return false;
			}
			if (HasSuccess != blizzardCheckoutInitializationResult.HasSuccess || (HasSuccess && !Success.Equals(blizzardCheckoutInitializationResult.Success)))
			{
				return false;
			}
			if (HasFailureReason != blizzardCheckoutInitializationResult.HasFailureReason || (HasFailureReason && !FailureReason.Equals(blizzardCheckoutInitializationResult.FailureReason)))
			{
				return false;
			}
			if (HasFailureDetails != blizzardCheckoutInitializationResult.HasFailureDetails || (HasFailureDetails && !FailureDetails.Equals(blizzardCheckoutInitializationResult.FailureDetails)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlizzardCheckoutInitializationResult Deserialize(Stream stream, BlizzardCheckoutInitializationResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlizzardCheckoutInitializationResult DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutInitializationResult blizzardCheckoutInitializationResult = new BlizzardCheckoutInitializationResult();
			DeserializeLengthDelimited(stream, blizzardCheckoutInitializationResult);
			return blizzardCheckoutInitializationResult;
		}

		public static BlizzardCheckoutInitializationResult DeserializeLengthDelimited(Stream stream, BlizzardCheckoutInitializationResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlizzardCheckoutInitializationResult Deserialize(Stream stream, BlizzardCheckoutInitializationResult instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 34:
					instance.FailureReason = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.FailureDetails = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, BlizzardCheckoutInitializationResult instance)
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
			if (instance.HasSuccess)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
			if (instance.HasFailureReason)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FailureReason));
			}
			if (instance.HasFailureDetails)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FailureDetails));
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
			if (HasSuccess)
			{
				num++;
				num++;
			}
			if (HasFailureReason)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FailureReason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasFailureDetails)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FailureDetails);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
