using System;

namespace TyperPad.Common.Model
{
    public class Angle
    {
        private readonly int _angle;

        public Angle(double input)
        {
            _angle = (int) FixAngle(input);
        }

        public bool IsBetween(Angle minAngle, Angle maxAngle)
        {
            if (minAngle._angle > maxAngle._angle)
            {
                return (minAngle._angle <= _angle && _angle <= 360)
                       || (maxAngle._angle > _angle && _angle >= 0);
            }

            return minAngle._angle <= _angle && maxAngle._angle > _angle;
        }
       
        private double FixAngle(double val)
        {
            if (val < 0)
            {
                return 360 - (Math.Abs(val) % 360);
            }
            else if (val > 360)
            {
                return val % 360;
            }
            else
            {
                return val;
            }
        }
    }
}