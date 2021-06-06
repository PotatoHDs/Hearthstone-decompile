using UnityEngine;

public class BaconHeroMulliganBestPlaceVisual : MonoBehaviour
{
	public UberText BestPlaceText;

	public Color FirstPlaceColorOverride;

	private PlayMakerFSM m_playmaker;

	private const int MaxSupportedPlace = 4;

	public void SetVisualActive(int place, int heroDbId)
	{
		if (place < 0 || place > 4)
		{
			BestPlaceText.gameObject.SetActive(value: false);
			return;
		}
		BestPlaceText.Text = GameStrings.Format("GAMEPLAY_MULLIGAN_BEST_PLACE", GameStrings.GetOrdinalNumber(place));
		if (place == 1)
		{
			BestPlaceText.TextColor = FirstPlaceColorOverride;
		}
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Birth");
		}
	}

	public void Hide()
	{
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Death");
		}
		Object.Destroy(base.gameObject, 10f);
	}
}
