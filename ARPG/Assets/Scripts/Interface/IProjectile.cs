using UnityEngine;

public interface IProjectile
{
    void SetCanPenetrate(bool getCanPenetrate);
    void SetDamage(int getDamage);
    void SetVelocity(int getVelocity);
    void SetCrit(int getCrit);
    void SetState(float state);
    void SetStateDmg(int stateDmg);
    void SetWidthProj(float width);
    void SetDirection(Vector3 direction);
    void SetTimeAlive(float time);
}

