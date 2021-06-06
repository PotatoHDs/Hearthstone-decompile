using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D3 RID: 1235
	public class ListChannelsOptions : IProtoBuf
	{
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x0010A6C7 File Offset: 0x001088C7
		// (set) Token: 0x060056D7 RID: 22231 RVA: 0x0010A6CF File Offset: 0x001088CF
		public uint StartIndex
		{
			get
			{
				return this._StartIndex;
			}
			set
			{
				this._StartIndex = value;
				this.HasStartIndex = true;
			}
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x0010A6DF File Offset: 0x001088DF
		public void SetStartIndex(uint val)
		{
			this.StartIndex = val;
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x0010A6E8 File Offset: 0x001088E8
		// (set) Token: 0x060056DA RID: 22234 RVA: 0x0010A6F0 File Offset: 0x001088F0
		public uint MaxResults
		{
			get
			{
				return this._MaxResults;
			}
			set
			{
				this._MaxResults = value;
				this.HasMaxResults = true;
			}
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x0010A700 File Offset: 0x00108900
		public void SetMaxResults(uint val)
		{
			this.MaxResults = val;
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060056DC RID: 22236 RVA: 0x0010A709 File Offset: 0x00108909
		// (set) Token: 0x060056DD RID: 22237 RVA: 0x0010A711 File Offset: 0x00108911
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x0010A724 File Offset: 0x00108924
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x0010A72D File Offset: 0x0010892D
		// (set) Token: 0x060056E0 RID: 22240 RVA: 0x0010A735 File Offset: 0x00108935
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x0010A745 File Offset: 0x00108945
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0010A74E File Offset: 0x0010894E
		// (set) Token: 0x060056E3 RID: 22243 RVA: 0x0010A756 File Offset: 0x00108956
		public uint Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = true;
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0010A766 File Offset: 0x00108966
		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x0010A76F File Offset: 0x0010896F
		// (set) Token: 0x060056E6 RID: 22246 RVA: 0x0010A777 File Offset: 0x00108977
		public uint CapacityFull
		{
			get
			{
				return this._CapacityFull;
			}
			set
			{
				this._CapacityFull = value;
				this.HasCapacityFull = true;
			}
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x0010A787 File Offset: 0x00108987
		public void SetCapacityFull(uint val)
		{
			this.CapacityFull = val;
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x0010A790 File Offset: 0x00108990
		// (set) Token: 0x060056E9 RID: 22249 RVA: 0x0010A798 File Offset: 0x00108998
		public AttributeFilter AttributeFilter { get; set; }

		// Token: 0x060056EA RID: 22250 RVA: 0x0010A7A1 File Offset: 0x001089A1
		public void SetAttributeFilter(AttributeFilter val)
		{
			this.AttributeFilter = val;
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x0010A7AA File Offset: 0x001089AA
		// (set) Token: 0x060056EC RID: 22252 RVA: 0x0010A7B2 File Offset: 0x001089B2
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x0010A7C5 File Offset: 0x001089C5
		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x0010A7D0 File Offset: 0x001089D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStartIndex)
			{
				num ^= this.StartIndex.GetHashCode();
			}
			if (this.HasMaxResults)
			{
				num ^= this.MaxResults.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasCapacityFull)
			{
				num ^= this.CapacityFull.GetHashCode();
			}
			num ^= this.AttributeFilter.GetHashCode();
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x0010A8A4 File Offset: 0x00108AA4
		public override bool Equals(object obj)
		{
			ListChannelsOptions listChannelsOptions = obj as ListChannelsOptions;
			return listChannelsOptions != null && this.HasStartIndex == listChannelsOptions.HasStartIndex && (!this.HasStartIndex || this.StartIndex.Equals(listChannelsOptions.StartIndex)) && this.HasMaxResults == listChannelsOptions.HasMaxResults && (!this.HasMaxResults || this.MaxResults.Equals(listChannelsOptions.MaxResults)) && this.HasName == listChannelsOptions.HasName && (!this.HasName || this.Name.Equals(listChannelsOptions.Name)) && this.HasProgram == listChannelsOptions.HasProgram && (!this.HasProgram || this.Program.Equals(listChannelsOptions.Program)) && this.HasLocale == listChannelsOptions.HasLocale && (!this.HasLocale || this.Locale.Equals(listChannelsOptions.Locale)) && this.HasCapacityFull == listChannelsOptions.HasCapacityFull && (!this.HasCapacityFull || this.CapacityFull.Equals(listChannelsOptions.CapacityFull)) && this.AttributeFilter.Equals(listChannelsOptions.AttributeFilter) && this.HasChannelType == listChannelsOptions.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(listChannelsOptions.ChannelType));
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x0010AA0F File Offset: 0x00108C0F
		public static ListChannelsOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsOptions>(bs, 0, -1);
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x0010AA19 File Offset: 0x00108C19
		public void Deserialize(Stream stream)
		{
			ListChannelsOptions.Deserialize(stream, this);
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x0010AA23 File Offset: 0x00108C23
		public static ListChannelsOptions Deserialize(Stream stream, ListChannelsOptions instance)
		{
			return ListChannelsOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x0010AA30 File Offset: 0x00108C30
		public static ListChannelsOptions DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsOptions listChannelsOptions = new ListChannelsOptions();
			ListChannelsOptions.DeserializeLengthDelimited(stream, listChannelsOptions);
			return listChannelsOptions;
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x0010AA4C File Offset: 0x00108C4C
		public static ListChannelsOptions DeserializeLengthDelimited(Stream stream, ListChannelsOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelsOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x0010AA74 File Offset: 0x00108C74
		public static ListChannelsOptions Deserialize(Stream stream, ListChannelsOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.StartIndex = 0U;
			instance.MaxResults = 16U;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 37)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.StartIndex = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 16)
							{
								instance.MaxResults = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 37)
							{
								instance.Program = binaryReader.ReadUInt32();
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 45)
						{
							instance.Locale = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 48)
						{
							instance.CapacityFull = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else if (num != 58)
					{
						if (num == 66)
						{
							instance.ChannelType = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (instance.AttributeFilter == null)
						{
							instance.AttributeFilter = AttributeFilter.DeserializeLengthDelimited(stream);
							continue;
						}
						AttributeFilter.DeserializeLengthDelimited(stream, instance.AttributeFilter);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x0010ABEF File Offset: 0x00108DEF
		public void Serialize(Stream stream)
		{
			ListChannelsOptions.Serialize(stream, this);
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x0010ABF8 File Offset: 0x00108DF8
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

		// Token: 0x060056F9 RID: 22265 RVA: 0x0010AD20 File Offset: 0x00108F20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasStartIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.StartIndex);
			}
			if (this.HasMaxResults)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxResults);
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasLocale)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCapacityFull)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CapacityFull);
			}
			uint serializedSize = this.AttributeFilter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasChannelType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1U;
		}

		// Token: 0x04001B4F RID: 6991
		public bool HasStartIndex;

		// Token: 0x04001B50 RID: 6992
		private uint _StartIndex;

		// Token: 0x04001B51 RID: 6993
		public bool HasMaxResults;

		// Token: 0x04001B52 RID: 6994
		private uint _MaxResults;

		// Token: 0x04001B53 RID: 6995
		public bool HasName;

		// Token: 0x04001B54 RID: 6996
		private string _Name;

		// Token: 0x04001B55 RID: 6997
		public bool HasProgram;

		// Token: 0x04001B56 RID: 6998
		private uint _Program;

		// Token: 0x04001B57 RID: 6999
		public bool HasLocale;

		// Token: 0x04001B58 RID: 7000
		private uint _Locale;

		// Token: 0x04001B59 RID: 7001
		public bool HasCapacityFull;

		// Token: 0x04001B5A RID: 7002
		private uint _CapacityFull;

		// Token: 0x04001B5C RID: 7004
		public bool HasChannelType;

		// Token: 0x04001B5D RID: 7005
		private string _ChannelType;
	}
}
