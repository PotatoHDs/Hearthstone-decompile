using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Takes in a player and returns a clone of their deck mesh gameObject.")]
	public class GetDeckMeshClone : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Which player's deck are we querying the size of?")]
		public Player.Side m_PlayerSide;

		[Tooltip("Disable the renderer of the object you're cloning? (Please remember to re-enable it later!)")]
		public bool m_DisableRenderer;

		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject.")]
		public FsmGameObject m_DeckClone;

		public override void Reset()
		{
			m_PlayerSide = Player.Side.NEUTRAL;
			m_DeckClone = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_DeckClone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store deck object!", this);
				Finish();
				return;
			}
			if (GameState.Get() == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - GameState is null!", this);
				Finish();
				return;
			}
			if (m_PlayerSide == Player.Side.NEUTRAL)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No deck exists for player side {1}!", this, m_PlayerSide);
				Finish();
				return;
			}
			Player player = ((m_PlayerSide == Player.Side.FRIENDLY) ? GameState.Get().GetFriendlySidePlayer() : GameState.Get().GetOpposingSidePlayer());
			if (player == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find player for side {1}!", this, m_PlayerSide);
				Finish();
				return;
			}
			ZoneDeck deckZone = player.GetDeckZone();
			if (deckZone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", this, player);
				Finish();
				return;
			}
			Actor activeThickness = deckZone.GetActiveThickness();
			GameObject gameObject = new GameObject();
			gameObject.transform.position = activeThickness.transform.position;
			gameObject.transform.rotation = activeThickness.transform.rotation;
			gameObject.AddComponent<MeshFilter>();
			gameObject.GetComponent<MeshFilter>().sharedMesh = Object.Instantiate(activeThickness.GetComponentInChildren<MeshFilter>().sharedMesh);
			gameObject.AddComponent<MeshRenderer>();
			gameObject.GetComponent<Renderer>().SetMaterial(Object.Instantiate(activeThickness.GetMeshRenderer().GetMaterial()));
			m_DeckClone.Value = gameObject;
			if (m_DisableRenderer)
			{
				activeThickness.GetMeshRenderer().enabled = false;
			}
			Finish();
		}
	}
}
