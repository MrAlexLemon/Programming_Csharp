using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLINQ
{
    class Program
    {
        //Приклад даних        
        static List<Project> d1 = new List<Project>()
        {
            new Project(1, "Project1", 11,new DateTime(2015, 7, 20),new DateTime(2015, 8, 30),new List<string>(){"Person1" ,"Person2"}),
            new Project(2, "Project2", 15,new DateTime(2015, 8, 21),new DateTime(2015, 9, 12),new List<string>(){"Person1" ,"Person2","Person3","Person4"}),
            new Project(3, "Project3", 13,new DateTime(2015, 8, 13),new DateTime(2015, 9, 12),new List<string>(){"Person2","Person3","Person4"}),
            new Project(5, "Project4", 17,new DateTime(2015, 9, 09),new DateTime(2015, 10, 17),new List<string>(){"Person3","Person4"})
        };

        static List<Project> d2 = new List<Project>()
        {
            new Project(1, "Project2", 111,new DateTime(2015, 7, 20),new DateTime(2015, 8, 30),new List<string>(){"Person1" ,"Person2"}),
            new Project(2, "Project3", 151,new DateTime(2015, 8, 21),new DateTime(2015, 9, 12),new List<string>(){"Person1" ,"Person2","Person3","Person4"}),
            new Project(2, "Project3", 132,new DateTime(2015, 8, 13),new DateTime(2015, 9, 12),new List<string>(){"Person2","Person3","Person4"}),
            new Project(4, "Project3", 174,new DateTime(2015, 9, 09),new DateTime(2015, 10, 17),new List<string>(){"Person3","Person4"})
        };

        static List<Project> d1_for_distinct = new List<Project>()
        {
            new Project(1, "Project1", 11,new DateTime(2015, 7, 20),new DateTime(2015, 8, 30),new List<string>(){"Person1" ,"Person2"}),
            new Project(1, "Project1", 11,new DateTime(2015, 8, 21),new DateTime(2015, 9, 12),new List<string>(){"Person1" ,"Person2","Person3","Person4"}),
            new Project(1, "Project1", 11,new DateTime(2015, 8, 13),new DateTime(2015, 9, 12),new List<string>(){"Person2","Person3","Person4"}),
            new Project(2, "Project1", 12,new DateTime(2015, 9, 09),new DateTime(2015, 10, 17),new List<string>(){"Person3","Person4"}),
            new Project(2, "Project1", 12,new DateTime(2015, 9, 09),new DateTime(2015, 10, 17),new List<string>(){"Person3","Person4"})
        };

        static List<ProjectLink> lnk = new List<ProjectLink>()
        {
            new ProjectLink(1, 1),
            new ProjectLink(1, 2),
            new ProjectLink(1, 4),
            new ProjectLink(2, 1),
            new ProjectLink(2, 2),
            new ProjectLink(2, 4),
            new ProjectLink(5, 1),
            new ProjectLink(5, 2)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Проста вибірка елементів");
            var q1 = from x in d1
                     select x;
            foreach (var x in q1)
                Console.WriteLine(x);

            Console.WriteLine("Вибірка окремого поля (проекція)");
            var q2 = from x in d1
                     select x.value;
            foreach (var x in q2)
                Console.WriteLine(x);

            Console.WriteLine("Створення нового об'єкта анонімного типу");
            var q3 = from x in d1
                     select new { IDENTIFIER = x.id, VALUE = x.value };
            foreach (var x in q3)
                Console.WriteLine(x);

            Console.WriteLine("Умова");
            var q4 = from x in d1
                     where x.id > 1 && (x.name == "Project1" || x.name == "Project2")
                     select x;
            foreach (var x in q4)
                Console.WriteLine(x);

            Console.WriteLine("Вибірка по значенню типа");
            object[] array = new object[] { 123, "строка 1", true, "строка 2" };
            var qo = from x in array.OfType<string>()
                     select x;
            foreach (var x in qo)
                Console.WriteLine(x);

            Console.WriteLine("Сортування");
            var q5 = from x in d1
                     where x.id > 1 && (x.name == "Project1" || x.name == "Project2")
                     orderby x.name descending, x.id descending
                     select x;
            foreach (var x in q5)
                Console.WriteLine(x);

            Console.WriteLine("Сортування (з використанням методів)");
            var q51 = d1.Where((x) =>
            {
                return x.id > 1 && (x.name == "Project1" || x.name == "Project2");
            }).OrderByDescending(x => x.name).ThenByDescending(x => x.id);
            foreach (var x in q51) Console.WriteLine(x);

            Console.WriteLine("Partitioning Operators");
            Console.WriteLine("Посторінкова вибірка даних");
            var qp = GetPage(d1, 2, 2);
            foreach (var x in qp)
                Console.WriteLine(x);

            Console.WriteLine("Використання SkipWhile і TakeWhile");
            int[] intArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var qw = intArray.SkipWhile(x => (x < 4)).TakeWhile(x => x <= 7);
            foreach (var x in qw)
                Console.WriteLine(x);

            Console.WriteLine("Декартовий добуток");
            var q6 = from x in d1
                     from y in d2
                     select new { v1 = x.value, v2 = y.value };
            foreach (var x in q6)
                Console.WriteLine(x);

            Console.WriteLine("Inner Join з використанням Where");
            var q7 = from x in d1
                     from y in d2
                     where x.id == y.id
                     select new { v1 = x.value, v2 = y.value };
            foreach (var x in q7)
                Console.WriteLine(x);


            Console.WriteLine("Cross Join (Inner Join) з використанням Join");
            var q8 = from x in d1
                     join y in d2 on x.id equals y.id
                     select new { v1 = x.value, v2 = y.value };
            foreach (var x in q8)
                Console.WriteLine(x);

            Console.WriteLine("Cross Join (збереження об'єкта)");
            var q9 = from x in d1
                     join y in d2 on x.id equals y.id
                     select new { v1 = x.value, d2Group = y };
            foreach (var x in q9)
                Console.WriteLine(x);


            //Вибираються всі елементи з d1 і якщо є звязані з d2 (outer join)            
            //В temp поміщаєтся вся група, її елементи можна перебирати окремо             
            Console.WriteLine("Group Join");
            var q10 = from x in d1
                      join y in d2 on x.id equals y.id into temp
                      select new { v1 = x.value, d2Group = temp };
            foreach (var x in q10)
            {
                Console.WriteLine(x.v1);
                foreach (var y in x.d2Group)
                    Console.WriteLine("   " + y);
            }

            Console.WriteLine("Cross Join і Group Join");
            var q11 = from x in d1
                      join y in d2 on x.id equals y.id into temp
                      from t in temp
                      select new { v1 = x.value, v2 = t.value, cnt = temp.Count() };
            foreach (var x in q11)
                Console.WriteLine(x);

            Console.WriteLine("Outer Join");
            var q12 = from x in d1
                      join y in d2 on x.id equals y.id into temp
                      from t in temp.DefaultIfEmpty()
                      select new { v1 = x.value, v2 = ((t == null) ? "null" : t.value.ToString()) }; //-----------------------------?
            foreach (var x in q12)
                Console.WriteLine(x);

            Console.WriteLine("Використання Join для составних ключів");
            var q12_1 = from x in d1
                        join y in d1_for_distinct on new { x.id, x.name } equals new { y.id, y.name } into details
                        from d in details
                        select d;
            foreach (var x in q12_1)
                Console.WriteLine(x);

            //Дії над множинами  
            Console.WriteLine("Distinct - неповторні значення");
            var q13 = (from x in d1 select x.name).Distinct();
            foreach (var x in q13)
                Console.WriteLine(x);

            Console.WriteLine("Distinct - повторні значення для обєктів");
            var q14 = (from x in d1_for_distinct select x).Distinct();
            foreach (var x in q14)
                Console.WriteLine(x);

            Console.WriteLine("Distinct - неповторні значення для обєктів");
            var q15 = (from x in d1_for_distinct select x).Distinct(new ProjectEqualityComparer());
            foreach (var x in q15)
                Console.WriteLine(x);

            Console.WriteLine("Union - объединение с исключением дубликатов");
            int[] i1 = new int[] { 1, 2, 3, 4 };
            int[] i1_1 = new int[] { 2, 3, 4, 1 };
            int[] i2 = new int[] { 2, 3, 4, 5 };
            foreach (var x in i1.Union(i2))
                Console.WriteLine(x);
            Console.WriteLine("Union - объединение для объектов");
            foreach (var x in d1.Union(d1_for_distinct))
                Console.WriteLine(x);

            Console.WriteLine("Union - объединение для объектов с исключением дубликатов 1");
            foreach (var x in d1.Union(d1_for_distinct, new ProjectEqualityComparer()))
                Console.WriteLine(x);
            Console.WriteLine("Union - объединение для объектов с исключением дубликатов 2");
            foreach (var x in d1.Union(d1_for_distinct).Union(d2).Distinct(new ProjectEqualityComparer()))
                Console.WriteLine(x);

            Console.WriteLine("Concat - объединение без исключения дубликатов");
            foreach (var x in i1.Concat(i2))
                Console.WriteLine(x);
            Console.WriteLine("SequenceEqual - проверка совпадения элементов и порядка их следования");
            Console.WriteLine(i1.SequenceEqual(i1));
            Console.WriteLine(i1.SequenceEqual(i2));


            Console.WriteLine("Intersect - пересечение множеств");
            foreach (var x in i1.Intersect(i2))
                Console.WriteLine(x);
            Console.WriteLine("Intersect - пересечение множеств для объектов");
            foreach (var x in d1.Intersect(d1_for_distinct, new ProjectEqualityComparer()))
                Console.WriteLine(x);

            Console.WriteLine("Except - вычитание множеств");
            foreach (var x in i1.Except(i2))
                Console.WriteLine(x);

            Console.WriteLine("Except - вычитание множеств для объектов");
            foreach (var x in d1.Except(d1_for_distinct, new ProjectEqualityComparer()))
                Console.WriteLine(x);

            Console.WriteLine("Функции агрегирования");
            Console.WriteLine("Count - количество элементов");
            Console.WriteLine(d1.Count());
            Console.WriteLine("Count с условием");
            Console.WriteLine(d1.Count(x => x.id > 1));

                     
            //Sum - сумма элементов             
            //Min - минимальный элемент             
            //Max - максимальный элемент            
            //Average - среднее значение  
            Console.WriteLine("Aggregate - агрегирование значений");
            //(1, "Project2", 111, new DateTime(2015, 7, 20), new DateTime(2015, 8, 30), new List<string>() { "Person1", "Person2" })
            var qa1 = d1.Aggregate(new Project(0, "", 0, new DateTime(), new DateTime(), new List<string>()), (total, next) =>
            {
                if (next.id > 1) total.id += next.id;
                return total;
            });
            Console.WriteLine(qa1);
            Console.WriteLine("Группировка");
            var q16 = from x in d1.Union(d2) group x by x.name into g select new { Key = g.Key, Values = g };
            foreach (var x in q16)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x.Values)
                    Console.WriteLine("   " + y);
            }
            Console.WriteLine("Группировка с функциями агрегирования");
            var q17 = from x in d1.Union(d2)
                      group x by x.name into g
                      select new { Key = g.Key, Values = g, cnt = g.Count(), cnt1 = g.Count(x => x.id > 1), sum = g.Sum(x => x.id), min = g.Min(x => x.id) };
            foreach (var x in q17)
            {
                Console.WriteLine(x);
                foreach (var y in x.Values)
                    Console.WriteLine("   " + y);
            }
            Console.WriteLine("Группировка - Any");
            var q18 = from x in d1.Union(d2)
                      group x by x.name into g
                      where g.Any(x => x.id > 3)
                      select new { Key = g.Key, Values = g };
            foreach (var x in q18)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x.Values)
                    Console.WriteLine("   " + y);
            }
            Console.WriteLine("Группировка - All");
            var q19 = from x in d1.Union(d2)
                      group x by x.name into g
                      where g.All(x => x.id > 1)
                      select new { Key = g.Key, Values = g };
            foreach (var x in q19)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x.Values)
                    Console.WriteLine("   " + y);
            }

            Console.WriteLine("Имитация связи много-ко-многим");
            var lnk1 = from x in d1
                       join l in lnk on x.id equals l.d1 into temp
                       from t1 in temp
                       join y in d2 on t1.d2 equals y.id into temp2
                       from t2 in temp2
                       select new { id1 = x.id, id2 = t2.id };
            foreach (var x in lnk1)
                Console.WriteLine(x);

            Console.WriteLine("Имитация связи много-ко-многим, проверка условия");
            var lnk2 = from x in d1
                       join l in lnk on x.id equals l.d1 into temp
                       from t1 in temp
                       join y in d2 on t1.d2 equals y.id into temp2
                       where temp2.Any(t => t.value == 24)
                       select x;
            foreach (var x in lnk2)
                Console.WriteLine(x);

            Console.WriteLine("Имитация связи много-ко-многим, использование let, проверка условия");
            var lnk3 = from x in d1
                       let temp1 = from l in lnk where l.d1 == x.id select l
                       from t1 in temp1
                       let temp2 = from y in d2
                                   where y.id == t1.d2 && y.value == 24
                                   select y
                       where temp2.Count() > 0
                       //let temp2 = from y in d2 where y.id == t1.d2          
                       //            select y                     
                       //where temp2.Any(t=>t.value == "24")                                                 
                       select x;
            foreach (var x in lnk3) Console.WriteLine(x);

            Console.WriteLine("Deferred Execution - відкладення виконання запроса");
            var e1 = from x in d1 select x;
            Console.WriteLine(e1.GetType().Name);
            foreach (var x in e1)
                Console.WriteLine(x);
            Console.WriteLine("При зміні джерела данних запрос видає нові результати");
            d1.Add(new Project(333, "", 0, new DateTime(), new DateTime(), new List<string>()));
            foreach (var x in e1)
                Console.WriteLine(x);

            Console.WriteLine("Immediate Execution - негайне виконання запроса, результат преобразуется в список ");
            var e2 = (from x in d1 select x).ToList();
            Console.WriteLine(e2.GetType().Name);
            foreach (var x in e2)
                Console.WriteLine(x);
            Console.WriteLine("Результат преобразуется в масив");
            var e3 = (from x in d1 select x).ToArray();
            Console.WriteLine(e3.GetType().Name);
            foreach (var x in e3)
                Console.WriteLine(x);
            Console.WriteLine("Результат преобразуется в Dictionary");
            var e4 = (from x in d1 select x).ToDictionary(x => x.id);
            Console.WriteLine(e4.GetType().Name);
            foreach (var x in e4)
                Console.WriteLine(x);
            Console.WriteLine("Результат преобразуется в Lookup");
            var e5 = (from x in d1_for_distinct select x).ToLookup(x => x.id);
            Console.WriteLine(e5.GetType().Name);
            foreach (var x in e5)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x)
                    Console.WriteLine("   " + y);
            }

            Console.WriteLine("Отримання першого елемента з вибірки");
            var f1 = (from x in d2 select x).First(x => x.id == 2);
            Console.WriteLine(f1);
            Console.WriteLine("Отримання першого елемента чи значення по умолчанию");
            var f2 = (from x in d2 select x).FirstOrDefault(x => x.id == 22);
            Console.WriteLine(f2 == null ? "null" : f2.ToString());

            Console.WriteLine("Отримання елементів в заданій позиції");
            var f3 = (from x in d2 select x).ElementAt(2);
            Console.WriteLine(f3);

            Console.WriteLine("Генрація послідовності");
            Console.WriteLine("Range");
            foreach (var x in Enumerable.Range(1, 5))
                Console.WriteLine(x);
            Console.WriteLine("Repeat");
            foreach (var x in Enumerable.Repeat<int>(10, 3))
                Console.WriteLine(x);
            Console.ReadLine();
        }

        /// <summary>   
        /// Отримання потрібної сторінки даних        
        /// </summary>     
        static List<Project> GetPage(List<Project> Project, int pageNum, int pageSize)
        {
            //Кількість пропускних елементів            
            int skipSize = (pageNum - 1) * pageSize;
            var q = Project.OrderBy(x => x.id).Skip(skipSize).Take(pageSize);
            return q.ToList();
        }
    }
}

