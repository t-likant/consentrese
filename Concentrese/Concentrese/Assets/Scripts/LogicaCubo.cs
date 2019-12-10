using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaCubo : MonoBehaviour
{
    public bool sePuedeVoltear; // VARIABLES BOOLEANAS QUE CONTROLAN EL COMPORTAMIENTO DE LOS CUBOS
    public bool sePuedeOcultar;
    public bool seleccionado;
    public bool correcto;
    public Vector3 posicionOriginal; // VARIABLE AUXILIAR QUE PERMITE BARAJAR
    public GameObject esteCubo; // VARIABLE QUE NOS AYUDA A IDENTIFICAR EL CUBO DENTRO DEL JUEGO PARA COMPARARLO
    public ControlGameOver contadorInterno; // NOS AYUDA A DETERMINAR LA CONDICION DE VICTORIA
    public ControladorCubos permisoVolteo; // NOS AYUDA A RESTRINGIR LA ACCION DE GIRO DE LOS CUBOS, SI YA SE GIRARON 2
    public int numCubo; // ID DEL CUBO EN LA LISTA
    bool activacionParticula; // NOS MUESTRA UNA PARTICULA QUE SIRVE DE RETROALIMENTACION, SI 2 CUBOS SON CORRECTOS
    float tiempoDestruccion = 0; // ELIMINA EL OBJETO DE LA ESCENA CUANDO CUMPLE SU COMETIDO


    void Awake()
    {
        esteCubo = this.gameObject; // IDENTIFICAMOS EL CUBO
        contadorInterno = FindObjectOfType<ControlGameOver>(); // UNIMOS LAS VARIABLES ENTRE SCRIPTS PARA SU FUNCION ANTES MENCIONADA
        permisoVolteo = FindObjectOfType<ControladorCubos>(); // UNIMOS LAS VARIABLES ENTRE SCRIPTS PARA SU FUNCION ANTES MENCIONADA
    }

    void Update()
    {
        MostrarCubo(); // FUNCION QUE NOS MUESTRA EL CUBO
        OcultarCubo(); // FUNCION QUE NOS OCULTA EL CUBO
        RetroAlimentacionParticulas(); // FUNCION QUE ENCIENDE LAS PARTICULAS Y DESTRUYE EL OBJETO
    }

    private void OnMouseDown() // Selecciona y comprueba si se puede girar
    {
        if (!permisoVolteo.restriccion) // CADENA DE CONDICIONALES IF QUE PERMITEN LA ACTIVACION DE LAS FUNCIONES
        {
            if (gameObject.transform.rotation.eulerAngles.y >= 0 && gameObject.transform.rotation.eulerAngles.y < 180)
            {
                sePuedeVoltear = true;
                sePuedeOcultar = false;
            }
        }
        
    }

    void RetroAlimentacionParticulas() // FUNCION QUE NOS MUESTRA LA ULTIMA ACCION DEL CUBO
    {
        
        if (tiempoDestruccion == 0 && correcto) // NOTA: CONDICION UNICA DE EJECUCION. 
            //DEBIDO A QUE LA REPETICION EN EL UPDATE RESETEA LA REPRODUCCION DE LAS PARTICULAS Y AL FINAL NO SE VEN
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        if (correcto)
        {
            tiempoDestruccion += Time.deltaTime; // INICIA EL TIEMPO PARA QUE SE DESTRUYA EL OBJETO
        }
        if (tiempoDestruccion >= 2.5f) // NOTA: FUE NECESARIO SEPARAR LA DESTRUCCION PARA QUE SE PUEDIESEN VER LAS PARTICULAS
        {
            contadorInterno.contadorDeCubos--; // DESCUENTA DEL TOTAL DE CUBOS CREADOS 1 CUBO. AL SER PAREJAS EN REALIDAD SE RESTA 2
            Destroy(gameObject); // DESTRUYE ESTE OBJETO
        }
    }

    public void PonerTextura(Material textura) // FUNCION QUE ASIGNA EL MATERIAL O TEXTURA AL CUBO. SE LLAMA EN EL GENERADOR
    {
        GetComponent<MeshRenderer>().material = textura;
    }

    void MostrarCubo() // FUNCION QUE ROTA Y MUESTRA EL CUBO
    {
        if (sePuedeVoltear) // Se muestra
        {
            gameObject.GetComponent<Transform>().Rotate(0f, 180f * Time.deltaTime * 0.9f, 0f);
            seleccionado = true;
            sePuedeOcultar = false;

            if (gameObject.transform.rotation.eulerAngles.y > 180f) // CONDICIONAL QUE CALCULA LA CANTIDAD ROTADA Y DETIENE LA FUNCION
            {
                sePuedeVoltear = false;
                gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }

    void OcultarCubo() // FUNCION QUE ROTA Y OCULTA EL CUBO
    {
        if (sePuedeOcultar && !correcto) // Se muestra
        {
            gameObject.GetComponent<Transform>().Rotate(0f, -180f * Time.deltaTime * 0.9f, 0f);
            seleccionado = false;
            sePuedeVoltear = false;
            if (gameObject.transform.rotation.eulerAngles.y > 180f) // CONDICIONAL QUE CALCULA LA CANTIDAD ROTADA Y DETIENE LA FUNCION
            {
                sePuedeOcultar = false;
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        if (correcto) // CONDICION QUE CORRIGE ERROR EN LA COMPARACION DONDE EL SEGUNDO CUBO SELECCIONADO PERMANECE SELECCIONADO Y NO LO OCULTA
        {
            seleccionado = false;
        }
        
    }
}
