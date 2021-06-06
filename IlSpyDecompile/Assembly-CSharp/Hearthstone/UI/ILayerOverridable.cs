namespace Hearthstone.UI
{
	public interface ILayerOverridable
	{
		bool HandlesChildLayers { get; }

		void SetLayerOverride(GameLayer layer);

		void ClearLayerOverride();
	}
}
