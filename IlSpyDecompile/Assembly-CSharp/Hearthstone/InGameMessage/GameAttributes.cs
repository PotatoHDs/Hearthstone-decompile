using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.InGameMessage
{
	public class GameAttributes
	{
		private Dictionary<string, bool> m_attrs = new Dictionary<string, bool>();

		private static GameAttributes s_instance;

		public bool Create(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (m_attrs.TryGetValue(name, out var _))
			{
				return false;
			}
			m_attrs.Add(name, value: false);
			return true;
		}

		public bool Delete(string name)
		{
			if (!Exist(name))
			{
				return false;
			}
			return m_attrs.Remove(name);
		}

		public bool Activate(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (m_attrs.TryGetValue(name, out var value))
			{
				value = true;
			}
			else
			{
				m_attrs.Add(name, value: true);
			}
			return true;
		}

		public bool Exist(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			return m_attrs.ContainsKey(name);
		}

		public IEnumerable<string> GetAttributions(bool activeOnly)
		{
			if (!activeOnly)
			{
				return m_attrs.Keys;
			}
			return from attr in m_attrs
				where attr.Value
				select attr.Key;
		}

		public static GameAttributes Get()
		{
			if (s_instance == null)
			{
				s_instance = new GameAttributes();
			}
			return s_instance;
		}
	}
}
