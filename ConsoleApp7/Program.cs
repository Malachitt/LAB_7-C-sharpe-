using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp7
{
	interface IComparer //Интерфейс для цены
	{
		int Compare(object o1, object o2);
	}
	enum Days           //Перечесление
	{
		Mondey,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday
	}
	struct User         //Структура
	{
		public int Ball { get; set; }
		public User(int ball)
		{
			Ball = ball;
		}
	}
	interface IInteres  //реализация интерфейса
	{
		int Sum { get; set; }
		void Operation(int x, int y);
	}
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Tovar printerr1 = new Printerr("Canon", 1000, "Принтер", "Цветной", 2);
				Tovar printerr2 = new Printerr("Hp", 2000, "Принтер", "Цветной", 2);
				Printerr printerr = printerr1 as Printerr;                              //идентификация типа объекта с помощью оператора as
				printerr.TypePrinter = "Ч/Б";                                           //Теперь возможно обращение

				Tovar scaner1 = new Scaner("Canon", 500, "Сканер", 4);
				Tovar scaner2 = new Scaner("Hp", 700, "Сканер", 4);

				Tovar computer1 = new Computer("Lenovo", 5000, "Ноутбук", 10);
				Tovar computer2 = new Computer("Hp", 4000, "Ноутбук", 10);

				Tovar planshet1 = new Planshet("Lenovo", 1500, "Планшет", 2);
				Tovar planshet2 = new Planshet("Samsung", 1800, "Планшет", 2);

				Laboratornaya ms1 = new Laboratornaya(new List<object>());
				ms1.Add1(printerr1);
				ms1.Add1(printerr2);
				ms1.Add1(scaner1);
				ms1.Add1(scaner2);
				ms1.Add1(computer1);
				ms1.Add1(computer1);
				ms1.Write1(ms1.Set);

				Console.WriteLine("\n");

				printerr1.Display();
				printerr2.Display();

				Console.WriteLine("\n");

				//iAmPrinting
				Interes interes = new Interes();
				Printer printer = new Printer();
				object printer1 = printer.iAmPrinting(interes);
				Console.WriteLine("Вызов метод iAmPrinting - " + printer);

				Console.WriteLine("\n");

				string s = printerr1.ToString();
				string s1 = computer1.ToString();
				string s2 = interes.ToString();
				Console.WriteLine("Вызов переопределенного метода ToString() printerr1 - " + s);
				Console.WriteLine("Вызов переопределенного метода ToString() computer1 - " + s1);
				Console.WriteLine("Вызов переопределенного метода ToString() interes - " + s2);

				Console.WriteLine("\n");

				interes.Operation(5, 7); //assert
				int operation_int = interes.Operation(5);
				Console.WriteLine("Результат первого метода - " + interes.Sum + " -- два одноименных метода с различной реализацией -- результат второго метода - " + operation_int);

				Console.WriteLine("\n");

				//Массив объектов с использованием метода iAmPrinting
				List<Tovar> mas1 = new List<Tovar>() { printerr1, scaner1, computer1, planshet1 };
				foreach (object x in mas1)
				{
					Console.WriteLine(" Массив объектов с использованием метода iAmPrinting - " + printer.iAmPrinting(x));
				}

				Console.WriteLine("\n");

				//Вывод техники старше заданного срока службы и в порядке убывания цены
				Tovar[] mas3 = new Tovar[] { printerr1, scaner1, computer1, planshet1 };
				Tovar[] mas2 = new Tovar[] { };
				Console.Write("Введите срок службы техники: ");
				int x1 = Convert.ToInt32(Console.ReadLine());
				Array.Sort(mas3, new PeopleComparer());
				Console.WriteLine("Вывод техники старше заданного срока службы и в порядке убывания цены: ");
				foreach (Technology i in mas3)
				{
					if (i.LifeTime > x1)
					{
						Console.WriteLine($"{i.Name} - {i.Price} - {i.LifeTime}");
					}
					else continue;
				}

				Console.WriteLine("\n");

				Console.ReadKey();
			}
			catch (PersonDivideByZero ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch (PersonNullReference ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch (PersonException ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			finally
			{
				Console.WriteLine("Блок finally.");
			}
			Console.WriteLine("Конец программы!");
		}
	}
	abstract class Tovar               //абстрактный класс
	{
		public abstract void Display();     //абстрактный метод
		public int LifeTime { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public Tovar(string name, int price, int lifeTime)
		{
			if (name == null)
			{
				throw new PersonNullReference("Укажите name");
			}
			else if (name.Trim() == "")
			{
				throw new PersonNullReference("Укажите name");
			}
			else
			{
				LifeTime = lifeTime;
				Name = name;
				Price = price;
			}
		}
		public override string ToString()
		{
			return "Тип объекта Name - " + Name.GetType() + ", его значение - " + Name;
		}
	}
	class PeopleComparer : IComparer<Tovar>     //Сортировка убывание цен
	{
		public int Compare(Tovar p1, Tovar p2)
		{
			if (p1.Price < p2.Price)
				return 1;
			else if (p1.Price > p2.Price)
				return -1;
			else
				return 0;
		}
	}
	class Technology : Tovar        //наследование
	{
		public string TypeOfTechnology { get; set; }
		public Technology(string name, int price, string type_of_technology, int lifeTime) : base(name, price, lifeTime)
		{
			if (type_of_technology.Trim() != "")
			{
				TypeOfTechnology = type_of_technology;
			}
		}
		public override void Display()
		{
			Console.WriteLine($"Имя - {Name}, цена - {Price}, тип техники - {TypeOfTechnology}, срок жизни - {LifeTime} .");
		}
		public override string ToString()
		{
			return "Тип объекта Name - " + Name.GetType() + ", его значение - " + Name;
		}
	}
	class Printerr : Technology
	{
		public string TypePrinter { get; set; }
		public Printerr(string name, int price, string type_of_technology, string typePrinter, int lifeTime) : base(name, price, type_of_technology, lifeTime)
		{
			TypePrinter = typePrinter;
		}
		public override string ToString()
		{
			return "Тип объекта Name - " + Name.GetType() + ", его значение - " + Name;
		}
		public override void Display()
		{
			Console.WriteLine($"Имя - {Name}, цена - {Price}, тип техники - {TypeOfTechnology}, тип принтера - {TypePrinter}, срок жизни - {LifeTime} .");
		}
	}
	class Scaner : Technology
	{
		public Scaner(string name, int price, string type_of_technology, int lifeTime) : base(name, price, type_of_technology, lifeTime)
		{

		}
		public override string ToString()           //переопределение метода
		{
			return "Тип объекта Name - " + Name.GetType() + ", его значение - " + Name;
		}
		public override void Display()
		{
			Console.WriteLine($" Имя - {Name}, цена - {Price}, тип техники - {TypeOfTechnology}, срок жизни - {LifeTime} .");
		}
	}
	class Computer : Technology
	{
		public Computer(string name, int price, string type_of_technology, int lifeTime) : base(name, price, type_of_technology, lifeTime)
		{

		}
		public override void Display()
		{
			Console.WriteLine($" Имя - {Name}, цена - {Price}, тип техники - {TypeOfTechnology}, срок жизни - {LifeTime} .");
		}
	}
	class Planshet : Technology
	{
		public Planshet(string name, int price, string type_of_technology, int lifeTime) : base(name, price, type_of_technology, lifeTime)
		{

		}
		//Переопределение методов Object
		public override string ToString()
		{
			if (String.IsNullOrEmpty(Name))
				return base.ToString();
			return Name;
		}
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			if (obj.GetType() != this.GetType()) return false;

			Technology technology = (Technology)obj;
			return this.Name == technology.Name;
		}
		public override void Display()
		{
			Console.WriteLine($" Имя - {Name}, цена - {Price}, тип техники - {TypeOfTechnology}, срок жизни - {LifeTime} .");
		}
	}
	sealed class Interes : IInteres //бесплодный класс				//assert
	{
		public int Sum { get; set; }
		public void Operation(int x, int y)
		{
			Sum = x + y;
			Debug.Assert(x > 0);                                    //assert
		}
		public int Operation(int x)
		{
			return ++x;
		}
		public override string ToString()
		{
			return "Тип объекта Sum - " + Sum.GetType() + ", его значение - " + Sum;
		}
	}
	class Printer
	{
		public object iAmPrinting(object x)
		{
			return "Тип объекта - " + x.GetType() + ", вызов метода ToString - " + x.ToString();
		}
	}
	class Laboratornaya
	{
		public List<object> Set { get; set; }

		public Laboratornaya()                      //конструктор
		{
			Set = new List<object>();
		}
		public Laboratornaya(List<object> set)
		{
			Set = set;
		}
		public void Add1(object x)					//Добавить
		{
			if (x == null)
			{
				throw new PersonNullReference("Укажите name ");
			}
			else if (x.ToString().Trim() == "")
			{
				throw new PersonNullReference("Укажите название товара который хотите добавить!");
			}
			else
			{
				Set.Add(x);
			}
		}
		public void Remove1(object list1)           //Удаление
		{
			Set.Remove(list1);
		}
		public void Write1(List<object> list1)      //Вывод
		{
			string s1 = "";
			foreach (object s in list1)
			{
				if (s == null)
				{
					continue;
				}
				else
				{
					s1 += " " + s + "\n";
				}
			}
			Console.WriteLine("Список техники: " + "\n" + s1);
		}
		public override string ToString()   //переопределение метода
		{
			string s = "";
			foreach (string val in Set)
			{
				if (val == null)
				{
					continue;
				}
				else
				{
					s += val + "; ";
				}
			}

			return s + " . ";
		}
	}
	class PersonException : Exception
	{
		public PersonException(string message)
			: base(message)
		{ }
	}
	class PersonDivideByZero : DivideByZeroException
	{
		public PersonDivideByZero(string message) : base(message)
		{ }
	}
	class PersonNullReference : NullReferenceException
	{
		public PersonNullReference(string message) : base(message)
		{ }
	}
}
