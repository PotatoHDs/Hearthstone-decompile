using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public Transform targetObj;

	private void LateUpdate()
	{
		base.transform.position = targetObj.position;
	}
}
