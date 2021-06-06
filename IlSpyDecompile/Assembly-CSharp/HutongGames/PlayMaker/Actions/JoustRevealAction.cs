using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Animate a card object out of the deck like joust")]
	public class JoustRevealAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Time it takes to reveal animation to finish")]
		public float m_AnimateTime = 1.2f;

		[RequiredField]
		[Tooltip("Dummy card object to move")]
		public FsmGameObject m_DummyCardObject;

		[RequiredField]
		[Tooltip("Destination location")]
		public FsmVector3 m_DestinationLocation;

		[RequiredField]
		[Tooltip("In-deck position offset")]
		public FsmVector3 m_DeckLocationOffset = new Vector3(0.15f, 0f, 0f);

		[RequiredField]
		[Tooltip("In-deck rotation offset")]
		public FsmVector3 m_DeckRotationOffset = new Vector3(0f, 0f, 180f);

		[Tooltip("Initial delay")]
		public float m_Delay;

		public override void OnEnter()
		{
			GameObject value = m_DummyCardObject.Value;
			if (value == null)
			{
				Finish();
				return;
			}
			Vector3 vector = new Vector3(4.488504f, -6.613904f, 0.9635792f);
			Player controller = SceneUtils.FindComponentInThisOrParents<Spell>(base.Owner).GetSourceCard().GetController();
			if (controller == null)
			{
				Finish();
				return;
			}
			Actor thicknessForLayout = controller.GetDeckZone().GetThicknessForLayout();
			if (thicknessForLayout == null)
			{
				Finish();
				return;
			}
			Renderer meshRenderer = thicknessForLayout.GetMeshRenderer();
			if (meshRenderer == null)
			{
				Finish();
				return;
			}
			vector = meshRenderer.bounds.center + Card.IN_DECK_OFFSET + m_DeckLocationOffset.Value - value.transform.position;
			float num = 0.5f * m_AnimateTime;
			Vector3 vector2 = value.transform.position + vector;
			Vector3 vector3 = vector2 + Card.ABOVE_DECK_OFFSET;
			Vector3 value2 = m_DestinationLocation.Value;
			Vector3 eulerAngles = value.transform.rotation.eulerAngles;
			Vector3 localScale = value.transform.localScale;
			Vector3[] array = new Vector3[3] { vector2, vector3, value2 };
			value.transform.position = vector2;
			value.transform.rotation = Card.IN_DECK_HIDDEN_ROTATION * Quaternion.Euler(m_DeckRotationOffset.Value);
			value.transform.localScale = Card.IN_DECK_SCALE;
			iTween.MoveTo(value, iTween.Hash("path", array, "delay", m_Delay, "time", m_AnimateTime, "easetype", iTween.EaseType.easeInOutQuart));
			iTween.RotateTo(value, iTween.Hash("rotation", eulerAngles, "delay", m_Delay + num, "time", num, "easetype", iTween.EaseType.easeInOutCubic));
			iTween.ScaleTo(value, iTween.Hash("scale", localScale, "delay", m_Delay + num, "time", num, "easetype", iTween.EaseType.easeInOutQuint));
			Finish();
		}
	}
}
