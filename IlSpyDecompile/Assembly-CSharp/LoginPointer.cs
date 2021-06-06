public class LoginPointer : PegUIElement
{
	protected override void OnPress()
	{
		GameUtils.LogoutConfirmation();
	}
}
