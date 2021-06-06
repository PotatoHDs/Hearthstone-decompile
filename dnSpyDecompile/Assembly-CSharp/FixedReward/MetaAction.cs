using System;

namespace FixedReward
{
	// Token: 0x02000B4E RID: 2894
	public class MetaAction
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06009A5E RID: 39518 RVA: 0x0031B08C File Offset: 0x0031928C
		public int MetaActionID { get; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06009A5F RID: 39519 RVA: 0x0031B094 File Offset: 0x00319294
		// (set) Token: 0x06009A60 RID: 39520 RVA: 0x0031B09C File Offset: 0x0031929C
		public ulong MetaActionFlags { get; private set; }

		// Token: 0x06009A61 RID: 39521 RVA: 0x0031B0A5 File Offset: 0x003192A5
		public MetaAction(int metaActionID)
		{
			this.MetaActionID = metaActionID;
			this.MetaActionFlags = 0UL;
		}

		// Token: 0x06009A62 RID: 39522 RVA: 0x0031B0BC File Offset: 0x003192BC
		public void UpdateFlags(ulong addFlags, ulong removeFlags)
		{
			this.MetaActionFlags |= addFlags;
			this.MetaActionFlags &= ~removeFlags;
		}

		// Token: 0x06009A63 RID: 39523 RVA: 0x0031B0DB File Offset: 0x003192DB
		public bool HasAllRequiredFlags(ulong requiredFlags)
		{
			return (this.MetaActionFlags & requiredFlags) == requiredFlags;
		}
	}
}
