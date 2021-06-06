using System;
using bnet.protocol;

namespace bgs.types
{
	// Token: 0x02000270 RID: 624
	public struct EntityId
	{
		// Token: 0x060025CB RID: 9675 RVA: 0x00086040 File Offset: 0x00084240
		public EntityId ToProtocol()
		{
			EntityId entityId = new EntityId();
			entityId.SetHigh(this.hi);
			entityId.SetLow(this.lo);
			return entityId;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008605F File Offset: 0x0008425F
		public EntityId(EntityId copyFrom)
		{
			this.hi = copyFrom.hi;
			this.lo = copyFrom.lo;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00086079 File Offset: 0x00084279
		public EntityId(EntityId protoEntityId)
		{
			this.hi = protoEntityId.High;
			this.lo = protoEntityId.Low;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x00086093 File Offset: 0x00084293
		public override bool Equals(object obj)
		{
			if (obj is EntityId)
			{
				return this == (EntityId)obj;
			}
			return base.Equals(obj);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000860C0 File Offset: 0x000842C0
		public override int GetHashCode()
		{
			return this.hi.GetHashCode() ^ this.lo.GetHashCode();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000860D9 File Offset: 0x000842D9
		public static bool operator ==(EntityId a, EntityId b)
		{
			return a.hi == b.hi && a.lo == b.lo;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000860F9 File Offset: 0x000842F9
		public static bool operator !=(EntityId a, EntityId b)
		{
			return !(a == b);
		}

		// Token: 0x04000FC7 RID: 4039
		public ulong hi;

		// Token: 0x04000FC8 RID: 4040
		public ulong lo;
	}
}
