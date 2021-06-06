using System.Collections;
using UnityEngine;

public class TavernMulliganIntro : InfoPopupMulliganIntro
{
	private const string s_IntroPopupWidget = "AdventureTutorialPopup_DAL.prefab:58e01991c604aad43bc7ae12db9023f6";

	private const string s_FriendlyBoneName = "FriendlyChoice";

	private static bool s_hasSeenTutorialPopup;

	public void Show(MonoBehaviour monoBehaviour)
	{
		monoBehaviour.StartCoroutine(ShowPopupIfNotAlreadySeen(monoBehaviour));
	}

	private IEnumerator ShowPopupIfNotAlreadySeen(MonoBehaviour monoBehaviour)
	{
		yield return ShowPopup("AdventureTutorialPopup_DAL.prefab:58e01991c604aad43bc7ae12db9023f6", "FriendlyChoice", s_hasSeenTutorialPopup);
		s_hasSeenTutorialPopup = true;
	}
}
