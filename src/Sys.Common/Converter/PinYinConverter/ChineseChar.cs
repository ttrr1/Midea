namespace iPortal.Converter.PinYinConverter
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// ��װ�˼������ĵĶ����ͱʻ��Ȼ�����Ϣ��
    /// </summary>
    /// <example>
    /// ���´�����ʾ�˷��ظ����ַ��ıʻ�����
    /// <code source="../../Example_CS/Program.cs" lang="cs"></code>
    /// <code source="../../Example_VB/Main.vb" lang="vbnet"></code>
    /// <code source="../../Example_CPP/Example_CPP.cpp" lang="cpp"></code>
    /// </example>
    public class ChineseChar
    {
        private static CharDictionary charDictionary;
        private char chineseCharacter;
        private static HomophoneDictionary homophoneDictionary;
        private bool isPolyphone;
        private const short MaxPolyphoneNum = 8;
        private short pinyinCount;
        private static PinyinDictionary pinyinDictionary;
        private string[] pinyinList = new string[8];
        private static StrokeDictionary strokeDictionary;
        private short strokeNumber;

        static ChineseChar()
        {
            string str;
            byte[] buffer;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            using (Stream stream = executingAssembly.GetManifestResourceStream("iPortal.Common.Converter.PinYinConverter.PinyinDictionary.resources"))
            {
                using (ResourceReader reader = new ResourceReader(stream))
                {
                    reader.GetResourceData("PinyinDictionary", out str, out buffer);
                    using (BinaryReader reader2 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        pinyinDictionary = PinyinDictionary.Deserialize(reader2);
                    }
                }
            }
            using (Stream stream2 = executingAssembly.GetManifestResourceStream("iPortal.Common.Converter.PinYinConverter.CharDictionary.resources"))
            {
                using (ResourceReader reader3 = new ResourceReader(stream2))
                {
                    reader3.GetResourceData("CharDictionary", out str, out buffer);
                    using (BinaryReader reader4 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        charDictionary = CharDictionary.Deserialize(reader4);
                    }
                }
            }
            using (Stream stream3 = executingAssembly.GetManifestResourceStream("iPortal.Common.Converter.PinYinConverter.HomophoneDictionary.resources"))
            {
                using (ResourceReader reader5 = new ResourceReader(stream3))
                {
                    reader5.GetResourceData("HomophoneDictionary", out str, out buffer);
                    using (BinaryReader reader6 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        homophoneDictionary = HomophoneDictionary.Deserialize(reader6);
                    }
                }
            }
            using (Stream stream4 = executingAssembly.GetManifestResourceStream("iPortal.Common.Converter.PinYinConverter.StrokeDictionary.resources"))
            {
                using (ResourceReader reader7 = new ResourceReader(stream4))
                {
                    reader7.GetResourceData("StrokeDictionary", out str, out buffer);
                    using (BinaryReader reader8 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        strokeDictionary = StrokeDictionary.Deserialize(reader8);
                    }
                }
            }
        }

        /// <summary>
        /// ChineseChar��Ĺ��캯����
        /// </summary>
        /// <param name="ch">ָ���ĺ����ַ���</param>
        /// <exception cref="T:System.NotSupportedException">
        /// �ַ����ڼ���������չ�ַ����С�
        /// </exception>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public ChineseChar(char ch)
        {
            if (!IsValidChar(ch))
            {
                throw new NotSupportedException(AssemblyResource.CHARACTER_NOT_SUPPORTED);
            }
            this.chineseCharacter = ch;
            CharUnit charUnit = charDictionary.GetCharUnit(ch);
            this.strokeNumber = charUnit.StrokeNumber;
            this.pinyinCount = charUnit.PinyinCount;
            this.isPolyphone = charUnit.PinyinCount > 1;
            for (int i = 0; i < this.pinyinCount; i++)
            {
                PinyinUnit pinYinUnitByIndex = pinyinDictionary.GetPinYinUnitByIndex(charUnit.PinyinIndexList[i]);
                this.pinyinList[i] = pinYinUnitByIndex.Pinyin;
            }
        }

        /// <summary>
        /// ���������ַ���ʵ���ַ��ıʻ������бȽϡ�
        /// </summary>
        /// <param name="ch">��ʾ�������ַ�</param>
        /// 
        /// <returns>
        /// ˵���Ƚϲ����Ľ����
        /// ��������ַ���ʵ���ַ��ıʻ�����ͬ������ֵΪ 0��
        /// ���ʵ���ַ��ȸ����ַ��ıʻ��࣬����ֵΪ&gt; 0��
        /// ���ʵ���ַ��ȸ����ַ��ıʻ��٣�����ֵΪ&lt; 0�� 
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public int CompareStrokeNumber(char ch)
        {
            CharUnit charUnit = charDictionary.GetCharUnit(ch);
            return (this.StrokeNumber - charUnit.StrokeNumber);
        }

        private static bool ExistSameElement<T>(T[] array1, T[] array2) where T: IComparable
        {
            int index = 0;
            int num2 = 0;
            while ((index < array1.Length) && (num2 < array2.Length))
            {
                if (array1[index].CompareTo(array2[num2]) < 0)
                {
                    index++;
                }
                else
                {
                    if (array1[index].CompareTo(array2[num2]) > 0)
                    {
                        num2++;
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ��������ָ���ʻ������ַ�������
        /// </summary>
        /// <param name="strokeNumber">��ʾ��Ҫ��ʶ��ıʻ�����</param>
        /// <returns>
        /// ���ؾ���ָ���ʻ������ַ�����
        /// ����ʻ�������Чֵ����-1��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ�� ChineseChar��ʵ����
        /// </remarks>
        public static short GetCharCount(short strokeNumber)
        {
            if (!IsValidStrokeNumber(strokeNumber))
            {
                return -1;
            }
            return strokeDictionary.GetStrokeUnit(strokeNumber).CharCount;
        }

        /// <summary>
        /// ��������ָ���ʻ����������ַ�����
        /// </summary>
        /// <param name="strokeNumber">ָ����Ҫ��ʶ��ıʻ�����</param>
        /// <returns>
        /// ���ؾ���ָ���ʻ������ַ��б�
        /// ����ʻ�������Чֵ���ؿա�
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public static char[] GetChars(short strokeNumber)
        {
            if (!IsValidStrokeNumber(strokeNumber))
            {
                return null;
            }
            return strokeDictionary.GetStrokeUnit(strokeNumber).CharList;
        }

        /// <summary>
        /// ��ȡ����ƴ��������ͬ���֡�                                                              
        /// </summary>
        /// <param name="pinyin">ָ��������</param>
        /// 
        /// <returns>
        /// ���ؾ�����ͬ��ָ��ƴ�����ַ����б�
        /// ���ƴ��������Чֵ�򷵻ؿա�
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// ƴ����һ�������á�
        /// </exception>
        public static char[] GetChars(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (!IsValidPinyin(pinyin))
            {
                return null;
            }
            return homophoneDictionary.GetHomophoneUnit(pinyinDictionary, pinyin).HomophoneList;
        }

        /// <summary>
        /// ��������ָ��ƴ�����ַ�����
        /// </summary>
        /// <param name="pinyin">��ʾ��Ҫ��ʶ���ƴ���ַ�����</param>             
        /// <returns>
        /// ���ؾ���ָ��ƴ�����ַ�����
        /// ���ƴ��������Чֵ�򷵻�-1��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// ƴ����һ�������á�
        /// </exception>
        public static short GetHomophoneCount(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (!IsValidPinyin(pinyin))
            {
                return -1;
            }
            return homophoneDictionary.GetHomophoneUnit(pinyinDictionary, pinyin).Count;
        }

        /// <summary>
        /// ����ָ���ַ��ıʻ�����
        /// </summary>
        /// <param name="ch">ָ����Ҫʶ����ַ���</param>
        /// <returns>
        /// ����ָ���ַ��ıʻ�����
        /// ����ַ�������Чֵ�򷵻�-1��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public static short GetStrokeNumber(char ch)
        {
            if (!IsValidChar(ch))
            {
                return -1;
            }
            return charDictionary.GetCharUnit(ch).StrokeNumber;
        }

        /// <summary>
        /// ʶ���ַ��Ƿ���ָ���Ķ�����
        /// </summary>
        /// <param name="pinyin">ָ������Ҫ��ʶ���ƴ����</param>
        /// <returns>
        /// ���ָ����ƴ���ַ�����ʵ���ַ���ƴ���������򷵻�ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// ƴ����һ�������á�
        /// </exception>
        public bool HasSound(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("HasSound_pinyin");
            }
            for (int i = 0; i < this.PinyinCount; i++)
            {
                if (string.Compare(this.Pinyins[i], pinyin, true, CultureInfo.CurrentCulture) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ʶ��������ַ��Ƿ���ʵ���ַ���ͬ���֡�
        /// </summary>
        /// <param name="ch">ָ����Ҫʶ����ַ���</param>
        /// <returns>
        /// ���������ʵ���ַ���ͬ�����򷵻�ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public bool IsHomophone(char ch)
        {
            return IsHomophone(this.chineseCharacter, ch);
        }

        /// <summary>
        /// ʶ������������ַ��Ƿ���ͬ���֡�
        /// </summary>
        /// <param name="ch1">ָ����һ���ַ�</param>
        /// <param name="ch2">ָ���ڶ����ַ�</param>
        /// <returns>
        /// ����������ַ���ͬ���ַ���ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public static bool IsHomophone(char ch1, char ch2)
        {
            CharUnit charUnit = charDictionary.GetCharUnit(ch1);
            CharUnit unit2 = charDictionary.GetCharUnit(ch2);
            return ExistSameElement<short>(charUnit.PinyinIndexList, unit2.PinyinIndexList);
        }

        /// <summary>
        /// ʶ��������ַ����Ƿ���һ����Ч�ĺ����ַ���
        /// </summary>
        /// <param name="ch">ָ����Ҫʶ����ַ���</param>
        /// <returns>
        /// ���ָ�����ַ���һ����Ч�ĺ����ַ��򷵻�ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public static bool IsValidChar(char ch)
        {
            return (charDictionary.GetCharUnit(ch) != null);
        }

        /// <summary> 
        /// ʶ�������ƴ���Ƿ���һ����Ч��ƴ���ַ�����
        /// </summary>
        /// <param name="pinyin">ָ����Ҫʶ����ַ�����</param>
        /// 
        /// <returns>
        /// ���ָ�����ַ�����һ����Ч��ƴ���ַ����򷵻�ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// ƴ����һ�������á�
        /// </exception>
        public static bool IsValidPinyin(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (pinyinDictionary.GetPinYinUnitIndex(pinyin) < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ʶ������ıʻ����Ƿ���һ����Ч�ıʻ�����
        /// </summary>
        /// <param name="strokeNumber">ָ����Ҫʶ��ıʻ�����</param>
        /// <returns>
        /// ���ָ���ıʻ�����һ����Ч�ıʻ����򷵻�ture�����򷵻�false��
        /// </returns>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public static bool IsValidStrokeNumber(short strokeNumber)
        {
            return (((strokeNumber >= 0) && (strokeNumber <= 0x30)) && (strokeDictionary.GetStrokeUnit(strokeNumber) != null));
        }

        /// <summary>
        /// ��ȡ��������ַ���
        /// </summary>
        /// <value>
        /// �����ַ���
        /// </value>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public char ChineseCharacter
        {
            get
            {
                return this.chineseCharacter;
            }
        }

        /// <summary>
        /// ��ȡ����ַ��Ƿ��Ƕ����֡�
        /// </summary>
        /// <value>
        /// ��������͵��ַ��Ƿ��Ƕ����֡�
        /// </value>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public bool IsPolyphone
        {
            get
            {
                return this.isPolyphone;
            }
        }

        /// <summary>
        /// ��ȡ����ַ���ƴ��������
        /// </summary>
        /// <value>
        /// ����ַ���ƴ������
        /// </value>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public short PinyinCount
        {
            get
            {
                return this.pinyinCount;
            }
        }

        /// <summary>
        /// ��ȡ����ַ���ƴ���� 
        /// </summary>
        /// <value>
        /// ����ַ���ƴ����
        /// </value>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public ReadOnlyCollection<string> Pinyins
        {
            get
            {
                return new ReadOnlyCollection<string>(this.pinyinList);
            }
        }

        /// <summary>
        /// ��ȡ����ַ��ıʻ�����
        /// </summary>
        /// <value>
        /// ����ַ��ıʻ�����
        /// </value>
        /// <remarks>
        /// �����<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />���鿴ʹ��ChineseChar��ʵ����
        /// </remarks>
        public short StrokeNumber
        {
            get
            {
                return this.strokeNumber;
            }
        }
    }
}

