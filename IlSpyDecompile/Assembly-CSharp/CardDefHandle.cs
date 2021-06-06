public class CardDefHandle
{
	private string m_cardId;

	private DefLoader.DisposableCardDef m_cardDef;

	public void SetCardId(string cardId)
	{
		m_cardId = cardId;
	}

	public void Set(CardDefHandle other)
	{
		m_cardId = other?.m_cardId;
		SetCardDef(other?.m_cardDef);
	}

	public bool SetCardDef(DefLoader.DisposableCardDef def)
	{
		if (def?.CardDef != m_cardDef?.CardDef)
		{
			ReleaseCardDef();
			m_cardDef = def?.Share();
			return true;
		}
		return false;
	}

	public DefLoader.DisposableCardDef Share()
	{
		if (m_cardDef == null)
		{
			m_cardDef = DefLoader.Get()?.GetCardDef(m_cardId);
		}
		return m_cardDef?.Share();
	}

	public CardDef Get()
	{
		if (m_cardDef == null)
		{
			m_cardDef = DefLoader.Get()?.GetCardDef(m_cardId);
		}
		return m_cardDef?.CardDef;
	}

	public void ReleaseCardDef()
	{
		m_cardDef?.Dispose();
		m_cardDef = null;
	}
}
