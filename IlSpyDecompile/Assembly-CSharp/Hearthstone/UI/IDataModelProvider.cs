using System.Collections.Generic;

namespace Hearthstone.UI
{
	public interface IDataModelProvider
	{
		int GetLocalDataVersion();

		bool GetDataModel(int id, out IDataModel model);

		ICollection<IDataModel> GetDataModels();
	}
}
