using System;
using UnityEngine;

// Token: 0x02000A89 RID: 2697
[CustomEditClass]
public class SetParenttByName : MonoBehaviour
{
	// Token: 0x06009072 RID: 36978 RVA: 0x002EDE74 File Offset: 0x002EC074
	private void Start()
	{
		if (string.IsNullOrEmpty(this.m_ParentName))
		{
			return;
		}
		GameObject gameObject = this.FindGameObject(this.m_ParentName);
		if (gameObject == null)
		{
			Log.Graphics.Print("SetParenttByName failed to locate parent object: {0}", new object[]
			{
				this.m_ParentName
			});
			return;
		}
		base.transform.parent = gameObject.transform;
	}

	// Token: 0x06009073 RID: 36979 RVA: 0x002EC440 File Offset: 0x002EA640
	private GameObject FindGameObject(string gameObjName)
	{
		if (gameObjName[0] != '/')
		{
			return GameObject.Find(gameObjName);
		}
		string[] array = gameObjName.Split(new char[]
		{
			'/'
		});
		return GameObject.Find(array[array.Length - 1]);
	}

	// Token: 0x04007945 RID: 31045
	[CustomEditField(T = EditType.SCENE_OBJECT)]
	public string m_ParentName;
}
