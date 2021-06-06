using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000826 RID: 2086
public class SpiralMovement : MonoBehaviour
{
	// Token: 0x0600702A RID: 28714 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x0600702B RID: 28715 RVA: 0x00242C18 File Offset: 0x00240E18
	public void ResetAnim()
	{
		this.partsCount = (float)this.parts.Length;
		if (this.partsCount == 0f)
		{
			return;
		}
		this.partMaterial = this.parts[0].GetComponent<MeshRenderer>().GetSharedMaterial();
		if (this.partsP != null)
		{
			this.partsP.Clear();
		}
		else
		{
			this.partsP = new List<SpiralMovement.Part>();
		}
		int num = 0;
		while ((float)num < this.partsCount)
		{
			float delay = (float)num / this.partsCount * this.partSpawnLifetime;
			float spawnPointRot = Mathf.Floor(UnityEngine.Random.Range(0f, this.partSpawnPointCount - float.Epsilon)) * 3.1415927f * 2f / this.partSpawnPointCount;
			SpiralMovement.Part item = new SpiralMovement.Part(this.parts[num], this.partFlytime.Evaluate((float)num / this.partsCount) * this.partFlytimeMul, this.partSpawnPointRotSpeed, spawnPointRot, this.partSpawnPointOffset, this.spawnY, this.partMaterial, delay, this.magnetTimeRemap, this.partRotTimeRemap, this.partScaleAnimation);
			this.partsP.Add(item);
			num++;
		}
		this.time = 0f;
		this.state = SpiralMovement.States.UPDATE;
	}

	// Token: 0x0600702C RID: 28716 RVA: 0x00242D48 File Offset: 0x00240F48
	private void Update()
	{
		if (this.state == SpiralMovement.States.HIDE)
		{
			return;
		}
		if (this.partsP == null)
		{
			return;
		}
		foreach (SpiralMovement.Part part in this.partsP)
		{
			part.Update(Time.deltaTime);
		}
		if (this.state == SpiralMovement.States.UPDATE)
		{
			if (this.time >= this.partsHideTime)
			{
				this.state = SpiralMovement.States.HIDE;
				foreach (SpiralMovement.Part part2 in this.partsP)
				{
					part2.Hide();
				}
				return;
			}
			foreach (SpiralMovement.MaterialParam materialParam in this.materialParams)
			{
				this.partMaterial.SetFloat(materialParam.paramName, materialParam.curveAnimation.Evaluate(this.time * materialParam.timeMul) * materialParam.valueMul);
			}
		}
		this.time += Time.deltaTime;
	}

	// Token: 0x040059E9 RID: 23017
	public GameObject[] parts;

	// Token: 0x040059EA RID: 23018
	public float partFlytimeMul = 1f;

	// Token: 0x040059EB RID: 23019
	public AnimationCurve partFlytime;

	// Token: 0x040059EC RID: 23020
	public float partSpawnPointCount = 3f;

	// Token: 0x040059ED RID: 23021
	public float partSpawnPointOffset = 2f;

	// Token: 0x040059EE RID: 23022
	public float partSpawnLifetime = 0.5f;

	// Token: 0x040059EF RID: 23023
	public float partsHideTime = 2f;

	// Token: 0x040059F0 RID: 23024
	public float partSpawnPointRotSpeed = 40f;

	// Token: 0x040059F1 RID: 23025
	public float spawnY = 1f;

	// Token: 0x040059F2 RID: 23026
	public AnimationCurve magnetTimeRemap;

	// Token: 0x040059F3 RID: 23027
	public AnimationCurve partRotTimeRemap;

	// Token: 0x040059F4 RID: 23028
	public AnimationCurve partScaleAnimation;

	// Token: 0x040059F5 RID: 23029
	[SerializeField]
	public SpiralMovement.MaterialParam[] materialParams;

	// Token: 0x040059F6 RID: 23030
	private float time;

	// Token: 0x040059F7 RID: 23031
	private List<SpiralMovement.Part> partsP;

	// Token: 0x040059F8 RID: 23032
	private float partsCount;

	// Token: 0x040059F9 RID: 23033
	private Material partMaterial;

	// Token: 0x040059FA RID: 23034
	private SpiralMovement.States state;

	// Token: 0x020023FA RID: 9210
	private enum States
	{
		// Token: 0x0400E8CF RID: 59599
		START,
		// Token: 0x0400E8D0 RID: 59600
		UPDATE,
		// Token: 0x0400E8D1 RID: 59601
		HIDE
	}

	// Token: 0x020023FB RID: 9211
	[Serializable]
	public class MaterialParam
	{
		// Token: 0x0400E8D2 RID: 59602
		[SerializeField]
		public string paramName;

		// Token: 0x0400E8D3 RID: 59603
		[SerializeField]
		public float valueMul = 1f;

		// Token: 0x0400E8D4 RID: 59604
		[SerializeField]
		public float timeMul = 1f;

		// Token: 0x0400E8D5 RID: 59605
		[SerializeField]
		public AnimationCurve curveAnimation;
	}

