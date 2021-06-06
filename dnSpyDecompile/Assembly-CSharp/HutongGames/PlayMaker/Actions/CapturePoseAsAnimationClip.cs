using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BEA RID: 3050
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Captures the current pose of a hierarchy as an animation clip.\n\nUseful to blend from an arbitrary pose (e.g. a rag-doll death) back to a known animation (e.g. idle).")]
	public class CapturePoseAsAnimationClip : FsmStateAction
	{
		// Token: 0x06009D1B RID: 40219 RVA: 0x00327D00 File Offset: 0x00325F00
		public override void Reset()
		{
			this.gameObject = null;
			this.position = false;
			this.rotation = true;
			this.scale = false;
			this.storeAnimationClip = null;
		}

		// Token: 0x06009D1C RID: 40220 RVA: 0x00327D34 File Offset: 0x00325F34
		public override void OnEnter()
		{
			this.DoCaptureAnimationClip();
			base.Finish();
		}

		// Token: 0x06009D1D RID: 40221 RVA: 0x00327D44 File Offset: 0x00325F44
		private void DoCaptureAnimationClip()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			AnimationClip animationClip = new AnimationClip();
			foreach (object obj in ownerDefaultTarget.transform)
			{
				Transform transform = (Transform)obj;
				this.CaptureTransform(transform, "", animationClip);
			}
			this.storeAnimationClip.Value = animationClip;
		}

		// Token: 0x06009D1E RID: 40222 RVA: 0x00327DD4 File Offset: 0x00325FD4
		private void CaptureTransform(Transform transform, string path, AnimationClip clip)
		{
			path += transform.name;
			if (this.position.Value)
			{
				this.CapturePosition(transform, path, clip);
			}
			if (this.rotation.Value)
			{
				this.CaptureRotation(transform, path, clip);
			}
			if (this.scale.Value)
			{
				this.CaptureScale(transform, path, clip);
			}
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				this.CaptureTransform(transform2, path + "/", clip);
			}
		}

		// Token: 0x06009D1F RID: 40223 RVA: 0x00327E84 File Offset: 0x00326084
		private void CapturePosition(Transform transform, string path, AnimationClip clip)
		{
			this.SetConstantCurve(clip, path, "localPosition.x", transform.localPosition.x);
			this.SetConstantCurve(clip, path, "localPosition.y", transform.localPosition.y);
			this.SetConstantCurve(clip, path, "localPosition.z", transform.localPosition.z);
		}

		// Token: 0x06009D20 RID: 40224 RVA: 0x00327EDC File Offset: 0x003260DC
		private void CaptureRotation(Transform transform, string path, AnimationClip clip)
		{
			this.SetConstantCurve(clip, path, "localRotation.x", transform.localRotation.x);
			this.SetConstantCurve(clip, path, "localRotation.y", transform.localRotation.y);
			this.SetConstantCurve(clip, path, "localRotation.z", transform.localRotation.z);
			this.SetConstantCurve(clip, path, "localRotation.w", transform.localRotation.w);
		}

		// Token: 0x06009D21 RID: 40225 RVA: 0x00327F4C File Offset: 0x0032614C
		private void CaptureScale(Transform transform, string path, AnimationClip clip)
		{
			this.SetConstantCurve(clip, path, "localScale.x", transform.localScale.x);
			this.SetConstantCurve(clip, path, "localScale.y", transform.localScale.y);
			this.SetConstantCurve(clip, path, "localScale.z", transform.localScale.z);
		}

		// Token: 0x06009D22 RID: 40226 RVA: 0x00327FA4 File Offset: 0x003261A4
		private void SetConstantCurve(AnimationClip clip, string childPath, string propertyPath, float value)
		{
			AnimationCurve animationCurve = AnimationCurve.Linear(0f, value, 100f, value);
			animationCurve.postWrapMode = WrapMode.Loop;
			clip.SetCurve(childPath, typeof(Transform), propertyPath, animationCurve);
		}

		// Token: 0x040082A0 RID: 33440
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject root of the hierarchy to capture.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082A1 RID: 33441
		[Tooltip("Capture position keys.")]
		public FsmBool position;

		// Token: 0x040082A2 RID: 33442
		[Tooltip("Capture rotation keys.")]
		public FsmBool rotation;

		// Token: 0x040082A3 RID: 33443
		[Tooltip("Capture scale keys.")]
		public FsmBool scale;

		// Token: 0x040082A4 RID: 33444
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AnimationClip))]
		[Tooltip("Store the result in an Object variable of type AnimationClip.")]
		public FsmObject storeAnimationClip;
	}
}
