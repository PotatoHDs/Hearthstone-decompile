using System;

// Token: 0x02000791 RID: 1937
public class DrustvarHorrorTargetingTextBuilder : ReferenceScriptDataNum1Num2EntityCardTextBuilder
{
	// Token: 0x06006C69 RID: 27753 RVA: 0x00231080 File Offset: 0x0022F280
	public override string GetTargetingArrowText(Entity entity)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(entity.GetCardId());
		if (cardRecord == null || cardRecord.TargetArrowText == null)
		{
			return string.Empty;
		}
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1));
		if (entity2 == null || !entity2.HasValidDisplayName())
		{
			return string.Empty;
		}
		string text = string.Format(cardRecord.TargetArrowText, entity2.GetName());
		return TextUtils.TransformCardText(entity, text);
	}
}
