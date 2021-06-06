using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x020002EB RID: 747
[CustomEditClass]
public class FiresideGatheringManagerData : ScriptableObject
{
	// Token: 0x0400168E RID: 5774
	public Vector3_MobileOverride m_nearbyFiresidePopupOffset;

	// Token: 0x0400168F RID: 5775
	public Vector3_MobileOverride m_nearbyFiresidePopupScale;

	// Token: 0x04001690 RID: 5776
	public Vector3_MobileOverride m_nearbyFiresidePopupRotation;

	// Token: 0x04001691 RID: 5777
	public Vector3_MobileOverride m_returnToFsgFriendListPopupOffset;

	// Token: 0x04001692 RID: 5778
	public Vector3_MobileOverride m_signPosition;

	// Token: 0x04001693 RID: 5779
	public Vector3_MobileOverride m_signScale;

	// Token: 0x04001694 RID: 5780
	public List<FiresideGatheringManagerData.SignTypeMapping> m_signTypeMapping = new List<FiresideGatheringManagerData.SignTypeMapping>();

	// Token: 0x04001695 RID: 5781
	public bool m_hasSeenReturnToFSGSceneTooltip;

	// Token: 0x02001618 RID: 5656
	[Serializable]
	public class SignTypeMapping
	{
		// Token: 0x0400AFCC RID: 45004
		public TavernSignType m_type;

		// Token: 0x0400AFCD RID: 45005
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_prefabName;
	}
}
