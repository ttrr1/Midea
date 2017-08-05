namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class HtmlAttributeCollection : IEnumerable
    {
        internal Hashtable _hashitems = new Hashtable();
        private ArrayList _items = new ArrayList();
        private HtmlNode _ownernode;

        internal HtmlAttributeCollection(HtmlNode ownernode)
        {
            this._ownernode = ownernode;
        }

        public HtmlAttribute Append(HtmlAttribute newAttribute)
        {
            if (newAttribute == null)
            {
                throw new ArgumentNullException("newAttribute");
            }
            this._hashitems[newAttribute.Name] = newAttribute;
            newAttribute._ownernode = this._ownernode;
            this._items.Add(newAttribute);
            this._ownernode._innerchanged = true;
            this._ownernode._outerchanged = true;
            return newAttribute;
        }

        public HtmlAttribute Append(string name)
        {
            HtmlAttribute newAttribute = this._ownernode._ownerdocument.CreateAttribute(name);
            return this.Append(newAttribute);
        }

        public HtmlAttribute Append(string name, string value)
        {
            HtmlAttribute newAttribute = this._ownernode._ownerdocument.CreateAttribute(name, value);
            return this.Append(newAttribute);
        }

        internal void Clear()
        {
            this._hashitems.Clear();
            this._items.Clear();
        }

        internal int GetAttributeIndex(HtmlAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            for (int i = 0; i < this._items.Count; i++)
            {
                if (((HtmlAttribute) this._items[i]) == attribute)
                {
                    return i;
                }
            }
            return -1;
        }

        internal int GetAttributeIndex(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            string str = name.ToLower();
            for (int i = 0; i < this._items.Count; i++)
            {
                if (((HtmlAttribute) this._items[i]).Name == str)
                {
                    return i;
                }
            }
            return -1;
        }

        public HtmlAttributeEnumerator GetEnumerator()
        {
            return new HtmlAttributeEnumerator(this._items);
        }

        public HtmlAttribute Prepend(HtmlAttribute newAttribute)
        {
            if (newAttribute == null)
            {
                throw new ArgumentNullException("newAttribute");
            }
            this._hashitems[newAttribute.Name] = newAttribute;
            newAttribute._ownernode = this._ownernode;
            this._items.Insert(0, newAttribute);
            this._ownernode._innerchanged = true;
            this._ownernode._outerchanged = true;
            return newAttribute;
        }

        public void Remove(HtmlAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            int attributeIndex = this.GetAttributeIndex(attribute);
            if (attributeIndex == -1)
            {
                throw new IndexOutOfRangeException();
            }
            this.RemoveAt(attributeIndex);
        }

        public void Remove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            string str = name.ToLower();
            for (int i = 0; i < this._items.Count; i++)
            {
                HtmlAttribute attribute = (HtmlAttribute) this._items[i];
                if (attribute.Name == str)
                {
                    this.RemoveAt(i);
                }
            }
        }

        public void RemoveAll()
        {
            this._hashitems.Clear();
            this._items.Clear();
            this._ownernode._innerchanged = true;
            this._ownernode._outerchanged = true;
        }

        public void RemoveAt(int index)
        {
            HtmlAttribute attribute = (HtmlAttribute) this._items[index];
            this._hashitems.Remove(attribute.Name);
            this._items.RemoveAt(index);
            this._ownernode._innerchanged = true;
            this._ownernode._outerchanged = true;
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

        public HtmlAttribute this[int index]
        {
            get
            {
                return (this._items[index] as HtmlAttribute);
            }
        }

        public HtmlAttribute this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                return (this._hashitems[name.ToLower()] as HtmlAttribute);
            }
        }

        public class HtmlAttributeEnumerator : IEnumerator,IDisposable
        {
            private int _index;
            private ArrayList _items;

            internal HtmlAttributeEnumerator(ArrayList items)
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

            public HtmlAttribute Current
            {
                get
                {
                    return (HtmlAttribute) this._items[this._index];
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

