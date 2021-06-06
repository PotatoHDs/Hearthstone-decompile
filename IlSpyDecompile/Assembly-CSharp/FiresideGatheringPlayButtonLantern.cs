using UnityEngine;

public class FiresideGatheringPlayButtonLantern : MonoBehaviour
{
	public GameObject LitLantern;

	public GameObject UnlitLantern;

	public void SetLanternLit(bool lit)
	{
		LitLantern.SetActive(lit);
		UnlitLantern.SetActive(!lit);
	}
}
