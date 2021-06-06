using UnityEngine;

public class Secret : MonoBehaviour
{
	public UberText secretLabelTop;

	public UberText secretLabelMiddle;

	public UberText secretLabelBottom;

	private void Start()
	{
		secretLabelTop.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
		secretLabelMiddle.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
		secretLabelBottom.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
	}
}
