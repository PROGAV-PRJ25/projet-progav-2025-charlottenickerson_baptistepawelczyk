public class Menthe:PlanteAromatique {

    public Menthe(Terrain terrain) : base(terrain) {
        Nom ="Menthe";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Marais";
        Espacement = 3;
        VitesseCroissance = 0.1;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {12, 28};
        MaladiesPossibles = new List<string> {"rouille", "pucerons"};
        ProbabilitesMaladies = new List<int> {25, 40};
        EsperanceDeVie = 1;
        Rendement = 17;
        EtatPlante ="Germination";
    }
}