namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class HtmlNodeCollection : IEnumerable
    {
        private ArrayList _items = new ArrayList();
        private HtmlNode _parentnode;

        internal HtmlNodeCollection(HtmlNode parentnode)
        {
            this._parentnode = parentnode;
        }

        internal void Add(HtmlNode node)
        {
            this._items.Add(node);
        }

        internal void Append(HtmlNode node)
        {
            HtmlNode node2 = null;
            if (this._items.Count > 0)
            {
                node2 = (HtmlNode) this._items[this._items.Count - 1];
            }
            this._items.Add(node);
            node._prevnode = node2;
            node._nextnode = null;
            node._parentnode = this._parentnode;
            if (node2 != null)
            {
                if (node2 == node)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }
                node2._nextnode = node;
            }
        }

        internal void Clear()
        {
            foreach (HtmlNode node in this._items)
            {
                node._parentnode = null;
                node._nextnode = null;
                node._prevnode = null;
            }
            this._items.Clear();
        }

        public HtmlNodeEnumerator GetEnumerator()
        {
            return new HtmlNodeEnumerator(this._items);
        }

        internal int GetNodeIndex(HtmlNode node)
        {
            for (int i = 0; i < this._items.Count; i++)
            {
                if (node == ((HtmlNode) this._items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        internal void Insert(int index, HtmlNode node)
        {
            HtmlNode node2 = null;
            HtmlNode node3 = null;
            if (index > 0)
            {
                node3 = (HtmlNode) this._items[index - 1];
            }
            if (index < this._items.Count)
            {
                node2 = (HtmlNode) this._items[index];
            }
            this._items.Insert(index, node);
            if (node3 != null)
            {
                if (node == node3)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }
                node3._nextnode = node;
            }
            if (node2 != null)
            {
                node2._prevnode = node;
            }
            node._prevnode = node3;
            if (node2 == node)
            {
                throw new InvalidProgramException("Unexpected error.");
            }
            node._nextnode = node2;
            node._parentnode = this._parentnode;
        }

        internal void Prepend(HtmlNode node)
        {
            HtmlNode node2 = null;
            if (this._items.Count > 0)
            {
                node2 = (HtmlNode) this._items[0];
            }
            this._items.Insert(0, node);
            if (node == node2)
            {
                throw new InvalidProgramException("Unexpected error.");
            }
            node._nextnode = node2;
            node._prevnode = null;
            node._parentnode = this._parentnode;
            if (node2 != null)
            {
                node2._prevnode = node;
            }
        }

        internal void Remove(int index)
        {
            HtmlNode node = null;
            HtmlNode node2 = null;
            HtmlNode node3 = (HtmlNode) this._items[index];
            if (index > 0)
            {
                node2 = (HtmlNode) this._items[index - 1];
            }
            if (index < (this._items.Count - 1))
            {
                node = (HtmlNode) this._items[index + 1];
            }
            this._items.RemoveAt(index);
            if (node2 != null)
            {
                if (node == node2)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }
                node2._nextnode = node;
            }
            if (node != null)
            {
                node._prevnode = node2;
            }
            node3._prevnode = null;
            node3._nextnode = null;
            node3._parentnode = null;
        }

        internal void Replace(int index, HtmlNode node)
        {
            HtmlNode node2 = null;
            HtmlNode node3 = null;
            HtmlNode node4 = (HtmlNode) this._items[index];
            if (index > 0)
            {
                node3 = (HtmlNode) this._items[index - 1];
            }
            if (index < (this._items.Count - 1))
            {
                node2 = (HtmlNode) this._items[index + 1];
            }
            this._items[index] = node;
            if (node3 != null)
            {
                if (node == node3)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }
                node3._nextnode = node;
            }
            if (node2 != null)
            {
                node2._prevnode = node;
            }
            node._prevnode = node3;
            if (node2 == node)
            {
                throw new InvalidProgramException("Unexpected error.");
            }
            node._nextnode = node2;
            node._parentnode = this._parentnode;
            node4._prevnode = null;
            node4._nextnode = null;
            node4._parentnode = null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        public HtmlNode this[int index]
        {
            get
            {
                return (this._items[index] as HtmlNode);
            }
        }

        public int this[HtmlNode node]
        {
            get
            {
                int nodeIndex = this.GetNodeIndex(node);
                if (nodeIndex == -1)
                {
                    throw new ArgumentOutOfRangeException("node", "Node \"" + node.CloneNode(false).OuterHtml + "\" was not found in the collection");
                }
                return nodeIndex;
            }
        }

        public class HtmlNodeEnumerator : IEnumerator,IDisposable
        {
            private int _index;
            private ArrayList _items;

            internal HtmlNodeEnumerator(ArrayList items)
            {
                this._items = items;
                this._index = -1;
            }

            public bool MoveNext()
            {
                this._index++;
                return (this._index < this._items.Count);
            }

            public void Reset()
            {
                this._index = -1;
            }

            public HtmlNode Current
            {
                get
                {
                    return (HtmlNode) this._items[this._index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            #region IDisposable ³ÉÔ±

            public void Dispose()
            {
                
            }

            #endregion
        }
    }
}

