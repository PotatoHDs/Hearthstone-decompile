using UnityEngine;

[ExecuteInEditMode]
public class HastyLookat : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		base.transform.LookAt(target);
	}
}
