using System.IO;

namespace bnet.protocol.account.v1
{
	public class ResolveAccountRequest : IProtoBuf
	{
		public bool HasRef;

		private AccountReference _Ref;

		public bool HasFetchId;

		private bool _FetchId;

		public AccountReference Ref
		{
			get
			{
				return _Ref;
			}
			set
			{
				_Ref = value;
				HasRef = value != null;
			}
		}

		public bool FetchId
		{
			get
			{
				return _FetchId;
			}
			set
			{
				_FetchId = value;
				HasFetchId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRef(AccountReference val)
		{
			Ref = val;
		}

		public void SetFetchId(bool val)
		{
			FetchId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRef)
			{
				num ^= Ref.GetHashCode();
			}
			if (HasFetchId)
			{
				num ^= FetchId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ResolveAccountRequest resolveAccountRequest = obj as ResolveAccountRequest;
			if (resolveAccountRequest == null)
			{
				return false;
			}
			if (HasRef != resolveAccountRequest.HasRef || (HasRef && !Ref.Equals(resolveAccountRequest.Ref)))
			{
				return false;
			}
			if (HasFetchId != resolveAccountRequest.HasFetchId || (HasFetchId && !FetchId.Equals(resolveAccountRequest.FetchId)))
			{
				return false;
			}
			return true;
		}

		public static ResolveAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ResolveAccountRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ResolveAccountRequest Deserialize(Stream stream, ResolveAccountRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ResolveAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			ResolveAccountRequest resolveAccountRequest = new ResolveAccountRequest();
			DeserializeLengthDelimited(stream, resolveAccountRequest);
			return resolveAccountRequest;
		}

		public static ResolveAccountRequest DeserializeLengthDelimited(Stream stream, ResolveAccountRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ResolveAccountRequest Deserialize(Stream stream, ResolveAccountRequest instance, long limit)
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
					if (instance.Ref == null)
					{
						instance.Ref = AccountReference.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountReference.DeserializeLengthDelimited(stream, instance.Ref);
					}
					continue;
				case 96:
					instance.FetchId = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ResolveAccountRequest instance)
		{
			if (instance.HasRef)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Ref.GetSerializedSize());
				AccountReference.Serialize(stream, instance.Ref);
			}
			if (instance.HasFetchId)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.FetchId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRef)
			{
				num++;
				uint serializedSize = Ref.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFetchId)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
