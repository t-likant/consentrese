using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCubos : MonoBehaviour
{
    float tiempoEspera; // VARIABLE QUE CONTROLA EL TIEMPO QUE TARDAN LOS CUBOS EN VOLVER A VOLTEARSE
    public bool restriccion; // VARIABLE QUE CONTROLA EL JUEGO: SOLO PERMITE QUE SE VOLTEEN DOS CUBOS POR VEZ
    void Update()
    {
        DeterminaParecido(); // FUNCION QUE SE ENCARGA DE ENCONTRAR LA SIMILITUD DE LOS CUBOS
    }

    void DeterminaParecido()
    {
        LogicaCubo cubo_1 = null; // VARIABLES LOCALES QUE PERMITEN LA COMPARACION DE DOS CUBOS
        LogicaCubo cubo_2 = null;
        foreach (var item in FindObjectsOfType<LogicaCubo>()) // CICLO FOREACH QUE BUSCA LOS CUBOS SELECCIONADOS
        {
            if(item.seleccionado == true && cubo_1 == null)
            {
                cubo_1 = item;
            }
            if(item.seleccionado == true && cubo_1 != item && cubo_2 == null)
            {
                cubo_2 = item;
            }
        }

        if (cubo_1 != null && cubo_2 != null)
        {
            restriccion = true; // ENCONTRO 2 CUBOS Y NO DEJA GIRAR MAS HASTA QUE VUELVAN A SU POSICION ORIGINAL
            if (cubo_1.esteCubo.GetComponent<MeshRenderer>().material.ToString() == cubo_2.esteCubo.GetComponent<MeshRenderer>().material.ToString())
            {
                cubo_1.correcto = true;
                cubo_2.correcto = true;
                cubo_1.seleccionado = false;
                cubo_2.seleccionado = false;
                restriccion = false; // PERMITE GIRAR MAS CUBOS PORQUE SE ENCONTRO LA PAREJA CORRECTA
            }
            else
            {
                cubo_1.correcto = false;
                cubo_2.correcto = false;

                tiempoEspera += Time.deltaTime; // CONTEO DE TIEMPO PARA REGRESAR A LA POSICION ORIGINAL
            }

            if(tiempoEspera >=2.0f) // CONDICION PARA VOLVER A GIRAR LOS CUBOS DE PAREJAS INCORRECTAS
            {
                if (cubo_1.correcto == false && cubo_2.correcto == false)
                {
                    cubo_1.sePuedeOcultar = true;
                    cubo_2.sePuedeOcultar = true;

                    cubo_1.seleccionado = false;
                    cubo_2.seleccionado = false;
                }
                tiempoEspera = 0f; // REINICIAMOS EL TIEMPO
                restriccion = false;
            }
            
            cubo_1 = null; // DESPUES DE LA COMPARACION SE IGUALA A NULL PARA CORREGIR PROBLEMAS CON UNA DE LAS PAREJAS
            cubo_2 = null;
        }
    }
}
