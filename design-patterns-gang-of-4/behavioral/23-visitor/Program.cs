var tree =
    new Node<int>
    {
        Left = new Leaf<int> { Value = 1 },
        Right = new Node<int>
        {
            Left = new Leaf<int> { Value = 2 },
            Right = new Leaf<int> { Value = 3 }
        }
    };

var leafCounter = new TreeLeafCounter<int>();
tree.Accept(leafCounter);
Console.WriteLine(leafCounter.Count);

var nodeCounter = new TreeLeafCounter<int>();
tree.Accept(nodeCounter);
Console.WriteLine(nodeCounter.Count);

var treeSummer = new TreeSummer();
tree.Accept(treeSummer);
Console.WriteLine(treeSummer.Sum);


abstract class TreePart<T>
{
    public abstract void Accept(TreeVisitor<T> visitor);
}

class Leaf<T> : TreePart<T>
{
    public T Value { get; init; }

    public override void Accept(TreeVisitor<T> visitor)
    {
        visitor.VisitLeaf(this);
    }
}

class Node<T> : TreePart<T>
{
    public TreePart<T> Left { get; init; }
    public TreePart<T> Right { get; init; }
    public override void Accept(TreeVisitor<T> visitor)
    {
        Left.Accept(visitor);
        Right.Accept(visitor);
        visitor.VisitNode(this);
    }
}

class TreeVisitor<T>
{
    public virtual void VisitLeaf(Leaf<T> leaf)
    {
    }

    public virtual void VisitNode(Node<T> node)
    {
    }
}
class TreeLeafCounter<T> : TreeVisitor<T>
{
    public int Count { get; private set; } = 0;
    public override void VisitLeaf(Leaf<T> leaf)
    {
        Count++;
    }
}

class TreeNodeCounter<T> : TreeVisitor<T>
{
    public int Count { get; private set; } = 0;
    public override void VisitNode(Node<T> leaf)
    {
        Count++;
    }
}

class TreeSummer : TreeVisitor<int>
{
    public int Sum { get; private set; } = 0;
    public override void VisitLeaf(Leaf<int> leaf)
    {
        Sum += leaf.Value;
    }
}
