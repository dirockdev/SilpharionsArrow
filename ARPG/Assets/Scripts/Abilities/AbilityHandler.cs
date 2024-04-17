using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Cinemachine.DocumentationSortingAttribute;

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
    IDataService dataService = new JsonDataService();

    [SerializeField]
    public List<AbilityContainer> Abilities { get => abilities; set => abilities = value; }


    private void Start()
    {
        InitializeAbilities();

        PlayerExp.OnUnlock += AddAbilityWhenLevelUp;
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
                if (abilities.Count < 3)
                {
                    AddAbility(dashAbility);
                    onDashAbilityUnlocked?.Invoke();

                }
                break;
            case 8:
                if (abilities.Count < 4)
                {
                    AddAbility(shotgunAbility);
                    onShotgunAbilityUnlocked?.Invoke();
                }
                break;
            case 11:
                if (abilities.Count < 5)
                {

                    AddAbility(arrowRainAbility);
                    onArrowRainAbilityUnlocked?.Invoke();

                }
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
        if (CharacterStats.isDead) return;
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
        }
        else if (abilityContainer is DashAbilityContainer)
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

    public void SerializeJson()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            AbilityContainer abilityContainer = abilities[i];
            if (abilityContainer != null)
            {

                if (dataService.SaveData($"/player-ability{i + 1}.json", abilityContainer, true))
                {
                    Debug.Log("Ability saved successfully!");
                }
                else
                {
                    Debug.Log("Could not save ability");
                }
            }
        }
    }
    public void DesSerializeJson()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            string filePath = $"/player-ability{i + 1}.json";
            switch (i)
            {
                case 0:
                    {
                        ProjectileAbilityContainer proj = FileHandler.ReadFromJSON<ProjectileAbilityContainer>(filePath);
                        ProjectileAbilityContainer loadedAbility = new ProjectileAbilityContainer(startingAbility);
                        loadedAbility.SetLoadValues(proj);
                        abilities[i] = loadedAbility;
                        break;
                    }

                case 1:
                    {
                        ProjectileAbilityContainer proj2 = FileHandler.ReadFromJSON<ProjectileAbilityContainer>(filePath);
                        ProjectileAbilityContainer loadedAbility2 = new ProjectileAbilityContainer(ability1);
                        loadedAbility2.SetLoadValues(proj2);
                        abilities[i] = loadedAbility2;
                        break;
                    }

                case 2:
                    {

                        DashAbilityContainer dash = FileHandler.ReadFromJSON<DashAbilityContainer>(filePath);
                        DashAbilityContainer loadedAbility3 = new DashAbilityContainer(dashAbility);
                        loadedAbility3.SetLoadValues(dash);
                        abilities[i] = loadedAbility3;
                        break;
                    }
                case 3:
                    {
                        ProjectileAbilityContainer proj3 = FileHandler.ReadFromJSON<ProjectileAbilityContainer>(filePath);
                        ProjectileAbilityContainer loadedAbility = new ProjectileAbilityContainer(shotgunAbility);
                        loadedAbility.SetLoadValues(proj3);
                        abilities[i] = loadedAbility;
                        break;
                    }
                case 4:
                    {
                        AreaAbilityContainer proj3 = FileHandler.ReadFromJSON<AreaAbilityContainer>(filePath);
                        AreaAbilityContainer loadedAbility = new AreaAbilityContainer(arrowRainAbility);
                        loadedAbility.SetValues(proj3);
                        abilities[i] = loadedAbility;
                        break;
                    }
            }
        }
    }


}
