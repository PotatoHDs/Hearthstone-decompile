using System.IO;

namespace bnet.protocol.account.v1
{
	public class RegisterAccountRequest : IProtoBuf
	{
		public bool HasIdentity;

		private Identity _Identity;

		public Identity Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(Identity val)
		{
			Identity = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegisterAccountRequest registerAccountRequest = obj as RegisterAccountRequest;
			if (registerAccountRequest == null)
			{
				return false;
			}
			if (HasIdentity != registerAccountRequest.HasIdentity || (HasIdentity && !Identity.Equals(registerAccountRequest.Identity)))
			{
				return false;
			}
			return true;
		}

		public static RegisterAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterAccountRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterAccountRequest Deserialize(Stream stream, RegisterAccountRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterAccountRequest registerAccountRequest = new RegisterAccountRequest();
			DeserializeLengthDelimited(stream, registerAccountRequest);
			return registerAccountRequest;
		}

		public static RegisterAccountRequest DeserializeLengthDelimited(Stream stream, RegisterAccountRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterAccountRequest Deserialize(Stream stream, RegisterAccountRequest instance, long limit)
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
					if (instance.Identity == null)
					{
						instance.Identity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Identity);
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

		public static void Serialize(Stream stream, RegisterAccountRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
