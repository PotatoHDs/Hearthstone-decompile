public class PoweredUpTargetingTextBuilder : CardTextBuilder
{
	public override string GetTargetingArrowText(Entity entity)
	{
		string text = base.GetTargetingArrowText(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			entity.HasCombo();
			text = ((!entity.GetRealTimePoweredUp() && (!entity.GetController().IsComboActive() || !entity.HasCombo())) ? text.Substring(0, num) : text.Substring(num + 1));
		}
		return TextUtils.TransformCardText(entity, text);
	}
}
