using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	public class ClientInfo : IProtoBuf
	{
		public bool HasClientAddress;

		private string _ClientAddress;

		public bool HasPrivilegedNetwork;

		private bool _PrivilegedNetwork;

		public string ClientAddress
		{
			get
			{
				return _ClientAddress;
			}
			set
			{
				_ClientAddress = value;
				HasClientAddress = value != null;
			}
		}

		public bool PrivilegedNetwork
		{
			get
			{
				return _PrivilegedNetwork;
			}
			set
			{
				_PrivilegedNetwork = value;
				HasPrivilegedNetwork = true;
			}
		}

		public bool IsInitialized => true;

		public void SetClientAddress(string val)
		{
			ClientAddress = val;
		}

		public void SetPrivilegedNetwork(bool val)
		{
			PrivilegedNetwork = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientAddress)
			{
				num ^= ClientAddress.GetHashCode();
			}
			if (HasPrivilegedNetwork)
			{
				num ^= PrivilegedNetwork.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientInfo clientInfo = obj as ClientInfo;
			if (clientInfo == null)
			{
				return false;
			}
			if (HasClientAddress != clientInfo.HasClientAddress || (HasClientAddress && !ClientAddress.Equals(clientInfo.ClientAddress)))
			{
				return false;
			}
			if (HasPrivilegedNetwork != clientInfo.HasPrivilegedNetwork || (HasPrivilegedNetwork && !PrivilegedNetwork.Equals(clientInfo.PrivilegedNetwork)))
			{
				return false;
			}
			return true;
		}

		public static ClientInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientInfo Deserialize(Stream stream, ClientInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientInfo DeserializeLengthDelimited(Stream stream)
		{
			ClientInfo clientInfo = new ClientInfo();
			DeserializeLengthDelimited(stream, clientInfo);
			return clientInfo;
		}

		public static ClientInfo DeserializeLengthDelimited(Stream stream, ClientInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientInfo Deserialize(Stream stream, ClientInfo instance, long limit)
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
					instance.ClientAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.PrivilegedNetwork = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ClientInfo instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasPrivilegedNetwork)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.PrivilegedNetwork);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPrivilegedNetwork)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
