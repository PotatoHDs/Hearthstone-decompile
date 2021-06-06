using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.FX
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Collider))]
	public class YoggWheelPointerController : MonoBehaviour
	{
		[Header("Animator Parameters")]
		[SerializeField]
		[Tooltip("Expecting a BOOL parameter that triggers a looping animation.")]
		private string m_loopingBool = "Loop";

		[SerializeField]
		[Tooltip("Expecting a TRIGGER parameter that triggers a one-shot animation.")]
		private string m_oneShotTrigger = "Tick";

		[Header("Colliders")]
		[SerializeField]
		[Tooltip("Colliders spaced around the wheel's perimeter.  These trigger the pointer's movement.")]
		private List<Collider> m_wheelSegments = new List<Collider>();

		private bool m_lookingForOneShots;

		private void Start()
		{
			GetComponent<Animator>().SetBool(m_loopingBool, value: false);
			GetComponent<Animator>().ResetTrigger(m_oneShotTrigger);
			m_lookingForOneShots = false;
			GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (m_lookingForOneShots && m_wheelSegments.Contains(other))
			{
				GetComponent<Animator>().SetTrigger(m_oneShotTrigger);
			}
		}

		public void StartLoopingAnimation()
		{
			GetComponent<Animator>().SetBool(m_loopingBool, value: true);
			m_lookingForOneShots = false;
		}

		public void StartLookingForOneShots()
		{
			GetComponent<Animator>().SetBool(m_loopingBool, value: false);
			m_lookingForOneShots = true;
		}

		public void StopPointer()
		{
			GetComponent<Animator>().SetBool(m_loopingBool, value: false);
			m_lookingForOneShots = false;
		}
	}
}
