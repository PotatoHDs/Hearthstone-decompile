using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02000FEC RID: 4076
	public interface IDataModelProvider
	{
		// Token: 0x0600B155 RID: 45397
		int GetLocalDataVersion();

		// Token: 0x0600B156 RID: 45398
		bool GetDataModel(int id, out IDataModel model);

		// Token: 0x0600B157 RID: 45399
		ICollection<IDataModel> GetDataModels();
	}
}
