using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class BaconHeroMulliganBestPlaceVisual : MonoBehaviour
{
	// Token: 0x06000608 RID: 1544 RVA: 0x000239F0 File Offset: 0x00021BF0
	public void SetVisualActive(int place, int heroDbId)
	{
		if (place < 0 || place > 4)
		{
			this.BestPlaceText.gameObject.SetActive(false);
			return;
		}
		this.BestPlaceText.Text = GameStrings.Format("GAMEPLAY_MULLIGAN_BEST_PLACE", new object[]
		{
			GameStrings.GetOrdinalNumber(place)
		});
		if (place == 1)
		{
			this.BestPlaceText.TextColor = this.FirstPlaceColorOverride;
		}
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Birth");
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00023A6C File Offset: 0x00021C6C
	public void Hide()
	{
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Death");
		}
		UnityEngine.Object.Destroy(base.gameObject, 10f);
	}

	// Token: 0x0400043A RID: 1082
	public UberText BestPlaceText;

	// Token: 0x0400043B RID: 1083
	public Color FirstPlaceColorOverride;

	// Token: 0x0400043C RID: 1084
	private PlayMakerFSM m_playmaker;

	// Token: 0x0400043D RID: 1085
	private const int MaxSupportedPlace = 4;
}
