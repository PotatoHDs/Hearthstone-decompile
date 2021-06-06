using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2A RID: 3882
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class CameraAction : FsmStateAction
	{
		// Token: 0x0600AC2F RID: 44079 RVA: 0x0035B8A0 File Offset: 0x00359AA0
		protected Camera GetCamera(CameraAction.WhichCamera which, FsmGameObject specificCamera, FsmString namedCamera)
		{
			if (which != CameraAction.WhichCamera.SPECIFIC)
			{
				if (which == CameraAction.WhichCamera.NAMED)
				{
					string text = namedCamera.IsNone ? null : namedCamera.Value;
					if (!string.IsNullOrEmpty(text))
					{
						if (this.m_namedCamera)
						{
							if (this.m_namedCamera.name == text)
							{
								return this.m_namedCamera;
							}
							this.m_namedCamera = null;
						}
						Camera camera = null;
						GameObject gameObject = GameObject.Find(text);
						if (gameObject)
						{
							camera = gameObject.GetComponent<Camera>();
						}
						if (camera)
						{
							this.m_namedCamera = camera;
							return this.m_namedCamera;
						}
					}
				}
			}
			else if (!specificCamera.IsNone)
			{
				return specificCamera.Value.GetComponent<Camera>();
			}
			return Camera.main;
		}

		// Token: 0x040092F9 RID: 37625
		protected Camera m_namedCamera;

		// Token: 0x020027BA RID: 10170
		public enum WhichCamera
		{
			// Token: 0x0400F579 RID: 62841
			MAIN,
			// Token: 0x0400F57A RID: 62842
			SPECIFIC,
			// Token: 0x0400F57B RID: 62843
			NAMED
		}
	}
}
