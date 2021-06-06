using System.Collections;
using UnityEngine;

public class DeckCardBarFlareUp : SpellImpl
{
	public GameObject m_fuseQuad;

	public GameObject m_fxSparks;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (base.gameObject.activeSelf)
		{
			StartCoroutine(BirthState());
		}
	}

	private IEnumerator BirthState()
	{
		SetVisibility(m_fuseQuad, visible: true);
		PlayParticles(m_fxSparks, includeChildren: false);
		PlayAnimation(m_fuseQuad, "DeckCardBar_FuseInOut", PlayMode.StopAll);
		OnSpellFinished();
		yield return new WaitForSeconds(2f);
		SetVisibility(m_fuseQuad, visible: false);
	}
}
