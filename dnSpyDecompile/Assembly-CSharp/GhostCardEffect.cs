using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007F2 RID: 2034
public class GhostCardEffect : Spell
{
	// Token: 0x06006ECF RID: 28367 RVA: 0x0023B874 File Offset: 0x00239A74
	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (this.m_Glow != null)
		{
			this.m_Glow.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_GlowUnique != null)
		{
			this.m_GlowUnique.GetComponent<Renderer>().enabled = false;
		}
		base.StartCoroutine(this.GhostEffect(prevStateType));
	}

	// Token: 0x06006ED0 RID: 28368 RVA: 0x0023B8D0 File Offset: 0x00239AD0
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (this.m_Glow != null)
		{
			this.m_Glow.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_GlowUnique != null)
		{
			this.m_GlowUnique.GetComponent<Renderer>().enabled = false;
		}
		base.OnDeath(prevStateType);
		this.OnSpellFinished();
	}

	// Token: 0x06006ED1 RID: 28369 RVA: 0x0023B928 File Offset: 0x00239B28
	private IEnumerator GhostEffect(SpellStateType prevStateType)
	{
		Actor actor = SceneUtils.FindComponentInParents<Actor>(base.gameObject);
		if (actor == null)
		{
			Debug.LogWarning("GhostCardEffect actor is null");
			yield break;
		}
		GhostCard ghostCard = base.gameObject.GetComponentInChildren<GhostCard>();
		if (ghostCard == null)
		{
			Debug.LogWarning("GhostCardEffect GhostCard is null");
			yield break;
		}
		if (this.m_Glow != null)
		{
			GameObject gameObject = this.m_Glow;
			if (actor.IsElite() && this.m_GlowUnique != null)
			{
				gameObject = this.m_GlowUnique;
			}
			gameObject.GetComponent<Renderer>().enabled = true;
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		ghostCard.RenderGhostCard();
		yield return new WaitForEndOfFrame();
		RenderToTexture componentInChildren = base.gameObject.GetComponentInChildren<RenderToTexture>();
		if (componentInChildren)
		{
			if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High && actor.GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				componentInChildren.m_RealtimeRender = true;
			}
			else
			{
				componentInChildren.m_RealtimeRender = false;
			}
			componentInChildren.m_LateUpdate = true;
		}
		ghostCard.RenderGhostCard(true);
		actor.Show();
		TooltipPanelManager.Get().HideKeywordHelp();
		componentInChildren.Render();
		base.OnBirth(prevStateType);
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x040058EB RID: 22763
	public GameObject m_Glow;

	// Token: 0x040058EC RID: 22764
	public GameObject m_GlowUnique;
}
