public class PageNode
{
	public PageData PageData { get; private set; }

	public PageNode PageToLeft { get; set; }

	public PageNode PageToRight { get; set; }

	public PageNode(PageData data)
	{
		PageData = data;
	}
}
