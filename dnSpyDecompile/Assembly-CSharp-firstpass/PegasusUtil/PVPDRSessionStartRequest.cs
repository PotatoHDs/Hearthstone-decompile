using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000081 RID: 129
	public class PVPDRSessionStartRequest : IProtoBuf
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001CAD6 File Offset: 0x0001ACD6
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001CADE File Offset: 0x0001ACDE
		public bool PaidEntry
		{
			get
			{
				return this._PaidEntry;
			}
			set
			{
				this._PaidEntry = value;
				this.HasPaidEntry = true;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPaidEntry)
			{
				num ^= this.PaidEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001CB24 File Offset: 0x0001AD24
		public override bool Equals(object obj)
		{
			PVPDRSessionStartRequest pvpdrsessionStartRequest = obj as PVPDRSessionStartRequest;
			return pvpdrsessionStartRequest != null && this.HasPaidEntry == pvpdrsessionStartRequest.HasPaidEntry && (!this.HasPaidEntry || this.PaidEntry.Equals(pvpdrsessionStartRequest.PaidEntry));
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public void Deserialize(Stream stream)
		{
			PVPDRSessionStartRequest.Deserialize(stream, this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001CB76 File Offset: 0x0001AD76
		public static PVPDRSessionStartRequest Deserialize(Stream stream, PVPDRSessionStartRequest instance)
		{
			return PVPDRSessionStartRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001CB84 File Offset: 0x0001AD84
		public static PVPDRSessionStartRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionStartRequest pvpdrsessionStartRequest = new PVPDRSessionStartRequest();
			PVPDRSessionStartRequest.DeserializeLengthDelimited(stream, pvpdrsessionStartRequest);
			return pvpdrsessionStartRequest;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		public static PVPDRSessionStartRequest DeserializeLengthDelimited(Stream stream, PVPDRSessionStartRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionStartRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		public static PVPDRSessionStartRequest Deserialize(Stream stream, PVPDRSessionStartRequest instance, long limit)
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
				else if (num == 8)
				{
					instance.PaidEntry = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06000804 RID: 2052 RVA: 0x0001CC47 File Offset: 0x0001AE47
		public void Serialize(Stream stream)
		{
			PVPDRSessionStartRequest.Serialize(stream, this);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001CC50 File Offset: 0x0001AE50
		public static void Serialize(Stream stream, PVPDRSessionStartRequest instance)
		{
			if (instance.HasPaidEntry)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.PaidEntry);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001CC70 File Offset: 0x0001AE70
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPaidEntry)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000268 RID: 616
		public bool HasPaidEntry;

		// Token: 0x04000269 RID: 617
		private bool _PaidEntry;

		// Token: 0x02000594 RID: 1428
		public enum PacketID
		{
			// Token: 0x04001F18 RID: 7960
			ID = 382,
			// Token: 0x04001F19 RID: 7961
			System = 0
		}
	}
}
