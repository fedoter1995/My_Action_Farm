using System;
using UnityEngine;


[RequireComponent(typeof(PlayerAnimatorController), typeof(Equipments))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private PlayerController _playerController;


    private IPlayerAnimatorController animatorController;
    private Equipments equipments;

    #region Events

    #endregion
    public Equipments Equipments => equipments;
    public PlayerController PlayerController => _playerController;

    private void Awake()
    {
        PlayerInit();
    }
    
    public void PlayerInit()
    {
        Interactor.PlayerInteractor = this;
        animatorController = GetComponent<PlayerAnimatorController>();
        equipments = GetComponent<Equipments>();
        _playerController.InitController(animatorController);
        equipments.InitEquipments();
    }

    #region Updates
    private void Update()
    {
        animatorController.SetMoveFloatValue(_playerController.UserInput.MoveFloat);
    }
    private void FixedUpdate()
    {
        _playerController.Move(_stats.Speed, transform);
    }
    #endregion
    #region Interaction with the barn
    private void EnterTheBarn(Collider other)
    {
        var exchangeZone = other.GetComponent<ExchangeZone>();
        if (exchangeZone != null)
        {
            var barn = other.GetComponentInParent<Barn>();
            if (barn != null)
                equipments.CurrentBackpack.StartRemoovedItems(barn);
        }
    }
    private void ExitTheBarn(Collider other)
    {
        var exchangeZone = other.GetComponent<ExchangeZone>();
        if (exchangeZone != null)
            equipments.CurrentBackpack.StopRemoovedItems();
    }
    #endregion
    public void OnTriggerEnter(Collider other)
    {
        EnterTheBarn(other);
    }
    public void OnTriggerExit(Collider other)
    {
        ExitTheBarn(other);
    }

}
