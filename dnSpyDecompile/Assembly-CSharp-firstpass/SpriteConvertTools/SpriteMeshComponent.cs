using System;
using UnityEngine;

namespace SpriteConvertTools
{
	// Token: 0x02000544 RID: 1348
	[ExecuteInEditMode]
	public class SpriteMeshComponent : MonoBehaviour
	{
		// Token: 0x04001DF5 RID: 7669
		public static string _meshPath = "Assets/Shared/Meshes/Generated";

		// Token: 0x04001DF6 RID: 7670
		public static string _meshPrefix = "UI_";

		// Token: 0x04001DF7 RID: 7671
		public static string _meshSuffix = "_mesh";

		// Token: 0x04001DF8 RID: 7672
		[SerializeField]
		[HideInInspector]
		private Sprite _sprite;

		// Token: 0x02000700 RID: 1792
		public enum Mode
		{
			// Token: 0x040022D3 RID: 8915
			Sprite,
			// Token: 0x040022D4 RID: 8916
			Quad
		}
	}
}
