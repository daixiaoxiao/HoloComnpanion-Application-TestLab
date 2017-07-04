using System.Numerics;

namespace MyHololenServiceHost
{
    class LineModel
    {
        private double LineStartingPoint;
        private double LineEndingPoint;

        private Vector3 PositionOfTheLineStartingPoint = new Vector3();
        private Vector3 PositionOfTheLineEndingPoint = new Vector3();

        public double LineStartingPoint1
        {
            get
            {
                return LineStartingPoint;
            }

            set
            {
                LineStartingPoint = value;
            }
        }

        public double LineEndingPoint1
        {
            get
            {
                return LineEndingPoint;
            }

            set
            {
                LineEndingPoint = value;
            }
        }

        public Vector3 PositionOfTheLineStartingPoint1
        {
            get
            {
                return PositionOfTheLineStartingPoint;
            }

            set
            {
                PositionOfTheLineStartingPoint = value;
            }
        }

        public Vector3 PositionOfTheLineEndingPoint1
        {
            get
            {
                return PositionOfTheLineEndingPoint;
            }

            set
            {
                PositionOfTheLineEndingPoint = value;
            }
        }
    }
}
