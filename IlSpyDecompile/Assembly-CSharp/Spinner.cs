using UnityEngine;

public class Spinner : MonoBehaviour
{
	public float SpeedX;

	public float SpeedY;

	public void Update()
	{
		base.transform.Rotate(Vector3.right, Time.deltaTime * SpeedX);
		base.transform.Rotate(Vector3.up, Time.deltaTime * SpeedY, Space.World);
	}
}
