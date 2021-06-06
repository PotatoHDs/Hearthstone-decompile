using System.Collections;
using UnityEngine;

[CustomEditClass]
public class ChessAttackAnimation : Spell
{
	public GameObject m_ChessShockwaveRed;

	public GameObject m_ChessShockwaveBlue;

	public GameObject m_ChessTrailRed;

	public GameObject m_ChessTrailBlue;

	public GameObject m_ChessImpactRed;

	public GameObject m_ChessImpactBlue;

	public GameObject m_ChessSettleDust;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowAttackSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowImpactSoundPrefab;

	public float m_ImpactEffectDelay = 0.3f;

	public float m_SpellFinishDelay = 0.15f;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(AttackAnimation());
	}

	private void Finish()
	{
		OnSpellFinished();
		OnStateFinished();
	}

	private IEnumerator AttackAnimation()
	{
		if (m_targets.Count == 0)
		{
			Finish();
			yield break;
		}
		string tweenLabel = ZoneMgr.Get().GetTweenName<ZonePlay>();
		while (iTween.CountByName(GetSourceCard().gameObject, tweenLabel) > 0)
		{
			yield return null;
		}
		GameObject gameObject = GetSourceCard().gameObject;
		Vector3 position = gameObject.transform.position;
		Vector3 eulerAngles = gameObject.transform.eulerAngles;
		Vector3 localScale = gameObject.transform.localScale;
		GameObject gameObject2 = m_targets[0].gameObject;
		Vector3 position2 = gameObject2.transform.position;
		GameObject gameObject3 = Object.Instantiate(m_ChessSettleDust);
		Vector3 vector = new Vector3(gameObject.transform.localScale.x * 1.2f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z * 1.2f);
		float num = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? Random.Range(0.65f, 0.85f) : Random.Range(-0.65f, -0.85f));
		float num2 = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? (-0.1f) : 0.1f);
		Vector3 vector2 = ((position.z > position2.z) ? new Vector3(eulerAngles.x - 15f, eulerAngles.y, eulerAngles.z) : new Vector3(eulerAngles.x + 15f, eulerAngles.y, eulerAngles.z));
		iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(position.x, position.y + 1f, position.z + num), "easetype", iTween.EaseType.easeOutQuart, "time", 0.15f));
		iTween.ScaleTo(gameObject, iTween.Hash("scale", vector, "time", 0.2f));
		StartCoroutine(DoSpellFinished());
		iTween.RotateTo(gameObject, iTween.Hash("rotation", vector2, "easetype", iTween.EaseType.easeOutQuart, "time", 0.1f, "delay", 0.15f));
		iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(position.x, position.y + 0.1f, position.z + num2), "easetype", iTween.EaseType.easeOutQuart, "time", 0.05f, "delay", 0.25f));
		iTween.RotateTo(gameObject, iTween.Hash("rotation", eulerAngles, "easetype", iTween.EaseType.easeOutQuart, "time", 0.05f, "delay", 0.25f));
		iTween.ScaleTo(gameObject, iTween.Hash("scale", localScale, "time", 0.05f, "delay", 0.25f));
		iTween.MoveTo(gameObject, iTween.Hash("position", position, "easetype", iTween.EaseType.easeOutQuart, "time", 0.3f, "delay", 0.25));
		gameObject3.transform.parent = base.transform;
		gameObject3.transform.position = new Vector3(position.x, position.y + 1f, position.z);
		gameObject3.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(m_ImpactEffectDelay);
		if (!string.IsNullOrEmpty(m_ShowAttackSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_ShowAttackSoundPrefab);
		}
		yield return new WaitForSeconds(0.3f);
	}

	private IEnumerator PlayImpactEffects()
	{
		if (m_targets.Count == 0)
		{
			Finish();
			yield break;
		}
		GameObject gameObject = GetSourceCard().gameObject;
		GameObject gameObject2 = m_targets[0].gameObject;
		Vector3 position = gameObject2.transform.position;
		Vector3 eulerAngles = gameObject2.transform.eulerAngles;
		GameObject gameObject3 = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? Object.Instantiate(m_ChessShockwaveRed) : Object.Instantiate(m_ChessShockwaveBlue));
		ParticleSystem.MainModule main = gameObject3.GetComponent<ParticleSystem>().main;
		GameObject gameObject4 = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? Object.Instantiate(m_ChessImpactBlue) : Object.Instantiate(m_ChessImpactRed));
		ParticleSystem.MainModule main2 = gameObject4.GetComponent<ParticleSystem>().main;
		float num = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? 0.25f : (-0.25f));
		float num2 = 0.15f;
		bool flag = ((m_targets.Count == 1 && (gameObject2.transform.position.z < -7f || gameObject2.transform.position.z > -2f)) ? true : false);
		float num3 = ((m_targets.Count == 2 && gameObject.transform.position.z > gameObject2.transform.position.z) ? 0f : ((m_targets.Count == 1 && gameObject.transform.position.z > gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) < -0.5f) ? 0.523599f : ((m_targets.Count == 1 && gameObject.transform.position.z > gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) > 0.5f) ? (-0.523599f) : ((m_targets.Count == 1 && gameObject.transform.position.z < gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) < -0.5f) ? 2.61799f : ((m_targets.Count == 1 && gameObject.transform.position.z < gameObject2.transform.position.z && Mathf.Abs(gameObject.transform.position.x) - Mathf.Abs(gameObject2.transform.position.x) > 0.5f) ? 3.66519f : ((m_targets.Count != 1 || !(gameObject.transform.position.z > gameObject2.transform.position.z)) ? 3.14159f : 0f))))));
		gameObject3.transform.parent = base.transform;
		gameObject4.transform.parent = base.transform;
		gameObject3.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z - num);
		main.startRotation = num3;
		if (m_targets.Count == 2)
		{
			main.startSize = 4f;
			iTween.MoveTo(gameObject3, iTween.Hash("position", new Vector3(gameObject.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z + num), "time", 0.4f));
			gameObject3.GetComponent<ParticleSystem>().Play();
		}
		else if (flag)
		{
			GameObject gameObject5 = ((gameObject.transform.position.z > gameObject2.transform.position.z) ? Object.Instantiate(m_ChessTrailRed) : Object.Instantiate(m_ChessTrailBlue));
			gameObject5.transform.parent = base.transform;
			gameObject5.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
			num2 = 0.5f;
			float x = (gameObject.transform.position.x + gameObject2.transform.position.x) * 0.5f;
			float z = ((gameObject2.transform.position.z > -4f) ? (-2.4f) : (-6.4f));
			if (gameObject.transform.position.x + gameObject2.transform.position.x < -17.5f || gameObject.transform.position.x + gameObject2.transform.position.x > -12.5f)
			{
				iTween.MoveTo(gameObject5, iTween.Hash("path", new Vector3[2]
				{
					new Vector3(x, gameObject2.transform.position.y + 2f, z),
					new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.1f, gameObject2.transform.position.z)
				}, "easetype", iTween.EaseType.easeOutQuad, "time", 0.4f));
			}
			else
			{
				num2 = 0.4f;
				iTween.MoveTo(gameObject5, iTween.Hash("position", new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z), "time", 0.3f));
			}
		}
		else
		{
			iTween.MoveTo(gameObject3, iTween.Hash("position", new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 0.5f, gameObject2.transform.position.z + num), "time", 0.4f));
			gameObject3.GetComponent<ParticleSystem>().Play();
		}
		gameObject4.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + 1f, gameObject2.transform.position.z);
		main2.startDelay = num2;
		gameObject4.GetComponent<ParticleSystem>().Play();
		if (!flag)
		{
			ShakeMinion(gameObject2, position, eulerAngles);
		}
		if (m_targets.Count == 2)
		{
			GameObject gameObject6 = m_targets[1].gameObject;
			Vector3 position2 = gameObject6.transform.position;
			Vector3 eulerAngles2 = gameObject6.transform.eulerAngles;
			GameObject obj = ((gameObject.transform.position.z > gameObject6.transform.position.z) ? Object.Instantiate(m_ChessImpactBlue) : Object.Instantiate(m_ChessImpactRed));
			obj.transform.parent = base.transform;
			obj.transform.position = new Vector3(gameObject6.transform.position.x, gameObject6.transform.position.y + 1f, gameObject6.transform.position.z);
			obj.GetComponent<ParticleSystem>().Play();
			ShakeMinion(gameObject6, position2, eulerAngles2);
		}
		yield return new WaitForSeconds(num2);
		if (!string.IsNullOrEmpty(m_ShowImpactSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_ShowImpactSoundPrefab);
		}
	}

	private void ShakeMinion(GameObject target, Vector3 targetOrgPos, Vector3 targetOrgRot)
	{
		iTween.MoveTo(target, iTween.Hash("position", new Vector3(targetOrgPos.x, targetOrgPos.y + 0.15f, targetOrgPos.z), "time", 0.05f, "islocal", true));
		iTween.RotateTo(target, iTween.Hash("rotation", new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(-15f, 15f)), "time", 0.08f, "islocal", true));
		iTween.RotateTo(target, iTween.Hash("rotation", new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), "time", 0.08f, "islocal", true, "delay", 0.08f));
		iTween.RotateTo(target, iTween.Hash("rotation", new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), "time", 0.08f, "islocal", true, "delay", 0.16f));
		iTween.MoveTo(target, iTween.Hash("position", targetOrgPos, "time", 0.08f, "islocal", true, "delay", 0.24f));
		iTween.RotateTo(target, iTween.Hash("rotation", targetOrgRot, "time", 0.08f, "islocal", true, "delay", 0.24f));
	}

	private IEnumerator DoSpellFinished()
	{
		if (m_targets.Count == 0)
		{
			Finish();
			yield break;
		}
		GameObject source = GetSourceCard().gameObject;
		GameObject gameObject = m_targets[0].gameObject;
		bool useSpellFinishDelay = false;
		if (m_targets.Count == 1 && (gameObject.transform.position.z < -7f || gameObject.transform.position.z > -2f) && (source.transform.position.x + gameObject.transform.position.x < -17.5f || source.transform.position.x + gameObject.transform.position.x > -12.5f))
		{
			useSpellFinishDelay = true;
		}
		yield return new WaitForSeconds(m_ImpactEffectDelay);
		StartCoroutine(PlayImpactEffects());
		if (useSpellFinishDelay)
		{
			yield return new WaitForSeconds(m_SpellFinishDelay);
		}
		foreach (GameObject target2 in m_targets)
		{
			GameUtils.DoDamageTasks(m_taskList, GetSourceCard(), target2.GetComponentInChildren<Card>());
		}
		foreach (GameObject target in m_targets)
		{
			while (iTween.HasTween(target))
			{
				yield return null;
			}
		}
		while (iTween.HasTween(source))
		{
			yield return null;
		}
		OnSpellFinished();
		yield return new WaitForSeconds(1f);
		OnStateFinished();
	}
}
