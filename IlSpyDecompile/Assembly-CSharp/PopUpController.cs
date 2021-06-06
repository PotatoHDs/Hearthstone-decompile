using UnityEngine;

public class PopUpController : MonoBehaviour
{
	public float LifeTime = 4f;

	public GameObject Portrait;

	public GameObject Banner;

	private void Start()
	{
	}

	private void Update()
	{
		if (LifeTime >= 0f)
		{
			LifeTime -= Time.deltaTime;
			if (LifeTime <= 0f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	public void Populate(int currentValue, int totalValue, int cardID)
	{
		DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(cardID);
		if (cardDef != null)
		{
			Texture portraitTexture = cardDef.CardDef.GetPortraitTexture();
			if (portraitTexture != null)
			{
				Portrait.GetComponent<Renderer>().GetMaterial().SetTexture("_MainTex", portraitTexture);
			}
			Banner.GetComponent<RewardBanner>().SetText(currentValue + " out of " + totalValue, "Entity: " + DefLoader.Get().GetEntityDef(cardID).GetName(), "");
		}
	}
}
