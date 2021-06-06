using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000526 RID: 1318
	public class ProgramTag : IProtoBuf
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x0011D482 File Offset: 0x0011B682
		// (set) Token: 0x06005E23 RID: 24099 RVA: 0x0011D48A File Offset: 0x0011B68A
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

		// Token: 0x06005E24 RID: 24100 RVA: 0x0011D49A File Offset: 0x0011B69A
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x0011D4A3 File Offset: 0x0011B6A3
		// (set) Token: 0x06005E26 RID: 24102 RVA: 0x0011D4AB File Offset: 0x0011B6AB
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

		// Token: 0x06005E27 RID: 24103 RVA: 0x0011D4BB File Offset: 0x0011B6BB
		public void SetTag(uint val)
		{
			this.Tag = val;
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x0011D4C4 File Offset: 0x0011B6C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasTag)
			{
				num ^= this.Tag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0011D510 File Offset: 0x0011B710
		public override bool Equals(object obj)
		{
			ProgramTag programTag = obj as ProgramTag;
			return programTag != null && this.HasProgram == programTag.HasProgram && (!this.HasProgram || this.Program.Equals(programTag.Program)) && this.HasTag == programTag.HasTag && (!this.HasTag || this.Tag.Equals(programTag.Tag));
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x0011D586 File Offset: 0x0011B786
		public static ProgramTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProgramTag>(bs, 0, -1);
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0011D590 File Offset: 0x0011B790
		public void Deserialize(Stream stream)
		{
			ProgramTag.Deserialize(stream, this);
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x0011D59A File Offset: 0x0011B79A
		public static ProgramTag Deserialize(Stream stream, ProgramTag instance)
		{
			return ProgramTag.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x0011D5A8 File Offset: 0x0011B7A8
		public static ProgramTag DeserializeLengthDelimited(Stream stream)
		{
			ProgramTag programTag = new ProgramTag();
			ProgramTag.DeserializeLengthDelimited(stream, programTag);
			return programTag;
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0011D5C4 File Offset: 0x0011B7C4
		public static ProgramTag DeserializeLengthDelimited(Stream stream, ProgramTag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProgramTag.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0011D5EC File Offset: 0x0011B7EC
		public static ProgramTag Deserialize(Stream stream, ProgramTag instance, long limit)
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
					instance.Program = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0011D68B File Offset: 0x0011B88B
		public void Serialize(Stream stream)
		{
			ProgramTag.Serialize(stream, this);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x0011D694 File Offset: 0x0011B894
		public static void Serialize(Stream stream, ProgramTag instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Tag);
			}
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x0011D6E0 File Offset: 0x0011B8E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProgram)
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

		// Token: 0x04001CF6 RID: 7414
		public bool HasProgram;

		// Token: 0x04001CF7 RID: 7415
		private uint _Program;

		// Token: 0x04001CF8 RID: 7416
		public bool HasTag;

		// Token: 0x04001CF9 RID: 7417
		private uint _Tag;
	}
}
