using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �o�����ɉ�]�A�I�Ƀ{�[�������������ۂɔ��U�A�X�R�A�����Z����X�N���v�g.
/// </summary>

// --- �󂹂�I�u�W�F�N�g�ɕt���� Breakable �X�N���v�g -------------------------
public class Breakable : MonoBehaviour
{
    // �^�[�Q�b�g�}�l�[�W���[.
    GameObject manager;
    TargetManager script;

    // ��].
    [SerializeField] float angle = 90f;
    [SerializeField] Vector3 axis = Vector3.up;
    [SerializeField] float step = 180f;
    Quaternion targetRot;

    float force = 1500f;      // ����Ƃ��Ɂi�����I�Ɂj�������

    void Start()
    {
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();

        targetRot = Quaternion.AngleAxis(angle, axis) * transform.rotation;
    }

    void Update()
    {
        //�o�����ɉ�]������.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step * Time.deltaTime * 3);
    }


    // --- ���鏈���B�q�I�u�W�F�N�g���擾���Ă��ꂼ�� ExplodePart ������ ------
    public void Break()
    {
        List<GameObject> children = GetAllChildren.GetAll(gameObject);

        foreach (GameObject obj in children)
        {
            ExplodePart(obj, force);
        }
        Destroy(gameObject, 0.5f);
    }


    // --- ���i�ɂ΂炵��Rigidbody��t���Ăӂ��Ƃ΂� --------------------------
    private void ExplodePart(GameObject child, float force)
    {
        child.transform.parent = null;
        Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(force, transform.position, 0f);
        Destroy(child.gameObject, 3f);
    }


    // --- �Փˌ��o ----------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            script.Hit();
            Break();
        }
    }
}