class Joueur
{
    string Nom;
    List<Terrain> TerrainsPossedes;
    float Argent;
    Inventaire Inventaire;
    List<ActionJoueur> ActionsDisponibles;

    void EffectuerAction(ActionJoueur action);
    void Vendre(Recolte recolte);
    void Acheter(Outil outil);
}