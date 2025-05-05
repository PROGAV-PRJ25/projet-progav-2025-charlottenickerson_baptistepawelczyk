public class Laitue:PlanteComestible {

    public Laitue(Terrain terrain) : base(terrain) {
        Nom ="Laitue";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Prairie";
        Espacement = 3;
        VitesseCroissance = 1;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {15, 20};
        MaladiesPossibles = new List<string> {"mildiou", "pucerons"};
        ProbabilitesMaladies = new List<int> {15, 20};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}