using System;

// Token: 0x02000026 RID: 38
public class PageNode
{
	// Token: 0x06000129 RID: 297 RVA: 0x000077B1 File Offset: 0x000059B1
	public PageNode(PageData data)
	{
		this.PageData = data;
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600012A RID: 298 RVA: 0x000077C0 File Offset: 0x000059C0
	// (set) Token: 0x0600012B RID: 299 RVA: 0x000077C8 File Offset: 0x000059C8
	public PageData PageData { get; private set; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600012C RID: 300 RVA: 0x000077D1 File Offset: 0x000059D1
	// (set) Token: 0x0600012D RID: 301 RVA: 0x000077D9 File Offset: 0x000059D9
	public PageNode PageToLeft { get; set; }

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600012E RID: 302 RVA: 0x000077E2 File Offset: 0x000059E2
	// (set) Token: 0x0600012F RID: 303 RVA: 0x000077EA File Offset: 0x000059EA
	public PageNode PageToRight { get; set; }
}
