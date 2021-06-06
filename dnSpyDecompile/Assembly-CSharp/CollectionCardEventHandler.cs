using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class CollectionCardEventHandler : MonoBehaviour
{
	// Token: 0x06000E77 RID: 3703 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCardRemoved(CollectionDeckTray collectionDeckTray, CollectionDeck deck)
	{
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldUpdateVisuals()
	{
		return true;
	}
}
