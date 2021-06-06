using System;

namespace bgs
{
	// Token: 0x02000206 RID: 518
	internal class HTTPHeader
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00070BBD File Offset: 0x0006EDBD
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00070BC5 File Offset: 0x0006EDC5
		public int ContentLength { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00070BCE File Offset: 0x0006EDCE
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00070BD6 File Offset: 0x0006EDD6
		public int ContentStart { get; set; }
	}
}
