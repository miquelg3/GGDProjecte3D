using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cuerpo : Salud
{
    public List<Salud> Partes { get; private set; }
    private int v, ve;
    public Cuerpo()
    { 
        if(PlayerPrefs.GetInt("Vida").Equals(null) || PlayerPrefs.GetInt("Vida") == 0)
        {
            v = 200;
            ve = v / 2;
        }
        else
        {
            v = PlayerPrefs.GetInt("Vida");
            ve = v / 2;
        }
            

            Partes = new List<Salud>();
            /** Salud Cabeza = new Cabeza(200);
             Salud Torso = new Torso(200);
             Salud BrazoI = new Brazos(100);
             Salud BrazoD = new Brazos(100);
             Salud PiernaD = new Piernas(100);
             Salud PiernaI = new Piernas(100);*/
            Salud Cabeza = new Cabeza(v);
            Salud Torso = new Torso(v);
            Salud BrazoI = new Brazos(ve);
            Salud BrazoD = new Brazos(ve);
            Salud PiernaD = new Piernas(ve);
            Salud PiernaI = new Piernas(ve);
            Partes.Add(Cabeza);
            Partes.Add(Torso);
            Partes.Add(BrazoI);
            Partes.Add(BrazoD);
            Partes.Add(PiernaD);
            Partes.Add(PiernaI);
            ListaSalud = Partes;
        
    }

    public override void Curado()
    {
        throw new System.NotImplementedException();
    }

    public override void Herida()
    {
        throw new System.NotImplementedException();
    }

    public override void Infeccion()
    {
        throw new System.NotImplementedException();
    }

}
