using System;
using System.Collections.Generic;
using System.Text;

namespace NTICS.OLE1C77
{
    public class OLE : IDisposable 
    {
        private OLEConnection Connection;
        protected object Handle;

        public OLE(object Value)
        {
            Handle = Value;
        }
        public OLE(OLEConnection Connection, object Handle)
        {
            this.Connection = Connection;
            this.Handle = Handle;
        }
        public OLE Global
        {
            get { return Connection.Global; }
        }
        private object Normalize(object Arg)
        {
            if (Arg == null) { return Arg; }
            
            if (Arg.GetType() == typeof(OLE))
            {
                return ((OLE)Arg).ToObject();
            }
            if ((Arg.GetType() == typeof(int)) || (Arg.GetType() == typeof(long)) || (Arg.GetType() == typeof(float)) || (Arg.GetType() == typeof(decimal)))
            {
                return (double)(int)Arg;
            }
            if (Arg.GetType() == typeof(bool))
            {
                if ((bool)Arg) { return (double)1; } else { return (double)0; }
            }
            return Arg;
        }
        private void NormalizeParams(ref object[] Arg)
        {
            // Поиск объектов 
            for (int i = 0; i < Arg.GetLength(0); i++)
            {
                Arg[i] = Normalize(Arg[i]);
            }

        }

        #region 1C77 METODS

        //
        public OLE Method(string MethodName, object[] Arg)
        {
            if (Connection == null)
            {
                throw new Exception("Даный объект не поддерживает вызовов Method");
            }
            // Поиск объектов типа OLE
            NormalizeParams(ref Arg);
            return Connection.Method(MethodName, Handle, Arg);
        }

        public OLE Method(string MethodName, ref object[] Arg)
        {
            if (Connection == null)
            {
                throw new Exception("Даный объект не поддерживает вызовов Method");
            }
            // Поиск объектов типа OLE
            for (int i = 0; i < Arg.GetLength(0); i++)
            {
                if (Arg[i].GetType() == typeof(OLE))
                {
                    Arg[i] = ((OLE)Arg[i]).ToObject();
                }
            }
            return Connection.Method(MethodName, Handle, Arg);
        }

        // Execute Metod no args
        public OLE Method(String MethodName)
        {
            return Method(MethodName, new object[0]);
        }

        // Execute Metod 1 args
        public OLE Method(String MethodName, object Arg1)
        {
            return Method(MethodName, new object[1] {Arg1});
        }

        // Execute Metod 2 args
        public OLE Method(String MethodName, object Arg1, object Arg2)
        {
            return Method(MethodName, new object[2] { Arg1, Arg2});
        }

        // Execute Metod 3 args
        public OLE Method(String MethodName, object Arg1, object Arg2, object Arg3)
        {
            return Method(MethodName, new object[3] { Arg1, Arg2, Arg3 });
        }

        //// Execute Metod 4 args
        public OLE Method(String MethodName, object Arg1, object Arg2, object Arg3, object Arg4)
        {
            return Method(MethodName, new object[4] {Arg1, Arg2, Arg3, Arg4});
        }

        //// Execute Metod 5 args
        public OLE Method(String MethodName, object Arg1, object Arg2, object Arg3, object Arg4, object Arg5)
        {
            return Method(MethodName, new object[5] { Arg1, Arg2, Arg3, Arg4 ,Arg5});
        }


    #endregion
        #region 1C77 PROPERTY

        public OLE Property(String PropertyName)
        {
            if (Connection == null)
            {
                throw new Exception("Даный объект не поддерживает вызовов Property");
            }
            
            return Connection.Property(PropertyName, Handle);
        }

        public void Property(String PropertyName, object Value)
        {
            if (Connection == null)
            {
                throw new Exception("Даный объект не поддерживает вызовов Property");
            }
       
            Connection.Property(PropertyName, Value, Handle);
        }

        #endregion
        # region GLOBAL Context
        public bool ExecuteBatch(String Text)
        {
            return Connection.Global.Method("ExecuteBatch", (object)Text).ToBool();
        }

        // 1C CreateObject
        public OLE CreateObject(String AgregateObject)
        {
            return new OLE(Connection, Connection.Global.Method("CreateObject", (object)AgregateObject).ToObject());
        }

        public OLE EvalExpr(String Expr)
        {
            return Connection.Global.Method("EvalExpr", (object)Expr);
        }
        #endregion
        #region Conversion Members

        public static explicit operator float(OLE value)
        {
            return (float)value.ToDouble();
        }
        public static explicit operator double(OLE value)
        {
            return value.ToDouble();
        }
        public static explicit operator long(OLE value)
        {
            return (long)value.ToDouble();
        }
        public static explicit operator int(OLE value)
        {
            return value.ToInt();
        }
        public static explicit operator string(OLE value)
        {
            return value.ToString();
        }
        public static explicit operator decimal(OLE value)
        {
            return value.ToDecimal();
        }
        public static explicit operator DateTime(OLE value)
        {
            return value.ToDateTime();
        }
        public static explicit operator bool(OLE value)
        {
            return value.ToBool();
        }
        //public static bool operator ==(OLE d1, OLE d2)
        //{
        //    if ((d1 == null) && (d2 == null)) { return true; }
        //    if ((d1 == null) || (d2 == null)) { return false; }
        //    if (d1.ToObject().GetType() != d2.ToObject().GetType()) {return false;}
        //    if (d1.ToObject().GetType() == typeof(string))
        //    {
        //        return (d1.ToString() == d2.ToString());
        //    }
        //    if (d1.ToObject().GetType() == typeof(double))
        //    {
        //        return (d1.ToDouble() == d2.ToDouble());
        //    }
        //    return (d1.ToObject() == d2.ToObject());
        //}
        //public static bool operator !=(OLE d1, OLE d2)
        //{
        //    return (!(d1 == d2));
        //}

        public object ToObject()
        {
            return Handle;
        }
        public override string ToString()
        {
            try
            {
                return (string)Handle;
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в string");
            }
        }
        public decimal ToDecimal()
        {
            try
            {
                return (decimal)(double)Handle;
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в Decimal");
            }
        }
        public int ToInt()
        {
            try
            {
                return (int)(double)Handle;
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в int");
            }

        }
        public double ToDouble()
        {
            try
            {
                return (double)Handle;
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в double");
            }

        }
        public bool ToBool()
        {
            try
            {
                if ((double)Handle == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в bool");
            }

        }
        public DateTime ToDateTime()
        {
            try
            {
                return (DateTime)Handle;
            }
            catch
            {
                throw new Exception("OLE Ошибка преобразования в bool");
            }
        }
        #endregion
        #region STATIC
        public static bool IsEmtyValue(OLE Value)
        {
            if (Value == null) { return true; }
            if (Value.Handle == null) { return true; }
            if (Value.Connection == null) { return false; }
            return Value.Connection.Global.Method("EmptyValue", Value.Handle).ToBool();
        }
        #endregion
        #region IDisposable Members
        ~OLE()
        {
            Dispose();
        }
 
        public void Dispose()
        {
            Handle = null;
            Connection = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
