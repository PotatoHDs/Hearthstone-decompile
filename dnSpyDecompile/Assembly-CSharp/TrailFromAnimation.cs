using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class TrailFromAnimation : MonoBehaviour
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnEnable()
	{
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0004BFCC File Offset: 0x0004A1CC
	private void Update()
	{
		this._animTime += Time.deltaTime;
		this._nodeArray = this.RefreshNodeArray(this.MaxNumDivisions, this.SourceAnimationClip, this.NodeAvatarParent, this.NodeAvatar, this._animTime, this.TrailLengthTime);
		this.RestructureLines(this.TargetLineRenderers, this._nodeArray);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0004C030 File Offset: 0x0004A230
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

	// Token: 0x06000D2C RID: 3372 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
	private void RestructureLines(LineRenderer[] lineRenderers, Vector3[] nodeArray)
	{
		for (int i = 0; i < lineRenderers.Length; i++)
		{
			lineRenderers[i].SetPositions(nodeArray);
			lineRenderers[i].positionCount = nodeArray.Length;
		}
	}

	// Token: 0x04000903 RID: 2307
	[Tooltip("Can output node pathing to multiple line renderers")]
	public LineRenderer[] TargetLineRenderers;

	// Token: 0x04000904 RID: 2308
	[Tooltip("The maximum number of divisions at full length. Number of divisions scales as trail gets shorter.")]
	public int MaxNumDivisions = 50;

	// Token: 0x04000905 RID: 2309
	[Tooltip("Maximum length of trail in terms of time. Trail automatically shortens at beginning and end of lifetime.")]
	public float TrailLengthTime = 0.5f;

	// Token: 0x04000906 RID: 2310
	[Tooltip("Scales length of trail over lifetime.")]
	public AnimationCurve LengthScaleOverTime;

	// Token: 0x04000907 RID: 2311
	[Tooltip("The animation clip to use for trail pathing.")]
	public AnimationClip SourceAnimationClip;

	// Token: 0x04000908 RID: 2312
	[Tooltip("The dummy gameobject tree to use for animation simulations. Need to match heirarchy of original animated object. Top level of heirarchy should correlate to the parent object that original animator & animation clip are attached to.")]
	public GameObject NodeAvatarParent;

	// Token: 0x04000909 RID: 2313
	[Tooltip("The specific gameobject in the heirarchy that the trail pathing should follow")]
	public GameObject NodeAvatar;

	// Token: 0x0400090A RID: 2314
	private float _animTime;

	// Token: 0x0400090B RID: 2315
	private Vector3[] _nodeArray;

	// Token: 0x0400090C RID: 2316
	[Tooltip("Output pathing nodes in reverse")]
	public bool ReverseNodeArray;

	// Token: 0x0400090D RID: 2317
	[Tooltip("Output pathing nodes to world space instead of local space")]
	public bool OutputWorldSpace;
}
