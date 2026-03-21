using UnityEngine;
using System.Collections.Generic;

public class ManagerStory : MonoBehaviour
{
    public enum EstadoHistoria { Inicio, BuscandoObjeto, Finalizado }
    public EstadoHistoria estadoActual = EstadoHistoria.Inicio;


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

    void AsignarRolesAleatorios()
    {
        // Creamos una lista de índices (0 a 4) y la barajamos
        List<int> indices = new List<int> { 0, 1, 2, 3, 4 };
        for (int i = 0; i < indices.Count; i++)
        {
            int temp = indices[i];
            int randomIndex = Random.Range(i, indices.Count);
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // Asignamos roles basados en la lista barajada
        idTargetInicio = indices[0];
        idTargetObjeto = indices[1];
        idTargetFinal = indices[2];

        Debug.Log("Inicio en Target " + idTargetInicio +", Objeto en Target " + idTargetObjeto +", Final en Target " + idTargetFinal);
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
