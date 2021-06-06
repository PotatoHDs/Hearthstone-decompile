using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x0200006F RID: 111
	public class GetThirdPartyPurchaseStatus : IProtoBuf
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001A3BB File Offset: 0x000185BB
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001A3C3 File Offset: 0x000185C3
		public string ThirdPartyId { get; set; }

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A3CC File Offset: 0x000185CC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ThirdPartyId.GetHashCode();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001A3E8 File Offset: 0x000185E8
		public override bool Equals(object obj)
		{
			GetThirdPartyPurchaseStatus getThirdPartyPurchaseStatus = obj as GetThirdPartyPurchaseStatus;
			return getThirdPartyPurchaseStatus != null && this.ThirdPartyId.Equals(getThirdPartyPurchaseStatus.ThirdPartyId);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001A417 File Offset: 0x00018617
		public void Deserialize(Stream stream)
		{
			GetThirdPartyPurchaseStatus.Deserialize(stream, this);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001A421 File Offset: 0x00018621
		public static GetThirdPartyPurchaseStatus Deserialize(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			return GetThirdPartyPurchaseStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001A42C File Offset: 0x0001862C
		public static GetThirdPartyPurchaseStatus DeserializeLengthDelimited(Stream stream)
		{
			GetThirdPartyPurchaseStatus getThirdPartyPurchaseStatus = new GetThirdPartyPurchaseStatus();
			GetThirdPartyPurchaseStatus.DeserializeLengthDelimited(stream, getThirdPartyPurchaseStatus);
			return getThirdPartyPurchaseStatus;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001A448 File Offset: 0x00018648
		public static GetThirdPartyPurchaseStatus DeserializeLengthDelimited(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetThirdPartyPurchaseStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001A470 File Offset: 0x00018670
		public static GetThirdPartyPurchaseStatus Deserialize(Stream stream, GetThirdPartyPurchaseStatus instance, long limit)
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
					instance.ThirdPartyId = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600070E RID: 1806 RVA: 0x0001A4F0 File Offset: 0x000186F0
		public void Serialize(Stream stream)
		{
			GetThirdPartyPurchaseStatus.Serialize(stream, this);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001A4F9 File Offset: 0x000186F9
		public static void Serialize(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001A534 File Offset: 0x00018734
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyId);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}

		// Token: 0x02000581 RID: 1409
		public enum PacketID
		{
			// Token: 0x04001EDF RID: 7903
			ID = 294,
			// Token: 0x04001EE0 RID: 7904
			System = 1
		}
	}
}
