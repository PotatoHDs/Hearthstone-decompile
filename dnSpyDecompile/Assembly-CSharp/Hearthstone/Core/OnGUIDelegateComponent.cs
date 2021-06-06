using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x0200107C RID: 4220
	public class OnGUIDelegateComponent : MonoBehaviour
	{
		// Token: 0x0600B653 RID: 46675 RVA: 0x0037EDA0 File Offset: 0x0037CFA0
		public void AddOnGUIDelegate(Action newAction)
		{
			bool flag = false;
			using (List<Action>.Enumerator enumerator = this.m_onGUIDelegates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == newAction)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this.m_onGUIDelegates.Add(newAction);
			}
		}

		// Token: 0x0600B654 RID: 46676 RVA: 0x0037EE08 File Offset: 0x0037D008
		public void RemoveOnGUIDelegate(Action action)
		{
			for (int i = 0; i < this.m_onGUIDelegates.Count; i++)
			{
				if (this.m_onGUIDelegates[i] == action)
				{
					this.m_onGUIDelegates.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600B655 RID: 46677 RVA: 0x0037EE4C File Offset: 0x0037D04C
		private void OnGUI()
		{
			foreach (Action action in this.m_onGUIDelegates)
			{
				action();
			}
		}

		// Token: 0x040097A6 RID: 38822
		private List<Action> m_onGUIDelegates = new List<Action>();
	}
}
