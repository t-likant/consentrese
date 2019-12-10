using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    float tiempoRegreso = 7f; // TIEMPO DE ESPERA PARA VOLVER
    void Start()
    {
        Invoke("VolverAlMenu", tiempoRegreso); // ESPERAMOS EL TIEMPO DE REGRESO PARA VOLVER AL MENU DE DIFICULTADES
    }

    void VolverAlMenu()
    {
        SceneManager.LoadScene("SeleccionarDificultad"); // REGRESAMOS A LA ESCENA DE SELECCIONAR EL MENU
    }
}
