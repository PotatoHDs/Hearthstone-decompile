using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BEF RID: 3055
	public abstract class ComponentAction<T> : FsmStateAction where T : Component
	{
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06009D47 RID: 40263 RVA: 0x0032888B File Offset: 0x00326A8B
		protected Rigidbody rigidbody
		{
			get
			{
				return this.cachedComponent as Rigidbody;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06009D48 RID: 40264 RVA: 0x0032889D File Offset: 0x00326A9D
		protected Rigidbody2D rigidbody2d
		{
			get
			{
				return this.cachedComponent as Rigidbody2D;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06009D49 RID: 40265 RVA: 0x003288AF File Offset: 0x00326AAF
		protected Renderer renderer
		{
			get
			{
				return this.cachedComponent as Renderer;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06009D4A RID: 40266 RVA: 0x003288C1 File Offset: 0x00326AC1
		protected Animation animation
		{
			get
			{
				return this.cachedComponent as Animation;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06009D4B RID: 40267 RVA: 0x003288D3 File Offset: 0x00326AD3
		protected AudioSource audio
		{
			get
			{
				return this.cachedComponent as AudioSource;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06009D4C RID: 40268 RVA: 0x003288E5 File Offset: 0x00326AE5
		protected Camera camera
		{
			get
			{
				return this.cachedComponent as Camera;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06009D4D RID: 40269 RVA: 0x003288F7 File Offset: 0x00326AF7
		protected GUIText guiText
		{
			get
			{
				return this.cachedComponent as GUIText;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06009D4E RID: 40270 RVA: 0x00328909 File Offset: 0x00326B09
		protected GUITexture guiTexture
		{
			get
			{
				return this.cachedComponent as GUITexture;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06009D4F RID: 40271 RVA: 0x0032891B File Offset: 0x00326B1B
		protected Light light
		{
			get
			{
				return this.cachedComponent as Light;
			}
		}

		// Token: 0x06009D50 RID: 40272 RVA: 0x00328930 File Offset: 0x00326B30
		protected bool UpdateCache(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if (this.cachedComponent == null || this.cachedGameObject != go)
			{
				this.cachedComponent = go.GetComponent<T>();
				this.cachedGameObject = go;
				if (this.cachedComponent == null)
				{
					base.LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name);
				}
			}
			return this.cachedComponent != null;
		}

		// Token: 0x06009D51 RID: 40273 RVA: 0x003289CC File Offset: 0x00326BCC
		protected bool UpdateCacheAddComponent(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if (this.cachedComponent == null || this.cachedGameObject != go)
			{
				this.cachedComponent = go.GetComponent<T>();
				this.cachedGameObject = go;
				if (this.cachedComponent == null)
				{
					this.cachedComponent = go.AddComponent<T>();
					this.cachedComponent.hideFlags = HideFlags.DontSaveInEditor;
				}
			}
			return this.cachedComponent != null;
		}

		// Token: 0x06009D52 RID: 40274 RVA: 0x00328A59 File Offset: 0x00326C59
		protected void SendEvent(FsmEventTarget eventTarget, FsmEvent fsmEvent)
		{
			base.Fsm.Event(this.cachedGameObject, eventTarget, fsmEvent);
		}

		// Token: 0x040082B8 RID: 33464
		protected GameObject cachedGameObject;

		// Token: 0x040082B9 RID: 33465
		protected T cachedComponent;
	}
}
