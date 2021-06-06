using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetGameStatsBucketsRequest : IProtoBuf
	{
		public bool HasFactoryFilter;

		private AttributeFilter _FactoryFilter;

		public bool HasGameFilter;

		private AttributeFilter _GameFilter;

		public AttributeFilter FactoryFilter
		{
			get
			{
				return _FactoryFilter;
			}
			set
			{
				_FactoryFilter = value;
				HasFactoryFilter = value != null;
			}
		}

		public AttributeFilter GameFilter
		{
			get
			{
				return _GameFilter;
			}
			set
			{
				_GameFilter = value;
				HasGameFilter = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetFactoryFilter(AttributeFilter val)
		{
			FactoryFilter = val;
		}

		public void SetGameFilter(AttributeFilter val)
		{
			GameFilter = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFactoryFilter)
			{
				num ^= FactoryFilter.GetHashCode();
			}
			if (HasGameFilter)
			{
				num ^= GameFilter.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameStatsBucketsRequest getGameStatsBucketsRequest = obj as GetGameStatsBucketsRequest;
			if (getGameStatsBucketsRequest == null)
			{
				return false;
			}
			if (HasFactoryFilter != getGameStatsBucketsRequest.HasFactoryFilter || (HasFactoryFilter && !FactoryFilter.Equals(getGameStatsBucketsRequest.FactoryFilter)))
			{
				return false;
			}
			if (HasGameFilter != getGameStatsBucketsRequest.HasGameFilter || (HasGameFilter && !GameFilter.Equals(getGameStatsBucketsRequest.GameFilter)))
			{
				return false;
			}
			return true;
		}

		public static GetGameStatsBucketsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsBucketsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameStatsBucketsRequest Deserialize(Stream stream, GetGameStatsBucketsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameStatsBucketsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsBucketsRequest getGameStatsBucketsRequest = new GetGameStatsBucketsRequest();
			DeserializeLengthDelimited(stream, getGameStatsBucketsRequest);
			return getGameStatsBucketsRequest;
		}

		public static GetGameStatsBucketsRequest DeserializeLengthDelimited(Stream stream, GetGameStatsBucketsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameStatsBucketsRequest Deserialize(Stream stream, GetGameStatsBucketsRequest instance, long limit)
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
					if (instance.FactoryFilter == null)
					{
						instance.FactoryFilter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.FactoryFilter);
					}
					continue;
				case 18:
					if (instance.GameFilter == null)
					{
						instance.GameFilter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.GameFilter);
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

		public static void Serialize(Stream stream, GetGameStatsBucketsRequest instance)
		{
			if (instance.HasFactoryFilter)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FactoryFilter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.FactoryFilter);
			}
			if (instance.HasGameFilter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameFilter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.GameFilter);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFactoryFilter)
			{
				num++;
				uint serializedSize = FactoryFilter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameFilter)
			{
				num++;
				uint serializedSize2 = GameFilter.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
