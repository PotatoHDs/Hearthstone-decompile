using UnityEngine;

public class CardStandIn : MonoBehaviour
{
	public Card linkedCard;

	public Collider standInCollider;

	public void DisableStandIn()
	{
		standInCollider.enabled = false;
	}

	public void EnableStandIn()
	{
		standInCollider.enabled = true;
	}
}
