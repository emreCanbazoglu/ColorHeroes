using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public abstract class DamagableBase : MonoBehaviour, IDamagable
{
    public List<DamagableBase> PriorDamagableList;

    public List<DamageTypeEnum> ImmuneDamageTypeList;

    public float DamageCooldown;

    protected IEnumerator _coolDownRoutine;

    public bool IsDestructed { get; private set; }
    public bool IsInCooldown { get; private set; }
    public bool IsActiveDamagable { get; private set; }

    protected IDamagable _damagableInterface;

    protected AttackInfo _curAttackInfo;

    #region Events

    public Action<IDamagable> OnDamagableDestructed;

    void FireOnDamagableDestructed()
    {
        if (OnDamagableDestructed != null)
            OnDamagableDestructed(this);
    }

    public Action<AttackInfo> OnDamageReflected;

    void FireOnDamageReflected()
    {
        if (OnDamageReflected != null)
            OnDamageReflected(_curAttackInfo);
    }
    #endregion

    protected virtual void Awake()
    {
        InitDamagableınterface();
    }

    protected void InitDamagableınterface()
    {
        _damagableInterface = gameObject.GetInterface<IDamagable>();
    }

    protected abstract void SetDamageAmount();

    protected bool IsImmuneToDamageType(DamageTypeEnum damageType)
    {
        if (ImmuneDamageTypeList.Contains(damageType))
            return true;

        return false;
    }

    protected virtual void ImmuneToDamageTypeAction(DamageTypeEnum damageType)
    {
        FireOnDamageReflected();
    }

    protected virtual bool CanTakeDamage()
    {
        if (IsInCooldown
            || IsDestructed
            || !IsActiveDamagable
            || (PriorDamagableList.Count != 0 && PriorDamagableList.Any(val => !val.IsDestructed)))
            return false;

        return true;
    }

    protected virtual void PerformCustomTakeDamageActions()
    {
        
    }

    protected virtual void DamagableDestructed()
    {
        IsDestructed = true;

        FireOnDamagableDestructed();
    }

    protected void EnterCooldown()
    {
        ExitCooldown();

        _coolDownRoutine = CooldownProgress();

        StartCoroutine(_coolDownRoutine);
    }

    protected void ExitCooldown()
    {
        if (_coolDownRoutine != null)
            StopCoroutine(_coolDownRoutine);

        IsInCooldown = false;
    }

    IEnumerator CooldownProgress()
    {
        IsInCooldown = true;

        yield return new WaitForSeconds(DamageCooldown);

        IsInCooldown = false;
    }

    protected abstract int GetCurHealth();

    protected abstract void IncreaseHealth(int addHealth);


    #region IDamagable Implementation

    public IDamagable GetDamagableInterface()
    {
        return _damagableInterface;
    }

    public virtual bool TakeDamage(AttackInfo attackInfo)
    {
        _curAttackInfo = attackInfo;

        if (!CanTakeDamage())
            return false;

        if (IsImmuneToDamageType(attackInfo.DamageType))
        {
            ImmuneToDamageTypeAction(attackInfo.DamageType);
            return false;
        }

        SetDamageAmount();

        IncreaseHealth(-attackInfo.DamageAmount);

        if (GetCurHealth() <= 0)
            DamagableDestructed();
        else
        {
            EnterCooldown();
        }

        PerformCustomTakeDamageActions();

        return true;
    }

    public virtual void RegisterToDamagableDestructedEvent(Action<IDamagable> action, bool isRegister)
    {
        if (isRegister)
            OnDamagableDestructed += action;
        else
            OnDamagableDestructed -= action;

    }

    public virtual void RegisterToDamageReflectedEvent(Action<AttackInfo> action, bool isRegister)
    {
        if (isRegister)
            OnDamageReflected += action;
        else
            OnDamageReflected -= action;
    }

    #endregion
}
