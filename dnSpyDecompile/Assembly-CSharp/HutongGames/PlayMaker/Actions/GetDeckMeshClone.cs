using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F39 RID: 3897
	[ActionCategory("Pegasus")]
	[Tooltip("Takes in a player and returns a clone of their deck mesh gameObject.")]
	public class GetDeckMeshClone : FsmStateAction
	{
		// Token: 0x0600AC70 RID: 44144 RVA: 0x0035CD0E File Offset: 0x0035AF0E
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.NEUTRAL;
			this.m_DeckClone = null;
		}

		// Token: 0x0600AC71 RID: 44145 RVA: 0x0035CD20 File Offset: 0x0035AF20
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_DeckClone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store deck object!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (GameState.Get() == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - GameState is null!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (this.m_PlayerSide == Player.Side.NEUTRAL)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No deck exists for player side {1}!", new object[]
				{
					this,
					this.m_PlayerSide
				});
				base.Finish();
				return;
			}
			Player player = (this.m_PlayerSide == Player.Side.FRIENDLY) ? GameState.Get().GetFriendlySidePlayer() : GameState.Get().GetOpposingSidePlayer();
			if (player == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find player for side {1}!", new object[]
				{
					this,
					this.m_PlayerSide
				});
				base.Finish();
				return;
			}
			ZoneDeck deckZone = player.GetDeckZone();
			if (deckZone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", new object[]
				{
					this,
					player
				});
				base.Finish();
				return;
			}
			Actor activeThickness = deckZone.GetActiveThickness();
			GameObject gameObject = new GameObject();
			gameObject.transform.position = activeThickness.transform.position;
			gameObject.transform.rotation = activeThickness.transform.rotation;
			gameObject.AddComponent<MeshFilter>();
			gameObject.GetComponent<MeshFilter>().sharedMesh = UnityEngine.Object.Instantiate<Mesh>(activeThickness.GetComponentInChildren<MeshFilter>().sharedMesh);
			gameObject.AddComponent<MeshRenderer>();
			gameObject.GetComponent<Renderer>().SetMaterial(UnityEngine.Object.Instantiate<Material>(activeThickness.GetMeshRenderer(false).GetMaterial()));
			this.m_DeckClone.Value = gameObject;
			if (this.m_DisableRenderer)
			{
				activeThickness.GetMeshRenderer(false).enabled = false;
			}
			base.Finish();
		}

		// Token: 0x04009347 RID: 37703
		[RequiredField]
		[Tooltip("Which player's deck are we querying the size of?")]
		public Player.Side m_PlayerSide;

		// Token: 0x04009348 RID: 37704
		[Tooltip("Disable the renderer of the object you're cloning? (Please remember to re-enable it later!)")]
		public bool m_DisableRenderer;

		// Token: 0x04009349 RID: 37705
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject.")]
		public FsmGameObject m_DeckClone;
	}
}
