using System;
using UnityEngine;

[Serializable]
public class CanvasAnchors
{
	public Transform m_Center;

	public Transform m_Left;

	public Transform m_Right;

	public Transform m_Bottom;

	public Transform m_Top;

	public Transform m_BottomLeft;

	public Transform m_BottomRight;

	public Transform m_TopLeft;

	public Transform m_TopRight;

	public Transform GetAnchor(CanvasAnchor type)
	{
		return type switch
		{
			CanvasAnchor.CENTER => m_Center, 
			CanvasAnchor.LEFT => m_Left, 
			CanvasAnchor.RIGHT => m_Right, 
			CanvasAnchor.BOTTOM => m_Bottom, 
			CanvasAnchor.TOP => m_Top, 
			CanvasAnchor.BOTTOM_LEFT => m_BottomLeft, 
			CanvasAnchor.BOTTOM_RIGHT => m_BottomRight, 
			CanvasAnchor.TOP_LEFT => m_TopLeft, 
			CanvasAnchor.TOP_RIGHT => m_TopRight, 
			_ => m_Center, 
		};
	}

	public void WillReset()
	{
		foreach (Transform item in m_Center)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		foreach (Transform item2 in m_Left)
		{
			UnityEngine.Object.Destroy(item2.gameObject);
		}
		foreach (Transform item3 in m_Right)
		{
			UnityEngine.Object.Destroy(item3.gameObject);
		}
		foreach (Transform item4 in m_Bottom)
		{
			UnityEngine.Object.Destroy(item4.gameObject);
		}
		foreach (Transform item5 in m_Top)
		{
			UnityEngine.Object.Destroy(item5.gameObject);
		}
		foreach (Transform item6 in m_BottomLeft)
		{
			UnityEngine.Object.Destroy(item6.gameObject);
		}
		foreach (Transform item7 in m_BottomRight)
		{
			UnityEngine.Object.Destroy(item7.gameObject);
		}
		foreach (Transform item8 in m_TopLeft)
		{
			UnityEngine.Object.Destroy(item8.gameObject);
		}
		foreach (Transform item9 in m_TopRight)
		{
			UnityEngine.Object.Destroy(item9.gameObject);
		}
	}
}
