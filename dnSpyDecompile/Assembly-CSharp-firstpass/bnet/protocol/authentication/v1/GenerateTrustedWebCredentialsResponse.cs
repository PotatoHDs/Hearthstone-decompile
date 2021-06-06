using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x02000503 RID: 1283
	public class GenerateTrustedWebCredentialsResponse : IProtoBuf
	{
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x001166A0 File Offset: 0x001148A0
		// (set) Token: 0x06005B5A RID: 23386 RVA: 0x001166A8 File Offset: 0x001148A8
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

		// Token: 0x06005B5B RID: 23387 RVA: 0x001166BB File Offset: 0x001148BB
		public void SetWebCredentials(byte[] val)
		{
			this.WebCredentials = val;
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x001166C4 File Offset: 0x001148C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWebCredentials)
			{
				num ^= this.WebCredentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x001166F4 File Offset: 0x001148F4
		public override bool Equals(object obj)
		{
			GenerateTrustedWebCredentialsResponse generateTrustedWebCredentialsResponse = obj as GenerateTrustedWebCredentialsResponse;
			return generateTrustedWebCredentialsResponse != null && this.HasWebCredentials == generateTrustedWebCredentialsResponse.HasWebCredentials && (!this.HasWebCredentials || this.WebCredentials.Equals(generateTrustedWebCredentialsResponse.WebCredentials));
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x00116739 File Offset: 0x00114939
		public static GenerateTrustedWebCredentialsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTrustedWebCredentialsResponse>(bs, 0, -1);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x00116743 File Offset: 0x00114943
		public void Deserialize(Stream stream)
		{
			GenerateTrustedWebCredentialsResponse.Deserialize(stream, this);
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0011674D File Offset: 0x0011494D
		public static GenerateTrustedWebCredentialsResponse Deserialize(Stream stream, GenerateTrustedWebCredentialsResponse instance)
		{
			return GenerateTrustedWebCredentialsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00116758 File Offset: 0x00114958
		public static GenerateTrustedWebCredentialsResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateTrustedWebCredentialsResponse generateTrustedWebCredentialsResponse = new GenerateTrustedWebCredentialsResponse();
			GenerateTrustedWebCredentialsResponse.DeserializeLengthDelimited(stream, generateTrustedWebCredentialsResponse);
			return generateTrustedWebCredentialsResponse;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x00116774 File Offset: 0x00114974
		public static GenerateTrustedWebCredentialsResponse DeserializeLengthDelimited(Stream stream, GenerateTrustedWebCredentialsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateTrustedWebCredentialsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0011679C File Offset: 0x0011499C
		public static GenerateTrustedWebCredentialsResponse Deserialize(Stream stream, GenerateTrustedWebCredentialsResponse instance, long limit)
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

		// Token: 0x06005B65 RID: 23397 RVA: 0x0011681C File Offset: 0x00114A1C
		public void Serialize(Stream stream)
		{
			GenerateTrustedWebCredentialsResponse.Serialize(stream, this);
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x00116825 File Offset: 0x00114A25
		public static void Serialize(Stream stream, GenerateTrustedWebCredentialsResponse instance)
		{
			if (instance.HasWebCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.WebCredentials);
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x00116844 File Offset: 0x00114A44
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

		// Token: 0x04001C65 RID: 7269
		public bool HasWebCredentials;

		// Token: 0x04001C66 RID: 7270
		private byte[] _WebCredentials;
	}
}
