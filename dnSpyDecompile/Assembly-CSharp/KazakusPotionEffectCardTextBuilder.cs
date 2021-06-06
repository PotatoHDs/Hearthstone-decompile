using System;

// Token: 0x0200079A RID: 1946
public class KazakusPotionEffectCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C8F RID: 27791 RVA: 0x00231CBC File Offset: 0x0022FEBC
	private string GetCorrectSubstring(string text)
	{
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			return text.Substring(0, num);
		}
		return text;
	}

	// Token: 0x06006C90 RID: 27792 RVA: 0x00231CE0 File Offset: 0x0022FEE0
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		return this.GetCorrectSubstring(text);
	}

	// Token: 0x06006C91 RID: 27793 RVA: 0x00231CFC File Offset: 0x0022FEFC
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		return this.GetCorrectSubstring(text);
	}

	// Token: 0x06006C92 RID: 27794 RVA: 0x00231D18 File Offset: 0x0022FF18
	public override string BuildCardTextInHistory(Entity entity)
	{
		string text = base.BuildCardTextInHistory(entity);
		return this.GetCorrectSubstring(text);
	}
}
