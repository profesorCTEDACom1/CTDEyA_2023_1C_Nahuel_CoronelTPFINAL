
using System;
using System.Collections.Generic;
using tp1;

namespace tpfinal
{

	public class Estrategia
	{
		private int CalcularDistancia(string str1, string str2)
		{
			// using the method
			String[] strlist1 = str1.ToLower().Split(' ');
			String[] strlist2 = str2.ToLower().Split(' ');
			int distance = 1000;
			foreach (String s1 in strlist1)
			{
				foreach (String s2 in strlist2)
				{
					distance = Math.Min(distance, Utils.calculateLevenshteinDistance(s1, s2));
				}
			}

			return distance;
		}

        public String Consulta1(ArbolGeneral<DatoDistancia> arbol) //PUNTO 3
        {
            string oracion1 = "Esta es la oracion que se formo:"; //creo una oracion inicial
            string oracion2 = ""; //creo una oracion donde voy a agrupar los texto de cada nodo
            if (arbol.esHoja())
            {
                oracion2 = oracion2 + " " + arbol.getDatoRaiz().texto;
            }
            else
            {
                Cola<ArbolGeneral<DatoDistancia>> cola = new Cola<ArbolGeneral<DatoDistancia>>(); //creo una cola
                ArbolGeneral<DatoDistancia> arbolaux; // creo un arbol auxiliar que servira de puntero
                cola.encolar(arbol); //encolo el arbol
                while (!cola.esVacia()) //mientras la cola no se vacie
                {
                    arbolaux = cola.desencolar(); // desencolo el arbol en el arbol auxiliar
                    foreach (var hijo in arbolaux.getHijos()) //tomo la lista de hijos
                    {
                        if (hijo.esHoja()) //si ese hijo es hoja
                        {
                            oracion2 = oracion2 + " " + hijo.getDatoRaiz().texto; //concatena todos los texto de las hojas
                        }
                        cola.encolar(hijo);//encola el hijo para seguir buscando mas hojas
                    }
                }
            }

            string oracion = oracion1 + oracion2;//concatena la oracion inial con el texto de las hojas
            return oracion; //retorna la oracion
        }


        public String Consulta2(ArbolGeneral<DatoDistancia> arbol) //PUNTO 4
        {
            string oracion1 = "camino hacia las hojas"; //creo oracion inial
            string oracion = ""; //creo una oracion donde se van a guardar el texto de cada nodo hacia las hojas
            if (arbol.esHoja())
            { //pregunto si es hoja
                oracion = oracion + " , " + arbol.getDatoRaiz().texto; //concatena el texto del arbol pasado como parametro
            }
            else
            { //si no es
                Cola<ArbolGeneral<DatoDistancia>> cola = new Cola<ArbolGeneral<DatoDistancia>>(); //creo una cola
                ArbolGeneral<DatoDistancia> arbolaux; // creo un arbol auxiliar que servira de puntero
                cola.encolar(arbol); //encolo el arbol
                oracion = oracion + " , " + arbol.getDatoRaiz().texto; //concatena el texto del arbol pasado como parametro
                while (!cola.esVacia())
                { //mientras la cola no se vacie
                    arbolaux = cola.desencolar(); // desencolo el arbol en el arbol auxiliar
                    foreach (var hijo in arbolaux.getHijos())
                    { //tomo la lista de hijos
                        oracion = oracion + " , " + hijo.getDatoRaiz().texto; //concatena todos los texto    
                        cola.encolar(hijo);//encola el hijo 
                    }
                }
            }
            return oracion; //retorna oracion
        }



        public String Consulta3(ArbolGeneral<DatoDistancia> arbol) // PUNTO 5
        {
            DatoDistancia datoaux = new DatoDistancia(999, "AAA", "AAA"); //creo un DatoDistancia que va a funcionar como flag
            ArbolGeneral<DatoDistancia> contador = new ArbolGeneral<DatoDistancia>(datoaux); //arbolgeneral del DatoDistancia para que la cola lo admita
            int nivel = -1; //contador de niveles, inicializado en -1 para que al menos haga la iteracion en el nivel 0
            string oracion1 = "Esta es la oracion que se formo: "; //creo una oracion inial
            string oracion2 = ""; //creo una oracion donde se van a guardar el texto de cada nodo hacia las hojas
            if (arbol.esHoja()) //pregunto si es hoja
            {
                nivel++; //subo el nivel para que quede en 0
                oracion2 = oracion2 + "nivel: " + nivel + " ,Distancia: " + arbol.getDatoRaiz().distancia + " ,Texto: " + arbol.getDatoRaiz().texto + " ,Descripcion: " + arbol.getDatoRaiz().descripcion; //concateno la oracion con los datos del DatoDistancia
            }
            else//si no es
            {
                nivel++; //subo el nivel
                Cola<ArbolGeneral<DatoDistancia>> cola = new Cola<ArbolGeneral<DatoDistancia>>();  //creo una cola
                ArbolGeneral<DatoDistancia> arbolaux;  // creo un arbol auxiliar que servira de puntero
                cola.encolar(arbol); //encolo el arbol pasado como parametro
                cola.encolar(contador); //encolo el flag
                while (!cola.esVacia()) //mientras la cola no este vacia
                {
                    arbolaux = cola.desencolar(); //desencolo el primer elemento de la cola
                    if (arbolaux != contador) //comparo si es distinto al flag
                    {
                        oracion2 = oracion2 + " Nivel: " + nivel + " ,Distancia: " + arbolaux.getDatoRaiz().distancia + " ,Texto: " + arbolaux.getDatoRaiz().texto + " ,Descripcion: " + arbolaux.getDatoRaiz().descripcion; //concateno la oracion con los datos del DatoDistancia
                        foreach (var hijo in arbolaux.getHijos())
                        { //pido la lista de hijos
                            cola.encolar(hijo); //los encolo
                        }
                    }
                    else
                    { //si es igual al flag 
                        nivel++; //subo el nivel
                        if (!cola.esVacia())
                        { //pregunto si la cola esta vacia
                            cola.encolar(contador); //encolo un flag
                        }
                    }
                }
            }
            string oracion = oracion1 + oracion2; //concateno las oraciones
            return oracion; //retorno las oraciones
        }

