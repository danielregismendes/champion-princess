using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public struct Dialogue
{
    public Sprite dialogeImage;
    public string name;
    [TextArea(0,10)]
    public string text;
}

[CreateAssetMenu(fileName = "DialogeData", menuName="ScriptableObject/TalkScript",order=1)]
public class DialogeData : ScriptableObject
{
    
    public List<Dialogue> talkScript;

}
