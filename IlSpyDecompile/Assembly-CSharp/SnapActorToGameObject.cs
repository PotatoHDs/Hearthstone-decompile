using UnityEngine;

public class SnapActorToGameObject : MonoBehaviour
{
	public bool m_SnapPostion = true;

	public bool m_SnapRotation = true;

	public bool m_SnapScale = true;

	public bool m_ResetTransformOnDisable;

	private Transform m_actorTransform;

	private Vector3 m_OrgPosition;

	private Quaternion m_OrgRotation;

	private Vector3 m_OrgScale;

	private void Start()
	{
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (actor == null)
		{
			Spell spell = SceneUtils.FindComponentInParents<Spell>(base.gameObject);
			if (spell != null)
			{
				actor = spell.GetSourceCard().GetActor();
			}
		}
		if (actor == null)
		{
			Debug.LogError($"SnapActorToGameObject on {base.gameObject.name} failed to find Actor object!");
			base.enabled = false;
		}
		else
		{
			m_actorTransform = actor.transform;
		}
	}

	private void OnEnable()
	{
		if (!(m_actorTransform == null))
		{
			m_OrgPosition = m_actorTransform.localPosition;
			m_OrgRotation = m_actorTransform.localRotation;
			m_OrgScale = m_actorTransform.localScale;
		}
	}

	private void OnDisable()
	{
		if (!(m_actorTransform == null) && m_ResetTransformOnDisable)
		{
			m_actorTransform.localPosition = m_OrgPosition;
			m_actorTransform.localRotation = m_OrgRotation;
			m_actorTransform.localScale = m_OrgScale;
		}
	}

	private void LateUpdate()
	{
		if (!(m_actorTransform == null))
		{
			if (m_SnapPostion)
			{
				m_actorTransform.position = base.transform.position;
			}
			if (m_SnapRotation)
			{
				m_actorTransform.rotation = base.transform.rotation;
			}
			if (m_SnapScale)
			{
				TransformUtil.SetWorldScale(m_actorTransform, base.transform.lossyScale);
			}
		}
	}
}
