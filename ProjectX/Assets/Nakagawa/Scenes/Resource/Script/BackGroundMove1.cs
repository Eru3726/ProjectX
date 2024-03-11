using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove1 : MonoBehaviour
{
	private const float k_maxLength = 1f;
	private const string k_propName = "_MainTex";

	[SerializeField]
	private Vector2 m_offsetSpeed;

	private Material m_material;

	public GameObject Player;
	public float SlideSpeedX;//背景を回すスピードをプレイヤーに合わせる
	public float SlideSpeedY;//背景を回すスピードをプレイヤーに合わせる
	Vector2 PlayerPos;
	private void Start()
	{
		if (GetComponent<Image>() is Image i)
		{
			m_material = i.material;
		}
	}

	private void Update()
	{
		PlayerPos.x = Player.transform.position.x/SlideSpeedX;
		PlayerPos.y = Player.transform.position.y/SlideSpeedY;
		if (m_material)
		{
			// xとyの値が0 ～ 1でリピートするようにする
			var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
			var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
			var offset = PlayerPos;
			m_material.SetTextureOffset(k_propName, offset);
		}
	}

	private void OnDestroy()
	{
		// ゲームをやめた後にマテリアルのOffsetを戻しておく
		if (m_material)
		{
			m_material.SetTextureOffset(k_propName, Vector2.zero);
		}
	}
}
