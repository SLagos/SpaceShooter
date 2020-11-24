using System.Collections;
using UnityEngine;

namespace Utils
{
    public class ScreenBorderUtility
    { 
        /// <summary>
        /// Utility method to check if a certain position is with in the bounds of the screen
        /// </summary>
        /// <param name="pos">The position to be check</param>
        /// <returns>If is in bounds or no with the current screen</returns>
        public static bool InScreenBounds(Vector3 pos)
        {
            var screenPos = Camera.main.WorldToScreenPoint(pos);
            bool xBound = (screenPos.x < Screen.width && screenPos.x > 0);
            bool yBound = (screenPos.y < Screen.height && screenPos.y > 0);
            return xBound && yBound;
        }

        /// <summary>
        /// Return a valid position With in boundries based on the position given
        /// </summary>
        /// <param name="pos">Position given</param>
        /// <returns>A position with in boundries of the screen</returns>
        public static Vector3 GetValidPosition(Vector3 pos)
        {       
            if (InScreenBounds(pos))
                return pos;
            Vector3 newPos;
            var screenPos = Camera.main.WorldToScreenPoint(pos);
            if (pos.x < 0)
                pos.x = 0;
            else if (pos.x > Screen.width)
                pos.x = Screen.width;
            if (pos.y < 0)
                pos.y = 0;
            else if (pos.y > Screen.height)
                pos.y = Screen.height;

            pos.z = 10;
            newPos = Camera.main.ScreenToWorldPoint(pos);
            return newPos;            
        }
    }
}