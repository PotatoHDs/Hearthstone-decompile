using System;

namespace bgs
{
	// Token: 0x020001FF RID: 511
	public class ExternalChallenge
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0006D24C File Offset: 0x0006B44C
		// (set) Token: 0x06001F3D RID: 7997 RVA: 0x0006D254 File Offset: 0x0006B454
		public string PayLoadType { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0006D25D File Offset: 0x0006B45D
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0006D265 File Offset: 0x0006B465
		public string URL { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0006D26E File Offset: 0x0006B46E
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0006D276 File Offset: 0x0006B476
		public ExternalChallenge Next { get; set; }
	}
}
