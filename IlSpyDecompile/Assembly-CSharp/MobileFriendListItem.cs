using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MobileFriendListItem : MonoBehaviour, ISelectableTouchListItem, ITouchListItem
{
	[Flags]
	public enum TypeFlags
	{
		FoundFiresideGathering = 0x100,
		Request = 0x80,
		CurrentFiresideGathering = 0x40,
		FiresideGatheringPlayer = 0x20,
		FiresideGatheringFooter = 0x10,
		NearbyPlayer = 0x8,
		CurrentGame = 0x4,
		Friend = 0x2,
		Header = 0x1
	}

	private Bounds m_localBounds;

	private ITouchListItem m_parent;

	private GameObject m_showObject;

	public TypeFlags Type { get; set; }

	public Bounds LocalBounds => m_localBounds;

	public bool Selectable
	{
		get
		{
			if (Type != TypeFlags.Friend)
			{
				return Type == TypeFlags.NearbyPlayer;
			}
			return true;
		}
	}

	public bool IsHeader => ItemIsHeader(Type);

	public bool Visible
	{
		get
		{
			if (m_parent != null)
			{
				return m_parent.Visible;
			}
			return true;
		}
		set
		{
			if (!(m_showObject == null) && value != m_showObject.activeSelf)
			{
				m_showObject.SetActive(value);
			}
		}
	}

	public event Action OnScrollOutOfViewEvent;

	public void SetParent(ITouchListItem parent)
	{
		m_parent = parent;
	}

	public void SetShowObject(GameObject showobj)
	{
		m_showObject = showobj;
	}

	public static bool ItemIsHeader(TypeFlags typeFlags)
	{
		return (typeFlags & TypeFlags.Header) != 0;
	}

	private void Awake()
	{
		Transform parent = base.transform.parent;
		TransformProps transformProps = new TransformProps();
		TransformUtil.CopyWorld(transformProps, base.transform);
		base.transform.parent = null;
		TransformUtil.Identity(base.transform);
		m_localBounds = ComputeWorldBounds();
		base.transform.parent = parent;
		TransformUtil.CopyWorld(base.transform, transformProps);
	}

	public bool IsSelected()
	{
		FriendListUIElement component = GetComponent<FriendListUIElement>();
		if (component != null)
		{
			return component.IsSelected();
		}
		return false;
	}

	public void Selected()
	{
		FriendListUIElement component = GetComponent<FriendListUIElement>();
		if (component != null)
		{
			component.SetSelected(enable: true);
		}
	}

	public void Unselected()
	{
		FriendListUIElement component = GetComponent<FriendListUIElement>();
		if (component != null)
		{
			component.SetSelected(enable: false);
		}
	}

	public Bounds ComputeWorldBounds()
	{
		return TransformUtil.ComputeSetPointBounds(base.gameObject);
	}

	public new T GetComponent<T>() where T : Component
	{
		return ((Component)this).GetComponent<T>();
	}

	public void OnScrollOutOfView()
	{
		if (this.OnScrollOutOfViewEvent != null)
		{
			this.OnScrollOutOfViewEvent();
		}
	}

	[SpecialName]
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	[SpecialName]
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}
}