	// Token: 0x020023FC RID: 9212
	private class Part
	{
		// Token: 0x06012DCA RID: 77258 RVA: 0x0051D880 File Offset: 0x0051BA80
		public Part(GameObject _go, float _lifetime, float _spawnPointRotSpeed, float _spawnPointRot, float _partSpawnPointOffset, float _spawnY, Material _sharedMaterial, float _delay, AnimationCurve _magnetTimeRemap, AnimationCurve _rotTimeRemap, AnimationCurve _scaleAnimCurve)
		{
			this.lifetime = _lifetime;
			this.go = _go;
			this.goTransform = this.go.transform;
			this.goMeshRenderer = _go.GetComponent<MeshRenderer>();
			this.endPos = this.goTransform.localPosition;
			this.spawnPointIniRot = _spawnPointRot;
			this.partSpawnPointOffset = _partSpawnPointOffset;
			this.spawnPointRotSpeed = _spawnPointRotSpeed;
			this.goMeshRenderer.SetSharedMaterial(_sharedMaterial);
			this.delay = _delay;
			this.goMeshRenderer.enabled = false;
			this.currentAngle = 0f;
			this.spawnY = _spawnY;
			this.endScale = this.goTransform.localScale;
			this.scaleAnimCurve = _scaleAnimCurve;
			this.magnetTimeRemap = _magnetTimeRemap;
			this.rotTimeRemap = _rotTimeRemap;
		}

		// Token: 0x06012DCB RID: 77259 RVA: 0x0051D950 File Offset: 0x0051BB50
		public void Update(float _deltatime)
		{
			if (this.partState == SpiralMovement.Part.PartStates.END)
			{
				return;
			}
			if (this.partState == SpiralMovement.Part.PartStates.DELAY && this.time > this.delay)
			{
				this.time -= this.delay;
				this.goMeshRenderer.enabled = true;
				this.partState = SpiralMovement.Part.PartStates.UPDATE;
			}
			if (this.partState == SpiralMovement.Part.PartStates.UPDATE)
			{
				if (this.time >= this.lifetime)
				{
					this.partState = SpiralMovement.Part.PartStates.END;
					this.goTransform.localRotation = this.endRot;
					this.goTransform.localPosition = this.endPos;
					this.goTransform.localScale = this.endScale;
					return;
				}
				float num = this.time / this.lifetime;
				float t = this.magnetTimeRemap.Evaluate(num);
				float num2 = this.rotTimeRemap.Evaluate(num);
				this.currentAngle = this.spawnPointRotSpeed * num2 + this.spawnPointIniRot;
				Vector3 vector = new Vector3(Mathf.Cos(this.currentAngle) * this.partSpawnPointOffset, this.spawnY, Mathf.Sin(this.currentAngle) * this.partSpawnPointOffset);
				vector = Vector3.Lerp(vector, this.endPos, t);
				this.goTransform.localPosition = vector;
				this.goTransform.localScale = this.endScale * this.scaleAnimCurve.Evaluate(t);
			}
			this.time += _deltatime;
		}

		// Token: 0x06012DCC RID: 77260 RVA: 0x0051DAB5 File Offset: 0x0051BCB5
		public void Hide()
		{
			this.goMeshRenderer.enabled = false;
			this.partState = SpiralMovement.Part.PartStates.END;
		}

		// Token: 0x0400E8D6 RID: 59606
		private float lifetime;

		// Token: 0x0400E8D7 RID: 59607
		private float time;

		// Token: 0x0400E8D8 RID: 59608
		private Vector3 endPos;

		// Token: 0x0400E8D9 RID: 59609
		private Quaternion endRot = Quaternion.identity;

		// Token: 0x0400E8DA RID: 59610
		private Vector3 endScale;

		// Token: 0x0400E8DB RID: 59611
		private Vector3 pos;

		// Token: 0x0400E8DC RID: 59612
		private Quaternion rot;

		// Token: 0x0400E8DD RID: 59613
		private Vector3 scale;

		// Token: 0x0400E8DE RID: 59614
		private float spawnPointRotSpeed;

		// Token: 0x0400E8DF RID: 59615
		private GameObject go;

		// Token: 0x0400E8E0 RID: 59616
		private Transform goTransform;

		// Token: 0x0400E8E1 RID: 59617
		private MeshRenderer goMeshRenderer;

		// Token: 0x0400E8E2 RID: 59618
		private SpiralMovement.Part.PartStates partState;

		// Token: 0x0400E8E3 RID: 59619
		private float delay;

		// Token: 0x0400E8E4 RID: 59620
		private float spawnPointIniRot;

		// Token: 0x0400E8E5 RID: 59621
		private float partSpawnPointOffset;

		// Token: 0x0400E8E6 RID: 59622
		private float currentAngle;

		// Token: 0x0400E8E7 RID: 59623
		private float spawnY;

		// Token: 0x0400E8E8 RID: 59624
		private AnimationCurve scaleAnimCurve;

		// Token: 0x0400E8E9 RID: 59625
		private AnimationCurve magnetTimeRemap;

		// Token: 0x0400E8EA RID: 59626
		private AnimationCurve rotTimeRemap;

		// Token: 0x020029A4 RID: 10660
		private enum PartStates
		{
			// Token: 0x0400FDEF RID: 65007
			DELAY,
			// Token: 0x0400FDF0 RID: 65008
			UPDATE,
			// Token: 0x0400FDF1 RID: 65009
			END
		}
	}
}
