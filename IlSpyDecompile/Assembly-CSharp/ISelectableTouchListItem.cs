public interface ISelectableTouchListItem : ITouchListItem
{
	bool Selectable { get; }

	bool IsSelected();

	void Selected();

	void Unselected();
}
