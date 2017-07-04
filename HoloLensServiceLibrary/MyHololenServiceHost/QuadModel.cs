using System.Numerics;

namespace MyHololenServiceHost
{
    class QuadModel
    {
        private double Quads1Point;
        private double Quads2Point;
        private double Quads3Point;
        private double Quads4Point;

        private Vector3 PositionOfTheQuads1Point = new Vector3();
        private Vector3 PositionOfTheQuads2Point = new Vector3();
        private Vector3 PositionOfTheQuads3Point = new Vector3();
        private Vector3 PositionOfTheQuads4Point = new Vector3();

        public double Quads1Point1
        {
            get
            {
                return Quads1Point;
            }

            set
            {
                Quads1Point = value;
            }
        }

        public double Quads2Point1
        {
            get
            {
                return Quads2Point;
            }

            set
            {
                Quads2Point = value;
            }
        }

        public double Quads3Point1
        {
            get
            {
                return Quads3Point;
            }

            set
            {
                Quads3Point = value;
            }
        }

        public double Quads4Point1
        {
            get
            {
                return Quads4Point;
            }

            set
            {
                Quads4Point = value;
            }
        }

        public Vector3 PositionOfTheQuads1Point1
        {
            get
            {
                return PositionOfTheQuads1Point;
            }

            set
            {
                PositionOfTheQuads1Point = value;
            }
        }

        public Vector3 PositionOfTheQuads2Point1
        {
            get
            {
                return PositionOfTheQuads2Point;
            }

            set
            {
                PositionOfTheQuads2Point = value;
            }
        }

        public Vector3 PositionOfTheQuads3Point1
        {
            get
            {
                return PositionOfTheQuads3Point;
            }

            set
            {
                PositionOfTheQuads3Point = value;
            }
        }

        public Vector3 PositionOfTheQuads4Point1
        {
            get
            {
                return PositionOfTheQuads4Point;
            }

            set
            {
                PositionOfTheQuads4Point = value;
            }
        }
    }
}
