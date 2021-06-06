using System;

// Token: 0x02000B1A RID: 2842
public interface ISelectableTouchListItem : ITouchListItem
{
	// Token: 0x17000895 RID: 2197
	// (get) Token: 0x06009716 RID: 38678
	bool Selectable { get; }

	// Token: 0x06009717 RID: 38679
	bool IsSelected();

	// Token: 0x06009718 RID: 38680
	void Selected();

	// Token: 0x06009719 RID: 38681
	void Unselected();
}
