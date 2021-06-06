using UnityEngine;

public class CameraSelector : MonoBehaviour
{
	public Vector3 cameraPosition;

	public Vector3 cameraRotation;

	public bool activateOnStart;

	private void Start()
	{
		if (activateOnStart)
		{
			Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
			Camera.main.transform.position = cameraPosition;
		}
	}

	private void OnMouseDown()
	{
		Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
		Camera.main.transform.position = cameraPosition;
	}
}
