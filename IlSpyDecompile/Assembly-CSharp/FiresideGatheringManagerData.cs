using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringManagerData : ScriptableObject
{
	[Serializable]
	public class SignTypeMapping
	{
		public TavernSignType m_type;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_prefabName;
	}

	public Vector3_MobileOverride m_nearbyFiresidePopupOffset;

	public Vector3_MobileOverride m_nearbyFiresidePopupScale;

	public Vector3_MobileOverride m_nearbyFiresidePopupRotation;

	public Vector3_MobileOverride m_returnToFsgFriendListPopupOffset;

	public Vector3_MobileOverride m_signPosition;

	public Vector3_MobileOverride m_signScale;

	public List<SignTypeMapping> m_signTypeMapping = new List<SignTypeMapping>();

	public bool m_hasSeenReturnToFSGSceneTooltip;
}
