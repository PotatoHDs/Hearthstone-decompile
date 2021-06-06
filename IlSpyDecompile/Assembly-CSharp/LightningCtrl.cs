using System.Collections;
using UnityEngine;

public class LightningCtrl : MonoBehaviour
{
	public GameObject mylightning;

	private GameObject lightningObj;

	public float lifetime = 1f;

	public float position_X;

	public float position_Y;

	public float position_Z;

	public float scale = 0.1f;

	public float speed = 1f;

	public GameObject target;

	public GameObject destination;

	private void Start()
	{
	}

	private void Update()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			Spawn(target.transform, destination.transform);
		}
	}

	public void Spawn(Transform targetTransform, Transform destinationTransform)
	{
		lightningObj = Object.Instantiate(mylightning, new Vector3(position_X, position_Y, position_Z), new Quaternion(0f, 0f, 0f, 0f));
		lightningObj.transform.localScale = new Vector3(scale, scale, scale);
		ElectroScript component = lightningObj.GetComponent<ElectroScript>();
		component.timers.timeToPowerUp = speed;
		component.prefabs.target.position = targetTransform.position;
		component.prefabs.destination.position = destinationTransform.position;
		StartCoroutine(DestroyLightning());
	}

	private IEnumerator DestroyLightning()
	{
		yield return new WaitForSeconds(lifetime);
		Object.Destroy(lightningObj);
	}
}
