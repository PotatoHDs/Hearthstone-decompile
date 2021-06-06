using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeepLinkExecuted : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDeepLink;

		private string _DeepLink;

		public bool HasSource;

		private string _Source;

		public bool HasCompleted;

		private bool _Completed;

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

		public string DeepLink
		{
			get
			{
				return _DeepLink;
			}
			set
			{
				_DeepLink = value;
				HasDeepLink = value != null;
			}
		}

		public string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				_Source = value;
				HasSource = value != null;
			}
		}

		public bool Completed
		{
			get
			{
				return _Completed;
			}
			set
			{
				_Completed = value;
				HasCompleted = true;
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
			if (HasDeepLink)
			{
				num ^= DeepLink.GetHashCode();
			}
			if (HasSource)
			{
				num ^= Source.GetHashCode();
			}
			if (HasCompleted)
			{
				num ^= Completed.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeepLinkExecuted deepLinkExecuted = obj as DeepLinkExecuted;
			if (deepLinkExecuted == null)
			{
				return false;
			}
			if (HasPlayer != deepLinkExecuted.HasPlayer || (HasPlayer && !Player.Equals(deepLinkExecuted.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != deepLinkExecuted.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(deepLinkExecuted.DeviceInfo)))
			{
				return false;
			}
			if (HasDeepLink != deepLinkExecuted.HasDeepLink || (HasDeepLink && !DeepLink.Equals(deepLinkExecuted.DeepLink)))
			{
				return false;
			}
			if (HasSource != deepLinkExecuted.HasSource || (HasSource && !Source.Equals(deepLinkExecuted.Source)))
			{
				return false;
			}
			if (HasCompleted != deepLinkExecuted.HasCompleted || (HasCompleted && !Completed.Equals(deepLinkExecuted.Completed)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeepLinkExecuted Deserialize(Stream stream, DeepLinkExecuted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeepLinkExecuted DeserializeLengthDelimited(Stream stream)
		{
			DeepLinkExecuted deepLinkExecuted = new DeepLinkExecuted();
			DeserializeLengthDelimited(stream, deepLinkExecuted);
			return deepLinkExecuted;
		}

		public static DeepLinkExecuted DeserializeLengthDelimited(Stream stream, DeepLinkExecuted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeepLinkExecuted Deserialize(Stream stream, DeepLinkExecuted instance, long limit)
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
				case 26:
					instance.DeepLink = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Source = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.Completed = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, DeepLinkExecuted instance)
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
			if (instance.HasDeepLink)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeepLink));
			}
			if (instance.HasSource)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Source));
			}
			if (instance.HasCompleted)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Completed);
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
			if (HasDeepLink)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeepLink);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasSource)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Source);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasCompleted)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
