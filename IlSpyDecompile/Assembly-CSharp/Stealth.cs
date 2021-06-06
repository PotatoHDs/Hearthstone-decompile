using UnityEngine;

public class Stealth : MonoBehaviour
{
	public int RenderResolutionX = 256;

	public int RenderResolutionY = 256;

	public float AboveClip = 0.4f;

	public float BelowClip = 0.4f;

	private Camera stealthCamera;

	private GameObject stealthCameraGO;

	private RenderTexture tempRenderBuffer;

	private const int IgnoreLayer = 21;

	private void Start()
	{
		CreateCamera();
		CreateRenderTexture();
		SetupCamera();
		SetupMaterial();
	}

	private void OnDestroy()
	{
		RenderTexture.ReleaseTemporary(tempRenderBuffer);
	}

	private void CameraRender()
	{
		stealthCamera.Render();
	}

	private void CreateCamera()
	{
		if (stealthCameraGO == null)
		{
			if (stealthCamera != null)
			{
				Object.Destroy(stealthCamera);
			}
			stealthCameraGO = new GameObject();
			stealthCamera = stealthCameraGO.AddComponent<Camera>();
			stealthCameraGO.name = base.name + "_StealthFXCamera";
			SceneUtils.SetHideFlags(stealthCameraGO, HideFlags.HideAndDontSave);
		}
	}

	private void SetupCamera()
	{
		stealthCamera.orthographic = true;
		UpdateOffScreenCamera();
		stealthCamera.transform.parent = base.transform;
		stealthCamera.nearClipPlane = 0f - AboveClip;
		stealthCamera.farClipPlane = BelowClip;
		stealthCamera.targetTexture = tempRenderBuffer;
		stealthCamera.depth = Camera.main.depth - 1f;
		stealthCamera.backgroundColor = Color.black;
		stealthCamera.clearFlags = CameraClearFlags.Color;
		stealthCamera.cullingMask &= -2097153;
		stealthCamera.enabled = false;
		stealthCamera.allowHDR = false;
	}

	private void UpdateOffScreenCamera()
	{
		stealthCamera.orthographicSize = 1f;
		stealthCameraGO.transform.position = base.transform.position;
		stealthCameraGO.transform.rotation = base.transform.rotation;
		stealthCameraGO.transform.Rotate(90f, 180f, 0f);
	}

	private void CreateRenderTexture()
	{
		tempRenderBuffer = RenderTexture.GetTemporary(RenderResolutionX, RenderResolutionY);
	}

	private void SetupMaterial()
	{
		base.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = tempRenderBuffer;
	}
}
