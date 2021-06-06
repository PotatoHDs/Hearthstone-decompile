using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class BigCardHelper
{
	// Token: 0x0600033F RID: 831 RVA: 0x00014328 File Offset: 0x00012528
	public static void ShowBigCard(Actor heroPowerBigCard, DefLoader.DisposableFullDef heroPowerFullDef, GameObject bone, float cardScale, Vector3? origin = null)
	{
		if (heroPowerBigCard == null)
		{
			Log.Adventures.PrintError("ShowHeroPowerBigCard called with null heroPowerBigCard big card reference.", Array.Empty<object>());
			return;
		}
		if (((heroPowerFullDef != null) ? heroPowerFullDef.CardDef : null) == null)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			NotificationManager.Get().DestroyActiveQuote(0.2f, true);
		}
		heroPowerBigCard.SetCardDef(heroPowerFullDef.DisposableCardDef);
		heroPowerBigCard.SetEntityDef(heroPowerFullDef.EntityDef);
		heroPowerBigCard.UpdateAllComponents();
		heroPowerBigCard.Show();
		heroPowerBigCard.transform.localScale = Vector3.one * cardScale;
		if (bone != null)
		{
			heroPowerBigCard.transform.localPosition = Vector3.zero;
			BigCardHelper.TweenPower(heroPowerBigCard.gameObject, null);
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			heroPowerBigCard.transform.localPosition = new Vector3(-7.77f, 1.56f, 0.39f);
			BigCardHelper.TweenPower(heroPowerBigCard.gameObject, origin);
			return;
		}
		heroPowerBigCard.transform.localPosition = (UniversalInputManager.Get().IsTouchMode() ? new Vector3(-3.18f, 0.54f, 0.1f) : new Vector3(0.019f, 0.54f, -1.12f));
		BigCardHelper.TweenPower(heroPowerBigCard.gameObject, null);
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001447A File Offset: 0x0001267A
	public static void HideBigCard(Actor heroPowerBigCard)
	{
		iTween.Stop(heroPowerBigCard.gameObject);
		heroPowerBigCard.Hide();
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00014490 File Offset: 0x00012690
	private static void TweenPower(GameObject go, Vector3? origin = null)
	{
		if (origin == null)
		{
			Vector3 b = PlatformSettings.IsTablet ? new Vector3(0f, 0.1f, 0.1f) : new Vector3(0.1f, 0.1f, 0.1f);
			iTween.ScaleFrom(go, go.transform.localScale * 0.5f, 0.15f);
			iTween.MoveTo(go, iTween.Hash(new object[]
			{
				"position",
				go.transform.localPosition + b,
				"isLocal",
				true,
				"time",
				10
			}));
			return;
		}
		AnimationUtil.GrowThenDrift(go, origin.Value, 1f);
	}
}
