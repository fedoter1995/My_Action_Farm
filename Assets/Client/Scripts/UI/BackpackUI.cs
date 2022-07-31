using System.Collections;
using TMPro;
using UnityEngine;

public sealed class BackpackUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void ChangeBackpackValues(int capacity, int amount)
    {
        text.text = $"{amount}/{capacity}";
    }
    //Initialized on Start
    public void Initialize()
    {

        var backpack = Interactor.BackpackInteractor;
        if (backpack != null)
        {
            backpack.OnInventoryAddedEvent += ChangeBackpackValues;
            backpack.OnInventoryRemovedEvent += ChangeBackpackValues;
        }
        else
            throw new System.Exception("Can't find the initialized backpack");        
    }
}
