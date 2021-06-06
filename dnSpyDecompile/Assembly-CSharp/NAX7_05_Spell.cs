using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class NAX7_05_Spell : Spell
{
	// Token: 0x06000D3B RID: 3387 RVA: 0x0004C2C1 File Offset: 0x0004A4C1
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.SpellEffect(prevStateType));
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0004C2D1 File Offset: 0x0004A4D1
	private IEnumerator SpellEffect(SpellStateType prevStateType)
	{
		PlayMakerFSM component = Board.Get().transform.Find("Board_NAX").Find("NAX_Crystal_Skinned").GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Debug.LogWarning("NAX7_05_Spell unable to get playmaker fsm");
			yield break;
		}
		component.SendEvent("ClickTop");
		base.OnBirth(prevStateType);
		this.OnSpellFinished();
		yield break;
	}
}
