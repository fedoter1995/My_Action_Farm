using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public sealed class Backpack : Equipment
{
    [SerializeField] private BackpackInfo _backpackInfo;

    private List<IBackpackSlot> _slots;
    //private float offset = 0.012f;

    #region Events
    public event Action<int,int> OnInventoryAddedEvent;
    public event Action<int,int> OnInventoryRemovedEvent;
    #endregion 
    public float Offset => _backpackInfo.ItemOffset;
    public int ItemAmount => GetItemAmount();
    public int Capacity => _backpackInfo.Capacity;

    public void Awake()
    {        
        _slots = new List<IBackpackSlot>(Capacity);
        for (int i = 0; i < Capacity; i++)
            _slots.Add(new BackpackSlot());
    }
    public IBackpackSlot GetLastSlotWithItem()
    {
        var lastItemIndex = _slots.FindLastIndex(slot => !slot.isEmpty);
        if(lastItemIndex >= 0)
            return _slots[lastItemIndex];
        return null;
    }
    public IBackpackItem GetItem(string itemID)
    {
        return _slots.Find(slot => slot.itemID == itemID).item;
    }
    public int GetItemAmount()
    {
        var amount = 0;
        var requairedSlots = _slots.FindAll(slot => !slot.isEmpty);
        foreach (var slot in requairedSlots)
            amount++;

        return amount;
    }
    public bool TryToAddToBackpack(IBackpackItem item)
    {
        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return TryToAddToSlot(emptySlot, item);

        return false;
    }
    private bool TryToAddToSlot(IBackpackSlot slot, IBackpackItem item)
    {
        if (slot.isEmpty)
        {
            slot.SetItem(item);
            var index = _slots.IndexOf(slot);
            item.ItemGameObject.transform.SetParent(gameObject.transform);
            item.ItemGameObject.transform.position = transform.position;
            item.ItemGameObject.transform.rotation = transform.rotation;
            item.ItemGameObject.transform.Translate(new Vector3(0, Offset * index, 0), Space.Self);
            OnInventoryAddedEvent?.Invoke(Capacity, ItemAmount);
            return true;
        }

        return TryToAddToBackpack(item);
    }
    public void RemooveItem(IBackpackSlot slotWithItem)
    {
        slotWithItem.Clear();
        OnInventoryRemovedEvent?.Invoke(Capacity, ItemAmount);
    }
    private IEnumerator RemooveItemRoutine(Barn barn)
    {
        var slotWithItem = GetLastSlotWithItem();
        while (slotWithItem != null)
        {
            slotWithItem = GetLastSlotWithItem();
            if(slotWithItem != null)
            {
                slotWithItem.item.ItemGameObject.transform.SetParent(barn.transform);
                slotWithItem.item.ItemGameObject.transform.DOMove(barn.transform.position, 1f);
                RemooveItem(slotWithItem);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield break;
            }
        }

    }
    public void StartRemoovedItems(Barn barn)
    {
        StartCoroutine(RemooveItemRoutine(barn));
    }
    public void StopRemoovedItems()
    {
        StopAllCoroutines();
    }
    public override IEquipmentStats GetStats()
    {
        return _backpackInfo;
    }
}
