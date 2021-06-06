using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004EE RID: 1262
	public class GenerateSSOTokenRequest : IProtoBuf
	{
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x001124AA File Offset: 0x001106AA
		// (set) Token: 0x0600599A RID: 22938 RVA: 0x001124B2 File Offset: 0x001106B2
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

		// Token: 0x0600599B RID: 22939 RVA: 0x001124C2 File Offset: 0x001106C2
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x001124CC File Offset: 0x001106CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x00112500 File Offset: 0x00110700
		public override bool Equals(object obj)
		{
			GenerateSSOTokenRequest generateSSOTokenRequest = obj as GenerateSSOTokenRequest;
			return generateSSOTokenRequest != null && this.HasProgram == generateSSOTokenRequest.HasProgram && (!this.HasProgram || this.Program.Equals(generateSSOTokenRequest.Program));
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x00112548 File Offset: 0x00110748
		public static GenerateSSOTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateSSOTokenRequest>(bs, 0, -1);
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x00112552 File Offset: 0x00110752
		public void Deserialize(Stream stream)
		{
			GenerateSSOTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0011255C File Offset: 0x0011075C
		public static GenerateSSOTokenRequest Deserialize(Stream stream, GenerateSSOTokenRequest instance)
		{
			return GenerateSSOTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00112568 File Offset: 0x00110768
		public static GenerateSSOTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateSSOTokenRequest generateSSOTokenRequest = new GenerateSSOTokenRequest();
			GenerateSSOTokenRequest.DeserializeLengthDelimited(stream, generateSSOTokenRequest);
			return generateSSOTokenRequest;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x00112584 File Offset: 0x00110784
		public static GenerateSSOTokenRequest DeserializeLengthDelimited(Stream stream, GenerateSSOTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateSSOTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x001125AC File Offset: 0x001107AC
		public static GenerateSSOTokenRequest Deserialize(Stream stream, GenerateSSOTokenRequest instance, long limit)
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

		// Token: 0x060059A5 RID: 22949 RVA: 0x00112633 File Offset: 0x00110833
		public void Serialize(Stream stream)
		{
			GenerateSSOTokenRequest.Serialize(stream, this);
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x0011263C File Offset: 0x0011083C
		public static void Serialize(Stream stream, GenerateSSOTokenRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x0011266C File Offset: 0x0011086C
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

		// Token: 0x04001C00 RID: 7168
		public bool HasProgram;

		// Token: 0x04001C01 RID: 7169
		private uint _Program;
	}
}
