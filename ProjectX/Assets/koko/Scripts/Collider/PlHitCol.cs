using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlHitCol : MonoBehaviour, IDamageable, IShockable, IInvincible
{

    // シーン名取得用
    string GetSceneName;
    Mng_Game gameMng;

    [SerializeField] public int maxHp = 20;
    [SerializeField] public int nowHp;
    [SerializeField] public float resist = 1;
    [SerializeField] public float time = 0;

    [SerializeField]
    public int barrier = 0;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public float invTime => time;

    public void TakeDamage(int value)
    {
        if (time <= 0)
        {
            if (barrier > 0)
            {
                barrier--;
            }
            else
            {
                nowHp -= value;
            }

            gameMng.OneShotSE_C(SEData.Type.PlayerSE, Mng_Game.ClipSe.Hit1);

            if (nowHp <= 0)
            {
                Die();
            }
        }
    }

    public void TakeShock(float value, Vector3 pos)
    {
        if (time <= 0)
        {
            Vector3 shockDir = pos - this.transform.position;

            if (mc != null)
            {
                mc.InputFlick(this.transform.position - shockDir, value * resist, 0.5f, false);
            }
        }
    }

    public void TakeStop() { }

    public void TakeInv(float value)
    {
        if (time <= 0)
        {
            time = value;
        }
    }

    void Die()
    {
        if (GetSceneName == "Heaven1") { return; }
        SceneManager.LoadScene(GetSceneName);

        if (body != null)
        {
            Destroy(body.gameObject);
        }

    }

    void Start()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        // シーン名取得
        GetSceneName = SceneManager.GetActiveScene().name;
        nowHp = maxHp;

        gameMng = GameObject.Find("GameManager").GetComponent<Mng_Game>();
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        GetSceneName = nextScene.name;
    }
}
