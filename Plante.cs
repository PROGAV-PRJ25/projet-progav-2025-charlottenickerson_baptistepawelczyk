abstract class Plante
{
    string Nom;
    TypePlante Type; // Enum : Comestible, Ornementale, Commerciale, MauvaiseHerbe
    Saison[] SaisonsSemis;
    TypeTerrain TerrainFavori;
    float Espacement;
    float SurfaceNecessaire;
    float VitesseCroissance;
    Besoins BesoinsEnvironnementaux;
    List<Maladie> MaladiesPossibles;
    int EsperanceDeVie;
    int Rendement;
    EtatPlante Etat; // Enum : Germination, Croissance, Mûre, Morte

    void Pousser(ConditionsEnvironnementales conditions);
    bool EstMorte();
}