using System;
using UnityEngine;

// Token: 0x02000A95 RID: 2709
public class SpotlightConstraint : MonoBehaviour
{
	// Token: 0x060090B5 RID: 37045 RVA: 0x002EEE08 File Offset: 0x002ED008
	private void OnEnable()
	{
		if (!this.SpotlightSource || !this.SpotlightTarget || !this.GroundCircle)
		{
			return;
		}
		this.targetMultiplier = this.maxLeftRotationTarget / this.maxLeftPosition * -1f;
		this.circleMultiplier = this.maxLeftCircleScale / this.maxLeftPosition * -1f;
	}

	// Token: 0x060090B6 RID: 37046 RVA: 0x002EEE70 File Offset: 0x002ED070
	private void Update()
	{
		this.originalTargetPosition = this.SpotlightTarget.transform.position;
		this.SpotlightTarget.transform.position = this.originalTargetPosition;
		this.eulerRotation = new Vector3(0f, this.targetMultiplier * this.SpotlightTarget.transform.position.x, 0f);
		this.SpotlightTarget.transform.localRotation = Quaternion.Euler(this.eulerRotation);
		this.eulerRotation = new Vector3(0f, 0f, 0f);
		this.GroundCircle.transform.rotation = Quaternion.Euler(this.eulerRotation);
		this.GroundCircle.transform.localScale = new Vector3(this.circleMultiplier * this.SpotlightTarget.transform.position.x + 1f, 1f, 1f);
	}

	// Token: 0x0400797E RID: 31102
	public GameObject SpotlightSource;

	// Token: 0x0400797F RID: 31103
	public GameObject SpotlightTarget;

	// Token: 0x04007980 RID: 31104
	public GameObject GroundCircle;

	// Token: 0x04007981 RID: 31105
	private float maxLeftPosition = 14f;

	// Token: 0x04007982 RID: 31106
	private float maxLeftRotationTarget = 50f;

	// Token: 0x04007983 RID: 31107
	private float maxLeftCircleScale = 0.25f;

	// Token: 0x04007984 RID: 31108
	private float targetMultiplier;

	// Token: 0x04007985 RID: 31109
	private float circleMultiplier;

	// Token: 0x04007986 RID: 31110
	private Vector3 eulerRotation;

	// Token: 0x04007987 RID: 31111
	private Vector3 originalTargetPosition;
}
