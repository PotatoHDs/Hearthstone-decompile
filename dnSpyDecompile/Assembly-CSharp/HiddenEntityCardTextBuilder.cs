using System;

// Token: 0x02000795 RID: 1941
public class HiddenEntityCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C7B RID: 27771 RVA: 0x002315A8 File Offset: 0x0022F7A8
	public override string BuildCardTextInHand(Entity entity)
	{
		string[] array = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()).Split(new char[]
		{
			'@'
		});
		string text = array[0];
		int tag = entity.GetTag(GAME_TAG.HIDDEN_CHOICE);
		if (tag > 0 && array.Length > 1)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null && entityDef.HasValidDisplayName())
			{
				text = string.Format(array[1], CardTextBuilder.GetRawCardName(entityDef));
			}
		}
		return TextUtils.TransformCardText(entity, text);
	}
}
