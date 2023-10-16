using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public TipoChave tipoChaveNecessaria = TipoChave.Blue;
    public AnimacaoPorta animacaoPorta = AnimacaoPorta.Blue;
}

public enum AnimacaoPorta
{
    Blue,
    Green,
    Pink,
}

