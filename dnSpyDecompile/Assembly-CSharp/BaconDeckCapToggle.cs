using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class BaconDeckCapToggle : MonoBehaviour
{
	// Token: 0x060005D8 RID: 1496 RVA: 0x000222C8 File Offset: 0x000204C8
	private void Awake()
	{
		GameState gameState = GameState.Get();
		if (gameState != null)
		{
			gameState.RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnGameCreated));
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x000222F4 File Offset: 0x000204F4
	private void OnGameCreated(GameState.CreateGamePhase phase, object userData)
	{
		bool flag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.DARKMOON_FAIRE_PRIZES_ACTIVE) == 1;
		GameObject[] array = this.deckCapObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!flag);
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002233C File Offset: 0x0002053C
	private void OnDestroy()
	{
		GameState gameState = GameState.Get();
		if (gameState != null)
		{
			gameState.UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnGameCreated));
		}
	}

	// Token: 0x04000423 RID: 1059
	public GameObject[] deckCapObjects;
}
