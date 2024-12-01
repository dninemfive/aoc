namespace d9.aoc.core;
public class Tree<T>(T value, IEnumerable<Tree<T>>? children = null)
{
    public IEnumerable<Tree<T>> Children = children ?? Enumerable.Empty<Tree<T>>();
    public T Value = value;
    public IEnumerable<T> DepthFirstEnumeration
    {
        get
        {
            foreach (Tree<T> child in Children)
            {
                foreach (T item in child.DepthFirstEnumeration)
                    yield return item;
            }
            yield return Value;
        }
    }
}