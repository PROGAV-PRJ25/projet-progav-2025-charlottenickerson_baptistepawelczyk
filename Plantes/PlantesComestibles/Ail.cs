public class Ail:PlanteComestible {

    public Ail(Terrain terrain) : base(terrain) {
        Nom ="Ail";
        Saisons = new List<string> {"Automne"};
        TerrainPrefere = "Plaine";
        Espacement = 2;
        VitesseCroissance = 8; // mois
        BesoinsEnEau = 2; // moyens
        BesoinsEnLuminosite = 3; // forte
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture du collet"};
        ProbabilitesMaladies = new List<int> {20, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}