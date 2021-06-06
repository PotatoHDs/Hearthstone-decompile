using System;
using bgs.types;
using bnet.protocol;
using bnet.protocol.channel.v2;

namespace bgs
{
	// Token: 0x02000243 RID: 579
	public class PartyId
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00080CDC File Offset: 0x0007EEDC
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		public ulong Hi { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00080CED File Offset: 0x0007EEED
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x00080CF5 File Offset: 0x0007EEF5
		public ulong Lo { get; private set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x00080CFE File Offset: 0x0007EEFE
		public bool IsEmpty
		{
			get
			{
				return this.Hi == 0UL && this.Lo == 0UL;
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00080D14 File Offset: 0x0007EF14
		public PartyId()
		{
			this.Hi = (this.Lo = 0UL);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00080D38 File Offset: 0x0007EF38
		public PartyId(ulong highBits, ulong lowBits)
		{
			this.Set(highBits, lowBits);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00080D48 File Offset: 0x0007EF48
		public PartyId(bgs.types.EntityId partyEntityId)
		{
			this.Set(partyEntityId.hi, partyEntityId.lo);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00080D62 File Offset: 0x0007EF62
		public void Set(ulong highBits, ulong lowBits)
		{
			this.Hi = highBits;
			this.Lo = lowBits;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00080D72 File Offset: 0x0007EF72
		public static implicit operator PartyId(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return null;
			}
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00080D90 File Offset: 0x0007EF90
		public static bool operator ==(PartyId a, PartyId b)
		{
			if (a == null)
			{
				return b == null;
			}
			return b != null && a.Hi == b.Hi && a.Lo == b.Lo;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00080DBD File Offset: 0x0007EFBD
		public static bool operator !=(PartyId a, PartyId b)
		{
			return !(a == b);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00080DC9 File Offset: 0x0007EFC9
		public override bool Equals(object obj)
		{
			if (obj is PartyId)
			{
				return this == (PartyId)obj;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00080DE8 File Offset: 0x0007EFE8
		public override int GetHashCode()
		{
			return this.Hi.GetHashCode() ^ this.Lo.GetHashCode();
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00080E12 File Offset: 0x0007F012
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.Hi, this.Lo);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00080E34 File Offset: 0x0007F034
		public static PartyId FromEntityId(bgs.types.EntityId entityId)
		{
			return new PartyId(entityId);
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00080E3C File Offset: 0x0007F03C
		public static PartyId FromBnetEntityId(BnetEntityId entityId)
		{
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00080E4F File Offset: 0x0007F04F
		public static PartyId FromProtocol(bnet.protocol.EntityId protoEntityId)
		{
			return new PartyId(protoEntityId.High, protoEntityId.Low);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00080E62 File Offset: 0x0007F062
		public static PartyId FromChannelId(ChannelId channelId)
		{
			if (channelId == null)
			{
				return null;
			}
			return new PartyId((ulong)channelId.Host.Epoch << 32 | (ulong)channelId.Host.Label, (ulong)channelId.Id);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00080E94 File Offset: 0x0007F094
		public bgs.types.EntityId ToEntityId()
		{
			return new bgs.types.EntityId
			{
				hi = this.Hi,
				lo = this.Lo
			};
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00080EC4 File Offset: 0x0007F0C4
		public BnetEntityId ToBnetEntityId()
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetHi(this.Hi);
			bnetEntityId.SetLo(this.Lo);
			return bnetEntityId;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00080EE4 File Offset: 0x0007F0E4
		public ChannelId ToChannelId()
		{
			ChannelId channelId = new ChannelId();
			ProcessId processId = new ProcessId();
			channelId.Id = (uint)this.Lo;
			channelId.SetHost(processId);
			processId.SetLabel((uint)this.Hi & uint.MaxValue);
			processId.SetEpoch((uint)(this.Hi >> 32));
			return channelId;
		}

		// Token: 0x04000EE1 RID: 3809
		public static readonly PartyId Empty = new PartyId(0UL, 0UL);
	}
}
