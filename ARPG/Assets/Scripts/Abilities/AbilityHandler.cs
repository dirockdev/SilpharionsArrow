using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class AbilityHandler : MonoBehaviour
{
    [SerializeField] Ability startingAbility, ability1, dashAbility, shotgunAbility, arrowRainAbility;

    List<AbilityContainer> abilities;
    public UnityEvent<AbilityContainer, int> onAbilityChange;
    public UnityEvent<float, int> onCooldownUpdate;

    public static event Action onDashAbilityUnlocked;
    public static event Action onShotgunAbilityUnlocked;
    public static event Action onArrowRainAbilityUnlocked;

    Animator anim;

    [SerializeField]
    public List<AbilityContainer> Abilities { get => abilities; set => abilities = value; }

    private void Start()
    {
        InitializeAbilities();
        PlayerExp.OnLevelUp += AddAbilityWhenLevelUp;
    }



    private void InitializeAbilities()
    {
        Abilities = new List<AbilityContainer>();

        // Añade las habilidades al iniciar
        AddAbility(startingAbility);
        AddAbility(ability1);
    }
    private void AddAbilityWhenLevelUp(int newLevel)
    {
        switch (newLevel)
        {
            case 5:
                AddAbility(dashAbility);
                onDashAbilityUnlocked?.Invoke();
                break;
            case 8:
                AddAbility(shotgunAbility);
                onShotgunAbilityUnlocked?.Invoke();
                break;
            case 11:
                AddAbility(arrowRainAbility);
                onArrowRainAbilityUnlocked?.Invoke();
                break;
        }
    }
    public void AddAbility(Ability abilityToAdd)
    {
        if (abilityToAdd == null)
            return;

        AbilityContainer abilityContainer = CreateAbilityContainer(abilityToAdd);
        Abilities.Add(abilityContainer);
        onAbilityChange?.Invoke(abilityContainer, Abilities.Count - 1);
    }
    private AbilityContainer CreateAbilityContainer(Ability ability)
    {
        if (ability is ProjectileAbility)
        {
            return new ProjectileAbilityContainer(ability);
        }
        else if (ability is DashAbility)
        {
            return new DashAbilityContainer(ability);
        }
        else if (ability is AreaAbility)
        {
            return new AreaAbilityContainer(ability);
        }
        else
        {
            return null;
        }
    }

    public void ActivateAbility(AbilityContainer abilityContainer)
    {
        if (abilityContainer.currentCooldown > 0f) { return; }
        IAbilityBehaviour abilityAction = CreateAbilityAction(abilityContainer);
        //CreateAbility
        abilityContainer.ability.UseAbility(transform, abilityAction, MouseInput.rayToWorldPoint, abilityContainer);
        AbilityAnimation(abilityContainer);

        abilityContainer.Cooldown();
    }

    private IAbilityBehaviour CreateAbilityAction(AbilityContainer abilityContainer)
    {
        if (abilityContainer.ability is ProjectileAbility)
        {

            return (ProjectileAbility)abilityContainer.ability;
        }
        else if (abilityContainer.ability is DashAbility)
        {
            return (DashAbility)abilityContainer.ability;
        }
        else if (abilityContainer.ability is AreaAbility)
        {
            return (AreaAbility)abilityContainer.ability;
        }
        else
        {
            return null;
        }
    }

    public void ActivateAbility(int abilityID, bool pressed)
    {
        if (abilityID >= Abilities.Count || Abilities[abilityID] == null)
            return;


        AbilityContainer abilityContainer = Abilities[abilityID];
        abilityContainer.isPressed = pressed;

    }


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        ProccessCooldown();
        ProccessHoldAbilities();

    }

    private void ProccessHoldAbilities()
    {
        for (int i = 0; i < Abilities.Count; i++)
        {
            if (Abilities[i].isPressed)
            {
                ActivateAbility(Abilities[i]);
            }
        }
    }
    private void ProccessCooldown()
    {
        for (int i = 0; i < Abilities.Count; i++)
        {

            Abilities[i].ReduceCooldown(Time.deltaTime);
            onCooldownUpdate?.Invoke(Abilities[i].CooldownNormalized, i);
        }

    }
    
    private void AbilityAnimation(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {
            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            anim.SetFloat("attackSpeed", projectileAbilityContainer.animSpeed);
            anim.SetTrigger("Attack");
        }else if(abilityContainer is DashAbilityContainer)
        {
            DashAbilityContainer dashAbilityContainer = (DashAbilityContainer)abilityContainer;
            anim.SetFloat("attackSpeed", dashAbilityContainer.animSpeed);

            anim.SetTrigger("Teleport");
        }


        Vector3 direction = (MouseInput.rayToWorldPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

        transform.DORotateQuaternion(targetRotation, 0.3f);
    }
}
