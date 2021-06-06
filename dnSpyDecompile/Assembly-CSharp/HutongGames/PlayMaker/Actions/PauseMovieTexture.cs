using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF9 RID: 3321
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Pauses a Movie Texture.")]
	public class PauseMovieTexture : FsmStateAction
	{
		// Token: 0x0600A1D1 RID: 41425 RVA: 0x0033942D File Offset: 0x0033762D
		public override void Reset()
		{
			this.movieTexture = null;
		}

		// Token: 0x0600A1D2 RID: 41426 RVA: 0x00339438 File Offset: 0x00337638
		public override void OnEnter()
		{
			MovieTexture movieTexture = this.movieTexture.Value as MovieTexture;
			if (movieTexture != null)
			{
				movieTexture.Pause();
			}
			base.Finish();
		}

		// Token: 0x040087E8 RID: 34792
		[RequiredField]
		[ObjectType(typeof(MovieTexture))]
		public FsmObject movieTexture;
	}
}
