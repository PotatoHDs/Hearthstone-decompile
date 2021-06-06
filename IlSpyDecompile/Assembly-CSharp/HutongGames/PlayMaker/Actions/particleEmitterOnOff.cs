using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Turns a Particle Emitter on and off with optional delay.")]
	public class particleEmitterOnOff : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Set to True to turn it on and False to turn it off.")]
		public FsmBool emitOnOff;

		[Tooltip("If 0 it just acts like a switch. Values cause it to Toggle value after delay time (sec).")]
		public FsmFloat delay;

		public FsmEvent finishEvent;

		public bool realTime;

		private float startTime;

		private float timer;

		public override void Reset()
		{
			gameObject = null;
			emitOnOff = false;
			delay = 0f;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			if (delay.Value <= 0f)
			{
				Finish();
				return;
			}
			startTime = Time.realtimeSinceStartup;
			timer = 0f;
		}

		public override void OnUpdate()
		{
			if (realTime)
			{
				timer = Time.realtimeSinceStartup - startTime;
			}
			else
			{
				timer += Time.deltaTime;
			}
			if (timer > delay.Value)
			{
				Finish();
				if (finishEvent != null)
				{
					base.Fsm.Event(finishEvent);
				}
			}
		}
	}
}
