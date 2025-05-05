public class Epinard:PlanteComestible {

    public Epinard(Terrain terrain) : base(terrain) {
        Nom ="Épinard";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 3;
        VitesseCroissance = 1;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"peronospora", "mildiou"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}