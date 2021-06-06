using UnityEngine;

[ExecuteAlways]
public class ScreenEffectGlow : ScreenEffect
{
	public bool m_RenderGlowOnly;

	private bool m_PreviousRenderGlowOnly;

	private int m_PreviousLayer;

	private void Awake()
	{
		m_PreviousLayer = base.gameObject.layer;
	}

	private void Start()
	{
		SetLayer();
	}

	private void Update()
	{
	}

	private void SetLayer()
	{
		if (m_PreviousRenderGlowOnly != m_RenderGlowOnly)
		{
			m_PreviousRenderGlowOnly = m_RenderGlowOnly;
			if (m_RenderGlowOnly)
			{
				m_PreviousLayer = base.gameObject.layer;
				SceneUtils.SetLayer(base.gameObject, GameLayer.ScreenEffects);
			}
			else
			{
				SceneUtils.SetLayer(base.gameObject, m_PreviousLayer);
			}
		}
	}
}
