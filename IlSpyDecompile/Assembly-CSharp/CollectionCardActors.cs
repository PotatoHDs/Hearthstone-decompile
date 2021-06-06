using System.Collections.Generic;
using System.Linq;

public class CollectionCardActors
{
	protected List<Actor> m_cardActors = new List<Actor>();

	public CollectionCardActors()
	{
	}

	public CollectionCardActors(Actor actor)
	{
		AddCardActor(actor);
	}

	public void AddCardActor(Actor actor)
	{
		m_cardActors.Add(actor);
	}

	public Actor GetPreferredActor()
	{
		return GetActor(CollectionManager.Get().GetPreferredPremium());
	}

	public int Count()
	{
		return m_cardActors.Count;
	}

	public Actor GetActor(TAG_PREMIUM premium)
	{
		for (int i = 0; i < m_cardActors.Count; i++)
		{
			if (m_cardActors[i].GetPremium() == premium)
			{
				return m_cardActors[i];
			}
		}
		return m_cardActors.Last();
	}

	public void Destroy()
	{
		for (int i = 0; i < m_cardActors.Count; i++)
		{
			m_cardActors[i].Destroy();
		}
	}
}
