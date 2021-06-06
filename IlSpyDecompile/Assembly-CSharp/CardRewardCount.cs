using UnityEngine;

public class CardRewardCount : MonoBehaviour
{
	public UberText m_countText;

	public UberText m_multiplierText;

	private void Awake()
	{
		m_multiplierText.Text = GameStrings.Get("GLOBAL_REWARD_CARD_COUNT_MULTIPLIER");
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetCount(int count)
	{
		m_countText.Text = count.ToString();
	}
}
