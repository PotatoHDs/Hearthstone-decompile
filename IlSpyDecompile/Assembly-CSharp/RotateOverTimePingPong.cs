using UnityEngine;

public class RotateOverTimePingPong : MonoBehaviour
{
	public float RotateSpeedX;

	public float RotateSpeedY;

	public float RotateSpeedZ;

	public bool RandomStartX = true;

	public bool RandomStartY = true;

	public bool RandomStartZ = true;

	public float RotateRangeXmin;

	public float RotateRangeXmax = 10f;

	public float RotateRangeYmin;

	public float RotateRangeYmax = 10f;

	public float RotateRangeZmin;

	public float RotateRangeZmax = 10f;

	private void Start()
	{
		if (RandomStartX)
		{
			base.transform.Rotate(Vector3.left, Random.Range(RotateRangeXmin, RotateRangeXmax));
		}
		if (RandomStartY)
		{
			base.transform.Rotate(Vector3.up, Random.Range(RotateRangeYmin, RotateRangeYmax));
		}
		if (RandomStartZ)
		{
			base.transform.Rotate(Vector3.forward, Random.Range(RotateRangeZmin, RotateRangeZmax));
		}
	}

	private void Update()
	{
		float z = Mathf.Sin(Time.time) * RotateRangeZmax;
		float y = base.gameObject.transform.localRotation.y;
		iTween.RotateUpdate(base.gameObject, iTween.Hash("rotation", new Vector3(0f, y, z), "isLocal", true, "time", 0));
	}
}
