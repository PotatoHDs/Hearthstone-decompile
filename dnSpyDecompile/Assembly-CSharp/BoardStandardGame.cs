using System;

// Token: 0x02000867 RID: 2151
public class BoardStandardGame : BoardLayout
{
	// Token: 0x06007428 RID: 29736 RVA: 0x0025430F File Offset: 0x0025250F
	private void Start()
	{
		this.DeckColors();
	}

	// Token: 0x06007429 RID: 29737 RVA: 0x00254318 File Offset: 0x00252518
	public void DeckColors()
	{
		Actor[] deckActors = this.m_DeckActors;
		for (int i = 0; i < deckActors.Length; i++)
		{
			deckActors[i].GetMeshRenderer(false).GetMaterial().color = Board.Get().m_DeckColor;
		}
	}

	// Token: 0x04005C46 RID: 23622
	public Actor[] m_DeckActors;

	// Token: 0x04005C47 RID: 23623
	private static BoardStandardGame s_instance;
}
