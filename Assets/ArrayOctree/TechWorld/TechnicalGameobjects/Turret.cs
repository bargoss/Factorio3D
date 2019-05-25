﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    // set in script
    public GoConnection goInterface;
    Vector3 muzzleInitialLocalPos;

    // set these in editor on prefab
    public Transform muzzle;
    public float aimDelay;
    public float recoilDelay;
    public float coolDown;

    public GameObject projectilePrefab;
    public float projectileVelocity;

    float lastShot;


    public void Initialize(GoConnection goInterface)
    {
        this.goInterface = goInterface;
    }
    private void Start()
    {
        lastShot = Time.time;
        muzzleInitialLocalPos = muzzle.transform.localPosition;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { StartShoot(Vector3.up + Vector3.forward); }
    }
    public bool CanShoot()
    {
        return true;

        if(goInterface.CanOutput() != 0 && Time.time > lastShot + coolDown)
        {
            return true;
        }
        return false;
    }
    void ConsumeAmmo()
    {
        return;
        goInterface.Output();
    }

    public void StartShoot(Vector3 targetDirection)
    {
        //goInterface.Output();
        StartCoroutine(AimAndShootCoroutine(targetDirection));
    }
    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzle.transform.position, muzzle.transform.rotation);
        Collider projectileCollider = projectile.GetComponent<Collider>();
        Physics.IgnoreCollision(projectileCollider, GetComponent<Collider>());

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = muzzle.transform.forward * projectileVelocity;
    }

    IEnumerator AimAndShootCoroutine(Vector3 targetDirection)
    {
        float startTime = Time.time;
        float fireTime = startTime + aimDelay;
        float recoilEnd = fireTime + recoilDelay;

        // Aim
        while(Time.time < fireTime)
        {
            AimTowardsDirection(targetDirection);
            yield return new WaitForFixedUpdate();
        }

        // Shoot
        lastShot = Time.time;
        ConsumeAmmo();

        // Recoil
        float kickVelocity = 0.69f * 1.2f;
        while (Time.time < recoilEnd)
        {
            // kick
            Vector3 kick = -muzzle.transform.forward * kickVelocity;
            muzzle.transform.position += kick;
            kickVelocity *= 0.05f;

            // recover
            muzzle.localPosition = Vector3.Lerp(muzzle.localPosition, muzzleInitialLocalPos, 0.13f);

            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    void AimTowardsDirection(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion myRot = muzzle.transform.rotation;
        Quaternion newRot = Quaternion.Slerp(myRot, targetRot, 0.15f);

        muzzle.transform.rotation = newRot;
    }
}
