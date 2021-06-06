using System;
using UnityEngine;

// Token: 0x02000A0F RID: 2575
public class AnnoyingTrailRenderer : MonoBehaviour
{
	// Token: 0x06008B35 RID: 35637 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06008B36 RID: 35638 RVA: 0x002C7DC8 File Offset: 0x002C5FC8
	private void OnEnable()
	{
		if (this.GameObjectWithAnimator)
		{
			this._runningAnimator = this.GameObjectWithAnimator.GetComponent<Animator>();
		}
		if (!this.TargetLineRenderer)
		{
			this.TargetLineRenderer = this.GameObjectWithLineRenderer.GetComponent<LineRenderer>();
		}
		this.TargetLineRenderer.sortingOrder = 1000;
		if (this.MoveToAnimParent)
		{
			base.gameObject.transform.parent = this.GameObjectWithAnimator.transform.parent.transform;
			base.gameObject.transform.localPosition = this.GameObjectWithAnimator.transform.localPosition;
			base.gameObject.transform.localScale = this.GameObjectWithAnimator.transform.localScale;
			base.gameObject.transform.rotation = this.GameObjectWithAnimator.transform.rotation;
		}
	}

	// Token: 0x06008B37 RID: 35639 RVA: 0x002C7EB4 File Offset: 0x002C60B4
	private void Update()
	{
		if (this._runningAnimator)
		{
			this._animTime = this.GetAnimationPosition(this._runningAnimator);
		}
		else if (this._cutoffTime == 0f)
		{
			this._cutoffTime = Time.time;
		}
		this._nodeArray = this.RefreshNodeArray(this.NumDivisions, this.SourceAnimationClip, this.NodeAvatarParent, this.NodeAvatar, this._animTime, this.TrailMaxTimeLag, this._cutoffTime);
		this.RestructureLine(this.TargetLineRenderer, this._nodeArray);
	}

	// Token: 0x06008B38 RID: 35640 RVA: 0x002C7F44 File Offset: 0x002C6144
	private Vector3[] RefreshNodeArray(int numDivisions, AnimationClip sourceAnimClip, GameObject nodeAvatarParent, GameObject nodeAvatar, float animTime, float trailMaxTimeLag, float cutoffTime)
	{
		float length = sourceAnimClip.length;
		float num;
		if (this._runningAnimator)
		{
			num = animTime * length;
		}
		else
		{
			num = animTime * length + (Time.time - cutoffTime);
		}
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
			if (this.OutputWorldSpace)
			{
				array[i] = nodeAvatar.transform.position;
			}
			else
			{
				array[i] = nodeAvatar.transform.localPosition;
			}
		}
		if (this.ReverseNodeArray)
		{
			Array.Reverse(array);
		}
		return array;
	}

	// Token: 0x06008B39 RID: 35641 RVA: 0x002C8024 File Offset: 0x002C6224
	private void RestructureLine(LineRenderer lineRenderer, Vector3[] nodeArray)
	{
		lineRenderer.positionCount = nodeArray.Length;
		lineRenderer.SetPositions(nodeArray);
	}

	// Token: 0x06008B3A RID: 35642 RVA: 0x002C8038 File Offset: 0x002C6238
	private float GetAnimationPosition(Animator runningAnimator)
	{
		float result = 0f;
		if (runningAnimator.GetCurrentAnimatorClipInfoCount(0) > 0)
		{
			result = runningAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}
		return result;
	}

	// Token: 0x040073A9 RID: 29609
	public GameObject GameObjectWithLineRenderer;

	// Token: 0x040073AA RID: 29610
	public LineRenderer TargetLineRenderer;

	// Token: 0x040073AB RID: 29611
	public int NumDivisions = 50;

	// Token: 0x040073AC RID: 29612
	public float TrailMaxTimeLag = 0.5f;

	// Token: 0x040073AD RID: 29613
	public GameObject GameObjectWithAnimator;

	// Token: 0x040073AE RID: 29614
	public Animator _runningAnimator;

	// Token: 0x040073AF RID: 29615
	public bool MoveToAnimParent;

	// Token: 0x040073B0 RID: 29616
	public AnimationClip SourceAnimationClip;

	// Token: 0x040073B1 RID: 29617
	public GameObject NodeAvatarParent;

	// Token: 0x040073B2 RID: 29618
	public GameObject NodeAvatar;

	// Token: 0x040073B3 RID: 29619
	private float _animTime;

	// Token: 0x040073B4 RID: 29620
	private Vector3[] _nodeArray;

	// Token: 0x040073B5 RID: 29621
	public bool ReverseNodeArray;

	// Token: 0x040073B6 RID: 29622
	public bool OutputWorldSpace = true;

	// Token: 0x040073B7 RID: 29623
	private float _cutoffTime;
}
