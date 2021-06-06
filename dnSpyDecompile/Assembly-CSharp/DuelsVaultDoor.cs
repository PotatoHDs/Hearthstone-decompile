using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000061 RID: 97
public class DuelsVaultDoor : MonoBehaviour
{
	// Token: 0x06000589 RID: 1417 RVA: 0x000200E3 File Offset: 0x0001E2E3
	private void Start()
	{
		GameUtils.OnAnimationExitEvent.AddListener(new UnityAction<string>(this.OnAnimationEnded));
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x000200FC File Offset: 0x0001E2FC
	private void OnAnimationEnded(string AnimationName)
	{
		if (AnimationName == "vaultpad_dialturn" && this.m_heroicWinText != null && this.m_prevHeroicWinText != null)
		{
			this.m_prevHeroicWinText.SetActive(false);
			this.m_heroicWinText.SetActive(true);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002014A File Offset: 0x0001E34A
	private void OnDestroy()
	{
		GameUtils.OnAnimationExitEvent.RemoveListener(new UnityAction<string>(this.OnAnimationEnded));
	}

	// Token: 0x040003E1 RID: 993
	public const string VAULT_DIAL_ANIM = "vaultpad_dialturn";

	// Token: 0x040003E2 RID: 994
	public GameObject m_heroicWinText;

	// Token: 0x040003E3 RID: 995
	public GameObject m_prevHeroicWinText;
}
