using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class ComponentAction<T> : FsmStateAction where T : Component
	{
		protected GameObject cachedGameObject;

		protected T cachedComponent;

		protected Rigidbody rigidbody => cachedComponent as Rigidbody;

		protected Rigidbody2D rigidbody2d => cachedComponent as Rigidbody2D;

		protected Renderer renderer => cachedComponent as Renderer;

		protected Animation animation => cachedComponent as Animation;

		protected AudioSource audio => cachedComponent as AudioSource;

		protected Camera camera => cachedComponent as Camera;

		protected GUIText guiText => cachedComponent as GUIText;

		protected GUITexture guiTexture => cachedComponent as GUITexture;

		protected Light light => cachedComponent as Light;

		protected bool UpdateCache(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if ((Object)cachedComponent == (Object)null || cachedGameObject != go)
			{
				cachedComponent = go.GetComponent<T>();
				cachedGameObject = go;
				if ((Object)cachedComponent == (Object)null)
				{
					LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name);
				}
			}
			return (Object)cachedComponent != (Object)null;
		}

		protected bool UpdateCacheAddComponent(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if ((Object)cachedComponent == (Object)null || cachedGameObject != go)
			{
				cachedComponent = go.GetComponent<T>();
				cachedGameObject = go;
				if ((Object)cachedComponent == (Object)null)
				{
					cachedComponent = go.AddComponent<T>();
					cachedComponent.hideFlags = HideFlags.DontSaveInEditor;
				}
			}
			return (Object)cachedComponent != (Object)null;
		}

		protected void SendEvent(FsmEventTarget eventTarget, FsmEvent fsmEvent)
		{
			base.Fsm.Event(cachedGameObject, eventTarget, fsmEvent);
		}
	}
}
