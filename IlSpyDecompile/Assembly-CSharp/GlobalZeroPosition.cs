using UnityEngine;

public class GlobalZeroPosition : MonoBehaviour
{
	private void LateUpdate()
	{
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
	}
}
