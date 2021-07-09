

namespace SyberryTask
{


    public class Rover
    {

        public static void CalculateRoverPath(int[,] map)
        {
            Graph graph = new Graph();
            int rows = map.GetUpperBound(0) + 1;
            int columns = map.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    graph.AddNode($"[{i}][{j}]");
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i != rows - 1)
                    {
                        graph.AddEdge($"[{i}][{j}]", $"[{i + 1}][{j}]", System.Math.Abs(map[i, j] - map[i + 1, j]) + 1);
                    }

                    if (j != columns - 1)
                    {
                        graph.AddEdge($"[{i}][{j}]", $"[{i}][{j + 1}]", System.Math.Abs(map[i, j] - map[i, j + 1]) + 1);
                    }
                }
            }

            new Dijkstra(graph).GetShortesPath("[0][0]", $"[{rows - 1}][{columns - 1}]");
        }

        /// <summary>
        /// Structure storing information about an edge
        /// </summary>
        //    struct NodeEdge
        //    {
        //        public string node; //the name of the node to which the edge goes
        //        public int wight; //weight of the edge

        //        public NodeEdge(string node, int wight)
        //        {
        //            this.node = node;
        //            this.wight = wight;
        //        }
        //    }

        //    /// <summary>
        //    /// Structure storing the node of the graph and its connections
        //    /// </summary>
        //    struct GraphNode
        //    {
        //        public string node; //name of the node
        //        public System.Collections.Generic.Stack<NodeEdge> edges; // node's edges

        //        public GraphNode(string node)
        //        {
        //            this.node = node;
        //            this.edges = new System.Collections.Generic.Stack<NodeEdge>();
        //        }
        //    }

        //    /// <summary>
        //    /// This class implements the structure of a weighted undirected graph
        //    /// </summary>
        //    class Graph
        //    {
        //        private System.Collections.Generic.Stack<GraphNode> nodes;

        //        public Graph()
        //        {
        //            nodes = new System.Collections.Generic.Stack<GraphNode>();
        //        }

        //        /// <summary>
        //        /// adding a graph node
        //        /// </summary>
        //        /// <param name="node">graph node name</param>
        //        public void AddNode(string node)
        //        {
        //            GraphNode graphNode = new GraphNode(node);
        //            nodes.Push(graphNode);
        //        }

        //        /// <summary>
        //        /// Adding graph edges
        //        /// </summary>
        //        /// <param name="node1">first node name</param>
        //        /// <param name="node2">second node name</param>
        //        /// <param name="wight">weight of the edge</param>
        //        public void AddEdge(string node1, string node2, int wight)
        //        {
        //            GetNode(node1).edges.Push(new NodeEdge(node2, wight));
        //            GetNode(node2).edges.Push(new NodeEdge(node1, wight));
        //        }

        //        public System.Collections.Generic.IEnumerable<string> GetNodes()
        //        {
        //            foreach (GraphNode graphNode in nodes.ToArray())
        //                yield return graphNode.node;
        //        }

        //        public GraphNode GetNode(string node)
        //        {
        //            foreach (GraphNode graphNode in nodes.ToArray())
        //            {
        //                if (graphNode.node == node)
        //                {
        //                    return graphNode;
        //                }
        //            }

        //            throw new System.Exception("Node not Found");
        //        }
        //    }

        //    /// <summary>
        //    /// A class that helps to implement the Dijkstra algorithm, which stores the necessary information about the node
        //    /// </summary>
        //    class NodeInf
        //    {
        //        public string node { get; set; } //name node
        //        public bool used { get; set; } //flag indicating whether the node is passed or not
        //        public int sumPath { get; set; } //sum of edges up to a given node 
        //        public string prevNode { get; set; } // name of previous node

        //        public NodeInf(string node)
        //        {
        //            this.node = node;
        //            used = false;
        //            sumPath = int.MaxValue;
        //            prevNode = node;
        //        }
        //    }

        //    /// <summary>
        //    /// Dijkstra's algorithm class
        //    /// </summary>
        //    class Dijkstra
        //    {
        //        private Graph graph;
        //        private System.Collections.Generic.Stack<NodeInf> info;

        //        public Dijkstra(Graph graph)
        //        {
        //            this.graph = graph;
        //        }

        //        /// <summary>
        //        /// Finding the shortest path from one point to another.
        //        /// </summary>
        //        /// <param name="node1">Start point</param>
        //        /// <param name="node2">End point</param>
        //        /// <returns></returns>
        //        public void GetShortesPath(string node1, string node2)
        //        {
        //            Init();
        //            GetNodeInf(node1).sumPath = 0;
        //            string curr;
        //            while ((curr = FindUnusedMinimalNode()) != null)
        //            {
        //                SetSumToNextNodes(curr);
        //            }

        //            RestorePath(node1, node2);
        //        }

        //        /// <summary>
        //        /// The function restores the path from one node to another and write to file path and other information
        //        /// </summary>
        //        /// <param name="node1">first node of the path</param>
        //        /// <param name="node2">second node of the path</param>
        //        private void RestorePath(string node1, string node2)
        //        {
        //            string path = node2.ToString();
        //            int steps = 0;
        //            int fuel = GetNodeInf(node2).sumPath;
        //            while (node2 != node1)
        //            {
        //                node2 = GetNodeInf(node2).prevNode;
        //                path = node2.ToString() + "->" + path;
        //                steps++;
        //            }

        //            string writePath = @"path-plan.txt";

        //            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(writePath, false, System.Text.Encoding.Default))
        //            {
        //                sw.WriteLine(path);
        //                sw.WriteLine("steps: " + steps);
        //                sw.WriteLine("fuel: " + fuel);
        //            }
        //        }

        //        private void SetSumToNextNodes(string curr)
        //        {
        //            NodeInf currInf = GetNodeInf(curr);
        //            currInf.used = true;

        //            foreach (NodeEdge next in graph.GetNode(curr).edges.ToArray())
        //            {
        //                int newSum = currInf.sumPath + next.wight;
        //                NodeInf nextInf = GetNodeInf(next.node);
        //                if (newSum < nextInf.sumPath)
        //                {
        //                    nextInf.sumPath = newSum;
        //                    nextInf.prevNode = curr;
        //                }
        //            }
        //        }

        //        private string FindUnusedMinimalNode()
        //        {
        //            int minSum = int.MaxValue;
        //            string minNode = null;

        //            foreach (string node in graph.GetNodes())
        //            {
        //                NodeInf info = GetNodeInf(node);
        //                if (info.used)
        //                {
        //                    continue;
        //                }

        //                if (info.sumPath < minSum)
        //                {
        //                    minSum = info.sumPath;
        //                    minNode = node;
        //                }
        //            }

        //            return minNode;
        //        }

        //        private void Init()
        //        {
        //            info = new System.Collections.Generic.Stack<NodeInf>();

        //            foreach (string node in graph.GetNodes())
        //            {
        //                info.Push(new NodeInf(node));
        //            }
        //        }

        //        private NodeInf GetNodeInf(string node)
        //        {
        //            foreach (NodeInf inf in info.ToArray())
        //            {
        //                if (inf.node == node)
        //                {
        //                    return inf;
        //                }
        //            }

        //            throw new System.Exception("Node not Found");
        //        }
        //    }
        //}
    }
}

