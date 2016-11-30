using UnityEngine;
using System.Collections;

public class AttackInfo
{
    public IAttacker Attacker;
    public IDamagable Damagable;
    public DamageTypeEnum DamageType;
    public ColorEnum AttackColor;
    public int DamageAmount;

    public AttackInfo(IAttacker attacker, IDamagable damagable, DamageTypeEnum damageType, ColorEnum AttackColor)
    {
        Attacker = attacker;
        Damagable = damagable;
        DamageType = damageType;
    }
}
