using UnityEngine;
using System;
using System.Collections;

public interface IDamagable 
{
    IDamagable GetDamagableInterface();

    bool TakeDamage(AttackInfo attackInfo);

    void RegisterToDamagableDestructedEvent(Action<IDamagable> function, bool isRegister);

    void RegisterToDamageReflectedEvent(Action<AttackInfo> function, bool isRegister);
}
