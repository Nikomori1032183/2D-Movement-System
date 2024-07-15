using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [TextArea] public string text;
    public Dialogue previousDialogue;
    public Dialogue nextDialogue;
}
