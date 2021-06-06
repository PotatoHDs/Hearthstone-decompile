using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000519 RID: 1305
	public class GetCAISInfoResponse : IProtoBuf
	{
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x0011A597 File Offset: 0x00118797
		// (set) Token: 0x06005CFF RID: 23807 RVA: 0x0011A59F File Offset: 0x0011879F
		public CAIS CaisInfo
		{
			get
			{
				return this._CaisInfo;
			}
			set
			{
				this._CaisInfo = value;
				this.HasCaisInfo = (value != null);
			}
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0011A5B2 File Offset: 0x001187B2
		public void SetCaisInfo(CAIS val)
		{
			this.CaisInfo = val;
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x0011A5BC File Offset: 0x001187BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCaisInfo)
			{
				num ^= this.CaisInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x0011A5EC File Offset: 0x001187EC
		public override bool Equals(object obj)
		{
			GetCAISInfoResponse getCAISInfoResponse = obj as GetCAISInfoResponse;
			return getCAISInfoResponse != null && this.HasCaisInfo == getCAISInfoResponse.HasCaisInfo && (!this.HasCaisInfo || this.CaisInfo.Equals(getCAISInfoResponse.CaisInfo));
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06005D03 RID: 23811 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x0011A631 File Offset: 0x00118831
		public static GetCAISInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetCAISInfoResponse>(bs, 0, -1);
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x0011A63B File Offset: 0x0011883B
		public void Deserialize(Stream stream)
		{
			GetCAISInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x0011A645 File Offset: 0x00118845
		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance)
		{
			return GetCAISInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x0011A650 File Offset: 0x00118850
		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetCAISInfoResponse getCAISInfoResponse = new GetCAISInfoResponse();
			GetCAISInfoResponse.DeserializeLengthDelimited(stream, getCAISInfoResponse);
			return getCAISInfoResponse;
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0011A66C File Offset: 0x0011886C
		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream, GetCAISInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetCAISInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x0011A694 File Offset: 0x00118894
		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance, long limit)
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
					if (instance.CaisInfo == null)
					{
						instance.CaisInfo = CAIS.DeserializeLengthDelimited(stream);
					}
					else
					{
						CAIS.DeserializeLengthDelimited(stream, instance.CaisInfo);
					}
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

		// Token: 0x06005D0A RID: 23818 RVA: 0x0011A72E File Offset: 0x0011892E
		public void Serialize(Stream stream)
		{
			GetCAISInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x0011A737 File Offset: 0x00118937
		public static void Serialize(Stream stream, GetCAISInfoResponse instance)
		{
			if (instance.HasCaisInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.CaisInfo.GetSerializedSize());
				CAIS.Serialize(stream, instance.CaisInfo);
			}
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x0011A768 File Offset: 0x00118968
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCaisInfo)
			{
				num += 1U;
				uint serializedSize = this.CaisInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001CB4 RID: 7348
		public bool HasCaisInfo;

		// Token: 0x04001CB5 RID: 7349
		private CAIS _CaisInfo;
	}
}
