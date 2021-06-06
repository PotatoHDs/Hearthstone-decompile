using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF6 RID: 3318
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Sets the Game Object as the Audio Source associated with the Movie Texture. The Game Object must have an AudioSource Component.")]
	public class MovieTextureAudioSettings : FsmStateAction
	{
		// Token: 0x0600A1C5 RID: 41413 RVA: 0x003392F5 File Offset: 0x003374F5
		public override void Reset()
		{
			this.movieTexture = null;
			this.gameObject = null;
		}

		// Token: 0x0600A1C6 RID: 41414 RVA: 0x00339308 File Offset: 0x00337508
		public override void OnEnter()
		{
			MovieTexture movieTexture = this.movieTexture.Value as MovieTexture;
			if (movieTexture != null && this.gameObject.Value != null)
			{
				AudioSource component = this.gameObject.Value.GetComponent<AudioSource>();
				if (component != null)
				{
					component.clip = movieTexture.audioClip;
				}
			}
			base.Finish();
		}

		// Token: 0x040087DF RID: 34783
		[RequiredField]
		[ObjectType(typeof(MovieTexture))]
		public FsmObject movieTexture;

		// Token: 0x040087E0 RID: 34784
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject gameObject;
	}
}
