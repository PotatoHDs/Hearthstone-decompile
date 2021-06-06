using UnityEngine;

public class OffScreenRenderFX : MonoBehaviour
{
	public GameObject ObjectToRender;

	public bool UseBounds = true;

	public int RenderResolutionX = 256;

	public int RenderResolutionY = 256;

	public float AboveClip = 1f;

	public float BelowClip = 1f;

	public float ForceSize;

	public Rect CameraRect = new Rect(0f, 0f, 1f, 1f);

	private Camera offscreenFXCamera;

	private GameObject offscreenFXCameraGO;

	private RenderTexture tempRenderBuffer;

	private const int IgnoreLayer = 21;

	private float Yoffset;

	private Bounds RenderBounds;

	private static float s_Yoffset = 250f;

	private static float s_Xoffset = 250f;

	private void Start()
	{
		if (UseBounds)
		{
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			RenderBounds = mesh.bounds;
		}
		Yoffset = s_Yoffset;
		s_Yoffset += 10f;
		PositionObjectToRender();
		CreateCamera();
		CreateRenderTexture();
		SetupCamera();
		SetupMaterial();
		GetComponent<Renderer>().enabled = true;
	}

	private void OnDestroy()
	{
		if (tempRenderBuffer != null && tempRenderBuffer.IsCreated())
		{
			RenderTexture.ReleaseTemporary(tempRenderBuffer);
		}
	}

	private void CreateCamera()
	{
		if (offscreenFXCameraGO == null)
		{
			if (offscreenFXCamera != null)
			{
				Object.Destroy(offscreenFXCamera);
			}
			offscreenFXCameraGO = new GameObject();
			offscreenFXCamera = offscreenFXCameraGO.AddComponent<Camera>();
			offscreenFXCameraGO.name = base.name + "_OffScreenFXCamera";
			SceneUtils.SetHideFlags(offscreenFXCameraGO, HideFlags.HideAndDontSave);
			UniversalInputManager.Get().AddIgnoredCamera(offscreenFXCamera);
		}
	}

	private void SetupCamera()
	{
		offscreenFXCamera.orthographic = true;
		UpdateOffScreenCamera();
		offscreenFXCamera.transform.parent = base.transform;
		offscreenFXCamera.nearClipPlane = 0f - AboveClip;
		offscreenFXCamera.farClipPlane = BelowClip;
		offscreenFXCamera.targetTexture = tempRenderBuffer;
		offscreenFXCamera.depth = Camera.main.depth - 1f;
		offscreenFXCamera.backgroundColor = Color.black;
		offscreenFXCamera.clearFlags = CameraClearFlags.Color;
		offscreenFXCamera.rect = CameraRect;
		offscreenFXCamera.enabled = true;
		offscreenFXCamera.allowHDR = false;
	}

	private void UpdateOffScreenCamera()
	{
		if (ObjectToRender == null)
		{
			return;
		}
		if (ForceSize == 0f)
		{
			float num = base.transform.localScale.x;
			if (UseBounds)
			{
				num *= RenderBounds.size.x;
			}
			offscreenFXCamera.orthographicSize = num / 2f;
		}
		else
		{
			offscreenFXCamera.orthographicSize = ForceSize;
		}
		offscreenFXCameraGO.transform.position = ObjectToRender.transform.position;
		offscreenFXCameraGO.transform.rotation = ObjectToRender.transform.rotation;
		offscreenFXCameraGO.transform.Rotate(90f, 180f, 0f);
	}

	private void CreateRenderTexture()
	{
		tempRenderBuffer = RenderTexture.GetTemporary(RenderResolutionX, RenderResolutionY);
	}

	private void SetupMaterial()
	{
		base.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = tempRenderBuffer;
	}

	private void PositionObjectToRender()
	{
		Vector3 vector = Vector3.up * Yoffset;
		Vector3 vector2 = Vector3.right * s_Xoffset;
		if (ObjectToRender != null)
		{
			ObjectToRender.transform.position += vector;
		}
		ObjectToRender.transform.position += vector2;
	}
}
