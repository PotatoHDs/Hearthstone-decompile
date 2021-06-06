namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Start playing the specified music playlist.")]
	public class AudioStartPlaylistAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The playlist you want to start playing.")]
		public MusicPlaylistType m_Playlist;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			MusicManager.Get().StartPlaylist(m_Playlist);
			Finish();
		}
	}
}
