public class OnOffToggleButton : CheckBox
{
	public string m_onLabel = "GLOBAL_ON";

	public string m_offLabel = "GLOBAL_OFF";

	public override void SetChecked(bool isChecked)
	{
		base.SetChecked(isChecked);
		SetButtonText(GameStrings.Get(isChecked ? m_onLabel : m_offLabel));
	}
}
