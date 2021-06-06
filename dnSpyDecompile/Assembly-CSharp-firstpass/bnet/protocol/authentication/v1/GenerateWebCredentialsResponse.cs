using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FB RID: 1275
	public class GenerateWebCredentialsResponse : IProtoBuf
	{
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x0011460C File Offset: 0x0011280C
		// (set) Token: 0x06005A98 RID: 23192 RVA: 0x00114614 File Offset: 0x00112814
		public byte[] WebCredentials
		{
			get
			{
				return this._WebCredentials;
			}
			set
			{
				this._WebCredentials = value;
				this.HasWebCredentials = (value != null);
			}
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00114627 File Offset: 0x00112827
		public void SetWebCredentials(byte[] val)
		{
			this.WebCredentials = val;
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x00114630 File Offset: 0x00112830
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWebCredentials)
			{
				num ^= this.WebCredentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x00114660 File Offset: 0x00112860
		public override bool Equals(object obj)
		{
			GenerateWebCredentialsResponse generateWebCredentialsResponse = obj as GenerateWebCredentialsResponse;
			return generateWebCredentialsResponse != null && this.HasWebCredentials == generateWebCredentialsResponse.HasWebCredentials && (!this.HasWebCredentials || this.WebCredentials.Equals(generateWebCredentialsResponse.WebCredentials));
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06005A9C RID: 23196 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x001146A5 File Offset: 0x001128A5
		public static GenerateWebCredentialsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateWebCredentialsResponse>(bs, 0, -1);
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x001146AF File Offset: 0x001128AF
		public void Deserialize(Stream stream)
		{
			GenerateWebCredentialsResponse.Deserialize(stream, this);
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x001146B9 File Offset: 0x001128B9
		public static GenerateWebCredentialsResponse Deserialize(Stream stream, GenerateWebCredentialsResponse instance)
		{
			return GenerateWebCredentialsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001146C4 File Offset: 0x001128C4
		public static GenerateWebCredentialsResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateWebCredentialsResponse generateWebCredentialsResponse = new GenerateWebCredentialsResponse();
			GenerateWebCredentialsResponse.DeserializeLengthDelimited(stream, generateWebCredentialsResponse);
			return generateWebCredentialsResponse;
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x001146E0 File Offset: 0x001128E0
		public static GenerateWebCredentialsResponse DeserializeLengthDelimited(Stream stream, GenerateWebCredentialsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateWebCredentialsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x00114708 File Offset: 0x00112908
		public static GenerateWebCredentialsResponse Deserialize(Stream stream, GenerateWebCredentialsResponse instance, long limit)
		{
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
				else if (num == 10)
				{
					instance.WebCredentials = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005AA3 RID: 23203 RVA: 0x00114788 File Offset: 0x00112988
		public void Serialize(Stream stream)
		{
			GenerateWebCredentialsResponse.Serialize(stream, this);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00114791 File Offset: 0x00112991
		public static void Serialize(Stream stream, GenerateWebCredentialsResponse instance)
		{
			if (instance.HasWebCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.WebCredentials);
			}
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001147B0 File Offset: 0x001129B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasWebCredentials)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.WebCredentials.Length) + (uint)this.WebCredentials.Length;
			}
			return num;
		}

		// Token: 0x04001C29 RID: 7209
		public bool HasWebCredentials;

		// Token: 0x04001C2A RID: 7210
		private byte[] _WebCredentials;
	}
}
