public class ScrollBarThumb : PegUIElement
{
	private bool m_isBarDragging;

	private void Update()
	{
		if (IsDragging() && UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			StopDragging();
		}
	}

	public bool IsDragging()
	{
		return m_isBarDragging;
	}

	public void StartDragging()
	{
		m_isBarDragging = true;
	}

	public void StopDragging()
	{
		m_isBarDragging = false;
	}

	protected override void OnDrag()
	{
		StartDragging();
	}
}
