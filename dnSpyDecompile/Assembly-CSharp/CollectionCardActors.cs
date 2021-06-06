using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x020000F7 RID: 247
public class CollectionCardActors
{
	// Token: 0x06000E5F RID: 3679 RVA: 0x00050F94 File Offset: 0x0004F194
	public CollectionCardActors()
	{
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00050FA7 File Offset: 0x0004F1A7
	public CollectionCardActors(Actor actor)
	{
		this.AddCardActor(actor);
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00050FC1 File Offset: 0x0004F1C1
	public void AddCardActor(Actor actor)
	{
		this.m_cardActors.Add(actor);
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x00050FCF File Offset: 0x0004F1CF
	public Actor GetPreferredActor()
	{
		return this.GetActor(CollectionManager.Get().GetPreferredPremium());
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x00050FE1 File Offset: 0x0004F1E1
	public int Count()
	{
		return this.m_cardActors.Count;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00050FF0 File Offset: 0x0004F1F0
	public Actor GetActor(TAG_PREMIUM premium)
	{
		for (int i = 0; i < this.m_cardActors.Count; i++)
		{
			if (this.m_cardActors[i].GetPremium() == premium)
			{
				return this.m_cardActors[i];
			}
		}
		return this.m_cardActors.Last<Actor>();
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00051040 File Offset: 0x0004F240
	public void Destroy()
	{
		for (int i = 0; i < this.m_cardActors.Count; i++)
		{
			this.m_cardActors[i].Destroy();
		}
	}

	// Token: 0x040009E4 RID: 2532
	protected List<Actor> m_cardActors = new List<Actor>();
}
