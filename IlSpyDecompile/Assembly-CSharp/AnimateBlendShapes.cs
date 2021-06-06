using UnityEngine;

[ExecuteAlways]
public class AnimateBlendShapes : MonoBehaviour
{
	private float prevBlendAmount;

	public float blendAmount;

	public int index;

	private SkinnedMeshRenderer skinMR;

	private void Start()
	{
		skinMR = GetComponent<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (prevBlendAmount != blendAmount)
		{
			prevBlendAmount = blendAmount;
			skinMR.SetBlendShapeWeight(index, blendAmount);
		}
	}
}
