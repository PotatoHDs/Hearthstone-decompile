using System;

[AttributeUsage(AttributeTargets.Property)]
public class DbfFieldAttribute : Attribute
{
	public string m_varName;

	public DbfFieldAttribute(string varName)
	{
		m_varName = varName;
	}
}
