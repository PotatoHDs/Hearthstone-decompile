using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.UI
{
	public class DataContext : IDataModelProvider
	{
		public delegate void DataChangedDelegate(IDataModel dataModel);

		public static int DataVersion;

		private int m_localDataVersion = 1;

		private readonly Map<int, IDataModel> m_modelMap = new Map<int, IDataModel>();

		private DataChangedDelegate m_onDataChanged;

		public void BindDataModel(IDataModel model)
		{
			if (!HasDataModelInstance(model))
			{
				if (m_modelMap.ContainsKey(model.DataModelId))
				{
					m_modelMap[model.DataModelId].RemoveChangedListener(HandleDataModelChanged);
				}
				m_modelMap[model.DataModelId] = model;
				model.RegisterChangedListener(HandleDataModelChanged, model);
				HandleDataChange(model);
				DataVersion++;
			}
		}

		public void UnbindDataModel(int id)
		{
			if (m_modelMap.ContainsKey(id))
			{
				IDataModel dataModel = m_modelMap[id];
				dataModel.RemoveChangedListener(HandleDataModelChanged);
				HandleDataChange(dataModel);
				m_modelMap.Remove(id);
				DataVersion++;
			}
		}

		public bool GetDataModel(int id, out IDataModel model)
		{
			return m_modelMap.TryGetValue(id, out model);
		}

		public bool HasDataModel(int id)
		{
			return m_modelMap.ContainsKey(id);
		}

		public bool HasDataModelInstance(IDataModel dataModel)
		{
			IDataModel model = null;
			if (dataModel == null || !GetDataModel(dataModel.DataModelId, out model))
			{
				return false;
			}
			return model == dataModel;
		}

		public ICollection<IDataModel> GetDataModels()
		{
			return m_modelMap.Values;
		}

		public void Clear()
		{
			IDataModel[] array = m_modelMap.Values.ToArray();
			foreach (IDataModel dataModel in array)
			{
				dataModel.RemoveChangedListener(HandleDataModelChanged);
				HandleDataChange(dataModel);
			}
			m_modelMap.Clear();
			DataVersion++;
		}

		private void HandleDataChange(IDataModel dataModel)
		{
			m_localDataVersion++;
			m_onDataChanged?.Invoke(dataModel);
		}

		public int GetLocalDataVersion()
		{
			return m_localDataVersion;
		}

		public void RegisterChangedListener(DataChangedDelegate listener)
		{
			m_onDataChanged = (DataChangedDelegate)Delegate.Remove(m_onDataChanged, listener);
			m_onDataChanged = (DataChangedDelegate)Delegate.Combine(m_onDataChanged, listener);
		}

		public void RemoveChangedListener(DataChangedDelegate listener)
		{
			m_onDataChanged = (DataChangedDelegate)Delegate.Remove(m_onDataChanged, listener);
		}

		private void HandleDataModelChanged(object payload)
		{
			HandleDataChange((IDataModel)payload);
		}
	}
}
