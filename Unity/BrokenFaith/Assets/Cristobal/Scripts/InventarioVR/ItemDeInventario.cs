using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeInventario : MonoBehaviour
{
    #region Variables
    private Item item;
    [SerializeField] private bool tieneUnItem;

    #endregion
    void Start()
    {
        if (tieneUnItem) CrearItem();
    }

   
    void Update()
    {
        
    }

    void CrearItem()
    {
        item = new Municion("0","El Flechas", true, 0, TipoMunicion.Metal, 1);
    }

    void Cojer()
    {

    }

    #region Setter & Getters
    public Item GetItem()
    {
        if (item != null) return item;
        else return null;
    }
    
    public void SetItem(Item itemNuevo)
    {
        if (itemNuevo != null) item = itemNuevo;
    }

    public string GetName()
    {
        return item.Nombre;
    }
    #endregion

}