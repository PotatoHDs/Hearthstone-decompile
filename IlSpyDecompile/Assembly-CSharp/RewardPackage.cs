using UnityEngine;

public class RewardPackage : PegUIElement
{
	public int m_RewardIndex = -1;

	protected override void Awake()
	{
		base.Awake();
	}

	public void OnEnable()
	{
		SetEnabled(enabled: true);
	}

	public void OnDisable()
	{
		SetEnabled(enabled: false);
	}

	public void Update()
	{
		if (InputCollection.GetKeyDown(KeyCode.Alpha1) && m_RewardIndex == 0)
		{
			OpenReward();
		}
		else if (InputCollection.GetKeyDown(KeyCode.Alpha2) && m_RewardIndex == 1)
		{
			OpenReward();
		}
		else if (InputCollection.GetKeyDown(KeyCode.Alpha3) && m_RewardIndex == 2)
		{
			OpenReward();
		}
		else if (InputCollection.GetKeyDown(KeyCode.Alpha4) && m_RewardIndex == 3)
		{
			OpenReward();
		}
		else if (InputCollection.GetKeyDown(KeyCode.Alpha5) && m_RewardIndex == 4)
		{
			OpenReward();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		GetComponent<PlayMakerFSM>().SendEvent("Action");
	}

	protected override void OnPress()
	{
		OpenReward();
	}

	private void OpenReward()
	{
		GetComponent<PlayMakerFSM>().SendEvent("Death");
		RewardBoxesDisplay.Get().OpenReward(m_RewardIndex, base.transform.position);
		base.enabled = false;
	}
}
