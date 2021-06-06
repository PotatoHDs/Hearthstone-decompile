using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class PopupCamera : MonoBehaviour
	{
		public Camera MirroredCamera { get; set; }

		public int CullingMask { get; set; }

		public float Depth { get; set; }

		public Camera Camera { get; private set; }

		private void Awake()
		{
			Camera = base.gameObject.AddComponent<Camera>();
			Camera.cullingMask = 0;
			Update();
		}

		private void Update()
		{
			if (MirroredCamera != null)
			{
				Transform obj = base.transform;
				Transform transform = MirroredCamera.transform;
				obj.position = transform.position;
				obj.rotation = transform.rotation;
				obj.localScale = transform.localScale;
				Camera.CopyFrom(MirroredCamera);
			}
			Camera.cullingMask = CullingMask;
			Camera.clearFlags = CameraClearFlags.Depth;
			Camera.depth = Depth;
		}
	}
}
