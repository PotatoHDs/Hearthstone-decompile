using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F15 RID: 3861
	[ActionCategory("Pegasus")]
	[Tooltip("Assign an actor's portrait mesh to children of a specified actor (used for Hero Portrait effects)")]
	public class AssignActorPortraitToChildrenAction : FsmStateAction
	{
		// Token: 0x0600ABD3 RID: 43987 RVA: 0x0035A1D2 File Offset: 0x003583D2
		public override void Reset()
		{
			this.m_ActorToAssignPortraitFrom = null;
			this.m_ActorToAssignPortraitTo = null;
		}

		// Token: 0x0600ABD4 RID: 43988 RVA: 0x0035A1E4 File Offset: 0x003583E4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_ActorToAssignPortraitTo == null)
			{
				base.Finish();
				return;
			}
			Actor actor;
			if (this.m_ActorToAssignPortraitFrom.Value)
			{
				actor = SceneUtils.FindComponentInThisOrParents<Actor>(this.m_ActorToAssignPortraitFrom.Value);
			}
			else
			{
				actor = SceneUtils.FindComponentInThisOrParents<Actor>(this.m_ActorToAssignPortraitTo.Value);
			}
			if (actor == null || actor.m_portraitMesh == null)
			{
				base.Finish();
				return;
			}
			List<Material> materials = actor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			if (materials.Count == 0 || actor.m_portraitMatIdx < 0)
			{
				base.Finish();
				return;
			}
			Texture mainTexture = materials[actor.m_portraitMatIdx].mainTexture;
			Renderer[] componentsInChildren = this.m_ActorToAssignPortraitTo.Value.GetComponentsInChildren<Renderer>();
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
			base.Finish();
		}

		// Token: 0x0400928B RID: 37515
		[Tooltip("Provide an actor to apply the portrait to. If no actor is provided for m_ActorToAssignPortraitFrom, this actor's parent will be used")]
		public FsmGameObject m_ActorToAssignPortraitTo;

		// Token: 0x0400928C RID: 37516
		[Tooltip("Provide an actor to specifically use for the portrait, otherwise we'll use a parent of m_ActorToAssignPortraitTo")]
		public FsmGameObject m_ActorToAssignPortraitFrom;
	}
}
