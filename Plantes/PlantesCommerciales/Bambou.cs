public class Bambou:PlanteCommerciale {

    public Bambou(Terrain terrain) : base(terrain) {
        Nom ="Bambou";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Jungle";
        Espacement = 4;
        VitesseCroissance = 0.1;
        BesoinsEnEau = 4;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 30};
        MaladiesPossibles = new List<string> {"rouille", "champignons racinaires"};
        ProbabilitesMaladies = new List<int> {25, 35};
        EsperanceDeVie = 2.5;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}