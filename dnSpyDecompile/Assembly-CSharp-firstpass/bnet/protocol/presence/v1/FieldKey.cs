using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000341 RID: 833
	public class FieldKey : IProtoBuf
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000AC3B3 File Offset: 0x000AA5B3
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x000AC3BB File Offset: 0x000AA5BB
		public uint Program { get; set; }

		// Token: 0x060033B4 RID: 13236 RVA: 0x000AC3C4 File Offset: 0x000AA5C4
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000AC3CD File Offset: 0x000AA5CD
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000AC3D5 File Offset: 0x000AA5D5
		public uint Group { get; set; }

		// Token: 0x060033B7 RID: 13239 RVA: 0x000AC3DE File Offset: 0x000AA5DE
		public void SetGroup(uint val)
		{
			this.Group = val;
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000AC3E7 File Offset: 0x000AA5E7
		// (set) Token: 0x060033B9 RID: 13241 RVA: 0x000AC3EF File Offset: 0x000AA5EF
		public uint Field { get; set; }

		// Token: 0x060033BA RID: 13242 RVA: 0x000AC3F8 File Offset: 0x000AA5F8
		public void SetField(uint val)
		{
			this.Field = val;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000AC401 File Offset: 0x000AA601
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000AC409 File Offset: 0x000AA609
		public ulong UniqueId
		{
			get
			{
				return this._UniqueId;
			}
			set
			{
				this._UniqueId = value;
				this.HasUniqueId = true;
			}
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000AC419 File Offset: 0x000AA619
		public void SetUniqueId(ulong val)
		{
			this.UniqueId = val;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000AC424 File Offset: 0x000AA624
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Program.GetHashCode();
			num ^= this.Group.GetHashCode();
			num ^= this.Field.GetHashCode();
			if (this.HasUniqueId)
			{
				num ^= this.UniqueId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000AC48C File Offset: 0x000AA68C
		public override bool Equals(object obj)
		{
			FieldKey fieldKey = obj as FieldKey;
			return fieldKey != null && this.Program.Equals(fieldKey.Program) && this.Group.Equals(fieldKey.Group) && this.Field.Equals(fieldKey.Field) && this.HasUniqueId == fieldKey.HasUniqueId && (!this.HasUniqueId || this.UniqueId.Equals(fieldKey.UniqueId));
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000AC51C File Offset: 0x000AA71C
		public static FieldKey ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldKey>(bs, 0, -1);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000AC526 File Offset: 0x000AA726
		public void Deserialize(Stream stream)
		{
			FieldKey.Deserialize(stream, this);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000AC530 File Offset: 0x000AA730
		public static FieldKey Deserialize(Stream stream, FieldKey instance)
		{
			return FieldKey.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000AC53C File Offset: 0x000AA73C
		public static FieldKey DeserializeLengthDelimited(Stream stream)
		{
			FieldKey fieldKey = new FieldKey();
			FieldKey.DeserializeLengthDelimited(stream, fieldKey);
			return fieldKey;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000AC558 File Offset: 0x000AA758
		public static FieldKey DeserializeLengthDelimited(Stream stream, FieldKey instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FieldKey.Deserialize(stream, instance, num);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000AC580 File Offset: 0x000AA780
		public static FieldKey Deserialize(Stream stream, FieldKey instance, long limit)
		{
			instance.UniqueId = 0UL;
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Program = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Group = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Field = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.UniqueId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
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

		// Token: 0x060033C7 RID: 13255 RVA: 0x000AC658 File Offset: 0x000AA858
		public void Serialize(Stream stream)
		{
			FieldKey.Serialize(stream, this);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000AC664 File Offset: 0x000AA864
		public static void Serialize(Stream stream, FieldKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Program);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Group);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Field);
			if (instance.HasUniqueId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.UniqueId);
			}
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000AC6C8 File Offset: 0x000AA8C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.Program);
			num += ProtocolParser.SizeOfUInt32(this.Group);
			num += ProtocolParser.SizeOfUInt32(this.Field);
			if (this.HasUniqueId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.UniqueId);
			}
			return num + 3U;
		}

		// Token: 0x0400140D RID: 5133
		public bool HasUniqueId;

		// Token: 0x0400140E RID: 5134
		private ulong _UniqueId;
	}
}
