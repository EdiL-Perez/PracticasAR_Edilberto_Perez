using UnityEngine;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI; 

public class ManagerStory : MonoBehaviour
{
    public enum EstadoHistoria { Inicio, HabloConNPC, TieneObjeto1, BuscandoObjeto, SabeUbicacionFinal, TieneObjetoFinal, Finalizado }
    public EstadoHistoria estadoActual = EstadoHistoria.Inicio;

    [Header("Prefabs de la Historia")]
    public GameObject prefabNPC;
    public GameObject prefabObjeto1;
    public GameObject prefabObjeto2;

    public GameObject Arma;
    public GameObject almohada;
    public GameObject prefabMeta;
    //public GameObject prefabObjetoMagico;
    //public GameObject prefabFinal;
    public GameObject modelo;



    public List<GameObject> targetsConfigurados;

    // Aquí guardaremos cuál marcador tiene cada evento
    private int idTargetInicio;
    private int idTargetNPC;
    private int idTargetObjeto1;
    private int idTargetObjeto2;
    private int idTargetMeta;

    [Header("Referencias de UI")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDialogo;
    public Image imagenRetrato;

    [Header("Sprites de Retratos")]
    public Sprite retratoPersonaje;
    public Sprite retratoNPC;

    private bool BienvenidaMostrada = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AsignarRolesAleatorios();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AsignarRolesAleatorios()
    {
        // Creamos una lista de índices (0 a 4) y la barajamos
        idTargetInicio = 0;
        List<int> indicesHistoria = new List<int> { 1, 2, 3, 4 };
        for (int i = 0; i < indicesHistoria.Count; i++)
        {
            int temp = indicesHistoria[i];
            int randomIndex = Random.Range(i, indicesHistoria.Count);
            indicesHistoria[i] = indicesHistoria[randomIndex];
            indicesHistoria[randomIndex] = temp;
        }

        // Asignamos roles basados en la lista barajada
        idTargetNPC = indicesHistoria[0];
        idTargetObjeto1 = indicesHistoria[1];
        idTargetObjeto2 = indicesHistoria[2];
        idTargetMeta = indicesHistoria[3];

        Debug.Log("Inicio en Target " + idTargetInicio +", Objeto1en Target " + idTargetObjeto1 +", objeto2 en Target " +idTargetObjeto2+"NPC Target"+idTargetNPC);

        UbicarObjeto(prefabNPC, idTargetNPC);
        UbicarObjeto(prefabObjeto1, idTargetObjeto1);
        UbicarObjeto(prefabObjeto2, idTargetObjeto2);
        UbicarObjeto(prefabMeta, idTargetMeta);
    }
    public void UbicarObjeto(GameObject prefab, int idTarget)
    {
        // Buscamos el Target que tiene ese ID en nuestra lista
        GameObject targetDestino = targetsConfigurados[idTarget];

        Transform ancla = targetDestino.transform.Find("ancla");
        if (ancla != null)
        {
            GameObject instancia = Instantiate(prefab, ancla);
            instancia.transform.localPosition = Vector3.zero;
            instancia.transform.localRotation = Quaternion.identity;
        }
        else
        {
            GameObject instancia = Instantiate(prefab, targetDestino.transform);
            instancia.transform.localPosition = Vector3.zero;
        }
    }

    public void CheckProgreso(int idLlegada )
    {
        ControladorEquipo equipo = modelo.GetComponent<ControladorEquipo>();
        if (idLlegada == idTargetNPC && estadoActual == EstadoHistoria.Inicio)
        {
            estadoActual = EstadoHistoria.HabloConNPC;
            ActualizarUI("TSUBAKI", "¡Hola YUKA! Me enteré que buscas tu arma, creo saber donde esta pero olvide mi almohada por el carro de comida,Si la encuentras, me la podrias traer y con gusto te digo donde buscar", retratoNPC);
            return;
        }

        if (idLlegada == idTargetObjeto1 && estadoActual == EstadoHistoria.HabloConNPC)
        {
            estadoActual = EstadoHistoria.TieneObjeto1;
            ActualizarUI("YUKA", "Debe ser esta su almohada para dormir, debo entregarla a TSUBAKI", retratoPersonaje);
            if(equipo != null)
            {
                equipo.EquiparObjeto(almohada);
            }
            return;
        }
        if (idLlegada == idTargetNPC && estadoActual == EstadoHistoria.TieneObjeto1)
        {
            estadoActual = EstadoHistoria.SabeUbicacionFinal;
            ActualizarUI("TSUBAKI", "Gracias por recuperar mi almohada, me parece que lo que buscas esta en la parte trasera del tanque", retratoNPC);
            if(equipo != null)
            {
                equipo.Desequipar();
            }
            return;
        }
        if (idLlegada == idTargetObjeto2 && estadoActual == EstadoHistoria.SabeUbicacionFinal)
        {
            estadoActual = EstadoHistoria.TieneObjetoFinal;
            ActualizarUI("YUKA", "Finalmente encontre mi arma, ahora debo ir a cumplir la mision", retratoPersonaje);
            if (equipo != null)
            {
                equipo.EquiparObjeto(Arma);
            }
            return;
        }
        if (idLlegada == idTargetMeta && estadoActual == EstadoHistoria.TieneObjetoFinal)
        {
            estadoActual = EstadoHistoria.Finalizado;
            ActualizarUI("YUKA", "Debo apresurarme ahora que tengo todo lo necesario para la mision", retratoPersonaje);
            return;
        }
        if (idLlegada == idTargetObjeto1 && estadoActual == EstadoHistoria.Inicio)
        {
            ActualizarUI("YUKA", "Hay una almohada por aqui pero de ¿quien sera?", retratoPersonaje);
            return;
        }

        if (idLlegada == idTargetMeta && estadoActual != EstadoHistoria.TieneObjetoFinal)
        {
            ActualizarUI("YUKA", "No puedo ir a entrenar si no tengo mi arma", retratoPersonaje);
            return;
        }
        ActualizarUI("YUKA", "Aqui no hay nada interesante...", retratoPersonaje);
        return;
    }

    void ActualizarUI(string nombre, string mensaje, Sprite retrato)
    {
        textoNombre.text = nombre;
        textoDialogo.text = mensaje;
        imagenRetrato.sprite = retrato;
    }
    public void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
    }
    public void IniciarNarrativa()
    {
        if (!BienvenidaMostrada)
        {
            panelDialogo.SetActive(true);
            ActualizarUI("YUKA", "Necesito encontrar mi arma", retratoPersonaje);
            BienvenidaMostrada = true; // Ya no volverá a entrar aquí
        }
    }



}
