using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FF6 RID: 4086
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class PopupCamera : MonoBehaviour
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600B184 RID: 45444 RVA: 0x0036C205 File Offset: 0x0036A405
		// (set) Token: 0x0600B183 RID: 45443 RVA: 0x0036C1FC File Offset: 0x0036A3FC
		public Camera MirroredCamera { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600B186 RID: 45446 RVA: 0x0036C216 File Offset: 0x0036A416
		// (set) Token: 0x0600B185 RID: 45445 RVA: 0x0036C20D File Offset: 0x0036A40D
		public int CullingMask { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600B188 RID: 45448 RVA: 0x0036C227 File Offset: 0x0036A427
		// (set) Token: 0x0600B187 RID: 45447 RVA: 0x0036C21E File Offset: 0x0036A41E
		public float Depth { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600B18A RID: 45450 RVA: 0x0036C238 File Offset: 0x0036A438
		// (set) Token: 0x0600B189 RID: 45449 RVA: 0x0036C22F File Offset: 0x0036A42F
		public Camera Camera { get; private set; }

		// Token: 0x0600B18B RID: 45451 RVA: 0x0036C240 File Offset: 0x0036A440
		private void Awake()
		{
			this.Camera = base.gameObject.AddComponent<Camera>();
			this.Camera.cullingMask = 0;
			this.Update();
		}

		// Token: 0x0600B18C RID: 45452 RVA: 0x0036C268 File Offset: 0x0036A468
		private void Update()
		{
			if (this.MirroredCamera != null)
			{
				Transform transform = base.transform;
				Transform transform2 = this.MirroredCamera.transform;
				transform.position = transform2.position;
				transform.rotation = transform2.rotation;
				transform.localScale = transform2.localScale;
				this.Camera.CopyFrom(this.MirroredCamera);
			}
			this.Camera.cullingMask = this.CullingMask;
			this.Camera.clearFlags = CameraClearFlags.Depth;
			this.Camera.depth = this.Depth;
		}
	}
}
