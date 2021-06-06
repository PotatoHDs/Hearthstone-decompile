using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000067 RID: 103
	public class CancelPurchase : IProtoBuf
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00019022 File Offset: 0x00017222
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001902A File Offset: 0x0001722A
		public bool IsAutoCancel { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00019033 File Offset: 0x00017233
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001903B File Offset: 0x0001723B
		public CancelPurchase.CancelReason Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001904B File Offset: 0x0001724B
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00019053 File Offset: 0x00017253
		public string DeviceId { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001905C File Offset: 0x0001725C
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00019064 File Offset: 0x00017264
		public string ErrorMessage
		{
			get
			{
				return this._ErrorMessage;
			}
			set
			{
				this._ErrorMessage = value;
				this.HasErrorMessage = (value != null);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00019078 File Offset: 0x00017278
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.IsAutoCancel.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			num ^= this.DeviceId.GetHashCode();
			if (this.HasErrorMessage)
			{
				num ^= this.ErrorMessage.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000190E8 File Offset: 0x000172E8
		public override bool Equals(object obj)
		{
			CancelPurchase cancelPurchase = obj as CancelPurchase;
			return cancelPurchase != null && this.IsAutoCancel.Equals(cancelPurchase.IsAutoCancel) && this.HasReason == cancelPurchase.HasReason && (!this.HasReason || this.Reason.Equals(cancelPurchase.Reason)) && this.DeviceId.Equals(cancelPurchase.DeviceId) && this.HasErrorMessage == cancelPurchase.HasErrorMessage && (!this.HasErrorMessage || this.ErrorMessage.Equals(cancelPurchase.ErrorMessage));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00019193 File Offset: 0x00017393
		public void Deserialize(Stream stream)
		{
			CancelPurchase.Deserialize(stream, this);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001919D File Offset: 0x0001739D
		public static CancelPurchase Deserialize(Stream stream, CancelPurchase instance)
		{
			return CancelPurchase.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000191A8 File Offset: 0x000173A8
		public static CancelPurchase DeserializeLengthDelimited(Stream stream)
		{
			CancelPurchase cancelPurchase = new CancelPurchase();
			CancelPurchase.DeserializeLengthDelimited(stream, cancelPurchase);
			return cancelPurchase;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000191C4 File Offset: 0x000173C4
		public static CancelPurchase DeserializeLengthDelimited(Stream stream, CancelPurchase instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelPurchase.Deserialize(stream, instance, num);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000191EC File Offset: 0x000173EC
		public static CancelPurchase Deserialize(Stream stream, CancelPurchase instance, long limit)
		{
			instance.Reason = CancelPurchase.CancelReason.PROVIDER_REPORTED_FAILURE;
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.IsAutoCancel = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Reason = (CancelPurchase.CancelReason)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.DeviceId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.ErrorMessage = ProtocolParser.ReadString(stream);
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

		// Token: 0x06000696 RID: 1686 RVA: 0x000192C4 File Offset: 0x000174C4
		public void Serialize(Stream stream)
		{
			CancelPurchase.Serialize(stream, this);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000192D0 File Offset: 0x000174D0
		public static void Serialize(Stream stream, CancelPurchase instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.IsAutoCancel);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			if (instance.HasErrorMessage)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorMessage));
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001936C File Offset: 0x0001756C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 1U;
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasErrorMessage)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ErrorMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2U;
		}

		// Token: 0x04000231 RID: 561
		public bool HasReason;

		// Token: 0x04000232 RID: 562
		private CancelPurchase.CancelReason _Reason;

		// Token: 0x04000234 RID: 564
		public bool HasErrorMessage;

		// Token: 0x04000235 RID: 565
		private string _ErrorMessage;

		// Token: 0x02000579 RID: 1401
		public enum PacketID
		{
			// Token: 0x04001EC1 RID: 7873
			ID = 274,
			// Token: 0x04001EC2 RID: 7874
			System = 1
		}

		// Token: 0x0200057A RID: 1402
		public enum CancelReason
		{
			// Token: 0x04001EC4 RID: 7876
			PROVIDER_REPORTED_FAILURE = 1,
			// Token: 0x04001EC5 RID: 7877
			NOT_RECOGNIZED_BY_PROVIDER,
			// Token: 0x04001EC6 RID: 7878
			USER_CANCELED_BEFORE_PAYMENT,
			// Token: 0x04001EC7 RID: 7879
			USER_CANCELING_TO_UNBLOCK,
			// Token: 0x04001EC8 RID: 7880
			CHALLENGE_TIMEOUT,
			// Token: 0x04001EC9 RID: 7881
			CHALLENGE_DENIED,
			// Token: 0x04001ECA RID: 7882
			CHALLENGE_OTHER_ERROR,
			// Token: 0x04001ECB RID: 7883
			LEGACY_PURCHASE_ATTEMPT
		}
	}
}
