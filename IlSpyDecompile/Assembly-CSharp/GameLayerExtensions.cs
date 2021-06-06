public static class GameLayerExtensions
{
	public static int LayerBit(this GameLayer gameLayer)
	{
		return 1 << (int)gameLayer;
	}
}
