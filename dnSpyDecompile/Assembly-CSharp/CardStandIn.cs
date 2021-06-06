using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class CardStandIn : MonoBehaviour
{
	// Token: 0x060028D7 RID: 10455 RVA: 0x000CF017 File Offset: 0x000CD217
	public void DisableStandIn()
	{
		this.standInCollider.enabled = false;
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x000CF025 File Offset: 0x000CD225
	public void EnableStandIn()
	{
		this.standInCollider.enabled = true;
	}

	// Token: 0x0400174D RID: 5965
	public Card linkedCard;

	// Token: 0x0400174E RID: 5966
	public Collider standInCollider;
}
