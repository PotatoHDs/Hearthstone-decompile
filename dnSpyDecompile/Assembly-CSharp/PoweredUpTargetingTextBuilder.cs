using System;

// Token: 0x0200079E RID: 1950
public class PoweredUpTargetingTextBuilder : CardTextBuilder
{
	// Token: 0x06006CAB RID: 27819 RVA: 0x00232394 File Offset: 0x00230594
	public override string GetTargetingArrowText(Entity entity)
	{
		string text = base.GetTargetingArrowText(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			entity.HasCombo();
			if (entity.GetRealTimePoweredUp() || (entity.GetController().IsComboActive() && entity.HasCombo()))
			{
				text = text.Substring(num + 1);
			}
			else
			{
				text = text.Substring(0, num);
			}
		}
		return TextUtils.TransformCardText(entity, text);
	}
}
