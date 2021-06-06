using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;

namespace Hearthstone.UI
{
	// Token: 0x02000FE3 RID: 4067
	public class DataModelEventDispatcher
	{
		// Token: 0x0600B111 RID: 45329 RVA: 0x0036AFFE File Offset: 0x003691FE
		public void RegisterChangedListener(Action<object> listener, object payload = null)
		{
			this.m_changedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B112 RID: 45330 RVA: 0x0036B00F File Offset: 0x0036920F
		public void RemoveChangedListener(Action<object> listener)
		{
			this.m_changedEvent.RemoveSetListener(listener);
		}

		// Token: 0x0600B113 RID: 45331 RVA: 0x0036B020 File Offset: 0x00369220
		public void DispatchChangedListeners(HashSet<DataModelEventDispatcher> seenList = null)
		{
			if (this.m_parentDispatchers.Count > 0 || seenList != null)
			{
				seenList = (seenList ?? new HashSet<DataModelEventDispatcher>());
				if (seenList.Contains(this))
				{
					return;
				}
				seenList.Add(this);
			}
			this.m_changedEvent.SetAndDispatch();
			foreach (DataModelEventDispatcher dataModelEventDispatcher in this.m_parentDispatchers)
			{
				dataModelEventDispatcher.DispatchChangedListeners(seenList);
			}
		}

		// Token: 0x0600B114 RID: 45332 RVA: 0x0036B0AC File Offset: 0x003692AC
		protected void RegisterNestedDataModel(DataModelEventDispatcher dispatcher)
		{
			if (dispatcher == null)
			{
				return;
			}
			dispatcher.m_parentDispatchers.Add(this);
		}

		// Token: 0x0600B115 RID: 45333 RVA: 0x0036B0BE File Offset: 0x003692BE
		protected void RemoveNestedDataModel(DataModelEventDispatcher dispatcher)
		{
			if (dispatcher == null)
			{
				return;
			}
			dispatcher.m_parentDispatchers.Remove(this);
		}

		// Token: 0x04009594 RID: 38292
		private FlagStateTracker m_changedEvent;

		// Token: 0x04009595 RID: 38293
		private List<DataModelEventDispatcher> m_parentDispatchers = new List<DataModelEventDispatcher>();
	}
}
