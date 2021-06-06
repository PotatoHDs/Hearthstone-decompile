using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200006E RID: 110
	public class SubmitThirdPartyReceipt : IProtoBuf
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00019F40 File Offset: 0x00018140
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00019F48 File Offset: 0x00018148
		public BattlePayProvider Provider { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00019F51 File Offset: 0x00018151
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00019F59 File Offset: 0x00018159
		public string ProductId { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00019F62 File Offset: 0x00018162
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00019F6A File Offset: 0x0001816A
		public int Quantity { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00019F73 File Offset: 0x00018173
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00019F7B File Offset: 0x0001817B
		public long TransactionId { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00019F84 File Offset: 0x00018184
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00019F8C File Offset: 0x0001818C
		public ThirdPartyReceiptData ReceiptData { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00019F95 File Offset: 0x00018195
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00019F9D File Offset: 0x0001819D
		public string DeviceId { get; set; }

		// Token: 0x060006FA RID: 1786 RVA: 0x00019FA8 File Offset: 0x000181A8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Provider.GetHashCode() ^ this.ProductId.GetHashCode() ^ this.Quantity.GetHashCode() ^ this.TransactionId.GetHashCode() ^ this.ReceiptData.GetHashCode() ^ this.DeviceId.GetHashCode();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001A018 File Offset: 0x00018218
		public override bool Equals(object obj)
		{
			SubmitThirdPartyReceipt submitThirdPartyReceipt = obj as SubmitThirdPartyReceipt;
			return submitThirdPartyReceipt != null && this.Provider.Equals(submitThirdPartyReceipt.Provider) && this.ProductId.Equals(submitThirdPartyReceipt.ProductId) && this.Quantity.Equals(submitThirdPartyReceipt.Quantity) && this.TransactionId.Equals(submitThirdPartyReceipt.TransactionId) && this.ReceiptData.Equals(submitThirdPartyReceipt.ReceiptData) && this.DeviceId.Equals(submitThirdPartyReceipt.DeviceId);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A0C4 File Offset: 0x000182C4
		public void Deserialize(Stream stream)
		{
			SubmitThirdPartyReceipt.Deserialize(stream, this);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A0CE File Offset: 0x000182CE
		public static SubmitThirdPartyReceipt Deserialize(Stream stream, SubmitThirdPartyReceipt instance)
		{
			return SubmitThirdPartyReceipt.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static SubmitThirdPartyReceipt DeserializeLengthDelimited(Stream stream)
		{
			SubmitThirdPartyReceipt submitThirdPartyReceipt = new SubmitThirdPartyReceipt();
			SubmitThirdPartyReceipt.DeserializeLengthDelimited(stream, submitThirdPartyReceipt);
			return submitThirdPartyReceipt;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001A0F8 File Offset: 0x000182F8
		public static SubmitThirdPartyReceipt DeserializeLengthDelimited(Stream stream, SubmitThirdPartyReceipt instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubmitThirdPartyReceipt.Deserialize(stream, instance, num);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001A120 File Offset: 0x00018320
		public static SubmitThirdPartyReceipt Deserialize(Stream stream, SubmitThirdPartyReceipt instance, long limit)
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
				else
				{
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.Provider = (BattlePayProvider)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num != 42)
						{
							if (num == 50)
							{
								instance.DeviceId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.ReceiptData == null)
							{
								instance.ReceiptData = ThirdPartyReceiptData.DeserializeLengthDelimited(stream);
								continue;
							}
							ThirdPartyReceiptData.DeserializeLengthDelimited(stream, instance.ReceiptData);
							continue;
						}
					}
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

		// Token: 0x06000701 RID: 1793 RVA: 0x0001A23E File Offset: 0x0001843E
		public void Serialize(Stream stream)
		{
			SubmitThirdPartyReceipt.Serialize(stream, this);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001A248 File Offset: 0x00018448
		public static void Serialize(Stream stream, SubmitThirdPartyReceipt instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Provider));
			if (instance.ProductId == null)
			{
				throw new ArgumentNullException("ProductId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			if (instance.ReceiptData == null)
			{
				throw new ArgumentNullException("ReceiptData", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.ReceiptData.GetSerializedSize());
			ThirdPartyReceiptData.Serialize(stream, instance.ReceiptData);
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(50);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001A33C File Offset: 0x0001853C
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Provider));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity)) + ProtocolParser.SizeOfUInt64((ulong)this.TransactionId);
			uint serializedSize = this.ReceiptData.GetSerializedSize();
			uint num3 = num2 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
			return num3 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 6U;
		}

		// Token: 0x02000580 RID: 1408
		public enum PacketID
		{
			// Token: 0x04001EDC RID: 7900
			ID = 293,
			// Token: 0x04001EDD RID: 7901
			System = 1
		}
	}
}
