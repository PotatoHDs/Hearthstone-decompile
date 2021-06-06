using UnityEngine;

public class FunctionLib : MonoBehaviour
{
	public LightningCtrl lightningScript;

	public GameObject target;

	public GameObject destination;

	private void onAnimaitonEvent()
	{
		lightningScript.Spawn(target.transform, destination.transform);
	}
}
