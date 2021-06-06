using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class ActivateZoneDeck : MonoBehaviour
{
	// Token: 0x06003479 RID: 13433 RVA: 0x0010C4F8 File Offset: 0x0010A6F8
	public void ToggleActive()
	{
		if (GameState.Get() == null || GameState.Get().GetFriendlySidePlayer() == null || GameState.Get().GetOpposingSidePlayer() == null)
		{
			Debug.LogError("ActivateZoneDeck - Game State not yet initialized.");
			return;
		}
		ZoneDeck deckZone;
		if (this.m_friendlyDeck)
		{
			deckZone = GameState.Get().GetFriendlySidePlayer().GetDeckZone();
		}
		else
		{
			deckZone = GameState.Get().GetOpposingSidePlayer().GetDeckZone();
		}
		if (deckZone == null)
		{
			Debug.LogError("ActivateZoneDeck - zoneDeck is null!");
			return;
		}
		deckZone.SetVisibility(this.onoff);
	}

	// Token: 0x04001CAE RID: 7342
	public bool m_friendlyDeck;

	// Token: 0x04001CAF RID: 7343
	private bool onoff = true;
}
