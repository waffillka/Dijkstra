
namespace SyberryTask
{
    class NodeInf
    {
        public string node;
        public bool used;
        public int sumPath;
        public string prevNode;

        public NodeInf(string node)
        {
            this.node = node;
            used = false;
            sumPath = int.MaxValue;
            prevNode = node;
        }
    }

    /// <summary>
    /// This class implement algorithm Dijkstra (find short path)
    /// </summary>
    class Dijkstra
    {
        private Graph graph;
        private System.Collections.Generic.Stack<NodeInf> info;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Finding the shortest path from one point to another.
        /// </summary>
        /// <param name="node1">Start point</param>
        /// <param name="node2">End point</param>
        /// <returns></returns>
        public void GetShortesPath(string node1, string node2)
        {
            Init();
            GetNodeInf(node1).sumPath = 0;
            string curr;
            while ((curr = FindUnusedMinimalNode()) != null)
            {
                SetSumToNextNodes(curr);
            }

            RestorePath(node1, node2);
        }

        private void RestorePath(string node1, string node2)
        {
            string path = node2.ToString();
            int steps = 0;
            int fuel = GetNodeInf(node2).sumPath;
            while (node2 != node1)
            {
                node2 = GetNodeInf(node2).prevNode;
                path = node2.ToString() + "->" + path;
                steps++;
            }

            string writePath = @"path-plan.txt";

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(path);
                sw.WriteLine("steps: " + steps);
                sw.WriteLine("fuel: " + fuel);
            }
        }

        private void SetSumToNextNodes(string curr)
        {
            NodeInf currInf = GetNodeInf(curr);
            currInf.used = true;

            foreach (NodeEdge next in graph.GetNode(curr).edges.ToArray())
            {
                int newSum = currInf.sumPath + next.wight;
                NodeInf nextInf = GetNodeInf(next.node);
                if (newSum < nextInf.sumPath)
                {
                    nextInf.sumPath = newSum;
                    nextInf.prevNode = curr;
                }
            }
        }

        private string FindUnusedMinimalNode()
        {
            int minSum = int.MaxValue;
            string minNode = null;

            foreach (string node in graph.GetNodes())
            {
                NodeInf info = GetNodeInf(node);
                if (info.used)
                {
                    continue;
                }

                if (info.sumPath < minSum)
                {
                    minSum = info.sumPath;
                    minNode = node;
                }
            }

            return minNode;
        }

        private void Init()
        {
            info = new System.Collections.Generic.Stack<NodeInf>();

            foreach (string node in graph.GetNodes())
            {
                info.Push(new NodeInf(node));
            }
        }

        private NodeInf GetNodeInf(string node)
        {
            foreach (NodeInf inf in info.ToArray())
            {
                if (inf.node == node)
                {
                    return inf;
                }
            }

            throw new System.Exception("Node not Found");
        }
    }
}
