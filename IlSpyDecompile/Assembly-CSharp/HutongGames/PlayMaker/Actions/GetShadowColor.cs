using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Get shadow color")]
	public class GetShadowColor : FsmStateAction
	{
		public FsmColor m_Color;

		public override void Reset()
		{
			m_Color = Color.black;
		}

		public override void OnEnter()
		{
			m_Color.Value = Board.Get().m_ShadowColor;
			Finish();
		}

		public override void OnUpdate()
		{
			m_Color.Value = Board.Get().m_ShadowColor;
		}
	}
}
