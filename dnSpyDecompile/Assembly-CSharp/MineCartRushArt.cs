using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class MineCartRushArt : MonoBehaviour
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x0004C189 File Offset: 0x0004A389
	public void DoPortraitSwap(Actor actor)
	{
		base.StartCoroutine(this.DoPortraitSwapWithTiming(actor));
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0004C199 File Offset: 0x0004A399
	private IEnumerator DoPortraitSwapWithTiming(Actor actor)
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
		actor.SetPortraitTextureOverride(this.GetNextPortrait());
		yield break;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0004C1B0 File Offset: 0x0004A3B0
	private Texture2D GetNextPortrait()
	{
		if (this.m_portraits.Count == 0)
		{
			return null;
		}
		if (this.m_portraits.Count == 1)
		{
			return this.m_portraits[0];
		}
		Texture2D value = this.m_portraits[0];
		int index = UnityEngine.Random.Range(1, this.m_portraits.Count);
		this.m_portraits[0] = this.m_portraits[index];
		this.m_portraits[index] = value;
		return this.m_portraits[0];
	}

	// Token: 0x04000911 RID: 2321
	public List<Texture2D> m_portraits = new List<Texture2D>();

	// Token: 0x04000912 RID: 2322
	public Spell m_portraitSwapSpell;

	// Token: 0x04000913 RID: 2323
	public float m_portraitSwapDelay = 0.5f;
}
