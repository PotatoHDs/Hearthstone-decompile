using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x0200006D RID: 109
	public class ThirdPartyReceiptData : IProtoBuf
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00019C21 File Offset: 0x00017E21
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00019C29 File Offset: 0x00017E29
		public string ThirdPartyId { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00019C32 File Offset: 0x00017E32
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00019C3A File Offset: 0x00017E3A
		public string Receipt { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00019C43 File Offset: 0x00017E43
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00019C4B File Offset: 0x00017E4B
		public string ThirdPartyUserId
		{
			get
			{
				return this._ThirdPartyUserId;
			}
			set
			{
				this._ThirdPartyUserId = value;
				this.HasThirdPartyUserId = (value != null);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00019C60 File Offset: 0x00017E60
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ThirdPartyId.GetHashCode();
			num ^= this.Receipt.GetHashCode();
			if (this.HasThirdPartyUserId)
			{
				num ^= this.ThirdPartyUserId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00019CAC File Offset: 0x00017EAC
		public override bool Equals(object obj)
		{
			ThirdPartyReceiptData thirdPartyReceiptData = obj as ThirdPartyReceiptData;
			return thirdPartyReceiptData != null && this.ThirdPartyId.Equals(thirdPartyReceiptData.ThirdPartyId) && this.Receipt.Equals(thirdPartyReceiptData.Receipt) && this.HasThirdPartyUserId == thirdPartyReceiptData.HasThirdPartyUserId && (!this.HasThirdPartyUserId || this.ThirdPartyUserId.Equals(thirdPartyReceiptData.ThirdPartyUserId));
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00019D1B File Offset: 0x00017F1B
		public void Deserialize(Stream stream)
		{
			ThirdPartyReceiptData.Deserialize(stream, this);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00019D25 File Offset: 0x00017F25
		public static ThirdPartyReceiptData Deserialize(Stream stream, ThirdPartyReceiptData instance)
		{
			return ThirdPartyReceiptData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00019D30 File Offset: 0x00017F30
		public static ThirdPartyReceiptData DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyReceiptData thirdPartyReceiptData = new ThirdPartyReceiptData();
			ThirdPartyReceiptData.DeserializeLengthDelimited(stream, thirdPartyReceiptData);
			return thirdPartyReceiptData;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019D4C File Offset: 0x00017F4C
		public static ThirdPartyReceiptData DeserializeLengthDelimited(Stream stream, ThirdPartyReceiptData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyReceiptData.Deserialize(stream, instance, num);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00019D74 File Offset: 0x00017F74
		public static ThirdPartyReceiptData Deserialize(Stream stream, ThirdPartyReceiptData instance, long limit)
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
					if (num != 18)
					{
						if (num != 26)
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
							instance.ThirdPartyUserId = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Receipt = ProtocolParser.ReadString(stream);
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

		// Token: 0x060006EA RID: 1770 RVA: 0x00019E22 File Offset: 0x00018022
		public void Serialize(Stream stream)
		{
			ThirdPartyReceiptData.Serialize(stream, this);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00019E2C File Offset: 0x0001802C
		public static void Serialize(Stream stream, ThirdPartyReceiptData instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			if (instance.Receipt == null)
			{
				throw new ArgumentNullException("Receipt", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Receipt));
			if (instance.HasThirdPartyUserId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyUserId));
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019ECC File Offset: 0x000180CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Receipt);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.HasThirdPartyUserId)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyUserId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num + 2U;
		}

		// Token: 0x04000240 RID: 576
		public bool HasThirdPartyUserId;

		// Token: 0x04000241 RID: 577
		private string _ThirdPartyUserId;
	}
}
