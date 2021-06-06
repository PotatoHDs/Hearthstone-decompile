using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02000FE6 RID: 4070
	public class DataModelList<T> : DataModelEventDispatcher, IDataModelList, IList, ICollection, IEnumerable, IDataModelProperties, IList<T>, ICollection<T>, IEnumerable<!0>
	{
		// Token: 0x0600B11E RID: 45342 RVA: 0x0036B1EB File Offset: 0x003693EB
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0600B11F RID: 45343 RVA: 0x0036B1EB File Offset: 0x003693EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0600B120 RID: 45344 RVA: 0x0036B1FD File Offset: 0x003693FD
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600B121 RID: 45345 RVA: 0x0036B204 File Offset: 0x00369404
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600B122 RID: 45346 RVA: 0x0001FA65 File Offset: 0x0001DC65
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600B123 RID: 45347 RVA: 0x00005576 File Offset: 0x00003776
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170008FE RID: 2302
		public T this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				if (!object.Equals(value, this.m_list[index]))
				{
					this.TryRemoveListener(this.m_list[index]);
					this.TryAddListener(value);
					this.m_list[index] = value;
					this.HandleDataChange(null);
				}
			}
		}

		// Token: 0x170008FF RID: 2303
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (T)((object)value);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600B128 RID: 45352 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600B129 RID: 45353 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B12A RID: 45354 RVA: 0x0036B29C File Offset: 0x0036949C
		private bool TryAddListener(T item)
		{
			DataModelEventDispatcher dataModelEventDispatcher = item as DataModelEventDispatcher;
			if (dataModelEventDispatcher != null)
			{
				base.RegisterNestedDataModel(dataModelEventDispatcher);
				return true;
			}
			return false;
		}

		// Token: 0x0600B12B RID: 45355 RVA: 0x0036B2C4 File Offset: 0x003694C4
		private bool TryRemoveListener(T item)
		{
			DataModelEventDispatcher dataModelEventDispatcher = item as DataModelEventDispatcher;
			if (dataModelEventDispatcher != null)
			{
				base.RemoveNestedDataModel(dataModelEventDispatcher);
				return true;
			}
			return false;
		}

		// Token: 0x0600B12C RID: 45356 RVA: 0x0036B2EA File Offset: 0x003694EA
		private void HandleDataChange(object unused = null)
		{
			if (this.m_updateDataVersionOnChange)
			{
				DataContext.DataVersion++;
				base.DispatchChangedListeners(null);
			}
		}

		// Token: 0x0600B12D RID: 45357 RVA: 0x0036B307 File Offset: 0x00369507
		public void Add(T item)
		{
			this.m_list.Add(item);
			this.HandleDataChange(null);
			this.TryAddListener(item);
		}

		// Token: 0x0600B12E RID: 45358 RVA: 0x0036B324 File Offset: 0x00369524
		public void AddDefaultValue()
		{
			object value;
			if (typeof(T) == typeof(string))
			{
				value = "";
			}
			else
			{
				value = Activator.CreateInstance<T>();
			}
			this.Add(value);
		}

		// Token: 0x0600B12F RID: 45359 RVA: 0x0036B367 File Offset: 0x00369567
		public int IndexOf(T item)
		{
			return this.m_list.IndexOf(item);
		}

		// Token: 0x0600B130 RID: 45360 RVA: 0x0036B375 File Offset: 0x00369575
		public void Insert(int index, T item)
		{
			this.m_list.Insert(index, item);
			this.HandleDataChange(null);
			this.TryAddListener(item);
		}

		// Token: 0x0600B131 RID: 45361 RVA: 0x0036B393 File Offset: 0x00369593
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_list.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x0036B3A2 File Offset: 0x003695A2
		public bool Remove(T item)
		{
			bool flag = this.m_list.Remove(item);
			if (flag)
			{
				this.TryRemoveListener(item);
				this.HandleDataChange(null);
			}
			return flag;
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x0036B3C2 File Offset: 0x003695C2
		public void Remove(object value)
		{
			if (this.m_list.Remove((T)((object)value)))
			{
				this.TryRemoveListener((T)((object)value));
				this.HandleDataChange(null);
			}
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x0036B3EB File Offset: 0x003695EB
		public void RemoveAt(int index)
		{
			this.TryRemoveListener(this.m_list[index]);
			this.m_list.RemoveAt(index);
			this.HandleDataChange(null);
		}

		// Token: 0x17000902 RID: 2306
		T IList<!0>.this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				this.m_list[index] = value;
			}
		}

		// Token: 0x0600B137 RID: 45367 RVA: 0x0036B422 File Offset: 0x00369622
		public int Add(object value)
		{
			this.m_list.Add((T)((object)value));
			this.HandleDataChange(null);
			this.TryAddListener((T)((object)value));
			return 1;
		}

		// Token: 0x0600B138 RID: 45368 RVA: 0x0036B44C File Offset: 0x0036964C
		public void AddRange(IEnumerable<T> collection)
		{
			this.m_list.AddRange(collection);
			this.HandleDataChange(null);
			foreach (T item in collection)
			{
				this.TryAddListener(item);
			}
		}

		// Token: 0x0600B139 RID: 45369 RVA: 0x0036B4A8 File Offset: 0x003696A8
		public bool Contains(object value)
		{
			return this.m_list.Contains((T)((object)value));
		}

		// Token: 0x0600B13A RID: 45370 RVA: 0x0036B4BC File Offset: 0x003696BC
		public void Clear()
		{
			if (this.m_list.Count == 0)
			{
				return;
			}
			foreach (T item in this.m_list)
			{
				this.TryRemoveListener(item);
			}
			this.m_list.Clear();
			this.HandleDataChange(null);
		}

		// Token: 0x0600B13B RID: 45371 RVA: 0x0036B530 File Offset: 0x00369730
		public bool Contains(T item)
		{
			return this.m_list.Contains(item);
		}

		// Token: 0x0600B13C RID: 45372 RVA: 0x0036B53E File Offset: 0x0036973E
		public int IndexOf(object value)
		{
			return this.m_list.IndexOf((T)((object)value));
		}

		// Token: 0x0600B13D RID: 45373 RVA: 0x0036B551 File Offset: 0x00369751
		public void Insert(int index, object value)
		{
			this.m_list.Insert(index, (T)((object)value));
			this.HandleDataChange(null);
			this.TryAddListener((T)((object)value));
		}

		// Token: 0x0600B13E RID: 45374 RVA: 0x0036B579 File Offset: 0x00369779
		public void Sort(Comparison<T> comparison)
		{
			this.m_list.Sort(comparison);
			this.HandleDataChange(null);
		}

		// Token: 0x0600B13F RID: 45375 RVA: 0x0036B27D File Offset: 0x0036947D
		public object GetElementAtIndex(int i)
		{
			return this[i];
		}

		// Token: 0x0600B140 RID: 45376 RVA: 0x0036B58E File Offset: 0x0036978E
		public void DontUpdateDataVersionOnChange()
		{
			this.m_updateDataVersionOnChange = false;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600B141 RID: 45377 RVA: 0x0036B597 File Offset: 0x00369797
		public DataModelProperty[] Properties
		{
			get
			{
				return DataModelList<T>.s_cachedProperties;
			}
		}

		// Token: 0x0600B142 RID: 45378 RVA: 0x0036B59E File Offset: 0x0036979E
		public bool GetPropertyValue(int id, out object value)
		{
			value = null;
			if (id == 0)
			{
				value = this.Count;
				return true;
			}
			return false;
		}

		// Token: 0x0600B143 RID: 45379 RVA: 0x0036B1FD File Offset: 0x003693FD
		public bool SetPropertyValue(int id, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600B144 RID: 45380 RVA: 0x0036B5B6 File Offset: 0x003697B6
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 1)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x0600B145 RID: 45381 RVA: 0x0036B5F4 File Offset: 0x003697F4
		public int GetPropertiesHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.Count; i++)
			{
				T t = this[i];
				num += 31 * ((t != null) ? t.GetHashCode() : 0);
			}
			return num;
		}

		// Token: 0x0600B146 RID: 45382 RVA: 0x00005576 File Offset: 0x00003776
		private static object Where(IEnumerable<object> matchingElements)
		{
			return matchingElements;
		}

		// Token: 0x04009596 RID: 38294
		private List<T> m_list = new List<T>();

		// Token: 0x04009597 RID: 38295
		private bool m_updateDataVersionOnChange = true;

		// Token: 0x04009598 RID: 38296
		private static readonly DataModelProperty[] s_cachedProperties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "count",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "where",
				QueryMethod = new DataModelProperty.QueryDelegate(DataModelList<T>.Where)
			}
		};
	}
}
