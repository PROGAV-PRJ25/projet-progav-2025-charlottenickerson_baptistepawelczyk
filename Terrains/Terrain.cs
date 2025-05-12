public abstract class Terrain
{
    protected string Nom;
    protected double SurfaceTotale;
    protected string Type; // Plaine, Desert, Cratere, Jungle, Montagne, Marais, Foret, Prairie, Riviere
    protected List<Plante> Plantes;
    protected double QualiteSol;

    public Terrain(string nom)
    {
        Nom = nom;
        SurfaceTotale = 9;
        Type = "Inconnu";
        Plantes = new List<Plante> {};
        QualiteSol = 0;
    }

    public override string ToString()
    {
        string message = $"{Nom.ToUpper()} est de type {Type}.";
        message += $"\nSa surface totale est de {SurfaceTotale}m² et la qualité de son sol est {QualiteSol}.";
        message += $"\nLes plantes sur le terrain sont les suivantes :\n";
        foreach (Plante plante in Plantes)
            message += $"{plante.Nom} • ";
        return message;
    }

    public void AjouterPlante(Plante plante)
    {
        Plantes.Add(plante);
    }

    // bool PeutPlanter(Plante plante);
}