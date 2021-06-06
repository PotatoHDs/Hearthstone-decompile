using System;

// Token: 0x02000AA8 RID: 2728
public struct WordWrapSettings
{
	// Token: 0x06009218 RID: 37400 RVA: 0x002F6DF8 File Offset: 0x002F4FF8
	public void Clear()
	{
		this.Height = 0f;
		this.Width = 0f;
		this.UnderwearWidth = 0f;
		this.UnderwearHeight = 0f;
		this.UnderwearHeightLocaleAdjustment = 0f;
		this.UnderwearWidthLocaleAdjustment = 0f;
		this.UseUnderwear = false;
		this.UnderwearFlip = false;
		this.Ellipsized = false;
		this.RichText = false;
		this.ResizeToFit = false;
		this.ForceWrapLargeWords = false;
		this.Alignment = UberText.AlignmentOptions.Center;
	}

	// Token: 0x04007A90 RID: 31376
	public float OriginalWidth;

	// Token: 0x04007A91 RID: 31377
	public float Width;

	// Token: 0x04007A92 RID: 31378
	public float Height;

	// Token: 0x04007A93 RID: 31379
	public bool Ellipsized;

	// Token: 0x04007A94 RID: 31380
	public bool RichText;

	// Token: 0x04007A95 RID: 31381
	public bool ResizeToFit;

	// Token: 0x04007A96 RID: 31382
	public bool ForceWrapLargeWords;

	// Token: 0x04007A97 RID: 31383
	public UberText.AlignmentOptions Alignment;

	// Token: 0x04007A98 RID: 31384
	public bool UseUnderwear;

	// Token: 0x04007A99 RID: 31385
	public float UnderwearHeight;

	// Token: 0x04007A9A RID: 31386
	public float UnderwearWidth;

	// Token: 0x04007A9B RID: 31387
	public float UnderwearHeightLocaleAdjustment;

	// Token: 0x04007A9C RID: 31388
	public float UnderwearWidthLocaleAdjustment;

	// Token: 0x04007A9D RID: 31389
	public bool UnderwearFlip;
}
