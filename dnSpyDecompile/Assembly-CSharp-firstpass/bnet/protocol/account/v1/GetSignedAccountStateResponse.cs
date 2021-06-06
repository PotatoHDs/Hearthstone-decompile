using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050F RID: 1295
	public class GetSignedAccountStateResponse : IProtoBuf
	{
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x00118807 File Offset: 0x00116A07
		// (set) Token: 0x06005C3D RID: 23613 RVA: 0x0011880F File Offset: 0x00116A0F
		public string Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = (value != null);
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x00118822 File Offset: 0x00116A22
		public void SetToken(string val)
		{
			this.Token = val;
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x0011882C File Offset: 0x00116A2C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x0011885C File Offset: 0x00116A5C
		public override bool Equals(object obj)
		{
			GetSignedAccountStateResponse getSignedAccountStateResponse = obj as GetSignedAccountStateResponse;
			return getSignedAccountStateResponse != null && this.HasToken == getSignedAccountStateResponse.HasToken && (!this.HasToken || this.Token.Equals(getSignedAccountStateResponse.Token));
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x001188A1 File Offset: 0x00116AA1
		public static GetSignedAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSignedAccountStateResponse>(bs, 0, -1);
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x001188AB File Offset: 0x00116AAB
		public void Deserialize(Stream stream)
		{
			GetSignedAccountStateResponse.Deserialize(stream, this);
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x001188B5 File Offset: 0x00116AB5
		public static GetSignedAccountStateResponse Deserialize(Stream stream, GetSignedAccountStateResponse instance)
		{
			return GetSignedAccountStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x001188C0 File Offset: 0x00116AC0
		public static GetSignedAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSignedAccountStateResponse getSignedAccountStateResponse = new GetSignedAccountStateResponse();
			GetSignedAccountStateResponse.DeserializeLengthDelimited(stream, getSignedAccountStateResponse);
			return getSignedAccountStateResponse;
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x001188DC File Offset: 0x00116ADC
		public static GetSignedAccountStateResponse DeserializeLengthDelimited(Stream stream, GetSignedAccountStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSignedAccountStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x00118904 File Offset: 0x00116B04
		public static GetSignedAccountStateResponse Deserialize(Stream stream, GetSignedAccountStateResponse instance, long limit)
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
					instance.Token = ProtocolParser.ReadString(stream);
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

		// Token: 0x06005C48 RID: 23624 RVA: 0x00118984 File Offset: 0x00116B84
		public void Serialize(Stream stream)
		{
			GetSignedAccountStateResponse.Serialize(stream, this);
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x0011898D File Offset: 0x00116B8D
		public static void Serialize(Stream stream, GetSignedAccountStateResponse instance)
		{
			if (instance.HasToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Token));
			}
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x001189B8 File Offset: 0x00116BB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Token);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001C8D RID: 7309
		public bool HasToken;

		// Token: 0x04001C8E RID: 7310
		private string _Token;
	}
}
