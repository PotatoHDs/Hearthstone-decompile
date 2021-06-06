using UnityEngine;

public class FiresideGatheringSignAnimationCallbackBehaviour : MonoBehaviour
{
	public FiresideGatheringSign m_ParentSign;

	public void EnableShadowOnSign()
	{
		m_ParentSign.SetSignShadowEnabled(enabled: true);
	}

	public void DisableShadowOnSign()
	{
		m_ParentSign.SetSignShadowEnabled(enabled: false);
	}

	public void OnSignSocketAnimationComplete()
	{
		m_ParentSign.FireSignSocketAnimationCompleteListener();
	}
}
