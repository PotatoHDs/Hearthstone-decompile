using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.UI
{
	// Token: 0x02000FE2 RID: 4066
	public class DataContext : IDataModelProvider
	{
		// Token: 0x0600B103 RID: 45315 RVA: 0x0036ADD0 File Offset: 0x00368FD0
		public void BindDataModel(IDataModel model)
		{
			if (this.HasDataModelInstance(model))
			{
				return;
			}
			if (this.m_modelMap.ContainsKey(model.DataModelId))
			{
				this.m_modelMap[model.DataModelId].RemoveChangedListener(new Action<object>(this.HandleDataModelChanged));
			}
			this.m_modelMap[model.DataModelId] = model;
			model.RegisterChangedListener(new Action<object>(this.HandleDataModelChanged), model);
			this.HandleDataChange(model);
			DataContext.DataVersion++;
		}

		// Token: 0x0600B104 RID: 45316 RVA: 0x0036AE54 File Offset: 0x00369054
		public void UnbindDataModel(int id)
		{
			if (!this.m_modelMap.ContainsKey(id))
			{
				return;
			}
			IDataModel dataModel = this.m_modelMap[id];
			dataModel.RemoveChangedListener(new Action<object>(this.HandleDataModelChanged));
			this.HandleDataChange(dataModel);
			this.m_modelMap.Remove(id);
			DataContext.DataVersion++;
		}

		// Token: 0x0600B105 RID: 45317 RVA: 0x0036AEAF File Offset: 0x003690AF
		public bool GetDataModel(int id, out IDataModel model)
		{
			return this.m_modelMap.TryGetValue(id, out model);
		}

		// Token: 0x0600B106 RID: 45318 RVA: 0x0036AEBE File Offset: 0x003690BE
		public bool HasDataModel(int id)
		{
			return this.m_modelMap.ContainsKey(id);
		}

		// Token: 0x0600B107 RID: 45319 RVA: 0x0036AECC File Offset: 0x003690CC
		public bool HasDataModelInstance(IDataModel dataModel)
		{
			IDataModel dataModel2 = null;
			return dataModel != null && this.GetDataModel(dataModel.DataModelId, out dataModel2) && dataModel2 == dataModel;
		}

		// Token: 0x0600B108 RID: 45320 RVA: 0x0036AEF4 File Offset: 0x003690F4
		public ICollection<IDataModel> GetDataModels()
		{
			return this.m_modelMap.Values;
		}

		// Token: 0x0600B109 RID: 45321 RVA: 0x0036AF04 File Offset: 0x00369104
		public void Clear()
		{
			foreach (IDataModel dataModel in this.m_modelMap.Values.ToArray<IDataModel>())
			{
				dataModel.RemoveChangedListener(new Action<object>(this.HandleDataModelChanged));
				this.HandleDataChange(dataModel);
			}
			this.m_modelMap.Clear();
			DataContext.DataVersion++;
		}

		// Token: 0x0600B10A RID: 45322 RVA: 0x0036AF64 File Offset: 0x00369164
		private void HandleDataChange(IDataModel dataModel)
		{
			this.m_localDataVersion++;
			DataContext.DataChangedDelegate onDataChanged = this.m_onDataChanged;
			if (onDataChanged == null)
			{
				return;
			}
			onDataChanged(dataModel);
		}

		// Token: 0x0600B10B RID: 45323 RVA: 0x0036AF85 File Offset: 0x00369185
		public int GetLocalDataVersion()
		{
			return this.m_localDataVersion;
		}

		// Token: 0x0600B10C RID: 45324 RVA: 0x0036AF8D File Offset: 0x0036918D
		public void RegisterChangedListener(DataContext.DataChangedDelegate listener)
		{
			this.m_onDataChanged = (DataContext.DataChangedDelegate)Delegate.Remove(this.m_onDataChanged, listener);
			this.m_onDataChanged = (DataContext.DataChangedDelegate)Delegate.Combine(this.m_onDataChanged, listener);
		}

		// Token: 0x0600B10D RID: 45325 RVA: 0x0036AFBD File Offset: 0x003691BD
		public void RemoveChangedListener(DataContext.DataChangedDelegate listener)
		{
			this.m_onDataChanged = (DataContext.DataChangedDelegate)Delegate.Remove(this.m_onDataChanged, listener);
		}

		// Token: 0x0600B10E RID: 45326 RVA: 0x0036AFD6 File Offset: 0x003691D6
		private void HandleDataModelChanged(object payload)
		{
			this.HandleDataChange((IDataModel)payload);
		}

		// Token: 0x04009590 RID: 38288
		public static int DataVersion;

		// Token: 0x04009591 RID: 38289
		private int m_localDataVersion = 1;

		// Token: 0x04009592 RID: 38290
		private readonly Map<int, IDataModel> m_modelMap = new Map<int, IDataModel>();

		// Token: 0x04009593 RID: 38291
		private DataContext.DataChangedDelegate m_onDataChanged;

		// Token: 0x0200281C RID: 10268
		// (Invoke) Token: 0x06013B01 RID: 80641
		public delegate void DataChangedDelegate(IDataModel dataModel);
	}
}
