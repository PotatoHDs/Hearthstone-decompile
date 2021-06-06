using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
	private ScreenEffectsMgr m_ScreenEffectsMgr;

	private void Awake()
	{
		m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
	}

	private void OnEnable()
	{
		if (m_ScreenEffectsMgr == null)
		{
			m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
		}
		ScreenEffectsMgr.RegisterScreenEffect(this);
	}

	private void OnDisable()
	{
		if (m_ScreenEffectsMgr == null)
		{
			m_ScreenEffectsMgr = ScreenEffectsMgr.Get();
		}
		if (m_ScreenEffectsMgr != null)
		{
			ScreenEffectsMgr.UnRegisterScreenEffect(this);
		}
	}
}
