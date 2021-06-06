using UnityEngine;

public class PlayAnimations : MonoBehaviour
{
	public void Update()
	{
		GetComponent<Animation>().PlayQueued("CoinDropA");
		GetComponent<Animation>().PlayQueued("CoinDropB");
	}
}
