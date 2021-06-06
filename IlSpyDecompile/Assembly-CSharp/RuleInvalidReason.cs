public class RuleInvalidReason
{
	public string DisplayError;

	public int CountParam;

	public bool IsMinimum;

	public RuleInvalidReason(string error, int countParam = 0, bool isMinimum = false)
	{
		DisplayError = error;
		CountParam = countParam;
		IsMinimum = isMinimum;
	}
}
