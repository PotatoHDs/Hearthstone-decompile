using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class IsFriendResponse : IProtoBuf
	{
		public bool HasResult;

		private bool _Result;

		public bool Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public bool IsInitialized => true;

		public void SetResult(bool val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IsFriendResponse isFriendResponse = obj as IsFriendResponse;
			if (isFriendResponse == null)
			{
				return false;
			}
			if (HasResult != isFriendResponse.HasResult || (HasResult && !Result.Equals(isFriendResponse.Result)))
			{
				return false;
			}
			return true;
		}

		public static IsFriendResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsFriendResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IsFriendResponse Deserialize(Stream stream, IsFriendResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IsFriendResponse DeserializeLengthDelimited(Stream stream)
		{
			IsFriendResponse isFriendResponse = new IsFriendResponse();
			DeserializeLengthDelimited(stream, isFriendResponse);
			return isFriendResponse;
		}

		public static IsFriendResponse DeserializeLengthDelimited(Stream stream, IsFriendResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IsFriendResponse Deserialize(Stream stream, IsFriendResponse instance, long limit)
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
				case 8:
					instance.Result = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, IsFriendResponse instance)
		{
			if (instance.HasResult)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Result);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasResult)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
