using UnityEngine;

public class ShakeObject : MonoBehaviour
{
	public float amount = 1f;

	private Vector3 orgPos;

	private void Start()
	{
		orgPos = base.transform.position;
	}

	private void Update()
	{
		float num = Random.value * amount;
		float num2 = Random.value * amount;
		float num3 = Random.value * amount;
		num *= amount;
		num2 *= amount;
		num3 *= amount;
		base.transform.position = orgPos + new Vector3(num, num2, num3);
	}
}
