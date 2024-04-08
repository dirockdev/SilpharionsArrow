
public interface IDamagable
{
    void GetDamage(int damage,bool crit=false);
    void GetStateDamage(int damage);
    void GetStunned();
    void GetHeal(int heal);

    bool isPoisoned();
}
