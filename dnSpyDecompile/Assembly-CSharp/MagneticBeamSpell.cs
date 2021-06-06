using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E3 RID: 227
[RequireComponent(typeof(LineRenderer))]
public class MagneticBeamSpell : Spell
{
	// Token: 0x06000D36 RID: 3382 RVA: 0x0004C255 File Offset: 0x0004A455
	protected override void Awake()
	{
		base.Awake();
		this.m_lineRenderer = base.GetComponent<LineRenderer>();
		this.m_lineRenderer.enabled = false;
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0004C275 File Offset: 0x0004A475
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.m_lineRenderer.enabled = true;
		base.StartCoroutine(this.DoUpdate());
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0004C297 File Offset: 0x0004A497
	private IEnumerator DoUpdate()
	{
		for (;;)
		{
			this.m_lineRenderer.SetPosition(0, this.m_source.transform.position);
			this.m_lineRenderer.SetPosition(1, this.m_targets[0].transform.position);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0004C2A6 File Offset: 0x0004A4A6
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		this.m_lineRenderer.enabled = false;
		base.StopAllCoroutines();
	}

	// Token: 0x04000914 RID: 2324
	private LineRenderer m_lineRenderer;
}
