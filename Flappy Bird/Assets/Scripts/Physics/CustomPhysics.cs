using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Physics
{
    public static class CustomPhysics
    {
        public static bool IsRectangleIntersect(Rect a, Rect b)
        {
            Tool.DrawRect(a, new Color(1, 1, 0, 1));
            Tool.DrawRect(b, Color.red);
            return IsLineIntersect(a.xMin, a.xMax, b.xMin, b.xMax) && IsLineIntersect(a.yMin, a.yMax, b.yMin, b.yMax);
        }

        private static bool IsLineIntersect(float a1, float a2, float b1, float b2)
        {
            return IsPointInLine(a1, a2, b1) || IsPointInLine(a1, a2, b2) || IsPointInLine(b1, b2, a1) || IsPointInLine(b1, b2, a2);
        }

        private static bool IsPointInLine(float l1, float l2, float point)
        {
            if (l1 <= l2)
            {
                return point >= l1 && point <= l2;
            }
            return point >= l2 && point <= l1;
        }
    }
}
