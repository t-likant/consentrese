using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LIBRERIA PARA CONTROLAR ESCENAS DEL JUEGO
using UnityEngine.UI; // NOS DEJA USAR EL CANVAS DE UNITY

public class ControlGameOver : MonoBehaviour
{
    public float tiempoLimite; // CONTADOR DE TIEMPO REGRESIVO
    int tiempo; // VARIABLE AUXILIAR PARA MOSTRAR TIEMPO EN ENTEROS
    public int contadorDeCubos; // CONTADOR DEL TOTAL DE CUBOS EN LA ESCENA
    GeneradorCubos variablesGenerador; // CONEXION CON OTRO SCRIPT DL JUEGO
    void Start()
    {
        variablesGenerador = FindObjectOfType<GeneradorCubos>(); // SACA INFORMACION DEL GENERADOR PARA CALCULAR LA CANTIDAD DE CUBOS EN LA ESCENA
        AsignarCubos(); // ASIGNA LA CANTIDAD DE CUBOS EN LA ESCENA
    }
    
    void Update()
    {
        CondicionDerrota(); // ACCIONA LA FUNCION DE DERROTA. SI SE CUMPLE
        CondicionVictoria(); // ACCIONA LA FUNCION DE VICTORIA. SI SE CUMPLE
    }

    void CondicionDerrota() // FUNCION QUE CONTROLA LA CONDICION DE DERROTA
    {
        tiempoLimite -= Time.deltaTime; // TIEMPO EN DECIMALES
        tiempo = (int)tiempoLimite; // TIEMPO EN ENTEROS. ASI ES COMO QUIERO QUE SE MUESTRE
        GetComponent<Text>().text = "Tiempo: " + tiempo.ToString(); // MUESTRA EL TIEMPO COMO UNA CADENA DE CARACTERES
        if(tiempoLimite <= 0) // CONDICION PARA PERDER
        {
            SceneManager.LoadScene("Perdiste"); // NOS LLEVA A LA ESCENA QUE MUESTRA EL MENSAJE DE DERROTA
        }
    }

    void CondicionVictoria() // FUNCION QUE CONTROLA LA CONDICION DE VICTORIA
    {
        if(contadorDeCubos == 0) // CONDICION PARA GANAR
        {
            SceneManager.LoadScene("Ganaste"); // NOS LLEVA A LA ESCENA QUE MUESTRA EL MENSAJE DE VICTORIA
        }
    }

    public void AsignarCubos() // FUNCION PARA CUBOS TOTALES
    {
        contadorDeCubos = variablesGenerador.alto * variablesGenerador.ancho; // OPERACION QUE NOS CALCULA EL NUMERO DE CUBOS EN LA ESCENA
    }
}