        public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato) //PUNTO 1
        {
            ArbolGeneral<DatoDistancia> nuevonodo = new ArbolGeneral<DatoDistancia>(dato); //se crea un arbol genera con el DatoDistancia
            arbol.agregarHijo(nuevonodo); //se agrega ese arbol al arbol que se paso como parametro
        }

        public void Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> collected) //PUNTO 2
        {
            int nivel = -1; //contador de niveles, inicializado en -1 para que al menos haga la iteracion en el nivel 0
            string oracion = ""; //string que va imprimir por pantalla
            DatoDistancia datoaux = new DatoDistancia(999, "AAA", "AAA"); //DatoDistancia que va a actuar como un flag
            ArbolGeneral<DatoDistancia> arbolcontador = new ArbolGeneral<DatoDistancia>(datoaux); //arbolgeneral del DatoDistancia para que la cola lo admita
            Cola<ArbolGeneral<DatoDistancia>> cola = new Cola<ArbolGeneral<DatoDistancia>>(); //creacion de la cola para recorrer el arbol general por nivles
            ArbolGeneral<DatoDistancia> arbolaux; //arbol auxiliar que nos va a servir de puntero
            cola.encolar(arbol); //encolo el arbol que pase como parametro
            while (!cola.esVacia())
            { //hasta que la cola no se termina el while no se termina
                arbolaux = cola.desencolar(); //desencolo el arbol auxiliar
                if (arbolaux.getDatoRaiz().texto == elementoABuscar)
                { //pregunto si el texto del DatoDistancia del arbol es igual al elemento a buscar
                    collected.Add(arbolaux.getDatoRaiz()); //si es igual al elemento a buscar lo agrego a la lista
                    Cola<ArbolGeneral<DatoDistancia>> cola2 = new Cola<ArbolGeneral<DatoDistancia>>(); //creo una lista para recorrer los hijos del elemento
                    ArbolGeneral<DatoDistancia> arbolaux2; //arbol auxiliar 2 que nos va a servir de puntero
                    cola2.encolar(arbolaux); //encolo el arbol que tiene el elemento
                    cola2.encolar(arbolcontador); //encolo el arbol que nos va a servir como flag
                    while (!cola2.esVacia())
                    { //pregunto si la cola esta vacia
                        arbolaux2 = cola2.desencolar(); //desencolo el arbol auxiliar2
                        if (nivel <= umbral)
                        { //si el nivel es menor o igual umbral sigo
                            if (arbolaux2 != arbolcontador)
                            { //si el arbol auxiliar 2 es diferente al flag
                                foreach (var hijo2 in arbolaux2.getHijos())
                                { //toma la lista de hijos
                                    cola2.encolar(hijo2); //los encola
                                    collected.Add(hijo2.getDatoRaiz()); //agrego el DatoDistancia a la list
                                }
                            }
                            else
                            { //si es igual al flag
                                nivel++; //sube 1 el contador de nivel
                                if (!cola2.esVacia())
                                { //mientas la cola2 no este vacia 
                                    cola2.encolar(arbolcontador); //encolo otro flag
                                }
                            }
                        }
                    }
                }
                else
                { //si no es igual 
                    foreach (var hijo in arbolaux.getHijos())
                    { //tomo la lista de hijos
                        cola.encolar(hijo); //y los encolo
                    }
                }
            }
            if (collected.Count == 0)
            { //no se encontro el elemento por lo tanto, no hay ningun elemento en la list
                Console.WriteLine("El elemento no se encontro"); //imprime por pantalla el aviso
            }
            foreach (var ele in collected)
            { //se recorre la lista
                oracion = oracion + ele.texto + " , "; //se forma la oracion con los textos de cada DatoDistancia
            }
            Console.WriteLine(oracion); //imprime la oracion formada
        }


    }
}