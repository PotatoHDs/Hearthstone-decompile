using System.IO;

namespace bnet.protocol.session.v1
{
	public class RefreshSessionKeyRequest : IProtoBuf
	{
		public bool HasSessionKey;

		private byte[] _SessionKey;

		public byte[] SessionKey
		{
			get
			{
				return _SessionKey;
			}
			set
			{
				_SessionKey = value;
				HasSessionKey = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSessionKey(byte[] val)
		{
			SessionKey = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSessionKey)
			{
				num ^= SessionKey.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RefreshSessionKeyRequest refreshSessionKeyRequest = obj as RefreshSessionKeyRequest;
			if (refreshSessionKeyRequest == null)
			{
				return false;
			}
			if (HasSessionKey != refreshSessionKeyRequest.HasSessionKey || (HasSessionKey && !SessionKey.Equals(refreshSessionKeyRequest.SessionKey)))
			{
				return false;
			}
			return true;
		}

		public static RefreshSessionKeyRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RefreshSessionKeyRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RefreshSessionKeyRequest Deserialize(Stream stream, RefreshSessionKeyRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RefreshSessionKeyRequest DeserializeLengthDelimited(Stream stream)
		{
			RefreshSessionKeyRequest refreshSessionKeyRequest = new RefreshSessionKeyRequest();
			DeserializeLengthDelimited(stream, refreshSessionKeyRequest);
			return refreshSessionKeyRequest;
		}

		public static RefreshSessionKeyRequest DeserializeLengthDelimited(Stream stream, RefreshSessionKeyRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RefreshSessionKeyRequest Deserialize(Stream stream, RefreshSessionKeyRequest instance, long limit)
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
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, RefreshSessionKeyRequest instance)
		{
			if (instance.HasSessionKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSessionKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SessionKey.Length) + SessionKey.Length);
			}
			return num;
		}
	}
}
