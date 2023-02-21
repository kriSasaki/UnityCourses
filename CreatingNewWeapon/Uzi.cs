using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon
{
    public override void Shoot(Transform shootPoint)
    {
        if (Delay >= FireRate)
        {
            Instantiate(Bullet, shootPoint.position, Quaternion.identity);
            Delay = 0;
        }

        Delay += Time.deltaTime;
    }
}
