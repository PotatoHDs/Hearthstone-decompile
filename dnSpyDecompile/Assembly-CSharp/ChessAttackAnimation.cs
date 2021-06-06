using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
[CustomEditClass]
public class ChessAttackAnimation : Spell
{
	// Token: 0x06006E2F RID: 28207 RVA: 0x002384B1 File Offset: 0x002366B1
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.AttackAnimation());
	}

	// Token: 0x06006E30 RID: 28208 RVA: 0x002384C7 File Offset: 0x002366C7
	private void Finish()
	{
		this.OnSpellFinished();
		this.OnStateFinished();
	}

	// Token: 0x06006E31 RID: 28209 RVA: 0x002384D5 File Offset: 0x002366D5
	private IEnumerator AttackAnimation()
	{
		if (this.m_targets.Count == 0)
		{
			this.Finish();
			yield break;
		}
		string tweenLabel = ZoneMgr.Get().GetTweenName<ZonePlay>();
		while (iTween.CountByName(base.GetSourceCard().gameObject, tweenLabel) > 0)
		{
			yield return null;
		}
		GameObject gameObject = base.GetSourceCard().gameObject;
		Vector3 position = gameObject.transform.position;
		Vector3 eulerAngles = gameObject.transform.eulerAngles;
		Vector3 localScale = gameObject.transform.localScale;
		GameObject gameObject2 = this.m_targets[0].gameObject;
		Vector3 position2 = gameObject2.transform.position;
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.m_ChessSettleDust);
		Vector3 vector = new Vector3(gameObject.transform.localScale.x * 1.2f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z * 1.2f);
		float num = (gameObject.transform.position.z > gameObject2.transform.position.z) ? UnityEngine.Random.Range(0.65f, 0.85f) : UnityEngine.Random.Range(-0.65f, -0.85f);
		float num2 = (gameObject.transform.position.z > gameObject2.transform.position.z) ? -0.1f : 0.1f;
		Vector3 vector2 = (position.z > position2.z) ? new Vector3(eulerAngles.x - 15f, eulerAngles.y, eulerAngles.z) : new Vector3(eulerAngles.x + 15f, eulerAngles.y, eulerAngles.z);
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(position.x, position.y + 1f, position.z + num),
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			0.15f
		}));
		iTween.ScaleTo(gameObject, iTween.Hash(new object[]
		{
			"scale",
			vector,
			"time",
			0.2f
		}));
		base.StartCoroutine(this.DoSpellFinished());
		iTween.RotateTo(gameObject, iTween.Hash(new object[]
		{
			"rotation",
			vector2,
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			0.1f,
			"delay",
			0.15f
		}));
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(position.x, position.y + 0.1f, position.z + num2),
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			0.05f,
			"delay",
			0.25f
		}));
		iTween.RotateTo(gameObject, iTween.Hash(new object[]
		{
			"rotation",
			eulerAngles,
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			0.05f,
			"delay",
			0.25f
		}));
		iTween.ScaleTo(gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.05f,
			"delay",
			0.25f
		}));
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			position,
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			0.3f,
			"delay",
			0.25
		}));
		gameObject3.transform.parent = base.transform;
		gameObject3.transform.position = new Vector3(position.x, position.y + 1f, position.z);
		gameObject3.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(this.m_ImpactEffectDelay);
		if (!string.IsNullOrEmpty(this.m_ShowAttackSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_ShowAttackSoundPrefab);
		}
		yield return new WaitForSeconds(0.3f);
		yield break;
	}

	// Token: 0x06006E32 RID: 28210 RVA: 0x002384E4 File Offset: 0x002366E4
	private IEnumerator PlayImpactEffects()
	{
		if (this.m_targets.Count == 0)
		{
			this.Finish();
			yield break;
		}
		GameObject gameObject = base.GetSourceCard().gameObject;
		GameObject gameObject2 = this.m_targets[0].gameObject;
		Vector3 position = gameObject2.transform.position;
		Vector3 eulerAngles = gameObject2.transform.eulerAngles;
		GameObject gameObject3 = (gameObject.transform.position.z > gameObject2.transform.position.z) ? UnityEngine.Object.Instantiate<GameObject>(this.m_ChessShockwaveRed) : UnityEngine.Object.Instantiate<GameObject>(this.m_ChessShockwaveBlue);
		ParticleSystem.MainModule main = gameObject3.GetComponent<ParticleSystem>().main;
		GameObject gameObject4 = (gameObject.transform.position.z > gameObject2.transform.position.z) ? UnityEngine.Object.Instantiate<GameObject>(this.m_ChessImpactBlue) : UnityEngine.Object.Instantiate<GameObject>(this.m_ChessImpactRed);
		ParticleSystem.MainModule main2 = gameObject4.GetComponent<ParticleSystem>().main;
		float num = (gameObject.transform.position.z > gameObject2.transform.position.z) ? 0.25f : -0.25f;
		float num2 = 0.15f;
		bool flag = this.m_targets.Count == 1 && (gameObject2.transform.position.z < -7f || gameObject2.transform.position.z > -2f);
		float constant;
		if (this.m_targets.Count == 2 && gameObject.transform.position.z > gameObject2.transform.position.z)
		{
			constant = 0f;
		}
		else if (this.m_targets.Count == 1 && gameObject.transform.position.z > gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) < -0.5f)
		{
			constant = 0.523599f;
		}
		else if (this.m_targets.Count == 1 && gameObject.transform.position.z > gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) > 0.5f)
		{
			constant = -0.523599f;
		}
		else if (this.m_targets.Count == 1 && gameObject.transform.position.z < gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) < -0.5f)
		{
			constant = 2.61799f;
		}
		else if (this.m_targets.Count == 1 && gameObject.transform.position.z < gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) > 0.5f)
		{
			constant = 3.66519f;
		}
		else if (this.m_targets.Count == 1 && gameObject.transform.position.z > gameObject2.transform.position.z)
		{
			constant = 0f;
		}
		else
		{
			constant = 3.14159f;
		}
		gameObject3.transform.parent = base.transform;
		gameObject4.transform.parent = base.transform;
		gameObject3.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z - num);
		main.startRotation = constant;
		if (this.m_targets.Count == 2)
		{
			main.startSize = 4f;
			iTween.MoveTo(gameObject3, iTween.Hash(new object[]
			{
				"position",
				new Vector3(gameObject.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z + num),
				"time",
				0.4f
			}));
			gameObject3.GetComponent<ParticleSystem>().Play();
		}
		else if (flag)
		{
			GameObject gameObject5 = (gameObject.transform.position.z > gameObject2.transform.position.z) ? UnityEngine.Object.Instantiate<GameObject>(this.m_ChessTrailRed) : UnityEngine.Object.Instantiate<GameObject>(this.m_ChessTrailBlue);
			gameObject5.transform.parent = base.transform;
			gameObject5.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
			num2 = 0.5f;
			float x = (gameObject.transform.position.x + gameObject2.transform.position.x) * 0.5f;
			float z = (gameObject2.transform.position.z > -4f) ? -2.4f : -6.4f;
			if (gameObject.transform.position.x + gameObject2.transform.position.x < -17.5f || gameObject.transform.position.x + gameObject2.transform.position.x > -12.5f)
			{
				iTween.MoveTo(gameObject5, iTween.Hash(new object[]
				{
					"path",
					new Vector3[]
					{
						new Vector3(x, gameObject2.transform.position.y + 2f, z),
						new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.1f, gameObject2.transform.position.z)
					},
					"easetype",
					iTween.EaseType.easeOutQuad,
					"time",
					0.4f
				}));
			}
			else
			{
				num2 = 0.4f;
				iTween.MoveTo(gameObject5, iTween.Hash(new object[]
				{
					"position",
					new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z),
					"time",
					0.3f
				}));
			}
		}
		else
		{
			iTween.MoveTo(gameObject3, iTween.Hash(new object[]
			{
				"position",
				new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z + num),
				"time",
				0.4f
			}));
			gameObject3.GetComponent<ParticleSystem>().Play();
		}
		gameObject4.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 1f, gameObject2.transform.position.z);
		main2.startDelay = num2;
		gameObject4.GetComponent<ParticleSystem>().Play();
		if (!flag)
		{
			this.ShakeMinion(gameObject2, position, eulerAngles);
		}
		if (this.m_targets.Count == 2)
		{
			GameObject gameObject6 = this.m_targets[1].gameObject;
			Vector3 position2 = gameObject6.transform.position;
			Vector3 eulerAngles2 = gameObject6.transform.eulerAngles;
			GameObject gameObject7 = (gameObject.transform.position.z > gameObject6.transform.position.z) ? UnityEngine.Object.Instantiate<GameObject>(this.m_ChessImpactBlue) : UnityEngine.Object.Instantiate<GameObject>(this.m_ChessImpactRed);
			gameObject7.transform.parent = base.transform;
			gameObject7.transform.position = new Vector3(gameObject6.transform.position.x, gameObject6.transform.position.y + 1f, gameObject6.transform.position.z);
			gameObject7.GetComponent<ParticleSystem>().Play();
			this.ShakeMinion(gameObject6, position2, eulerAngles2);
		}
		yield return new WaitForSeconds(num2);
		if (!string.IsNullOrEmpty(this.m_ShowImpactSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_ShowImpactSoundPrefab);
		}
		yield break;
	}

	// Token: 0x06006E33 RID: 28211 RVA: 0x002384F4 File Offset: 0x002366F4
	private void ShakeMinion(GameObject target, Vector3 targetOrgPos, Vector3 targetOrgRot)
	{
		iTween.MoveTo(target, iTween.Hash(new object[]
		{
			"position",
			new Vector3(targetOrgPos.x, targetOrgPos.y + 0.15f, targetOrgPos.z),
			"time",
			0.05f,
			"islocal",
			true
		}));
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f)),
			"time",
			0.08f,
			"islocal",
			true
		}));
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)),
			"time",
			0.08f,
			"islocal",
			true,
			"delay",
			0.08f
		}));
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f)),
			"time",
			0.08f,
			"islocal",
			true,
			"delay",
			0.16f
		}));
		iTween.MoveTo(target, iTween.Hash(new object[]
		{
			"position",
			targetOrgPos,
			"time",
			0.08f,
			"islocal",
			true,
			"delay",
			0.24f
		}));
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			targetOrgRot,
			"time",
			0.08f,
			"islocal",
			true,
			"delay",
			0.24f
		}));
	}

	// Token: 0x06006E34 RID: 28212 RVA: 0x002387B4 File Offset: 0x002369B4
	private IEnumerator DoSpellFinished()
	{
		if (this.m_targets.Count == 0)
		{
			this.Finish();
			yield break;
		}
		GameObject source = base.GetSourceCard().gameObject;
		GameObject gameObject = this.m_targets[0].gameObject;
		bool useSpellFinishDelay = false;
		if (this.m_targets.Count == 1 && (gameObject.transform.position.z < -7f || gameObject.transform.position.z > -2f) && (source.transform.position.x + gameObject.transform.position.x < -17.5f || source.transform.position.x + gameObject.transform.position.x > -12.5f))
		{
			useSpellFinishDelay = true;
		}
		yield return new WaitForSeconds(this.m_ImpactEffectDelay);
		base.StartCoroutine(this.PlayImpactEffects());
		if (useSpellFinishDelay)
		{
			yield return new WaitForSeconds(this.m_SpellFinishDelay);
		}
		foreach (GameObject gameObject2 in this.m_targets)
		{
			GameUtils.DoDamageTasks(this.m_taskList, base.GetSourceCard(), gameObject2.GetComponentInChildren<Card>());
		}
		foreach (GameObject target in this.m_targets)
		{
			while (iTween.HasTween(target))
			{
				yield return null;
			}
			target = null;
		}
		List<GameObject>.Enumerator enumerator2 = default(List<GameObject>.Enumerator);
		while (iTween.HasTween(source))
		{
			yield return null;
		}
		this.OnSpellFinished();
		yield return new WaitForSeconds(1f);
		this.OnStateFinished();
		yield break;
		yield break;
	}

	// Token: 0x0400585E RID: 22622
	public GameObject m_ChessShockwaveRed;

	// Token: 0x0400585F RID: 22623
	public GameObject m_ChessShockwaveBlue;

	// Token: 0x04005860 RID: 22624
	public GameObject m_ChessTrailRed;

	// Token: 0x04005861 RID: 22625
	public GameObject m_ChessTrailBlue;

	// Token: 0x04005862 RID: 22626
	public GameObject m_ChessImpactRed;

	// Token: 0x04005863 RID: 22627
	public GameObject m_ChessImpactBlue;

	// Token: 0x04005864 RID: 22628
	public GameObject m_ChessSettleDust;

	// Token: 0x04005865 RID: 22629
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowAttackSoundPrefab;

	// Token: 0x04005866 RID: 22630
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowImpactSoundPrefab;

	// Token: 0x04005867 RID: 22631
	public float m_ImpactEffectDelay = 0.3f;

	// Token: 0x04005868 RID: 22632
	public float m_SpellFinishDelay = 0.15f;
}
