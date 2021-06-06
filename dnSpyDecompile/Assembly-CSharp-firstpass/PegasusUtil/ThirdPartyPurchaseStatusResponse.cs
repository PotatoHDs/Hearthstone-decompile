using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000B7 RID: 183
	public class ThirdPartyPurchaseStatusResponse : IProtoBuf
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002F723 File Offset: 0x0002D923
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0002F72B File Offset: 0x0002D92B
		public string ThirdPartyId { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002F734 File Offset: 0x0002D934
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0002F73C File Offset: 0x0002D93C
		public ThirdPartyPurchaseStatusResponse.Status Status_ { get; set; }

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002F748 File Offset: 0x0002D948
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ThirdPartyId.GetHashCode() ^ this.Status_.GetHashCode();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002F784 File Offset: 0x0002D984
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = obj as ThirdPartyPurchaseStatusResponse;
			return thirdPartyPurchaseStatusResponse != null && this.ThirdPartyId.Equals(thirdPartyPurchaseStatusResponse.ThirdPartyId) && this.Status_.Equals(thirdPartyPurchaseStatusResponse.Status_);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002F7D6 File Offset: 0x0002D9D6
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseStatusResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002F7E0 File Offset: 0x0002D9E0
		public static ThirdPartyPurchaseStatusResponse Deserialize(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			return ThirdPartyPurchaseStatusResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002F7EC File Offset: 0x0002D9EC
		public static ThirdPartyPurchaseStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = new ThirdPartyPurchaseStatusResponse();
			ThirdPartyPurchaseStatusResponse.DeserializeLengthDelimited(stream, thirdPartyPurchaseStatusResponse);
			return thirdPartyPurchaseStatusResponse;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002F808 File Offset: 0x0002DA08
		public static ThirdPartyPurchaseStatusResponse DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseStatusResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002F830 File Offset: 0x0002DA30
		public static ThirdPartyPurchaseStatusResponse Deserialize(Stream stream, ThirdPartyPurchaseStatusResponse instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
						instance.Status_ = (ThirdPartyPurchaseStatusResponse.Status)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ThirdPartyId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002F8C9 File Offset: 0x0002DAC9
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseStatusResponse.Serialize(stream, this);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
		public static void Serialize(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Status_));
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyId);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Status_)) + 2U;
		}

		// Token: 0x020005C2 RID: 1474
		public enum PacketID
		{
			// Token: 0x04001F93 RID: 8083
			ID = 295
		}

		// Token: 0x020005C3 RID: 1475
		public enum Status
		{
			// Token: 0x04001F95 RID: 8085
			NOT_FOUND = 1,
			// Token: 0x04001F96 RID: 8086
			SUCCEEDED,
			// Token: 0x04001F97 RID: 8087
			FAILED,
			// Token: 0x04001F98 RID: 8088
			IN_PROGRESS
		}
	}
}
