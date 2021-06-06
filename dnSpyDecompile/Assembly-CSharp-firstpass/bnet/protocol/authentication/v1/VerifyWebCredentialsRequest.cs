using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004FC RID: 1276
	public class VerifyWebCredentialsRequest : IProtoBuf
	{
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x001147E5 File Offset: 0x001129E5
		// (set) Token: 0x06005AA8 RID: 23208 RVA: 0x001147ED File Offset: 0x001129ED
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

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00114800 File Offset: 0x00112A00
		public void SetWebCredentials(byte[] val)
		{
			this.WebCredentials = val;
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x0011480C File Offset: 0x00112A0C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWebCredentials)
			{
				num ^= this.WebCredentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x0011483C File Offset: 0x00112A3C
		public override bool Equals(object obj)
		{
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = obj as VerifyWebCredentialsRequest;
			return verifyWebCredentialsRequest != null && this.HasWebCredentials == verifyWebCredentialsRequest.HasWebCredentials && (!this.HasWebCredentials || this.WebCredentials.Equals(verifyWebCredentialsRequest.WebCredentials));
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x00114881 File Offset: 0x00112A81
		public static VerifyWebCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VerifyWebCredentialsRequest>(bs, 0, -1);
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x0011488B File Offset: 0x00112A8B
		public void Deserialize(Stream stream)
		{
			VerifyWebCredentialsRequest.Deserialize(stream, this);
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x00114895 File Offset: 0x00112A95
		public static VerifyWebCredentialsRequest Deserialize(Stream stream, VerifyWebCredentialsRequest instance)
		{
			return VerifyWebCredentialsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x001148A0 File Offset: 0x00112AA0
		public static VerifyWebCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = new VerifyWebCredentialsRequest();
			VerifyWebCredentialsRequest.DeserializeLengthDelimited(stream, verifyWebCredentialsRequest);
			return verifyWebCredentialsRequest;
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x001148BC File Offset: 0x00112ABC
		public static VerifyWebCredentialsRequest DeserializeLengthDelimited(Stream stream, VerifyWebCredentialsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VerifyWebCredentialsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x001148E4 File Offset: 0x00112AE4
		public static VerifyWebCredentialsRequest Deserialize(Stream stream, VerifyWebCredentialsRequest instance, long limit)
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

		// Token: 0x06005AB3 RID: 23219 RVA: 0x00114964 File Offset: 0x00112B64
		public void Serialize(Stream stream)
		{
			VerifyWebCredentialsRequest.Serialize(stream, this);
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0011496D File Offset: 0x00112B6D
		public static void Serialize(Stream stream, VerifyWebCredentialsRequest instance)
		{
			if (instance.HasWebCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.WebCredentials);
			}
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x0011498C File Offset: 0x00112B8C
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

		// Token: 0x04001C2B RID: 7211
		public bool HasWebCredentials;

		// Token: 0x04001C2C RID: 7212
		private byte[] _WebCredentials;
	}
}
