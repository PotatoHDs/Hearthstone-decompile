using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class ThinkEmoteManager : MonoBehaviour
{
	// Token: 0x060030D6 RID: 12502 RVA: 0x000FB68C File Offset: 0x000F988C
	private void Awake()
	{
		ThinkEmoteManager.s_instance = this;
	}

	// Token: 0x060030D7 RID: 12503 RVA: 0x000FB694 File Offset: 0x000F9894
	private void OnDestroy()
	{
		ThinkEmoteManager.s_instance = null;
	}

	// Token: 0x060030D8 RID: 12504 RVA: 0x000FB69C File Offset: 0x000F989C
	public static ThinkEmoteManager Get()
	{
		return ThinkEmoteManager.s_instance;
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x000FB6A4 File Offset: 0x000F98A4
	private void Update()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		if (!gameState.IsMainPhase())
		{
			return;
		}
		float? thinkEmoteDelayOverride = gameState.GetGameEntity().GetThinkEmoteDelayOverride();
		if (thinkEmoteDelayOverride == null)
		{
			thinkEmoteDelayOverride = new float?(20f);
		}
		this.m_secondsSinceAction += Time.deltaTime;
		float secondsSinceAction = this.m_secondsSinceAction;
		float? num = thinkEmoteDelayOverride;
		if (secondsSinceAction > num.GetValueOrDefault() & num != null)
		{
			this.PlayThinkEmote();
		}
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x000FB718 File Offset: 0x000F9918
	private void PlayThinkEmote()
	{
		this.m_secondsSinceAction = 0f;
		GameState.Get().GetGameEntity().OnPlayThinkEmote();
	}

	// Token: 0x060030DB RID: 12507 RVA: 0x000FB734 File Offset: 0x000F9934
	public void NotifyOfActivity()
	{
		this.m_secondsSinceAction = 0f;
	}

	// Token: 0x04001B24 RID: 6948
	private float m_secondsSinceAction;

	// Token: 0x04001B25 RID: 6949
	public const float DEFAULT_THINK_EMOTE_DELAY = 20f;

	// Token: 0x04001B26 RID: 6950
	private static ThinkEmoteManager s_instance;
}
