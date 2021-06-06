public class Asset
{
	private string m_guid;

	public Asset(string guid)
	{
		m_guid = guid;
	}

	public string GetGuid()
	{
		return m_guid;
	}

	public override string ToString()
	{
		return $"{{guid={m_guid}}}";
	}
}
