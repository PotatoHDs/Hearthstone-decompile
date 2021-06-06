using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F51 RID: 3921
	[ActionCategory("Pegasus")]
	[Tooltip("Animate a card object out of the deck like joust")]
	public class JoustRevealAction : FsmStateAction
	{
		// Token: 0x0600ACCF RID: 44239 RVA: 0x0035E94C File Offset: 0x0035CB4C
		public override void OnEnter()
		{
			GameObject value = this.m_DummyCardObject.Value;
			if (value == null)
			{
				base.Finish();
				return;
			}
			Vector3 b = new Vector3(4.488504f, -6.613904f, 0.9635792f);
			Player controller = SceneUtils.FindComponentInThisOrParents<Spell>(base.Owner).GetSourceCard().GetController();
			if (controller == null)
			{
				base.Finish();
				return;
			}
			Actor thicknessForLayout = controller.GetDeckZone().GetThicknessForLayout();
			if (thicknessForLayout == null)
			{
				base.Finish();
				return;
			}
			Renderer meshRenderer = thicknessForLayout.GetMeshRenderer(false);
			if (meshRenderer == null)
			{
				base.Finish();
				return;
			}
			b = meshRenderer.bounds.center + Card.IN_DECK_OFFSET + this.m_DeckLocationOffset.Value - value.transform.position;
			float num = 0.5f * this.m_AnimateTime;
			Vector3 vector = value.transform.position + b;
			Vector3 vector2 = vector + Card.ABOVE_DECK_OFFSET;
			Vector3 value2 = this.m_DestinationLocation.Value;
			Vector3 eulerAngles = value.transform.rotation.eulerAngles;
			Vector3 localScale = value.transform.localScale;
			Vector3[] array = new Vector3[]
			{
				vector,
				vector2,
				value2
			};
			value.transform.position = vector;
			value.transform.rotation = Card.IN_DECK_HIDDEN_ROTATION * Quaternion.Euler(this.m_DeckRotationOffset.Value);
			value.transform.localScale = Card.IN_DECK_SCALE;
			iTween.MoveTo(value, iTween.Hash(new object[]
			{
				"path",
				array,
				"delay",
				this.m_Delay,
				"time",
				this.m_AnimateTime,
				"easetype",
				iTween.EaseType.easeInOutQuart
			}));
			iTween.RotateTo(value, iTween.Hash(new object[]
			{
				"rotation",
				eulerAngles,
				"delay",
				this.m_Delay + num,
				"time",
				num,
				"easetype",
				iTween.EaseType.easeInOutCubic
			}));
			iTween.ScaleTo(value, iTween.Hash(new object[]
			{
				"scale",
				localScale,
				"delay",
				this.m_Delay + num,
				"time",
				num,
				"easetype",
				iTween.EaseType.easeInOutQuint
			}));
			base.Finish();
		}

		// Token: 0x040093A0 RID: 37792
		[RequiredField]
		[Tooltip("Time it takes to reveal animation to finish")]
		public float m_AnimateTime = 1.2f;

		// Token: 0x040093A1 RID: 37793
		[RequiredField]
		[Tooltip("Dummy card object to move")]
		public FsmGameObject m_DummyCardObject;

		// Token: 0x040093A2 RID: 37794
		[RequiredField]
		[Tooltip("Destination location")]
		public FsmVector3 m_DestinationLocation;

		// Token: 0x040093A3 RID: 37795
		[RequiredField]
		[Tooltip("In-deck position offset")]
		public FsmVector3 m_DeckLocationOffset = new Vector3(0.15f, 0f, 0f);

		// Token: 0x040093A4 RID: 37796
		[RequiredField]
		[Tooltip("In-deck rotation offset")]
		public FsmVector3 m_DeckRotationOffset = new Vector3(0f, 0f, 180f);

		// Token: 0x040093A5 RID: 37797
		[Tooltip("Initial delay")]
		public float m_Delay;
	}
}
