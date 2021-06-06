using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class PopupRenderer : MonoBehaviour, IPopupRendering, ILayerOverridable
	{
		private PopupCamera m_popupCamera;

		private GameLayer m_layer;

		private Renderer[] m_renderers;

		private bool[] m_states;

		private GameLayer m_originalLayer;

		private GameLayer? m_layerOverride;

		public bool HandlesChildLayers => false;

		private void Awake()
		{
			m_originalLayer = (GameLayer)base.gameObject.layer;
		}

		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			base.enabled = true;
			m_layer = GameLayer.Reserved29;
			base.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			m_popupCamera = popupRoot.PopupCamera;
			base.gameObject.layer = GetLayer();
		}

		public void DisablePopupRendering()
		{
			base.enabled = false;
			m_layer = m_originalLayer;
			base.gameObject.layer = GetLayer();
		}

		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		public void SetLayerOverride(GameLayer layer)
		{
			m_layerOverride = layer;
		}

		public void ClearLayerOverride()
		{
			m_layerOverride = null;
		}

		private void Update()
		{
			base.gameObject.layer = GetLayer();
		}

		private void OnWillRenderObject()
		{
			if (m_popupCamera == null || Camera.current == m_popupCamera.Camera || (Camera.current != null && Camera.current.GetComponent<RenderToTextureCamera>() != null))
			{
				return;
			}
			m_renderers = GetComponents<Renderer>();
			if (m_states == null || m_states.Length != m_renderers.Length)
			{
				m_states = new bool[m_renderers.Length];
			}
			for (int i = 0; i < m_renderers.Length; i++)
			{
				Renderer renderer = m_renderers[i];
				m_states[i] = renderer.enabled;
				if (renderer != null)
				{
					renderer.enabled = false;
				}
			}
		}

		private void OnRenderObject()
		{
			if (m_renderers != null && m_states != null)
			{
				for (int i = 0; i < m_renderers.Length; i++)
				{
					m_renderers[i].enabled = m_states[i];
				}
			}
		}

		private int GetLayer()
		{
			if (m_layerOverride.HasValue)
			{
				return (int)m_layerOverride.Value;
			}
			return (int)m_layer;
		}
	}
}
