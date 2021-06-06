using System.IO;

namespace bnet.protocol.account.v1
{
	public class RegionTag : IProtoBuf
	{
		public bool HasRegion;

		private uint _Region;

		public bool HasTag;

		private uint _Tag;

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public uint Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
				HasTag = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public void SetTag(uint val)
		{
			Tag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			if (HasTag)
			{
				num ^= Tag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegionTag regionTag = obj as RegionTag;
			if (regionTag == null)
			{
				return false;
			}
			if (HasRegion != regionTag.HasRegion || (HasRegion && !Region.Equals(regionTag.Region)))
			{
				return false;
			}
			if (HasTag != regionTag.HasTag || (HasTag && !Tag.Equals(regionTag.Tag)))
			{
				return false;
			}
			return true;
		}

		public static RegionTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegionTag>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegionTag Deserialize(Stream stream, RegionTag instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegionTag DeserializeLengthDelimited(Stream stream)
		{
			RegionTag regionTag = new RegionTag();
			DeserializeLengthDelimited(stream, regionTag);
			return regionTag;
		}

		public static RegionTag DeserializeLengthDelimited(Stream stream, RegionTag instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegionTag Deserialize(Stream stream, RegionTag instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 13:
					instance.Region = binaryReader.ReadUInt32();
					continue;
				case 21:
					instance.Tag = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, RegionTag instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRegion)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Region);
			}
			if (instance.HasTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Tag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRegion)
			{
				num++;
				num += 4;
			}
			if (HasTag)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
