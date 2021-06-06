using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
	public float RotateSpeedX;

	public float RotateSpeedY;

	public float RotateSpeedZ;

	public bool RandomStartX;

	public bool RandomStartY;

	public bool RandomStartZ;

	private void Start()
	{
		if (RandomStartX)
		{
			base.transform.Rotate(Vector3.left, Random.Range(0, 360));
		}
		if (RandomStartY)
		{
			base.transform.Rotate(Vector3.up, Random.Range(0, 360));
		}
		if (RandomStartZ)
		{
			base.transform.Rotate(Vector3.forward, Random.Range(0, 360));
		}
	}

	private void Update()
	{
		base.transform.Rotate(Vector3.left, Time.deltaTime * RotateSpeedX, Space.Self);
		base.transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeedY, Space.Self);
		base.transform.Rotate(Vector3.forward, Time.deltaTime * RotateSpeedZ, Space.Self);
	}
}
