using UnityEngine;
using System;

public class GameObjectUID : MonoBehaviour
{
    public string UID;

    private void Start()
    {
        UID = Guid.NewGuid().ToString(); // Convertir el Guid a un string y asignarlo a UID
    }
}
