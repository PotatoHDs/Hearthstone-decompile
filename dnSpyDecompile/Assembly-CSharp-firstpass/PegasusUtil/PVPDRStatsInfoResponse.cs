using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000112 RID: 274
	public class PVPDRStatsInfoResponse : IProtoBuf
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x000401DD File Offset: 0x0003E3DD
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x000401E5 File Offset: 0x0003E3E5
		public int Rating
		{
			get
			{
				return this._Rating;
			}
			set
			{
				this._Rating = value;
				this.HasRating = true;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000401F5 File Offset: 0x0003E3F5
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x000401FD File Offset: 0x0003E3FD
		public int HighWatermark
		{
			get
			{
				return this._HighWatermark;
			}
			set
			{
				this._HighWatermark = value;
				this.HasHighWatermark = true;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004020D File Offset: 0x0003E40D
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00040215 File Offset: 0x0003E415
		public int PaidRating
		{
			get
			{
				return this._PaidRating;
			}
			set
			{
				this._PaidRating = value;
				this.HasPaidRating = true;
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00040228 File Offset: 0x0003E428
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRating)
			{
				num ^= this.Rating.GetHashCode();
			}
			if (this.HasHighWatermark)
			{
				num ^= this.HighWatermark.GetHashCode();
			}
			if (this.HasPaidRating)
			{
				num ^= this.PaidRating.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00040290 File Offset: 0x0003E490
		public override bool Equals(object obj)
		{
			PVPDRStatsInfoResponse pvpdrstatsInfoResponse = obj as PVPDRStatsInfoResponse;
			return pvpdrstatsInfoResponse != null && this.HasRating == pvpdrstatsInfoResponse.HasRating && (!this.HasRating || this.Rating.Equals(pvpdrstatsInfoResponse.Rating)) && this.HasHighWatermark == pvpdrstatsInfoResponse.HasHighWatermark && (!this.HasHighWatermark || this.HighWatermark.Equals(pvpdrstatsInfoResponse.HighWatermark)) && this.HasPaidRating == pvpdrstatsInfoResponse.HasPaidRating && (!this.HasPaidRating || this.PaidRating.Equals(pvpdrstatsInfoResponse.PaidRating));
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00040334 File Offset: 0x0003E534
		public void Deserialize(Stream stream)
		{
			PVPDRStatsInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004033E File Offset: 0x0003E53E
		public static PVPDRStatsInfoResponse Deserialize(Stream stream, PVPDRStatsInfoResponse instance)
		{
			return PVPDRStatsInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004034C File Offset: 0x0003E54C
		public static PVPDRStatsInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRStatsInfoResponse pvpdrstatsInfoResponse = new PVPDRStatsInfoResponse();
			PVPDRStatsInfoResponse.DeserializeLengthDelimited(stream, pvpdrstatsInfoResponse);
			return pvpdrstatsInfoResponse;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00040368 File Offset: 0x0003E568
		public static PVPDRStatsInfoResponse DeserializeLengthDelimited(Stream stream, PVPDRStatsInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRStatsInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00040390 File Offset: 0x0003E590
		public static PVPDRStatsInfoResponse Deserialize(Stream stream, PVPDRStatsInfoResponse instance, long limit)
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
							instance.PaidRating = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.HighWatermark = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Rating = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00040440 File Offset: 0x0003E640
		public void Serialize(Stream stream)
		{
			PVPDRStatsInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004044C File Offset: 0x0003E64C
		public static void Serialize(Stream stream, PVPDRStatsInfoResponse instance)
		{
			if (instance.HasRating)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Rating));
			}
			if (instance.HasHighWatermark)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HighWatermark));
			}
			if (instance.HasPaidRating)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PaidRating));
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000404B0 File Offset: 0x0003E6B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRating)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Rating));
			}
			if (this.HasHighWatermark)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HighWatermark));
			}
			if (this.HasPaidRating)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PaidRating));
			}
			return num;
		}

		// Token: 0x040005A1 RID: 1441
		public bool HasRating;

		// Token: 0x040005A2 RID: 1442
		private int _Rating;

		// Token: 0x040005A3 RID: 1443
		public bool HasHighWatermark;

		// Token: 0x040005A4 RID: 1444
		private int _HighWatermark;

		// Token: 0x040005A5 RID: 1445
		public bool HasPaidRating;

		// Token: 0x040005A6 RID: 1446
		private int _PaidRating;

		// Token: 0x02000612 RID: 1554
		public enum PacketID
		{
			// Token: 0x04002071 RID: 8305
			ID = 379,
			// Token: 0x04002072 RID: 8306
			System = 0
		}
	}
}
