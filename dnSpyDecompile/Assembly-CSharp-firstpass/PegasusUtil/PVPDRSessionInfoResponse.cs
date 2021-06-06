using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000111 RID: 273
	public class PVPDRSessionInfoResponse : IProtoBuf
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0003FE6F File Offset: 0x0003E06F
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0003FE77 File Offset: 0x0003E077
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0003FE80 File Offset: 0x0003E080
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0003FE88 File Offset: 0x0003E088
		public PVPDRSeasonInfo CurrentSeason
		{
			get
			{
				return this._CurrentSeason;
			}
			set
			{
				this._CurrentSeason = value;
				this.HasCurrentSeason = (value != null);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003FE9B File Offset: 0x0003E09B
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0003FEA3 File Offset: 0x0003E0A3
		public PVPDRSessionInfo Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
				this.HasSession = (value != null);
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasCurrentSeason)
			{
				num ^= this.CurrentSeason.GetHashCode();
			}
			if (this.HasSession)
			{
				num ^= this.Session.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003FF18 File Offset: 0x0003E118
		public override bool Equals(object obj)
		{
			PVPDRSessionInfoResponse pvpdrsessionInfoResponse = obj as PVPDRSessionInfoResponse;
			return pvpdrsessionInfoResponse != null && this.ErrorCode.Equals(pvpdrsessionInfoResponse.ErrorCode) && this.HasCurrentSeason == pvpdrsessionInfoResponse.HasCurrentSeason && (!this.HasCurrentSeason || this.CurrentSeason.Equals(pvpdrsessionInfoResponse.CurrentSeason)) && this.HasSession == pvpdrsessionInfoResponse.HasSession && (!this.HasSession || this.Session.Equals(pvpdrsessionInfoResponse.Session));
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003FFAB File Offset: 0x0003E1AB
		public void Deserialize(Stream stream)
		{
			PVPDRSessionInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003FFB5 File Offset: 0x0003E1B5
		public static PVPDRSessionInfoResponse Deserialize(Stream stream, PVPDRSessionInfoResponse instance)
		{
			return PVPDRSessionInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003FFC0 File Offset: 0x0003E1C0
		public static PVPDRSessionInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionInfoResponse pvpdrsessionInfoResponse = new PVPDRSessionInfoResponse();
			PVPDRSessionInfoResponse.DeserializeLengthDelimited(stream, pvpdrsessionInfoResponse);
			return pvpdrsessionInfoResponse;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003FFDC File Offset: 0x0003E1DC
		public static PVPDRSessionInfoResponse DeserializeLengthDelimited(Stream stream, PVPDRSessionInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00040004 File Offset: 0x0003E204
		public static PVPDRSessionInfoResponse Deserialize(Stream stream, PVPDRSessionInfoResponse instance, long limit)
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
						else if (instance.Session == null)
						{
							instance.Session = PVPDRSessionInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							PVPDRSessionInfo.DeserializeLengthDelimited(stream, instance.Session);
						}
					}
					else if (instance.CurrentSeason == null)
					{
						instance.CurrentSeason = PVPDRSeasonInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						PVPDRSeasonInfo.DeserializeLengthDelimited(stream, instance.CurrentSeason);
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

		// Token: 0x06001226 RID: 4646 RVA: 0x000400EC File Offset: 0x0003E2EC
		public void Serialize(Stream stream)
		{
			PVPDRSessionInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000400F8 File Offset: 0x0003E2F8
		public static void Serialize(Stream stream, PVPDRSessionInfoResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.HasCurrentSeason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSeason.GetSerializedSize());
				PVPDRSeasonInfo.Serialize(stream, instance.CurrentSeason);
			}
			if (instance.HasSession)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				PVPDRSessionInfo.Serialize(stream, instance.Session);
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00040174 File Offset: 0x0003E374
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.HasCurrentSeason)
			{
				num += 1U;
				uint serializedSize = this.CurrentSeason.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSession)
			{
				num += 1U;
				uint serializedSize2 = this.Session.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x0400059D RID: 1437
		public bool HasCurrentSeason;

		// Token: 0x0400059E RID: 1438
		private PVPDRSeasonInfo _CurrentSeason;

		// Token: 0x0400059F RID: 1439
		public bool HasSession;

		// Token: 0x040005A0 RID: 1440
		private PVPDRSessionInfo _Session;

		// Token: 0x02000611 RID: 1553
		public enum PacketID
		{
			// Token: 0x0400206F RID: 8303
			ID = 377
		}
	}
}
