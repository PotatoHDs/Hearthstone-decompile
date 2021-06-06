using System;
using UnityEngine;

// Token: 0x0200069F RID: 1695
public class SetRotationIcon : MonoBehaviour
{
	// Token: 0x06005EA9 RID: 24233 RVA: 0x001EC6ED File Offset: 0x001EA8ED
	private void Awake()
	{
		this.m_YearIconQuad.GetComponent<Renderer>().GetMaterial().mainTextureOffset = this.GetYearIconTextureOffset();
	}

	// Token: 0x06005EAA RID: 24234 RVA: 0x001EC70C File Offset: 0x001EA90C
	private Vector2 GetYearIconTextureOffset()
	{
		Vector2 set_ROTATION_FIRST_TEXTURE_OFFSET = SetRotationIcon.SET_ROTATION_FIRST_TEXTURE_OFFSET;
		Vector2 set_ROTATION_SECOND_TEXTURE_OFFSET = SetRotationIcon.SET_ROTATION_SECOND_TEXTURE_OFFSET;
		if (!SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, true))
		{
			return set_ROTATION_SECOND_TEXTURE_OFFSET;
		}
		return set_ROTATION_FIRST_TEXTURE_OFFSET;
	}

	// Token: 0x04004FCF RID: 20431
	public GameObject m_YearIconQuad;

	// Token: 0x04004FD0 RID: 20432
	public const string SHOW_EVENT = "SHOW";

	// Token: 0x04004FD1 RID: 20433
	public const string HIDE_EVENT = "HIDE";

	// Token: 0x04004FD2 RID: 20434
	private static Vector2 SET_ROTATION_FIRST_TEXTURE_OFFSET = new Vector2(0f, 0.5f);

	// Token: 0x04004FD3 RID: 20435
	private static Vector2 SET_ROTATION_SECOND_TEXTURE_OFFSET = new Vector2(0.5f, 0.5f);
}
