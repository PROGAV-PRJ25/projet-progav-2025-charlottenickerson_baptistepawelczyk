public class Rosier:PlanteOrnementale {

    public Rosier(Terrain terrain) : base(terrain) {
        Nom ="Rosier";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Prarie";
        Espacement = 3;
        VitesseCroissance = 0.2;
        BesoinsEnEau = 4;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {12, 25};
        MaladiesPossibles = new List<string> {"taches noires", "oïdium", "pucerons"};
        ProbabilitesMaladies = new List<int> {10, 15, 20};
        EsperanceDeVie = 2;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}