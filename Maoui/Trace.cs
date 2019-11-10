#define _TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Maoui
{
    class Trace
    {   
        public static void trace()
        {
#if _TRACE
            var sf = new StackTrace().GetFrame(2);
            if (sf != null)
                Log($"{sf.GetFileName()}:{sf.GetFileLineNumber()} -> {sf.GetMethod()}");
#endif
        }

        public static void Log(Type t, string info)
        {
#if _TRACE
            var sf = new StackTrace().GetFrame(2);
            if (sf != null)
            {
                var mi = sf.GetMethod();
                Log(t.Name + "::" + mi.Name + " : " + info);
            }
            else
                Log(info);
#endif
        }

        public static void Error(Type t, Exception e)
        {
#if _TRACE
            var sf = new StackTrace().GetFrame(2);
            if (sf != null)
            {
                var mi = sf.GetMethod();
                Err(t.Name + "::" + mi.Name + ":" + e.Message);
            }
            Err(e.StackTrace);
#endif
        }

        public static void Error(string s, Exception e)
        {
#if _TRACE  
            Err(s + ":" + e.Message);
            Err(e.StackTrace);
#endif
        }

        public static void Error(Exception e)
        {
#if _TRACE
            var sf = new StackTrace().GetFrame(2);
            if (sf != null)
            {
                var mi = sf.GetMethod();
                Err(mi.Name + ":" + e.Message);
            }
            Err(e.StackTrace);
#endif
        }

        public static void Error(string err)
        {
#if _TRACE
            Err(err);
#endif
        }

        public static void Log(string trace)
        {
            Console.WriteLine("TRC: " + trace);
        }

        private static void Err(string err)
        {
            Console.WriteLine("ERR: " + err);
        }

    }
}

