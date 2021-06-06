using UnityEngine;

public abstract class CardBackPagingArrowBase : MonoBehaviour
{
	public abstract void EnablePaging(bool enable);

	public abstract void AddEventListener(UIEventType eventType, UIEvent.Handler handler);
}
