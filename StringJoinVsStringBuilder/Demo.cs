using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringJoinVsStringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var lst = GetListofClassID(500);
            lst.Add(null);
            string str = lst.ToComma(x => x.ID);
            Console.WriteLine(str);
            Console.ReadLine();
        }


        public static List<ClassWithGuid> GetListofClassID(int max)
        {
            List<ClassWithGuid> ids = new List<ClassWithGuid>();
            for (int j = 0; j < max; j++)
            {
                ClassWithGuid obj = new ClassWithGuid() { ID = Guid.NewGuid() };
                ids.Add(obj);
            }
            Guid id;
            ids.Add(new ClassWithGuid());
            return ids;
        }

    }

    public class ClassWithGuid
    {
        public Guid ID { get; set; }
    }

    public static class ExtUtils
    {
        public static string ToComma<T,
        TU>(this IEnumerable<T> source, Func<T, TU> func)
        {
            var filterLst = source.Where(x => (x != null));
            return string.Join(",",
            filterLst.Select(s => func(s).ToString()).ToArray());
        }
    }



}
