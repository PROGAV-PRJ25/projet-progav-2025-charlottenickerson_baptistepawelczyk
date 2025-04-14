abstract class Terrain
{
    string Nom;
    float SurfaceTotale;
    TypeTerrain Type; // Enum : Plaine, Desert, Cratere, Jungle, Montagne, Marais, Foret, Prairie, Riviere
    List<Plante> Plantes;
    float QualiteSol;

    bool PeutPlanter(Plante plante);
    void AjouterPlante(Plante plante);
}

