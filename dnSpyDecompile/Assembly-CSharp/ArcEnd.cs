using System;
using UnityEngine;

// Token: 0x02000755 RID: 1877
public class ArcEnd : MonoBehaviour
{
	// Token: 0x060069BD RID: 27069 RVA: 0x00227010 File Offset: 0x00225210
	private void Start()
	{
		this.s = base.transform.localScale;
	}

	// Token: 0x060069BE RID: 27070 RVA: 0x00227024 File Offset: 0x00225224
	private void FixedUpdate()
	{
		Vector3 upwards = Camera.main.transform.position - base.transform.position;
		Quaternion rotation = Quaternion.LookRotation(Vector3.up, upwards);
		base.transform.rotation = rotation;
		base.transform.Rotate(Vector3.up, UnityEngine.Random.value * 360f);
		if (UnityEngine.Random.value > 0.8f)
		{
			base.transform.localScale = this.s * 1.5f;
			if (this.l != null)
			{
				this.l.range = 100f;
				this.l.intensity = 1.5f;
				return;
			}
		}
		else
		{
			base.transform.localScale = this.s;
			if (this.l != null)
			{
				this.l.range = 50f;
				this.l.intensity = 1f;
			}
		}
	}

	// Token: 0x04005682 RID: 22146
	private Vector3 s;

	// Token: 0x04005683 RID: 22147
	public Light l;
}
