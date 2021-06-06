namespace Hearthstone.UI
{
	public interface IDynamicPropertyResolverProxy : IDynamicPropertyResolver
	{
		void SetTarget(object target);
	}
}
