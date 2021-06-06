using System.Collections.Generic;

namespace Hearthstone.UI
{
	public interface IDebugContext
	{
		string DebugPath { get; }

		IDebugContext ParentContext { get; set; }

		ICollection<IDebugContext> ChildContexts { get; }
	}
}
