using System;

// Token: 0x02000798 RID: 1944
public class JadeGolemCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C88 RID: 27784 RVA: 0x00231B2E File Offset: 0x0022FD2E
	public JadeGolemCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
		this.m_showJadeGolemStatsInPlay = false;
	}

	// Token: 0x06006C89 RID: 27785 RVA: 0x00231B44 File Offset: 0x0022FD44
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			if (entity.GetZone() == TAG_ZONE.PLAY && !this.m_showJadeGolemStatsInPlay)
			{
				text = text.Substring(num + 1);
			}
			else
			{
				text = text.Substring(0, num);
			}
		}
		return this.FormatJadeGolemText(text, entity.GetJadeGolem());
	}

	// Token: 0x06006C8A RID: 27786 RVA: 0x00231B9C File Offset: 0x0022FD9C
	private string FormatJadeGolemText(string builtText, int jadeGolemValue)
	{
		string arg = "";
		if (jadeGolemValue == 8 || jadeGolemValue == 11 || jadeGolemValue == 18)
		{
			arg = "n";
		}
		return string.Format(builtText, jadeGolemValue + "/" + jadeGolemValue, arg);
	}

	// Token: 0x06006C8B RID: 27787 RVA: 0x00231BE0 File Offset: 0x0022FDE0
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = text.Substring(num + 1);
		}
		return text;
	}

	// Token: 0x06006C8C RID: 27788 RVA: 0x00231C10 File Offset: 0x0022FE10
	public override string BuildCardTextInHistory(Entity entity)
	{
		JadeGolemCardTextHistoryData jadeGolemCardTextHistoryData = entity.GetCardTextHistoryData() as JadeGolemCardTextHistoryData;
		if (jadeGolemCardTextHistoryData == null)
		{
			Log.All.Print("JadeGolemCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a JadeGolemCardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return string.Empty;
		}
		string text = base.BuildCardTextInHistory(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			if (jadeGolemCardTextHistoryData.m_wasInPlay && !this.m_showJadeGolemStatsInPlay)
			{
				text = text.Substring(num + 1);
			}
			else
			{
				text = text.Substring(0, num);
			}
		}
		return this.FormatJadeGolemText(text, entity.GetJadeGolem());
	}

	// Token: 0x06006C8D RID: 27789 RVA: 0x00231C9C File Offset: 0x0022FE9C
	public override CardTextHistoryData CreateCardTextHistoryData()
	{
		return new JadeGolemCardTextHistoryData();
	}

	// Token: 0x040057C0 RID: 22464
	protected bool m_showJadeGolemStatsInPlay;
}
