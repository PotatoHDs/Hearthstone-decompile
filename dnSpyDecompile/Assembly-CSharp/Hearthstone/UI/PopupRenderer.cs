using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FF7 RID: 4087
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class PopupRenderer : MonoBehaviour, IPopupRendering, ILayerOverridable
	{
		// Token: 0x0600B18E RID: 45454 RVA: 0x0036C2F7 File Offset: 0x0036A4F7
		private void Awake()
		{
			this.m_originalLayer = (GameLayer)base.gameObject.layer;
		}

		// Token: 0x0600B18F RID: 45455 RVA: 0x0036C30A File Offset: 0x0036A50A
		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			base.enabled = true;
			this.m_layer = GameLayer.Reserved29;
			base.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.m_popupCamera = popupRoot.PopupCamera;
			base.gameObject.layer = this.GetLayer();
		}

		// Token: 0x0600B190 RID: 45456 RVA: 0x0036C340 File Offset: 0x0036A540
		public void DisablePopupRendering()
		{
			base.enabled = false;
			this.m_layer = this.m_originalLayer;
			base.gameObject.layer = this.GetLayer();
		}

		// Token: 0x0600B191 RID: 45457 RVA: 0x000052EC File Offset: 0x000034EC
		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600B192 RID: 45458 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool HandlesChildLayers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B193 RID: 45459 RVA: 0x0036C366 File Offset: 0x0036A566
		public void SetLayerOverride(GameLayer layer)
		{
			this.m_layerOverride = new GameLayer?(layer);
		}

		// Token: 0x0600B194 RID: 45460 RVA: 0x0036C374 File Offset: 0x0036A574
		public void ClearLayerOverride()
		{
			this.m_layerOverride = null;
		}

		// Token: 0x0600B195 RID: 45461 RVA: 0x0036C382 File Offset: 0x0036A582
		private void Update()
		{
			base.gameObject.layer = this.GetLayer();
		}

		// Token: 0x0600B196 RID: 45462 RVA: 0x0036C398 File Offset: 0x0036A598
		private void OnWillRenderObject()
		{
			if (this.m_popupCamera == null || Camera.current == this.m_popupCamera.Camera)
			{
				return;
			}
			if (Camera.current != null && Camera.current.GetComponent<RenderToTextureCamera>() != null)
			{
				return;
			}
			this.m_renderers = base.GetComponents<Renderer>();
			if (this.m_states == null || this.m_states.Length != this.m_renderers.Length)
			{
				this.m_states = new bool[this.m_renderers.Length];
			}
			for (int i = 0; i < this.m_renderers.Length; i++)
			{
				Renderer renderer = this.m_renderers[i];
				this.m_states[i] = renderer.enabled;
				if (renderer != null)
				{
					renderer.enabled = false;
				}
			}
		}

		// Token: 0x0600B197 RID: 45463 RVA: 0x0036C460 File Offset: 0x0036A660
		private void OnRenderObject()
		{
			if (this.m_renderers != null && this.m_states != null)
			{
				for (int i = 0; i < this.m_renderers.Length; i++)
				{
					this.m_renderers[i].enabled = this.m_states[i];
				}
			}
		}

		// Token: 0x0600B198 RID: 45464 RVA: 0x0036C4A5 File Offset: 0x0036A6A5
		private int GetLayer()
		{
			if (this.m_layerOverride != null)
			{
				return (int)this.m_layerOverride.Value;
			}
			return (int)this.m_layer;
		}

		// Token: 0x040095B6 RID: 38326
		private PopupCamera m_popupCamera;

		// Token: 0x040095B7 RID: 38327
		private GameLayer m_layer;

		// Token: 0x040095B8 RID: 38328
		private Renderer[] m_renderers;

		// Token: 0x040095B9 RID: 38329
		private bool[] m_states;

		// Token: 0x040095BA RID: 38330
		private GameLayer m_originalLayer;

		// Token: 0x040095BB RID: 38331
		private GameLayer? m_layerOverride;
	}
}
