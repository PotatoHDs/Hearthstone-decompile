using UnityEngine;

public class CollectionCardEventHandler : MonoBehaviour
{
	public virtual void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
	}

	public virtual void OnCardRemoved(CollectionDeckTray collectionDeckTray, CollectionDeck deck)
	{
	}

	public virtual bool ShouldUpdateVisuals()
	{
		return true;
	}
}
