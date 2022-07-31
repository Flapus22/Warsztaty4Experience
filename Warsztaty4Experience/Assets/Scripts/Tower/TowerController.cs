using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [field: SerializeField]
    public int TowerPrice { get; set; }

    [field: SerializeField]
    private float MaxRaycastDistance { get; set; }
    [field: SerializeField]
    private LayerMask FloorLayerMask { get; set; }
    [field: SerializeField]
    private LayerMask BuildGroundLayerMask { get; set; }

    [field: Space]
    [field: SerializeField]
    private Material DefaultTowerMaterial { get; set; }
    [field: SerializeField]
    private Material TowerMaterialForIncorrectLocation { get; set; }
    [field: SerializeField]
    private MeshRenderer[] TowerRendererCollection { get; set; }
    [field: SerializeField]
    public TowerAttackData TowerAttackData { get; private set; }
    [field: SerializeField]
    private CapsuleCollider CapsuleCollider { get; set; }

    private Collider[] EnemyCollider { get; set; }

    private Camera MainCamera { get; set; }
    private bool IsOnBuildGround { get; set; }
    private bool IsColliding { get; set; }
    private bool IsPlaced { get; set; }
    private float CurrentDelay { get; set; }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        if (IsPlaced == false)
        {
            TryUpdatePositionTower();
            UpdateIsOnBuildGround();
        }
        else
        {
            TryTakeShot();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        IsColliding = false;
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        IsColliding = true;
    }

    public bool CheckIfCanBePlaced()
    {
        return IsOnBuildGround == true && IsColliding == false;
    }

    public void PlaceTower()
    {
        IsPlaced = true;
    }

    private void Initialize()
    {
        MainCamera = Camera.main;
        IsPlaced = false;
        EnemyCollider = new Collider[1];
    }

    private void TryUpdatePositionTower()
    {
        Ray vRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(vRay, out RaycastHit vHit, MaxRaycastDistance, FloorLayerMask) == true)
        {
            transform.position = new Vector3(vHit.point.x, 0.0f, vHit.point.z);
        }

        UpdateMaterial();
    }

    private void UpdateIsOnBuildGround()
    {
        Ray vRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        IsOnBuildGround = Physics.Raycast(vRay, MaxRaycastDistance, BuildGroundLayerMask);

        Debug.Log(CheckIfCanBePlaced() ? "Can be placed" : "Cannot be placed");
    }

    private void UpdateMaterial()
    {
        for (int i = 0; i < TowerRendererCollection.Length; i++)
        {
            if (CheckIfCanBePlaced() == true)
                TowerRendererCollection[i].material = DefaultTowerMaterial;
            else
                TowerRendererCollection[i].material = TowerMaterialForIncorrectLocation;
        }
    }

    private void TryTakeShot()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, TowerAttackData.AttackRadius, EnemyCollider, TowerAttackData.EnemyLayerMask);
        CurrentDelay += Time.deltaTime;
        if (size > 0 && CurrentDelay > TowerAttackData.FireRate)
        {
            Enemy enemyTarget = EnemyCollider[0].GetComponent<Enemy>();

            if (enemyTarget != null)
            {
                var bulletStartPosition = CapsuleCollider.ClosestPointOnBounds(EnemyCollider[0].transform.position);
                bulletStartPosition.y = 5;
                //enemyTarget.TakeDamage(TowerAttackData.Damage);
                BulletManager.Instance.ShootToEnemyBullet(bulletStartPosition, enemyTarget, TowerAttackData.Damage);
            }
            CurrentDelay = 0;
        }
    }
}
