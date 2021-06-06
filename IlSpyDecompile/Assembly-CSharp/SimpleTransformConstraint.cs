using UnityEngine;

[ExecuteInEditMode]
public class SimpleTransformConstraint : MonoBehaviour
{
	public int currentParent;

	public Transform[] parents;

	public bool position = true;

	public bool rotation = true;

	public bool scale = true;

	private void Update()
	{
		if (position)
		{
			base.transform.position = parents[currentParent].position;
		}
		if (rotation)
		{
			base.transform.rotation = parents[currentParent].rotation;
		}
		if (scale)
		{
			base.transform.localScale = parents[currentParent].localScale;
		}
	}
}
