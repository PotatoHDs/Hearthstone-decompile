using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
	private Quaternion rotation;

	private void Awake()
	{
		rotation = base.transform.rotation;
	}

	private void LateUpdate()
	{
		base.transform.rotation = rotation;
	}
}
