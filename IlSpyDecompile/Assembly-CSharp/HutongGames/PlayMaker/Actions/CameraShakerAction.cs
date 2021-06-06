using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Shakes a camera over time.")]
	public class CameraShakerAction : CameraAction
	{
		public WhichCamera m_WhichCamera;

		[CheckForComponent(typeof(Camera))]
		public FsmGameObject m_SpecificCamera;

		public FsmString m_NamedCamera;

		public FsmVector3 m_Amount;

		[RequiredField]
		public FsmAnimationCurve m_IntensityCurve;

		public FsmFloat m_Delay;

		[Tooltip("[Optional] Hold the shake forever once the shake passes this time.")]
		public FsmFloat m_HoldAtTime;

		public FsmEvent m_FinishedEvent;

		private float m_timerSec;

		private bool m_shakeFired;

		public override void Reset()
		{
			m_WhichCamera = WhichCamera.MAIN;
			m_SpecificCamera = new FsmGameObject
			{
				UseVariable = true
			};
			m_NamedCamera = new FsmString
			{
				UseVariable = false
			};
			m_Amount = new FsmVector3
			{
				UseVariable = false
			};
			m_IntensityCurve = null;
			m_Delay = 0f;
			m_HoldAtTime = new FsmFloat
			{
				UseVariable = true
			};
			m_FinishedEvent = null;
		}

		public override void OnEnter()
		{
			m_timerSec = 0f;
			m_shakeFired = false;
		}

		public override void OnUpdate()
		{
			Camera camera = GetCamera(m_WhichCamera, m_SpecificCamera, m_NamedCamera);
			if (!camera)
			{
				Error.AddDevFatal("CameraShakerAction.OnUpdate() - Failed to get a camera. Owner={0}", base.Owner);
				Finish();
			}
			m_timerSec += Time.deltaTime;
			float num = (m_Delay.IsNone ? 0f : m_Delay.Value);
			if (m_timerSec < num)
			{
				return;
			}
			if (!m_shakeFired)
			{
				if (m_IntensityCurve == null || m_IntensityCurve.curve == null)
				{
					base.Fsm.Event(m_FinishedEvent);
					Finish();
					return;
				}
				Shake(camera);
			}
			if (!CameraShakeMgr.IsShaking(camera))
			{
				base.Fsm.Event(m_FinishedEvent);
				Finish();
			}
		}

		private void Shake(Camera camera)
		{
			Vector3 amount = (m_Amount.IsNone ? Vector3.zero : m_Amount.Value);
			AnimationCurve curve = m_IntensityCurve.curve;
			float? holdAtTime = null;
			if (!m_HoldAtTime.IsNone)
			{
				holdAtTime = m_HoldAtTime.Value;
			}
			CameraShakeMgr.Shake(camera, amount, curve, holdAtTime);
			m_shakeFired = true;
		}
	}
}
