using System;
using UnityEngine;

// Token: 0x02000A4B RID: 2635
[ExecuteAlways]
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06008E5E RID: 36446 RVA: 0x002DF080 File Offset: 0x002DD280
	private void Awake()
	{
		this.CreateLookAtTarget();
	}

	// Token: 0x06008E5F RID: 36447 RVA: 0x002DF088 File Offset: 0x002DD288
	private void Start()
	{
		this.m_MainCamera = Camera.main;
	}

	// Token: 0x06008E60 RID: 36448 RVA: 0x002DF098 File Offset: 0x002DD298
	private void Update()
	{
		if (this.m_MainCamera == null)
		{
			this.m_MainCamera = Camera.main;
			if (this.m_MainCamera == null)
			{
				return;
			}
		}
		if (this.m_LookAtTarget == null)
		{
			this.CreateLookAtTarget();
			if (this.m_LookAtTarget == null)
			{
				return;
			}
		}
		this.m_LookAtTarget.transform.position = this.m_MainCamera.transform.position + this.m_LookAtPositionOffset;
		base.transform.LookAt(this.m_LookAtTarget.transform, this.Z_VECTOR);
		base.transform.Rotate(this.X_VECTOR, 90f);
		base.transform.Rotate(this.Y_VECTOR, 180f);
	}

	// Token: 0x06008E61 RID: 36449 RVA: 0x002DF163 File Offset: 0x002DD363
	private void OnDestroy()
	{
		if (this.m_LookAtTarget)
		{
			UnityEngine.Object.Destroy(this.m_LookAtTarget);
		}
	}

	// Token: 0x06008E62 RID: 36450 RVA: 0x002DF17D File Offset: 0x002DD37D
	private void CreateLookAtTarget()
	{
		this.m_LookAtTarget = new GameObject();
		this.m_LookAtTarget.name = "LookAtCamera Target";
	}

	// Token: 0x0400767C RID: 30332
	private readonly Vector3 X_VECTOR = new Vector3(1f, 0f, 0f);

	// Token: 0x0400767D RID: 30333
	private readonly Vector3 Y_VECTOR = new Vector3(0f, 1f, 0f);

	// Token: 0x0400767E RID: 30334
	private readonly Vector3 Z_VECTOR = new Vector3(0f, 0f, 1f);

	// Token: 0x0400767F RID: 30335
	public Vector3 m_LookAtPositionOffset = Vector3.zero;

	// Token: 0x04007680 RID: 30336
	private Camera m_MainCamera;

	// Token: 0x04007681 RID: 30337
	private GameObject m_LookAtTarget;
}
