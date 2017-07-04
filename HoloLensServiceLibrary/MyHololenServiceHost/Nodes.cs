using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HololensServiceHost
{
    class Nodes
    {
        private List<double> InternalXNodes = new List<double>();
        private List<double> InternalYNodes = new List<double>();
        private List<double> InternalZNodes = new List<double>();

        public List<double> XNodes
        {
            get
            {
                return InternalXNodes;
            }

            set
            {
                InternalXNodes = value;
            }
        }

        public List<double> YNodes
        {
            get
            {
                return InternalYNodes;
            }

            set
            {
                InternalYNodes = value;
            }
        }

        public List<double> ZNodes
        {
            get
            {
                return InternalZNodes;
            }

            set
            {
                InternalZNodes = value;
            }
        }

        public void SetNodes(Array xNodes, Array yNodes, Array zNodes)
        {
            XNodes.AddRange((double[])xNodes);
            YNodes.AddRange((double[])yNodes);
            ZNodes.AddRange((double[])zNodes);
        }
        public Nodes()
        {

        }

        public Nodes(List<double> xNodes, List<double> yNodes, List<double> zNodes)
        {
            XNodes = xNodes;
            YNodes = yNodes;
            ZNodes = zNodes;
        }
    }
}
