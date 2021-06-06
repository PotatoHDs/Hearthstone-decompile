using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	public class DataModelList<T> : DataModelEventDispatcher, IDataModelList, IList, ICollection, IEnumerable, IDataModelProperties, IList<T>, ICollection<T>, IEnumerable<T>
	{
		private List<T> m_list = new List<T>();

		private bool m_updateDataVersionOnChange = true;

		private static readonly DataModelProperty[] s_cachedProperties;

		public int Count => m_list.Count;

		bool ICollection.IsSynchronized => false;

		object ICollection.SyncRoot => this;

		public T this[int index]
		{
			get
			{
				return m_list[index];
			}
			set
			{
				if (!object.Equals(value, m_list[index]))
				{
					TryRemoveListener(m_list[index]);
					TryAddListener(value);
					m_list[index] = value;
					HandleDataChange();
				}
			}
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (T)value;
			}
		}

		public bool IsReadOnly => false;

		public bool IsFixedSize => false;

		T IList<T>.this[int index]
		{
			get
			{
				return m_list[index];
			}
			set
			{
				m_list[index] = value;
			}
		}

		public DataModelProperty[] Properties => s_cachedProperties;

		public IEnumerator<T> GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		private bool TryAddListener(T item)
		{
			DataModelEventDispatcher dataModelEventDispatcher = item as DataModelEventDispatcher;
			if (dataModelEventDispatcher != null)
			{
				RegisterNestedDataModel(dataModelEventDispatcher);
				return true;
			}
			return false;
		}

		private bool TryRemoveListener(T item)
		{
			DataModelEventDispatcher dataModelEventDispatcher = item as DataModelEventDispatcher;
			if (dataModelEventDispatcher != null)
			{
				RemoveNestedDataModel(dataModelEventDispatcher);
				return true;
			}
			return false;
		}

		private void HandleDataChange(object unused = null)
		{
			if (m_updateDataVersionOnChange)
			{
				DataContext.DataVersion++;
				DispatchChangedListeners();
			}
		}

		public void Add(T item)
		{
			m_list.Add(item);
			HandleDataChange();
			TryAddListener(item);
		}

		public void AddDefaultValue()
		{
			object value = ((!(typeof(T) == typeof(string))) ? ((object)Activator.CreateInstance<T>()) : "");
			Add(value);
		}

		public int IndexOf(T item)
		{
			return m_list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			m_list.Insert(index, item);
			HandleDataChange();
			TryAddListener(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			m_list.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			bool num = m_list.Remove(item);
			if (num)
			{
				TryRemoveListener(item);
				HandleDataChange();
			}
			return num;
		}

		public void Remove(object value)
		{
			if (m_list.Remove((T)value))
			{
				TryRemoveListener((T)value);
				HandleDataChange();
			}
		}

		public void RemoveAt(int index)
		{
			TryRemoveListener(m_list[index]);
			m_list.RemoveAt(index);
			HandleDataChange();
		}

		public int Add(object value)
		{
			m_list.Add((T)value);
			HandleDataChange();
			TryAddListener((T)value);
			return 1;
		}

		public void AddRange(IEnumerable<T> collection)
		{
			m_list.AddRange(collection);
			HandleDataChange();
			foreach (T item in collection)
			{
				TryAddListener(item);
			}
		}

		public bool Contains(object value)
		{
			return m_list.Contains((T)value);
		}

		public void Clear()
		{
			if (m_list.Count == 0)
			{
				return;
			}
			foreach (T item in m_list)
			{
				TryRemoveListener(item);
			}
			m_list.Clear();
			HandleDataChange();
		}

		public bool Contains(T item)
		{
			return m_list.Contains(item);
		}

		public int IndexOf(object value)
		{
			return m_list.IndexOf((T)value);
		}

		public void Insert(int index, object value)
		{
			m_list.Insert(index, (T)value);
			HandleDataChange();
			TryAddListener((T)value);
		}

		public void Sort(Comparison<T> comparison)
		{
			m_list.Sort(comparison);
			HandleDataChange();
		}

		public object GetElementAtIndex(int i)
		{
			return this[i];
		}

		public void DontUpdateDataVersionOnChange()
		{
			m_updateDataVersionOnChange = false;
		}

		public bool GetPropertyValue(int id, out object value)
		{
			value = null;
			if (id == 0)
			{
				value = Count;
				return true;
			}
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			throw new NotImplementedException();
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		public int GetPropertiesHashCode()
		{
			int num = 17;
			for (int i = 0; i < Count; i++)
			{
				num += 31 * (this[i]?.GetHashCode() ?? 0);
			}
			return num;
		}

		private static object Where(IEnumerable<object> matchingElements)
		{
			return matchingElements;
		}

		static DataModelList()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "count",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "where",
				QueryMethod = Where
			};
			array[1] = dataModelProperty;
			s_cachedProperties = array;
		}
	}
}
