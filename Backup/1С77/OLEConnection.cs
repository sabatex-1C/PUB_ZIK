using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace NTICS.OLE1C77
{
    public class OLEConnection : Object, IDisposable
    {
        private Type V77OLE;
        private OLE GlobalContext;
        private OLE FirmReference;
        protected object Handle;
        private string FPathToDatabase;
        private string FUser;
        private string FPassword;
        private bool FExclusive;

        public OLEConnection(string PathToDatabase, V77Servers ServerType, bool Exclusive, string User, string Password)
        {
            FPathToDatabase = PathToDatabase;
            FUser = User;
            FPassword = Password;
            FExclusive = Exclusive;
            if (!Directory.Exists(PathToDatabase) && (PathToDatabase !=""))
            {
                throw new OLE1C77Exception("Сбой соединения !!! Каталог - " + PathToDatabase + "  не существует !!!");
            }
            V77OLE = Type.GetTypeFromProgID(Enum.Format(typeof(V77Servers), ServerType, "G") + ".Application");
            Handle = Activator.CreateInstance(V77OLE);
            GlobalContext = new OLE(this, Handle);
            OLE RMTrade = Global.Method("RMTrade");

            string ParamStr = "/D" + PathToDatabase;
            if (Exclusive) { ParamStr = ParamStr + " /M"; }
            if (User != "") { ParamStr = ParamStr + " /N" + User; }
            if (Password != "") { ParamStr = ParamStr + " /P" + Password; }

            if (!(bool)Global.Method("Initialize", RMTrade, ParamStr, "").ToObject())
            {
                GlobalContext = null;
                Handle = null;
                V77OLE = null;
                throw new OLE1C77Exception("Сбой соединения !!! Вероятней всего программа запущена монопольно или требует переиндексации !!!");
            }

            ntics_CommonAllConfig conf = new ntics_CommonAllConfig();
            FirmReference = Global.CreateObject("Справочник.Фирмы");
            FirmReference.Method("ВыбратьЭлементы");
            while (FirmReference.Method("ПолучитьЭлемент").ToBool())
            {
                if (FirmReference.Method("DeleteMark").ToBool()) continue;
                if (FirmReference.Property("ЕДРПОУ").ToString().Trim() == conf.ЕДРПОУ)
                {
                    FirmReference = FirmReference.Method("CurrentItem");
                    return ;
                }
            }

            string s = Global.EvalExpr("SystemCaption()").ToString();
            FirmReference = null;
            GlobalContext = null;
            Handle = null;
            V77OLE = null;
            throw new OLE1C77Exception("Программа не зарегистрирована для даной базы: " + s);
        }

        public OLE Global
        {
            get { return GlobalContext;}
        }


        public object OLE1C77Function(string FuncName, BindingFlags invokeAttr, object Handle, object[] Args)
        {
            try
            {
                return V77OLE.InvokeMember(FuncName, invokeAttr, null, Handle, Args);
                //return V77OLE.InvokeMember(FuncName, invokeAttr, null, null, null);
            }
            catch (ArgumentNullException e)
            {// Ошибка параметра FuncName , FuncName == null
                throw (e);
            }
            catch (ArgumentException e)
            {/* args is multidimensional.
                -or- 
                invokeAttr is not a valid BindingFlags attribute.
                -or- 
                invokeAttr contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.
                -or- 
                invokeAttr contains both GetField and SetField.
                -or- 
                invokeAttr contains both GetProperty and SetProperty.
                -or- 
                invokeAttr contains InvokeMethod combined with SetField or SetProperty.
                -or- 
                invokeAttr contains SetField and args has more than one element.
                -or- 
                This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.
                -or- 
                One of the named parameter arrays contains a string that is a null reference*/
                throw(e);
            }
            catch (MethodAccessException e)
            {//The specified member is a class initializer.
                throw(e);

            }
            catch (MissingFieldException e)
            {//The field or property cannot be found.
                throw(e);

            }
            catch (MissingMethodException e)
            {/*The method cannot be found.
             -or- 
             The current Type object represents a type that contains open type parameters, that is, ContainsGenericParameters returns true.*/
                throw(e);
            }
            catch (TargetException e)
            {//The specified member cannot be invoked on target. 
                throw(e);
            }
            catch (AmbiguousMatchException e)
            {//More than one method matches the binding criteria. 
                throw(e);
            }
            catch (NotSupportedException e)
            {//The .NET Compact Framework does not currently support this property.
                throw(e);

            }
            catch (Exception e)
            {
                throw(e);
            }
        }


        public OLE Method(string MethodName, object Handle, object[] Args)
        {
            return new OLE(this, OLE1C77Function(MethodName, BindingFlags.InvokeMethod, Handle, Args));
        }
        public OLE Method(string MethodName, object Handle, ref object[] Args)
        {
            return new OLE(this, OLE1C77Function(MethodName, BindingFlags.InvokeMethod, Handle, Args));
        }
        public OLE Property(String PropertyName, object Handle)
        {
            return new OLE(this,OLE1C77Function(PropertyName, BindingFlags.GetProperty, Handle, null));
        }
        public void Property(String PropertyName, object Value, object Handle)
        {
            object[] Args = new object[1];
            if ((Value == null) || (Value.GetType() != typeof(OLE)))
            {
                Args[0] = Value;
            } else
            {
                Args[0] = ((OLE)Value).ToObject();
            }
            OLE1C77Function(PropertyName, BindingFlags.PutDispProperty, Handle, Args);
        
        }


        #region IDisposable Members
        ~OLEConnection()
        {
            Dispose();
        }
 

        public void Dispose()
        {
            V77OLE = null;
            GlobalContext = null;
            Handle = null;
            FirmReference = null;
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        #endregion
    }
}
