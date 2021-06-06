using System;

namespace Hearthstone.UI
{
	public static class DataModelExtensions
	{
		public static T CloneDataModel<T>(this T inputDataModel) where T : IDataModel
		{
			if (inputDataModel == null)
			{
				return default(T);
			}
			T result = (T)Activator.CreateInstance(inputDataModel.GetType());
			DataModelProperty[] properties = inputDataModel.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				DataModelProperty dataModelProperty = properties[i];
				if (inputDataModel.GetPropertyValue(dataModelProperty.PropertyId, out var value))
				{
					result.SetPropertyValue(dataModelProperty.PropertyId, value);
				}
			}
			return result;
		}

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
			DataModelProperty[] properties = inputDataModel.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				DataModelProperty dataModelProperty = properties[i];
				if (copiedDataModel.GetPropertyValue(dataModelProperty.PropertyId, out var value))
				{
					inputDataModel.SetPropertyValue(dataModelProperty.PropertyId, value);
				}
			}
			return inputDataModel;
		}
	}
}
