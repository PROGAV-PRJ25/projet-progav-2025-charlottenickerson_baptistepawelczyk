public class Carotte:PlanteComestible {

    public Carotte(Terrain terrain) : base(terrain) {
        Nom ="Carotte";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Prairie";
        Espacement = 3;
        VitesseCroissance = 3;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture des racines"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}