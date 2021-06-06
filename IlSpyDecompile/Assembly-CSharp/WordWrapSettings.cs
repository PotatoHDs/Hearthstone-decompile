public struct WordWrapSettings
{
	public float OriginalWidth;

	public float Width;

	public float Height;

	public bool Ellipsized;

	public bool RichText;

	public bool ResizeToFit;

	public bool ForceWrapLargeWords;

	public UberText.AlignmentOptions Alignment;

	public bool UseUnderwear;

	public float UnderwearHeight;

	public float UnderwearWidth;

	public float UnderwearHeightLocaleAdjustment;

	public float UnderwearWidthLocaleAdjustment;

	public bool UnderwearFlip;

	public void Clear()
	{
		Height = 0f;
		Width = 0f;
		UnderwearWidth = 0f;
		UnderwearHeight = 0f;
		UnderwearHeightLocaleAdjustment = 0f;
		UnderwearWidthLocaleAdjustment = 0f;
		UseUnderwear = false;
		UnderwearFlip = false;
		Ellipsized = false;
		RichText = false;
		ResizeToFit = false;
		ForceWrapLargeWords = false;
		Alignment = UberText.AlignmentOptions.Center;
	}
}
