using System;
using UnityEngine;

public class TrailFromAnimation : MonoBehaviour
{
	[Tooltip("Can output node pathing to multiple line renderers")]
	public LineRenderer[] TargetLineRenderers;

	[Tooltip("The maximum number of divisions at full length. Number of divisions scales as trail gets shorter.")]
	public int MaxNumDivisions = 50;

	[Tooltip("Maximum length of trail in terms of time. Trail automatically shortens at beginning and end of lifetime.")]
	public float TrailLengthTime = 0.5f;

	[Tooltip("Scales length of trail over lifetime.")]
	public AnimationCurve LengthScaleOverTime;

	[Tooltip("The animation clip to use for trail pathing.")]
	public AnimationClip SourceAnimationClip;

	[Tooltip("The dummy gameobject tree to use for animation simulations. Need to match heirarchy of original animated object. Top level of heirarchy should correlate to the parent object that original animator & animation clip are attached to.")]
	public GameObject NodeAvatarParent;

	[Tooltip("The specific gameobject in the heirarchy that the trail pathing should follow")]
	public GameObject NodeAvatar;

	private float _animTime;

	private Vector3[] _nodeArray;

	[Tooltip("Output pathing nodes in reverse")]
	public bool ReverseNodeArray;

	[Tooltip("Output pathing nodes to world space instead of local space")]
	public bool OutputWorldSpace;

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
		_animTime += Time.deltaTime;
		_nodeArray = RefreshNodeArray(MaxNumDivisions, SourceAnimationClip, NodeAvatarParent, NodeAvatar, _animTime, TrailLengthTime);
		RestructureLines(TargetLineRenderers, _nodeArray);
	}

	private Vector3[] RefreshNodeArray(int numDivisions, AnimationClip sourceAnimClip, GameObject nodeAvatarParent, GameObject nodeAvatar, float animTime, float trailMaxTimeLag)
	{
		float length = sourceAnimClip.length;
		float num = animTime * length;
		float num2 = Mathf.Clamp(num, 0f, length);
		float num3 = Mathf.Clamp(num - trailMaxTimeLag, 0f, length);
		float num4 = num2 - num3;
		float num5 = num4 / trailMaxTimeLag;
		int num6 = Mathf.RoundToInt((float)numDivisions * num5);
		float num7 = num4 / (float)num6;
		Vector3[] array = new Vector3[num6];
		for (int i = 0; i < num6; i++)
		{
			float time = num2 - num7 * (float)i;
			sourceAnimClip.SampleAnimation(nodeAvatarParent, time);
			if (OutputWorldSpace)
			{
				array[i] = nodeAvatar.transform.position;
			}
			else
			{
				array[i] = nodeAvatar.transform.localPosition;
			}
		}
		if (ReverseNodeArray)
		{
			Array.Reverse(array);
		}
		return array;
	}

	private void RestructureLines(LineRenderer[] lineRenderers, Vector3[] nodeArray)
	{
		for (int i = 0; i < lineRenderers.Length; i++)
		{
			lineRenderers[i].SetPositions(nodeArray);
			lineRenderers[i].positionCount = nodeArray.Length;
		}
	}
}
