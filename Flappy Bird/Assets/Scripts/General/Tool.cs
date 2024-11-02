using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace FlappyBird
{
    public static class Tool
    {
        public static Vector2 CalcWorldSize(Sprite sprite, Vector3 scale)
        {
            return sprite.rect.size / 100 * (Vector2)scale;
        }

        public static async Task Timer(Action action, Func<bool> continueCondition, float waitTime, Func<bool> pauseCondition, int timeStep = 500, float passedTime = 0)
        {
            if (continueCondition())
            {
                if (passedTime > waitTime)
                {
                    action();
                }
                else
                {
                    await Task.Delay(timeStep);
                    await Timer(action, continueCondition, waitTime, pauseCondition, timeStep, passedTime + timeStep / 1000f);
                }
            }
            else if (pauseCondition())
            {
                await Task.Delay(100);
                await Timer(action, continueCondition, waitTime, pauseCondition, timeStep, passedTime);
            }
        }

        public static bool IsScreenPressed()
        {
            return Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began;
        }

        public static Vector2 Abs(Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
        }

        public static void DrawRect(Rect rect, Color color)
        {
            Debug.DrawLine(rect.min, rect.min + Vector2.right * rect.width, color);
            Debug.DrawLine(rect.min, rect.min + Vector2.up * rect.height, color);
            Debug.DrawLine(rect.min + Vector2.right * rect.width, rect.max, color);
            Debug.DrawLine(rect.min + Vector2.up * rect.height, rect.max, color);
        }
    }
}
