using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class ElectroScript : MonoBehaviour
{
	// Token: 0x060069C9 RID: 27081 RVA: 0x002271D0 File Offset: 0x002253D0
	private void Start()
	{
		this.srcTrgDist = 0f;
		this.srcDstDist = 0f;
		this.numVertices = 0;
		this.deltaV1 = this.prefabs.destination.position - this.prefabs.source.position;
		this.lastUpdate = 0f;
		this.branches = new Hashtable();
	}

	// Token: 0x060069CA RID: 27082 RVA: 0x0022723C File Offset: 0x0022543C
	private void Update()
	{
		this.srcTrgDist = Vector3.Distance(this.prefabs.source.position, this.prefabs.target.position);
		this.srcDstDist = Vector3.Distance(this.prefabs.source.position, this.prefabs.destination.position);
		if (this.branches.Count > 0)
		{
			foreach (object obj in ((Hashtable)this.branches.Clone()).Keys)
			{
				int num = (int)obj;
				LineRenderer lineRenderer = (LineRenderer)this.branches[num];
				if (lineRenderer.GetComponent<BranchScript>().timeSpawned + this.timers.branchLife < Time.time)
				{
					this.branches.Remove(num);
					UnityEngine.Object.Destroy(lineRenderer.gameObject);
				}
			}
		}
		if (this.prefabs.target.localPosition != this.prefabs.destination.localPosition)
		{
			if (Vector3.Distance(Vector3.zero, this.deltaV1) * Time.deltaTime * (1f / this.timers.timeToPowerUp) > Vector3.Distance(this.prefabs.target.position, this.prefabs.destination.position))
			{
				this.prefabs.target.position = this.prefabs.destination.position;
			}
			else
			{
				this.prefabs.target.Translate(this.deltaV1 * Time.deltaTime * (1f / this.timers.timeToPowerUp));
			}
		}
		if (Time.time - this.lastUpdate < this.timers.timeToUpdate)
		{
			return;
		}
		this.lastUpdate = Time.time;
		this.AnimateArc();
		this.DrawArc();
		this.RayCast();
	}

	// Token: 0x060069CB RID: 27083 RVA: 0x00227460 File Offset: 0x00225660
	private void DrawArc()
	{
		this.numVertices = Mathf.RoundToInt(this.srcTrgDist / this.lines.keyVertexDist) * (1 + this.lines.numInterpoles) + 1;
		this.deltaV2 = (this.prefabs.target.localPosition - this.prefabs.source.localPosition) / (float)this.numVertices;
		Vector3 vector = this.prefabs.source.localPosition;
		Vector3[] array = new Vector3[this.numVertices];
		this.prefabs.lightning.positionCount = this.numVertices;
		for (int i = 0; i < this.numVertices; i++)
		{
			Vector3 vector2 = vector;
			vector2.y += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
			vector2.z += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
			this.prefabs.lightning.SetPosition(i, vector2);
			array[i] = vector2;
			if (!this.branches.ContainsKey(i))
			{
				if (UnityEngine.Random.value < this.dynamics.chanceToArc)
				{
					LineRenderer lineRenderer = UnityEngine.Object.Instantiate<LineRenderer>(this.prefabs.branch, Vector3.zero, Quaternion.identity);
					lineRenderer.GetComponent<BranchScript>().timeSpawned = Time.time;
					lineRenderer.transform.parent = this.prefabs.lightning.transform;
					this.branches.Add(i, lineRenderer);
					lineRenderer.transform.position = this.prefabs.lightning.transform.TransformPoint(vector2);
					vector2.x = UnityEngine.Random.value - 0.5f;
					vector2.y = (UnityEngine.Random.value - 0.5f) * 2f;
					vector2.z = (UnityEngine.Random.value - 0.5f) * 2f;
					lineRenderer.transform.LookAt(lineRenderer.transform.TransformPoint(vector2));
					lineRenderer.transform.Find("stop").localPosition = lineRenderer.transform.Find("start").localPosition + new Vector3(0f, 0f, UnityEngine.Random.Range(this.lines.minBranchLength, this.lines.maxBranchLength));
					int num = Mathf.RoundToInt(Vector3.Distance(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position) / this.lines.keyVertexDist) * (1 + this.lines.numInterpoles) + 1;
					Vector3 vector3 = (lineRenderer.transform.Find("stop").localPosition - lineRenderer.transform.Find("start").localPosition) / (float)num;
					Vector3 vector4 = lineRenderer.transform.Find("start").localPosition;
					Vector3[] array2 = new Vector3[num];
					lineRenderer.positionCount = num;
					for (int j = 0; j < num; j++)
					{
						Vector3 vector5 = vector4;
						vector5.x += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
						vector5.y += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
						lineRenderer.SetPosition(j, vector5);
						array2[j] = vector5;
						vector4 += vector3 * (float)(this.lines.numInterpoles + 1);
						j += this.lines.numInterpoles;
					}
					lineRenderer.SetPosition(0, lineRenderer.transform.Find("start").localPosition);
					lineRenderer.SetPosition(num - 1, lineRenderer.transform.Find("stop").localPosition);
					for (int k = 0; k < num; k++)
					{
						if (k % (this.lines.numInterpoles + 1) != 0)
						{
							Vector3 vector6 = array2[k - 1];
							Vector3 vector7 = array2[k + this.lines.numInterpoles];
							float num2 = Vector3.Distance(vector6, vector7) / (float)(this.lines.numInterpoles + 1) / Vector3.Distance(vector6, vector7) * 3.1415927f;
							for (int l = 0; l < this.lines.numInterpoles; l++)
							{
								Vector3 position;
								position.x = vector6.x + vector3.x * (float)(1 + l);
								position.y = vector6.y + (Mathf.Sin(num2 - 1.5707964f) / 2f + 0.5f) * (vector7.y - vector6.y);
								position.z = vector6.z + (Mathf.Sin(num2 - 1.5707964f) / 2f + 0.5f) * (vector7.z - vector6.z);
								lineRenderer.SetPosition(k + l, position);
								num2 += num2;
							}
							k += this.lines.numInterpoles;
						}
					}
				}
			}
			else
			{
				LineRenderer lineRenderer2 = (LineRenderer)this.branches[i];
				int num3 = Mathf.RoundToInt(Vector3.Distance(lineRenderer2.transform.Find("start").position, lineRenderer2.transform.Find("stop").position) / this.lines.keyVertexDist) * (1 + this.lines.numInterpoles) + 1;
				Vector3 vector8 = (lineRenderer2.transform.Find("stop").localPosition - lineRenderer2.transform.Find("start").localPosition) / (float)num3;
				Vector3 vector9 = lineRenderer2.transform.Find("start").localPosition;
				Vector3[] array3 = new Vector3[num3];
				lineRenderer2.positionCount = num3;
				lineRenderer2.SetPosition(0, lineRenderer2.transform.Find("start").localPosition);
				for (int m = 0; m < num3; m++)
				{
					Vector3 vector10 = vector9;
					vector10.x += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
					vector10.y += (UnityEngine.Random.value * 2f - 1f) * this.lines.keyVertexRange;
					lineRenderer2.SetPosition(m, vector10);
					array3[m] = vector10;
					vector9 += vector8 * (float)(this.lines.numInterpoles + 1);
					m += this.lines.numInterpoles;
				}
				lineRenderer2.SetPosition(0, lineRenderer2.transform.Find("start").localPosition);
				lineRenderer2.SetPosition(num3 - 1, lineRenderer2.transform.Find("stop").localPosition);
				for (int n = 0; n < num3; n++)
				{
					if (n % (this.lines.numInterpoles + 1) != 0)
					{
						Vector3 vector11 = array3[n - 1];
						Vector3 vector12 = array3[n + this.lines.numInterpoles];
						float num4 = Vector3.Distance(vector11, vector12) / (float)(this.lines.numInterpoles + 1) / Vector3.Distance(vector11, vector12) * 3.1415927f;
						for (int num5 = 0; num5 < this.lines.numInterpoles; num5++)
						{
							Vector3 position2;
							position2.x = vector11.x + vector8.x * (float)(1 + num5);
							position2.y = vector11.y + (Mathf.Sin(num4 - 1.5707964f) / 2f + 0.5f) * (vector12.y - vector11.y);
							position2.z = vector11.z + (Mathf.Sin(num4 - 1.5707964f) / 2f + 0.5f) * (vector12.z - vector11.z);
							lineRenderer2.SetPosition(n + num5, position2);
							num4 += num4;
						}
						n += this.lines.numInterpoles;
					}
				}
			}
			vector += this.deltaV2 * (float)(this.lines.numInterpoles + 1);
			i += this.lines.numInterpoles;
		}
		this.prefabs.lightning.SetPosition(0, this.prefabs.source.localPosition);
		this.prefabs.lightning.SetPosition(this.numVertices - 1, this.prefabs.target.localPosition);
		for (int num6 = 0; num6 < this.numVertices; num6++)
		{
			if (num6 % (this.lines.numInterpoles + 1) != 0)
			{
				Vector3 vector13 = array[num6 - 1];
				Vector3 vector14 = array[num6 + this.lines.numInterpoles];
				float num7 = Vector3.Distance(vector13, vector14) / (float)(this.lines.numInterpoles + 1) / Vector3.Distance(vector13, vector14) * 3.1415927f;
				for (int num8 = 0; num8 < this.lines.numInterpoles; num8++)
				{
					Vector3 position3;
					position3.x = vector13.x + this.deltaV2.x * (float)(1 + num8);
					position3.y = vector13.y + (Mathf.Sin(num7 - 1.5707964f) / 2f + 0.5f) * (vector14.y - vector13.y);
					position3.z = vector13.z + (Mathf.Sin(num7 - 1.5707964f) / 2f + 0.5f) * (vector14.z - vector13.z);
					this.prefabs.lightning.SetPosition(num6 + num8, position3);
					num7 += num7;
				}
				num6 += this.lines.numInterpoles;
			}
		}
	}

	// Token: 0x060069CC RID: 27084 RVA: 0x00227ED8 File Offset: 0x002260D8
	private void AnimateArc()
	{
		Material material = this.prefabs.lightning.GetComponent<Renderer>().GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureOffset.x += Time.deltaTime * this.tex.animateSpeed;
		mainTextureOffset.y = this.tex.offsetY;
		mainTextureScale.x = this.srcTrgDist / this.srcDstDist * this.tex.scaleX;
		mainTextureScale.y = this.tex.scaleY;
		material.mainTextureOffset = mainTextureOffset;
		material.mainTextureScale = mainTextureScale;
	}

	// Token: 0x060069CD RID: 27085 RVA: 0x00227F78 File Offset: 0x00226178
	private void RayCast()
	{
		foreach (RaycastHit raycastHit in Physics.RaycastAll(this.prefabs.source.position, this.prefabs.target.position - this.prefabs.source.position, Vector3.Distance(this.prefabs.source.position, this.prefabs.target.position)))
		{
			UnityEngine.Object.Instantiate<Transform>(this.prefabs.sparks, raycastHit.point, Quaternion.identity);
		}
		if (this.branches.Count > 0)
		{
			foreach (object obj in ((Hashtable)this.branches.Clone()).Keys)
			{
				int num = (int)obj;
				LineRenderer lineRenderer = (LineRenderer)this.branches[num];
				foreach (RaycastHit raycastHit2 in Physics.RaycastAll(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position - lineRenderer.transform.Find("start").position, Vector3.Distance(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position)))
				{
					UnityEngine.Object.Instantiate<Transform>(this.prefabs.sparks, raycastHit2.point, Quaternion.identity);
				}
			}
		}
	}

	// Token: 0x04005688 RID: 22152
	public ElectroScript.Prefabs prefabs;

	// Token: 0x04005689 RID: 22153
	public ElectroScript.Timers timers;

	// Token: 0x0400568A RID: 22154
	public ElectroScript.Dynamics dynamics;

	// Token: 0x0400568B RID: 22155
	public ElectroScript.LineSettings lines;

	// Token: 0x0400568C RID: 22156
	public ElectroScript.TextureSettings tex;

	// Token: 0x0400568D RID: 22157
	private int numVertices;

	// Token: 0x0400568E RID: 22158
	private Vector3 deltaV1;

	// Token: 0x0400568F RID: 22159
	private Vector3 deltaV2;

	// Token: 0x04005690 RID: 22160
	private float srcTrgDist;

	// Token: 0x04005691 RID: 22161
	private float srcDstDist;

	// Token: 0x04005692 RID: 22162
	private float lastUpdate;

	// Token: 0x04005693 RID: 22163
	private Hashtable branches;

	// Token: 0x0200232F RID: 9007
	[Serializable]
	public class Prefabs
	{
		// Token: 0x0400E5F3 RID: 58867
		public LineRenderer lightning;

		// Token: 0x0400E5F4 RID: 58868
		public LineRenderer branch;

		// Token: 0x0400E5F5 RID: 58869
		public Transform sparks;

		// Token: 0x0400E5F6 RID: 58870
		public Transform source;

		// Token: 0x0400E5F7 RID: 58871
		public Transform destination;

		// Token: 0x0400E5F8 RID: 58872
		public Transform target;
	}

	// Token: 0x02002330 RID: 9008
	[Serializable]
	public class Timers
	{
		// Token: 0x0400E5F9 RID: 58873
		public float timeToUpdate = 0.05f;

		// Token: 0x0400E5FA RID: 58874
		public float timeToPowerUp = 0.5f;

		// Token: 0x0400E5FB RID: 58875
		public float branchLife = 0.1f;
	}

	// Token: 0x02002331 RID: 9009
	[Serializable]
	public class Dynamics
	{
		// Token: 0x0400E5FC RID: 58876
		public float chanceToArc = 0.2f;
	}

	// Token: 0x02002332 RID: 9010
	[Serializable]
	public class LineSettings
	{
		// Token: 0x0400E5FD RID: 58877
		public float keyVertexDist = 3f;

		// Token: 0x0400E5FE RID: 58878
		public float keyVertexRange = 4f;

		// Token: 0x0400E5FF RID: 58879
		public int numInterpoles = 5;

		// Token: 0x0400E600 RID: 58880
		public float minBranchLength = 11f;

		// Token: 0x0400E601 RID: 58881
		public float maxBranchLength = 16f;
	}

	// Token: 0x02002333 RID: 9011
	[Serializable]
	public class TextureSettings
	{
		// Token: 0x0400E602 RID: 58882
		public float scaleX;

		// Token: 0x0400E603 RID: 58883
		public float scaleY;

		// Token: 0x0400E604 RID: 58884
		public float animateSpeed;

		// Token: 0x0400E605 RID: 58885
		public float offsetY;
	}
}
