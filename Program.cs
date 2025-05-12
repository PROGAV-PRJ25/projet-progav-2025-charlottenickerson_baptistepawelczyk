/*namespace projstartover
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une instance de la classe Temps
            Temps temps = new Temps();
            
            // Démarrage de la boucle de jeu
            temps.DemarrerJeu();
        }
    }
}*/

Plaine plaine = new Plaine("NomDuTerrain");
Ananas ananas = new Ananas(plaine);
Coton coton = new Coton(plaine);

Console.WriteLine(ananas);
Console.WriteLine();
Console.WriteLine(coton);
Console.WriteLine();
Console.WriteLine(plaine);