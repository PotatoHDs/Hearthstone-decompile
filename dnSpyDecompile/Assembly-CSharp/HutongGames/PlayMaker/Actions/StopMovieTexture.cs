using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E13 RID: 3603
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Stops playing the Movie Texture, and rewinds it to the beginning.")]
	public class StopMovieTexture : FsmStateAction
	{
		// Token: 0x0600A71A RID: 42778 RVA: 0x0034B6AC File Offset: 0x003498AC
		public override void Reset()
		{
			this.movieTexture = null;
		}

		// Token: 0x0600A71B RID: 42779 RVA: 0x0034B6B8 File Offset: 0x003498B8
		public override void OnEnter()
		{
			MovieTexture movieTexture = this.movieTexture.Value as MovieTexture;
			if (movieTexture != null)
			{
				movieTexture.Stop();
			}
			base.Finish();
		}

		// Token: 0x04008D98 RID: 36248
		[RequiredField]
		[ObjectType(typeof(MovieTexture))]
		public FsmObject movieTexture;
	}
}
