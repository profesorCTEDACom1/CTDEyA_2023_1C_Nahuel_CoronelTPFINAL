// See https://aka.ms/new-console-template for more information
using MyApp;
using tp1;


if (args == null || args.Length==0)
{
    Console.WriteLine("Indique la ruta al archivo .csv de entrada");
}
else
{
    string file = args[0];
    ArbolGeneral<DatoDistancia> arbol = new ArbolGeneral<DatoDistancia>(new DatoDistancia(0, ".", " "));
    Estrategia estrategia = new Estrategia();
    if (!File.Exists(file))
    {
        Console.WriteLine("El archivo: {0} no fue encontrado", file);

    }
    else {
        Console.WriteLine("Cargando el archivo: {0}", file);

        using (var reader = new StreamReader(@file))
        {
            string line = reader.ReadLine();
            string[] fields = line.Split(',');
            string titulo = Utils.RemoveSpecialCharacters(fields[1]);
            string descript = Utils.RemoveSpecialCharacters(fields[2]);
            arbol = new ArbolGeneral<DatoDistancia>(new DatoDistancia(0, titulo, descript));
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                fields = line.Split(',');
                titulo = Utils.RemoveSpecialCharacters(fields[1]);
                descript = Utils.RemoveSpecialCharacters(fields[2]);
                estrategia.AgregarDato(arbol, new DatoDistancia(0, titulo, descript));
            }
        }
    
        Console.WriteLine("Indique la Opcion: 1->'Buscar' - 2->'Consulta 1' - 3-> 'Consulta 2' - 4->'Consulta 3' 0->Salir");
        string val = Console.ReadLine();
        int opcion = 0;

        try
        {
            opcion = Convert.ToInt32(val);
            while (opcion!=0)
            {
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Indique la frase a buscar:");
                        string frase = Console.ReadLine();
                        Console.WriteLine("Indique la tolerancia:");
                        int tolerancia = Convert.ToInt32(Console.ReadLine());
                        List<DatoDistancia> lista = new List<DatoDistancia>();
                        estrategia.Buscar(arbol, frase, tolerancia, lista);
                        foreach (var item in lista)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case 2:
                        Console.WriteLine(estrategia.Consulta1(arbol));
                        break ;
                    case 3:
                        Console.WriteLine(estrategia.Consulta2(arbol));
                        break;
                    case 4:
                        Console.WriteLine(estrategia.Consulta3(arbol));
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Indique la Opcion: 1->'Buscar' - 2->'Consulta 1' - 3-> 'Consulta 2' - 4->'Consulta 3' 0->Salir");
                val = Console.ReadLine();
                opcion = Convert.ToInt32(val);
            }
        }
        catch (Exception)
        {

            Console.WriteLine("Se a producido un error con la opcion ingresada: {0}", opcion);
        }
    }

}
