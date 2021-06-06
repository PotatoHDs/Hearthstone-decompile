using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.FX
{
	// Token: 0x02001172 RID: 4466
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Collider))]
	public class YoggWheelPointerController : MonoBehaviour
	{
		// Token: 0x0600C406 RID: 50182 RVA: 0x003B2843 File Offset: 0x003B0A43
		private void Start()
		{
			base.GetComponent<Animator>().SetBool(this.m_loopingBool, false);
			base.GetComponent<Animator>().ResetTrigger(this.m_oneShotTrigger);
			this.m_lookingForOneShots = false;
			base.GetComponent<Collider>().isTrigger = true;
		}

		// Token: 0x0600C407 RID: 50183 RVA: 0x003B287B File Offset: 0x003B0A7B
		private void OnTriggerEnter(Collider other)
		{
			if (this.m_lookingForOneShots && this.m_wheelSegments.Contains(other))
			{
				base.GetComponent<Animator>().SetTrigger(this.m_oneShotTrigger);
			}
		}

		// Token: 0x0600C408 RID: 50184 RVA: 0x003B28A4 File Offset: 0x003B0AA4
		public void StartLoopingAnimation()
		{
			base.GetComponent<Animator>().SetBool(this.m_loopingBool, true);
			this.m_lookingForOneShots = false;
		}

		// Token: 0x0600C409 RID: 50185 RVA: 0x003B28BF File Offset: 0x003B0ABF
		public void StartLookingForOneShots()
		{
			base.GetComponent<Animator>().SetBool(this.m_loopingBool, false);
			this.m_lookingForOneShots = true;
		}

		// Token: 0x0600C40A RID: 50186 RVA: 0x003B28DA File Offset: 0x003B0ADA
		public void StopPointer()
		{
			base.GetComponent<Animator>().SetBool(this.m_loopingBool, false);
			this.m_lookingForOneShots = false;
		}

		// Token: 0x04009D05 RID: 40197
		[Header("Animator Parameters")]
		[SerializeField]
		[Tooltip("Expecting a BOOL parameter that triggers a looping animation.")]
		private string m_loopingBool = "Loop";

		// Token: 0x04009D06 RID: 40198
		[SerializeField]
		[Tooltip("Expecting a TRIGGER parameter that triggers a one-shot animation.")]
		private string m_oneShotTrigger = "Tick";

		// Token: 0x04009D07 RID: 40199
		[Header("Colliders")]
		[SerializeField]
		[Tooltip("Colliders spaced around the wheel's perimeter.  These trigger the pointer's movement.")]
		private List<Collider> m_wheelSegments = new List<Collider>();

		// Token: 0x04009D08 RID: 40200
		private bool m_lookingForOneShots;
	}
}
