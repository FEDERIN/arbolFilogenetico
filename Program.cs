namespace arbolFilogenetico
{
    class Sample
    {

        public static void pintar(Arbol arbol)
        {
            Console.WriteLine(ntab(arbol.nivel-1)+arbol.id+ " " + arbol.descripcion);
            if(arbol.arbols != null && arbol.arbols.Count > 0)
            {
                foreach(Arbol arol in arbol.arbols)
                {
                    pintar(arol);
                }
            }
        }

        public static String ntab(int nivel)
        {
            string tab = "";
            for(int i = 0; i < nivel; i++)
            {
                tab=tab+"\t";
            }
            return tab;
        }

        public static void Main()
        {
            Console.Clear();
            Console.WriteLine("Escriba el nodo a buscar: ");
            String arbolBuscar = Console.ReadLine();

            if (arbolBuscar != null && arbolBuscar != "")
            {
                String line;
                try
                {
                    StreamReader sr = new StreamReader("C:\\Sample.txt");
                    line = sr.ReadLine();

                    List<Arbol> arbolList = new List<Arbol>();

                    while (line != null)
                    {
                        int i = line.IndexOf(","); //SEPARAR EL ID DEL DETALLE
                        if (i >= 0)
                        {
                            String id = line.Substring(0, i);
                            String descripcion = line.Substring(i + 1);

                            Arbol arbol = new Arbol();
                            if (id.IndexOf(arbolBuscar) == 0)
                            {
                                arbol.id = id;
                                arbol.descripcion = descripcion;
                                int j = id.LastIndexOf(".");
                                
                                if (j >= 0)
                                {
                                    String nodoPadre = id.Substring(0, j);
                                    arbol.padre = nodoPadre;
                                    string[] ArrayNivel = id.Split('.');
                                    arbol.nivel = ArrayNivel.Length;
                                }
                                else
                                {
                                    arbol.padre = "";
                                    arbol.nivel = 1;

                                }
                                arbolList.Add(arbol);
                            }
                        }
                        //Read the next line
                        line = sr.ReadLine();
                    }
                    //close the file
                    arbolList.Sort((x, y) => y.padre.CompareTo(x.padre));

                    List<Arbol> arbolList2 = new List<Arbol>();
                    List<List<Arbol>> arbolList3 = new List<List<Arbol>>();
                    if(arbolList.Count >0)
                    {
                        String padreActual = arbolList.First().padre;

                        foreach (Arbol element in arbolList)
                        {

                            if (element.id != arbolBuscar)
                            {
                                if (padreActual.CompareTo(element.padre) != 0)
                                {
                                    if (padreActual.CompareTo(element.id) == 0)
                                    {
                                        element.arbols = arbolList2;
                                        arbolList2 = new List<Arbol>();
                                        arbolList2.Add(element);
                                        padreActual = element.padre;
                                    }
                                    else
                                    {
                                        int pos = 0;
                                        bool existe = false;

                                        if (arbolList3.Count > 0)
                                        {
                                            foreach (List<Arbol> element2 in arbolList3)
                                            {
                                                if (element2.First().padre.CompareTo(element.id) == 0)
                                                {
                                                    existe = true;
                                                    element.arbols = element2;
                                                    break;
                                                }
                                                pos++;
                                            }
                                            if (existe) arbolList3.RemoveAt(pos);
                                        }

                                        arbolList3.Add(arbolList2);
                                        arbolList2 = new List<Arbol>();
                                        arbolList2.Add(element);
                                        padreActual = element.padre;
                                    }

                                }
                                else
                                {
                                    int pos = 0;
                                    bool existe = false;
                                    if (arbolList3.Count > 0)
                                    {
                                        foreach (List<Arbol> element2 in arbolList3)
                                        {
                                            if (element2.First().padre.CompareTo(element.id) == 0)
                                            {
                                                existe = true;
                                                element.arbols = element2;
                                                break;
                                            }
                                            pos++;
                                        }
                                        if (existe) arbolList3.RemoveAt(pos);
                                    }

                                    arbolList2.Add(element);
                                }
                            }
                            else
                            {
                                element.arbols = arbolList2;
                                pintar(element);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nodo no encontrado para:" + arbolBuscar);
                    }


                    sr.Close();

                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
            } 
        }
    }
}