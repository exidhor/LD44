using UnityEngine;
using System.Collections;

namespace Tools
{
    public static class MathHelper
    {
        public static float DeltaAngle(float from, float to)
        {
            from = GetAngle360(from);
            to = GetAngle360(to);

            return DeltaAngle360(from, to);
        }

        public static float DeltaAngle360(float from360, float to360)
        {
            if(from360 > to360)
            {
                return 360 - from360 + to360;
            }
            else
            {
                return to360 - from360;
            }
        }

        public static float GetAngle360(float angle)
        {
            angle %= 360;

            if(angle < 0)
            {
                angle = 360 - angle;
            }

            return angle;
        }
    }
}