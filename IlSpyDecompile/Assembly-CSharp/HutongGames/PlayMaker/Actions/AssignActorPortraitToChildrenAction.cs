using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Assign an actor's portrait mesh to children of a specified actor (used for Hero Portrait effects)")]
	public class AssignActorPortraitToChildrenAction : FsmStateAction
	{
		[Tooltip("Provide an actor to apply the portrait to. If no actor is provided for m_ActorToAssignPortraitFrom, this actor's parent will be used")]
		public FsmGameObject m_ActorToAssignPortraitTo;

		[Tooltip("Provide an actor to specifically use for the portrait, otherwise we'll use a parent of m_ActorToAssignPortraitTo")]
		public FsmGameObject m_ActorToAssignPortraitFrom;

		public override void Reset()
		{
			m_ActorToAssignPortraitFrom = null;
			m_ActorToAssignPortraitTo = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_ActorToAssignPortraitTo == null)
			{
				Finish();
				return;
			}
			Actor actor = null;
			actor = ((!m_ActorToAssignPortraitFrom.Value) ? SceneUtils.FindComponentInThisOrParents<Actor>(m_ActorToAssignPortraitTo.Value) : SceneUtils.FindComponentInThisOrParents<Actor>(m_ActorToAssignPortraitFrom.Value));
			if (actor == null || actor.m_portraitMesh == null)
			{
				Finish();
				return;
			}
			List<Material> materials = actor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			if (materials.Count == 0 || actor.m_portraitMatIdx < 0)
			{
				Finish();
				return;
			}
			Texture mainTexture = materials[actor.m_portraitMatIdx].mainTexture;
			Renderer[] componentsInChildren = m_ActorToAssignPortraitTo.Value.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				foreach (Material material in componentsInChildren[i].GetMaterials())
				{
					if (material.name.Contains("portrait"))
					{
						material.mainTexture = mainTexture;
					}
				}
			}
			Finish();
		}
	}
}
