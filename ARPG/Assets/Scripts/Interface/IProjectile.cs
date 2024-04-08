using UnityEngine;

public interface IProjectile
{
    void SetCanPenetrate(bool getCanPenetrate);
    void SetDamage(int getDamage);
    void SetVelocity(int getVelocity);
    void SetCrit(int getCrit);
    void SetState(float state);
    void SetStun(float stunProb);
    void SetStateDmg(int stateDmg);
    void SetWidthProj(float width);
    void SetDirection(Vector3 direction);
    void SetTimeAlive(float time);
    void SetHealOnCrits(bool canHeal);
    void SetReduceCooldown(bool canReduceCooldown);
}

