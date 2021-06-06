using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;

	public bool spawnOnAwake;

	public bool destroyOnSpawn = true;

	protected virtual void Awake()
	{
		if (spawnOnAwake)
		{
			Spawn();
		}
	}

	public GameObject Spawn()
	{
		GameObject obj = Object.Instantiate(prefab);
		obj.transform.parent = base.transform.parent;
		TransformUtil.CopyLocal(obj, base.transform);
		SceneUtils.SetLayer(obj, base.gameObject.layer);
		if (destroyOnSpawn)
		{
			Object.Destroy(base.gameObject);
		}
		return obj;
	}

	public T Spawn<T>() where T : MonoBehaviour
	{
		if ((Object)prefab.GetComponent<T>() != (Object)null)
		{
			return Spawn().GetComponent<T>();
		}
		Debug.Log($"The prefab for spawner {base.gameObject.name} does not have component {typeof(T).Name}");
		return null;
	}
}
