using UnityEngine;

public class MobileActorMinion : MonoBehaviour
{
	public Vector3 m_minionScaleFactor = Vector3.one;

	private void Awake()
	{
		if (PlatformSettings.IsMobile() && (bool)UniversalInputManager.UsePhoneUI)
		{
			Vector3 localScale = base.gameObject.transform.localScale;
			localScale.x *= m_minionScaleFactor.x;
			localScale.y *= m_minionScaleFactor.y;
			localScale.z *= m_minionScaleFactor.z;
			base.gameObject.transform.localScale = localScale;
		}
	}
}
