using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class GetFriendsRequest : IProtoBuf
	{
		public bool HasOptions;

		private GetFriendsOptions _Options;

		public bool HasContinuation;

		private ulong _Continuation;

		public GetFriendsOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void SetOptions(GetFriendsOptions val)
		{
			Options = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFriendsRequest getFriendsRequest = obj as GetFriendsRequest;
			if (getFriendsRequest == null)
			{
				return false;
			}
			if (HasOptions != getFriendsRequest.HasOptions || (HasOptions && !Options.Equals(getFriendsRequest.Options)))
			{
				return false;
			}
			if (HasContinuation != getFriendsRequest.HasContinuation || (HasContinuation && !Continuation.Equals(getFriendsRequest.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFriendsRequest Deserialize(Stream stream, GetFriendsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsRequest getFriendsRequest = new GetFriendsRequest();
			DeserializeLengthDelimited(stream, getFriendsRequest);
			return getFriendsRequest;
		}

		public static GetFriendsRequest DeserializeLengthDelimited(Stream stream, GetFriendsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFriendsRequest Deserialize(Stream stream, GetFriendsRequest instance, long limit)
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
				case 18:
					if (instance.Options == null)
					{
						instance.Options = GetFriendsOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GetFriendsOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 24:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetFriendsRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetFriendsOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
