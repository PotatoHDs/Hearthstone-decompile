public class Key
{
	public uint Field { get; set; }

	public Wire WireType { get; set; }

	public Key(uint field, Wire wireType)
	{
		Field = field;
		WireType = wireType;
	}

	public override string ToString()
	{
		return $"[Key: {Field}, {WireType}]";
	}
}
