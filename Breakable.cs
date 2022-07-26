using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出現時に回転、的にボールが当たった際に爆散、スコアを加算するスクリプト.
/// </summary>

// --- 壊せるオブジェクトに付ける Breakable スクリプト -------------------------
public class Breakable : MonoBehaviour
{
    // ターゲットマネージャー.
    GameObject manager;
    TargetManager script;

    // 回転.
    [SerializeField] float angle = 90f;
    [SerializeField] Vector3 axis = Vector3.up;
    [SerializeField] float step = 180f;
    Quaternion targetRot;

    float force = 1500f;      // 壊れるときに（爆発的に）かかる力

    void Start()
    {
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();

        targetRot = Quaternion.AngleAxis(angle, axis) * transform.rotation;
    }

    void Update()
    {
        //出現時に回転させる.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step * Time.deltaTime * 3);
    }


    // --- 壊れる処理。子オブジェクトを取得してそれぞれ ExplodePart させる ------
    public void Break()
    {
        List<GameObject> children = GetAllChildren.GetAll(gameObject);

        foreach (GameObject obj in children)
        {
            ExplodePart(obj, force);
        }
        Destroy(gameObject, 0.5f);
    }


    // --- 部品にばらしてRigidbodyを付けてふっとばす --------------------------
    private void ExplodePart(GameObject child, float force)
    {
        child.transform.parent = null;
        Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(force, transform.position, 0f);
        Destroy(child.gameObject, 3f);
    }


    // --- 衝突検出 ----------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            script.Hit();
            Break();
        }
    }
}