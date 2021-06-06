using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class ViewFriendsRequest : IProtoBuf
	{
		public bool HasTargetAccountId;

		private ulong _TargetAccountId;

		public bool HasContinuation;

		private ulong _Continuation;

		public ulong TargetAccountId
		{
			get
			{
				return _TargetAccountId;
			}
			set
			{
				_TargetAccountId = value;
				HasTargetAccountId = true;
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

		public void SetTargetAccountId(ulong val)
		{
			TargetAccountId = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetAccountId)
			{
				num ^= TargetAccountId.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ViewFriendsRequest viewFriendsRequest = obj as ViewFriendsRequest;
			if (viewFriendsRequest == null)
			{
				return false;
			}
			if (HasTargetAccountId != viewFriendsRequest.HasTargetAccountId || (HasTargetAccountId && !TargetAccountId.Equals(viewFriendsRequest.TargetAccountId)))
			{
				return false;
			}
			if (HasContinuation != viewFriendsRequest.HasContinuation || (HasContinuation && !Continuation.Equals(viewFriendsRequest.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static ViewFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsRequest viewFriendsRequest = new ViewFriendsRequest();
			DeserializeLengthDelimited(stream, viewFriendsRequest);
			return viewFriendsRequest;
		}

		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream, ViewFriendsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance, long limit)
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
				case 16:
					instance.TargetAccountId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ViewFriendsRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
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
			if (HasTargetAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(TargetAccountId);
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
