using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Artillery : MonoBehaviour
{
    [SerializeField] private BoardSide boardSide;
    public bool IsReadyToShoot {get { return isReadyToShoot; }}

    protected bool isReadyToShoot = true;
    protected float reloadTime;
    protected Transform aim;
    protected Projectile projectile;
    protected Coroutine reloadRoutine;
    protected Pool projectilesPool;
    protected string OurShipID;

    private void Start()
    {
        projectilesPool = Pool.Instance;
        aim = transform.Find("Aim");
        OurShipID = GetComponentInParent<Ship>().ShipID;
    }

    public void Shoot(float power)
    {
        if (IsReadyToShoot)
        {
            ShootMechanic(power);
            Reload();
        }
    }

    protected abstract void ShootMechanic(float power);


    public BoardSide GetBoardSide()
    {
        return boardSide;
    }


    public void ChangeProjectile(Projectile projectile)
    {
        if (this.projectile != projectile)
        {
            if (reloadRoutine != null)
            {
                StopCoroutine(reloadRoutine);
            }
            StartCoroutine(_reload(projectile));
        }
    }

    private void Reload()
    {
        if (reloadRoutine != null)
        {
            StopCoroutine(reloadRoutine);
        }
        StartCoroutine(_reload());
    }

    private IEnumerator _reload()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(GlobalConstants.RELOAD_TIME);
        isReadyToShoot = true;
    }

    private IEnumerator _reload(Projectile projectile)
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(5f);
        this.projectile = projectile;
        isReadyToShoot = true;
    }

    protected GameObject GetActualProjectile()
    {
        var missile = projectilesPool.Spawn(projectile.ToString(), aim.position, Quaternion.identity);
        return missile;
        //switch (projectile)
        //{
        //    case Projectile.Cannonbal:
                
        //        break;
        //    case Projectile.Bomb:
        //        break;
        //    case Projectile.Knuppel:
        //        break;
        //    default:
        //        break;
        //}
    }
}
