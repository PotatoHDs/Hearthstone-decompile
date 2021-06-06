using System;
using System.Linq;

namespace Hearthstone.UI.Editor
{
	// Token: 0x0200105F RID: 4191
	public static class EditorDataContextProvider
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600B54B RID: 46411 RVA: 0x0037BDBC File Offset: 0x00379FBC
		public static IDataModel[] GlobalDataModels
		{
			get
			{
				if (EditorDataContextProvider.s_globalDataModels == null)
				{
					EditorDataContextProvider.s_globalDataModels = (from a in EditorDataContextProvider.DataModelTypes
					select Activator.CreateInstance(a) as IDataModel).ToArray<IDataModel>();
				}
				return EditorDataContextProvider.s_globalDataModels;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600B54C RID: 46412 RVA: 0x0037BE08 File Offset: 0x0037A008
		public static Type[] DataModelTypes
		{
			get
			{
				if (EditorDataContextProvider.s_dataModelTypes == null)
				{
					Type dataModelInterface = typeof(IDataModel);
					EditorDataContextProvider.s_dataModelTypes = (from a in dataModelInterface.Assembly.GetTypes()
					where a != dataModelInterface && dataModelInterface.IsAssignableFrom(a)
					select a).ToArray<Type>();
				}
				return EditorDataContextProvider.s_dataModelTypes;
			}
		}

		// Token: 0x04009738 RID: 38712
		private static IDataModel[] s_globalDataModels;

		// Token: 0x04009739 RID: 38713
		private static Type[] s_dataModelTypes;
	}
}
