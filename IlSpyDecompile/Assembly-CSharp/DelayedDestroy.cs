using UnityEngine;

[ExecuteAlways]
public class DelayedDestroy : MonoBehaviour
{
	private void Awake()
	{
		base.hideFlags = HideFlags.HideAndDontSave;
		base.gameObject.hideFlags = HideFlags.HideAndDontSave;
	}

	private void Start()
	{
		Object.DestroyImmediate(base.transform.gameObject);
	}
}
