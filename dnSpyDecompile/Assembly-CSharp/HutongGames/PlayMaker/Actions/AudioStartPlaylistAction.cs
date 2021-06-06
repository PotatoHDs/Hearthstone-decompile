using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F25 RID: 3877
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Start playing the specified music playlist.")]
	public class AudioStartPlaylistAction : FsmStateAction
	{
		// Token: 0x0600AC20 RID: 44064 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AC21 RID: 44065 RVA: 0x0035B53C File Offset: 0x0035973C
		public override void OnEnter()
		{
			MusicManager.Get().StartPlaylist(this.m_Playlist);
			base.Finish();
		}

		// Token: 0x040092E8 RID: 37608
		[RequiredField]
		[Tooltip("The playlist you want to start playing.")]
		public MusicPlaylistType m_Playlist;
	}
}
