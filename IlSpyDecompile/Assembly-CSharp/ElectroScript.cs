using System;
using System.Collections;
using UnityEngine;

public class ElectroScript : MonoBehaviour
{
	[Serializable]
	public class Prefabs
	{
		public LineRenderer lightning;

		public LineRenderer branch;

		public Transform sparks;

		public Transform source;

		public Transform destination;

		public Transform target;
	}

	[Serializable]
	public class Timers
	{
		public float timeToUpdate = 0.05f;

		public float timeToPowerUp = 0.5f;

		public float branchLife = 0.1f;
	}

	[Serializable]
	public class Dynamics
	{
		public float chanceToArc = 0.2f;
	}

	[Serializable]
	public class LineSettings
	{
		public float keyVertexDist = 3f;

		public float keyVertexRange = 4f;

		public int numInterpoles = 5;

		public float minBranchLength = 11f;

		public float maxBranchLength = 16f;
	}

	[Serializable]
	public class TextureSettings
	{
		public float scaleX;

		public float scaleY;

		public float animateSpeed;

		public float offsetY;
	}

	public Prefabs prefabs;

	public Timers timers;

	public Dynamics dynamics;

	public LineSettings lines;

	public TextureSettings tex;

	private int numVertices;

	private Vector3 deltaV1;

	private Vector3 deltaV2;

	private float srcTrgDist;

	private float srcDstDist;

	private float lastUpdate;

	private Hashtable branches;

	private void Start()
	{
		srcTrgDist = 0f;
		srcDstDist = 0f;
		numVertices = 0;
		deltaV1 = prefabs.destination.position - prefabs.source.position;
		lastUpdate = 0f;
		branches = new Hashtable();
	}

	private void Update()
	{
		srcTrgDist = Vector3.Distance(prefabs.source.position, prefabs.target.position);
		srcDstDist = Vector3.Distance(prefabs.source.position, prefabs.destination.position);
		if (branches.Count > 0)
		{
			foreach (int key in ((Hashtable)branches.Clone()).Keys)
			{
				LineRenderer lineRenderer = (LineRenderer)branches[key];
				if (lineRenderer.GetComponent<BranchScript>().timeSpawned + timers.branchLife < Time.time)
				{
					branches.Remove(key);
					UnityEngine.Object.Destroy(lineRenderer.gameObject);
				}
			}
		}
		if (prefabs.target.localPosition != prefabs.destination.localPosition)
		{
			if (Vector3.Distance(Vector3.zero, deltaV1) * Time.deltaTime * (1f / timers.timeToPowerUp) > Vector3.Distance(prefabs.target.position, prefabs.destination.position))
			{
				prefabs.target.position = prefabs.destination.position;
			}
			else
			{
				prefabs.target.Translate(deltaV1 * Time.deltaTime * (1f / timers.timeToPowerUp));
			}
		}
		if (!(Time.time - lastUpdate < timers.timeToUpdate))
		{
			lastUpdate = Time.time;
			AnimateArc();
			DrawArc();
			RayCast();
		}
	}

