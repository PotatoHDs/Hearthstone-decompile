using System.Linq;

public static class BoolExtension
{
	public static bool AllSame(this bool firstValue, params bool[] bools)
	{
		return !bools.Any((bool b) => b != firstValue);
	}
}
