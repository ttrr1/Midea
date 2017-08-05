namespace iPortal.Converter.PinYinConverter
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// 封装了简体中文的读音和笔画等基本信息。
    /// </summary>
    /// <example>
    /// 以下代码演示了返回给出字符的笔划数。
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
        /// ChineseChar类的构造函数。
        /// </summary>
        /// <param name="ch">指定的汉字字符。</param>
        /// <exception cref="T:System.NotSupportedException">
        /// 字符不在简体中文扩展字符集中。
        /// </exception>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
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
        /// 将给出的字符和实例字符的笔画数进行比较。
        /// </summary>
        /// <param name="ch">显示给出的字符</param>
        /// 
        /// <returns>
        /// 说明比较操作的结果。
        /// 如果给出字符和实例字符的笔画数相同，返回值为 0。
        /// 如果实例字符比给出字符的笔画多，返回值为&gt; 0。
        /// 如果实例字符比给出字符的笔画少，返回值为&lt; 0。 
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
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
        /// 检索具有指定笔画数的字符个数。
        /// </summary>
        /// <param name="strokeNumber">显示需要被识别的笔画数。</param>
        /// <returns>
        /// 返回具有指定笔画数的字符数。
        /// 如果笔画数是无效值返回-1。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用 ChineseChar的实例。
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
        /// 检索具有指定笔画数的所有字符串。
        /// </summary>
        /// <param name="strokeNumber">指出需要被识别的笔画数。</param>
        /// <returns>
        /// 返回具有指定笔画数的字符列表。
        /// 如果笔画数是无效值返回空。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
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
        /// 获取给定拼音的所有同音字。                                                              
        /// </summary>
        /// <param name="pinyin">指出读音。</param>
        /// 
        /// <returns>
        /// 返回具有相同的指定拼音的字符串列表。
        /// 如果拼音不是有效值则返回空。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// 拼音是一个空引用。
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
        /// 检索具有指定拼音的字符数。
        /// </summary>
        /// <param name="pinyin">显示需要被识别的拼音字符串。</param>             
        /// <returns>
        /// 返回具有指定拼音的字符数。
        /// 如果拼音不是有效值则返回-1。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// 拼音是一个空引用。
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
        /// 检索指定字符的笔画数。
        /// </summary>
        /// <param name="ch">指出需要识别的字符。</param>
        /// <returns>
        /// 返回指定字符的笔画数。
        /// 如果字符不是有效值则返回-1。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
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
        /// 识别字符是否有指定的读音。
        /// </summary>
        /// <param name="pinyin">指定的需要被识别的拼音。</param>
        /// <returns>
        /// 如果指定的拼音字符串在实例字符的拼音集合中则返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// 拼音是一个空引用。
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
        /// 识别给出的字符是否是实例字符的同音字。
        /// </summary>
        /// <param name="ch">指出需要识别的字符。</param>
        /// <returns>
        /// 如果给出的实例字符是同音字则返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public bool IsHomophone(char ch)
        {
            return IsHomophone(this.chineseCharacter, ch);
        }

        /// <summary>
        /// 识别给出的两个字符是否是同音字。
        /// </summary>
        /// <param name="ch1">指出第一个字符</param>
        /// <param name="ch2">指出第二个字符</param>
        /// <returns>
        /// 如果给出的字符是同音字返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public static bool IsHomophone(char ch1, char ch2)
        {
            CharUnit charUnit = charDictionary.GetCharUnit(ch1);
            CharUnit unit2 = charDictionary.GetCharUnit(ch2);
            return ExistSameElement<short>(charUnit.PinyinIndexList, unit2.PinyinIndexList);
        }

        /// <summary>
        /// 识别给出的字符串是否是一个有效的汉字字符。
        /// </summary>
        /// <param name="ch">指出需要识别的字符。</param>
        /// <returns>
        /// 如果指定的字符是一个有效的汉字字符则返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public static bool IsValidChar(char ch)
        {
            return (charDictionary.GetCharUnit(ch) != null);
        }

        /// <summary> 
        /// 识别给出的拼音是否是一个有效的拼音字符串。
        /// </summary>
        /// <param name="pinyin">指出需要识别的字符串。</param>
        /// 
        /// <returns>
        /// 如果指定的字符串是一个有效的拼音字符串则返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar。
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">
        /// 拼音是一个空引用。
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
        /// 识别给出的笔画数是否是一个有效的笔画数。
        /// </summary>
        /// <param name="strokeNumber">指出需要识别的笔画数。</param>
        /// <returns>
        /// 如果指定的笔画数是一个有效的笔画数则返回ture，否则返回false。
        /// </returns>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public static bool IsValidStrokeNumber(short strokeNumber)
        {
            return (((strokeNumber >= 0) && (strokeNumber <= 0x30)) && (strokeDictionary.GetStrokeUnit(strokeNumber) != null));
        }

        /// <summary>
        /// 获取这个汉字字符。
        /// </summary>
        /// <value>
        /// 汉字字符。
        /// </value>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public char ChineseCharacter
        {
            get
            {
                return this.chineseCharacter;
            }
        }

        /// <summary>
        /// 获取这个字符是否是多音字。
        /// </summary>
        /// <value>
        /// 这个布尔型的字符是否是多音字。
        /// </value>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public bool IsPolyphone
        {
            get
            {
                return this.isPolyphone;
            }
        }

        /// <summary>
        /// 获取这个字符的拼音个数。
        /// </summary>
        /// <value>
        /// 这个字符的拼音数。
        /// </value>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public short PinyinCount
        {
            get
            {
                return this.pinyinCount;
            }
        }

        /// <summary>
        /// 获取这个字符的拼音。 
        /// </summary>
        /// <value>
        /// 这个字符的拼音。
        /// </value>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
        /// </remarks>
        public ReadOnlyCollection<string> Pinyins
        {
            get
            {
                return new ReadOnlyCollection<string>(this.pinyinList);
            }
        }

        /// <summary>
        /// 获取这个字符的笔画数。
        /// </summary>
        /// <value>
        /// 这个字符的笔画数。
        /// </value>
        /// <remarks>
        /// 请参阅<see cref="T:iPortal.Converter.PinYinConverter.ChineseChar" />来查看使用ChineseChar的实例。
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

