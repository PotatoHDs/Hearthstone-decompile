using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200057C RID: 1404
public class TavernMulliganIntro : InfoPopupMulliganIntro
{
	// Token: 0x06004E47 RID: 20039 RVA: 0x0019D697 File Offset: 0x0019B897
	public void Show(MonoBehaviour monoBehaviour)
	{
		monoBehaviour.StartCoroutine(this.ShowPopupIfNotAlreadySeen(monoBehaviour));
	}

	// Token: 0x06004E48 RID: 20040 RVA: 0x0019D6A7 File Offset: 0x0019B8A7
	private IEnumerator ShowPopupIfNotAlreadySeen(MonoBehaviour monoBehaviour)
	{
		yield return base.ShowPopup("AdventureTutorialPopup_DAL.prefab:58e01991c604aad43bc7ae12db9023f6", "FriendlyChoice", TavernMulliganIntro.s_hasSeenTutorialPopup);
		TavernMulliganIntro.s_hasSeenTutorialPopup = true;
		yield break;
	}

	// Token: 0x04004513 RID: 17683
	private const string s_IntroPopupWidget = "AdventureTutorialPopup_DAL.prefab:58e01991c604aad43bc7ae12db9023f6";

	// Token: 0x04004514 RID: 17684
	private const string s_FriendlyBoneName = "FriendlyChoice";

	// Token: 0x04004515 RID: 17685
	private static bool s_hasSeenTutorialPopup;
}
