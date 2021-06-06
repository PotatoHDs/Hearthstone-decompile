using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FE4 RID: 4068
	public static class DataModelExtensions
	{
		// Token: 0x0600B117 RID: 45335 RVA: 0x0036B0E4 File Offset: 0x003692E4
		public static T CloneDataModel<T>(this T inputDataModel) where T : IDataModel
		{
			if (inputDataModel == null)
			{
				return default(T);
			}
			T result = (T)((object)Activator.CreateInstance(inputDataModel.GetType()));
			foreach (DataModelProperty dataModelProperty in inputDataModel.Properties)
			{
				object value;
				if (inputDataModel.GetPropertyValue(dataModelProperty.PropertyId, out value))
				{
					result.SetPropertyValue(dataModelProperty.PropertyId, value);
				}
			}
			return result;
		}

		// Token: 0x0600B118 RID: 45336 RVA: 0x0036B170 File Offset: 0x00369370
		public static T CopyFromDataModel<T>(this T inputDataModel, T copiedDataModel) where T : IDataModel
		{
			if (inputDataModel == null)
			{
				return default(T);
			}
			if (copiedDataModel == null)
			{
				return inputDataModel;
			}
			foreach (DataModelProperty dataModelProperty in inputDataModel.Properties)
			{
				object value;
				if (copiedDataModel.GetPropertyValue(dataModelProperty.PropertyId, out value))
				{
					inputDataModel.SetPropertyValue(dataModelProperty.PropertyId, value);
				}
			}
			return inputDataModel;
		}
	}
}
