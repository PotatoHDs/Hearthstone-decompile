using System;
using System.Linq;

namespace Hearthstone.UI.Editor
{
	public static class EditorDataContextProvider
	{
		private static IDataModel[] s_globalDataModels;

		private static Type[] s_dataModelTypes;

		public static IDataModel[] GlobalDataModels
		{
			get
			{
				if (s_globalDataModels == null)
				{
					s_globalDataModels = DataModelTypes.Select((Type a) => Activator.CreateInstance(a) as IDataModel).ToArray();
				}
				return s_globalDataModels;
			}
		}

		public static Type[] DataModelTypes
		{
			get
			{
				if (s_dataModelTypes == null)
				{
					Type dataModelInterface = typeof(IDataModel);
					s_dataModelTypes = (from a in dataModelInterface.Assembly.GetTypes()
						where a != dataModelInterface && dataModelInterface.IsAssignableFrom(a)
						select a).ToArray();
				}
				return s_dataModelTypes;
			}
		}
	}
}
