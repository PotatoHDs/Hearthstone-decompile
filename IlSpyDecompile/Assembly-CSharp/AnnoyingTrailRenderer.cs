using System;
using UnityEngine;

public class AnnoyingTrailRenderer : MonoBehaviour
{
	public GameObject GameObjectWithLineRenderer;

	public LineRenderer TargetLineRenderer;

	public int NumDivisions = 50;

	public float TrailMaxTimeLag = 0.5f;

	public GameObject GameObjectWithAnimator;

	public Animator _runningAnimator;

	public bool MoveToAnimParent;

	public AnimationClip SourceAnimationClip;

	public GameObject NodeAvatarParent;

	public GameObject NodeAvatar;

	private float _animTime;

	private Vector3[] _nodeArray;

	public bool ReverseNodeArray;

	public bool OutputWorldSpace = true;

	private float _cutoffTime;

	private void Start()
	{
	}

	private void OnEnable()
	{
		if ((bool)GameObjectWithAnimator)
		{
			_runningAnimator = GameObjectWithAnimator.GetComponent<Animator>();
		}
		if (!TargetLineRenderer)
		{
			TargetLineRenderer = GameObjectWithLineRenderer.GetComponent<LineRenderer>();
		}
		TargetLineRenderer.sortingOrder = 1000;
		if (MoveToAnimParent)
		{
			base.gameObject.transform.parent = GameObjectWithAnimator.transform.parent.transform;
			base.gameObject.transform.localPosition = GameObjectWithAnimator.transform.localPosition;
			base.gameObject.transform.localScale = GameObjectWithAnimator.transform.localScale;
			base.gameObject.transform.rotation = GameObjectWithAnimator.transform.rotation;
		}
	}

	private void Update()
	{
		if ((bool)_runningAnimator)
		{
			_animTime = GetAnimationPosition(_runningAnimator);
		}
		else if (_cutoffTime == 0f)
		{
			_cutoffTime = Time.time;
		}
		_nodeArray = RefreshNodeArray(NumDivisions, SourceAnimationClip, NodeAvatarParent, NodeAvatar, _animTime, TrailMaxTimeLag, _cutoffTime);
		RestructureLine(TargetLineRenderer, _nodeArray);
	}

	private Vector3[] RefreshNodeArray(int numDivisions, AnimationClip sourceAnimClip, GameObject nodeAvatarParent, GameObject nodeAvatar, float animTime, float trailMaxTimeLag, float cutoffTime)
	{
		float length = sourceAnimClip.length;
		float num = ((!_runningAnimator) ? (animTime * length + (Time.time - cutoffTime)) : (animTime * length));
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

	private void RestructureLine(LineRenderer lineRenderer, Vector3[] nodeArray)
	{
		lineRenderer.positionCount = nodeArray.Length;
		lineRenderer.SetPositions(nodeArray);
	}

	private float GetAnimationPosition(Animator runningAnimator)
	{
		float result = 0f;
		if (runningAnimator.GetCurrentAnimatorClipInfoCount(0) > 0)
		{
			result = runningAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}
		return result;
	}
}
