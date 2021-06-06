using System;

// Token: 0x020009AB RID: 2475
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class CustomEditField : Attribute
{
	// Token: 0x060086F9 RID: 34553 RVA: 0x002B977F File Offset: 0x002B797F
	public override string ToString()
	{
		if (this.Sections == null)
		{
			return this.T.ToString();
		}
		return string.Format("Sections={0} T={1}", this.Sections, this.T);
	}

	// Token: 0x0400722A RID: 29226
	public bool Hide;

	// Token: 0x0400722B RID: 29227
	public string HidePredicate;

	// Token: 0x0400722C RID: 29228
	public bool HidePredicateInParent;

	// Token: 0x0400722D RID: 29229
	public bool SortPopupByName;

	// Token: 0x0400722E RID: 29230
	public string SearchField;

	// Token: 0x0400722F RID: 29231
	public string Sections;

	// Token: 0x04007230 RID: 29232
	public string Parent;

	// Token: 0x04007231 RID: 29233
	public string Label;

	// Token: 0x04007232 RID: 29234
	public string Range;

	// Token: 0x04007233 RID: 29235
	public bool ListTable;

	// Token: 0x04007234 RID: 29236
	public int ListSortPriority = -1;

	// Token: 0x04007235 RID: 29237
	public bool ListSortable;

	// Token: 0x04007236 RID: 29238
	public bool AllowSceneObject = true;

	// Token: 0x04007237 RID: 29239
	public EditType T;
}
