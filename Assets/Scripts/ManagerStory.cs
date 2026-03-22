using UnityEngine;
using System.Collections.Generic;

public class ManagerStory : MonoBehaviour
{
    public enum EstadoHistoria { Inicio, BuscandoObjeto, Finalizado }
    public EstadoHistoria estadoActual = EstadoHistoria.Inicio;

    [Header("Prefabs de la Historia")]
    public GameObject prefabObjetoMagico;
    public GameObject prefabFinal;


    public List<GameObject> targetsConfigurados;

    // Aquí guardaremos cuál marcador tiene cada evento
    private int idTargetInicio;
    private int idTargetObjeto;
    private int idTargetFinal;



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
        idTargetObjeto = indicesHistoria[0];
        idTargetFinal = indicesHistoria[1];

        Debug.Log("Inicio en Target " + idTargetInicio +", Objeto en Target " + idTargetObjeto +", Final en Target " + idTargetFinal);

        UbicarObjeto(prefabObjetoMagico, idTargetObjeto);
        UbicarObjeto(prefabFinal, idTargetFinal);
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
    public string CheckProgreso(int idLlegada)
    {
        if (estadoActual == EstadoHistoria.Inicio && idLlegada == idTargetInicio)
        {
            estadoActual = EstadoHistoria.BuscandoObjeto;
            return "¡Hola! Necesito que encuentres la llave mágica.";
        }

        if (estadoActual == EstadoHistoria.BuscandoObjeto && idLlegada == idTargetObjeto)
        {
            estadoActual = EstadoHistoria.Finalizado;
            return "¡Encontraste la llave! Ahora ve a la salida.";
        }

        if (estadoActual == EstadoHistoria.Finalizado && idLlegada == idTargetFinal)
        {
            return "¡Felicidades! Has completado la historia.";
        }

        return "Aquí no hay nada interesante...";
    }



}
