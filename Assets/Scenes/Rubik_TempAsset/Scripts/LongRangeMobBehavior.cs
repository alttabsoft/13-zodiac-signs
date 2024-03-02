using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeMobMove : MonoBehaviour
{
    private GameObject shadow;
    [SerializeField] private GameObject longRangeMob;
    [SerializeField] private GameObject bullet;
    [SerializeField] private BulletStatus bulletStatus;
    
    private CharacterController bulletMoveController;
    private int attackCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bulletShot());
        shadow = GameObject.FindGameObjectWithTag("Shadow").gameObject;
        bulletMoveController = bullet.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        longRangeMob.transform.position = new Vector3(longRangeMob.transform.position.x,longRangeMob.transform.position.y,shadow.transform.position.z+0.3f);
        attackCounter += 1;

        if (attackCounter > 100)
        {
            bullet.transform.position = longRangeMob.transform.position;
            bullet.SetActive(true);
            bulletStatus.bulletActiveState = true;
            attackCounter = 0;
        }
    }

    private IEnumerator bulletShot()
    {
        while (true)
        {
            if (bulletStatus.bulletActiveState)
            {
                bulletMoveController.Move(new Vector3(-1,0,0) * 0.1f);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
