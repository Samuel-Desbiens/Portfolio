using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnnemyBehaviour
{
    float GetDamage();

    void GetDamaged(float DMG,SpellBehaviour.SpellElements Element);

    void SetDamageModifier(float DamageModifier,float Timer);

    void SetMovementSpeedModifier(float MovementSpeedModifier, float Timer);

    void SetStun(float Timer);

    void SetBurn(float DMG, float Timer);

}
