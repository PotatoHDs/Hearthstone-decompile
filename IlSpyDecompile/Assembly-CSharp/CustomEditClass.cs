using System;

[AttributeUsage(AttributeTargets.Class)]
public class CustomEditClass : Attribute
{
	public bool DefaultCollapsed;
}
