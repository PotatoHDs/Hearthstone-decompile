using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The position ( normalized or not) in the parent RectTransform keeping the anchor rect size intact. This lets you position the whole Rect in one go. Use this to easily animate movement (like IOS sliding UIView)")]
	public class RectTransformSetAnchorRectPosition : BaseUpdateAction
	{
		public enum AnchorReference
		{
			TopLeft,
			Top,
			TopRight,
			Right,
			BottomRight,
			Bottom,
			BottomLeft,
			Left,
			Center
		}

		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The reference for the given position")]
		public AnchorReference anchorReference;

		[Tooltip("Are the supplied screen coordinates normalized (0-1), or in pixels.")]
		public FsmBool normalized;

		[Tooltip("The Vector2 position, and/or set individual axis below.")]
		public FsmVector2 anchor;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat x;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat y;

		private RectTransform _rt;

		private Rect _anchorRect;

		public override void Reset()
		{
			base.Reset();
			normalized = true;
			gameObject = null;
			anchorReference = AnchorReference.BottomLeft;
			anchor = null;
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			DoSetAnchor();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoSetAnchor();
		}

		private void DoSetAnchor()
		{
			_anchorRect = default(Rect);
			_anchorRect.min = _rt.anchorMin;
			_anchorRect.max = _rt.anchorMax;
			Vector2 vector = Vector2.zero;
			vector = _anchorRect.min;
			if (!anchor.IsNone)
			{
				if (normalized.Value)
				{
					vector = anchor.Value;
				}
				else
				{
					vector.x = anchor.Value.x / (float)Screen.width;
					vector.y = anchor.Value.y / (float)Screen.height;
				}
			}
			if (!x.IsNone)
			{
				if (normalized.Value)
				{
					vector.x = x.Value;
				}
				else
				{
					vector.x = x.Value / (float)Screen.width;
				}
			}
			if (!y.IsNone)
			{
				if (normalized.Value)
				{
					vector.y = y.Value;
				}
				else
				{
					vector.y = y.Value / (float)Screen.height;
				}
			}
			if (anchorReference == AnchorReference.BottomLeft)
			{
				_anchorRect.x = vector.x;
				_anchorRect.y = vector.y;
			}
			else if (anchorReference == AnchorReference.Left)
			{
				_anchorRect.x = vector.x;
				_anchorRect.y = vector.y - 0.5f;
			}
			else if (anchorReference == AnchorReference.TopLeft)
			{
				_anchorRect.x = vector.x;
				_anchorRect.y = vector.y - 1f;
			}
			else if (anchorReference == AnchorReference.Top)
			{
				_anchorRect.x = vector.x - 0.5f;
				_anchorRect.y = vector.y - 1f;
			}
			else if (anchorReference == AnchorReference.TopRight)
			{
				_anchorRect.x = vector.x - 1f;
				_anchorRect.y = vector.y - 1f;
			}
			else if (anchorReference == AnchorReference.Right)
			{
				_anchorRect.x = vector.x - 1f;
				_anchorRect.y = vector.y - 0.5f;
			}
			else if (anchorReference == AnchorReference.BottomRight)
			{
				_anchorRect.x = vector.x - 1f;
				_anchorRect.y = vector.y;
			}
			else if (anchorReference == AnchorReference.Bottom)
			{
				_anchorRect.x = vector.x - 0.5f;
				_anchorRect.y = vector.y;
			}
			else if (anchorReference == AnchorReference.Center)
			{
				_anchorRect.x = vector.x - 0.5f;
				_anchorRect.y = vector.y - 0.5f;
			}
			_rt.anchorMin = _anchorRect.min;
			_rt.anchorMax = _anchorRect.max;
		}
	}
}
