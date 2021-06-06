using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.Core
{
	public class OnGUIDelegateComponent : MonoBehaviour
	{
		private List<Action> m_onGUIDelegates = new List<Action>();

		public void AddOnGUIDelegate(Action newAction)
		{
			bool flag = false;
			foreach (Action onGUIDelegate in m_onGUIDelegates)
			{
				if (onGUIDelegate == newAction)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				m_onGUIDelegates.Add(newAction);
			}
		}

		public void RemoveOnGUIDelegate(Action action)
		{
			for (int i = 0; i < m_onGUIDelegates.Count; i++)
			{
				if (m_onGUIDelegates[i] == action)
				{
					m_onGUIDelegates.RemoveAt(i);
					break;
				}
			}
		}

		private void OnGUI()
		{
			foreach (Action onGUIDelegate in m_onGUIDelegates)
			{
				onGUIDelegate();
			}
		}
	}
}
