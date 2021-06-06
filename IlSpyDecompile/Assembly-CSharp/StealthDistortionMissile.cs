using UnityEngine;

public class StealthDistortionMissile : MonoBehaviour
{
	public GameObject ObjectToRender;

	public int ParticleResolutionX = 256;

	public int ParticleResolutionY = 256;

	public float ParticleAboveClip = 1f;

	public float ParticleBelowClip = 1f;

	public float RenderOffsetX;

	public float RenderOffsetY;

	public float RenderOffsetZ;

	public int DistortionResolutionX = 256;

	public int DistortionResolutionY = 256;

	public float DistortionAboveClip = -0.1f;

	public float DistortionBelowClip = 10f;

	private Camera particleCamera;

	private GameObject particleCameraGO;

	private RenderTexture particleRenderBuffer;

	private Camera boardCamera;

	private GameObject boardCameraGO;

	private RenderTexture boardRenderBuffer;

	private const int IgnoreLayer = 21;

	private float Yoffset;

	private static float s_Yoffset = 50f;

	private void Start()
	{
		Yoffset = s_Yoffset;
		s_Yoffset += 10f;
		PositionObjectToRender();
		CreateCameras();
		CreateRenderTextures();
		SetupCameras();
		SetupMaterial();
		GetComponent<Renderer>().enabled = true;
	}

	private void OnDestroy()
	{
		RenderTexture.ReleaseTemporary(particleRenderBuffer);
		RenderTexture.ReleaseTemporary(boardRenderBuffer);
	}

	private void CreateCameras()
	{
		if (particleCameraGO == null)
		{
			if (particleCamera != null)
			{
				Object.Destroy(particleCamera);
			}
			particleCameraGO = new GameObject();
			particleCamera = particleCameraGO.AddComponent<Camera>();
			particleCameraGO.name = base.name + "_DistortionParticleFXCamera";
		}
		if (boardCameraGO == null)
		{
			if (boardCamera != null)
			{
				Object.Destroy(boardCamera);
			}
			boardCameraGO = new GameObject();
			boardCamera = boardCameraGO.AddComponent<Camera>();
			boardCameraGO.name = base.name + "_DistortionBoardFXCamera";
		}
	}

	private void SetupCameras()
	{
		particleCamera.orthographic = true;
		particleCamera.orthographicSize = base.transform.localScale.x / 2f;
		particleCameraGO.transform.position = ObjectToRender.transform.position;
		particleCameraGO.transform.Translate(RenderOffsetX, RenderOffsetY, RenderOffsetZ);
		particleCameraGO.transform.rotation = ObjectToRender.transform.rotation;
		particleCameraGO.transform.Rotate(90f, 180f, 0f);
		particleCamera.transform.parent = base.transform;
		particleCamera.nearClipPlane = 0f - ParticleAboveClip;
		particleCamera.farClipPlane = ParticleBelowClip;
		particleCamera.targetTexture = particleRenderBuffer;
		particleCamera.depth = Camera.main.depth - 1f;
		particleCamera.backgroundColor = Color.black;
		particleCamera.clearFlags = CameraClearFlags.Color;
		particleCamera.cullingMask &= -2097153;
		particleCamera.enabled = true;
		particleCamera.allowHDR = false;
		boardCamera.orthographic = true;
		boardCamera.orthographicSize = base.transform.localScale.x / 2f;
		boardCameraGO.transform.position = base.transform.position;
		boardCameraGO.transform.rotation = base.transform.rotation;
		boardCameraGO.transform.Rotate(90f, 180f, 0f);
		boardCamera.transform.parent = base.transform;
		boardCamera.nearClipPlane = 0f - DistortionAboveClip;
		boardCamera.farClipPlane = DistortionBelowClip;
		boardCamera.targetTexture = boardRenderBuffer;
		boardCamera.depth = Camera.main.depth - 1f;
		boardCamera.backgroundColor = Color.black;
		boardCamera.clearFlags = CameraClearFlags.Color;
		boardCamera.cullingMask &= -2097153;
		boardCamera.enabled = true;
		boardCamera.allowHDR = false;
	}

	private void CreateRenderTextures()
	{
		particleRenderBuffer = RenderTexture.GetTemporary(ParticleResolutionX, ParticleResolutionY);
		boardRenderBuffer = RenderTexture.GetTemporary(DistortionResolutionX, DistortionResolutionY);
	}

	private void SetupMaterial()
	{
		Material material = base.gameObject.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = boardRenderBuffer;
		material.SetTexture("_ParticleTex", particleRenderBuffer);
	}

	private void PositionObjectToRender()
	{
		Vector3 vector = Vector3.up * Yoffset;
		ObjectToRender.transform.position += vector;
	}
}
