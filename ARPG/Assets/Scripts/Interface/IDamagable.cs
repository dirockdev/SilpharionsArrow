
public interface IDamagable
{
    void GetDamage(int damage,bool crit=false);
    void GetStateDamage(int damage);
    void GetHeal(int heal);
}
