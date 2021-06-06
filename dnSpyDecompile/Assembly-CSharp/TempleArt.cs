using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class TempleArt : MonoBehaviour
{
	// Token: 0x06000D2E RID: 3374 RVA: 0x0004C13A File Offset: 0x0004A33A
	public void DoPortraitSwap(Actor actor, int turn)
	{
		base.StartCoroutine(this.DoPortraitSwapWithTiming(actor, turn));
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0004C14B File Offset: 0x0004A34B
	private IEnumerator DoPortraitSwapWithTiming(Actor actor, int turn)
	{
		if (actor == null)
		{
			yield break;
		}
		if (this.m_portraitSwapSpell != null)
		{
			Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_portraitSwapSpell);
			spell2.transform.parent = actor.transform;
			spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				if (spell.GetActiveState() == SpellStateType.NONE)
				{
					UnityEngine.Object.Destroy(spell.gameObject);
				}
			});
			spell2.SetSource(actor.gameObject);
			spell2.Activate();
			yield return new WaitForSeconds(this.m_portraitSwapDelay);
		}
		actor.SetPortraitTextureOverride(this.GetArtForTurn(turn));
		yield break;
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0004C168 File Offset: 0x0004A368
	private Texture2D GetArtForTurn(int turn)
	{
		return this.m_portraits[turn];
	}

	// Token: 0x0400090E RID: 2318
	public List<Texture2D> m_portraits;

	// Token: 0x0400090F RID: 2319
	public Spell m_portraitSwapSpell;

	// Token: 0x04000910 RID: 2320
	public float m_portraitSwapDelay = 0.5f;
}
