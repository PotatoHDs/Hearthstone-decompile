using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class Spawner : MonoBehaviour
{
	// Token: 0x06000AC0 RID: 2752 RVA: 0x0003FC70 File Offset: 0x0003DE70
	protected virtual void Awake()
	{
		if (this.spawnOnAwake)
		{
			this.Spawn();
		}
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0003FC84 File Offset: 0x0003DE84
	public GameObject Spawn()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
		gameObject.transform.parent = base.transform.parent;
		TransformUtil.CopyLocal(gameObject, base.transform);
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		if (this.destroyOnSpawn)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		return gameObject;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0003FCEC File Offset: 0x0003DEEC
	public T Spawn<T>() where T : MonoBehaviour
	{
		if (this.prefab.GetComponent<T>() != null)
		{
			return this.Spawn().GetComponent<T>();
		}
		Debug.Log(string.Format("The prefab for spawner {0} does not have component {1}", base.gameObject.name, typeof(T).Name));
		return default(T);
	}

	// Token: 0x040006D3 RID: 1747
	public GameObject prefab;

	// Token: 0x040006D4 RID: 1748
	public bool spawnOnAwake;

	// Token: 0x040006D5 RID: 1749
	public bool destroyOnSpawn = true;
}
