using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ClientReset : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasForceLogin;

		private bool _ForceLogin;

		public bool HasForceNoAccountTutorial;

		private bool _ForceNoAccountTutorial;

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

		public bool ForceLogin
		{
			get
			{
				return _ForceLogin;
			}
			set
			{
				_ForceLogin = value;
				HasForceLogin = true;
			}
		}

		public bool ForceNoAccountTutorial
		{
			get
			{
				return _ForceNoAccountTutorial;
			}
			set
			{
				_ForceNoAccountTutorial = value;
				HasForceNoAccountTutorial = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasForceLogin)
			{
				num ^= ForceLogin.GetHashCode();
			}
			if (HasForceNoAccountTutorial)
			{
				num ^= ForceNoAccountTutorial.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientReset clientReset = obj as ClientReset;
			if (clientReset == null)
			{
				return false;
			}
			if (HasDeviceInfo != clientReset.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(clientReset.DeviceInfo)))
			{
				return false;
			}
			if (HasForceLogin != clientReset.HasForceLogin || (HasForceLogin && !ForceLogin.Equals(clientReset.ForceLogin)))
			{
				return false;
			}
			if (HasForceNoAccountTutorial != clientReset.HasForceNoAccountTutorial || (HasForceNoAccountTutorial && !ForceNoAccountTutorial.Equals(clientReset.ForceNoAccountTutorial)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientReset Deserialize(Stream stream, ClientReset instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientReset DeserializeLengthDelimited(Stream stream)
		{
			ClientReset clientReset = new ClientReset();
			DeserializeLengthDelimited(stream, clientReset);
			return clientReset;
		}

		public static ClientReset DeserializeLengthDelimited(Stream stream, ClientReset instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientReset Deserialize(Stream stream, ClientReset instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 16:
					instance.ForceLogin = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.ForceNoAccountTutorial = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ClientReset instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasForceLogin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ForceLogin);
			}
			if (instance.HasForceNoAccountTutorial)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ForceNoAccountTutorial);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasForceLogin)
			{
				num++;
				num++;
			}
			if (HasForceNoAccountTutorial)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
