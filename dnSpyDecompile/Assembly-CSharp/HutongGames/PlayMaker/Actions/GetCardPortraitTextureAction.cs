using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F37 RID: 3895
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting variables that reference the parent actor and its contents.")]
	public class GetCardPortraitTextureAction : FsmStateAction
	{
		// Token: 0x0600AC6A RID: 44138 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AC6B RID: 44139 RVA: 0x0035CAF8 File Offset: 0x0035ACF8
		public override void OnEnter()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				Debug.LogWarningFormat("Failed to find Actor in GetCardPortraitTextureAction: {0}", new object[]
				{
					SceneUtils.FindComponentInThisOrParents<Card>(base.Owner)
				});
				base.Finish();
				return;
			}
			if (!actor.HasCardDef)
			{
				Debug.LogWarningFormat("Failed to get CardDef in GetCardPortraitTextureAction: {0}", new object[]
				{
					SceneUtils.FindComponentInThisOrParents<Card>(base.Owner)
				});
				base.Finish();
				return;
			}
			Texture portraitTexture = actor.PortraitTexture;
			if (portraitTexture == null)
			{
				Debug.LogWarningFormat("Failed to get GetPortraitTexture in GetCardPortraitTextureAction: {0}", new object[]
				{
					SceneUtils.FindComponentInThisOrParents<Card>(base.Owner)
				});
				base.Finish();
				return;
			}
			this.storedTexture.Value = portraitTexture;
			base.Finish();
		}

		// Token: 0x04009344 RID: 37700
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Title("StoreTexture")]
		[Tooltip("Store the portrait texture in a variable.")]
		public FsmTexture storedTexture;
	}
}
