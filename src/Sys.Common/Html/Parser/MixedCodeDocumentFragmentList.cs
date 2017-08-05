namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class MixedCodeDocumentFragmentList : IEnumerable
    {
        private MixedCodeDocument _doc;
        private ArrayList _items = new ArrayList();

        internal MixedCodeDocumentFragmentList(MixedCodeDocument doc)
        {
            this._doc = doc;
        }

        public void Append(MixedCodeDocumentFragment newFragment)
        {
            if (newFragment == null)
            {
                throw new ArgumentNullException("newFragment");
            }
            this._items.Add(newFragment);
        }

        internal void Clear()
        {
            this._items.Clear();
        }

        public MixedCodeDocumentFragmentEnumerator GetEnumerator()
        {
            return new MixedCodeDocumentFragmentEnumerator(this._items);
        }

        internal int GetFragmentIndex(MixedCodeDocumentFragment fragment)
        {
            if (fragment == null)
            {
                throw new ArgumentNullException("fragment");
            }
            for (int i = 0; i < this._items.Count; i++)
            {
                if (((MixedCodeDocumentFragment) this._items[i]) == fragment)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Prepend(MixedCodeDocumentFragment newFragment)
        {
            if (newFragment == null)
            {
                throw new ArgumentNullException("newFragment");
            }
            this._items.Insert(0, newFragment);
        }

        public void Remove(MixedCodeDocumentFragment fragment)
        {
            if (fragment == null)
            {
                throw new ArgumentNullException("fragment");
            }
            int fragmentIndex = this.GetFragmentIndex(fragment);
            if (fragmentIndex == -1)
            {
                throw new IndexOutOfRangeException();
            }
            this.RemoveAt(fragmentIndex);
        }

        public void RemoveAll()
        {
            this._items.Clear();
        }

        public void RemoveAt(int index)
        {
            MixedCodeDocumentFragment fragment1 = (MixedCodeDocumentFragment) this._items[index];
            this._items.RemoveAt(index);
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

        public MixedCodeDocumentFragment this[int index]
        {
            get
            {
                return (this._items[index] as MixedCodeDocumentFragment);
            }
        }

        public class MixedCodeDocumentFragmentEnumerator : IEnumerator
        {
            private int _index;
            private ArrayList _items;

            internal MixedCodeDocumentFragmentEnumerator(ArrayList items)
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

            public MixedCodeDocumentFragment Current
            {
                get
                {
                    return (MixedCodeDocumentFragment) this._items[this._index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }
    }
}

