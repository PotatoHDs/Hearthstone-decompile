using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Move a GameObject to another GameObject with an Animation Curve. Works like iTween Move To, but with better performance.")]
	public class MoveObjectWithAnimationCurve : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault objectToMove;

		[Tooltip("Object to move to.")]
		public FsmGameObject destinationObject;

		[Tooltip("Keep track of destination object location every frame, otherwise the location will only be looked up once at the beginning of the action.")]
		public FsmBool trackDestination = false;

		[Tooltip("Move to a specific position vector. If Destination Object is defined, this is used as an offset.")]
		public FsmVector3 destinationLocation;

		[Tooltip("Use worldspace instead of local.")]
		public FsmBool worldSpace = true;

		[Tooltip("Animation curve to use as easing for movement.")]
		[RequiredField]
		public FsmAnimationCurve animationCurve;

		[Tooltip("Time scale of animation curve.")]
		public FsmFloat animCurveTimeScale = 1f;

		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		private float elapsedTime;

		private float currentTime;

		private float maxTime;

		private Vector3 destinationPosition;

		private Vector3 sourcePosition;

		private GameObject gameObjectToMove;

		public override void Reset()
		{
			elapsedTime = 0f;
			worldSpace = true;
			destinationObject = new FsmGameObject
			{
				UseVariable = true
			};
			destinationLocation = new FsmVector3
			{
				UseVariable = true
			};
			animCurveTimeScale = 1f;
		}

		public override void OnEnter()
		{
			if (animationCurve == null)
			{
				Finish();
				return;
			}
			if (animationCurve.curve.length == 0)
			{
				Finish();
				return;
			}
			if (animCurveTimeScale.Value <= 0f)
			{
				Finish();
				return;
			}
			if (!destinationObject.Value)
			{
				destinationObject = new FsmGameObject
				{
					UseVariable = true
				};
			}
			elapsedTime = 0f;
			gameObjectToMove = base.Fsm.GetOwnerDefaultTarget(objectToMove);
			GetDestinationPosition();
			GetSourcePosition();
			GetMaxTime();
		}

		public override void OnUpdate()
		{
			elapsedTime += Time.deltaTime;
			currentTime = elapsedTime * animCurveTimeScale.Value;
			if (trackDestination.Value)
			{
				GetDestinationPosition();
			}
			UpdatePosition();
			if (currentTime >= maxTime)
			{
				DoEvent();
				Finish();
			}
		}

		private void UpdatePosition()
		{
			gameObjectToMove = base.Fsm.GetOwnerDefaultTarget(objectToMove);
			if (!gameObjectToMove)
			{
				Finish();
				return;
			}
			Vector3 vector = sourcePosition + (destinationPosition - sourcePosition) * animationCurve.curve.Evaluate(currentTime);
			if (worldSpace.Value)
			{
				gameObjectToMove.transform.position = vector;
			}
			else
			{
				gameObjectToMove.transform.localPosition = vector;
			}
		}

		private void DoEvent()
		{
			if (finishEvent != null)
			{
				base.Fsm.Event(finishEvent);
			}
		}

		private void GetDestinationPosition()
		{
			Vector3 vector = (destinationLocation.IsNone ? Vector3.zero : destinationLocation.Value);
			destinationPosition = Vector3.zero;
			if (!destinationObject.IsNone)
			{
				destinationPosition = (worldSpace.Value ? destinationObject.Value.transform.position : destinationObject.Value.transform.localPosition);
			}
			destinationPosition += vector;
		}

		private void GetSourcePosition()
		{
			sourcePosition = Vector3.zero;
			if ((bool)gameObjectToMove)
			{
				sourcePosition = (worldSpace.Value ? gameObjectToMove.transform.position : gameObjectToMove.transform.localPosition);
			}
		}

		private void GetMaxTime()
		{
			AnimationCurve curve = animationCurve.curve;
			maxTime = curve[curve.length - 1].time;
		}
	}
}
