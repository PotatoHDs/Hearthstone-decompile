public class KeyValue
{
	public Key Key { get; set; }

	public byte[] Value { get; set; }

	public KeyValue(Key key, byte[] value)
	{
		Key = key;
		Value = value;
	}

	public override string ToString()
	{
		return $"[KeyValue: {Key.Field}, {Key.WireType}, {Value.Length} bytes]";
	}
}
