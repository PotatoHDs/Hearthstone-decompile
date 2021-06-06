using UnityEngine;

public class CoinEffect : MonoBehaviour
{
	public GameObject coinSpawnObject;

	private string coinSpawnAnim = "CoinSpawn1_edit";

	public GameObject coin;

	private string coinDropAnim = "MulliganCoinDropGo2Card";

	public GameObject coinGlow;

	private string coinDropAnim2 = "MulliganCoinDrop2_Edit";

	private string animToUse;

	private string coinGlowDropAnim = "MulliganCoinDrop1Glow_Edit";

	private string coinGlowDropAnim2 = "MulliganCoinDrop2Glow_Edit";

	private string GlowanimToUse;

	private void Start()
	{
	}

	public void DoAnim(bool localWin)
	{
		if (localWin)
		{
			animToUse = coinDropAnim2;
			GlowanimToUse = coinGlowDropAnim2;
		}
		else
		{
			animToUse = coinDropAnim;
			GlowanimToUse = coinGlowDropAnim;
		}
		coinSpawnObject.SetActive(value: true);
		coin.SetActive(value: true);
		coinGlow.SetActive(value: true);
		coinSpawnObject.GetComponent<Animation>().Stop(coinSpawnAnim);
		coin.GetComponent<Animation>().Stop(animToUse);
		coinGlow.GetComponent<Animation>().Stop(GlowanimToUse);
		coinSpawnObject.GetComponent<Animation>().Play(coinSpawnAnim);
		coin.GetComponent<Animation>().Play(animToUse);
		coinGlow.GetComponent<Animation>().Play(GlowanimToUse);
	}
}
