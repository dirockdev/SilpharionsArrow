using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] Ability startingAbility, ability1, dashAbility, shotgunAbility, arrowRainAbility, potion;

    CharacterStats character;

    List<AbilityContainer> abilities;
    public static event Action<AbilityContainer, int> onAbilityChange;

    public static event Action<float, int> onCooldownUpdate;

    public static Action onDashAbilityUnlocked;
    public static Action onShotgunAbilityUnlocked;
    public static Action onArrowRainAbilityUnlocked;
    public static Action onManaUpdate;

    Animator anim;
    IDataService dataService = new JsonDataService();

    [SerializeField]
    public List<AbilityContainer> Abilities { get => abilities; set => abilities = value; }

    private void Awake()
    {
        abilities = new List<AbilityContainer>(6);
        anim = GetComponentInChildren<Animator>();
        character = GetComponent<CharacterStats>();
        PlayerExp.OnUnlock += AddAbilityWhenLevelUp;
    }
    private void OnDisable()
    {
        PlayerExp.OnUnlock -= AddAbilityWhenLevelUp;
        
    }
    private void Start()
    {
        InitializeAbilities();
        
    }



    private void InitializeAbilities()
    {

        AddAbility(startingAbility);
        abilities[0].isUnlocked = true;
        AddAbility(ability1);
        abilities[1].isUnlocked = true;

        AddAbility(dashAbility);
        abilities[2].isUnlocked = false;

        AddAbility(shotgunAbility);
        abilities[3].isUnlocked = false;
        
        AddAbility(arrowRainAbility);
        abilities[4].isUnlocked = false;
        
        AddAbility(potion);
        abilities[5].isUnlocked = true;



    }
    private void AddAbilityWhenLevelUp(int newLevel)
    {
        switch (newLevel)
        {
            case 5:
                if (!abilities[2].isUnlocked)
                {
                    abilities[2].isUnlocked = true;
                    onDashAbilityUnlocked?.Invoke();
                    onAbilityChange?.Invoke(abilities[2], 2);
                }
                break;
            case 8:
                if (!abilities[3].isUnlocked)
                {

                    abilities[3].isUnlocked = true;
                    onShotgunAbilityUnlocked?.Invoke();
                    onAbilityChange?.Invoke(abilities[3], 3);
                }
                break;
            case 11:
                if (!abilities[4].isUnlocked)
                {
                    abilities[4].isUnlocked = true;


                    onArrowRainAbilityUnlocked?.Invoke();
                    onAbilityChange?.Invoke(abilities[4], 4);
                }
                break;
        }
    }
    public void AddAbility(Ability abilityToAdd)
    {
        if (abilityToAdd == null)
            return;

        AbilityContainer abilityContainer = CreateAbilityContainer(abilityToAdd);
        abilities.Add(abilityContainer);
        onAbilityChange?.Invoke(abilityContainer, abilities.Count - 1);
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
        else if (ability is PotionAbility)
        {
            return new PotionAbilityContainer(ability);
        }
        else
        {
            return null;
        }
    }

    public void ActivateAbility(AbilityContainer abilityContainer)
    {
        if (abilityContainer.currentCooldown > 0f || character.Mana<abilityContainer.manaCost) { return; }
        IAbilityBehaviour abilityAction = CreateAbilityAction(abilityContainer);
        
        Vector3 targetAbility=InteractInput.interactTarget==null? MouseInput.rayToWorldPoint: InteractInput.interactTarget.GetPosition();


        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            AbilityAnimation(abilityContainer);
        }
        else
        {
            RotateToMouse();
        }
        
        abilityContainer.ability.UseAbility(transform, abilityAction, targetAbility, abilityContainer);
        character.Mana-=abilityContainer.manaCost;
        onManaUpdate?.Invoke();
        abilityContainer.Cooldown();
    }

    public void ActivateAbility(int abilityID, bool pressed)
    {
        if (abilityID >= Abilities.Count || Abilities[abilityID] == null)
            return;


        AbilityContainer abilityContainer = Abilities[abilityID];
        abilityContainer.isPressed = pressed;

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
        else if (abilityContainer.ability is PotionAbility)
        {
            return (PotionAbility)abilityContainer.ability;
        }
        else
        {
            return null;
        }
    }


    private void Update()
    {
        ProccessCooldown();
        ProccessHoldAbilities();

    }

    private void ProccessHoldAbilities()
    {
        if (CharacterStats.isDead) return;
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].isUnlocked)
            {
                if (abilities[i].isPressed)
                {
                    ActivateAbility(Abilities[i]);
                }

            }

        }
    }
    private void ProccessCooldown()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].isUnlocked)
            {
                abilities[i].ReduceCooldown(Time.deltaTime);
                onCooldownUpdate?.Invoke(abilities[i].CooldownNormalized, i);

            }

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

        RotateToMouse();
    }

    private void RotateToMouse()
    {
        Vector3 direction = (MouseInput.rayToWorldPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

        transform.DORotateQuaternion(targetRotation, 0.3f);
    }

    public void SerializeJson()
    {
        for (int i = 0; i < abilities.Count - 1; i++)
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
        try
        {
            for (int i = 0; i < abilities.Count - 1; i++)
            {
                string filePath = $"/player-ability{i + 1}.json";
                switch (i)
                {
                    case 0:
                        {
                            LoadProjSkill(i, filePath,startingAbility);
                            break;
                        }

                    case 1:
                        {
                            LoadProjSkill(i, filePath, ability1);

                            break;
                        }

                    case 2:
                        {
                            LoadDashSkill(i, filePath);
                            break;
                        }
                    case 3:
                        {
                            LoadShotGunSkill(i, filePath);
                            break ;
                        }
                    case 4:
                        {
                            LoadAreaSkill(i, filePath);
                            break;
                        }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadShotGunSkill(int i, string filePath)
    {
        ProjectileAbilityContainer proj3 = FileHandler.ReadFromJSON<ProjectileAbilityContainer>(filePath);
        if (proj3 != null)
        {

            ProjectileAbilityContainer loadedAbility = new ProjectileAbilityContainer(shotgunAbility);
            loadedAbility.SetLoadValues(proj3);
            abilities[i] = loadedAbility;
            if (abilities[i].isUnlocked)
            {
                onDashAbilityUnlocked?.Invoke();
                onAbilityChange?.Invoke(abilities[i], i);

            }
        }
      
    }

    private void LoadAreaSkill(int i, string filePath)
    {
        AreaAbilityContainer proj3 = FileHandler.ReadFromJSON<AreaAbilityContainer>(filePath);
        if (proj3 != null)
        {

            AreaAbilityContainer loadedAbility = new AreaAbilityContainer(arrowRainAbility);
            loadedAbility.SetValues(proj3);
            abilities[i] = loadedAbility;
            if (abilities[i].isUnlocked)
            {
                onDashAbilityUnlocked?.Invoke();
                onAbilityChange?.Invoke(abilities[i], i);

            }
        }
    }

    private void LoadDashSkill(int i, string filePath)
    {
        DashAbilityContainer dash = FileHandler.ReadFromJSON<DashAbilityContainer>(filePath);
        if (dash != null)
        {

            DashAbilityContainer loadedAbility3 = new DashAbilityContainer(dashAbility);
            loadedAbility3.SetLoadValues(dash);
            abilities[i] = loadedAbility3;
            if (abilities[i].isUnlocked)
            {
                onDashAbilityUnlocked?.Invoke();
                onAbilityChange?.Invoke(abilities[2], 2);

            }
        }
    }

    private void LoadProjSkill(int i, string filePath, Ability ability)
    {
        ProjectileAbilityContainer proj = FileHandler.ReadFromJSON<ProjectileAbilityContainer>(filePath);
        if (proj != null)
        {

            ProjectileAbilityContainer loadedAbility = new ProjectileAbilityContainer(ability);
            loadedAbility.SetLoadValues(proj);
            abilities[i] = loadedAbility;
        }
    }
}
