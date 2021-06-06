using UnityEngine;

[CustomEditClass]
public class SetParenttByName : MonoBehaviour
{
	[CustomEditField(T = EditType.SCENE_OBJECT)]
	public string m_ParentName;

	private void Start()
	{
		if (!string.IsNullOrEmpty(m_ParentName))
		{
			GameObject gameObject = FindGameObject(m_ParentName);
			if (gameObject == null)
			{
				Log.Graphics.Print("SetParenttByName failed to locate parent object: {0}", m_ParentName);
			}
			else
			{
				base.transform.parent = gameObject.transform;
			}
		}
	}

	private GameObject FindGameObject(string gameObjName)
	{
		if (gameObjName[0] != '/')
		{
			return GameObject.Find(gameObjName);
		}
		string[] array = gameObjName.Split('/');
		return GameObject.Find(array[array.Length - 1]);
	}
}
