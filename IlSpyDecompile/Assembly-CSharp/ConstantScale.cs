using UnityEngine;

public class ConstantScale : MonoBehaviour
{
	public Vector3 scale = Vector3.one;

	public bool everyFrame;

	private bool isItFirstIteration = true;

	private void LateUpdate()
	{
		if (!everyFrame)
		{
			if (!isItFirstIteration)
			{
				return;
			}
			isItFirstIteration = false;
		}
		Vector3 vector = Vector3.one;
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.transform.lossyScale;
		}
		if (vector.x + vector.y + vector.z == 0f)
		{
			vector = new Vector3(1E-05f, 1E-05f, 1E-05f);
		}
		base.transform.localScale = Vector3.Scale(new Vector3(1f / vector.x, 1f / vector.y, 1f / vector.z), scale);
	}
}
