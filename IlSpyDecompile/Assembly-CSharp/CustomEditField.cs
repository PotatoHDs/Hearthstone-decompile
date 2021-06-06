using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class CustomEditField : Attribute
{
	public bool Hide;

	public string HidePredicate;

	public bool HidePredicateInParent;

	public bool SortPopupByName;

	public string SearchField;

	public string Sections;

	public string Parent;

	public string Label;

	public string Range;

	public bool ListTable;

	public int ListSortPriority = -1;

	public bool ListSortable;

	public bool AllowSceneObject = true;

	public EditType T;

	public override string ToString()
	{
		if (Sections == null)
		{
			return T.ToString();
		}
		return $"Sections={Sections} T={T}";
	}
}
