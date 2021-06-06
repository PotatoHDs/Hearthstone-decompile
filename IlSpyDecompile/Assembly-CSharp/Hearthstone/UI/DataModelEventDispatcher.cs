using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;

namespace Hearthstone.UI
{
	public class DataModelEventDispatcher
	{
		private FlagStateTracker m_changedEvent;

		private List<DataModelEventDispatcher> m_parentDispatchers = new List<DataModelEventDispatcher>();

		public void RegisterChangedListener(Action<object> listener, object payload = null)
		{
			m_changedEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet: false);
		}

		public void RemoveChangedListener(Action<object> listener)
		{
			m_changedEvent.RemoveSetListener(listener);
		}

		public void DispatchChangedListeners(HashSet<DataModelEventDispatcher> seenList = null)
		{
			if (m_parentDispatchers.Count > 0 || seenList != null)
			{
				seenList = seenList ?? new HashSet<DataModelEventDispatcher>();
				if (seenList.Contains(this))
				{
					return;
				}
				seenList.Add(this);
			}
			m_changedEvent.SetAndDispatch();
			foreach (DataModelEventDispatcher parentDispatcher in m_parentDispatchers)
			{
				parentDispatcher.DispatchChangedListeners(seenList);
			}
		}

		protected void RegisterNestedDataModel(DataModelEventDispatcher dispatcher)
		{
			dispatcher?.m_parentDispatchers.Add(this);
		}

		protected void RemoveNestedDataModel(DataModelEventDispatcher dispatcher)
		{
			dispatcher?.m_parentDispatchers.Remove(this);
		}
	}
}
