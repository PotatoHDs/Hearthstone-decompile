using UnityEngine;

public class ActivateZoneDeck : MonoBehaviour
{
	public bool m_friendlyDeck;

	private bool onoff = true;

	public void ToggleActive()
	{
		if (GameState.Get() == null || GameState.Get().GetFriendlySidePlayer() == null || GameState.Get().GetOpposingSidePlayer() == null)
		{
			Debug.LogError("ActivateZoneDeck - Game State not yet initialized.");
			return;
		}
		ZoneDeck zoneDeck = ((!m_friendlyDeck) ? GameState.Get().GetOpposingSidePlayer().GetDeckZone() : GameState.Get().GetFriendlySidePlayer().GetDeckZone());
		if (zoneDeck == null)
		{
			Debug.LogError("ActivateZoneDeck - zoneDeck is null!");
		}
		else
		{
			zoneDeck.SetVisibility(onoff);
		}
	}
}
