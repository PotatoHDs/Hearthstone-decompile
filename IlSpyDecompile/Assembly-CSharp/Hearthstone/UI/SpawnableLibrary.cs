using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	[CreateAssetMenu(fileName = "Spawnable Library", menuName = "UI Framework/Spawnable Library")]
	public class SpawnableLibrary : ScriptableObject
	{
		public enum ItemType
		{
			Texture,
			Widget
		}

		[Serializable]
		public class ItemData
		{
			[SerializeField]
			private int m_id = -1;

			[SerializeField]
			private string m_name = string.Empty;

			[SerializeField]
			private ItemType m_itemType;

			[SerializeField]
			private string m_reference = string.Empty;

			public int ID => m_id;

			public string Name => m_name;

			public ItemType ItemType => m_itemType;

			public string Reference => m_reference;
		}

		[SerializeField]
		private string m_baseMaterial;

		[SerializeField]
		private List<ItemData> m_itemData = new List<ItemData>();

		public string BaseMaterial => m_baseMaterial;

		public ItemData GetItemDataByID(int id)
		{
			for (int i = 0; i < m_itemData.Count; i++)
			{
				ItemData itemData = m_itemData[i];
				if (itemData.ID == id)
				{
					return itemData;
				}
			}
			return null;
		}

		public ItemData GetItemDataByName(string name)
		{
			for (int i = 0; i < m_itemData.Count; i++)
			{
				ItemData itemData = m_itemData[i];
				if (itemData.Name == name)
				{
					return itemData;
				}
			}
			return null;
		}
	}
}
