using UnityEngine;

namespace SpriteConvertTools
{
	[ExecuteInEditMode]
	public class SpriteMeshComponent : MonoBehaviour
	{
		public enum Mode
		{
			Sprite,
			Quad
		}

		public static string _meshPath = "Assets/Shared/Meshes/Generated";

		public static string _meshPrefix = "UI_";

		public static string _meshSuffix = "_mesh";

		[SerializeField]
		[HideInInspector]
		private Sprite _sprite;
	}
}
