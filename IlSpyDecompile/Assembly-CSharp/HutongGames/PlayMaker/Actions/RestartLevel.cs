using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Restarts current level.")]
	public class RestartLevel : FsmStateAction
	{
		public override void OnEnter()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
			Finish();
		}
	}
}
