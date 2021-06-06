using System;

// Token: 0x02000020 RID: 32
public abstract class PageData
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000E2 RID: 226
	public abstract AdventureBookPageType PageType { get; }

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x000052BD File Offset: 0x000034BD
	// (set) Token: 0x060000E4 RID: 228 RVA: 0x000052C5 File Offset: 0x000034C5
	public int BookSection { get; set; }

	// Token: 0x04000091 RID: 145
	public AdventureDbId Adventure;

	// Token: 0x04000092 RID: 146
	public AdventureModeDbId AdventureMode;

	// Token: 0x04000094 RID: 148
	public const int NO_SECTION = -1;
}
