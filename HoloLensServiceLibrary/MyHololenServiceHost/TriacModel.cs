using System.Numerics;

namespace MyHololenServiceHost
{
    class TriacModel
    {
        private double TriacStartingPoint;
        private double TriacMiddlePoint;
        private double TriacEndingPoint;

        private Vector3 PositionOfTheTriacStartingPoint = new Vector3();
        private Vector3 PositionOfTheTriacMiddlePoint = new Vector3();
        private Vector3 PositionOfTheTriacEndingPoint = new Vector3();

        public double TriacStartingPoint1
        {
            get
            {
                return TriacStartingPoint;
            }

            set
            {
                TriacStartingPoint = value;
            }
        }

        public double TriacMiddlePoint1
        {
            get
            {
                return TriacMiddlePoint;
            }

            set
            {
                TriacMiddlePoint = value;
            }
        }

        public double TriacEndingPoint1
        {
            get
            {
                return TriacEndingPoint;
            }

            set
            {
                TriacEndingPoint = value;
            }
        }

        public Vector3 PositionOfTheTriacStartingPoint1
        {
            get
            {
                return PositionOfTheTriacStartingPoint;
            }

            set
            {
                PositionOfTheTriacStartingPoint = value;
            }
        }

        public Vector3 PositionOfTheTriacMiddlePoint1
        {
            get
            {
                return PositionOfTheTriacMiddlePoint;
            }

            set
            {
                PositionOfTheTriacMiddlePoint = value;
            }
        }

        public Vector3 PositionOfTheTriacEndingPoint1
        {
            get
            {
                return PositionOfTheTriacEndingPoint;
            }

            set
            {
                PositionOfTheTriacEndingPoint = value;
            }
        }
    }
}
