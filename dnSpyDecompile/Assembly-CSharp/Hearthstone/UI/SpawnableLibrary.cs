using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FF9 RID: 4089
	[CreateAssetMenu(fileName = "Spawnable Library", menuName = "UI Framework/Spawnable Library")]
	public class SpawnableLibrary : ScriptableObject
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600B1A7 RID: 45479 RVA: 0x0036C755 File Offset: 0x0036A955
		public string BaseMaterial
		{
			get
			{
				return this.m_baseMaterial;
			}
		}

		// Token: 0x0600B1A8 RID: 45480 RVA: 0x0036C760 File Offset: 0x0036A960
		public SpawnableLibrary.ItemData GetItemDataByID(int id)
		{
			for (int i = 0; i < this.m_itemData.Count; i++)
			{
				SpawnableLibrary.ItemData itemData = this.m_itemData[i];
				if (itemData.ID == id)
				{
					return itemData;
				}
			}
			return null;
		}

		// Token: 0x0600B1A9 RID: 45481 RVA: 0x0036C79C File Offset: 0x0036A99C
		public SpawnableLibrary.ItemData GetItemDataByName(string name)
		{
			for (int i = 0; i < this.m_itemData.Count; i++)
			{
				SpawnableLibrary.ItemData itemData = this.m_itemData[i];
				if (itemData.Name == name)
				{
					return itemData;
				}
			}
			return null;
		}

		// Token: 0x040095C1 RID: 38337
		[SerializeField]
		private string m_baseMaterial;

		// Token: 0x040095C2 RID: 38338
		[SerializeField]
		private List<SpawnableLibrary.ItemData> m_itemData = new List<SpawnableLibrary.ItemData>();

		// Token: 0x02002825 RID: 10277
		public enum ItemType
		{
			// Token: 0x0400F89B RID: 63643
			Texture,
			// Token: 0x0400F89C RID: 63644
			Widget
		}

		// Token: 0x02002826 RID: 10278
		[Serializable]
		public class ItemData
		{
			// Token: 0x17002D17 RID: 11543
			// (get) Token: 0x06013B1B RID: 80667 RVA: 0x0053A7C8 File Offset: 0x005389C8
			public int ID
			{
				get
				{
					return this.m_id;
				}
			}

			// Token: 0x17002D18 RID: 11544
			// (get) Token: 0x06013B1C RID: 80668 RVA: 0x0053A7D0 File Offset: 0x005389D0
			public string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17002D19 RID: 11545
			// (get) Token: 0x06013B1D RID: 80669 RVA: 0x0053A7D8 File Offset: 0x005389D8
			public SpawnableLibrary.ItemType ItemType
			{
				get
				{
					return this.m_itemType;
				}
			}

			// Token: 0x17002D1A RID: 11546
			// (get) Token: 0x06013B1E RID: 80670 RVA: 0x0053A7E0 File Offset: 0x005389E0
			public string Reference
			{
				get
				{
					return this.m_reference;
				}
			}

			// Token: 0x0400F89D RID: 63645
			[SerializeField]
			private int m_id = -1;

			// Token: 0x0400F89E RID: 63646
			[SerializeField]
			private string m_name = string.Empty;

			// Token: 0x0400F89F RID: 63647
			[SerializeField]
			private SpawnableLibrary.ItemType m_itemType;

			// Token: 0x0400F8A0 RID: 63648
			[SerializeField]
			private string m_reference = string.Empty;
		}
	}
}
