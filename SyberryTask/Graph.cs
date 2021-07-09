
namespace SyberryTask
{
    public struct NodeEdge
    {
        public string node;
        public int wight;

        public NodeEdge(string node, int wight)
        {
            this.node = node;
            this.wight = wight;
        }
    }

    public struct GraphNode
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
            nodes.ToArray();

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
}
