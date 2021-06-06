using System.Collections.Generic;
using bgs;

public class BnetPlayerChangelist
{
	private List<BnetPlayerChange> m_changes = new List<BnetPlayerChange>();

	public List<BnetPlayerChange> GetChanges()
	{
		return m_changes;
	}

	public void AddChange(BnetPlayerChange change)
	{
		m_changes.Add(change);
	}

	public bool HasChange(BnetGameAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return HasChange(player);
	}

	public bool HasChange(BnetAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return HasChange(player);
	}

	public bool HasChange(BnetPlayer player)
	{
		return FindChange(player) != null;
	}

	public BnetPlayerChange FindChange(BnetGameAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return FindChange(player);
	}

	public BnetPlayerChange FindChange(BnetAccountId id)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		return FindChange(player);
	}

	public BnetPlayerChange FindChange(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		return m_changes.Find((BnetPlayerChange change) => change.GetPlayer() == player);
	}
}
