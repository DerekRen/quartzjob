using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuartzJob.Common
{
    public static class Extensions
    {
        public static bool ParseToBool(this object val)
        {
            if ((val == null) || (val == DBNull.Value))
            {
                return false;
            }
            if (val is bool)
            {
                return (bool)val;
            }
            return ((val.ToString().ToLower() == "true") || (val.ToString().ToLower() == "1"));
        }
        public static byte ParseToByte(this object val)
        {
            return ParseToByte(val, 0);
        }
        public static byte ParseToByte(this object val, byte defaultValue)
        {
            byte num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is byte)
            {
                return (byte)val;
            }
            if (!byte.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static byte? ParseToByteNullable(this object val)
        {
            byte num = ParseToByte(val);
            if (num.Equals((byte)0))
            {
                return null;
            }
            return new byte?(num);
        }

        public static DateTime ParseToDateTime(this object val)
        {
            DateTime time;
            if ((val == null) || (val == DBNull.Value))
            {
                return new DateTime(0x76c, 1, 1);
            }
            if (val is DateTime)
            {
                return (DateTime)val;
            }
            if (!DateTime.TryParse(val.ToString(), out time))
            {
                return new DateTime(0x76c, 1, 1);
            }
            return time;
        }
        public static DateTime? ParseToDateTimeNullable(this object val)
        {
            DateTime time = ParseToDateTime(val);
            if (time.Equals(new DateTime(0x76c, 1, 1)))
            {
                return null;
            }
            return new DateTime?(time);
        }
        public static decimal ParseToDecimal(this object val)
        {
            return ParseToDecimal(val, 0M, 2);
        }
        public static decimal ParseToDecimal(this object val, int decimals)
        {
            return ParseToDecimal(val, 0M, decimals);
        }

        public static decimal ParseToDecimal(this object val, decimal defaultValue, int decimals)
        {
            decimal num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is decimal)
            {
                return Math.Round((decimal)val, decimals);
            }
            if (!decimal.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return Math.Round(num, decimals);
        }
        public static decimal? ParseToDecimalNullable(this object val)
        {
            decimal num = ParseToDecimal(val);
            if (num.Equals((decimal)0.0M))
            {
                return null;
            }
            return new decimal?(num);
        }
        public static double ParseToDouble(this object val)
        {
            return ParseToDouble(val, 0.0, 2);
        }
        public static double ParseToDouble(this object val, int digits)
        {
            return ParseToDouble(val, 0.0, digits);
        }
        public static double ParseToDouble(this object val, double defaultValue, int digits)
        {
            double num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is double)
            {
                return Math.Round((double)val, digits);
            }
            if (!double.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return Math.Round(num, digits);
        }
        public static double? ParseToDoubleNullable(this object val)
        {
            double num = ParseToDouble(val);
            if (num.Equals((double)0.0))
            {
                return null;
            }
            return new double?(num);
        }
        public static float ParseToFloat(this object val)
        {
            return ParseToFloat(val, 0f);
        }
        public static float ParseToFloat(this object val, float defaultValue)
        {
            float num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is float)
            {
                return (float)val;
            }
            if (!float.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static float? ParseToFloatNullable(this object val)
        {
            float num = ParseToFloat(val);
            if (num.Equals((float)0f))
            {
                return null;
            }
            return new float?(num);
        }
        public static int ParseToInt(this object val)
        {
            return ParseToInt(val, 0);
        }
        public static int ParseToInt(this object val, int defaultValue)
        {
            int num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is int)
            {
                return (int)val;
            }
            if (!int.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static int? ParseToIntNullable(this object val)
        {
            int num = ParseToInt(val);
            if (num.Equals(0))
            {
                return null;
            }
            return new int?(num);
        }
        public static long ParseToLong(this object val)
        {
            return ParseToLong(val, 0L);
        }
        public static long ParseToLong(this object val, long defaultValue)
        {
            long num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is long)
            {
                return (long)val;
            }
            if (!long.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static long? ParseToLongNullable(this object val)
        {
            long num = ParseToLong(val);
            if (num.Equals((long)0L))
            {
                return null;
            }
            return new long?(num);
        }
        public static sbyte ParseToSbyte(this object val)
        {
            return ParseToSbyte(val, 0);
        }
        public static sbyte ParseToSbyte(this object val, sbyte defaultValue)
        {
            sbyte num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is sbyte)
            {
                return (sbyte)val;
            }
            if (!sbyte.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static sbyte? ParseToSbyteNullable(this object val)
        {
            sbyte num = ParseToSbyte(val);
            if (num.Equals((sbyte)0))
            {
                return null;
            }
            return new sbyte?(num);
        }
        public static short ParseToShort(this object val)
        {
            return ParseToShort(val, 0);
        }
        public static short ParseToShort(this object val, short defaultValue)
        {
            short num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is short)
            {
                return (short)val;
            }
            if (!short.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static short? ParseToShortNullable(this object val)
        {
            short num = ParseToShort(val);
            if (num.Equals((short)0))
            {
                return null;
            }
            return new short?(num);
        }
        public static string ParseToString(this object val)
        {
            if ((val == null) || (val == DBNull.Value))
            {
                return string.Empty;
            }
            if (val.GetType() == typeof(byte[]))
            {
                return Encoding.ASCII.GetString((byte[])val, 0, ((byte[])val).Length);
            }
            return val.ToString();
        }
        public static string ParseToString(this object val, string replace)
        {
            string str = ParseToString(val);
            return (string.IsNullOrEmpty(str) ? replace : str);
        }
        public static string ToStringNullable(this object val)
        {
            string str = ParseToString(val);
            return (string.IsNullOrEmpty(str) ? null : str);
        }
        public static uint ParseToUint(this object val)
        {
            return ParseToUint(val, 0);
        }
        public static uint ParseToUint(this object val, uint defaultValue)
        {
            uint num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is uint)
            {
                return (uint)val;
            }
            if (!uint.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static uint? ParseToUintNullable(this object val)
        {
            uint num = ParseToUint(val);
            if (num.Equals((uint)0))
            {
                return null;
            }
            return new uint?(num);
        }
        public static ulong ParseToUlong(this object val)
        {
            return ParseToUlong(val, 0L);
        }
        public static ulong ParseToUlong(this object val, ulong defaultValue)
        {
            ulong num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is long)
            {
                return (ulong)val;
            }
            if (!ulong.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static ulong? ParseToUlongNullable(this object val)
        {
            ulong num = ParseToUlong(val);
            if (num.Equals((ulong)0L))
            {
                return null;
            }
            return new ulong?(num);
        }
        public static ushort ParseToUshort(this object val)
        {
            return ParseToUshort(val, 0);
        }
        public static ushort ParseToUshort(this object val, ushort defaultValue)
        {
            ushort num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is ushort)
            {
                return (ushort)val;
            }
            if (!ushort.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }
        public static ushort? ToUshortNullable(this object val)
        {
            ushort num = ParseToUshort(val);
            if (num.Equals((ushort)0))
            {
                return null;
            }
            return new ushort?(num);
        }
        public static bool ParseTryToEnum<T>(byte value, out T parsed)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                parsed = (T)Enum.ToObject(typeof(T), value);
                return true;
            }
            parsed = (T)Enum.Parse(typeof(T), Enum.GetNames(typeof(T))[0]);
            return false;
        }
        public static bool ParseTryToEnum<T>(string name, out T parsed)
        {
            if (Enum.IsDefined(typeof(T), name))
            {
                parsed = (T)Enum.Parse(typeof(T), name, true);
                return true;
            }
            parsed = (T)Enum.Parse(typeof(T), Enum.GetNames(typeof(T))[0]);
            return false;
        }
        public static bool CheckDataTable(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断列表是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> source) where T : class
        {
            return source == null || source.Count == 0;
        }
    }

    /// <summary>
    /// 公共枚举
    /// </summary>
    public class CommonEnum
    {
        /// <summary>
        /// 根据枚举值获得描述
        /// </summary>
        /// <param name="enumtype">类型</param>
        /// <param name="value">枚举项的值</param>
        /// <returns></returns>
        public static string GetValueByEnumName(Type enumtype, int value)
        {
            string enumname = string.Empty;
            FieldInfo[] fields = enumtype.GetFields();
            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    //枚举英文
                    string name = field.Name;
                    if ((int)Enum.Parse(enumtype, name, true) == value)
                    {
                        DescriptionAttribute da = null;
                        object[] arrobj = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrobj.Length > 0)
                        {
                            da = arrobj[0] as DescriptionAttribute;
                        }
                        if (da != null)
                        {
                            //枚举中文描述
                            enumname = da.Description;
                        }
                    }
                }
            }
            return enumname;
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public enum ValidStatus
        {
            /// <summary>
            /// 无效
            /// </summary>
            [Description("无效")]
            InValid = 0,
            /// <summary>
            /// 有效
            /// </summary>
            [Description("有效")]
            Valid = 1
        }
    }
}
