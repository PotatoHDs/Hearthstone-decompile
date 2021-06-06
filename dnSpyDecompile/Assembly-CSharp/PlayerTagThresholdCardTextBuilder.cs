using System;

// Token: 0x0200079D RID: 1949
public class PlayerTagThresholdCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CA7 RID: 27815 RVA: 0x00232278 File Offset: 0x00230478
	public override string BuildCardTextInHand(Entity entity)
	{
		Player controller = entity.GetController();
		GAME_TAG tag = (GAME_TAG)entity.GetTag(GAME_TAG.PLAYER_TAG_THRESHOLD_TAG_ID);
		int num = (controller != null) ? controller.GetTag(tag) : 0;
		int tag2 = entity.GetTag(GAME_TAG.PLAYER_TAG_THRESHOLD_VALUE);
		string text = base.BuildCardTextInHand(entity);
		int num2 = text.IndexOf('@');
		int num3 = text.IndexOf('@', num2 + 1);
		if (num2 >= 0 && num3 >= 0)
		{
			string text2 = text.Substring(0, num2);
			if (num >= tag2)
			{
				text2 += text.Substring(num3 + 1);
			}
			else
			{
				int length = num3 - num2 - 1;
				text2 += text.Substring(num2 + 1, length);
				text2 = string.Format(text2, tag2 - num);
			}
			text = text2;
		}
		return text;
	}

	// Token: 0x06006CA8 RID: 27816 RVA: 0x0023233C File Offset: 0x0023053C
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}

	// Token: 0x06006CA9 RID: 27817 RVA: 0x00232368 File Offset: 0x00230568
	public override string BuildCardTextInHistory(Entity entity)
	{
		string text = base.BuildCardTextInHistory(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}
}
