using System;

namespace bgs
{
	// Token: 0x0200022B RID: 555
	public struct FullMethodId
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0007B6E0 File Offset: 0x000798E0
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x0007B6E8 File Offset: 0x000798E8
		public uint ServiceHash { get; private set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0007B6F1 File Offset: 0x000798F1
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x0007B6F9 File Offset: 0x000798F9
		public uint MethodId { get; private set; }

		// Token: 0x06002337 RID: 9015 RVA: 0x0007B702 File Offset: 0x00079902
		public FullMethodId(uint serviceHash, uint methodId)
		{
			this.ServiceHash = serviceHash;
			this.MethodId = methodId;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0007B714 File Offset: 0x00079914
		public override bool Equals(object obj)
		{
			if (!(obj is FullMethodId))
			{
				return false;
			}
			FullMethodId fullMethodId = (FullMethodId)obj;
			return this.ServiceHash == fullMethodId.ServiceHash && this.MethodId == fullMethodId.MethodId;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0007B754 File Offset: 0x00079954
		public override int GetHashCode()
		{
			return this.ServiceHash.GetHashCode() ^ this.MethodId.GetHashCode();
		}
	}
}
