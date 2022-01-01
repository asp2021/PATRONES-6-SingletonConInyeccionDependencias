using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace SingletonConInyeccionDependencias
{
    public interface ISingletonContainer
    {
        int GetPopulation(string name);
    }

    public class SingletonDataContainer : ISingletonContainer
    {
        private Dictionary<string, int> _capitals = new Dictionary<string, int>();
        private SingletonDataContainer()
        {
            Console.WriteLine("Nueva Instancia SingletonDataContainer" + Environment.NewLine);
            var elements = File.ReadAllLines("capitals.txt");
            for (int i = 0; i < elements.Length; i += 2)
            {
                _capitals.Add(elements[i], int.Parse(elements[i + 1]));
            }
        }

        // Con Lazy se va a crear la instancia solamente cuando se necesite
        private static Lazy<SingletonDataContainer> instance = new Lazy<SingletonDataContainer>(() => new SingletonDataContainer());

        //Sin Lazy -> private static SingletonDataContainer instance = new SingletonDataContainer();
        public static SingletonDataContainer Instance => instance.Value;

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    public class SingletonFinder
    {
        private ISingletonContainer _dataContainer;

        public SingletonFinder(ISingletonContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }

        public int GetPopulation(IEnumerable<string> names)
        {
            int total = 0;
            foreach (var name in names)
            {
                total += _dataContainer.GetPopulation(name);
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("SINGLETON CON INYECCION DE DEPENDENCIA" + '\n');
            WriteLine("Crear una instancia de un objeto y poder compartirla a traves de la aplicación" + '\n');
            WriteLine("El archivo capitals.txt se entiende que nunca va a cambiar - por eso conviene aplicar Singleton" + Environment.NewLine);

            WriteLine("PARA PODER EJECUTAR LA PRUBA UNITARIA CON SingletonFinder USO INYECCION DE DEPENDENCIA" + Environment.NewLine);

            // El archivo capitals.txt se entiende que nunca va a cambiar - por eso conviene aplicar Singleton.
            // lo hago varias veces para demostrar que solo una vez se llama la instancia realmente
            var capitals = SingletonDataContainer.Instance;
            var capitals1 = SingletonDataContainer.Instance;
            var capitals2 = SingletonDataContainer.Instance;
            Console.WriteLine($"Los habitantes de Londres: {capitals.GetPopulation("London")}");
            Console.ReadLine();
        }
    }
}
