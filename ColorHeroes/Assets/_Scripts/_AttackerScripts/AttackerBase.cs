using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public abstract class AttackerBase : MonoBehaviour, IAttacker
{
    public DamageTypeEnum DamageType;
    public ColorEnum AttackColor;

    protected AttackInfo _attackInfo;

    protected List<IDamagable> _targetDamagableList;

    protected IAttacker _attackerInterface;

    protected Action<IDamagable> _onDamagableDestructedAction;
    protected Action<AttackInfo> _onDamageReflectedAction;

    protected List<IDamagable> _registeredDamagableList;

    public bool IsAttackSucceeded { get; private set; }

    protected bool _canAttack;

    protected virtual void Awake()
    {
        InitAttackerInterface();
        InitTargetDamagableList();
    }

    protected void InitAttackerInterface()
    {
        _attackerInterface = gameObject.GetInterface<IAttacker>();
    }

    protected void InitTargetDamagableList()
    {
        _targetDamagableList = new List<IDamagable>();
        _registeredDamagableList = new List<IDamagable>();
    }

    public void SetCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }

    public virtual void Attack()
    {
        if (!_canAttack)
            return;

        AttackTargetDamagables();
    }

    protected void AttackTargetDamagables()
    {
        IsAttackSucceeded = false;

        foreach(IDamagable damagable in _targetDamagableList)
        {
            bool hitDamagable = AttackTargetDamagable(damagable);

            if (hitDamagable)
                IsAttackSucceeded = true;

            PerformCustomAttackActions();
        }
    }

    protected virtual bool AttackTargetDamagable(IDamagable damagable)
    {
        bool hitDamagable = false;

        SetAttackInfo(damagable);

        _onDamagableDestructedAction = delegate(IDamagable targetDamagable)
        {
            OnDamagableDestructed(targetDamagable);
        };
        damagable.RegisterToDamagableDestructedEvent(_onDamagableDestructedAction, true);

        _onDamageReflectedAction = delegate(AttackInfo attackInfo)
        {
            OnDamageReflected(attackInfo);
        };
        damagable.RegisterToDamageReflectedEvent(_onDamageReflectedAction, true);

        _registeredDamagableList.Add(damagable);

        if (damagable.TakeDamage(_attackInfo))
        {
            hitDamagable = true;
        }

        return hitDamagable;
    }

    protected virtual void PerformCustomAttackActions()
    {

    }

    protected virtual void SetAttackInfo(IDamagable damagable)
    {
        _attackInfo = new AttackInfo(_attackerInterface, damagable, DamageType, AttackColor);
        _attackInfo.DamageAmount = GetDamageAmount(damagable);
    }

    protected abstract int GetDamageAmount(IDamagable damagable);

    protected virtual void OnDamagableDestructed(IDamagable damagable)
    {
        _targetDamagableList.Remove(damagable);

        damagable.RegisterToDamagableDestructedEvent(_onDamagableDestructedAction, false);
        damagable.RegisterToDamageReflectedEvent(_onDamageReflectedAction, false);
    }

    protected virtual void OnDamageReflected(AttackInfo attackInfo)
    {
        if (attackInfo.Attacker != _attackerInterface)
            return;

        attackInfo.Damagable.RegisterToDamageReflectedEvent(_onDamageReflectedAction, false);
    }

    public virtual void DeactivateAttacker()
    {
        UnregisterFromAllDamgables();
        ResetTargetDamagables();

        SetCanAttack(false);
    }

    protected void UnregisterFromAllDamgables()
    {
        foreach (IDamagable damagable in _registeredDamagableList)
        {
            damagable.RegisterToDamagableDestructedEvent(_onDamagableDestructedAction, false);
            damagable.RegisterToDamageReflectedEvent(_onDamageReflectedAction, false);
        }

        _registeredDamagableList.Clear();
    }

    protected void ResetTargetDamagables()
    {
        _targetDamagableList.Clear();
    }
}
