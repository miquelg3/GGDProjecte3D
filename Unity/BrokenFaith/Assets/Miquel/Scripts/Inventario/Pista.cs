using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : Item
{
    public string Contenido { get; private set; }
<<<<<<< HEAD
    public Pista(string id, string nombre) : base(id, nombre)
    {
    }
    public Pista(string id, string nombre,string contenido) : base(id, nombre)
=======
    public Pista(string id, string nombre, string contenido) : base(id, nombre, false)
>>>>>>> Feature/Miquel
    {
        Contenido = contenido;
    }
}