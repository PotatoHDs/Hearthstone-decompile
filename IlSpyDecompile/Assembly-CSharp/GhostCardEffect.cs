using System.Collections;
using UnityEngine;

public class GhostCardEffect : Spell
{
	public GameObject m_Glow;

	public GameObject m_GlowUnique;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (m_Glow != null)
		{
			m_Glow.GetComponent<Renderer>().enabled = false;
		}
		if (m_GlowUnique != null)
		{
			m_GlowUnique.GetComponent<Renderer>().enabled = false;
		}
		StartCoroutine(GhostEffect(prevStateType));
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (m_Glow != null)
		{
			m_Glow.GetComponent<Renderer>().enabled = false;
		}
		if (m_GlowUnique != null)
		{
			m_GlowUnique.GetComponent<Renderer>().enabled = false;
		}
		base.OnDeath(prevStateType);
		OnSpellFinished();
	}

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
		if (m_Glow != null)
		{
			GameObject gameObject = m_Glow;
			if (actor.IsElite() && m_GlowUnique != null)
			{
				gameObject = m_GlowUnique;
			}
			gameObject.GetComponent<Renderer>().enabled = true;
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		ghostCard.RenderGhostCard();
		yield return new WaitForEndOfFrame();
		RenderToTexture componentInChildren = base.gameObject.GetComponentInChildren<RenderToTexture>();
		if ((bool)componentInChildren)
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
		ghostCard.RenderGhostCard(forceRender: true);
		actor.Show();
		TooltipPanelManager.Get().HideKeywordHelp();
		componentInChildren.Render();
		base.OnBirth(prevStateType);
		OnSpellFinished();
	}
}
