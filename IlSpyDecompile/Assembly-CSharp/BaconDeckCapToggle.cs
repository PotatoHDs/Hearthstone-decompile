using UnityEngine;

public class BaconDeckCapToggle : MonoBehaviour
{
	public GameObject[] deckCapObjects;

	private void Awake()
	{
		GameState.Get()?.RegisterCreateGameListener(OnGameCreated);
	}

	private void OnGameCreated(GameState.CreateGamePhase phase, object userData)
	{
		bool flag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.DARKMOON_FAIRE_PRIZES_ACTIVE) == 1;
		GameObject[] array = deckCapObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!flag);
		}
	}

	private void OnDestroy()
	{
		GameState.Get()?.UnregisterCreateGameListener(OnGameCreated);
	}
}
