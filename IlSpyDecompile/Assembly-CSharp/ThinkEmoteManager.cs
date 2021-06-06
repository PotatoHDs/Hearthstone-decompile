using UnityEngine;

public class ThinkEmoteManager : MonoBehaviour
{
	private float m_secondsSinceAction;

	public const float DEFAULT_THINK_EMOTE_DELAY = 20f;

	private static ThinkEmoteManager s_instance;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static ThinkEmoteManager Get()
	{
		return s_instance;
	}

	private void Update()
	{
		GameState gameState = GameState.Get();
		if (gameState != null && gameState.IsMainPhase())
		{
			float? num = gameState.GetGameEntity().GetThinkEmoteDelayOverride();
			if (!num.HasValue)
			{
				num = 20f;
			}
			m_secondsSinceAction += Time.deltaTime;
			if (m_secondsSinceAction > num)
			{
				PlayThinkEmote();
			}
		}
	}

	private void PlayThinkEmote()
	{
		m_secondsSinceAction = 0f;
		GameState.Get().GetGameEntity().OnPlayThinkEmote();
	}

	public void NotifyOfActivity()
	{
		m_secondsSinceAction = 0f;
	}
}
