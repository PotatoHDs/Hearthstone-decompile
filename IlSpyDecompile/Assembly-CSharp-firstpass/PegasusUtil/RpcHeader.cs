using System.IO;

namespace PegasusUtil
{
	public class RpcHeader : IProtoBuf
	{
		public bool HasRetryCount;

		private ulong _RetryCount;

		public bool HasRequestNotHandledCount;

		private ulong _RequestNotHandledCount;

		public ulong Type { get; set; }

		public ulong RetryCount
		{
			get
			{
				return _RetryCount;
			}
			set
			{
				_RetryCount = value;
				HasRetryCount = true;
			}
		}

		public ulong RequestNotHandledCount
		{
			get
			{
				return _RequestNotHandledCount;
			}
			set
			{
				_RequestNotHandledCount = value;
				HasRequestNotHandledCount = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			if (HasRetryCount)
			{
				hashCode ^= RetryCount.GetHashCode();
			}
			if (HasRequestNotHandledCount)
			{
				hashCode ^= RequestNotHandledCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RpcHeader rpcHeader = obj as RpcHeader;
			if (rpcHeader == null)
			{
				return false;
			}
			if (!Type.Equals(rpcHeader.Type))
			{
				return false;
			}
			if (HasRetryCount != rpcHeader.HasRetryCount || (HasRetryCount && !RetryCount.Equals(rpcHeader.RetryCount)))
			{
				return false;
			}
			if (HasRequestNotHandledCount != rpcHeader.HasRequestNotHandledCount || (HasRequestNotHandledCount && !RequestNotHandledCount.Equals(rpcHeader.RequestNotHandledCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RpcHeader Deserialize(Stream stream, RpcHeader instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RpcHeader DeserializeLengthDelimited(Stream stream)
		{
			RpcHeader rpcHeader = new RpcHeader();
			DeserializeLengthDelimited(stream, rpcHeader);
			return rpcHeader;
		}

		public static RpcHeader DeserializeLengthDelimited(Stream stream, RpcHeader instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RpcHeader Deserialize(Stream stream, RpcHeader instance, long limit)
		{
			instance.RetryCount = 0uL;
			instance.RequestNotHandledCount = 0uL;
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
					instance.Type = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.RetryCount = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.RequestNotHandledCount = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RpcHeader instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.Type);
			if (instance.HasRetryCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.RetryCount);
			}
			if (instance.HasRequestNotHandledCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequestNotHandledCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64(Type);
			if (HasRetryCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(RetryCount);
			}
			if (HasRequestNotHandledCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(RequestNotHandledCount);
			}
			return num + 1;
		}
	}
}
