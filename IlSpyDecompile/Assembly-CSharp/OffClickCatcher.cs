public class OffClickCatcher : PegUIElement
{
	protected override void OnRelease()
	{
		Navigation.GoBack();
	}

	protected override void OnRightClick()
	{
		Navigation.GoBack();
	}
}
