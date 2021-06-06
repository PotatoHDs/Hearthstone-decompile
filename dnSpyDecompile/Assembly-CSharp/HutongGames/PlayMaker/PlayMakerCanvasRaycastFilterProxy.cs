using System;
using UnityEngine;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B8D RID: 2957
	public class PlayMakerCanvasRaycastFilterProxy : MonoBehaviour, ICanvasRaycastFilter
	{
		// Token: 0x06009B5D RID: 39773 RVA: 0x0031F589 File Offset: 0x0031D789
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.RayCastingEnabled;
		}

		// Token: 0x040080D2 RID: 32978
		public bool RayCastingEnabled = true;
	}
}