	private void DrawArc()
	{
		numVertices = Mathf.RoundToInt(srcTrgDist / lines.keyVertexDist) * (1 + lines.numInterpoles) + 1;
		deltaV2 = (prefabs.target.localPosition - prefabs.source.localPosition) / numVertices;
		Vector3 localPosition = prefabs.source.localPosition;
		Vector3[] array = new Vector3[numVertices];
		prefabs.lightning.positionCount = numVertices;
		int num;
		Vector3 position = default(Vector3);
		Vector3 position2 = default(Vector3);
		for (num = 0; num < numVertices; num++)
		{
			Vector3 vector = localPosition;
			vector.y += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
			vector.z += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
			prefabs.lightning.SetPosition(num, vector);
			array[num] = vector;
			if (!branches.ContainsKey(num))
			{
				if (UnityEngine.Random.value < dynamics.chanceToArc)
				{
					LineRenderer lineRenderer = UnityEngine.Object.Instantiate(prefabs.branch, Vector3.zero, Quaternion.identity);
					lineRenderer.GetComponent<BranchScript>().timeSpawned = Time.time;
					lineRenderer.transform.parent = prefabs.lightning.transform;
					branches.Add(num, lineRenderer);
					lineRenderer.transform.position = prefabs.lightning.transform.TransformPoint(vector);
					vector.x = UnityEngine.Random.value - 0.5f;
					vector.y = (UnityEngine.Random.value - 0.5f) * 2f;
					vector.z = (UnityEngine.Random.value - 0.5f) * 2f;
					lineRenderer.transform.LookAt(lineRenderer.transform.TransformPoint(vector));
					lineRenderer.transform.Find("stop").localPosition = lineRenderer.transform.Find("start").localPosition + new Vector3(0f, 0f, UnityEngine.Random.Range(lines.minBranchLength, lines.maxBranchLength));
					int num2 = Mathf.RoundToInt(Vector3.Distance(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position) / lines.keyVertexDist) * (1 + lines.numInterpoles) + 1;
					Vector3 vector2 = (lineRenderer.transform.Find("stop").localPosition - lineRenderer.transform.Find("start").localPosition) / num2;
					Vector3 localPosition2 = lineRenderer.transform.Find("start").localPosition;
					Vector3[] array2 = new Vector3[num2];
					lineRenderer.positionCount = num2;
					int num3;
					for (num3 = 0; num3 < num2; num3++)
					{
						Vector3 vector3 = localPosition2;
						vector3.x += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
						vector3.y += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
						lineRenderer.SetPosition(num3, vector3);
						array2[num3] = vector3;
						localPosition2 += vector2 * (lines.numInterpoles + 1);
						num3 += lines.numInterpoles;
					}
					lineRenderer.SetPosition(0, lineRenderer.transform.Find("start").localPosition);
					lineRenderer.SetPosition(num2 - 1, lineRenderer.transform.Find("stop").localPosition);
					for (int i = 0; i < num2; i++)
					{
						if (i % (lines.numInterpoles + 1) != 0)
						{
							Vector3 a = array2[i - 1];
							Vector3 b = array2[i + lines.numInterpoles];
							float num4 = Vector3.Distance(a, b) / (float)(lines.numInterpoles + 1) / Vector3.Distance(a, b) * (float)Math.PI;
							for (int j = 0; j < lines.numInterpoles; j++)
							{
								position.x = a.x + vector2.x * (float)(1 + j);
								position.y = a.y + (Mathf.Sin(num4 - (float)Math.PI / 2f) / 2f + 0.5f) * (b.y - a.y);
								position.z = a.z + (Mathf.Sin(num4 - (float)Math.PI / 2f) / 2f + 0.5f) * (b.z - a.z);
								lineRenderer.SetPosition(i + j, position);
								num4 += num4;
							}
							i += lines.numInterpoles;
						}
					}
				}
			}
			else
			{
				LineRenderer lineRenderer2 = (LineRenderer)branches[num];
				int num5 = Mathf.RoundToInt(Vector3.Distance(lineRenderer2.transform.Find("start").position, lineRenderer2.transform.Find("stop").position) / lines.keyVertexDist) * (1 + lines.numInterpoles) + 1;
				Vector3 vector4 = (lineRenderer2.transform.Find("stop").localPosition - lineRenderer2.transform.Find("start").localPosition) / num5;
				Vector3 localPosition3 = lineRenderer2.transform.Find("start").localPosition;
				Vector3[] array3 = new Vector3[num5];
				lineRenderer2.positionCount = num5;
				lineRenderer2.SetPosition(0, lineRenderer2.transform.Find("start").localPosition);
				int num6;
				for (num6 = 0; num6 < num5; num6++)
				{
					Vector3 vector5 = localPosition3;
					vector5.x += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
					vector5.y += (UnityEngine.Random.value * 2f - 1f) * lines.keyVertexRange;
					lineRenderer2.SetPosition(num6, vector5);
					array3[num6] = vector5;
					localPosition3 += vector4 * (lines.numInterpoles + 1);
					num6 += lines.numInterpoles;
				}
				lineRenderer2.SetPosition(0, lineRenderer2.transform.Find("start").localPosition);
				lineRenderer2.SetPosition(num5 - 1, lineRenderer2.transform.Find("stop").localPosition);
				for (int k = 0; k < num5; k++)
				{
					if (k % (lines.numInterpoles + 1) != 0)
					{
						Vector3 a2 = array3[k - 1];
						Vector3 b2 = array3[k + lines.numInterpoles];
						float num7 = Vector3.Distance(a2, b2) / (float)(lines.numInterpoles + 1) / Vector3.Distance(a2, b2) * (float)Math.PI;
						for (int l = 0; l < lines.numInterpoles; l++)
						{
							position2.x = a2.x + vector4.x * (float)(1 + l);
							position2.y = a2.y + (Mathf.Sin(num7 - (float)Math.PI / 2f) / 2f + 0.5f) * (b2.y - a2.y);
							position2.z = a2.z + (Mathf.Sin(num7 - (float)Math.PI / 2f) / 2f + 0.5f) * (b2.z - a2.z);
							lineRenderer2.SetPosition(k + l, position2);
							num7 += num7;
						}
						k += lines.numInterpoles;
					}
				}
			}
			localPosition += deltaV2 * (lines.numInterpoles + 1);
			num += lines.numInterpoles;
		}
		prefabs.lightning.SetPosition(0, prefabs.source.localPosition);
		prefabs.lightning.SetPosition(numVertices - 1, prefabs.target.localPosition);
		Vector3 position3 = default(Vector3);
		for (int m = 0; m < numVertices; m++)
		{
			if (m % (lines.numInterpoles + 1) != 0)
			{
				Vector3 a3 = array[m - 1];
				Vector3 b3 = array[m + lines.numInterpoles];
				float num8 = Vector3.Distance(a3, b3) / (float)(lines.numInterpoles + 1) / Vector3.Distance(a3, b3) * (float)Math.PI;
				for (int n = 0; n < lines.numInterpoles; n++)
				{
					position3.x = a3.x + deltaV2.x * (float)(1 + n);
					position3.y = a3.y + (Mathf.Sin(num8 - (float)Math.PI / 2f) / 2f + 0.5f) * (b3.y - a3.y);
					position3.z = a3.z + (Mathf.Sin(num8 - (float)Math.PI / 2f) / 2f + 0.5f) * (b3.z - a3.z);
					prefabs.lightning.SetPosition(m + n, position3);
					num8 += num8;
				}
				m += lines.numInterpoles;
			}
		}
	}

