public class BoardStandardGame : BoardLayout
{
	public Actor[] m_DeckActors;

	private static BoardStandardGame s_instance;

	private void Start()
	{
		DeckColors();
	}

	public void DeckColors()
	{
		Actor[] deckActors = m_DeckActors;
		for (int i = 0; i < deckActors.Length; i++)
		{
			deckActors[i].GetMeshRenderer().GetMaterial().color = Board.Get().m_DeckColor;
		}
	}
}
