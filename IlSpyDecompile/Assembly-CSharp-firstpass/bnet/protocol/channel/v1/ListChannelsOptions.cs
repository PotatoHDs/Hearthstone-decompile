using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class ListChannelsOptions : IProtoBuf
	{
		public bool HasStartIndex;

		private uint _StartIndex;

		public bool HasMaxResults;

		private uint _MaxResults;

		public bool HasName;

		private string _Name;

		public bool HasProgram;

		private uint _Program;

		public bool HasLocale;

		private uint _Locale;

		public bool HasCapacityFull;

		private uint _CapacityFull;

		public bool HasChannelType;

		private string _ChannelType;

		public uint StartIndex
		{
			get
			{
				return _StartIndex;
			}
			set
			{
				_StartIndex = value;
				HasStartIndex = true;
			}
		}

		public uint MaxResults
		{
			get
			{
				return _MaxResults;
			}
			set
			{
				_MaxResults = value;
				HasMaxResults = true;
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public uint Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = true;
			}
		}

		public uint CapacityFull
		{
			get
			{
				return _CapacityFull;
			}
			set
			{
				_CapacityFull = value;
				HasCapacityFull = true;
			}
		}

		public AttributeFilter AttributeFilter { get; set; }

		public string ChannelType
		{
			get
			{
				return _ChannelType;
			}
			set
			{
				_ChannelType = value;
				HasChannelType = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetStartIndex(uint val)
		{
			StartIndex = val;
		}

		public void SetMaxResults(uint val)
		{
			MaxResults = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetLocale(uint val)
		{
			Locale = val;
		}

		public void SetCapacityFull(uint val)
		{
			CapacityFull = val;
		}

		public void SetAttributeFilter(AttributeFilter val)
		{
			AttributeFilter = val;
		}

		public void SetChannelType(string val)
		{
			ChannelType = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasStartIndex)
			{
				num ^= StartIndex.GetHashCode();
			}
			if (HasMaxResults)
			{
				num ^= MaxResults.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasCapacityFull)
			{
				num ^= CapacityFull.GetHashCode();
			}
			num ^= AttributeFilter.GetHashCode();
			if (HasChannelType)
			{
				num ^= ChannelType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListChannelsOptions listChannelsOptions = obj as ListChannelsOptions;
			if (listChannelsOptions == null)
			{
				return false;
			}
			if (HasStartIndex != listChannelsOptions.HasStartIndex || (HasStartIndex && !StartIndex.Equals(listChannelsOptions.StartIndex)))
			{
				return false;
			}
			if (HasMaxResults != listChannelsOptions.HasMaxResults || (HasMaxResults && !MaxResults.Equals(listChannelsOptions.MaxResults)))
			{
				return false;
			}
			if (HasName != listChannelsOptions.HasName || (HasName && !Name.Equals(listChannelsOptions.Name)))
			{
				return false;
			}
			if (HasProgram != listChannelsOptions.HasProgram || (HasProgram && !Program.Equals(listChannelsOptions.Program)))
			{
				return false;
			}
			if (HasLocale != listChannelsOptions.HasLocale || (HasLocale && !Locale.Equals(listChannelsOptions.Locale)))
			{
				return false;
			}
			if (HasCapacityFull != listChannelsOptions.HasCapacityFull || (HasCapacityFull && !CapacityFull.Equals(listChannelsOptions.CapacityFull)))
			{
				return false;
			}
			if (!AttributeFilter.Equals(listChannelsOptions.AttributeFilter))
			{
				return false;
			}
			if (HasChannelType != listChannelsOptions.HasChannelType || (HasChannelType && !ChannelType.Equals(listChannelsOptions.ChannelType)))
			{
				return false;
			}
			return true;
		}

		public static ListChannelsOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListChannelsOptions Deserialize(Stream stream, ListChannelsOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListChannelsOptions DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsOptions listChannelsOptions = new ListChannelsOptions();
			DeserializeLengthDelimited(stream, listChannelsOptions);
			return listChannelsOptions;
		}

		public static ListChannelsOptions DeserializeLengthDelimited(Stream stream, ListChannelsOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListChannelsOptions Deserialize(Stream stream, ListChannelsOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.StartIndex = 0u;
			instance.MaxResults = 16u;
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
					instance.StartIndex = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.MaxResults = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 37:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 45:
					instance.Locale = binaryReader.ReadUInt32();
					continue;
				case 48:
					instance.CapacityFull = ProtocolParser.ReadUInt32(stream);
					continue;
				case 58:
					if (instance.AttributeFilter == null)
					{
						instance.AttributeFilter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.AttributeFilter);
					}
					continue;
				case 66:
					instance.ChannelType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ListChannelsOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasStartIndex)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.StartIndex);
			}
			if (instance.HasMaxResults)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MaxResults);
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.HasCapacityFull)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.CapacityFull);
			}
			if (instance.AttributeFilter == null)
			{
				throw new ArgumentNullException("AttributeFilter", "Required by proto specification.");
			}
			stream.WriteByte(58);
			ProtocolParser.WriteUInt32(stream, instance.AttributeFilter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.AttributeFilter);
			if (instance.HasChannelType)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasStartIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(StartIndex);
			}
			if (HasMaxResults)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxResults);
			}
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasLocale)
			{
				num++;
				num += 4;
			}
			if (HasCapacityFull)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CapacityFull);
			}
			uint serializedSize = AttributeFilter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasChannelType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1;
		}
	}
}
