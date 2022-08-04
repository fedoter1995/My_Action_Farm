using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Equipments : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Backpack _backpack;

    private WeaponPoint weaponPoint;
    private SpaceForBackpack spaceForBackpack;


    public Weapon CurrentWeapon { get; private set; }
    public Backpack CurrentBackpack { get; private set; }

    private Weapon CreateWeapon(Weapon prefab)
    {   
        var weapon = Instantiate(prefab, weaponPoint.transform);
        weapon.ChangeActivity();

        return weapon;
    }
    private Backpack CreateBackpack(Backpack prefab)
    {
        var backpack = Instantiate(prefab, spaceForBackpack.transform);
        return backpack;
    }
    public void InitEquipments()
    {
        GetPoint(out weaponPoint);
        GetPoint(out spaceForBackpack);
        InitBackpack();
        InitWeapon();
    }
    public void UpgradeWeapon(Weapon newWeapon)
    {
        Destroy(CurrentWeapon.gameObject);
        CurrentWeapon = CreateWeapon(newWeapon);
        weaponPoint.ChangeCurrentEquipment(CurrentWeapon);
    }
    private void GetPoint<T>(out T point)
    {
        try
        {
            point = GetComponentInChildren<T>();
        }
        catch
        {
            throw new Exception($"can't get a component type : {typeof(T)} ");
        }
    }
    
    private void InitWeapon()
    {
        if (_weapon == null)
        {
            var item = Resources.Load<Weapon>("Equipments/Weapons/DefaultWeapon");
            CurrentWeapon = CreateWeapon(item);
        }
        else
        {
            CurrentWeapon = CreateWeapon(_weapon);
        }
        weaponPoint.ChangeCurrentEquipment(CurrentWeapon);
    }
    private void InitBackpack()
    {
        if (_backpack == null)
        {
            var item = Resources.Load<Backpack>("Equipments/Backpack/backpack");
            CurrentBackpack = CreateBackpack(item);
        }
        else
        {
            CurrentBackpack = CreateBackpack(_backpack);
        }
        Interactor.BackpackInteractor = CurrentBackpack;
        spaceForBackpack.ChangeCurrentEquipment(CurrentBackpack);
    }
}
