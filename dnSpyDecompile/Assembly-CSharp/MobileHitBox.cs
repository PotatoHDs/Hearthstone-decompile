using System;
using UnityEngine;

// Token: 0x02000B20 RID: 2848
public class MobileHitBox : MonoBehaviour
{
	// Token: 0x0600974B RID: 38731 RVA: 0x0030E5A0 File Offset: 0x0030C7A0
	private void Start()
	{
		if (this.m_boxCollider != null && this.m_isMobile && (!this.m_phoneOnly || UniversalInputManager.UsePhoneUI))
		{
			Vector3 size = default(Vector3);
			size.x = this.m_boxCollider.size.x * this.m_scaleX;
			size.y = this.m_boxCollider.size.y * this.m_scaleY;
			if (this.m_scaleZ == 0f)
			{
				size.z = this.m_boxCollider.size.z * this.m_scaleY;
			}
			else
			{
				size.z = this.m_boxCollider.size.z * this.m_scaleZ;
			}
			this.m_boxCollider.size = size;
			this.m_boxCollider.center += this.m_offset;
			this.m_hasExecuted = true;
		}
	}

	// Token: 0x0600974C RID: 38732 RVA: 0x0030E6A3 File Offset: 0x0030C8A3
	public bool HasExecuted()
	{
		return this.m_hasExecuted;
	}

	// Token: 0x04007E95 RID: 32405
	public BoxCollider m_boxCollider;

	// Token: 0x04007E96 RID: 32406
	public float m_scaleX = 1f;

	// Token: 0x04007E97 RID: 32407
	public float m_scaleY = 1f;

	// Token: 0x04007E98 RID: 32408
	public float m_scaleZ;

	// Token: 0x04007E99 RID: 32409
	public Vector3 m_offset;

	// Token: 0x04007E9A RID: 32410
	public bool m_phoneOnly;

	// Token: 0x04007E9B RID: 32411
	private bool m_hasExecuted;

	// Token: 0x04007E9C RID: 32412
	private PlatformDependentValue<bool> m_isMobile = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		Tablet = true,
		MiniTablet = true,
		Phone = true,
		PC = false
	};
}
