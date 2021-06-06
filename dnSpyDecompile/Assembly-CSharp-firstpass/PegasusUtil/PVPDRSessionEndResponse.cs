using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000115 RID: 277
	public class PVPDRSessionEndResponse : IProtoBuf
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00040829 File Offset: 0x0003EA29
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x00040831 File Offset: 0x0003EA31
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0004083A File Offset: 0x0003EA3A
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00040842 File Offset: 0x0003EA42
		public int NewRating
		{
			get
			{
				return this._NewRating;
			}
			set
			{
				this._NewRating = value;
				this.HasNewRating = true;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00040852 File Offset: 0x0003EA52
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0004085A File Offset: 0x0003EA5A
		public int NewPaidRating
		{
			get
			{
				return this._NewPaidRating;
			}
			set
			{
				this._NewPaidRating = value;
				this.HasNewPaidRating = true;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004086C File Offset: 0x0003EA6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasNewRating)
			{
				num ^= this.NewRating.GetHashCode();
			}
			if (this.HasNewPaidRating)
			{
				num ^= this.NewPaidRating.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000408D0 File Offset: 0x0003EAD0
		public override bool Equals(object obj)
		{
			PVPDRSessionEndResponse pvpdrsessionEndResponse = obj as PVPDRSessionEndResponse;
			return pvpdrsessionEndResponse != null && this.ErrorCode.Equals(pvpdrsessionEndResponse.ErrorCode) && this.HasNewRating == pvpdrsessionEndResponse.HasNewRating && (!this.HasNewRating || this.NewRating.Equals(pvpdrsessionEndResponse.NewRating)) && this.HasNewPaidRating == pvpdrsessionEndResponse.HasNewPaidRating && (!this.HasNewPaidRating || this.NewPaidRating.Equals(pvpdrsessionEndResponse.NewPaidRating));
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00040969 File Offset: 0x0003EB69
		public void Deserialize(Stream stream)
		{
			PVPDRSessionEndResponse.Deserialize(stream, this);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00040973 File Offset: 0x0003EB73
		public static PVPDRSessionEndResponse Deserialize(Stream stream, PVPDRSessionEndResponse instance)
		{
			return PVPDRSessionEndResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00040980 File Offset: 0x0003EB80
		public static PVPDRSessionEndResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionEndResponse pvpdrsessionEndResponse = new PVPDRSessionEndResponse();
			PVPDRSessionEndResponse.DeserializeLengthDelimited(stream, pvpdrsessionEndResponse);
			return pvpdrsessionEndResponse;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004099C File Offset: 0x0003EB9C
		public static PVPDRSessionEndResponse DeserializeLengthDelimited(Stream stream, PVPDRSessionEndResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionEndResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000409C4 File Offset: 0x0003EBC4
		public static PVPDRSessionEndResponse Deserialize(Stream stream, PVPDRSessionEndResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
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
							instance.NewPaidRating = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.NewRating = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00040A74 File Offset: 0x0003EC74
		public void Serialize(Stream stream)
		{
			PVPDRSessionEndResponse.Serialize(stream, this);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00040A80 File Offset: 0x0003EC80
		public static void Serialize(Stream stream, PVPDRSessionEndResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.HasNewRating)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NewRating));
			}
			if (instance.HasNewPaidRating)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NewPaidRating));
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00040ADC File Offset: 0x0003ECDC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.HasNewRating)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NewRating));
			}
			if (this.HasNewPaidRating)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NewPaidRating));
			}
			return num + 1U;
		}

		// Token: 0x040005AA RID: 1450
		public bool HasNewRating;

		// Token: 0x040005AB RID: 1451
		private int _NewRating;

		// Token: 0x040005AC RID: 1452
		public bool HasNewPaidRating;

		// Token: 0x040005AD RID: 1453
		private int _NewPaidRating;

		// Token: 0x02000615 RID: 1557
		public enum PacketID
		{
			// Token: 0x04002078 RID: 8312
			ID = 389
		}
	}
}
