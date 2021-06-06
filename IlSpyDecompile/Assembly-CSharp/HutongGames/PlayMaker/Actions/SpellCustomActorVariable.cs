using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting a variable that references one of the Actor's game objects by name.")]
	public class SpellCustomActorVariable : FsmStateAction
	{
		public FsmString m_objectName;

		public FsmGameObject m_actorObject;

		public override void Reset()
		{
			m_objectName = "";
			m_actorObject = null;
		}

		public override void OnEnter()
		{
			if (!m_actorObject.IsNone)
			{
				Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
				if (actor != null)
				{
					GameObject gameObject = SceneUtils.FindChildBySubstring(actor.gameObject, m_objectName.Value);
					if (gameObject == null)
					{
						Debug.LogWarning(string.Concat("Could not find object of name ", m_objectName, " in actor"));
					}
					else
					{
						m_actorObject.Value = gameObject;
					}
				}
			}
			Finish();
		}

		public override void OnUpdate()
		{
		}
	}
}
