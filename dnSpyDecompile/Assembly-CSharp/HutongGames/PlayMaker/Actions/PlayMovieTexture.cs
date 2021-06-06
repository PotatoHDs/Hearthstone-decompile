using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D31 RID: 3377
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Plays a Movie Texture. Use the Movie Texture in a Material, or in the GUI.")]
	public class PlayMovieTexture : FsmStateAction
	{
		// Token: 0x0600A2E8 RID: 41704 RVA: 0x0033DE9B File Offset: 0x0033C09B
		public override void Reset()
		{
			this.movieTexture = null;
			this.loop = false;
		}

		// Token: 0x0600A2E9 RID: 41705 RVA: 0x0033DEB0 File Offset: 0x0033C0B0
		public override void OnEnter()
		{
			MovieTexture movieTexture = this.movieTexture.Value as MovieTexture;
			if (movieTexture != null)
			{
				movieTexture.loop = this.loop.Value;
				movieTexture.Play();
			}
			base.Finish();
		}

		// Token: 0x04008942 RID: 35138
		[RequiredField]
		[ObjectType(typeof(MovieTexture))]
		public FsmObject movieTexture;

		// Token: 0x04008943 RID: 35139
		public FsmBool loop;
	}
}
