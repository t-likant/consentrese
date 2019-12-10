using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCubos : MonoBehaviour
{
    public GameObject cuboPrefab; // GAMEOBJECT DE REFERENCIA
    public int ancho; // MEDIDAS DEL TABLERO DE JUEGO
    public int alto;
    public Transform cubosParent; // AGRUPA EN UN GAMEOBJECT TODOS LOS CUBOS GENERADOS
    public List<GameObject> cubos = new List<GameObject>(); // LISTA QUE NOS AYUDA A ASIGNAR MATERIALES

    public Material[] materials; // LISTA DE MATERIALES

    void Start()
    {
        Crear(); // FUNCION CREADORA
    }

    public void Crear()
    {
        int cont = 0; // ASIGNA ID AL CUBO
        for (int i = 0; i < alto; i++) // CICLO PARA VALORES EN Y
        {
            for (int x = 0; x < ancho; x++) // CICLO PARA VALORES EN X
            {
                GameObject cuboTemp = Instantiate(cuboPrefab, new Vector3((x*2)-5, (i*2)-5, 0), Quaternion.identity); // UBICA LOS CUBOS. 
                // LA (*) ES PARA ESPACIAR LOS CUBOS Y LA (-) PARA CENTRARLOS MAS EN EL TABLERO
                cubos.Add(cuboTemp); // INGRESA LA VARIABLE TEMPORAL A LA LISTA PARA PODER MANIPULARLOS FUERA DE LOS CICLOS FOR FACILMENTE

                cuboTemp.GetComponent<LogicaCubo>().posicionOriginal = new Vector3((x * 2) - 5, (i * 2) - 5, 0); // ASIGNA LA POSICION INICIAL
                cuboTemp.GetComponent<LogicaCubo>().numCubo = cont; // ID DEL CUBO
                cuboTemp.transform.SetParent(cubosParent);

                cont++;
            }
        }
        AsignarTexturas(); // AÑADE LAS TEXTURAS A LOS CUBOS
        Barajar(); // DESORDENA LA POSICION DE LOS CUBOS PARA EL JUEGO
    }

    void AsignarTexturas() // FUNCION DENTRO DEL SCRIPT QUE ASIGNA LAS TEXTURAS DE CADA CUBO
    {
        int contachon = 0;
        for (int i = 0; i < cubos.Count; i++)
        {
            cubos[i].GetComponent<LogicaCubo>().PonerTextura(materials[contachon / 2]); // PRIMERO LOS ASIGNA SEGUIDO. MAS ADELANTE SE BARAJAN
            contachon++;
            if (contachon > 2*(materials.Length)-1) contachon = 0; // REINICIA EL CONTADOR PARA SEGUIR USANDO SOLO LAS TEXTURAS DE LA LISTA
        }

    }

    void Barajar() // FUNCION SHUFFLE PARA BARAJAR EL JUEGO
    {
        int aleatorio;

        for (int i = 0; i < cubos.Count; i++)
        {
            aleatorio = Random.Range(i, cubos.Count); // RANDOM QUE SELECCIONA CUBOS DE MODO QUE LOS ANTERIORES NO CAMBIEN DE POSICION
            cubos[i].transform.position = cubos[aleatorio].transform.position; // EL CUBO EN ITERACION CAMBIA DE POSICION CON EL ALEATORIO SELECCIONADO
            cubos[aleatorio].transform.position = cubos[i].GetComponent<LogicaCubo>().posicionOriginal; // EL CUBO ALEATORIO CAMBIA DE POSICION CON EL ITERADO ACTUAL

            cubos[i].GetComponent<LogicaCubo>().posicionOriginal = cubos[i].transform.position; // SE ACTUALIZA LA POSICION INICIAL
            cubos[aleatorio].GetComponent<LogicaCubo>().posicionOriginal = cubos[aleatorio].transform.position; // SE ACTUALIZA LA POSICION INICIAL

        }
    }
}
