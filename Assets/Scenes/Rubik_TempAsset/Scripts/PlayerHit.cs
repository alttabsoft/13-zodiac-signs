using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject sword;

    private bool isAttacking = false;
    
    public void SwordAttack()
    {
        sword.SetActive(true);
        isAttacking = true;
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.15f);
        sword.SetActive(false);
        isAttacking = false;
    }
    private IEnumerator HitEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator MobRespawn(GameObject mob)
    {
        mob.SetActive(false);
        mob.transform.position = new Vector3(1.54f, 0.9f, -5.01f);
        yield return new WaitForSeconds(0.5f);
        mob.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            if (isAttacking)
            {
                Debug.Log("몬스터를 공격! 10의 데미지를 주었다.");
                StartCoroutine(MobRespawn(other.gameObject));
            }
            else
            {
                Debug.Log("몬스터와 충돌! 10의 데미지를 입었다.");
                StartCoroutine(HitEffect());
                StartCoroutine(MobRespawn(other.gameObject));
            }
        }
    }
}