/**public class Rover
{
    public static void CalculateRoverPath(int[,] map)
    {
        Graph graph = new Graph();
        int rows = map.GetUpperBound(0) + 1;
        int columns = map.Length / rows;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                graph.AddNode($"[{i}][{j}]");
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i != rows - 1)
                {
                    graph.AddEdge($"[{i}][{j}]", $"[{i + 1}][{j}]", System.Math.Abs(map[i, j] - map[i + 1, j]) + 1);
                }

                if (j != columns - 1)
                {
                    graph.AddEdge($"[{i}][{j}]", $"[{i}][{j + 1}]", System.Math.Abs(map[i, j] - map[i, j + 1]) + 1);
                }
            }
        }

        new Dijkstra(graph).GetShortesPath("[0][0]", $"[{rows - 1}][{columns - 1}]");
    }

    struct NodeEdge
    {
        public string node;
        public int wight;

        public NodeEdge(string node, int wight)
        {
            this.node = node;
            this.wight = wight;
        }
    }

    struct GraphNode
    {
        public string node;
        public System.Collections.Generic.Stack<NodeEdge> edges;

        public GraphNode(string node)
        {
            this.node = node;
            this.edges = new System.Collections.Generic.Stack<NodeEdge>();
        }
    }

    /// <summary>
    /// This class implement struct of graph
    /// </summary>
    class Graph
    {
        private System.Collections.Generic.Stack<GraphNode> nodes;

        public Graph()
        {
            nodes = new System.Collections.Generic.Stack<GraphNode>();
        }

        public void AddNode(string node)
        {
            GraphNode graphNode = new GraphNode(node);
            nodes.Push(graphNode);
        }

        public void AddEdge(string node1, string node2, int wight)
        {
            GetNode(node1).edges.Push(new NodeEdge(node2, wight));
            GetNode(node2).edges.Push(new NodeEdge(node1, wight));
        }

        public System.Collections.Generic.IEnumerable<string> GetNodes()
        {
            foreach (GraphNode graphNode in nodes.ToArray())
                yield return graphNode.node;
        }

        public GraphNode GetNode(string node)
        {
            foreach (GraphNode graphNode in nodes.ToArray())
            {
                if (graphNode.node == node)
                {
                    return graphNode;
                }
            }

            throw new System.Exception("Node not Found");
        }
    }

    class NodeInf
    {
        public string node { get; set; }
        public bool used { get; set; }
        public int sumPath { get; set; }
        public string prevNode { get; set; }

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
}*/

