using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FA RID: 1274
	public class GenerateWebCredentialsRequest : IProtoBuf
	{
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x00114429 File Offset: 0x00112629
		// (set) Token: 0x06005A88 RID: 23176 RVA: 0x00114431 File Offset: 0x00112631
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

		// Token: 0x06005A89 RID: 23177 RVA: 0x00114441 File Offset: 0x00112641
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0011444C File Offset: 0x0011264C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x00114480 File Offset: 0x00112680
		public override bool Equals(object obj)
		{
			GenerateWebCredentialsRequest generateWebCredentialsRequest = obj as GenerateWebCredentialsRequest;
			return generateWebCredentialsRequest != null && this.HasProgram == generateWebCredentialsRequest.HasProgram && (!this.HasProgram || this.Program.Equals(generateWebCredentialsRequest.Program));
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x001144C8 File Offset: 0x001126C8
		public static GenerateWebCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateWebCredentialsRequest>(bs, 0, -1);
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x001144D2 File Offset: 0x001126D2
		public void Deserialize(Stream stream)
		{
			GenerateWebCredentialsRequest.Deserialize(stream, this);
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x001144DC File Offset: 0x001126DC
		public static GenerateWebCredentialsRequest Deserialize(Stream stream, GenerateWebCredentialsRequest instance)
		{
			return GenerateWebCredentialsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x001144E8 File Offset: 0x001126E8
		public static GenerateWebCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateWebCredentialsRequest generateWebCredentialsRequest = new GenerateWebCredentialsRequest();
			GenerateWebCredentialsRequest.DeserializeLengthDelimited(stream, generateWebCredentialsRequest);
			return generateWebCredentialsRequest;
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x00114504 File Offset: 0x00112704
		public static GenerateWebCredentialsRequest DeserializeLengthDelimited(Stream stream, GenerateWebCredentialsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateWebCredentialsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0011452C File Offset: 0x0011272C
		public static GenerateWebCredentialsRequest Deserialize(Stream stream, GenerateWebCredentialsRequest instance, long limit)
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
				else if (num == 13)
				{
					instance.Program = binaryReader.ReadUInt32();
				}
				else
				{
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

		// Token: 0x06005A93 RID: 23187 RVA: 0x001145B3 File Offset: 0x001127B3
		public void Serialize(Stream stream)
		{
			GenerateWebCredentialsRequest.Serialize(stream, this);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001145BC File Offset: 0x001127BC
		public static void Serialize(Stream stream, GenerateWebCredentialsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001145EC File Offset: 0x001127EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001C27 RID: 7207
		public bool HasProgram;

		// Token: 0x04001C28 RID: 7208
		private uint _Program;
	}
}
