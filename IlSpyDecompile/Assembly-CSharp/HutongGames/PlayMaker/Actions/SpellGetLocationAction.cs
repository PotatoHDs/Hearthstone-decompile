using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Get position from the bones in the board")]
	public class SpellGetLocationAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		[Tooltip("Choose a location from the spell to get the position from")]
		public SpellLocation m_Location;

		[Tooltip("Choose a bone, usually used with board location")]
		public FsmString m_Bone;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the position")]
		public FsmVector3 m_StorePosition;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(m_spell, m_Location, m_Bone.Value);
				if (spellLocationObject != null)
				{
					m_StorePosition.Value = spellLocationObject.transform.position;
				}
				Finish();
			}
		}
	}
}
