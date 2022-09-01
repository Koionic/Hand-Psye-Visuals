using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFunctions
{
    public static class MathFunctions
    {
        public static void LerpValue(ref float current, float target, float lerp)
        {
            if (Mathf.Abs(target - current) <= 0.05f)
            {
                current = target;
                return;
            }

            current = Mathf.Lerp(current, target, lerp);
        }
    }
}
