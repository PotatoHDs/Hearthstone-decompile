using System;

namespace bgs.types
{
	// Token: 0x0200026F RID: 623
	public class GameServerInfo
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00085F85 File Offset: 0x00084185
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x00085F8D File Offset: 0x0008418D
		public string Address { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x00085F96 File Offset: 0x00084196
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x00085F9E File Offset: 0x0008419E
		public uint Port { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00085FA7 File Offset: 0x000841A7
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x00085FAF File Offset: 0x000841AF
		public uint GameHandle { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00085FB8 File Offset: 0x000841B8
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x00085FC0 File Offset: 0x000841C0
		public long ClientHandle { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00085FC9 File Offset: 0x000841C9
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x00085FD1 File Offset: 0x000841D1
		public string AuroraPassword { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00085FDA File Offset: 0x000841DA
		// (set) Token: 0x060025BF RID: 9663 RVA: 0x00085FE2 File Offset: 0x000841E2
		public string Version { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x00085FEB File Offset: 0x000841EB
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x00085FF3 File Offset: 0x000841F3
		public int Mission { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00085FFC File Offset: 0x000841FC
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x00086004 File Offset: 0x00084204
		public int BrawlLibraryItemId { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x0008600D File Offset: 0x0008420D
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x00086015 File Offset: 0x00084215
		public bool Resumable { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x0008601E File Offset: 0x0008421E
		// (set) Token: 0x060025C7 RID: 9671 RVA: 0x00086026 File Offset: 0x00084226
		public string SpectatorPassword { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0008602F File Offset: 0x0008422F
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x00086037 File Offset: 0x00084237
		public bool SpectatorMode { get; set; }
	}
}