	private void AnimateArc()
	{
		Material material = prefabs.lightning.GetComponent<Renderer>().GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureOffset.x += Time.deltaTime * tex.animateSpeed;
		mainTextureOffset.y = tex.offsetY;
		mainTextureScale.x = srcTrgDist / srcDstDist * tex.scaleX;
		mainTextureScale.y = tex.scaleY;
		material.mainTextureOffset = mainTextureOffset;
		material.mainTextureScale = mainTextureScale;
	}

	private void RayCast()
	{
		RaycastHit[] array = Physics.RaycastAll(prefabs.source.position, prefabs.target.position - prefabs.source.position, Vector3.Distance(prefabs.source.position, prefabs.target.position));
		foreach (RaycastHit raycastHit in array)
		{
			UnityEngine.Object.Instantiate(prefabs.sparks, raycastHit.point, Quaternion.identity);
		}
		if (branches.Count <= 0)
		{
			return;
		}
		foreach (int key in ((Hashtable)branches.Clone()).Keys)
		{
			LineRenderer lineRenderer = (LineRenderer)branches[key];
			array = Physics.RaycastAll(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position - lineRenderer.transform.Find("start").position, Vector3.Distance(lineRenderer.transform.Find("start").position, lineRenderer.transform.Find("stop").position));
			foreach (RaycastHit raycastHit2 in array)
			{
				UnityEngine.Object.Instantiate(prefabs.sparks, raycastHit2.point, Quaternion.identity);
			}
		}
	}
}
