public static class MaterialReferenceExtensions
{
	public static bool IsInitialized(this MaterialReference materialReference)
	{
		if (materialReference != null)
		{
			return !string.IsNullOrWhiteSpace(materialReference.MaterialRef);
		}
		return false;
	}
}
