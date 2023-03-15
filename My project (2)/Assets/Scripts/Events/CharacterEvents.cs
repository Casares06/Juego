using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class CharacterEvents
{
    public static UnityAction <GameObject, int> characterDamaged;
    public static UnityAction <GameObject, int> characterHealed;
}


