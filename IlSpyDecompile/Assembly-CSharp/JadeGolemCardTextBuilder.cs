public class JadeGolemCardTextBuilder : CardTextBuilder
{
	protected bool m_showJadeGolemStatsInPlay;

	public JadeGolemCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
		m_showJadeGolemStatsInPlay = false;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = ((entity.GetZone() != TAG_ZONE.PLAY || m_showJadeGolemStatsInPlay) ? text.Substring(0, num) : text.Substring(num + 1));
		}
		return FormatJadeGolemText(text, entity.GetJadeGolem());
	}

	private string FormatJadeGolemText(string builtText, int jadeGolemValue)
	{
		string arg = "";
		if (jadeGolemValue == 8 || jadeGolemValue == 11 || jadeGolemValue == 18)
		{
			arg = "n";
		}
		return string.Format(builtText, jadeGolemValue + "/" + jadeGolemValue, arg);
	}

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

	public override string BuildCardTextInHistory(Entity entity)
	{
		JadeGolemCardTextHistoryData jadeGolemCardTextHistoryData = entity.GetCardTextHistoryData() as JadeGolemCardTextHistoryData;
		if (jadeGolemCardTextHistoryData == null)
		{
			Log.All.Print("JadeGolemCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a JadeGolemCardTextHistoryData object.", entity.GetEntityId());
			return string.Empty;
		}
		string text = base.BuildCardTextInHistory(entity);
		int num = text.IndexOf('@');
		if (num >= 0)
		{
			text = ((!jadeGolemCardTextHistoryData.m_wasInPlay || m_showJadeGolemStatsInPlay) ? text.Substring(0, num) : text.Substring(num + 1));
		}
		return FormatJadeGolemText(text, entity.GetJadeGolem());
	}

	public override CardTextHistoryData CreateCardTextHistoryData()
	{
		return new JadeGolemCardTextHistoryData();
	}
}
