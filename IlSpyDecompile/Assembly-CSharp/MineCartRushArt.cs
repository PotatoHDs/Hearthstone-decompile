using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartRushArt : MonoBehaviour
{
	public List<Texture2D> m_portraits = new List<Texture2D>();

	public Spell m_portraitSwapSpell;

	public float m_portraitSwapDelay = 0.5f;

	public void DoPortraitSwap(Actor actor)
	{
		StartCoroutine(DoPortraitSwapWithTiming(actor));
	}

	private IEnumerator DoPortraitSwapWithTiming(Actor actor)
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
		actor.SetPortraitTextureOverride(GetNextPortrait());
	}

	private Texture2D GetNextPortrait()
	{
		if (m_portraits.Count == 0)
		{
			return null;
		}
		if (m_portraits.Count == 1)
		{
			return m_portraits[0];
		}
		Texture2D value = m_portraits[0];
		int index = Random.Range(1, m_portraits.Count);
		m_portraits[0] = m_portraits[index];
		m_portraits[index] = value;
		return m_portraits[0];
	}
}
