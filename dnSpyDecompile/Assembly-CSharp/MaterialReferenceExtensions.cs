using System;

// Token: 0x020008EA RID: 2282
public static class MaterialReferenceExtensions
{
	// Token: 0x06007E90 RID: 32400 RVA: 0x0028F3AF File Offset: 0x0028D5AF
	public static bool IsInitialized(this MaterialReference materialReference)
	{
		return materialReference != null && !string.IsNullOrWhiteSpace(materialReference.MaterialRef);
	}
}
