using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [field: SerializeField]
    public bool VisibleCursor { get; private set; }
    [field: SerializeField]
    public bool MainPanel { get; private set; }
}
