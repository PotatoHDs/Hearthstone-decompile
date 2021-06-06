using UnityEngine;

public class DuelsVaultDoor : MonoBehaviour
{
	public const string VAULT_DIAL_ANIM = "vaultpad_dialturn";

	public GameObject m_heroicWinText;

	public GameObject m_prevHeroicWinText;

	private void Start()
	{
		GameUtils.OnAnimationExitEvent.AddListener(OnAnimationEnded);
	}

	private void OnAnimationEnded(string AnimationName)
	{
		if (AnimationName == "vaultpad_dialturn" && m_heroicWinText != null && m_prevHeroicWinText != null)
		{
			m_prevHeroicWinText.SetActive(value: false);
			m_heroicWinText.SetActive(value: true);
		}
	}

	private void OnDestroy()
	{
		GameUtils.OnAnimationExitEvent.RemoveListener(OnAnimationEnded);
	}
}
