public class Sauge:PlanteAromatique {

    public Sauge(Terrain terrain) : base(terrain) {
        Nom ="Sauge";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Montagne";
        Espacement = 2;
        VitesseCroissance = 0.167;
        BesoinsEnEau = 0.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {18, 30};
        MaladiesPossibles = new List<string> {"oïdium", "rouille"};
        ProbabilitesMaladies = new List<int> {15, 20};
        EsperanceDeVie = 1.25;
        Rendement = 7;
        EtatPlante ="Germination";
    }
}