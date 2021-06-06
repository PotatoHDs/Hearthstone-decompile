using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleArt : MonoBehaviour
{
	public List<Texture2D> m_portraits;

	public Spell m_portraitSwapSpell;

	public float m_portraitSwapDelay = 0.5f;

	public void DoPortraitSwap(Actor actor, int turn)
	{
		StartCoroutine(DoPortraitSwapWithTiming(actor, turn));
	}

	private IEnumerator DoPortraitSwapWithTiming(Actor actor, int turn)
	{
		if (actor == null)
		{
			yield break;
		}
		if (m_portraitSwapSpell != null)
		{
			Spell spell2 = Object.Instantiate(m_portraitSwapSpell);
			spell2.transform.parent = actor.transform;
			spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				if (spell.GetActiveState() == SpellStateType.NONE)
				{
					Object.Destroy(spell.gameObject);
				}
			});
			spell2.SetSource(actor.gameObject);
			spell2.Activate();
			yield return new WaitForSeconds(m_portraitSwapDelay);
		}
		actor.SetPortraitTextureOverride(GetArtForTurn(turn));
	}

	private Texture2D GetArtForTurn(int turn)
	{
		return m_portraits[turn];
	}
}
