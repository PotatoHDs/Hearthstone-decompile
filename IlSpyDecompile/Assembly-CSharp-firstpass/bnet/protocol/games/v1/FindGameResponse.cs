using System.IO;

namespace bnet.protocol.games.v1
{
	public class FindGameResponse : IProtoBuf
	{
		public bool HasRequestId;

		private ulong _RequestId;

		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasQueued;

		private bool _Queued;

		public ulong RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = true;
			}
		}

		public ulong FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = true;
			}
		}

		public bool Queued
		{
			get
			{
				return _Queued;
			}
			set
			{
				_Queued = value;
				HasQueued = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(ulong val)
		{
			RequestId = val;
		}

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetQueued(bool val)
		{
			Queued = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasQueued)
			{
				num ^= Queued.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindGameResponse findGameResponse = obj as FindGameResponse;
			if (findGameResponse == null)
			{
				return false;
			}
			if (HasRequestId != findGameResponse.HasRequestId || (HasRequestId && !RequestId.Equals(findGameResponse.RequestId)))
			{
				return false;
			}
			if (HasFactoryId != findGameResponse.HasFactoryId || (HasFactoryId && !FactoryId.Equals(findGameResponse.FactoryId)))
			{
				return false;
			}
			if (HasQueued != findGameResponse.HasQueued || (HasQueued && !Queued.Equals(findGameResponse.Queued)))
			{
				return false;
			}
			return true;
		}

		public static FindGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FindGameResponse DeserializeLengthDelimited(Stream stream)
		{
			FindGameResponse findGameResponse = new FindGameResponse();
			DeserializeLengthDelimited(stream, findGameResponse);
			return findGameResponse;
		}

		public static FindGameResponse DeserializeLengthDelimited(Stream stream, FindGameResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
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
				case 9:
					instance.RequestId = binaryReader.ReadUInt64();
					continue;
				case 17:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 24:
					instance.Queued = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, FindGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Queued);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestId)
			{
				num++;
				num += 8;
			}
			if (HasFactoryId)
			{
				num++;
				num += 8;
			}
			if (HasQueued)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
