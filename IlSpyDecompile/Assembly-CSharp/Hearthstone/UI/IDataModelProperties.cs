namespace Hearthstone.UI
{
	public interface IDataModelProperties
	{
		DataModelProperty[] Properties { get; }

		bool GetPropertyValue(int id, out object value);

		bool SetPropertyValue(int id, object value);

		bool GetPropertyInfo(int id, out DataModelProperty info);

		int GetPropertiesHashCode();
	}
}
