public class Chou_de_Bruxelles:PlanteComestible {

    public Chou_de_Bruxelles(Terrain terrain) : base(terrain) {
        Nom ="Chou de Bruxelles";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 6;
        VitesseCroissance = 4;
        BesoinsEnEau = 4;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 25};
        MaladiesPossibles = new List<string> {"mildiou", "pucerons"};
        ProbabilitesMaladies = new List<int> {25, 10};
        EsperanceDeVie = 1;
        Rendement = 8;
        EtatPlante ="Germination";
    }
}