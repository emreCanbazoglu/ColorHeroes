using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AttackerCollider : MonoBehaviour 
{
    public List<LayerEnum> TargetDamagableLayerList;
    public Collider AttackCollider;

    #region Events

    public static Action<IDamagable> OnDamagableEnteredBattleRange;

    void FireOnDamagableEnteredBattleRange(IDamagable damagable)
    {
        if (OnDamagableEnteredBattleRange != null)
            OnDamagableEnteredBattleRange(damagable);
    }

    public static Action<IDamagable> OnDamagableExitedBattleRange;

    void FireOnDamagableExitedBattleRange(IDamagable damagable)
    {
        if (OnDamagableExitedBattleRange != null)
            OnDamagableExitedBattleRange(damagable);
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (TargetDamagableLayerList.Contains((LayerEnum)other.gameObject.layer))
            DamagableEntered(other);
        
    }

    void OnTriggerStay(Collider other)
    {
        if (TargetDamagableLayerList.Contains((LayerEnum)other.gameObject.layer))
            DamagableEntered(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (TargetDamagableLayerList.Contains((LayerEnum)other.gameObject.layer))
            DamagableExited(other);
    }

    void DamagableEntered(Collider other)
    {
        IDamagable damagable = other.gameObject.GetInterfaceInChildren<IDamagable>();

        FireOnDamagableEnteredBattleRange(damagable);
    }

    void DamagableExited(Collider other)
    {
        IDamagable damagable = other.gameObject.GetInterfaceInChildren<IDamagable>();

        FireOnDamagableExitedBattleRange(damagable);
    }

    public float GetColliderTopBoundY()
    {
        return AttackCollider.bounds.max.y;
    }

}
