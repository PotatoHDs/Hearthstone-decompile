namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control TGT Grand Stands")]
	public class TGTGrandStandAction : FsmStateAction
	{
		public enum EMOTE
		{
			Cheer,
			OhNo
		}

		[RequiredField]
		public EMOTE m_emote;

		protected Actor m_actor;

		public void PlayEmote(EMOTE emote)
		{
			TGTGrandStand tGTGrandStand = TGTGrandStand.Get();
			if (!(tGTGrandStand == null))
			{
				switch (emote)
				{
				case EMOTE.Cheer:
					tGTGrandStand.PlayCheerAnimation();
					break;
				case EMOTE.OhNo:
					tGTGrandStand.PlayOhNoAnimation();
					break;
				}
			}
		}

		public override void Reset()
		{
			m_emote = EMOTE.Cheer;
		}

		public override void OnEnter()
		{
			PlayEmote(m_emote);
		}
	}
}
