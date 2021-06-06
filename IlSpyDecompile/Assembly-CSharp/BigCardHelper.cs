using UnityEngine;

public class BigCardHelper
{
	public static void ShowBigCard(Actor heroPowerBigCard, DefLoader.DisposableFullDef heroPowerFullDef, GameObject bone, float cardScale, Vector3? origin = null)
	{
		if (heroPowerBigCard == null)
		{
			Log.Adventures.PrintError("ShowHeroPowerBigCard called with null heroPowerBigCard big card reference.");
		}
		else if (!(heroPowerFullDef?.CardDef == null))
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				NotificationManager.Get().DestroyActiveQuote(0.2f, ignoreAudio: true);
			}
			heroPowerBigCard.SetCardDef(heroPowerFullDef.DisposableCardDef);
			heroPowerBigCard.SetEntityDef(heroPowerFullDef.EntityDef);
			heroPowerBigCard.UpdateAllComponents();
			heroPowerBigCard.Show();
			heroPowerBigCard.transform.localScale = Vector3.one * cardScale;
			if (bone != null)
			{
				heroPowerBigCard.transform.localPosition = Vector3.zero;
				TweenPower(heroPowerBigCard.gameObject);
			}
			else if ((bool)UniversalInputManager.UsePhoneUI)
			{
				heroPowerBigCard.transform.localPosition = new Vector3(-7.77f, 1.56f, 0.39f);
				TweenPower(heroPowerBigCard.gameObject, origin);
			}
			else
			{
				heroPowerBigCard.transform.localPosition = (UniversalInputManager.Get().IsTouchMode() ? new Vector3(-3.18f, 0.54f, 0.1f) : new Vector3(0.019f, 0.54f, -1.12f));
				TweenPower(heroPowerBigCard.gameObject);
			}
		}
	}

	public static void HideBigCard(Actor heroPowerBigCard)
	{
		iTween.Stop(heroPowerBigCard.gameObject);
		heroPowerBigCard.Hide();
	}

	private static void TweenPower(GameObject go, Vector3? origin = null)
	{
		if (!origin.HasValue)
		{
			Vector3 vector = (PlatformSettings.IsTablet ? new Vector3(0f, 0.1f, 0.1f) : new Vector3(0.1f, 0.1f, 0.1f));
			iTween.ScaleFrom(go, go.transform.localScale * 0.5f, 0.15f);
			iTween.MoveTo(go, iTween.Hash("position", go.transform.localPosition + vector, "isLocal", true, "time", 10));
		}
		else
		{
			AnimationUtil.GrowThenDrift(go, origin.Value, 1f);
		}
	}
}
