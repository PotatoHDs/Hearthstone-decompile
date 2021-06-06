using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000527 RID: 1319
	public class RegionTag : IProtoBuf
	{
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x0011D710 File Offset: 0x0011B910
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x0011D718 File Offset: 0x0011B918
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0011D728 File Offset: 0x0011B928
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06005E38 RID: 24120 RVA: 0x0011D731 File Offset: 0x0011B931
		// (set) Token: 0x06005E39 RID: 24121 RVA: 0x0011D739 File Offset: 0x0011B939
		public uint Tag
		{
			get
			{
				return this._Tag;
			}
			set
			{
				this._Tag = value;
				this.HasTag = true;
			}
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0011D749 File Offset: 0x0011B949
		public void SetTag(uint val)
		{
			this.Tag = val;
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0011D754 File Offset: 0x0011B954
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			if (this.HasTag)
			{
				num ^= this.Tag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0011D7A0 File Offset: 0x0011B9A0
		public override bool Equals(object obj)
		{
			RegionTag regionTag = obj as RegionTag;
			return regionTag != null && this.HasRegion == regionTag.HasRegion && (!this.HasRegion || this.Region.Equals(regionTag.Region)) && this.HasTag == regionTag.HasTag && (!this.HasTag || this.Tag.Equals(regionTag.Tag));
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x0011D816 File Offset: 0x0011BA16
		public static RegionTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegionTag>(bs, 0, -1);
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x0011D820 File Offset: 0x0011BA20
		public void Deserialize(Stream stream)
		{
			RegionTag.Deserialize(stream, this);
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x0011D82A File Offset: 0x0011BA2A
		public static RegionTag Deserialize(Stream stream, RegionTag instance)
		{
			return RegionTag.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E41 RID: 24129 RVA: 0x0011D838 File Offset: 0x0011BA38
		public static RegionTag DeserializeLengthDelimited(Stream stream)
		{
			RegionTag regionTag = new RegionTag();
			RegionTag.DeserializeLengthDelimited(stream, regionTag);
			return regionTag;
		}

		// Token: 0x06005E42 RID: 24130 RVA: 0x0011D854 File Offset: 0x0011BA54
		public static RegionTag DeserializeLengthDelimited(Stream stream, RegionTag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegionTag.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E43 RID: 24131 RVA: 0x0011D87C File Offset: 0x0011BA7C
		public static RegionTag Deserialize(Stream stream, RegionTag instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 13)
				{
					if (num != 21)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Tag = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Region = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x0011D91B File Offset: 0x0011BB1B
		public void Serialize(Stream stream)
		{
			RegionTag.Serialize(stream, this);
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0011D924 File Offset: 0x0011BB24
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

		// Token: 0x06005E46 RID: 24134 RVA: 0x0011D970 File Offset: 0x0011BB70
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRegion)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasTag)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001CFA RID: 7418
		public bool HasRegion;

		// Token: 0x04001CFB RID: 7419
		private uint _Region;

		// Token: 0x04001CFC RID: 7420
		public bool HasTag;

		// Token: 0x04001CFD RID: 7421
		private uint _Tag;
	}
}
